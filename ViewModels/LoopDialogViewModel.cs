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
    public class LoopDialogViewModel : ViewModelBase
    {
        public LoopDialogViewModel() {
            this.text = "";
        }

        public LoopDialogViewModel(Loop l, Window w) {
            this.text = "";
            this.l = l;
            this.w = w;
        }
        public Loop l;
        public Window w;
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
            set {this.RaiseAndSetIfChanged(ref loopCondition, value);}
        }

        public void OnDoneCommand(){
            l.text_str = setLoop;
            Text += "Done Loop\n";
            w.Close();
            //Console.WriteLine("hi there dude");
        }

    }

}