using System;
using System.Threading.Tasks;
using Lextatico.Domain.Models;

namespace Lextatico.Domain.Interfaces.Services
{
    public interface IUserService
    {
        (string token, string refreshToken) GenerateFullJwt(string email);
        Task<ApplicationUser> GetUserLoggedAsync();
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        ApplicationUser GetUserByRefreshToken(string refreshToken);
        Task<bool> CreateAsync(ApplicationUser applicationUser, string password);
        Task<bool> SignInAsync(string email, string password);
        Task UpdateRefreshTokenAsync(string email, string refreshToken, DateTime refreshTokenExpiration);
        Task<bool> ForgotPasswordAsync(string email);
        Task<bool> ResetPasswordAsync(string email, string password, string resetToken);
    }
}
