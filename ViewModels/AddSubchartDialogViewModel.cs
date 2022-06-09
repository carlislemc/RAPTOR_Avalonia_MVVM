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
        public AddSubchartDialogViewModel(Window w, bool modding) {
            this.text = "";
            this.w = w;
            this.modding = modding;

            if (modding)
            {
                MainWindowViewModel mw = MainWindowViewModel.GetMainWindowViewModel();
                Subchart s = mw.theTabs[mw.setViewTab];
                subchartName = s.Header;
            }
        }
        public Window w;
        public bool modding;
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
            Subchart addMe = new Subchart(setSubchartName);

            if (!modding)
            {
                tbs.Add(addMe);
            }
            else
            {
                MainWindowViewModel mw = MainWindowViewModel.GetMainWindowViewModel();
                int spot = mw.setViewTab;
                tbs.RemoveAt(spot);
                tbs.Insert(spot, addMe);
                mw.setViewTab = spot;
            }

            Undo_Stack.Make_Add_Tab_Undoable(tbs[tbs.Count-1]);
            w.Close();
        }

    }

}