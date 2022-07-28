using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using RAPTOR_Avalonia_MVVM.ViewModels;
using RAPTOR_Avalonia_MVVM.Views;

namespace RAPTOR_Avalonia_MVVM
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public static string[]? desktopArgs;

        public static string[]? getArgs()
        {
            return desktopArgs;
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktopArgs = desktop.Args;
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
                

            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
