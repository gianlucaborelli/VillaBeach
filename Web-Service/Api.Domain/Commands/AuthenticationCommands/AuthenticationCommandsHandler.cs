using Microsoft.AspNetCore.Identity;

using Api.Core.Events.Messaging;
using Api.Domain.Interface;

using FluentValidation.Results;
using MediatR;
using Api.CrossCutting.Identity.Authentication.Model;
using Microsoft.Extensions.Configuration;
using Api.Domain.Events.Authentication;

namespace Api.Domain.Commands.AuthenticationCommands
{
    public class AuthenticationCommandsHandler(
        IUserRepository userRepository,
        UserManager<AppUser> _userManager,
        RoleManager<IdentityRole<Guid>> _roleManager,
        IConfiguration config) : CommandHandler,
        IRequestHandler<ForgetPasswordVerificationCommand, ValidationResult>
    {
        private readonly IConfiguration _config = config;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly UserManager<AppUser> _userManager = _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager = _roleManager;       

        

        public async Task<ValidationResult> Handle(ForgetPasswordVerificationCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                AddError("User not found");
                return ValidationResult;
            }

            var userEntity = await _userRepository.GetByIdentityIdAsync(user.Id);

            if (userEntity is null)
            {
                AddError("Not Found");
                return ValidationResult;
            }

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            var domainEvent = new ForgotPasswordRequestVerifiedEvent(
                user.Id,
                user.Email!,
                true
            );

            if (result.Succeeded)
            {
                domainEvent.Failed = false;

                userEntity.AddDomainEvent(domainEvent);

                _userRepository.Update(userEntity);

                return await Commit(_userRepository.UnitOfWork);
            }

            domainEvent.Failed = true;

            foreach (var error in result.Errors)
                AddError(error.Description);

            userEntity.AddDomainEvent(domainEvent);

            _userRepository.Update(userEntity);

            await Commit(_userRepository.UnitOfWork);

            return ValidationResult;
        }
    }
}
