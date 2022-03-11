using Lextatico.Infra.Services.Implementations;
using Lextatico.Infra.Services.Interfaces;
using Lextatico.Infra.Services.Models.EmailService;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lextatico.Infra.CrossCutting.IoC
{
    public static class ConfigureInfraServices
    {
        public static IServiceCollection AddEmailSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var emailSettings = configuration.GetSection("EmailSettings").Get<EmailSettings>();

            var password = configuration["EmailPassword"];

            services.Configure<EmailSettings>(options =>
            {
                options.Email = emailSettings.Email;
                options.DisplayName = emailSettings.DisplayName;
                options.Host = emailSettings.Host;
                options.Password = password;
                options.Port = emailSettings.Port;
            });

            return services;
        }
        public static IServiceCollection AddInfraServices(this IServiceCollection services)
        {
            // INFRA SERVICES
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}