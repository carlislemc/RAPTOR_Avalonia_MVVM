using Avalonia.Controls.ApplicationLifetimes;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RAPTOR_Avalonia_MVVM.ViewModels;
using raptor;

namespace RAPTOR_Avalonia_MVVM
{
    class MessageBoxButtons
    {
        public const int OK = 0;
        public const int YesNoCancel = 1;
    }
    class MessageBoxIcon
    {
        public const int Warning = 0;
        public const int Error = 1;
    }
    class MessageBoxClass
    {
        public static void Show(string text) {
            if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {

                    var msBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                        .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                        {
                            ButtonDefinitions = ButtonEnum.Ok,
                            ContentTitle = "Title",
                            ContentMessage = text,
                            Icon = Icon.Plus,
                            Style = Style.Windows
                        });

                    msBoxStandardWindow.ShowDialog(desktop.MainWindow);
            }
        }

        
        internal async static Task Show(string v1, string v2, int oK, int warning)
        {

            if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                Icon icon;
                if(warning == 0)
                {
                    icon = Icon.Warning;
                }
                else
                {
                    icon = Icon.Error;
                }

                ButtonEnum b;
                if(oK == 0)
                {
                    b = ButtonEnum.Ok;
                }
                else
                {
                    b = ButtonEnum.YesNoCancel;
                }

                var msBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                    {
                        ButtonDefinitions = b,
                        ContentTitle = v2,
                        ContentMessage = v1,
                        Icon = icon, 
                        Style = Style.Windows
                    });

             

               MainWindowViewModel.GetMainWindowViewModel().buttonAnswer =  await msBoxStandardWindow.ShowDialog(desktop.MainWindow);
               return;
            }

            MainWindowViewModel.GetMainWindowViewModel().buttonAnswer = ButtonResult.Cancel;
            //throw new NotImplementedException();
        }
    }
}
