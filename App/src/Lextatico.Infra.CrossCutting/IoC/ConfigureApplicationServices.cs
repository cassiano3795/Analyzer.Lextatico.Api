using Lextatico.Application.Services;
using Lextatico.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Lextatico.Infra.CrossCutting.IoC
{
    public static class ConfigureApplicationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IAnalyzerAppService, AnalyzerAppService>();
            services.AddScoped<ITerminalTokenAppService, TerminalTokenAppService>();

            return services;
        }
    }
}
