using MusicCatalogServer.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();
builder.Services.AddSingleton<MusicCatalog>();

var app = builder.Build();
app.MapGrpcService<MusicCatalogService>();

app.Run();
