using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Travel.Services;
using Microsoft.Extensions.Configuration;
using Travel.Models;

namespace Travel
{
    public class Startup
    {
        private IHostingEnvironment enviroment;
        private IConfigurationRoot config;

        public Startup(IHostingEnvironment enviroment)
        {
            this.enviroment = enviroment;
            var builder = new ConfigurationBuilder()
                .SetBasePath(enviroment.ContentRootPath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
            this.config = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(config);
            if (enviroment.IsEnvironment("Development"))
                services.AddScoped<IMailService, MailService>();

            services.AddDbContext<TravelContext>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseMvc(config =>
            {
                config.MapRoute(
                     name: "Default",
                     template: "{controller}/{action}/{id?}",
                     defaults: new { controller = "App", action = "Index" });
            });
        }
    }
}
