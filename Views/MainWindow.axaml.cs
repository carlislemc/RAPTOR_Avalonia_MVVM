using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Reflection;
using Avalonia.Input;
using RAPTOR_Avalonia_MVVM.Controls;

namespace RAPTOR_Avalonia_MVVM.Views
{
    public partial class MainWindow : Window
    {
        private int x = 0;
        public static MasterConsole? masterConsole;
        public static MainWindow? topWindow;

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            topWindow = this;
            this.Closing += (s, e) =>
            {
                e.Cancel = (this.DataContext as RAPTOR_Avalonia_MVVM.ViewModels.MainWindowViewModel).OnClosingCommand();
            };

            this.FindControl<TabControl>("tc12").PointerPressed += (s, e) =>
            {
                if (e.GetCurrentPoint((TabControl)s).Properties.IsRightButtonPressed && e.Pointer.Type == PointerType.Mouse)
                {
                    MethodInfo mi = typeof(TabControl).GetMethod("UpdateSelectionFromEventSource", BindingFlags.NonPublic | BindingFlags.Instance);
                    mi.Invoke(s, new object[] { e.Source, true, false, false, false });
                }
            };

            this.KeyDown += (s, e) =>
            {
                KeyEventArgs f = (KeyEventArgs)e;
                if (f.Key == Avalonia.Input.Key.LeftCtrl || f.Key == Avalonia.Input.Key.RightCtrl)
                {
                    FlowchartControl.ctrl = true;
                }
            };

            this.KeyUp += (s, e) =>
            {
                KeyEventArgs f = (KeyEventArgs)e;
                if (f.Key == Avalonia.Input.Key.LeftCtrl || f.Key == Avalonia.Input.Key.RightCtrl)
                {
                    FlowchartControl.ctrl = false;
                }
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            masterConsole = new MasterConsole();
            masterConsole.Show();
            raptor.PensBrushes.initialize();
        }
    }
}
