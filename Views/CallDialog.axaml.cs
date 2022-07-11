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

            this.FindControl<TextBox>("proc").IsTabStop = false;
            this.FindControl<TreeView>("treeview").IsTabStop = false;
            this.FindControl<Button>("done").IsTabStop = false;

            this.FindControl<TextBox>("proc").KeyDown += (s, e) => {
                RAPTOR_Avalonia_MVVM.ViewModels.CallDialogViewModel v = ((RAPTOR_Avalonia_MVVM.ViewModels.CallDialogViewModel)DataContext);
                if(e.Key == Avalonia.Input.Key.Tab){
                    if(v.setSuggestions.Count == 0)
                    {
                        this.FindControl<TextBox>("proc").IsTabStop = true;
                        this.FindControl<TreeView>("treeview").IsTabStop = true;
                        this.FindControl<Button>("done").IsTabStop = true;
                    }
                    if(v.setSuggestions.Count > 0){
                        this.FindControl<TextBox>("proc").IsTabStop = false;
                        this.FindControl<TreeView>("treeview").IsTabStop = false;
                        this.FindControl<Button>("done").IsTabStop = false;
                        string ans = v.setIndex;
                        fillSuggestion(ans);
                        this.FindControl<TextBox>("proc").CaretIndex =  this.FindControl<TextBox>("proc").Text.Length;
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
                fillSuggestion(ans);    
            };

        }

        private void fillSuggestion(string ans){
            if(ans.Contains("(")){
                    ans = ans.Substring(0, ans.IndexOf("("));
                }
                RAPTOR_Avalonia_MVVM.ViewModels.CallDialogViewModel v = ((RAPTOR_Avalonia_MVVM.ViewModels.CallDialogViewModel)DataContext);


                string temp = v.setProcedure;
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
                }
                if(spot < 0)
                {
                    return;
                }

                temp = temp.Substring(0, spot);
                temp += ans;
                v.setProcedure = temp;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
