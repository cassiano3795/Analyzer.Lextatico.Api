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

        public async Task<AuthenticatedUserDto> LogInAsync(UserLogInDto userLogIn)
        {
            var result = await _userService.SignInAsync(userLogIn.Email, userLogIn.Password);

            if (result)
            {
                var userDto = _mapper.Map<UserDetailDto>(await _userService.GetUserByEmailAsync(userLogIn.Email));

                var (token, refreshToken) = _userService.GenerateFullJwt(userLogIn.Email);

                var authenticatedUser = new AuthenticatedUserDto(
                    userDto,
                    true,
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddSeconds(_tokenConfiguration.Seconds),
                    token,
                    refreshToken,
                    DateTime.UtcNow.AddSeconds(_tokenConfiguration.SecondsRefresh));

                await _userService.UpdateRefreshTokenAsync(userLogIn.Email, authenticatedUser.RefreshToken, authenticatedUser.RefreshTokenExpiration);

                return authenticatedUser;
            }

            return null;
        }

        public async Task<AuthenticatedUserDto> RefreshTokenAsync(UserRefreshDto userRefresh)
        {
            var applicationUser = _userService.GetUserByRefreshToken(userRefresh.RefreshToken);

            if (applicationUser == null)
            {
                return null;
            }

            var userDto = _mapper.Map<UserDetailDto>(await _userService.GetUserByEmailAsync(applicationUser.Email));

            var (token, refreshToken) = _userService.GenerateFullJwt(applicationUser.Email);

            var authenticatedUser = new AuthenticatedUserDto(
                userDto,
                true,
                DateTime.UtcNow,
                DateTime.UtcNow.AddSeconds(_tokenConfiguration.Seconds),
                token,
                refreshToken,
                DateTime.UtcNow.AddSeconds(_tokenConfiguration.SecondsRefresh));

            await _userService.UpdateRefreshTokenAsync(applicationUser.Email, authenticatedUser.RefreshToken, authenticatedUser.RefreshTokenExpiration);

            return authenticatedUser;
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
