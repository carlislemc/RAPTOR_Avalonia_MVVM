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
        public InputDialogViewModel(Parallelogram p, Window w) {
            this.text = "";
            this.p = p;
            this.w = w;
        }
        public Parallelogram p;
        public Window w;
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
            set { this.RaiseAndSetIfChanged(ref prompt, value); }
        }

        public string variable = "";

        public string getVariable{
            get { return variable; }
            set { this.RaiseAndSetIfChanged(ref variable, value); }
        }


        public void OnDoneCommand(){
            p.prompt = getPrompt;
            p.text_str = getVariable;
            Text += "Done Input\n";
            w.Close();
            //Console.WriteLine("hi there dude");
        }

    }

}