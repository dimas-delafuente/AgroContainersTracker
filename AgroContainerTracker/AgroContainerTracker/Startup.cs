
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ElectronNET.API;
using Radzen;
using AgroContainerTracker.Infrastructure;
using AgroContainerTracker.Data;
using FluentValidation.AspNetCore;
using AgroContainerTracker.Infrastructure.Validators;

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
            services.AddRazorPages()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RateValidator>());
            services.AddServerSideBlazor();

            services.AddInfrastructureServices();
            services.AddDataLayerServices(Configuration.GetConnectionString("sqlConnection"));

            // Radzen Services
            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();
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
