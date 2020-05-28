
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
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using AgroContainerTracker.Areas.Identity;
using AgroContainerTracker.Data.Contexts;
using AgroContainerTracker.Domain.Reports;

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
            services.Configure<ReportsConfig>(Configuration.GetSection("ReportsConfig"));

            services.AddRazorPages()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RateValidator>());
            services.AddServerSideBlazor();

            services.AddInfrastructureServices();
            services.AddDataLayerServices(Configuration.GetConnectionString("sqlConnection"));

            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<ApplicationContext>();

            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();


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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
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
