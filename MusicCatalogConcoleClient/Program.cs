using Grpc.Net.Client;
using MusicCatalogServer.Api;

using var channel = GrpcChannel.ForAddress("http://localhost:5268");
var client = new MusicCatalog.MusicCatalogClient(channel);
await client.AddGenreAsync(new Genre() { Name = "Rock" });
await client.AddGenreAsync(new Genre() { Name = "Pop" });
await client.AddGenreAsync(new Genre() { Name = "Classic" });
await client.AddGenreAsync(new Genre() { Name = "Alternate" });
var reply = await client.SearchGenreAsync(new SearchGenreRequest() { Name = "O" });
Console.WriteLine(reply);
Console.ReadKey();