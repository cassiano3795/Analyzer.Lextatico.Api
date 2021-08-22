using Lextatico.Infra.Data.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lextatico.Infra.CrossCutting.IoC
{
    public static class ConfigureContext
    {
        public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LextaticoContext>(op =>
            {
                var sqlStringBuilder = new SqlConnectionStringBuilder(configuration.GetConnectionString(nameof(LextaticoContext)));

                sqlStringBuilder.Password = configuration["DbPassword"];

                var connectionString = sqlStringBuilder.ToString();

                op.UseSqlServer(connectionString);
            });

            return services;
        }
    }
}
