using Microsoft.Extensions.DependencyInjection;

namespace Lextatico.Infra.Identity.User
{
    public static class Abstractions
    {
        public static IServiceCollection AddAspNetUserConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IAspNetUser, AspNetUser>();

            return services;
        }
    }
}
