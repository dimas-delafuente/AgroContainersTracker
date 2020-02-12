using ElectronNET.API;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace AgroContainerTracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Run();
        }

        public static IWebHost CreateHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseElectron(args)
                .Build();
        }
    }
}
