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


                //songs = (List<Song>)songs.Where(g => request.Singers.Contains(g.Singers)));
                //songs = songs.Contains(x => x.Singers.Except(request.Singers).toList());
                //if (request.Singers != null)
                //{
                //    //songs = songs.Find(x => request.Singers.Contains(y => x. ))
                //    //foreach (var singer in request.Singers)
                //    //    songs = songs
                //    //    .Select(o => o.Singers.Contains(singer));
                //}

                //if (request.Genres != null)
                //    foreach (var genre in request.Genres)
                //        songs = songs
                //            .Where(g => g.Genres.Contains<List<string>>(request.Genres));



                var str = request.Title.ToString().ToLower();

                var singers = request.Title.ToLower().Split(' ');
                var found = songs.Where(o => o.Title == request.Title && o.Singers == request.Singers);
                //songs = songs.Find(g => g.Title == request.Title);
                    //|| g.Singers.ToString().Contains(request.Singers.ToString())
                    //|| g.Genres.ToString().Contains(request.Genres.ToString()));


                //songs = (List<Song>)songs.Where(g => g.Title.ToString().ToLower().Split(' ').Intersect(singers) ||
                //    g.Singers == request.Singers ||
                //    g.Genres == request.Genres);



                //if (request.Singers != null)
                //{
                //    foreach (var singer in request.Singers)
                //    {
                //        var s = singer.ToLower().Split(" ");
                //        foreach (var r in s)
                //            str.Append(r);
                //    }
                //}
                //if (request.Genres != null)
                //{
                //    foreach (var genre in request.Genres)
                //        str.Intersect(genre.ToLower().Split(" "));
                //}
                ////if (request.Singers != null)
                ////{
                ////    foreach (var singer in request.Singers)
                ////        str.Intersect(singer.ToString().ToLower().Split(" "));
                ////}

                //foreach (var i in str)
                //    songs = (List<Api.Song>)songs.Where(g => g
                //        .ToString()
                //        .ToLower()
                //        .Contains(i.ToString()));

                //songs = (List<Api.Song>)_songRepository
                //    .GetAll()
                //    .Result
                //    .Where(g => g.DurationSecs == request.DurationSecs);



                //if (request.Title != null)
                //    foreach (var i in str)
                //        songs = (List<Api.Song>)songs.Where(g => g.Title.ToLower().Contains(i));

                //if (request.Singers != null)
                //{
                //    foreach (var singer in request.Singers)
                //        str = singer.ToLower().Split(" ");
                //}
                //foreach (var i in str)
                //    songs = (List<Api.Song>)songs.Where(g => g.Singers.ToString().ToLower().Contains(i));

                //if (request.Genres != null)
                //{
                //    foreach (var singer in request.Genres)
                //        str = singer.ToLower().Split(" ");
                //}
                //foreach (var i in str)
                //    songs = (List<Api.Song>)songs.Where(g => g.Genres.ToString().ToLower().Contains(i));

                //songs = (List<Api.Song>)songs.Where(g => g.DurationSecs == request.DurationSecs);




                //songs = (List<Api.Song>)songs.Where(g => g.Title == request.Title);
                //songs = (List<Api.Song>)songs.Where(g => g.Singers == request.Singers);
                //songs = (List<Api.Song>)songs.Where(g => g.Genres == request.Genres);

                //songs = (List<Song>)songs.Where(g => request.Singers.Contains(g.Singers)));
                //songs = songs.Contains(x => x.Singers.Except(request.Singers).toList());
                //if (request.Singers != null)
                //{
                //    //songs = songs.Find(x => request.Singers.Contains(y => x. ))
                //    //foreach (var singer in request.Singers)
                //    //    songs = songs
                //    //    .Select(o => o.Singers.Contains(singer));
                //}

                //if (request.Genres != null)
                //    foreach (var genre in request.Genres)
                //        songs = songs
                //            .Where(g => g.Genres.Contains(genre, StringComparison.InvariantCultureIgnoreCase));

                reply.Songs.AddRange(found);
                return reply;
            });
        }

        //    public Task<SearchGenreReply> SearchGenre(SearchRequest request)
        //    {
        //        return Task.Run(() =>
        //        {
        //            var genres = _genres.Values
        //            .Where(g => g.Name.Contains(request.Name, StringComparison.InvariantCultureIgnoreCase));
        //            var reply = new SearchGenreReply();
        //            reply.Genres.AddRange(genres);
        //            return reply;
        //        });
        //    }

        //    public Task<SearchSingerReply> SearchSinger(SearchRequest request)
        //    {
        //        return Task.Run(() =>
        //        {
        //            var singers = _singers.Values
        //            .Where(g => g.Name.Contains(request.Name, StringComparison.InvariantCultureIgnoreCase));
        //            var reply = new SearchSingerReply();
        //            reply.Singers.AddRange(singers);
        //            return reply;
        //        });
        //    }

        //    public Task<SearchSongReply> SearchSong(SearchRequest request)
        //    {
        //        return Task.Run(() =>
        //        {
        //            var songs = _songs.Values
        //            .Where(g => g.Name.Contains(request.Name, StringComparison.InvariantCultureIgnoreCase));
        //            var reply = new SearchSongReply();
        //            reply.Songs.AddRange(songs);
        //            return reply;
        //        });
        //    }
    }
}

