using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using raptor;
using System;
using System.Collections.ObjectModel;

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
                    v.setValue = ans;
                }
                else
                {

                    ObservableCollection<string> parts = Suggestions.parseInput(v.toValue);
                    parts[parts.Count - 1] = ans;

                    string fullText = "";
                    foreach (string st in parts)
                    {
                        fullText += st;
                    }
                    v.toValue = fullText;
                }
            };

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
