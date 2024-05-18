using System.Security.Cryptography;
using Api.Core.Events.Messaging;
using Api.CrossCutting.Identity.Authentication;
using Api.CrossCutting.Identity.Authentication.Exceptions;
using Api.CrossCutting.Identity.Authentication.Model;
using Api.Domain.Entities;
using Api.Domain.Events;
using Api.Domain.Interface;
using FluentValidation.Results;
using MediatR;

namespace Api.Domain.Commands.AuthenticationCommands
{
    public class AuthenticationCommandsHandler : CommandHandler,
        IRequestHandler<RegisterNewUserCommand, ValidationResult>,
        IRequestHandler<LoginRequestCommand, ValidationResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationCommandsHandler(
            IUserRepository userRepository,
            IAuthenticationService authenticationService)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
        }

        public async Task<ValidationResult> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            _authenticationService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var newUser = new User
            {
                Name = request.Name,
                Email = new Email
                {
                    EmailVerificationToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                    Address = request.Email,
                },

                Authentication = new Authentication
                {
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                },
            };

            if (await _userRepository.GetByEmailAsync(newUser.Email.Address) != null)
            {
                AddError("The user e-mail has already been taken.");
                return ValidationResult;
            }

            newUser.AddDomainEvent(new NewUserRegisteredEvent(newUser.Id, newUser.Name, newUser.Email.Address));

            _userRepository.Add(newUser);

            return await Commit(_userRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(LoginRequestCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var user = await _userRepository.GetByEmailAsync(request.Email)
                            ?? throw new AuthenticationException("User not found.");

            if (!user.Email.EmailIsVerified)
            {
                AddError("Email has not been verified.");
                return ValidationResult;
            }

            if (!_authenticationService.VerifyPasswordHash(request.Password, user.Authentication!.PasswordHash, user.Authentication!.PasswordSalt))
            {
                AddError("Wrong password.");
                return ValidationResult;
            }
            
            var newRefreshToken = _authenticationService.GenerateRefreshToken();

            user.Authentication!.RefreshToken = newRefreshToken.Token;
            user.Authentication!.RefreshTokenExpires = newRefreshToken.Expires;

            _userRepository.Update(user);            
                     
            
            return await Commit(_userRepository.UnitOfWork);
        }
    }
}
