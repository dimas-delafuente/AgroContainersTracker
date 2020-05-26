using AgroContainerTracker.Core.Services;
using AgroContainerTracker.Core.Services.Reports;
using AgroContainerTracker.Infrastructure.Services;
using AgroContainerTracker.Infrastructure.Services.Reports;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace AgroContainerTracker.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IContainerService, ContainerService>();
            services.AddTransient<IPalotService, PalotService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ISupplierService, SupplierService>();
            services.AddTransient<ICreditorService, CreditorService>();
            services.AddTransient<ICarrierService, CarrierService>();
            services.AddTransient<IRateService, RateService>();
            services.AddTransient<IPackagingService, PackagingService>();
            services.AddTransient<IFruitService, FruitService>();

            services.AddSingleton<IReportService, PackagingMovementsReportService>();
            return services;
        }
    }
}
