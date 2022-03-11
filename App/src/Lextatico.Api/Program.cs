using System;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using Lextatico.Api.Extensions;
using Lextatico.Infra.CrossCutting.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HostEnvironmentEnvExtensions = Lextatico.Api.Extensions.HostEnvironmentEnvExtensions;

namespace Lextatico.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // TODO: ESSA REGRA AQUI É PARA DAR TEMPO DE DAR ATTACH NA API
            if (HostEnvironmentEnvExtensions.IsDocker())
                Thread.Sleep(30000);

            var host = CreateHostBuilder(args).ConfigureAppConfiguration((hostContext, builder) =>
            {
                if (hostContext.HostingEnvironment.IsLocalDevelopment())
                    builder.AddUserSecrets<Program>();
            }).Build();

            var webHostEnvironment = host.Services.GetRequiredService<IWebHostEnvironment>();

            if (!webHostEnvironment.IsProduction())
                await host.Services.MigrateContextDbAsync();

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
