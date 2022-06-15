using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using MessageBox.Avalonia.DTO;
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
    public class InputDialogViewModel : ViewModelBase
    {
        public InputDialogViewModel() {
            this.text = "";
        }
        public InputDialogViewModel(Parallelogram p, Window w, bool modding) {
            this.text = "";
            this.p = p;
            this.w = w;
            this.modding = modding;

            if (modding)
            {
                this.prompt = p.prompt;
                this.variable = p.Text;
            }
        }
        public Parallelogram p;
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

        public string prompt = "";

        public string getPrompt{
            get { return prompt; }
            set { this.RaiseAndSetIfChanged(ref prompt, value);
                variableName = false;
                setSuggestions = getSuggestion(); }
        }

        public string variable = "";

        public string getVariable{
            get { return variable; }
            set { this.RaiseAndSetIfChanged(ref variable, value);
                variableName = true;
                setSuggestions = getSuggestion();
            }
        }

        public ObservableCollection<string> suggestions = new ObservableCollection<string>();
        public ObservableCollection<string> setSuggestions
        {
            get { return suggestions; }
            set { this.RaiseAndSetIfChanged(ref suggestions, value); }
        }

        public Subchart getSubchart(){
            MainWindowViewModel mw = MainWindowViewModel.GetMainWindowViewModel();
            ObservableCollection<Variable> vars = mw.theVariables;
            ObservableCollection<Subchart> sc = mw.theTabs;
            if(sc.Count == 1){
                return sc[0];
            }
            else{
                int i = mw.activeTab;
                return sc[i];
            }
        }

        public bool variableName;
        public ObservableCollection<string> getSuggestion()
        {
            if (variableName)
            {
                Suggestions s = new Suggestions(p, getVariable, variableName, getSubchart());
                return s.getSuggestions();
            }
            else
            {
                Suggestions s = new Suggestions(p, getPrompt, variableName, getSubchart());
                return s.getSuggestions();
            }

        }


        public void OnDoneCommand(){
            Syntax_Result res = interpreter_pkg.input_syntax(getVariable);
            if(res.valid){
                Undo_Stack.Make_Undoable(getSubchart());
                p.prompt = getPrompt;
                p.text_str = getVariable;
                p.parse_tree = res.tree;
                Text += "Done Input\n";
                MainWindowViewModel.GetMainWindowViewModel().modified = true;
                w.Close();
            } else {
                Text = res.message + '\n';
            }
        }

    }

}