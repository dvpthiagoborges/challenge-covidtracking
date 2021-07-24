using BoxTI.Challenge.CovidTracking.CrossCutting.Identity.Base;
using BoxTI.Challenge.CovidTracking.CrossCutting.Identity.Data;
using BoxTI.Challenge.CovidTracking.CrossCutting.Identity.Models;
using BoxTI.Challenge.CovidTracking.Data.Context;
using BoxTI.Challenge.CovidTracking.Data.Repository;
using BoxTI.Challenge.CovidTracking.Data.Repository.Interfaces;
using BoxTI.Challenge.CovidTracking.ExternalData.Interfaces;
using BoxTI.Challenge.CovidTracking.Models.ImportExport.Interfaces;
using BoxTI.Challenge.CovidTracking.Models.ImportExport.Services;
using BoxTI.Challenge.CovidTracking.Services.Interfaces;
using BoxTI.Challenge.CovidTracking.Services.Notifications;
using BoxTI.Challenge.CovidTracking.Services.Notifications.Interfaces;
using BoxTI.Challenge.CovidTracking.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BoxTI.Challenge.CovidTracking.API.Configurations
{
    public static class DependencyInjectionSetup
    {
        public static void AddDependencyInjectionSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Notifications
            services.AddScoped<INotifier, Notifier>();

            // Context
            services.AddScoped<CovidTrackingContext>();

            // ContextIdentity
            services.AddScoped<CovidTrackingContextIdentity>();

            // Repositorys
            services.AddScoped<IInfoCovidRepository, InfoCovidRepository>();

            // Services
            services.AddScoped<IInfoCovidService, InfoCovidService>();
            services.AddScoped<ICovidTrackingService, CovidTrackingService>();
            services.AddScoped<IImportExportService, ImportExportService>();

            services.AddScoped<IUser, AspNetUser>();
        }
    }
}
