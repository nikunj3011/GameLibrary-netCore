using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GameLibrary.Data;
using GameLibrary.Data.Entities;
using GameLibrary.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Newtonsoft.Json;

namespace GameLibrary
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<StoreUser, IdentityRole>(cfg=>
            {
                cfg.User.RequireUniqueEmail = true;

            })
                .AddEntityFrameworkStores<GameContext>();

            services.AddAuthentication().AddCookie()
                .AddJwtBearer(cfg=>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = _config["Token:Issuer"],
                        ValidAudience = _config["Token:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]))
                    };
                });

            services.AddDbContext<GameContext>(cfg=>
            {
                cfg.UseSqlServer(_config.GetConnectionString("GameConnectionString"));
            });

            services.AddTransient<GameSeeder>();

            services.AddScoped<IGameRepository, GameRepository>(); 

            services.AddTransient<IMailService, NullMailService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //support for real mail afterwards
            services.AddControllersWithViews()
                .AddNewtonsoftJson(cfg=>cfg.SerializerSettings
                                    .ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseDefaultFiles();
            if (env.IsEnvironment("Development")) {
                app.UseDeveloperExceptionPage();
            }

            else { 
                app.UseExceptionHandler("/error");
            }

            app.UseStaticFiles();
            app.UseNodeModules();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(cfg=>
            {
                cfg.MapControllerRoute("Fallback",
                    "{controller}/{action}/{id?}",
                    new { controller = "App", action = "Index" });
                //cfg.MapControllerRoute("API",
                //    "api/{controller}",
                //    new { controller = "GameAPI", action = "Get" });
            });
        }
    }
}
