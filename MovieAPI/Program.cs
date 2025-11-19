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
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Hosting;

//namespace MovieAPI
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            CreateHostBuilder(args).Build().Run();
//        }

//        public static IHostBuilder CreateHostBuilder(string[] args) =>
//            Host.CreateDefaultBuilder(args)

//                // Suprascriem configurarea cu variabile din launchSettings.json
//                .ConfigureAppConfiguration((hostingContext, config) =>
//                {
//                    var dbName = Environment.GetEnvironmentVariable("MovieDbName");
//                    var syncHost = Environment.GetEnvironmentVariable("SyncHost");

//                    var overrides = new List<KeyValuePair<string, string>>();

//                    if (!string.IsNullOrWhiteSpace(dbName))
//                    {
//                        overrides.Add(new KeyValuePair<string, string>(
//                            "MongoDbSettings:DatabaseName", dbName));
//                    }

//                    if (!string.IsNullOrWhiteSpace(syncHost))
//                    {
//                        overrides.Add(new KeyValuePair<string, string>(
//                            "SyncServiceSettings:Host", syncHost));
//                    }

//                    // Aplicăm suprascrierea în configurare
//                    if (overrides.Count > 0)
//                        config.AddInMemoryCollection(overrides);
//                })

//                .ConfigureWebHostDefaults(webBuilder =>
//                {
//                    webBuilder.UseStartup<Startup>();
//                });
//    }
//}
