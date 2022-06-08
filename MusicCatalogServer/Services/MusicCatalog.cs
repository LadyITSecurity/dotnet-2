using MusicCatalogServer.Api;
using MusicCatalogServer.Repository;

using System.Collections.Concurrent;

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
                //if (repository != null)
                //{
                if (repository.Select(o => o.Title == request.Title)
                    == repository.Select(o => o.Singers == request.Singers))
                    return Task.FromResult(new Reply() { Success = false, ErrorMessage = "The song is already there!" });
                id = 1;
                //}
                //else
                id = repository.Count == 0 ? 1 : repository.Max(x => x.Id) + 1;
                var song = request;
                song.Id = id;
                _songRepository.Add(song);
                return Task.FromResult(new Reply() { Success = true });
            }
        }

        public async Task<Reply> DeleteSong(DeleteSongRequest request)
        {
            var result = await _songRepository.Remove(request.Id);
            return new Reply { Success = result, ErrorMessage = result ? String.Empty : "Id not found" };
        }

        public Task<SongList> SearchSong(Song request)
        {
            throw new InvalidOperationException();
            return Task.Run(() =>
            {
                var reply = new SongList();
                if (request.Singers == null && request.Genres == null && request.Title == null)
                {

                    reply.Songs.AddRange(_songRepository.GetAll().Result);
                    return reply;
                }
                throw new InvalidCastException();

                var songs = _songRepository.GetAll().Result;
                songs = (List<Song>)songs.Where(g => g.Title == request.Title);
                songs = (List<Song>)songs.Where(g => g.Singers == request.Singers);
                songs = (List<Song>)songs.Where(g => g.Genres == request.Genres);
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

                reply.Songs.AddRange(songs);
                return reply;
            });
        }

        //public Task<GenreReply> AddGenre(Genre request)
        //{
        //    return Task.Run(() =>
        //    {
        //        lock (_genres)
        //        {
        //            //проверка на непустую строку
        //            if (_genres.Values.Select(o => o.Name).Contains(request.Name))
        //                return new GenreReply() { Success = false };

        //            var id = _genres.Keys.Count == 0 ? 1 : _genres.Keys.Max() + 1;
        //            var genre = new Genre() { Id = id, Name = request.Name };
        //            _genres.TryAdd(id, genre);
        //            return new GenreReply() { Success = true, Genre = genre };
        //        }
        //    });
        //}


        //public Task<GenreReply> AddGenre(GenreRequest request)
        //{
        //    return Task.FromResult(new GenreReply
        //    {
        //        Id = _genreRepository.AddGenresAsync(
        //            new Genre(request.Name)).Result.ToString(),
        //    });
        //}

        //public override Task<SongReply> ChangeSong(SongRequest request, ServerCallContext context)
        //{
        //    Song song = new Song();
        //    if (IsGuid(request.Singer.Id))
        //    {
        //        if (_singerRepository.CheckSingerAsync(Guid.Parse(request.Singer.Id)).Result)
        //        {
        //            song.Singer = _singerRepository.GetSingerAsync(Guid.Parse(request.Singer.Id)).Result;
        //        }
        //        else
        //        {
        //            order.Customer = new Customer(Guid.Parse(request.Customer.CustomerId), request.Customer.Name, request.Customer.Phone);
        //            _singerRepository.AddCustomerAsync(order.Customer);
        //        }
        //    }
        //    else
        //    {
        //        order.Customer = new Customer(request.Customer.Name, request.Customer.Phone);
        //        _singerRepository.AddCustomerAsync(order.Customer);
        //    }

        //    order.Products = new List<Product>();
        //    foreach (ProductRequest product in request.Products)
        //    {
        //        if (IsGuid(product.ProductId))
        //        {
        //            if (_genreRepository.CheckProductAsync(Guid.Parse(product.ProductId)).Result)
        //            {
        //                order.Products.Add(_genreRepository.GetGenreAsync(Guid.Parse(product.ProductId)).Result);
        //            }
        //            else
        //            {
        //                Product newProduct = new Product(Guid.Parse(product.ProductId), product.Name, product.Price);
        //                order.Products.Add(newProduct);
        //                _genreRepository.AddProductAsync(newProduct);
        //            }
        //        }
        //        else
        //        {
        //            Product newProduct = new Product(product.Name, product.Price);
        //            order.Products.Add(newProduct);
        //            _genreRepository.AddProductAsync(newProduct);
        //        }

        //    }
        //    order.Status = request.Status;
        //    order.Date = request.Date;
        //    return Task.FromResult(new OrderReply
        //    {
        //        OrderId = _songRepository.ChangeOrderAsync(Guid.Parse(request.OrderId), order).Result.ToString()
        //    });
        //}


        //public Task<SingerReply> AddSinger(Singer request)
        //    {
        //        return Task.Run(() =>
        //        {
        //            lock (_singers)
        //            {
        //                //проверка на непустую строку
        //                if (_singers.Values.Select(o => o.Name).Contains(request.Name))
        //                    return new SingerReply() { Success = false };

        //                var id = _singers.Keys.Count == 0 ? 1 : _genres.Keys.Max() + 1;
        //                var singer = new Singer() { Id = id, Name = request.Name };
        //                _singers.TryAdd(id, singer);
        //                return new SingerReply() { Success = true, Singer = singer };
        //            }
        //        });
        //    }


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

        //    public Task<SearchSongReply> RemoveSong(SearchRequest request)
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

