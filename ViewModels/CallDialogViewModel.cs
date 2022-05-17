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
    public class CallDialogViewModel : ViewModelBase
    {
        public CallDialogViewModel() {
            this.text = "";
        }

        public CallDialogViewModel(Rectangle r, Window w){
            this.text = "";
            this.r = r;
            this.w = w;
        }
        public Rectangle r;
        public Window w;
        public bool modified = false;
        public bool runningState = false;

        public string procedure = "";

        public string setProcedure{
            get { return procedure; }
            set { this.RaiseAndSetIfChanged(ref procedure, value); }
        }
        public string text = "";
        public string Text   // property
        {
            get { return text; }   // get method
            set { this.RaiseAndSetIfChanged(ref text,value); }  // set method
        }


        public void OnDoneCommand(){
            r.text_str = setProcedure;
            Text += "Done Call\n";
            w.Close();
        }

    }

}