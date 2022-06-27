using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace MusicCatalogAvaloniaClient.Models
{
    internal class SongInfo: ReactiveObject
    {
        public int ID { get; set; }
        public List<string> Singers { get; set; } = new();
        public List<string> Genres { get; set; } = new();
        public int DurationSecs { get; set; }

        public string FormatSingers()
        {
            string result = Singers[0];
            foreach (var s in Singers)
                result += ", " + s;
            return result;
        }

        public string FormatGenres()
        {
            string result = Singers[0];
            foreach (var s in Singers)
                result += ", " + s;
            return result;
        }
    }
}
