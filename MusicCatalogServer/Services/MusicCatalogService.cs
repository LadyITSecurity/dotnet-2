using Grpc.Core;

using MusicCatalogServer.Api;

namespace MusicCatalogServer.Services
{
    public class MusicCatalogService : Api.MusicCatalog.MusicCatalogBase
    {

        //private readonly MusicCatalog _catalog;

        //public MusicCatalogService(MusicCatalog catalog)
        //{
        //    _catalog = catalog;
        //}

        public override Task<Reply> AddSong(Song request, ServerCallContext context)
        {
                return base.AddSong(request, context);
        }

        public override Task<Reply> DeleteSong(DeleteSongRequest request, ServerCallContext context)
        {
            return base.DeleteSong(request, context);
        }

        public override Task<SongList> SearchSong(Song request, ServerCallContext context)
        {
            return base.SearchSong(request, context);
        }
    }
}
