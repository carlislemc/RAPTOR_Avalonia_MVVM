using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using raptor;
using System.Collections.ObjectModel;
namespace RAPTOR_Avalonia_MVVM.Views
{
    public partial class SelectionDialog : Window
    {
        public SelectionDialog(){

        }
        public SelectionDialog(IF_Control i, bool modding)
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            DataContext = new RAPTOR_Avalonia_MVVM.ViewModels.SelectionDialogViewModel(i, this, modding);

            this.FindControl<TreeView>("treeview").DoubleTapped += (s, e) =>
            {
                string ans = ((string)((TreeView)s).SelectedItem);
                if(ans.Contains("(")){
                    ans = ans.Substring(0, ans.IndexOf("("));
                }
                RAPTOR_Avalonia_MVVM.ViewModels.SelectionDialogViewModel v = ((RAPTOR_Avalonia_MVVM.ViewModels.SelectionDialogViewModel)DataContext);


                string temp = v.selection;
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
                v.selection = temp;

            };

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
