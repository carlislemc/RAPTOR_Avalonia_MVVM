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
using System.Diagnostics;

namespace RAPTOR_Avalonia_MVVM.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        public AboutViewModel() {
            this.text = "";
        }
        public AboutViewModel(Window w) {
            this.text = "";
            this.w = w;
        }
        public Window w;
        public string text = "";
        public string Text   // property
        {
            get { return text; }   // get method
            set { this.RaiseAndSetIfChanged(ref text,value); }  // set method
        }
        

        public void OnOkCommand(){
            w.Close();
        }

        public void goToLinkCommand(){
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "http://www.raptor.martincarlisle.com",
                UseShellExecute = true
            });
        }

    }

}