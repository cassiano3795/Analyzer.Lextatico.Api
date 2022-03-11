using Lextatico.Domain.Dtos.Message;
using Microsoft.Extensions.DependencyInjection;

namespace Lextatico.Infra.CrossCutting.IoC
{
    public static class ConfigureMessage
    {
        public static IServiceCollection AddMessage(this IServiceCollection services)
        {
            services.AddScoped<IMessage, Message>();

            return services;
        }
    }
}
