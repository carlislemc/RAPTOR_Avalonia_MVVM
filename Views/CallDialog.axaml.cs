using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using raptor;
using System.Collections.ObjectModel;
namespace RAPTOR_Avalonia_MVVM.Views
{
    public partial class CallDialog : Window
    {
        public CallDialog(){

        }
        public CallDialog(Rectangle r, bool modding)
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            DataContext = new RAPTOR_Avalonia_MVVM.ViewModels.CallDialogViewModel(r, this, modding);

            this.FindControl<TreeView>("treeview").DoubleTapped += (s, e) =>
            {
                string ans = ((string)((TreeView)s).SelectedItem);
                if(ans.Contains("(")){
                    ans = ans.Substring(0, ans.IndexOf("("));
                }
                RAPTOR_Avalonia_MVVM.ViewModels.CallDialogViewModel v = ((RAPTOR_Avalonia_MVVM.ViewModels.CallDialogViewModel)DataContext);
                

                ObservableCollection<string> parts = Suggestions.parseInput(v.setProcedure);
                parts[parts.Count - 1] = ans;

                string fullText = "";
                foreach (string st in parts)
                {
                    fullText += st;
                }
                v.setProcedure = fullText;
            };

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
