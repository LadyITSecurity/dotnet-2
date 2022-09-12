using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

using MusicCatalogAvaloniaClient.Views;

using ReactiveUI;

namespace MusicCatalogAvaloniaClient.ViewModels
{
    public class StartViewModel
    {
        public string ConnectionAddress { get; set; } = "http://localhost:5000";
        public Interaction<Unit, Unit> CloseWindow { get; } = new();
        public ReactiveCommand<Unit, Unit> OkCommand { get; }
        public ReactiveCommand<Unit, Unit> CancelCommand { get; }

        public StartViewModel()
        {
            OkCommand = ReactiveCommand.CreateFromTask(Ok);
            CancelCommand = ReactiveCommand.Create(Cancel);
        }

        private async Task Ok()
        {
            //var msgBox = new Window
            //{
            //    Content = new Label
            //    {
            //        Content = "The name string can`t be empty!",
            //        FontSize = 16,
            //        HorizontalAlignment = HorizontalAlignment.Center,
            //        VerticalAlignment = VerticalAlignment.Center
            //    },
            //    Width = 350,
            //    Height = 50,
            //    CanResize = false,
            //    WindowStartupLocation = WindowStartupLocation.CenterOwner,
            //    Title = "MessageBox"
            //};
            //Console.WriteLine("Connected to server!");
            //await msgBox.ShowDialog(_mainWindow);

            try
            {
                var vm = new MainViewModel(ConnectionAddress);
                var mainWindow = new MainWindow { DataContext = vm };
                mainWindow.Show();

                await CloseWindow.Handle(Unit.Default);

            }
            catch (Exception e)
            {
                Console.WriteLine("The name string can`t be empty!");
            }
        }
        private void Cancel()
        {
            //_mainWindow.Close();
        }
    }
}
