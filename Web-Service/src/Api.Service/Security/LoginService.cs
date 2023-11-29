using Api.Domain.Dtos.Login;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using Api.Domain.Interfaces.Services.Login;
using Microsoft.AspNetCore.Http;
using Api.Domain.Repository;
using Api.Domain.Entities;
using AutoMapper;
using Api.Service.Exceptions;

namespace Api.Service.Security
{
    public class LoginService : ILoginService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenService _refreshTokenService;

        public LoginService(IUserRepository repository,
                            IMapper mapper,
                            IHttpContextAccessor httpContextAccessor,
                            ITokenService tokenService,
                            IRefreshTokenService refreshTokenService)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = repository;
            _mapper = mapper;
            _tokenService = tokenService;
            _refreshTokenService = refreshTokenService;
        }

        /// <summary>
        /// Returns the ID of the currently authenticated user in the HTTP context.
        /// </summary>
        /// <returns cref="Guid"> User ID </returns>
        /// <exception cref="InvalidOperationException">Thrown when the user is null in HttpContext, which typically occurs when this method is accessed by an unauthenticated user.</exception>
        public Guid GetUserId() =>
                Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
                        ?? throw new InvalidOperationException("User ID not found in claims."));

        /// <summary>
        /// Returns the E-mail of the currently authenticated user in the HTTP context.
        /// </summary>
        /// <returns> User E-mail </returns>
        /// <exception cref="InvalidOperationException">Thrown when the user is null in HttpContext, which typically occurs when this method is accessed by an unauthenticated user.</exception>
        public string GetUserEmail() =>
                _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)
                    ?? throw new InvalidOperationException("User Email not found in claims.");

        public async Task<LoginDtoResult> Login(string email, string password)
        {
            var user = await _repository.FindByEmail(email);

            if (user == null)
            {
                throw new AuthenticationServiceException("User not found.", 404);
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                throw new AuthenticationServiceException("Wrong password.", 400);
            }

            string newAccessToken = _tokenService.CreateToken(user);
            var newRefreshToken = _refreshTokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken.Token;
            user.RefreshTokenExpires = newRefreshToken.Expires;

            await _repository.UpdateAsync(user);

            return new LoginDtoResult { AccessToken = newAccessToken, RefreshToken = newRefreshToken.Token };
        }

        public async Task<bool> Logout()
        {
            var result = await _repository.FindById(GetUserId());

            if (result is null)
            {
                throw new AuthenticationServiceException("User not found.", 404);
            }

            result.RefreshToken = string.Empty;
            result.RefreshTokenExpires = null;


            await _repository.UpdateAsync(result);

            return true;
        }

        public async Task<Guid> Register(RegisterDtoRequest userRequest)
        {
            if (await _repository.UserExists(userRequest.Email))
                throw new AuthenticationServiceException("User already exists.", 400);

            CreatePasswordHash(userRequest.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var newUser = new UserEntity
            {
                Name = userRequest.Name,
                Email = userRequest.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };

            await _repository.InsertAsync(newUser);

            return newUser.Id;
        }

        public async Task<bool> ChangePassword(string userId, string newPassword)
        {
            var user = await _repository.FindById(GetUserId());
            if (user == null)
            {
                throw new AuthenticationServiceException("User not found.", 404);
            }

            CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _repository.UpdateAsync(user);

            return true;
        }

        public async Task<RefreshTokenDtoResult> RefreshToken(RefreshTokenDtoRequest request)
        {
            var refreshToken = request.RefreshToken;

            var principal = _refreshTokenService.GetPrincipalFromExpiredToken(request.AccessToken);
            if (principal == null)
            {
                throw new SecurityTokenException("Invalid access token/refresh token");
            }

            string email = principal.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Email)?.Value 
                        ?? throw new InvalidOperationException("User Email not found in claims.");

            var user = await _repository.FindByEmail(email);

            if (user is null)
            {
                throw new AuthenticationServiceException("User not found", 404);
            }
            else
            {
                if (!user.RefreshToken.Equals(refreshToken))
                {
                    throw new SecurityTokenException("Invalid Refresh Token.");
                }
                else if (user.RefreshTokenExpires < DateTime.Now)
                {
                    throw new SecurityTokenException("Token expired.");
                }

                string newAccessToken = _tokenService.CreateToken(user);
                var newRefreshToken = _refreshTokenService.GenerateRefreshToken();

                user.RefreshToken = newRefreshToken.Token;
                user.RefreshTokenExpires = newRefreshToken.Expires;

                await _repository.UpdateAsync(user);
                return new RefreshTokenDtoResult { AccessToken = newAccessToken, RefreshToken = newRefreshToken.Token };
            };
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash =
                    hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
