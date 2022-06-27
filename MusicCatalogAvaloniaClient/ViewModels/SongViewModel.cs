using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

using MusicCatalogServer.Api;

using ReactiveUI;

namespace MusicCatalogAvaloniaClient.ViewModels
{
    public class SongViewModel : ReactiveObject
    {
        public MainViewModel _MainViewModel { get; set; }
        public Song SongProto { get; set; } = new();
        public int ID { get; set; }
        public string Title { get; set; }
        public List<string> Singers { get; set; } = new();
        public List<string> Genres { get; set; } = new();
        public int DurationSecs { get; set; }
        public ReactiveCommand<Unit, Unit> OkCommand { get; }
        public ReactiveCommand<Unit, Unit> CancelCommand { get; }

        public SongViewModel(MainViewModel mainViewModel, Song songProto)
        {
            _MainViewModel = mainViewModel;
            SongProto = songProto;
            ID = songProto.Id;
            Title = songProto.Title;
            DurationSecs = songProto.DurationSecs;
            foreach (var singer in songProto.Singers)
                Singers.Add(singer);
            foreach (var genre in songProto.Genres)
                Genres.Add(genre);
        }
        public SongViewModel(MainViewModel mainViewModel)
        {
            OkCommand = ReactiveCommand.Create(Add);
            CancelCommand = ReactiveCommand.Create(Cancel);
        }



        public void Add()
        {
            if (Title == string.Empty)
            {
                //
            }

        }

        private void Cancel()
        {
            throw new NotImplementedException();
        }

    }
}
