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
    public class LoopDialogViewModel : ViewModelBase
    {
        public LoopDialogViewModel() {
            this.text = "";
        }

        public LoopDialogViewModel(Loop l, Window w, bool modding) {
            this.text = "";
            this.l = l;
            this.w = w;
            this.modding = modding;

            if (modding)
            {
                loopCondition = l.Text;
            }
        }
        public Loop l;
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

        public string loopCondition = "";

        public string setLoop{
            get { return loopCondition; }
            set {this.RaiseAndSetIfChanged(ref loopCondition, value); Text = getSuggestion(); }
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

        public string getSuggestion()
        {
            Suggestions s = new Suggestions(l, setLoop, false, getSubchart());
            return s.getSuggestions();
        }

        public void OnDoneCommand(){
            Syntax_Result res = interpreter_pkg.conditional_syntax(setLoop);
            if(res.valid){
                Undo_Stack.Make_Undoable(getSubchart());
                l.text_str = setLoop;
                l.parse_tree = res.tree;
                Text += "Done Loop\n";
                MainWindowViewModel.GetMainWindowViewModel().modified = true;
                w.Close();
            } else {
                Text = res.message;
            }
        }

    }

}