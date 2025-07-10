using Api.Core.Events.Messaging;
using Api.CrossCutting.Identity.Authentication.Model;
using Api.Domain.Commands.AuthenticationCommands;
using Api.Domain.Events;
using Api.Domain.Interface;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Domain.Commands.AuthenticationCommands
{
    public class ForgetPasswordRequestCommandHandler(
        IUserRepository userRepository,
        UserManager<AppUser> _userManager,
        RoleManager<IdentityRole<Guid>> _roleManager,
        IConfiguration config) : CommandHandler,
        IRequestHandler<ForgetPasswordRequestCommand, ValidationResult>        
    {
        private readonly IConfiguration _config = config;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly UserManager<AppUser> _userManager = _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager = _roleManager;

        public async Task<ValidationResult> Handle(ForgetPasswordRequestCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                AddError("User not found");
                return ValidationResult;
            }

            var token = WebUtility.UrlEncode(await _userManager.GeneratePasswordResetTokenAsync(user));

            var userEntity = await _userRepository.GetByIdentityIdAsync(user.Id);

            if (userEntity is null)
            {
                AddError("Not Found");
                return ValidationResult;
            }

            userEntity.AddDomainEvent(
                new ForgottenPasswordRecoveryEvent(
                    user.Id,
                    user.Name,
                    user.Email!,
                    $"{_config["Host:Url"]}/Authentication/PasswordRecovery?email={user.Email}&token={token}"
                )
            );

            _userRepository.Update(userEntity);

            return await Commit(_userRepository.UnitOfWork);
        }
    }
}
