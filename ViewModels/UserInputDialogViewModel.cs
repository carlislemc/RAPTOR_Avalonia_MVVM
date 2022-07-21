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
    public class UserInputDialogViewModel : ViewModelBase
    {
        public UserInputDialogViewModel() {
            this.text = "";
        }
        public UserInputDialogViewModel(Parallelogram p, numbers.value v, Window w, bool compileMode = false) {
            try
            {
                this.text = numbers.Numbers.msstring_view_image(v);
            }
            catch
            {
                this.text = p.prompt;
            }
            
            this.p = p;
            this.w = w;
            this.compileMode = compileMode;
        }

        public Parallelogram p;
        public Window w;
        public bool compileMode;

        public bool modified = false;
        public bool runningState = false;


        public string text = "";
        public string Text   // property
        {
            get { return text; }   // get method
            set { this.RaiseAndSetIfChanged(ref text,value); }  // set method
        }

        public string variable = "";

        public string getVariable{
            get { return variable; }
            set { this.RaiseAndSetIfChanged(ref variable, value); }
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


        public void OnDoneCommand(){
            p.result = interpreter_pkg.assignment_syntax(p.text_str, getVariable);
            p.assign = p.text_str + ":=" + getVariable;

            if (compileMode)
            {
                p.pans = numbers.Numbers.make_value__6(getVariable);
            }

            w.Close();
        }

    }

}