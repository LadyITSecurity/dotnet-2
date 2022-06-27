using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MusicCatalogServer.Api;

namespace MusicCatalogAvaloniaClient.ViewModels
{
    public class SongViewModel
    {

        public Song ThisSong { get; set; } = new();
        public int ID { get; set; }
        public List<string> Singers { get; set; } = new();
        public List<string> Genres { get; set; } = new();
        public int DurationSecs { get; set; }


    }
}
