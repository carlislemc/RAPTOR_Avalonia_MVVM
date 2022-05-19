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
    public class AddSubchartDialogViewModel : ViewModelBase
    {
        public AddSubchartDialogViewModel() {
            this.text = "";
        }
        public AddSubchartDialogViewModel(Window w) {
            this.text = "";
            this.w = w;
        }
        public Window w;
        public bool modified = false;
        public bool runningState = false;

        public string text = "";
        public string Text   // property
        {
            get { return text; }   // get method
            set { this.RaiseAndSetIfChanged(ref text,value); }  // set method
        }

        public string subchartName = "";
        public string setSubchartName { 
            get { return subchartName; } 
            set { this.RaiseAndSetIfChanged(ref subchartName, value); } 
        }


        public void OnDoneCommand(){
            //Syntax_Result res = interpreter_pkg.assignment_syntax(setValue, toValue);
            ObservableCollection<Subchart> tbs = MainWindowViewModel.GetMainWindowViewModel().theTabs;
            tbs.Add(new Subchart(setSubchartName));
            w.Close();
        }

    }

}