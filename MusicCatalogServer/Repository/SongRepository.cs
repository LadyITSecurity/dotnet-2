using System.Xml.Serialization;

using MusicCatalogServer.Api;

namespace MusicCatalogServer.Repository
{
    public class SongRepository : ISongRepository
    {
        private readonly SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1, 1);
        private readonly string _path;
        private List<Api.Song> _songs = new();
        //public IEnumerable<object> Values;

        public SongRepository(IConfiguration configuration)
        {
            _path = "RepositoryPath.xml";//configuration.GetValue<string>("RepositoryPath");
            _songs = new List<Api.Song>();
            //Values = _songs;
        }

        public IEnumerable<object> Values { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public async Task<List<Song>> GetAll()
        {
            await ReadSongsFileAsync();
            return _songs;
        }

        public async Task<int> Add(Api.Song request)
        {
            if (request.Singers == null && request.Genres == null && request.Title == null)
            {
                throw new InvalidOperationException();
            }
            await ReadSongsFileAsync();
            _songs.Add(request);
            await WriteSongsFileAsync();
            return request.Id;
        }

        public async Task<bool> Remove(int id)
        {
            await ReadSongsFileAsync();
            if (_songs == null)
                return false;

            var ind = _songs.Find(f => f.Id == id);
            if (ind != null && _songs.Remove(ind))
            {
                await WriteSongsFileAsync();
                return true;
            }
            return false;
        }




        //public async Task<List<Song>> GetAllSongsAsync()
        //{
        //    await ReadSongsFileAsync();
        //    return _songs;
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
                _songs = (List<Api.Song>)formatter.Deserialize(fileStream)!;
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

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
