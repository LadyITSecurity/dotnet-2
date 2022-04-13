using Grpc.Core;
using MusicCatalogServer.Api;
using System.Collections.Concurrent;

namespace MusicCatalogServer.Services
{
    public class MusicCatalogService : Api.MusicCatalog.MusicCatalogBase
    {

        private readonly MusicCatalog _catalog;

        public MusicCatalogService(MusicCatalog catalog)
        {
            _catalog = catalog;
        }

        public override Task<GenreReply> AddGenre(Genre request, ServerCallContext context)
        {
            return _catalog.AddGenre(request);
        }

        public override Task<Reply> RemoveGenre(Id request, ServerCallContext context)
        {
            return _catalog.RemoveGenre(request);
        }

        public override Task<SearchGenreReply> SearchGenre(SearchGenreRequest request, ServerCallContext context)
        {
            return _catalog.SearchGenre(request);
        }
    }
}