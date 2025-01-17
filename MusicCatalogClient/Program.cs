using Avalonia;
using Avalonia.Reactive;

using Grpc.Net.Client;

using MusicCatalogServer.Api;

using System;
using System.Threading.Tasks;

namespace MusicCatalogClient
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
        }

            using var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client = new MusicCatalog.MusicCatalogClient(channel);

            await client.AddSongAsync(new Song()
            {
                Title = "Forgiven",
                Genres = { "Rock", "Hardrock" },
                Singers = { "Skillet" },
                DurationSecs = 123
            });


            await client.AddSongAsync(new Song()
            {
                Title = "Over and Over",
                Genres = { "Rock" },
                Singers = { "Three Days Grace" },
                DurationSecs = 123
            });

            Console.WriteLine(client.ToString());

            var searchSong = new Song();
            var reply = await client.SearchSongAsync(searchSong);
            Console.WriteLine(reply);


            //await client.Add(new GenreRequest() { Name = "Pop" });
            //await client.AddGenreAsync(new GenreRequest() { Name = "Classic" });
            //await client.AddGenreAsync(new GenreRequest() { Name = "Alternate" });
            //var reply = await client.SearchGenreAsync(new SearchRequest() { Name = "" });
            //Console.WriteLine(reply);
            Console.ReadKey();

           // await client.DeleteGenreAsync(new Id() { "Alternate" });
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        //public static AppBuilder BuildAvaloniaApp()
        //=> AppBuilder.Configure<App>()
        //    .UsePlatformDetect()
        //    .LogToTrace();
    }
}
