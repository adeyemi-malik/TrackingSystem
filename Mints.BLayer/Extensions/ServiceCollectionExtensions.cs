using Mints.DLayer.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Mints.BLayer.Repositories;
using Microsoft.AspNetCore.Builder;

namespace Mints.BLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddEntityFramework(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<Context>(options =>
                    options.UseMySQL(connectionString));
        }

        public static void RunMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<Context>();
                context.Database.Migrate();
            }
        }


        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IFarmerRepository, FarmerRepository>();
            services.AddTransient<IAnimalRepository, AnimalRepository>();
            services.AddTransient<ILocationRepository, LocationRepository>();
            services.AddTransient<IApiClientRepository, ApiClientRepository>();
            return services;
        }
    }
}
