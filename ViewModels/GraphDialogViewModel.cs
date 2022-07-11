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
using RAPTOR_Avalonia_MVVM.Views;
using Avalonia.Threading;

namespace RAPTOR_Avalonia_MVVM.ViewModels
{
    public class GraphDialogViewModel : ViewModelBase
    {
        // public GraphDialogViewModel() {

        // }

        //public static void OpenGraphWindow(int w, int h)
        //{
        //    Dispatcher.UIThread.Post(() =>
        //    {
        //        DotnetGraph gd = new DotnetGraph(w, h);
        //        SetWindowTitle("RAPTORGraph");
        //        gd.Show();

        //    }, DispatcherPriority.Background);
            
        //}

        public static void DrawLine(int x1, int y1, int x2, int y2,Color_Type c)
        {
            DotnetGraphControl.dngw.DrawLine(x1, y1, x2, y2, c);
        }

        public static void DrawBox(int x1, int y1, int x2, int y2, Color_Type hue, bool filled)
        {
            DotnetGraphControl.dngw.DrawBox(x1, y1, x2, y2, hue, filled);

        }

        public static void DrawCircle(int x1, int y1, int rad, Color_Type hue, bool filled)
        {
            DotnetGraphControl.dngw.DrawCircle(x1, y1, rad, hue, filled);

        }

        public static void DrawEllipse(int x1, int y1, int x2, int y2, Color_Type hue, bool filled)
        {
            DotnetGraphControl.dngw.DrawEllipse(x1, y1, x2, y2, hue, filled);

        }
        public static void DrawArc(int x1, int y1, int x2, int y2, int startx, int starty, int endx, int endy, Color_Type hue)
        {
            DotnetGraphControl.dngw.DrawArc(x1, y1, x2, y2, startx, starty, endx, endy, hue);

        }
        public static void DrawEllipseRotate(int x1, int y1, int x2, int y2, double angle, Color_Type hue, bool filled)
        {
            DotnetGraphControl.dngw.DrawEllipseRotate(x1, y1, x2, y2, angle, hue, filled);
        }

        public static void DisplayText(int x1, int y1, string text, Color_Type hue)
        {
            DotnetGraphControl.dngw.DisplayText(x1, y1, text, hue);
        }

        public static void DisplayNumber(int x1, int y1, double number, Color_Type hue)
        {
            DotnetGraphControl.dngw.DisplayNumber(x1, y1, number, hue);
        }

        public static void FontSize(int size)
        {
            DotnetGraphControl.dngw.SetFontSize(size);
        }

        public static void WaitForMouseButton(MouseButton b)
        {
            DotnetGraphControl.dngw.WaitForMouseButton(b);
        }
        public static void WaitForKey()
        {
            DotnetGraphControl.dngw.WaitForKey();
        }

        public static void SetWindowTitle(string title)
        {
            DotnetGraphControl.dngw.SetWindowTitle(title);
        }

        public static void ClearWindow(Color_Type hue)
        {
            DotnetGraphControl.dngw.ClearWindow(hue);
        }

        public static void PlaySound(string s)
        {
            DotnetGraph gd = new DotnetGraph(1, 1);
            DotnetGraphControl.dngw.PlaySound(s);
        }
        public static void PlaySoundBackground(string s)
        {
            DotnetGraph gd = new DotnetGraph(1, 1);
            DotnetGraphControl.dngw.PlaySoundBackground(s);
        }
        public static void PlaySoundBackgroundLoop(string s)
        {
            DotnetGraph gd = new DotnetGraph(1, 1);
            DotnetGraphControl.dngw.PlaySoundBackgroundLoop(s);
        }

        public static double GetWindowHeight()
        {
            return DotnetGraphControl.dngw.Height;
        }

        public static double GetWindowWidth()
        {
            return DotnetGraphControl.dngw.Width;
        }

        public async static Task DelayFor(int seconds)
        {
            await DotnetGraphControl.dngw.DelayFor(seconds);
        }

        public static void FloodFill(int x, int y, Color_Type hue)
        {
            DotnetGraphControl.dngw.FloodFill(x, y, hue);
        }

        public static void PutPixel(int x, int y, Color_Type hue)
        {
            DotnetGraphControl.dngw.PutPixel(x, y, hue);
        }

        //public static void GetMaxHeight()
        //{
        //    DotnetGraphControl.dngw.GetMaxHeight();
        //}

        //public static void GetMaxWidth()
        //{
        //    DotnetGraphControl.dngw.GetMaxWidth();
        //}

        public static void OpenGraphWindow(int w, int h)
        {
            Dispatcher.UIThread.Post(() =>
            {
                DotnetGraphControl.OpenGraphWindow(w, h);
            }, DispatcherPriority.Background);
            
        }

        public static void CloseGraphWindow()
        {
            DotnetGraphControl.dngw.CloseGraphWindow();
        }
    }

}