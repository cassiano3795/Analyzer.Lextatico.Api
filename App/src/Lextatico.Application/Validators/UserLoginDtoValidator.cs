using System.Linq;
using FluentValidation;
using Lextatico.Application.Dtos.User;
using Lextatico.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Lextatico.Application.Validators
{
    public class UserLoginDtoValidator : UserDtoValidator<UserLogInDto>
    {
        public UserLoginDtoValidator(UserManager<ApplicationUser> userManager)
            : base(userManager)
        {
            ValidateEmail();
        }
    }
}
