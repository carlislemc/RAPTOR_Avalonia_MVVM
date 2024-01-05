using Avalonia.Controls.ApplicationLifetimes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RAPTOR_Avalonia_MVVM.ViewModels;
using raptor;
using MsBox.Avalonia;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;

namespace RAPTOR_Avalonia_MVVM
{
    public class MessageBoxButtons
    {
        public const int OK = 0;
        public const int YesNoCancel = 1;
    }
    public class MessageBoxIcon
    {
        public const int Warning = 0;
        public const int Error = 1;
    }
    public class MessageBoxClass
    {
        public static void Show(string text) {
            if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {

                    var msBoxStandardWindow = MessageBoxManager
                        .GetMessageBoxStandard(new MessageBoxStandardParams
                        {
                            ButtonDefinitions = ButtonEnum.Ok,
                            ContentTitle = "Title",
                            ContentMessage = text,
                            Icon = Icon.Plus//,
                            //Style = Style.Windows
                        });

                    msBoxStandardWindow.ShowWindowDialogAsync(desktop.MainWindow);
            }
        }

        
        public async static Task Show(string v1, string v2, int oK, int warning)
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

                var msBoxStandardWindow = MessageBoxManager
                    .GetMessageBoxStandard(new MessageBoxStandardParams
                    {
                        ButtonDefinitions = b,
                        ContentTitle = v2,
                        ContentMessage = v1,
                        Icon = icon//, 
                        //Style = Style.Windows
                    });

             

               MainWindowViewModel.GetMainWindowViewModel().buttonAnswer =  await msBoxStandardWindow.ShowWindowDialogAsync(desktop.MainWindow);
               return;
            }

            MainWindowViewModel.GetMainWindowViewModel().buttonAnswer = ButtonResult.Cancel;
            //throw new NotImplementedException();
        }
    }
}
