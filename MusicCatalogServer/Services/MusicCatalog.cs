using Grpc.Core;
using MusicCatalogServer.Api;
using System.Collections.Concurrent;

namespace MusicCatalogServer.Services
{
    public class MusicCatalog
    {
        private readonly ConcurrentDictionary<int, Genre> _genres = new();

        private readonly ILogger<MusicCatalogService> _logger;
        public MusicCatalog(ILogger<MusicCatalogService> logger)
        {
            _logger = logger;
        }

        public Task<GenreReply> AddGenre(Genre request)
        {
            return Task.Run(() =>
            {
                lock (_genres)
                {
                    //проверка на непустую строку
                    if (_genres.Values.Select(o => o.Name).Contains(request.Name))
                        return new GenreReply() { Success = false };

                    var id = _genres.Keys.Count == 0 ? 1 : _genres.Keys.Max() + 1;
                    var genre = new Genre() { Id = id, Name = request.Name };
                    _genres.TryAdd(id, genre);
                    return new GenreReply() { Success = true, Genre = genre };
                }
            });
        }

        public Task<Reply> RemoveGenre(Id request)
        {
            return Task.FromResult(new Reply());
        }

        public Task<SearchGenreReply> SearchGenre(SearchGenreRequest request)
        {
            return Task.Run(() =>
            {
                var genres = _genres.Values
                .Where(g => g.Name.Contains(request.Name, StringComparison.InvariantCultureIgnoreCase));
                var reply = new SearchGenreReply();
                reply.Genres.AddRange(genres);
                return reply;
            });
        }
    }
}