using Lextatico.Domain.Interfaces.Services;
using Lextatico.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Lextatico.Infra.CrossCutting.IoC
{
    public static class ConfigureDomainServices
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            // DOMAIN SERVICES
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAnalyzerService, AnalyzerService>();
            services.AddScoped<ITerminalTokenService, TerminalTokenService>();

            return services;
        }
    }
}
