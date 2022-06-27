using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Text;
using System.Threading.Channels;

using Grpc.Net.Client;

using MusicCatalogServer.Api;

using ReactiveUI;

namespace MusicCatalogAvaloniaClient.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        public ObservableCollection<SongViewModel> Songs = new ObservableCollection<SongViewModel>();

        private MusicCatalog.MusicCatalogClient _client;
        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<Unit, Unit> DeteleCommand { get; }
        public ReactiveCommand<Unit, Unit> SearchCommand { get; }


        public MainViewModel(string address)
        {            
            _client = new(GrpcChannel.ForAddress(address));
            //using var channel = GrpcChannel.ForAddress("http://localhost:5000");
            // _client = new MusicCatalog.MusicCatalogClient(channel);


            AddCommand = ReactiveCommand.Create(AddTask);
            DeteleCommand = ReactiveCommand.Create(DeleteTask);
            SearchCommand = ReactiveCommand.Create(SearchTask);

            UpdateDatabase();
        }

        private void UpdateDatabase()
        {
           
        }

        private void SearchTask()
        {
            throw new NotImplementedException();
        }

        private void DeleteTask()
        {
            throw new NotImplementedException();
        }

        private void AddTask()
        {
            throw new NotImplementedException();
        }
        //                using var channel = GrpcChannel.ForAddress("http://localhost:5000");
        //var client = new MusicCatalog.MusicCatalogClient(channel);
    }
}
