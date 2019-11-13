using AutoMapper;
using FApps.API.Filters;
using FApps.Core.AutoMapper;
using FApps.Core.Context;
using FApps.Data.Interfaces;
using FApps.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using System.Diagnostics.CodeAnalysis;

namespace FApps.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceExtension
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());
            });
        }
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Test API",
                    Description = "ASP.NET Core Web API"
                });
            });
        }
        public static void ConfigureMSSqlContext(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<ApplicationDbContext>(item => item.UseSqlServer(
                Configuration.GetConnectionString("myconn"), b => b.MigrationsAssembly("FApps.API")));
        }
        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IUserServices, UserServices>();
        }
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            var MappingConfiguration = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = MappingConfiguration.CreateMapper();
            services.AddSingleton(mapper);
        }
        public static void ConfigureModelStateValidation(this IServiceCollection services)
        {
            services.AddMvc(config =>
                   config.Filters.Add(new ModelStateValidationFilter()))
                  .AddJsonOptions(options => { options.SerializerSettings.Formatting = Formatting.Indented; });
        }
    }
}
