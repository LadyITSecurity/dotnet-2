using Grpc.Core;

using MusicCatalogServer.Api;

namespace MusicCatalogServer.Services
{
    public class MusicCatalogService : Api.MusicCatalog.MusicCatalogBase
    {

        private readonly MusicCatalog _catalog;

        public MusicCatalogService(MusicCatalog catalog)
        {
            _catalog = catalog;
        }

        public override Task<Reply> AddSong(Song request, ServerCallContext context)
        {
            return _catalog.AddSong(request);
        }

        public override Task<Reply> DeleteSong(DeleteSongRequest request, ServerCallContext context)
        {
            return _catalog.DeleteSong(request);
        }

        public override Task<SongList> SearchSong(Api.Song request, ServerCallContext context)
        {
            return _catalog.SearchSong(request);
        }

        public override Task<SongList> GetAll(NullRequest request, ServerCallContext context)
        {
            return _catalog.GetAll();
        }
    }
}
