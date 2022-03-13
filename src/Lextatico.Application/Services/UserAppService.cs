using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lextatico.Application.Dtos.User;
using Lextatico.Application.Services.Interfaces;
using Lextatico.Domain.Interfaces.Services;
using Lextatico.Domain.Models;
using Lextatico.Domain.Security;

namespace Lextatico.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly TokenConfiguration _tokenConfiguration;
        private readonly SigningConfiguration _signingConfiguration;
        public UserAppService(IMapper mapper,
            IUserService userService,
            ITokenService tokenService,
            TokenConfiguration tokenConfiguration,
            SigningConfiguration signingConfiguration)
        {
            _mapper = mapper;
            _userService = userService;
            _tokenService = tokenService;
            _tokenConfiguration = tokenConfiguration;
            _signingConfiguration = signingConfiguration;
        }

        public async Task<UserDetailDto> GetUserLoggedAsync()
        {
            var userDto = _mapper.Map<UserDetailDto>(await _userService.GetUserLoggedAsync());

            return userDto;
        }

        public async Task<bool> CreateAsync(UserSignInDto userSignIn)
        {
            var applicationUser = _mapper.Map<ApplicationUser>(userSignIn);

            var result = await _userService.CreateAsync(applicationUser, userSignIn.Password);

            return result;
        }

        public async Task<bool> ForgotPasswordAsync(UserForgotPasswordDto userForgotPassword)
        {
            var applicationUser = await _userService.GetUserByEmailAsync(userForgotPassword.Email);

            // TODO: AQUI VERIFICAR COMO LANÇAR 404
            if (applicationUser == null)
                return false;

            var result = await _userService.ForgotPasswordAsync(userForgotPassword.Email);

            return result;
        }

        public async Task<bool> ResetPasswordAsync(UserResetPasswordDto userResetPassword)
        {
            var applicationUser = await _userService.GetUserByEmailAsync(userResetPassword.Email);

            // TODO: AQUI VERIFICAR COMO LANÇAR 404
            if (applicationUser == null)
                return false;

            var result = await _userService.ResetPasswordAsync(userResetPassword.Email, userResetPassword.Password, userResetPassword.ResetToken);

            return result;
        }
    }
}
