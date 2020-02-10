using System.Reflection;
using FApps.API.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace FApps.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //MY CODE     
            services.ConfigureCors();
            services.ConfigureSwagger();
            services.ConfigureMSSqlContext(Configuration);
            services.ConfigureDependencyInjection();
            services.ConfigureAutoMapper();
            services.ConfigureModelStateValidation();
            services.ConfigureJWTAuthentication();

            //Mediatr Configurations
            services.AddMediatR(typeof(Startup));
            services.AddMediatorHandlers(typeof(Startup).GetTypeInfo().Assembly);
            services.AddMediatorHandlers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            #region Custome Error Handling Middlewa
            //app.ErrorLogging();
            loggerFactory.AddSerilog();
            #endregion

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            #region Use Authentication
             app.UseAuthentication();
            #endregion

            app.UseMvc();

            //Sawgger Configuration
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
            });
        }
    }
}
