using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;

using MusicCatalogAvaloniaClient.Views;

using ReactiveUI;

namespace MusicCatalogAvaloniaClient.ViewModels
{
    public class StartViewModel
    {
        public string ConnectionAddress { get; set; } = string.Empty;
        public ReactiveCommand<Unit, Unit> OkCommand { get; }
        public ReactiveCommand<Unit, Unit> CancelCommand { get; }

        public StartViewModel()
        {
            OkCommand = ReactiveCommand.CreateFromTask(Add);
            CancelCommand = ReactiveCommand.Create(Cancel);
        }

        private async Task Add()
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
                MainWindow mainWindow = new(ConnectionAddress);
                mainWindow.Show();
                foreach (Window window in App.Current.Resources.Values)
                {
                    if (window.GetType() == typeof(StartWindow))
                    {
                        window.Close();
                    }
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }
        private void Cancel()
        {
            //_mainWindow.Close();
        }
    }
}
