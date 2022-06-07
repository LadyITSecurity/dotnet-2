using Grpc.Core;
using MusicCatalogServer.Api;

using System.Collections.Concurrent;

namespace MusicCatalogServer.Services
{
    public class MusicCatalog
    {
        private readonly ConcurrentDictionary<int, Genre> _genres = new();
        private readonly ConcurrentDictionary<int, Singer> _singers = new();
        private readonly ConcurrentDictionary<int, Song> _songs = new();

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

        public Task<SingerReply> AddSinger(Singer request)
        {
            return Task.Run(() =>
            {
                lock (_singers)
                {
                    //проверка на непустую строку
                    if (_singers.Values.Select(o => o.Name).Contains(request.Name))
                        return new SingerReply() { Success = false };

                    var id = _singers.Keys.Count == 0 ? 1 : _genres.Keys.Max() + 1;
                    var singer = new Singer() { Id = id, Name = request.Name };
                    _singers.TryAdd(id, singer);
                    return new SingerReply() { Success = true, Singer = singer };
                }
            });
        }


        public Task<SearchGenreReply> SearchGenre(SearchRequest request)
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

        public Task<SearchSingerReply> SearchSinger(SearchRequest request)
        {
            return Task.Run(() =>
            {
                var singers = _singers.Values
                .Where(g => g.Name.Contains(request.Name, StringComparison.InvariantCultureIgnoreCase));
                var reply = new SearchSingerReply();
                reply.Singers.AddRange(singers);
                return reply;
            });
        }

        public Task<SearchSongReply> SearchSong(SearchRequest request)
        {
            return Task.Run(() =>
            {
                var songs = _songs.Values
                .Where(g => g.Name.Contains(request.Name, StringComparison.InvariantCultureIgnoreCase));
                var reply = new SearchSongReply();
                reply.Songs.AddRange(songs);
                return reply;
            });
        }
    }
}
