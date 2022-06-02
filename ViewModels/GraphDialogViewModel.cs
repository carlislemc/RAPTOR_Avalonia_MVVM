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
    public class GraphDialogViewModel : ViewModelBase
    {
        public GraphDialogViewModel() {
            this.width = 300;
            this.height = 300;
        }
        public GraphDialogViewModel(int width, int h, Window w) {
            this.w = w;
            this.width = width;
            this.height = h;
        }

        public Window w;
        public int height;
        public int getHeight{
            get{return height;}
            set{height = value;}
        }
        public int width;
        public int getWidth{
            get{return width;}
            set{width = value;}
        }
        public bool modified = false;
        public bool runningState = false;

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

        public void OnDoneCommand(){
            w.Close();
        }

    }

}