using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using MusicCatalogServer;

namespace OrderAccountingSystem
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
                    webBuilder.UseStartup<Startup>();
                });
    }
}










//using MusicCatalogServer.Services;

//var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddGrpc();
////builder.Services.AddSingleton<MusicCatalog>();

//var app = builder.Build();
//app.MapGrpcService<MusicCatalogService>();

//app.Run();
