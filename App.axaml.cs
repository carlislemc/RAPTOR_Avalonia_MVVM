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

                this.UrlsOpened += (s, e) =>
                {
                    MainWindowViewModel mw = MainWindowViewModel.GetMainWindowViewModel();
                    desktopArgs = e.Urls;
                    for (int i = 0; i < desktopArgs.Length; i++)
                    {
                        string str = desktopArgs[i];
                        if (str.Contains("file://"))
                        {
                            str = str.Substring(str.IndexOf("file://")+7).Trim();
                            mw.Load_File(str);
                        }
                        
                    }
                };
                
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
                

            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
