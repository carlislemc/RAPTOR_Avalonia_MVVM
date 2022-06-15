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
                
                ObservableCollection<string> parts = Suggestions.parseInput(v.getOutput);
                parts[parts.Count - 1] = ans;

                string fullText = "";
                foreach (string st in parts)
                {
                    fullText += st;
                }
                v.getOutput = fullText;
                
            };

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
