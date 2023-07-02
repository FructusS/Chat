using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaChat.Desktop.ViewModels;
using AvaloniaChat.Desktop.Views;
using Prism.Events;

namespace AvaloniaChat.Desktop
{
    public partial class App : Avalonia.Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(new EventAggregator()),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
