using AutoMapper;
using GameLibrary.Controllers;
using GameLibrary.Data;
using GameLibrary.Data.Entities;
using GameSystems.AsyncDataServices;
using GameSystems.EventProcessing;
using GameSystems.SyncDataServices.Grpc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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
            services.AddIdentity<StoreUser, IdentityRole>(cfg =>
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

            services.AddDbContext<GameContext>(cfg =>
            {
                cfg.UseSqlServer(_config.GetConnectionString("GameConnectionString"));
            });

            services.AddTransient<GameSeeder>();
            services.AddSingleton<IEventProcessor, EventProcessor>(); //one rabbitmq processor throughout lifetime of program
            services.AddHostedService<MessageBusSubscriber>();
            services.AddScoped<IGameRepository, GameRepository>();

            //services.AddTransient<IMailService, Services.MailRequest>();

            //add automapper to map model and viewmodel
            //we can use this or other one (auto search for profile class or manual profile map
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new GameMappingProfile());
            });

            services.AddScoped<IGameDataClient, GameDataClient>();
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            //support for real mail afterwards
            services.AddControllersWithViews()
                .AddNewtonsoftJson(cfg => cfg.SerializerSettings
                                    .ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseDefaultFiles();
            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();
            }

            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseStaticFiles();
            //app.UseNodeModules();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(cfg =>
            {
                cfg.MapControllerRoute("Fallback",
                    "{controller}/{action}/{id?}",
                    new { controller = "App", action = "Index" });
                //cfg.MapControllerRoute("API",
                //    "api/{controller}",
                //    new { controller = "GameAPI", action = "Get" });


            });

            GameswithGrpc.Prep(app);

        }
    }
}
