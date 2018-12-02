using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Mints.BLayer.Extensions;
using Mints.BLayer.Models.Identity;
using Mints.Helpers;
using Mints.Middleware;
using Swashbuckle.AspNetCore.Swagger;

namespace Mints
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
            services.AddSingleton(Configuration);
            //var conn = Environment.GetEnvironmentVariable("MYSQLCONNSTR_localdb").ToString();
            services.AddEntityFramework(Configuration.GetConnectionString("Context"));
            var providerOptions = Configuration.GetSection("TokenAuthentication");
            services.Configure<TokenProviderOptions>(providerOptions);
            var tokenProviderOptions = providerOptions.Get<TokenProviderOptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            )
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = tokenProviderOptions.RequireHttpsMetadata;
                options.SaveToken = tokenProviderOptions.SaveToken;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenProviderOptions.SecretKey)),
                    ValidIssuer = tokenProviderOptions.Issuer,
                    ValidAudience = tokenProviderOptions.Audience,
                    ValidateIssuer = tokenProviderOptions.ValidateIssuer,
                    ValidateAudience = tokenProviderOptions.ValidateAudience,
                    ValidateIssuerSigningKey = tokenProviderOptions.ValidateIssuerSigningKey,
                    ValidateLifetime = tokenProviderOptions.ValidateLifetime,
                    ClockSkew = tokenProviderOptions.ClockSkew
                };
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });

            
            services.AddRepositories();

            services.AddTransient<IUserStore<User>, IdentityUserStore>();
            services.AddTransient<IRoleStore<Role>, IdentityRoleStore>();
            services.AddIdentity<User, Role>()
               .AddDefaultTokenProviders();

            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Exam Card Pin Api Doc", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            //app.UseHttpsRedirection();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseStaticFiles();

            //app.RunMigration();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseApiKeyValidation();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "auth-route",
                    template: "api/v1/auth/{action}/{id?}");

                routes.MapRoute(
                    name: "location-route",
                    template: "api/v1/location/{action}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
