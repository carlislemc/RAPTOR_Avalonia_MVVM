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
            Dispatcher.UIThread.Post(() =>
            {
                DotnetGraphControl.dngw.DrawLine(x1, y1, x2, y2, c);
            }, DispatcherPriority.Background);
        }

        public static void DrawBox(int x1, int y1, int x2, int y2, Color_Type hue, bool filled)
        {
            Dispatcher.UIThread.Post(() =>
            {
                DotnetGraphControl.dngw.DrawBox(x1, y1, x2, y2, hue, filled);
            }, DispatcherPriority.Background);

        }

        public static void DrawCircle(int x1, int y1, int rad, Color_Type hue, bool filled)
        {
            Dispatcher.UIThread.Post(() =>
            {
                DotnetGraphControl.dngw.DrawCircle(x1, y1, rad, hue, filled);
            }, DispatcherPriority.Background);

        }

        public static void DrawEllipse(int x1, int y1, int x2, int y2, Color_Type hue, bool filled)
        {
            Dispatcher.UIThread.Post(() =>
            {
                DotnetGraphControl.dngw.DrawEllipse(x1, y1, x2, y2, hue, filled);
            }, DispatcherPriority.Background);

        }
        public static void DrawArc(int x1, int y1, int x2, int y2, int startx, int starty, int endx, int endy, Color_Type hue)
        {
            Dispatcher.UIThread.Post(() =>
            {
                DotnetGraphControl.dngw.DrawArc(x1, y1, x2, y2, startx, starty, endx, endy, hue);
            }, DispatcherPriority.Background);

        }
        public static void DrawEllipseRotate(int x1, int y1, int x2, int y2, double angle, Color_Type hue, bool filled)
        {
            Dispatcher.UIThread.Post(() =>
            {
                DotnetGraphControl.dngw.DrawEllipseRotate(x1, y1, x2, y2, angle, hue, filled);
            }, DispatcherPriority.Background);
        }

        public static void DisplayText(int x1, int y1, string text, Color_Type hue)
        {
            Dispatcher.UIThread.Post(() =>
            {
                DotnetGraphControl.dngw.DisplayText(x1, y1, text, hue);
            }, DispatcherPriority.Background);
        }

        public static void DisplayNumber(int x1, int y1, double number, Color_Type hue)
        {
            Dispatcher.UIThread.Post(() =>
            {
                DotnetGraphControl.dngw.DisplayNumber(x1, y1, number, hue);
            }, DispatcherPriority.Background);
        }

        public static void FontSize(int size)
        {
            Dispatcher.UIThread.Post(() =>
            {
                DotnetGraphControl.dngw.SetFontSize(size);
            }, DispatcherPriority.Background);
        }

        public static void WaitForMouseButton(MouseButton b)
        {
            Dispatcher.UIThread.Post(() =>
            {
                DotnetGraphControl.dngw.WaitForMouseButton(b);
            }, DispatcherPriority.Background);
        }
        public static void WaitForKey()
        {
            Dispatcher.UIThread.Post(() =>
            {
                DotnetGraphControl.dngw.WaitForKey();
            }, DispatcherPriority.Background);
        }

        public static void SetWindowTitle(string title)
        {
            Dispatcher.UIThread.Post(() =>
            {
                DotnetGraphControl.dngw.SetWindowTitle(title);
            }, DispatcherPriority.Background);
        }

        public static void ClearWindow(Color_Type hue)
        {
            Dispatcher.UIThread.Post(() =>
            {
                DotnetGraphControl.dngw.ClearWindow(hue);
            }, DispatcherPriority.Background);
        }

        public static void PlaySound(string s)
        {
            Dispatcher.UIThread.Post(() =>
            {
                DotnetGraph gd = new DotnetGraph(1, 1);
                DotnetGraphControl.dngw.PlaySound(s);
            }, DispatcherPriority.Background);
        }
        public static void PlaySoundBackground(string s)
        {
            Dispatcher.UIThread.Post(() =>
            {
                DotnetGraph gd = new DotnetGraph(1, 1);
                DotnetGraphControl.dngw.PlaySoundBackground(s);
            }, DispatcherPriority.Background);
        }
        public static void PlaySoundBackgroundLoop(string s)
        {
            Dispatcher.UIThread.Post(() =>
            {
                DotnetGraph gd = new DotnetGraph(1, 1);
                DotnetGraphControl.dngw.PlaySoundBackgroundLoop(s);
            }, DispatcherPriority.Background);
        }

        public static double GetWindowHeight()
        {
            return DotnetGraphControl.dngw.Height;
        }

        public static double GetWindowWidth()
        {
            return DotnetGraphControl.dngw.Width;
        }

        public static void DelayFor(int seconds)
        {
            Dispatcher.UIThread.Post(() =>
            {
                DotnetGraphControl.DelayFor(seconds);
            }, DispatcherPriority.Background);
        }

        public static void FloodFill(int x, int y, Color_Type hue)
        {
            Dispatcher.UIThread.Post(() =>
            {
                DotnetGraphControl.dngw.FloodFill(x, y, hue);
            }, DispatcherPriority.Background);
        }

        public static void PutPixel(int x, int y, Color_Type hue)
        {
            Dispatcher.UIThread.Post(() =>
            {
                DotnetGraphControl.dngw.PutPixel(x, y, hue);
            }, DispatcherPriority.Background);
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
                //DotnetGraphControl.dngw.OpenGraphWindow(w, h);
            }, DispatcherPriority.Background);

        }

        public static void CloseGraphWindow()
        {
            Dispatcher.UIThread.Post(() =>
            {
                DotnetGraphControl.dngw.CloseGraphWindow();
            }, DispatcherPriority.Background);
        }

        public static void FreezeGraphWindow()
        {
            Dispatcher.UIThread.Post(() =>
            {
                DotnetGraphControl.dngw.FreezeGraphWindow();
            }, DispatcherPriority.Background);
        }

        public static void UnFreezeGraphWindow()
        {
            Dispatcher.UIThread.Post(() =>
            {
                DotnetGraphControl.dngw.UnfreezeGraphWindow();
            }, DispatcherPriority.Background);
        }

        public static void UpdateGraphWindow()
        {
            Dispatcher.UIThread.Post(() =>
            {
                DotnetGraphControl.dngw.FreezeGraphWindow();
            }, DispatcherPriority.Background);
        }
    }

}