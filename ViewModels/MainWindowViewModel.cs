using System;
using System.Collections.Generic;
using System.Text;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using MessageBox.Avalonia.DTO;
using Avalonia.Controls;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

namespace RAPTOR_Avalonia_MVVM.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Avalonia!";
        private Window GetWindow()
        {
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
            {
                return desktopLifetime.MainWindow;
            }
            return null;
        }


        public async void OnOpenCommand()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filters.Add(new FileDialogFilter() { Name = "RAPTOR", Extensions = { "rap" } });
            dialog.Filters.Add(new FileDialogFilter() { Name = "All Files", Extensions = { "*" } });
            dialog.AllowMultiple = false;
            if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                string[] result = await dialog.ShowAsync(desktop.MainWindow);
                if (result != null)
                {
                    var msBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                        .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                        {
                            ButtonDefinitions = ButtonEnum.Ok,
                            ContentTitle = "Title",
                            ContentMessage = result[0],
                            Icon = Icon.Plus,
                            Style = Style.Windows
                        });

                    ButtonResult br= await msBoxStandardWindow.ShowDialog(desktop.MainWindow);
                }
            }

        }
        public void OnNewCommand()
        {
            var msBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                {
                    ButtonDefinitions = ButtonEnum.OkAbort,
                    ContentTitle = "Title",
                    ContentMessage = "Message",
                    Icon = Icon.Plus,
                    Style = Style.Windows
                });
            msBoxStandardWindow.Show();
        }
    }
}
