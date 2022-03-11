using FluentValidation;
using Lextatico.Application.Dtos.User;

namespace Lextatico.Application.Validators
{
    public class UserForgotPasswordDtoValidator : AbstractValidator<UserForgotPasswordDto>
    {
        public UserForgotPasswordDtoValidator()
        {
            ValidateEmail();
        }

        protected void ValidateEmail()
        {
            RuleFor(userLoginDto => userLoginDto.Email)
                .EmailAddress()
                .WithMessage("Insira um endereço de email válido.");
        }
    }
}