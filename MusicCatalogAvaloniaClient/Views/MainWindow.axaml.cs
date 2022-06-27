using Avalonia.Controls;

using MusicCatalogAvaloniaClient.ViewModels;

namespace MusicCatalogAvaloniaClient.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(string address)
        {
            InitializeComponent();
            DataContext = new MainViewModel(address);
        }
    }
}
