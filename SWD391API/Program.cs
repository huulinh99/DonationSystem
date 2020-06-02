
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SWD391API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Run();
        }

        public static IWebHost CreateWebHostBuilder(string[] args) => WebHost.CreateDefaultBuilder(args)
                .UseIISIntegration()
                .UseStartup<Startup>().Build();
    }   
}
