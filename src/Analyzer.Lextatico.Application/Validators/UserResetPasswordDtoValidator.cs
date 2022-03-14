using FluentValidation;
using Analyzer.Lextatico.Application.Dtos.User;
using Analyzer.Lextatico.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Analyzer.Lextatico.Application.Validators
{
    public class UserResetPasswordDtoValidator : UserDtoValidator<UserResetPasswordDto>
    {
        public UserResetPasswordDtoValidator(UserManager<ApplicationUser> userManager) : base(userManager)
        {
            ValidatePassword();

            ValidatePasswordEqualConfirmPassword();
        }

        private void ValidatePasswordEqualConfirmPassword()
        {
            RuleFor(userSignin => userSignin.ConfirmPassword)
                .Equal(userSignin => userSignin.Password)
                .WithMessage("As senhas não conferem.");
        }
    }
}
