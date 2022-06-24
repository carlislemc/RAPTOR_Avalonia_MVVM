using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Input;
using raptor;
using RAPTOR_Avalonia_MVVM.ViewModels;

namespace RAPTOR_Avalonia_MVVM.Views
{
    public partial class DotnetGraph : Window
    {
        public DotnetGraph()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }
        public DotnetGraph(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.CanResize = false;
            InitializeComponent();
            
#if DEBUG
            this.AttachDevTools();
#endif


            //this.FindControl<UserControl>("dng")
            this.KeyDown += (s, e) =>
            {
                MainWindowViewModel mw = MainWindowViewModel.GetMainWindowViewModel();
                if (mw.waitingForKey == true)
                {
                    mw.waitingForKey = false;
                    mw.goToNextComponent();
                    if (mw.myTimer != null)
                    {
                        mw.myTimer.Start();
                    }
                }

            };

            this.PointerPressed += (s, e) =>
            {
                MainWindowViewModel mw = MainWindowViewModel.GetMainWindowViewModel();
                if (mw.waitingForMouse == true)
                {
                    if (e.MouseButton == mw.mouseWait)
                    {
                        mw.waitingForMouse = false;
                        mw.goToNextComponent();
                        if (mw.myTimer != null)
                        {
                            mw.myTimer.Start();
                        }
                    }
                }

            };


        }

        //protected override void OnKeyDown(Avalonia.Input.KeyEventArgs e)
        //{
        //    MainWindowViewModel mw = MainWindowViewModel.GetMainWindowViewModel();
        //    if (mw.waitingForKey == true)
        //    {
        //        mw.waitingForKey = false;
        //        if (mw.myTimer != null)
        //        {
        //            mw.myTimer.Start();
        //        }
        //    }
        //}



        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

