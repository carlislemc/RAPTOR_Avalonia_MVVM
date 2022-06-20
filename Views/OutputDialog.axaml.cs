using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using raptor;
using System.Collections.ObjectModel;
namespace RAPTOR_Avalonia_MVVM.Views
{
    public partial class OutputDialog : Window
    {
        public OutputDialog(){
            
        }
        public OutputDialog(Parallelogram p, bool modding)
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            DataContext = new RAPTOR_Avalonia_MVVM.ViewModels.OutputDialogViewModel(p, this, modding);

            this.FindControl<TreeView>("treeview").DoubleTapped += (s, e) =>
            {
                string ans = ((string)((TreeView)s).SelectedItem);
                if(ans.Contains("(")){
                    ans = ans.Substring(0, ans.IndexOf("("));
                }
                RAPTOR_Avalonia_MVVM.ViewModels.OutputDialogViewModel v = ((RAPTOR_Avalonia_MVVM.ViewModels.OutputDialogViewModel)DataContext);

                string temp = v.getOutput;
                int spot = -1;
                for (int k = 0; k < ans.Length; k++)
                {
                    string searchWord = ans.Substring(0, ans.Length - k);
                    if (searchWord.Length > temp.Length)
                    {
                        continue;
                    }
                    if (temp == null || searchWord == null || temp == "" || searchWord == "")
                    {
                        continue;
                    }
                    int first = temp.Length - searchWord.Length;
                    int second = temp.Length;
                    string tryThis = temp.Substring(first).Trim();
                    if (tryThis.Equals(searchWord))
                    {
                        spot = first;
                        break;
                    }

                    temp = temp.Substring(0, spot);
                temp += ans;
                v.getOutput = temp;

            };

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
