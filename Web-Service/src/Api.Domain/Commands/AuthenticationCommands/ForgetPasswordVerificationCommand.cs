using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Core.Events.Messaging;
using Api.Domain.Commands.AuthenticationCommands.Validations;
using FluentValidation;

namespace Api.Domain.Commands.AuthenticationCommands
{
    public class ForgetPasswordVerificationCommand : Command
    {
        public required string Token { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public required string ConfirmPassword { get; set; }

        public ForgetPasswordVerificationCommand(string token, string email, string password, string confirmPassword)
        {            
            Token = token;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        public override bool IsValid()
        {
            ValidationResult = new ForgetPasswordVerificationCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}