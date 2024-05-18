using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using System.Security.Authentication;
using System.IdentityModel.Tokens.Jwt;
using Api.CrossCutting.Identity.Authentication.Model;
using System.Text;

namespace Api.CrossCutting.Identity.Authentication
{
    /// <summary>
    /// Service responsible for handling user authentication, registration, and token management.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(
                    IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
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
        /// Creates an access token for the specified user entity by generating a JWT (JSON Web Token)
        /// with the user's claims such as NameIdentifier, Name, Email, and Role.
        /// </summary>
        /// <param name="user">The UserEntity for whom the access token is generated.</param>
        /// <returns>A string representation of the generated JWT access token.</returns>
        /// <exception cref="ApplicationException">Thrown when the token key is not configured in the environment variables.</exception>
        public string CreateAccessToken(Guid userId, string userName, string userEmail, string userRole)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Email, userEmail),
                new Claim(ClaimTypes.Role, userRole)
            };

            var tokenKey = Environment.GetEnvironmentVariable("VILLABEACH_TOKEN_KEY")
                            ?? throw new ApplicationException("Token key is not configured.");

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(tokenKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.UtcNow.AddMinutes(60),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenKey = Environment.GetEnvironmentVariable("VILLABEACH_TOKEN_KEY") ??
                            throw new ApplicationException("Token key is not configured.");

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                                   .GetBytes(tokenKey)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512Signature, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        /// <summary>
        /// Logs out the currently authenticated user by updating the refresh token to an empty value.
        /// </summary>
        /// <returns>True if logout is successful.</returns>
        /// <exception cref="AuthenticationException">Thrown when the user is not found.</exception>
        // public async Task<bool> Logout()
        // {
        //     var result = await _repository.GetByIdAsync(GetUserId())
        //                     ?? throw new AuthenticationException("User not found.");

        //     await UpdateUserRefreshToken(result, new RefreshTokenDto { Token = string.Empty, Expires = null });

        //     return true;
        // }

        /// <summary>
        /// Registers a new user with the provided information, creating a unique user entity,
        /// generating password hash and salt, and storing the user and associated settings in repositories.
        /// </summary>
        /// <param name="userRequest">User registration data</param>
        /// <returns>The unique identifier (ID) of the newly registered user.</returns>
        /// <exception cref="AuthenticationException">Thrown if a user with the provided email already exists.</exception>
        public async Task EmailVerificationToken(string emailVerificationToken)
        {
            // var user = await _repository.GetByEmailVerificationTokenAsync(emailVerificationToken)
            //                 ?? throw new SecurityTokenException("Invalid Token");

            // user.Email.EmailVerifiedAt = DateTime.UtcNow;
            // user.Email.EmailIsVerified = true;
            // user.Email.EmailVerificationToken = null;
            // _repository.Update(user);
        }

        public async Task<bool> ChangePassword(string newPassword)
        {
            // var user = await _repository.GetByIdAsync(GetUserId())
            //                     ?? throw new AuthenticationException("User not found.");

            // CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

            // user.Authentication!.PasswordHash = passwordHash;
            // user.Authentication!.PasswordSalt = passwordSalt;

            // _repository.Update(user);

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
        // public async Task<RefreshTokenDtoResult> RefreshToken(RefreshTokenDtoRequest request)
        // {
        //     var refreshToken = request.RefreshToken;

        //     var principal = _refreshTokenService.GetPrincipalFromExpiredToken(request.AccessToken)
        //                         ?? throw new SecurityTokenException("Invalid access token/refresh token");

        //     string email = principal.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Email)?.Value
        //                         ?? throw new InvalidOperationException("User Email not found in claims.");

        //     var user = await _repository.GetByEmailAsync(email)
        //                         ?? throw new AuthenticationException("User not found");

        //     ValidateRefreshToken(user, refreshToken);

        //     string newAccessToken = _tokenService.CreateAccessToken(user);
        //     var newRefreshToken = _refreshTokenService.GenerateRefreshToken();

        //     await UpdateUserRefreshToken(user, newRefreshToken);

        //     return new RefreshTokenDtoResult { AccessToken = newAccessToken, RefreshToken = newRefreshToken.Token };
        // }

        // private static void ValidateRefreshToken(User user, string refreshToken)
        // {
        //     if (!user.Authentication!.RefreshToken.Equals(refreshToken))
        //     {
        //         throw new SecurityTokenException("Invalid Refresh Token.");
        //     }
        //     else if (user.Authentication!.RefreshTokenExpires < DateTime.Now)
        //     {
        //         throw new SecurityTokenException("Refresh Token expired.");
        //     }
        // }

        // private async Task UpdateUserRefreshToken(User user, RefreshTokenDto newRefreshToken)
        // {
        //     user.Authentication!.RefreshToken = newRefreshToken.Token;
        //     user.Authentication!.RefreshTokenExpires = newRefreshToken.Expires;

        //     _repository.Update(user);
        // }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac
                .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash =
                hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }

        // public async Task ForgotPasswordRequest(string userEmail)
        // {
        //     var user = await _repository.GetByEmailAsync(userEmail);

        //     if (user == null) return;

        //     user.Authentication!.ForgotPasswordExpires = DateTime.UtcNow.AddDays(1);
        //     user.Authentication!.ForgotPasswordToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        //     _repository.Update(user);

        //     await _emailService.SendForgotPasswordEmail(user.Email.Address, user.Name, user.Authentication!.ForgotPasswordToken);
        // }

        // public async Task<bool> SetRoler(Guid userId, string newRole)
        // {
        //     var user = await _repository.GetByIdAsync(userId)
        //                     ?? throw new ArgumentException("User not found");

        //     if (!RolesModels.IsValidRole(newRole))
        //         throw new ArgumentNullException($"The role '{newRole}' is not defined in the system. Make sure to use a valid role.");

        //     user.Authentication!.Role = newRole;
        //     _repository.Update(user);

        //     return true;
        // }

        // public async Task Revoke(Guid id)
        // {
        //     var user = await _repository.GetByIdAsync(id)
        //                     ?? throw new ArgumentException("User not found");

        //     user.Authentication!.RefreshToken = string.Empty;
        //     user.Authentication!.RefreshTokenExpires = null;

        //     _repository.Update(user);
        // }

        // public async Task RevokeAll()
        // {
        //     var userList = await _repository.GetAllAsync()
        //                     ?? throw new ArgumentException("User not found");

        //     foreach (var user in userList)
        //     {
        //         if (!string.IsNullOrEmpty(user.Authentication!.RefreshToken))
        //         {
        //             user.Authentication!.RefreshToken = string.Empty;
        //             user.Authentication!.RefreshTokenExpires = null;
        //             _repository.Update(user);
        //         }
        //     }
        // }
    }
}
