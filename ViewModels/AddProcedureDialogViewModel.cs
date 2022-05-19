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
    public class AddProcedureDialogViewModel : ViewModelBase
    {
        public AddProcedureDialogViewModel() {
            this.text = "";
        }
        public AddProcedureDialogViewModel(Window w) {
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

        public string procedureName = "";
        public string setProcedureName { 
            get { return procedureName; } 
            set { this.RaiseAndSetIfChanged(ref procedureName, value); } 
        }

        public string param1 = "";
        public string setParam1 {
            get { return param1; }
            set { this.RaiseAndSetIfChanged(ref param1, value); }
        }

        public string param2 = "";
        public string setParam2 {
            get { return param2; }
            set { this.RaiseAndSetIfChanged(ref param2, value); }
        }

        public string param3 = "";
        public string setParam3 {
            get { return param3; }
            set { this.RaiseAndSetIfChanged(ref param3, value); }
        }

        public string param4 = "";
        public string setParam4 {
            get { return param4; }
            set { this.RaiseAndSetIfChanged(ref param4, value); }
        }

        public string param5 = "";
        public string setParam5 {
            get { return param5; }
            set { this.RaiseAndSetIfChanged(ref param5, value); }
        }

        public string param6 = "";
        public string setParam6 {
            get { return param6; }
            set { this.RaiseAndSetIfChanged(ref param6, value); }
        }

        public void OnDoneCommand(){
            //Syntax_Result res = interpreter_pkg.assignment_syntax(setValue, toValue);

            ObservableCollection<Subchart> tbs = MainWindowViewModel.GetMainWindowViewModel().theTabs;

            Subchart addMe = new Subchart(setProcedureName);
            addMe.kind=Subchart_Kinds.Procedure;
            tbs.Add(addMe);
            w.Close();
        }

    }

}