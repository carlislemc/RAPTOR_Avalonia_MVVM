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
using Avalonia.Media;
using RAPTOR_Avalonia_MVVM.Controls;

namespace RAPTOR_Avalonia_MVVM.ViewModels
{
    public class CommentDialogViewModel : ViewModelBase
    {
        public CommentDialogViewModel() {
            this.text = "";
        }
        public CommentDialogViewModel(CommentBox c, Window w) {
            this.text = "";
            this.c = c;
            this.w = w;
        }
        public Window w;
        public CommentBox c;
        public bool modified = false;
        public bool runningState = false;

        public string text = "";
        public string Text   // property
        {
            get { return text; }   // get method
            set { this.RaiseAndSetIfChanged(ref text,value); }  // set method
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

        public void OnDoneCommand(DrawingContext d){
            Undo_Stack.Make_Undoable(getSubchart());
            if(Text != ""){
                c.Text_Array[c.Text_Array.Length-1] = Text;
                c.text_change = true;
            }
            w.Close();            
        }

    }

}