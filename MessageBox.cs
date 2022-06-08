using Avalonia.Controls.ApplicationLifetimes;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAPTOR_Avalonia_MVVM
{
    class MessageBoxButtons
    {
        public const int OK = 0;
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

        
        internal static void Show(string v1, string v2, int oK, int warning)
        {


            throw new NotImplementedException();
        }
    }
}
