using FilmIzle.Business.DiContainer;
using FilmIzle.DataAccess.Concrete.EntityFrameworkCore.Context;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmIzle.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get; set; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();
            services.AddDistributedMemoryCache();

            services.AddContainerWithDependencies(Configuration);
            services.AddAutoMapper(typeof(Startup));
            services.AddCustomValidator();
            services.AddControllersWithViews().AddFluentValidation();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area}/{controller=home}/{action=index}/{id?}"
                    );

                //endpoints.MapControllerRoute(
                //    name: "category",
                //    pattern: "{controller=home}/{action=category}/{name?}/{page?}"
                //    );

                endpoints.MapControllerRoute(
                    name: default,
                    pattern: "{controller=film}/{action=index}/{name?}"///{page?}
                    );
            });
        }
    }
}
