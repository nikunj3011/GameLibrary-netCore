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
using AutoMapper;
using Crypto.API;
using GameLibrary.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net.Http.Headers;

namespace GameLibrary
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }
        public IConfiguration _config { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                    .WithOrigins("https://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            services.Configure<MailSettings>(_config.GetSection("MailSettings")); 
            services.AddSignalR(); //configures app to use signalr
            services.AddTransient<IMailService, Services.MailService>();
            services.AddIdentity<StoreUser, IdentityRole>(cfg=>
            {
                cfg.User.RequireUniqueEmail = true;

            })
                .AddEntityFrameworkStores<GameContext>();

            //services.AddAuthentication().AddCookie()
            //    .AddJwtBearer(cfg=>
            //    {
            //        cfg.TokenValidationParameters = new TokenValidationParameters()
            //        {
            //            ValidIssuer = _config["Token:Issuer"],
            //            ValidAudience = _config["Token:Audience"],
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]))
            //        };
            //    });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.Audience = _config["AAD:ResourceId"];
                    opt.Authority = $"{_config["AAD:Instance"]}{_config["AAD:TenantId"]}";
                });

            services.AddDbContext<GameContext>(cfg=>
            {
                cfg.UseSqlServer(_config.GetConnectionString("GameConnectionString"));
            });

            services.AddTransient<GameSeeder>();

            services.AddScoped<IGameRepository, GameRepository>(); 

            //services.AddTransient<IMailService, Services.MailRequest>();

            //add automapper to map model and viewmodel
            //we can use this or other one (auto search for profile class or manual profile map
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new GameMappingProfile());
            });

            services.AddHttpClient("gameService", c =>
            {
                c.BaseAddress = new Uri( _config["GameSystemAPIService"]);
                c.DefaultRequestHeaders.Add("Accept", "application/json");
                c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiJRWHBvWmtFVUdvMXdheDU3IiwianRpIjoiY2NkOWZkNThlNzEzYzgwMzkyMjllZGJjMzg4OTJlMjY2NWEyMDJhODQ1Njg5OWEwMjZkOWE1ZWNlYTkyMDhmNjhhNWU5OGEyYzBhMjViNjgiLCJpYXQiOjE2MzA1ODc0MjIsIm5iZiI6MTYzMDU4NzQyMiwiZXhwIjoxNjYyMTIzNDIyLCJzdWIiOiJhZG1pbiIsInNjb3BlcyI6WyJGdWxsX0FjY2VzcyIsIk9yZ2FuaXphdGlvbiIsIkVtcGxveWVlcyIsIlRpbWVzaGVldCIsIlBheXJvbGwiLCJQYXlyb2xsX0xvYW5zIl19.MYeqEsJmm1XTYZp3obxoURNhXVXZXYs3sISHay_Jq9MJdIlGiljsrRKap2-kPmUMIeiv-xT89j_r0pv2yiXzazGg1EPZG5RBiy6iheAsJnnW55o2SK0xwtCA33yTRtAAdzaEUgxIebBsU8XXwCYoH0SrNLDqjXRmuWUR6B_-IJwHfOAkrg9moYmsWVQae3CvBXk2NKCRxA6vJ5qdB0UH_PDmkaporJcwlmtOM5bT3Yq3meIhdGjB5iXOQo3uqhuQo4lBpf4QIHStu0lQzGliPZI_3KETzPgW1SXhq8lkY65XgbngDUGR029nfAx_oOzelEK2FHLwuM6ISMtLPLNesA");
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
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
            app.UseCors("CorsPolicy");
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
                cfg.MapHub<CryptoHub>("/CryptoAPI");

                cfg.MapHub<GameHub>("/gamehub");


            });
            
        }
    }
}
