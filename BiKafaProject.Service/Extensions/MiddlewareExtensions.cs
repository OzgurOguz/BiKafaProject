using BiKafaProject.Service.MiddleWares;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiKafaProject.Service.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseHeaderControlMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HeaderControlMiddleware>();
        }


    }
}
