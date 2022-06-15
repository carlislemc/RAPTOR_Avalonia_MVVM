using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using raptor;

namespace RAPTOR_Avalonia_MVVM.Views
{
    public partial class AssignmentDialog : Window
    {
        public AssignmentDialog() 
        {
            
        }
        public AssignmentDialog(Rectangle r, bool modding)
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            DataContext = new RAPTOR_Avalonia_MVVM.ViewModels.AssignmentDialogViewModel(r, this, modding);

            this.FindControl<TreeView>("treeview").DoubleTapped += (s, e) =>
            {
                string ans = ((string)((TreeView)s).SelectedItem);
                if(ans.Contains("(")){
                    ans = ans.Substring(0, ans.IndexOf("("));
                }
                RAPTOR_Avalonia_MVVM.ViewModels.AssignmentDialogViewModel v = ((RAPTOR_Avalonia_MVVM.ViewModels.AssignmentDialogViewModel)DataContext);
                if(v.editingName){
                    string currentValue = v.setValue;
                    for(int i = 0; i < ans.Length; i++){
                        
                    }
                    v.setValue = ans;
                } else{
                     v.toValue = ans;
                }
            };

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
