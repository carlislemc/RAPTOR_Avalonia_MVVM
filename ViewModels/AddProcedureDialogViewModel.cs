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

        public bool in1 = true;
        public bool setIn1{
            get{ return in1; }
            set{ this.RaiseAndSetIfChanged(ref in1, !in1); }
        }
        public bool in2 = true;
        public bool setIn2{
            get{ return in2; }
            set{ this.RaiseAndSetIfChanged(ref in2, !in2); }
        }
        public bool in3 = true;
        public bool setIn3{
            get{ return in3; }
            set{ this.RaiseAndSetIfChanged(ref in3, !in3); }
        }
        public bool in4 = true;
        public bool setIn4{
            get{ return in4; }
            set{ this.RaiseAndSetIfChanged(ref in4, !in4); }
        }
        public bool in5 = true;
        public bool setIn5{
            get{ return in5; }
            set{ this.RaiseAndSetIfChanged(ref in5, !in5); }
        }
        public bool in6 = true;
        public bool setIn6{
            get{ return in6; }
            set{ this.RaiseAndSetIfChanged(ref in6, !in6); }
        }

        public bool out1 = false;
        public bool setOut1{
            get{ return out1; }
            set{ this.RaiseAndSetIfChanged(ref out1, !out1); }
        }
        public bool out2 = false;
        public bool setOut2{
            get{ return out2; }
            set{ this.RaiseAndSetIfChanged(ref out2, !out2); }
        }
        public bool out3 = false;
        public bool setOut3{
            get{ return out3; }
            set{ this.RaiseAndSetIfChanged(ref out3, !out3); }
        }
        public bool out4 = false;
        public bool setOut4{
            get{ return out4; }
            set{ this.RaiseAndSetIfChanged(ref out4, !out4); }
        }
        public bool out5 = false;
        public bool setOut5{
            get{ return out5; }
            set{ this.RaiseAndSetIfChanged(ref out5, !out5); }
        }
        public bool out6 = false;
        public bool setOut6{
            get{ return out6; }
            set{ this.RaiseAndSetIfChanged(ref out6, !out6); }
        }

        private string[] getParams(){
            switch(paramCount()){
                case 0:
                    return new string[]{};
                case 1:
                    return new string[]{param1};
                case 2:
                    return new string[]{param1, param2};
                case 3:
                    return new string[]{param1, param2, param3};
                case 4:
                    return new string[]{param1, param2, param3, param4};
                case 5:
                    return new string[]{param1, param2, param3, param4, param5};
                case 6:
                    return new string[]{param1, param2, param3, param4, param5, param6};

            }
            return new string[]{};
        }

        private bool[] getIns(){
            switch(paramCount()){
                case 0:
                    return new bool[]{};
                case 1:
                    return new bool[]{in1};
                case 2:
                    return new bool[]{in1, in2};
                case 3:
                    return new bool[]{in1, in2, in3};
                case 4:
                    return new bool[]{in1, in2, in3, in4};
                case 5:
                    return new bool[]{in1, in2, in3, in4, in5};
                case 6:
                    return new bool[]{in1, in2, in3, in4, in5, in6};

            }
            return new bool[]{};
        }

        private bool[] getOuts(){
            switch(paramCount()){
                case 0:
                    return new bool[]{};
                case 1:
                    return new bool[]{out1};
                case 2:
                    return new bool[]{out1, out2};
                case 3:
                    return new bool[]{out1, out2, out3};
                case 4:
                    return new bool[]{out1, out2, out3, out4};
                case 5:
                    return new bool[]{out1, out2, out3, out4, out5};
                case 6:
                    return new bool[]{out1, out2, out3, out4, out5, out6};

            }
            return new bool[]{};
        }

        private int paramCount(){
            int count = 0;
            if(param1 != ""){
                count++;
            }
            if(param2 != ""){
                count++;
            }
            if(param3 != ""){
                count++;
            }
            if(param4 != ""){
                count++;
            }
            if(param5 != ""){
                count++;
            }
            if(param6 != ""){
                count++;
            }
            return count;
        }
        public void OnDoneCommand(){
            //Syntax_Result res = interpreter_pkg.assignment_syntax(setValue, toValue);

            ObservableCollection<Subchart> tbs = MainWindowViewModel.GetMainWindowViewModel().theTabs;

            Subchart addMe = new Procedure_Chart(setProcedureName, getParams(), getIns(), getOuts());
            addMe.kind=Subchart_Kinds.Procedure;
            addMe.Num_Params = paramCount();
            tbs.Add(addMe);
            Undo_Stack.Make_Add_Tab_Undoable(tbs[tbs.Count-1]);
            w.Close();
        }

    }

}