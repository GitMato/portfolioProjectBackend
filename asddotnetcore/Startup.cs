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
using System.Text;

using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace asddotnetcore
{
    public class Startup
    {
        private string _secretKey;
        private SymmetricSecurityKey _signinKey;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;          
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Get SecretKey and generate new symmetric key
            _secretKey = Configuration["SecretKey"];
            _signinKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secretKey));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // TODO DON'T ALLOW ALL SITES TO ACCESS
            // Create MyCorsPolicy to allow multiple policies (not needed in this project)
            services.AddCors(o => o.AddPolicy("MyCorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));


            // Add the Cors policy
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("MyCorsPolicy"));
            });

            // Register Project and Tool context for db
            services.AddEntityFrameworkNpgsql().AddDbContext<MyWebApiContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("MyWebApiConnection")));

            // Register identity context for db
            services.AddEntityFrameworkNpgsql().AddDbContext<MyIdentityContext>(options =>
                                                options.UseNpgsql(Configuration.GetConnectionString("MyWebApiConnection")));
            // Register identity
            services.AddIdentity<ApiUser, IdentityRole>(options =>
                { 
                    //password requirements
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequiredUniqueChars = 2;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = true;
                    
                })
                .AddEntityFrameworkStores<MyIdentityContext>()
                .AddDefaultTokenProviders();

            

            // JWT - get options from app settings (JwtIssuerOptions -class)
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions object from appsettings
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signinKey, SecurityAlgorithms.HmacSha256);
            });

            // Configure token validation parameters
            // https://blogs.msdn.microsoft.com/webdev/2017/04/06/jwt-validation-and-authorization-in-asp-net-core/
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signinKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero, // remove delay of token when expire

            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            // add authorization policy for admins
            services.AddAuthorization(options =>
            {        
                // checks if the jwtToken has attribute "Admin" with value "True". JwtIsValidated before this. Thus, the data provided in jwt is legit.
                options.AddPolicy("Admin", policy => policy.RequireClaim("Admin", "True"));
            });

        }

        // This method gets called by the runtime. Configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            
            Console.WriteLine("Portfolio REST API");
            
            var logger = loggerFactory.CreateLogger("Startup");
            logger.LogInformation("Booting");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // use CORS
            app.UseCors("MyCorsPolicy");
            //app.UseCors(builder =>
            //    builder.WithOrigins("*")
            //    .AllowAnyHeader()
            //    .AllowAnyMethod()
            //    .AllowCredentials()
            //    .AllowAnyOrigin()
            //    );

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
