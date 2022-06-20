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
                    string temp = v.toValue;
                    int spot = -1;
                    for(int k = 0; k < ans.Length; k++)
                    {
                        string searchWord = ans.Substring(0, ans.Length-k);
                        if(searchWord.Length > temp.Length)
                        {
                            continue;
                        }
                        if(temp == null || searchWord == null || temp == "" || searchWord == "")
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
                        
                    }
                    temp = temp.Substring(0, spot);
                    temp += ans;
                    v.toValue = temp;
                }
            };

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
