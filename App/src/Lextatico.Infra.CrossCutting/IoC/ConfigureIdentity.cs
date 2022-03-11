using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Lextatico.Infra.Data.Context;
using Lextatico.Domain.Models;
using Lextatico.Infra.Identity.Extensions;

namespace Lextatico.Infra.CrossCutting.IoC
{
    public static class ConfigureIdentity
    {
        public static IServiceCollection AddLextaticoIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddErrorDescriber<LocalizedIdentityErrorDescriber>()
                .AddEntityFrameworkStores<LextaticoContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
