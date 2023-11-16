using Api.Domain.Dtos.Login;
using Api.Domain;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Api.Domain.Interfaces.Services.Login;
using Microsoft.AspNetCore.Http;
using Api.Domain.Dtos.User;
using Api.Domain.Repository;
using Api.Domain.Entities;
using AutoMapper;


namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {        
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        private readonly IUserRepository _repository;

        public LoginService(IUserRepository repository,
                            IMapper mapper,          
                            IHttpContextAccessor httpContextAccessor)
        {   
            _httpContextAccessor = httpContextAccessor;
            _repository =  repository;
            _mapper = mapper;
        }

        public int GetUserId()
        {
            return int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public string GetUserEmail() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);

        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _repository.FindByEmail(email);

            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }            
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password.";
            }
            else
            {
                response.Data = CreateToken(user);
            }

            return response;
        }

        public async Task<ServiceResponse<Guid>> Register(RegisterDtoRequest userRequest)
        {
            if (await _repository.UserExists(userRequest.Email))
            {
                return new ServiceResponse<Guid>
                {
                    Success = false,
                    Message = "User already exists."
                };
            }

            CreatePasswordHash(userRequest.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var newUser = new UserEntity{
                Name = userRequest.Name,
                Email = userRequest.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };

            await _repository.InsertAsync(newUser);

            return new ServiceResponse<Guid> { Data = newUser.Id, Message = "Registration successful!" };
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

        private string CreateToken(UserEntity user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var tokenKey = Environment.GetEnvironmentVariable("VILLABEACH_TOKEN_KEY");

            if (tokenKey == null)
            {
                throw new ApplicationException("Token key is not configured.");
            }
            
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(tokenKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<ServiceResponse<bool>> ChangePassword(string userId, string newPassword)
        {
            var user = await _repository.FindById(userId);
            if (user == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _repository.UpdateAsync(user);

            return new ServiceResponse<bool> { Data = true, Message = "Password has been changed." };
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            var entity =  await _repository.FindByEmail(email);
            return _mapper.Map<UserDto>(entity);            
        }        

        public Task<bool> UserExists(string email)
        {
            return _repository.UserExists(email);
        }        
    }
}
