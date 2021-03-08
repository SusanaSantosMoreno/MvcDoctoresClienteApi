using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcDoctoresClienteApi.Services;

namespace MvcCore {
    public class Startup {

        IConfiguration configuration;

        public Startup (IConfiguration conf) {
            this.configuration = conf;
        }
        
        public void ConfigureServices(IServiceCollection services) {
            services.AddTransient(x => new ServiceApiDepartamentos(this.configuration["urlDepartamentos"]));
            services.AddTransient(x => new ServiceEmpleados(this.configuration["urlApiOAUTHEmpleados"]));

            //configuramos los services para session y seguridad
            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(15);
                options.Cookie.IsEssential = true;
            });
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie();

            services.AddControllersWithViews(options => options.EnableEndpointRouting = false);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthorization();
            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(options => {
                options.MapRoute(
                    name: "default",
                    template: "{controller=Doctores}/{action=Index}/{id?}"
                );
            });
        }
    }
}
