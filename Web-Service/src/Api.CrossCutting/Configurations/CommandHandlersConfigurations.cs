using Api.Domain.Commands.AuthenticationCommands;
using Api.Domain.Commands.ProductCommands;
using Api.Domain.Commands.UserCommands;
using Domain.Commands.AuthenticationCommands;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.Configuration
{
    public static class CommandHandlersConfigurations
    {
        public static void AddCommandHandlers(this WebApplicationBuilder builder)
        {
            // Authentication Commands
            builder.Services.AddScoped<IRequestHandler<RegisterNewUserCommand, ValidationResult>, RegisterNewUserCommandHandler>();
            builder.Services.AddScoped<IRequestHandler<ForgetPasswordRequestCommand, ValidationResult>, ForgetPasswordRequestCommandHandler>();
            builder.Services.AddScoped<IRequestHandler<ForgetPasswordVerificationCommand, ValidationResult>, ForgetPasswordVerificationCommandHandler>();

            // User Commands
            builder.Services.AddScoped<IRequestHandler<CreateNewUserCommand, ValidationResult>, CreateNewUserCommandHandler>();
            builder.Services.AddScoped<IRequestHandler<UpdateUserCommand, ValidationResult>, UpdateUserCommandHandler>();
            builder.Services.AddScoped<IRequestHandler<DeleteUserCommand, ValidationResult>, DeleteUserCommandHandler>();
            builder.Services.AddScoped<IRequestHandler<AddAddressToUserCommand, ValidationResult>, AddAddressToUserCommandHandler>();
            builder.Services.AddScoped<IRequestHandler<UpdateUserAddressCommand, ValidationResult>, UpdateUserAddressCommandHandler>();

            // Product Commands
            builder.Services.AddScoped<IRequestHandler<CreateProductCommand, ValidationResult>, CreateProductCommandHandler>();
            builder.Services.AddScoped<IRequestHandler<UpdateProductCommand, ValidationResult>, UpdateProductCommandHandler>();
            builder.Services.AddScoped<IRequestHandler<DeleteProductCommand, ValidationResult>, DeleteProductCommandHandler>();
        }
    }
}
