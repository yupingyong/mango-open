using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

using Mango.Framework;
namespace Mango.WebHost.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 启用Swagger组件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomizedSwagger(this IApplicationBuilder app)
        {
            app.UseSwaggerUI(c =>
            {
                foreach (var module in GlobalConfiguration.Modules)
                {
                    if (module.IsApplicationPart)
                    {
                        c.SwaggerEndpoint($"/swagger/{module.Name}/swagger.json", $"{module.Name} API");
                    }
                }
                c.RoutePrefix = "swagger";
            });
            app.UseSwagger();
            return app;
        }
        /// <summary>
        /// 启用MVC相关组件
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomizedMvc(this IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                foreach (var module in GlobalConfiguration.Modules)
                {
                    if (module.IsApplicationPart)
                    {
                        endpoints.MapAreaControllerRoute(
                            name: "area",
                           areaName: module.Name,
                           pattern: "api/{area:exists}/{controller}/{id?}"
                         );
                    }
                }
            });
            return app;
        }
    }
}
