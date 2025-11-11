using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MovieAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // This line tells ASP.NET Core to use your Startup class
                    webBuilder.UseStartup<Startup>();
                });
    }
}
