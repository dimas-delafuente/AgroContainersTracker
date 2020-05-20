using AgroContainerTracker.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AgroContainerTracker.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataLayerServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationContext>(options => options
                .UseMySql(connectionString),
                ServiceLifetime.Transient
            );

            return services;
        }
    }
}
