using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiKafaProject.Service.MiddleWares
{
    public class HeaderControlMiddleware
    {
        private readonly RequestDelegate _next;

        public HeaderControlMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext context)
        {
            if ((context.Request.Headers["secret"].ToString() == null || context.Request.Headers["secret"].ToString().Length < 10) && context.Request.Method == "POST")
            {
                context.Response.StatusCode = 401;

            }
            else
            {
                await _next(context);
            }

        }

    }
}
