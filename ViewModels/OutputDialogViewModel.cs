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
    public class OutputDialogViewModel : ViewModelBase
    {
        public OutputDialogViewModel() {
            this.text = "";
        }
        public OutputDialogViewModel(Parallelogram p, Window w, bool modding) {
            this.text = "";
            this.p = p;
            this.w = w;
            this.modding = modding;

            if (modding)
            {
                this.output = p.Text;
                this.checkBox = p.new_line;
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

        public string output = "";
        public string getOutput{
            get { return output; }
            set { this.RaiseAndSetIfChanged(ref output, value); Text = getSuggestion(); }
        }

        public bool checkBox = true;

        public bool checkedBox{
            get{ return checkBox; }
            set{ checkBox = value;
                 p.new_line = checkBox; }
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
            Suggestions s = new Suggestions(p, getOutput, false, getSubchart());
            return s.getSuggestions();
        }

        public void OnDoneCommand(){
            Syntax_Result res = interpreter_pkg.output_syntax(getOutput, checkedBox);
            if(res.valid){
                Undo_Stack.Make_Undoable(getSubchart());
                p.text_str = getOutput;
                p.parse_tree = res.tree;
                Text += "Done Output\n";
                MainWindowViewModel.GetMainWindowViewModel().modified = true;
                w.Close();
            } else{
                Text = res.message;
            }
        }

    }

}