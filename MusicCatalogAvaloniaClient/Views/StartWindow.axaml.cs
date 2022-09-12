using System;
using System.Reactive;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

using MusicCatalogAvaloniaClient.ViewModels;

using ReactiveUI;

namespace MusicCatalogAvaloniaClient.Views
{
    public partial class StartWindow : ReactiveWindow<StartViewModel>
    {
        public StartWindow()
        {
            AvaloniaXamlLoader.Load(this);
#if DEBUG
            this.AttachDevTools();
#endif

            ViewModelProperty.Changed.Subscribe((AvaloniaPropertyChangedEventArgs<StartViewModel?> args) =>
            {
                if (args.NewValue.Value is not { } viewModel)
                    return;

                viewModel.CloseWindow.RegisterHandler(Close);
            });

        }

        private void Close(InteractionContext<Unit, Unit> ctx)
        {
            Close();
            ctx.SetOutput(Unit.Default);
        }
    }
}
