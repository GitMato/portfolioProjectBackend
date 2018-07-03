using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyIdentity.Models;
using MyWebApi.Models;

namespace asddotnetcore
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // TODO DON'T ALLOW ALL SITES TO ACCESS
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));


            //miks tää on tässä? Eikö tän voisi tehä helpomminkin?
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("MyPolicy"));
            });

            // Project and Tool context for fb
            services.AddEntityFrameworkNpgsql().AddDbContext<MyWebApiContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("MyWebApiConnection")));

            // identity context for db
            services.AddEntityFrameworkNpgsql().AddDbContext<MyIdentityContext>(options =>
                                                options.UseNpgsql(Configuration.GetConnectionString("MyWebApiConnection")));
            services.AddIdentity<Admin, IdentityRole>()
                .AddEntityFrameworkStores<MyIdentityContext>()
                .AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            //Logger testing
            //var loggerFactory = new LoggerFactory().AddConsole();

            Console.WriteLine("Hello World!");

            //loggerFactory.MinimumLevel = LogLevel.Debug;
            //loggerFactory.AddDebug(LogLevel.Debug);
            var logger = loggerFactory.CreateLogger("Startup");
            logger.LogWarning("Hi!");
            logger.LogInformation("Hi info!");

            // logger testing ends

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // use CORS
            app.UseCors("MyPolicy");
            //app.UseCors(builder =>
            //    builder.WithOrigins("*")
            //    .AllowAnyHeader()
            //    .AllowAnyMethod()
            //    .AllowCredentials()
            //    .AllowAnyOrigin()
            //    );

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
