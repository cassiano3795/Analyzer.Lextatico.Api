using System;
using System.Security.Claims;
using Analyzer.Lextatico.Domain.Models;
using Analyzer.Lextatico.Domain.Security;
using Microsoft.AspNetCore.Identity;

namespace Analyzer.Lextatico.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        ITokenService WithUserManager(UserManager<ApplicationUser> userManager);
        ITokenService WithTokenConfiguration(TokenConfiguration tokenConfiguration);
        ITokenService WithSigningConfiguration(SigningConfiguration signingConfiguration);
        ITokenService WithEmail(string email);
        ITokenService WithJwtClaims();
        ITokenService WithUserClaims();
        ITokenService WithUserRoles();
        (string token, string refreshToken) BuildToken();
        string GetEmail();
        string GetUserName();
        string GetEmail(string token);
        string GetUserName(string token);
    }
}