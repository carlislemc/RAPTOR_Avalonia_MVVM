using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Input;
using raptor;
using RAPTOR_Avalonia_MVVM.ViewModels;
using RAPTOR_Avalonia_MVVM.Controls;
using Avalonia.Threading;
using System.Threading.Tasks;

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


            this.Closing += (s, e) =>
            {
                DotnetGraphControl.onClosingCommand();
                dotnetgraph = null;
            };

            this.KeyDown += (s, e) =>
            {
                MainWindowViewModel mw = MainWindowViewModel.GetMainWindowViewModel();
                

                if (mw.waitingForKey == true)
                {
                    mw.waitingForKey = false;
                    DotnetGraphControl.waitForKey = false;
                    mw.goToNextComponent();
                    if (mw.myTimer != null)
                    {
                        mw.myTimer.Start();
                    }
                }
         
                if(DotnetGraphControl.dngw != null)
                {
                    DotnetGraphControl.key = e.Key;
                    DotnetGraphControl.keyHit = true;
                    DotnetGraphControl.keyDown = true;
                }
            };

            this.KeyUp += (s, e) =>
            {
                if (DotnetGraphControl.dngw != null)
                {
                    DotnetGraphControl.keyDown = false;
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

                if (DotnetGraphControl.dngw != null)
                {
                    if(e.MouseButton == MouseButton.Left)
                    {
                        DotnetGraphControl.leftMouseButtonPressed = true;
                    }
                    else
                    {
                        DotnetGraphControl.rightMouseButtonPressed = true;
                    }
                    DotnetGraphControl.mouseDown = true;
                    DotnetGraphControl.buttonDown = e.MouseButton;
                }

            };

            this.PointerReleased += (s, e) =>
            {
                if (DotnetGraphControl.dngw != null)
                {
                    DotnetGraphControl.mouseDown = false;

                    if(e.InitialPressMouseButton == MouseButton.Left)
                    {
                        DotnetGraphControl.leftMouseButtonReleased = true;

                    }
                    else
                    {
                        DotnetGraphControl.rightMouseButtonReleased = true;
                    }

                    if (DotnetGraphControl.waitForMouse)
                    {
                        if(e.InitialPressMouseButton == DotnetGraphControl.mb)
                        {
                            DotnetGraphControl.waitForMouse = false;
                        }
                    }
                    
                    DotnetGraphControl.xLoc = DotnetGraphControl.xCord;
                    DotnetGraphControl.yLoc = DotnetGraphControl.yCord;
                }
            };

            this.PointerMoved += (s, e) =>
            {
                DotnetGraphControl.xCord = (int)e.GetPosition(dotnetgraph).X - 1;
                DotnetGraphControl.yCord = (int)dotnetgraph.Height - (int)e.GetPosition(dotnetgraph).Y;
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

        public static double getMaxHeight()
        {
            return 3;
        }

        public async static Task WaitForMouseButton(MouseButton button)
        {
            MainWindowViewModel mw = MainWindowViewModel.GetMainWindowViewModel();

            if (mw.myTimer != null)
            {
                mw.myTimer.Stop();
            }
            bool waiting = true;
            while (waiting)
            {
                waiting = DotnetGraphControl.waitForMouse;
                await Task.Delay(1);
            }

            if (mw.myTimer != null)
            {
                mw.myTimer.Start();
            }
        }

        public async static Task waitForKey()
        {
            MainWindowViewModel mw = MainWindowViewModel.GetMainWindowViewModel();

            if (mw.myTimer != null)
            {
                mw.myTimer.Stop();
            }
            bool waiting = true;
            while (waiting)
            {
                waiting = DotnetGraphControl.waitForKey;
                await Task.Delay(1);
            }

            if (mw.myTimer != null)
            {
                mw.myTimer.Start();
            }

        }
    }
}

