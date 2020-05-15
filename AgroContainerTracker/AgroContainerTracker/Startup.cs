
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ElectronNET.API;
using AgroContainerTracker.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AgroContainerTracker.Core.Services;
using AgroContainerTracker.Infrastructure.Services;
using System;
using Radzen;

namespace AgroContainerTracker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IContainerService, ContainerService>();
            services.AddTransient<IPalotService, PalotService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ISupplierService, SupplierService>();
            services.AddTransient<ICreditorService, CreditorService>();
            services.AddTransient<ICarrierService, CarrierService>();
            services.AddTransient<IRateService, RateService>();
            services.AddTransient<IPackagingService, PackagingService>();

            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDbContext<ApplicationContext>(options => options
                .UseMySql(Configuration.GetConnectionString("sqlConnection")),
                ServiceLifetime.Transient
            );

        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });



            // ConfigureElectronDesktop();
        }

        //private async void ConfigureElectronDesktop()
        //{
        //    await Electron.WindowManager.CreateWindowAsync();
        //}
    }
}
