using BoxTI.Challenge.CovidTracking.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using BoxTI.Challenge.CovidTracking.CrossCutting.Identity.Data;

namespace BoxTI.Challenge.CovidTracking.API.Configurations
{
    public static class DatabaseSetup
    {
        public static void AddDatabaseSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<CovidTrackingContext>(options =>
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))));

            services.AddDbContext<CovidTrackingContextIdentity>(options =>
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))));
        }
    }
}
