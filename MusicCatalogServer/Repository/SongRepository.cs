using System.Xml.Serialization;

using MusicCatalogServer.Api;

namespace MusicCatalogServer.Repository
{
    public class SongRepository : ISongRepository
    {
        private readonly SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1, 1);
        private readonly string _path;
        private List<Api.Song> _songs;
        //public IEnumerable<object> Values;

        public SongRepository(IConfiguration configuration)
        {
            _path = configuration.GetValue<string>("RepositoryPath");
            _songs = new List<Api.Song>();
            //Values = _songs;
        }

        public IEnumerable<object> Values { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public async Task<int> Add(Api.Song request)
        {
            if (request.Singers == null && request.Genres == null && request.Title == null)
            {
                throw new InvalidOperationException();
            }
            //await ReadSongsFileAsync();
            _songs.Add(request);
            //await WriteSongsFileAsync();
            return request.Id;
        }

        public async Task<List<Api.Song>> GetAll()
        {
            await ReadSongsFileAsync();
            return _songs;
        }

        public async Task<bool> Remove(int id)
        {
            await ReadSongsFileAsync();
            if (_songs.Remove(_songs.Find(f => f.Id == id)))
            {
                await WriteSongsFileAsync();
                return false;
            }
            throw new InvalidOperationException();
        }




        //public async Task<List<Song>> GetAllSongsAsync()
        //{
        //    await ReadSongsFileAsync();
        //    return _songs;
        //}


        //public async Task<Song> GetSongAsync(Guid id)
        //{
        //    await ReadSongsFileAsync();
        //    foreach (Song song in _songs)
        //    {
        //        if (song.Id.Equals(id))
        //        {
        //            return song;
        //        }
        //    }
        //    throw new InvalidOperationException();
        //}


        //public async Task<Guid> ChangeSongAsync(Guid id, Song newSong)
        //{
        //    if (newSong.Singer == null || newSong.Genres == null)
        //    {
        //        throw new InvalidOperationException();
        //    }
        //    await ReadSongsFileAsync();
        //    foreach (Song song in _songs)
        //    {
        //        if (song.Id == id)
        //        {
        //            song.Singer = newSong.Singer;
        //            song.Genres = newSong.Genres;
        //            await WriteSongsFileAsync();
        //            return id;
        //        }
        //    }
        //    throw new InvalidOperationException();
        //}

        //public async Task<bool> CheckSongAsync(Guid id)
        //{
        //    await ReadSongsFileAsync();
        //    if (_songs.Find(f => f.Id.Equals(id)) != null)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        private async Task ReadSongsFileAsync()
        {
            await SemaphoreSlim.WaitAsync();
            try
            {
                if (!File.Exists(_path))
                {
                    _songs = new List<Api.Song>();
                    return;
                }
                XmlSerializer formatter = new XmlSerializer(typeof(List<Api.Song>));
                await using FileStream fileStream = new FileStream(_path, FileMode.OpenOrCreate);
                _songs = (List<Api.Song>)formatter.Deserialize(fileStream);
            }
            finally
            {
                SemaphoreSlim.Release();
            }
        }

        private async Task WriteSongsFileAsync()
        {
            await SemaphoreSlim.WaitAsync();
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<Api.Song>));
                await using FileStream fileStream = new FileStream(_path, FileMode.Create);
                formatter.Serialize(fileStream, _songs);
            }
            finally
            {
                SemaphoreSlim.Release();
            }
        }

    }
}
