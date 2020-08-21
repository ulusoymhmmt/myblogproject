using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UlusoyBlogFront.ApiServices.Concrete;
using UlusoyBlogFront.ApiServices.Interfaces;

namespace UlusoyBlogFront
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddHttpContextAccessor(); //http context nesnesine erismemiz icin normalde controller clasindan olmasi gerekiyordu fakat biz context accessor i controller olmayan cs classlarinda context nesnesine erismek icin kullaniriz
            services.AddSession();///Token i session a gonderecegimiz icin configure ettik

            services.AddHttpClient<ICategoryApiService, CategoryApiManager>(); //interface i kur modeli yerlestir manager i kur service de http client in icine bu interface i manager ile gonder onu gordugunde bunu orneklesin
            services.AddHttpClient<IBloggApiService, BloggApiManager>();

            services.AddControllersWithViews();
            services.AddHttpClient<IImageApiService, ImageApiManager>(); 
            services.AddHttpClient<IAuthApiService, AuthApiManager>(); // bunlarin icindede http client kullandir dedik
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            
            
            
            app.UseSession(); //session un use u
            
            app.UseRouting();
            app.UseStaticFiles();
            
            app.UseEndpoints(endpoints =>
            {
                //birtane rotalama artik bizim sitemiz icin yetmedi area ekledikten sonra demekki ne yapacagiz ayri ayri rotalarin ayri ayri viewlari modelleri falan olacak front end 
       
            endpoints.MapControllerRoute(name:"areas",pattern:"{area}/{controller=Blogg}/{action=Index}/{id?}");

            endpoints.MapControllerRoute(name:"default",pattern:"{controller=Home}/{action=Index}/{id?}");
                
            });
        }
    }
}
