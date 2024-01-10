using Api.Domain.Dtos.Login;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using Api.Domain.Interfaces.Services.Authentication;
using Microsoft.AspNetCore.Http;
using Api.Domain.Repository;
using Api.Domain.Entities;
using AutoMapper;
using Api.Domain.Dtos.User;
using System.Security.Authentication;
using Api.Domain.Interfaces.Services.Email;

namespace Api.Service.Security
{
    /// <summary>
    /// Service responsible for handling user authentication, registration, and token management.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;
        private readonly IAccessTokenService _tokenService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IEmailService _emailService;

        public AuthenticationService(IUserRepository repository,
                            IMapper mapper,
                            IHttpContextAccessor httpContextAccessor,
                            IAccessTokenService tokenService,
                            IRefreshTokenService refreshTokenService,
                            IEmailService emailService)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = repository;
            _mapper = mapper;
            _tokenService = tokenService;
            _refreshTokenService = refreshTokenService;
            _emailService = emailService;
        }

        /// <summary>
        /// Returns the ID of the currently authenticated user in the HTTP context.
        /// </summary>
        /// <returns cref="Guid"> User ID </returns>
        /// <exception cref="InvalidOperationException">Thrown when the user is null in HttpContext, 
        /// which typically occurs when this method is accessed by an unauthenticated user.</exception>
        public Guid GetUserId() =>
                Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
                        ?? throw new InvalidOperationException("User ID not found in claims."));

        /// <summary>
        /// Returns the E-mail of the currently authenticated user in the HTTP context.
        /// </summary>
        /// <returns> User E-mail </returns>
        /// <exception cref="InvalidOperationException">Thrown when the user is null in HttpContext, 
        /// which typically occurs when this method is accessed by an unauthenticated user.</exception>
        public string GetUserEmail() =>
                _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)
                    ?? throw new InvalidOperationException("User Email not found in claims.");

        /// <summary>
        /// Authenticates a user by verifying the provided email and password.
        /// Generates access and refresh tokens upon successful authentication.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="password">User password</param>
        /// <returns>Login result containing access token, refresh token, and user settings.</returns>
        /// <exception cref="AuthenticationException">Thrown when authentication fails.</exception>
        public async Task<LoginDtoResult> Login(string email, string password)
        {
            var user = await _repository.FindByEmail(email)
                            ?? throw new AuthenticationException("User not found.");

            if (!user.Email.EmailIsVerified) throw new AuthenticationException("Email has not been verified.");

            if (!VerifyPasswordHash(password, user.Authentication!.PasswordHash, user.Authentication!.PasswordSalt))
                throw new AuthenticationException("Wrong password.");

            string newAccessToken = _tokenService.CreateAccessToken(user);
            var newRefreshToken = _refreshTokenService.GenerateRefreshToken();

            user.Authentication!.RefreshToken = newRefreshToken.Token;
            user.Authentication!.RefreshTokenExpires = newRefreshToken.Expires;

            await _repository.UpdateAsync(user);

            return new LoginDtoResult
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken.Token,
                Settings = _mapper.Map<UserSettingsDto>(user.Settings)
            };
        }

        /// <summary>
        /// Logs out the currently authenticated user by updating the refresh token to an empty value.
        /// </summary>
        /// <returns>True if logout is successful.</returns>
        /// <exception cref="AuthenticationException">Thrown when the user is not found.</exception>
        public async Task<bool> Logout()
        {
            var result = await _repository.FindById(GetUserId())
                            ?? throw new AuthenticationException("User not found.");

            await UpdateUserRefreshToken(result, new RefreshTokenDto { Token = string.Empty, Expires = null });

            return true;
        }

        /// <summary>
        /// Registers a new user with the provided information, creating a unique user entity,
        /// generating password hash and salt, and storing the user and associated settings in repositories.
        /// </summary>
        /// <param name="userRequest">User registration data</param>
        /// <returns>The unique identifier (ID) of the newly registered user.</returns>
        /// <exception cref="AuthenticationException">Thrown if a user with the provided email already exists.</exception>
        public async Task<Guid> Register(RegisterDtoRequest userRequest)
        {
            if (await _repository.UserExists(userRequest.Email))
                throw new AuthenticationException("User already exists.");

            CreatePasswordHash(userRequest.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var newUser = new UserEntity
            {
                Name = userRequest.Name,
                Email = new UserEmailEntity
                {
                    EmailVerificationToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                    Address = userRequest.Email,
                },
                
                Authentication = new UserAuthenticationEntity
                {
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,

                },
            };

            await _repository.InsertAsync(newUser);

            await _emailService.SendEmailVerification(newUser.Email.Address, newUser.Name, newUser.Email.EmailVerificationToken);

            return newUser.Id;
        }

        public async Task EmailVerificationToken(string emailVerificationToken)
        {
            var user = await _repository.FindByEmailVerificationToken(emailVerificationToken)
                            ?? throw new SecurityTokenException("Invalid Token");

            user.Email.EmailVerifiedAt = DateTime.UtcNow;
            user.Email.EmailIsVerified = true;
            user.Email.EmailVerificationToken = null;
            await _repository.UpdateAsync(user);
        }

        public async Task<bool> ChangePassword(string newPassword)
        {
            var user = await _repository.FindById(GetUserId())
                                ?? throw new AuthenticationException("User not found.");

            CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

            user.Authentication!.PasswordHash = passwordHash;
            user.Authentication!.PasswordSalt = passwordSalt;

            await _repository.UpdateAsync(user);

            return true;
        }

        /// <summary>
        /// Refreshes an access token by validating the provided refresh token and obtaining a new access token.
        /// </summary>
        /// <param name="request">Refresh token request containing the expired access token and refresh token.</param>
        /// <returns>Result containing the newly generated access token and refresh token.</returns>
        /// <exception cref="SecurityTokenException">Thrown when the provided access token or refresh token is invalid.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the user email is not found in the authentication claims.</exception>
        /// <exception cref="AuthenticationException">Thrown when the user associated with the email is not found.</exception>
        public async Task<RefreshTokenDtoResult> RefreshToken(RefreshTokenDtoRequest request)
        {
            var refreshToken = request.RefreshToken;

            var principal = _refreshTokenService.GetPrincipalFromExpiredToken(request.AccessToken)
                                ?? throw new SecurityTokenException("Invalid access token/refresh token");

            string email = principal.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Email)?.Value
                                ?? throw new InvalidOperationException("User Email not found in claims.");

            var user = await _repository.FindByEmail(email)
                                ?? throw new AuthenticationException("User not found");

            ValidateRefreshToken(user, refreshToken);

            string newAccessToken = _tokenService.CreateAccessToken(user);
            var newRefreshToken = _refreshTokenService.GenerateRefreshToken();

            await UpdateUserRefreshToken(user, newRefreshToken);

            return new RefreshTokenDtoResult { AccessToken = newAccessToken, RefreshToken = newRefreshToken.Token };
        }

        private static void ValidateRefreshToken(UserEntity user, string refreshToken)
        {
            if (!user.Authentication!.RefreshToken.Equals(refreshToken))
            {
                throw new SecurityTokenException("Invalid Refresh Token.");
            }
            else if (user.Authentication!.RefreshTokenExpires < DateTime.Now)
            {
                throw new SecurityTokenException("Refresh Token expired.");
            }
        }

        private async Task UpdateUserRefreshToken(UserEntity user, RefreshTokenDto newRefreshToken)
        {
            user.Authentication!.RefreshToken = newRefreshToken.Token;
            user.Authentication!.RefreshTokenExpires = newRefreshToken.Expires;

            await _repository.UpdateAsync(user);
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac
                .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash =
                hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }

        public async Task ForgotPasswordRequest(string userEmail)
        {
            var user = await _repository.FindByEmail(userEmail);

            if (user == null) return;

            user.Authentication!.ForgotPasswordExpires = DateTime.UtcNow.AddDays(1);
            user.Authentication!.ForgotPasswordToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            await _repository.UpdateAsync(user);

            await _emailService.SendForgotPasswordEmail(user.Email.Address, user.Name, user.Authentication!.ForgotPasswordToken);
        }

        public async Task<bool> SetRoler(Guid userId, string newRole)
        {
            var user = await _repository.FindById(userId)
                            ?? throw new ArgumentException("User not found");

            if (!RolesModels.IsValidRole(newRole))
                throw new ArgumentNullException($"The role '{newRole}' is not defined in the system. Make sure to use a valid role.");

            user.Authentication!.Role = newRole;
            await _repository.UpdateAsync(user);

            return true;
        }

        public async Task Revoke(Guid id)
        {
            var user = await _repository.FindById(id)
                            ?? throw new ArgumentException("User not found");

            user.Authentication!.RefreshToken = string.Empty;
            user.Authentication!.RefreshTokenExpires = null;

            await _repository.UpdateAsync(user);
        }

        public async Task RevokeAll()
        {
            var userList = await _repository.SelectAsync()
                            ?? throw new ArgumentException("User not found");

            foreach (var user in userList)
            {
                if (!string.IsNullOrEmpty(user.Authentication!.RefreshToken))
                {
                    user.Authentication!.RefreshToken = string.Empty;
                    user.Authentication!.RefreshTokenExpires = null;
                    await _repository.UpdateAsync(user);
                }
            }
        }
    }
}
