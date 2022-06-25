using MusicCatalogServer.Api;

namespace MusicCatalogServer.Repository
{
    public interface ISongRepository
    {
        IEnumerable<object> Values { get; set; }

        Task<int> Add(Song request);
        void Dispose();
        Task<List<Song>> GetAll();
        Task<bool> Remove(int id);

        ////Task<Guid> ChangeSongAsync(Guid id, Song newOrder);
        //Task<Guid> DeleteSongAsync(int id);
        //Task<List<Song>> GetAllSongsAsync();
        //Task<Song> GetSongAsync(Guid id);
    }
}
