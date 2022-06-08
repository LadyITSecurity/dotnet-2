//using Avalonia;

using Grpc.Net.Client;

using MusicCatalogClient;

using MusicCatalogServer.Api;

using System;
using System.Threading.Tasks;

namespace MusicCatalogConcoleServer
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            //BuildAvaloniaApp()
            //.StartWithClassicDesktopLifetime(args);


            using var channel = GrpcChannel.ForAddress("http://localhost:5001");
            var client = new MusicCatalog.MusicCatalogClient(channel);
            var song = new Song() { Title = "Forgiven" };
            song.Singers.Add("Skillet");
            song.Singers.Add("Three days grace");
            song.Genres.Add("Rock");

            await client.AddSongAsync(song);

            song.Title = "Over and Over";
            song.Singers.Add("Three Days Grace");
            song.Genres.Add("Rock");
            song.Genres.Add("Hardrock");
            await client.AddSongAsync(song);

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
