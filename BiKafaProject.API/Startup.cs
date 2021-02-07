using BiKafaProject.Core.Filters;
using BiKafaProject.Core.Interfaces;
using BiKafaProject.Core.Models.DbModels;
using BiKafaProject.Service.Extensions;
using BiKafaProject.Service.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiKafaProject.API
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

            services.AddScoped<NotFoundFilter>();
            services.Configure<Settings>(o => { o.IConfigurationRoot = (IConfigurationRoot)Configuration; });
            services.AddScoped<IUserOperationsRepository, UserOperationsRepository>();
            services.AddControllers();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHeaderControlMiddleware();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

     

                //});
                //app.Use(async (context, next) =>
                //     {

                //        //.GetEndpoint()s
                //        //.Metadata
                //        //.GetMetadata<ControllerActionDescriptor>();
                //        //if (context.Request.Method == "POST" && context.GetEndpoint().Metadata.GetMetadata<ControllerActionDescriptor>().ControllerName == "SaveData")
                //        // {
                //            //context.Response.WriteAsync("aa");
                //             context.Response.Headers.Add("deneme", "aa");
                //         next();
                //         //}
                //         //throw new Exception();


                //     });
            }
    }
}
