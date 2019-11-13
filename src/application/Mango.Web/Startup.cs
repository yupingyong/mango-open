using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Encodings.Web;
using System.Text.Unicode;
namespace Mango.Web
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
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //});

            services.AddSession();
            services.AddMemoryCache();

            services.AddControllersWithViews(options => {
                //options.Filters.Add(new Extensions.AuthorizationActionFilter());
            }).AddNewtonsoftJson();
            services.AddRazorPages();
            //
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
            //添加Session访问容器
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            //启用会话存储(Session)
            app.UseSession();
            app.UseCookiePolicy();

            app.UseAuthorization();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                   name: "Account",
                   areaName: "Account",
                   pattern: "{area:exists}/{controller}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute(
                   name: "Docs",
                   areaName: "Docs",
                   pattern: "{area:exists}/{controller}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                   name: "Cms",
                   areaName: "Cms",
                   pattern: "{area:exists}/{controller}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                   name: "User",
                   areaName: "User",
                   pattern: "{area:exists}/{controller}/{action=Index}/{id?}");
                //
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Portal}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            Core.Configuration.AddItem("ApiServerUrl", Configuration.GetSection("ApiServer:ApiServerUrl").Value);
            //腾讯配置项
            Core.Configuration.AddItem("Tencent_VerificationAppId", Configuration.GetSection("Tencent:VerificationAppId").Value);
            Core.Configuration.AddItem("Tencent_VerificationAppSecretKey", Configuration.GetSection("Tencent:VerificationAppSecretKey").Value);
        }
    }
}
