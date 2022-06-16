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
                int spot = temp.LastIndexOf(ans[0]);
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
