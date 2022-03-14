using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Esdc.Ccoe.AspNetCore.Mvc.Cdts.Aad
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
