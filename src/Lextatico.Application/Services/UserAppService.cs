using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lextatico.Application.Dtos.Responses;
using Lextatico.Application.Dtos.User;
using Lextatico.Application.Services.Interfaces;
using Lextatico.Domain.Interfaces.Services;
using Lextatico.Domain.Models;
using Lextatico.Domain.Security;
using Microsoft.AspNetCore.Identity;

namespace Lextatico.Application.Services
{
    public class UserAppService : IUserAppService
    {
        public Response Response { get; } = new();
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TokenConfiguration _tokenConfiguration;
        private readonly SigningConfiguration _signingConfiguration;
        public UserAppService(IMapper mapper,
            IUserService userService,
            ITokenService tokenService,
            UserManager<ApplicationUser> userManager,
            TokenConfiguration tokenConfiguration,
            SigningConfiguration signingConfiguration)
        {
            _mapper = mapper;
            _userService = userService;
            _tokenService = tokenService;
            _userManager = userManager;
            _tokenConfiguration = tokenConfiguration;
            _signingConfiguration = signingConfiguration;
        }

        public async Task<Response> CreateAsync(UserSignInDto userSignIn)
        {
            var applicationUser = _mapper.Map<ApplicationUser>(userSignIn);

            var result = await _userService.CreateAsync(applicationUser, userSignIn.Password);

            if (result.Succeeded)
            {
                Response.Result = true;
            }
            else
            {
                Response.Result = false;
                foreach (var error in result.Errors)
                {
                    Response.AddError(string.Empty, error.Description);
                }
            }

            return Response;
        }

        public async Task<Response> LogInAsync(UserLogInDto userLogIn)
        {
            var result = await _userService.SignInAsync(userLogIn.Email, userLogIn.Password);

            if (result.Succeeded)
            {
                var (token, refreshToken) = GenerateFullJwt(userLogIn.Email);

                var authenticatedUser = new AuthenticatedUserDto(
                    true,
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddSeconds(_tokenConfiguration.Seconds),
                    token,
                    refreshToken,
                    DateTime.UtcNow.AddSeconds(_tokenConfiguration.SecondsRefresh));

                await _userService.UpdateRefreshToken(userLogIn.Email, authenticatedUser.RefreshToken, authenticatedUser.RefreshTokenExpiration);

                Response.Result = authenticatedUser;
            }
            else
            {
                if (result.IsLockedOut)
                    Response.AddError(string.Empty, "Usuário bloqueado.");
                else if (result.IsNotAllowed)
                {
                    Response.AddError(string.Empty, "Usuário não está liberado para logar.");
                }
                else
                {
                    Response.AddError(string.Empty, "Usuário ou senha incorreto.");
                }
            }

            return Response;
        }

        public async Task<Response> RefreshTokenAsync(UserRefreshDto userRefresh)
        {
            var email = _tokenService.GetUserName(userRefresh.Token);

            var applicationUser = _userManager.Users.FirstOrDefault(user => user.Email == email
                && user.RefreshToken == userRefresh.RefreshToken
                && DateTime.UtcNow <= user.RefreshTokenExpiration);

            if (applicationUser != null)
            {
                var (token, refreshToken) = GenerateFullJwt(email);

                var authenticatedUser = new AuthenticatedUserDto(
                    true,
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddSeconds(_tokenConfiguration.Seconds),
                    token,
                    refreshToken,
                    DateTime.UtcNow.AddSeconds(_tokenConfiguration.SecondsRefresh));

                await _userService.UpdateRefreshToken(email, authenticatedUser.RefreshToken, authenticatedUser.RefreshTokenExpiration);

                Response.Result = authenticatedUser;
            }
            else
            {
                Response.AddError(string.Empty, "Token ou RefreshToken inválido, faça o login novamente.");
            }

            return Response;
        }

        private (string token, string refreshToken) GenerateFullJwt(string email)
        {
            return _tokenService
                    .WithUserManager(_userManager)
                    .WithEmail(email)
                    .WithJwtClaims()
                    .WithUserClaims()
                    .WithUserRoles()
                    .BuildToken();
        }
    }
}
