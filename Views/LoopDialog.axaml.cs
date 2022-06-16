using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using raptor;
using System.Collections.ObjectModel;
namespace RAPTOR_Avalonia_MVVM.Views
{
    public partial class LoopDialog : Window
    {
        public LoopDialog(){

        }
        public LoopDialog(Loop l, bool modding)
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            DataContext = new RAPTOR_Avalonia_MVVM.ViewModels.LoopDialogViewModel(l, this, modding);

            this.FindControl<TreeView>("treeview").DoubleTapped += (s, e) =>
            {
                string ans = ((string)((TreeView)s).SelectedItem);
                if(ans.Contains("(")){
                    ans = ans.Substring(0, ans.IndexOf("("));
                }
                RAPTOR_Avalonia_MVVM.ViewModels.LoopDialogViewModel v = ((RAPTOR_Avalonia_MVVM.ViewModels.LoopDialogViewModel)DataContext);


                string temp = v.setLoop;
                int spot = -1;
                for (int k = 0; k < ans.Length; k++)
                {
                    string searchWord = ans.Substring(0, ans.Length - k);
                    spot = temp.LastIndexOf(searchWord);
                    if (spot != -1)
                    {
                        break;
                    }
                }
                temp = temp.Substring(0, spot);
                temp += ans;
                v.setLoop = temp;

            };

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
