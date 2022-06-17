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
using RAPTOR_Avalonia_MVVM.Controls;

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

        public void DrawLine()
        {
            DotnetGraphControl.dngw.DrawLine(40, 40, 100, 100, Color_Type.Cyan);
        }

        public void DrawBox()
        {
            DotnetGraphControl.dngw.DrawBox(500, 500, 700, 700, Color_Type.Cyan, false);

        }

        public void DrawCircle()
        {
            DotnetGraphControl.dngw.DrawCircle(300, 300, 100, Color_Type.Cyan, true);

        }

        public void DrawEllipse()
        {
            DotnetGraphControl.dngw.DrawEllipse(100, 100, 200, 250, Color_Type.Cyan, true);

        }
        public void DrawArc()
        {
            DotnetGraphControl.dngw.DrawArc(100,100,200,0,100,200,300,250,Color_Type.Black);

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




        public void OnDoneCommand(){
            w.Close();
        }

    }

}