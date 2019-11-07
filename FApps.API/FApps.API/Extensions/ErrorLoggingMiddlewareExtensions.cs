using FApps.API.Exceptions;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace FApps.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ErrorLoggingMiddlewareExtensions
    {
        public static void UseErrorLogging(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(er => er.Run(ErrorLogging.HandleAsync));
        }
    }
}
