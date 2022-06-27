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
        public static DotnetGraph dotnetgraph;
        public DotnetGraph(int width, int height)
        {
            dotnetgraph = this;
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

        public static void SetTitle(string title)
        {
            dotnetgraph.Title = title;
        }


        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

