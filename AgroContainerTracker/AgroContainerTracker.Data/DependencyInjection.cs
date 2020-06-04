﻿using AgroContainerTracker.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AgroContainerTracker.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataLayerServices(this IServiceCollection services, string connectionString)
        {
            //dotnet ef --startup-project ../AgroContainerTracker migrations add MigrationName (AgroContainerTracker.Data)
            //Then: cd AgroContainerTracker -> dotnet ef database update

            services.AddDbContext<ApplicationContext>(options => options
                .UseMySql(
                    connectionString,
                    b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName))
            );

            return services;
        }
    }
}
