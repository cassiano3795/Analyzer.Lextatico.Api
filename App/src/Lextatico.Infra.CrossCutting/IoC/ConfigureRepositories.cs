using Lextatico.Domain.Interfaces.Repositories;
using Lextatico.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Lextatico.Infra.CrossCutting.IoC
{
    public static class ConfigureRepositories
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // REPOSITORIES
            services.AddScoped<IAnalyzerRepository, AnalyzerRepository>();
            services.AddScoped<ITerminalTokenRepository, TerminalTokenRepository>();

            return services;
        }
    }
}
