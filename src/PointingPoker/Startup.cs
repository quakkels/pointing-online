using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using PointingPoker.DataAccess;
using PointingPoker.Domain;
using Microsoft.AspNetCore.Http;

namespace PointingPoker
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddJsonFile("appsettings.json", false, true);
            }
            else
            {
                builder.AddJsonFile("appsettings.Production.json", false, true);
            }

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAuthentication();
            services.AddDataAccess(Configuration.GetConnectionString("PointingPoker"));
            services.AddDomainServices();
            services.AddMvc();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                LoginPath = "/User/SignIn",
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
            });

            app.UseStaticFiles();
            app.UseStatusCodePages();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Pointinating}/{action=Dashboard}/{id?}");
            });
        }
    }
}
