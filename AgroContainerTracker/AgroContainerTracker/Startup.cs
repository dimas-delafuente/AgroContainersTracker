
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
            services.AddScoped<ICountryService, CountryService>();

            services.AddScoped<IContainerService, ContainerService>();
            services.AddScoped<IPalotService, PalotService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<ICreditorService, CreditorService>();
            services.AddScoped<ICarrierService, CarrierService>();



            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDbContext<ApplicationContext>(options => options
                .UseMySql(Configuration.GetConnectionString("sqlConnection")
            ));

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
