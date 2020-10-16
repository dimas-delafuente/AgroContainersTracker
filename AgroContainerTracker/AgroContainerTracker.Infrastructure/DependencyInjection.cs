using AgroContainerTracker.Core.Reports;
using AgroContainerTracker.Core.Services;
using AgroContainerTracker.Core.Services.Reports;
using AgroContainerTracker.Domain.Reports;
using AgroContainerTracker.Infrastructure.Services;
using AgroContainerTracker.Infrastructure.Services.Reports;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;
using System.Reflection;

namespace AgroContainerTracker.Infrastructure
{

    public static class DependencyInjection
    {
        public static readonly TimeSpan InfiniteTimeSpan;

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IColdRoomService, ColdRoomService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ISupplierService, SupplierService>();
            services.AddTransient<ICreditorService, CreditorService>();
            services.AddTransient<ICarrierService, CarrierService>();
            services.AddTransient<IRateService, RateService>();
            services.AddTransient<IPackagingService, PackagingService>();
            services.AddTransient<IFruitService, FruitService>();

            services.AddTransient<ICampaingService, CampaingService>();
            services.AddTransient<IProductEntryService, ProductEntryService>();
            services.AddTransient<IProductWeighingService, ProductWeighingService>();


            services.AddSingleton<IReportService<PackagingReport>, PackagingReportService>();
            services.AddSingleton<ILabelReportFactory, LabelReportFactory>();
            services.AddHttpClient<ILabelReportService, A5LabelReportService>(client =>
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/gif"));
            });

            services.AddHttpClient<ILabelReportService, StickerLabelReportService>(client =>
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/gif"));
            });

            return services;
        }
    }
}
