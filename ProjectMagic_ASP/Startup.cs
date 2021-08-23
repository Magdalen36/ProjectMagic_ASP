using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectMagic.Services;
using ProjectMagic_ASP.Models;
using ProjectMagic_ASP.Models.Forms;
using ProjectMagic_ASP.Services;
using ProjectMagic_ASP.Services.Bases;
using ProjectMagic_ASP.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMagic_ASP
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
            services.AddControllersWithViews();

            services.AddSession(
               options => {
                   options.IdleTimeout = TimeSpan.FromMinutes(15);
                   options.Cookie.Name = "ProjectMagicCookie";
                   options.Cookie.HttpOnly = true;
                   options.Cookie.IsEssential = true;
                }
           );

            services.AddScoped<IService<EditionModel, EditionForm>, EditionService>();
            services.AddScoped<IService<CardModel, CardForm>, CardService>();
            services.AddScoped<IService<UserModel, UserForm>, UserService>();
            services.AddScoped<IService<CollectionModel, CollectionForm>, CollectionService>();
            services.AddScoped<IService<ColorModel, ColorForm>, ColorService>();
            services.AddScoped<IService<TypeModel, TypeForm>, TypeService>();
            services.AddScoped<IService<SousTypeModel, SousTypeForm>, SousTypeService>();
            services.AddScoped<IService<RarityModel, RarityForm>, RarityService>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();
            SessionUtils.Services = app.ApplicationServices;

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
