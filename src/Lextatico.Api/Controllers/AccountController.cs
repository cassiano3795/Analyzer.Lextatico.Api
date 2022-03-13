using System.Threading.Tasks;
using Lextatico.Api.Controllers.Base;
using Lextatico.Application.Dtos.User;
using Lextatico.Application.Services.Interfaces;
using Lextatico.Domain.Dtos.Message;
using Lextatico.Infra.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lextatico.Api.Controllers
{
    public class AccountController : LextaticoController
    {
        private readonly IUserAppService _userAppService;
        private readonly IEmailService _emailService;

        public AccountController(IUserAppService userAppService, IEmailService emailService, IMessage message)
            : base(message)
        {
            _userAppService = userAppService;
            _emailService = emailService;
        }

        [HttpGet, Route("[action]")]
        public async Task<IActionResult> GetUser()
        {
            var result = await _userAppService.GetUserLoggedAsync();

            return ReturnOk(result);
        }

        [HttpPost, Route("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Signin([FromBody] UserSignInDto userSignIn)
        {
            await _userAppService.CreateAsync(userSignIn);

            return ReturnCreated();
        }

        [HttpPost, Route("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] UserForgotPasswordDto userForgotPassword)
        {
            await _userAppService.ForgotPasswordAsync(userForgotPassword);

            return ReturnOk();
        }

        [HttpPost, Route("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] UserResetPasswordDto userResetPassword)
        {
            await _userAppService.ResetPasswordAsync(userResetPassword);

            return ReturnOk();
        }
    }
}
