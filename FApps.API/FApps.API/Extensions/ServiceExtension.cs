using AutoMapper;
using FApps.API.Filters;
using FApps.Core.AutoMapper;
using FApps.Core.Context;
using FApps.Data.Interfaces;
using FApps.Data.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

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
        public static void ConfigureJWTAuthentication(this IServiceCollection services)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                //TODO: add url to appsetting.json file
                ValidIssuer = "https://localhost:44350/api/",

                ValidateAudience = true,
                //TODO: add url to appsetting.json file
                ValidAudience = "https://localhost:44350/api/",

                ValidateIssuerSigningKey = true,
                //TODO: add key to appsetting.json file
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("condimentumvestibulumSuspendissesitametpulvinarorcicondimentummollisjusto")),

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = tokenValidationParameters;
            });

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
        }
    }
}
