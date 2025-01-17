using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MusicCatalogAvaloniaClient.ViewModels;
using MusicCatalogAvaloniaClient.Views;

namespace MusicCatalogAvaloniaClient
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new StartWindow
                {
                    DataContext = new StartViewModel(),  
                };
            }
            base.OnFrameworkInitializationCompleted();
        }
    }
}
