using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

using Grpc.Net.Client;

using MusicCatalogClient;

using MusicCatalogServer.Api;

using System;
using System.Threading.Tasks;

namespace MusicCatalogConcoleServer
{
    internal class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static async Task Main(string[] args)
        {
            BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);


            using var channel = GrpcChannel.ForAddress("http://localhost:5001");
            var client = new MusicCatalog.MusicCatalogClient(channel);
            await client.AddGenreAsync(new Genre() { Name = "Rock" });
            await client.AddGenreAsync(new Genre() { Name = "Pop" });
            await client.AddGenreAsync(new Genre() { Name = "Classic" });
            await client.AddGenreAsync(new Genre() { Name = "Alternate" });
            var reply = await client.SearchGenreAsync(new SearchRequest() { Name = "" });
            Console.WriteLine(reply);
            Console.ReadKey();
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace();
    }
}
