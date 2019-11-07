using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FApps.API.Extensions
{
    public static class AutoMapperExtension
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(DependencyContext.Default);
        }

        public static void AddAutoMapper(this IServiceCollection services, DependencyContext dependencyContext)
        {
            services.AddAutoMapper(dependencyContext.RuntimeLibraries
                .SelectMany(lib => lib.GetDefaultAssemblyNames(dependencyContext).Select(Assembly.Load)));
        }
    }
}
