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

namespace RAPTOR_Avalonia_MVVM.ViewModels
{
    public class AssignmentDialogViewModel : ViewModelBase
    {
        public AssignmentDialogViewModel() {
            this.text = "";
        }
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
            set { this.RaiseAndSetIfChanged(ref variableName, value); } 
        }

        public string variableValue = "";
        public string toValue {
            get { return variableValue; }
            set { this.RaiseAndSetIfChanged(ref variableValue, value); }
        }

        public void OnDoneCommand(){
            if(setValue == "" || toValue == ""){
                Text += "Error!\n";
            } else {
                Text += setValue + "<--" + toValue + "\n";
            }
            
        }

    }

}