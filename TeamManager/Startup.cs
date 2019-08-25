using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TeamManager.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace TeamManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options => { options.LoginPath = "/Login"; });
            services.AddMvc().AddRazorPagesOptions(options =>
            {
                options.Conventions.AuthorizeFolder("/");
                options.Conventions.AllowAnonymousToPage("/Login");
            });




            //services.AddDbContext<KarateKidDbContext>(options => options.UseSqlServer(
            //    Configuration["Data:TeamManagerKids:ConnectionString"]));
            //services.AddTransient<IKidsRepository, EFKaratekidRepository>();
            //services.AddMvc();
            //services.AddDbContext<KarateKidDbContext>(options => options.UseSqlServer(
            //    Configuration["Data:TeamManagerGroups:ConnectionString"]));
            //services.AddTransient<IGroupsRepository, EFGroupsRepository>();

            //services.AddDbContext<KarateKidDbContext>(options => options.UseSqlServer(
            //  Configuration["Data:TeamManagerDateModel:ConnectionString"]));
            //services.AddTransient<IDateModelRepository, EFDateModelRepository>();



            services.AddDbContext<KarateKidDbContext>(options => options.UseSqlServer(
            Configuration["Data:teammanager20190825105012dbserve:ConnectionString"]));
            services.AddTransient<IDateModelRepository, EFDateModelRepository>();

            services.AddMvc();
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            app.UseMvc();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            //KidsData.EnsureKids(app);
            //KidsData.EnsureGroups(app);
        }
    }
}
