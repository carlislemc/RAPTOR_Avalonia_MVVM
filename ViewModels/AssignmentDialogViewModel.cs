using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using Avalonia.Controls;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using raptor;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Avalonia.Input;
using ReactiveUI;
using System.Reactive;
using interpreter;


namespace RAPTOR_Avalonia_MVVM.ViewModels
{
    public class AssignmentDialogViewModel : ViewModelBase
    {
        public AssignmentDialogViewModel() {
            this.text = "";
        }
        public AssignmentDialogViewModel(Rectangle r, Window w, bool modding) {
            this.text = "";
            this.r = r;
            this.w = w;
            this.modding = modding;

            if (modding)
            {
                string[] temp = r.text_str.Split(":=");
                variableName = temp[0];
                variableValue = temp[1];
            }
        }
        public Rectangle r;
        public Window w;
        public bool modding;
        public bool modified = false;
        public bool runningState = false;

        public string text = "";
        public string Text   // property
        {
            get { return text; }   // get method
            set { this.RaiseAndSetIfChanged(ref text,value); }  // set method
        }

        public string variableName = "";
        public string setValue { 
            get { return variableName; } 
            set { this.RaiseAndSetIfChanged(ref variableName, value);
                    editingName = true;
                    setSuggestions = getSuggestions();
                    if(setSuggestions.Count > 0){
                        setIndex = setSuggestions[0];
                    }
                } 
        }

        public string variableValue = "";
        public string toValue {
            get { return variableValue; }
            set { this.RaiseAndSetIfChanged(ref variableValue, value);
                    editingName = false;
                    setSuggestions = getSuggestions();
                    if(setSuggestions.Count > 0){
                        setIndex = setSuggestions[0];
                    }
                }
        }

        public ObservableCollection<string> suggestions = new ObservableCollection<string>();
        public ObservableCollection<string> setSuggestions
        {
            get { return suggestions; }
            set { this.RaiseAndSetIfChanged(ref suggestions, value); }
        }

        public string suggestionIndex = "";
        public string setIndex
        {
            get { return suggestionIndex; }
            set { this.RaiseAndSetIfChanged(ref suggestionIndex, value); }
        }

        public Subchart getSubchart(){
            MainWindowViewModel mw = MainWindowViewModel.GetMainWindowViewModel();
            ObservableCollection<Variable> vars = mw.theVariables;
            ObservableCollection<Subchart> sc = mw.theTabs;
            if(sc.Count == 1){
                return sc[0];
            }
            else{
                int i = mw.viewTab;
                return sc[i];
            }
        }

        public bool editingName;

        public ObservableCollection<string> getSuggestions()
        {
            if (editingName)
            {
                Suggestions s = new Suggestions(r, setValue, editingName, getSubchart());
                return s.getSuggestions();
            }
            else
            {
                Suggestions s = new Suggestions(r, toValue, editingName, getSubchart());
                return s.getSuggestions();
            }
            

        }

        public void OnDoneCommand(){
            Syntax_Result res = interpreter_pkg.assignment_syntax(setValue, toValue);
            if(res.valid){
                Undo_Stack.Make_Undoable(getSubchart());
                r.text_str = setValue + ":=" + toValue;
                r.parse_tree = res.tree;
                MainWindowViewModel.GetMainWindowViewModel().modified = true;
                w.Close();
            } else {
                Text = res.message;
            }
            
        }

    }

}