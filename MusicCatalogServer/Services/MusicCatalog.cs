using MusicCatalogServer.Api;
using MusicCatalogServer.Repository;

using System.Collections.Concurrent;
using System.Linq;

namespace MusicCatalogServer.Services
{
    public sealed class MusicCatalog
    {
        private readonly ISongRepository _songRepository;
        private readonly ILogger<MusicCatalog> _logger;

        public MusicCatalog(
            ISongRepository songRepository,
            ILogger<MusicCatalog> logger)
        {
            _songRepository = songRepository;
            _logger = logger;
        }

        public Task<Reply> AddSong(Song request)
        {
            lock (_songRepository)
            {
                int id = 0;
                var repository = _songRepository.GetAll().Result;
                var reply = new Reply();

                if (_songRepository == null)
                    _songRepository.Add(request);

                var found = repository.Where(o => o.Title == request.Title && o.Singers == request.Singers);

                if (found != null)
                    return Task.FromResult(new Reply { Success = false, ErrorMessage = "The song is already there!" });

                id = repository.Count == 0 ? 1 : repository.Max(x => x.Id) + 1;
                var song = request;
                song.Id = id;
                _songRepository.Add(song);
                return Task.FromResult(new Reply { Success = true, ErrorMessage = "" });
            }
        }

        public async Task<Reply> DeleteSong(DeleteSongRequest request)
        {
            var result = await _songRepository.Remove(request.Id);
            return new Reply { Success = result, ErrorMessage = result ? String.Empty : "Id not found" };
        }

        public Task<SongList> SearchSong(Song request)
        {
            return Task.Run(() =>
            {
                var reply = new SongList();
                if (request.CalculateSize() == new Song().CalculateSize())
                {
                    reply.Songs.AddRange(_songRepository.GetAll().Result);
                    return reply;
                }
                var songs = _songRepository.GetAll().Result;

                var str = request.Title.ToString().ToLower().Split(' ');
                List<string> substring = new();
                foreach (var i in str)
                    substring.Add(i);

                if (request.Singers != null)
                {
                    foreach (var singer in request.Singers)
                    {
                        var s = singer.ToString().ToLower().Split(" ");
                        foreach (var r in s)
                            substring.Add(r);
                    }
                }

                if (request.Genres != null)
                {
                    foreach (var genres in request.Genres)
                    {
                        var s = genres.ToString().ToLower().Split(" ");
                        foreach (var r in s)
                            substring.Add(r);
                    }
                }

                List<Song> result = new();

                foreach (var i in substring)
                    foreach (var song in songs)
                    {
                        if (request.Title != null)
                            if (song.Title.ToLower().Contains(i.ToString()) && result.Find(g => g.Id == song.Id) == null)
                                result.Add(song);
                        if (request.Singers != null)
                            foreach (var singer in song.Singers)
                                if (singer.ToLower().Contains(i.ToString()) && result.Find(g => g.Id == song.Id) == null)
                                    result.Add(song);
                        if (request.Genres != null)
                            foreach (var genre in song.Genres)
                                if (genre.ToLower().Contains(i.ToString()) && result.Find(g => g.Id == song.Id) == null)
                                    result.Add(song);
                    }
                reply.Songs.AddRange(result);
                return reply;
            });
        }
    }
}
