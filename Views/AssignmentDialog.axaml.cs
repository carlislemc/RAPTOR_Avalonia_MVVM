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

            this.IsTabStop = true;
            
            RAPTOR_Avalonia_MVVM.ViewModels.AssignmentDialogViewModel v2 = ((RAPTOR_Avalonia_MVVM.ViewModels.AssignmentDialogViewModel)DataContext);

            this.FindControl<TextBox>("typingVal").IsTabStop = false;
            this.FindControl<TextBox>("typingName").IsTabStop = false;
            this.FindControl<TreeView>("treeview").IsTabStop = false;
            this.FindControl<Button>("done").IsTabStop = false;

            this.FindControl<TextBox>("typingName").KeyDown += (s, e) => {
                RAPTOR_Avalonia_MVVM.ViewModels.AssignmentDialogViewModel v = ((RAPTOR_Avalonia_MVVM.ViewModels.AssignmentDialogViewModel)DataContext);
                if(e.Key == Avalonia.Input.Key.Tab){
                    if(v.setSuggestions.Count > 0){
                        string ans = v.setIndex;
                        fillSuggestion(s, ans);
                        this.FindControl<TextBox>("typingName").CaretIndex =  this.FindControl<TextBox>("typingName").Text.Length;
                    }
                }
                else if(e.Key == Avalonia.Input.Key.Down){
                    if(v.setSuggestions.Count > 0 && v.setIndex != v.setSuggestions[v.setSuggestions.Count-1]){
                        v.setIndex = v.setSuggestions[v.setSuggestions.IndexOf(v.setIndex)+1];
                    }
                }
                else if(e.Key == Avalonia.Input.Key.Up){
                    if(v.setSuggestions.Count > 0 && v.setIndex != v.setSuggestions[0]){
                        v.setIndex = v.setSuggestions[v.setSuggestions.IndexOf(v.setIndex)-1];
                    }
                }
            };

            this.FindControl<TextBox>("typingVal").KeyDown += (s, e) => {
                RAPTOR_Avalonia_MVVM.ViewModels.AssignmentDialogViewModel v = ((RAPTOR_Avalonia_MVVM.ViewModels.AssignmentDialogViewModel)DataContext);
                if(e.Key == Avalonia.Input.Key.Tab){
                    if(v.setSuggestions.Count > 0){
                        string ans = v.setIndex;
                        fillSuggestion(s, ans);
                        this.FindControl<TextBox>("typingVal").CaretIndex =  this.FindControl<TextBox>("typingVal").Text.Length;
                    }
                }
                else if(e.Key == Avalonia.Input.Key.Down){
                    if(v.setSuggestions.Count > 0 && v.setIndex != v.setSuggestions[v.setSuggestions.Count-1]){
                        v.setIndex = v.setSuggestions[v.setSuggestions.IndexOf(v.setIndex)+1];
                    }
                }
                else if(e.Key == Avalonia.Input.Key.Up){
                    if(v.setSuggestions.Count > 0 && v.setIndex != v.setSuggestions[0]){
                        v.setIndex = v.setSuggestions[v.setSuggestions.IndexOf(v.setIndex)-1];
                    }
                }
                
            };

            this.FindControl<TreeView>("treeview").DoubleTapped += (s, e) =>
            {
                string ans = ((string)((TreeView)s).SelectedItem);
                fillSuggestion(s, ans);
                
            };
        }

        private void fillSuggestion(Object s, string ans){
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
                    if(spot == -1){
                        return;
                    }
                    temp = temp.Substring(0, spot);
                    temp += ans;
                    v.toValue = temp;  
                }
                
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
