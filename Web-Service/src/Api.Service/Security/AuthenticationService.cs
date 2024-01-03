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
        private readonly IUserSettingsRepository _userSettingsRepository;
        private readonly IAccessTokenService _tokenService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IEmailService _emailService;

        public AuthenticationService(IUserRepository repository,
                            IUserSettingsRepository userSettingsRepository,
                            IMapper mapper,
                            IHttpContextAccessor httpContextAccessor,
                            IAccessTokenService tokenService,
                            IRefreshTokenService refreshTokenService,
                            IEmailService emailService)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = repository;
            _userSettingsRepository = userSettingsRepository;
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

            if (!user.EmailIsVerified) throw new AuthenticationException("Email has not been verified.");

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new AuthenticationException("Wrong password.");

            string newAccessToken = _tokenService.CreateAccessToken(user);
            var newRefreshToken = _refreshTokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken.Token;
            user.RefreshTokenExpires = newRefreshToken.Expires;

            await _repository.UpdateAsync(user);

            var settings = await _userSettingsRepository.GetSettingByUserId(user.Id);

            return new LoginDtoResult
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken.Token,
                Settings = _mapper.Map<UserSettingsDto>(settings)
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
                Email = userRequest.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                EmailVerificationToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            };

            await _repository.InsertAsync(newUser);

            var newUserSettings = new UserSettingsEntity
            {
                User = newUser
            };

            await _userSettingsRepository.InsertAsync(newUserSettings);

            await _emailService.SendEmailVerification(newUser.Email, newUser.Name, newUser.EmailVerificationToken);

            return newUser.Id;
        }

        public async Task EmailVerificationToken(string emailVerificationToken)
        {
            var user = await _repository.FindByEmailVerificationToken(emailVerificationToken)
                            ?? throw new SecurityTokenException("Invalid Token");

            user.EmailVerifiedAt = DateTime.UtcNow;
            user.EmailIsVerified = true;
            user.EmailVerificationToken = null;
            await _repository.UpdateAsync(user);            
        }

        public async Task<bool> ChangePassword(string newPassword)
        {
            var user = await _repository.FindById(GetUserId())
                                ?? throw new AuthenticationException("User not found.");

            CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

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
            if (!user.RefreshToken.Equals(refreshToken))
            {
                throw new SecurityTokenException("Invalid Refresh Token.");
            }
            else if (user.RefreshTokenExpires < DateTime.Now)
            {
                throw new SecurityTokenException("Refresh Token expired.");
            }
        }

        private async Task UpdateUserRefreshToken(UserEntity user, RefreshTokenDto newRefreshToken)
        {
            user.RefreshToken = newRefreshToken.Token;
            user.RefreshTokenExpires = newRefreshToken.Expires;

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
            
            user.ForgotPasswordExpires = DateTime.UtcNow.AddDays(1);
            user.ForgotPasswordToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            await _repository.UpdateAsync(user);   

            await _emailService.SendForgotPasswordEmail(user.Email, user.Name, user.ForgotPasswordToken);
        }
    }
}
