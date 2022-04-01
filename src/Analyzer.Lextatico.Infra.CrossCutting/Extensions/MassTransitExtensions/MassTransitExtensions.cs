using Analyzer.Lextatico.Application.Consumers.ApplicationUser;
using Analyzer.Lextatico.Application.ConsumersDefinition.ApplicationUser;
using Analyzer.Lextatico.Infra.Services.MessageBroker.Bus;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Analyzer.Lextatico.Infra.CrossCutting.Extensions.MassTransitExtensions
{
    public static class MassTransitExtensions
    {
        public static IServiceCollection AddLextaticoMassTransitWithRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAccountHost(configuration);

            return services;
        }

        private static IServiceCollection AddAccountHost(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit<IAccountBus>(x =>
            {
                x.AddConsumer<UserCreatedEventConsumer>(typeof(UserCreatedEventConsumerDefinition));

                x.AddConsumer<UserUpdatedEventConsumer>(typeof(UserUpdatedEventConsumerDefinition));

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ConfigurationAccountHost(configuration);

                    cfg.UseRawJsonSerializer(isDefault: true);

                    cfg.UseRawJsonDeserializer(isDefault: true);

                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}
