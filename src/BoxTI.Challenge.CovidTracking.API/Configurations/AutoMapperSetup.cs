using BoxTI.Challenge.CovidTracking.Services.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BoxTI.Challenge.CovidTracking.API.Configurations
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(ViewModelToDomainMappingProfile));
        }
    }
}
