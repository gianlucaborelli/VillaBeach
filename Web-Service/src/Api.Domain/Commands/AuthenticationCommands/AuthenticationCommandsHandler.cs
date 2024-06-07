using Microsoft.AspNetCore.Identity;

using Api.Core.Events.Messaging;
using Api.Domain.Entities;
using Api.Domain.Events;
using Api.Domain.Interface;

using FluentValidation.Results;
using MediatR;
using System.Net;
using Api.CrossCutting.Identity.Authentication.Model;
using Microsoft.Extensions.Configuration;

namespace Api.Domain.Commands.AuthenticationCommands
{
    public class AuthenticationCommandsHandler(
        IUserRepository userRepository,
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager,
        IConfiguration config) : CommandHandler,
        IRequestHandler<RegisterNewUserCommand, ValidationResult>
    {
        private readonly IConfiguration _config = config;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager = roleManager;

        public async Task<ValidationResult> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;            

            var user = new AppUser
            {
                Name = request.Name,
                Email = request.Email,
                UserName = (request.Name + Guid.NewGuid().ToString()[..8])
                    .Replace(" ", string.Empty)
                    .Replace("-", string.Empty)
            };

            var role = await _roleManager.FindByNameAsync("User");            

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded is false)
            {
                foreach (var error in result.Errors)
                    AddError(error.Description);

                return ValidationResult;
            }
            
            var addRoleResult = await _userManager.AddToRoleAsync(user, role!.Name!);
            if (addRoleResult.Succeeded is false)
            {
                foreach (var error in addRoleResult.Errors)
                    AddError(error.Description);

                await _userManager.DeleteAsync(user);

                return ValidationResult;
            }

            var userEntity = new User
            {
                Name = user.UserName,
                IdentityId = user.Id
            };

            _userRepository.Add(userEntity);

            var token = WebUtility.UrlEncode(await _userManager.GenerateEmailConfirmationTokenAsync(user));

            userEntity.AddDomainEvent(
                new NewUserRegisteredEvent(
                    user.Id,
                    user.Name,
                    user.Email,
                    $"{_config["Host:Url"]}/Authentication/EmailVerification?email={user.Email}&token={token}"
                ));

            return await Commit(_userRepository.UnitOfWork);
        }
    }
}
