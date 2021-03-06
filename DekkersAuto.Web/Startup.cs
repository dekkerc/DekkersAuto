﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Autofac;
using DekkersAuto.Dependencies;
using DekkersAuto.Database;
//I, Christopher Dekker, student number 000311337, certify that all code
//submitted is my own work; that I have not copied it from any other source
//I also certify that I have not allowed by work to be copied by others
namespace DekkersAuto.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    option => option.EnableRetryOnFailure()));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
           {
               options.Password.RequireDigit = true;
               options.Password.RequiredLength = 8;
               options.Password.RequireNonAlphanumeric = false;
               options.Password.RequireUppercase = true;
               options.Password.RequireLowercase = true;
           });

      
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<DependencyModule>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {
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

            app.UseAuthentication();
            CreateUserRoles(services).Wait();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private async Task CreateUserRoles(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            IdentityResult roleResult;
            //Adding Admin Role
            var adminCheck = await roleManager.RoleExistsAsync("Admin");
            if (!adminCheck)
            {
                //create the roles and seed them to the database
                roleResult = await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            //Adding Secretary Role
            var secretaryCheck = await roleManager.RoleExistsAsync("Secretary");
            if (!secretaryCheck)
            {
                //create the roles and seed them to the database
                roleResult = await roleManager.CreateAsync(new IdentityRole("Secretary"));
            }
        }
    }
    
}
