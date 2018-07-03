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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyIdentity.Models;
using MyWebApi.Models;
using Microsoft.IdentityModel.Tokens;

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


            // Eikö tän voisi tehä helpomminkin?
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("MyPolicy"));
            });

            // Register Project and Tool context for db
            services.AddEntityFrameworkNpgsql().AddDbContext<MyWebApiContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("MyWebApiConnection")));

            // Register identity context for db
            services.AddEntityFrameworkNpgsql().AddDbContext<MyIdentityContext>(options =>
                                                options.UseNpgsql(Configuration.GetConnectionString("MyWebApiConnection")));
            // Register identity
            services.AddIdentity<Admin, IdentityRole>()
                .AddEntityFrameworkStores<MyIdentityContext>()
                .AddDefaultTokenProviders();

            // JWT - get options from app settings (JwtIssuerOptions -class)
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                //options.SigningCredentials = new SigningCredentials(_signinKey, SecurityAlgorithms.HmacSha256);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            

            Console.WriteLine("Portfolio REST API");
            
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
