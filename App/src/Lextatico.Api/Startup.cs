using Lextatico.Infra.CrossCutting.Extensions;
using Lextatico.Infra.CrossCutting.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Lextatico.Api.Configurations;
using Lextatico.Infra.Identity.User;
using Microsoft.AspNetCore.Http;

namespace Lextatico.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddHttpContextAccessor()
                .AddMessage()
                .AddAspNetUserConfiguration()
                .AddEmailSettings(Configuration)
                .AddUrlsConfiguration(Configuration)
                .AddRepositories()
                .AddInfraServices()
                .AddDomainServices()
                .AddLextaticoAutoMapper()
                .AddApplicationServices()
                .AddContext(Configuration)
                .AddLextaticoIdentity()
                .AddJwtConfiguration(Configuration)
                .AddLexitaticoCors()
                .AddLextaticoControllers()
                .AddSwaggerConfiguration();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsProduction())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                
                app.UseSwaggerUI(c => c.SwaggerEndpoint("doc/swagger.json", "Lextatico Api v1"));
            }

            if (env.IsProduction())
            {
                app.UseHttpsRedirection();

                app.UseHsts();
            }

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseTransaction();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
