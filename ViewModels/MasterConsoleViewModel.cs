using System.Text;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using MessageBox.Avalonia.DTO;
using Avalonia.Controls;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;

namespace RAPTOR_Avalonia_MVVM.ViewModels
{
    public class MasterConsoleViewModel : ReactiveObject
    {
        public static MasterConsoleViewModel? MC = null;

        private string text="Welcome to RAPTOR";
        public string Text   // property
        {
            get { return text; }   // get method
            set { this.RaiseAndSetIfChanged(ref text,value); }  // set method
        }
        public void clear_txt()
        {
            text = "";
        }
        public void OnClearPush()
        {
            Text += "cleared\n";
        }
    }
}
