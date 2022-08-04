using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Reflection;
using Avalonia.Input;
using RAPTOR_Avalonia_MVVM.Controls;
using RAPTOR_Avalonia_MVVM.ViewModels;
using Avalonia.Platform;

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
            this.Closing += async (s, e) =>
            {
                MainWindowViewModel mw = (this.DataContext as RAPTOR_Avalonia_MVVM.ViewModels.MainWindowViewModel);

                bool ans = mw.shouldClose;
                e.Cancel = ans;
                if (ans)
                {
                    mw.OnClosingCommand();
                }
                
            };

            this.FindControl<TabControl>("tc12").PointerPressed += (s, e) =>
            {
                if (e.GetCurrentPoint((TabControl)s).Properties.IsRightButtonPressed && e.Pointer.Type == PointerType.Mouse)
                {
                    MethodInfo mi = typeof(TabControl).GetMethod("UpdateSelectionFromEventSource", BindingFlags.NonPublic | BindingFlags.Instance);
                    mi.Invoke(s, new object[] { e.Source, true, false, false, false });
                }
            };

            this.FindControl<TabControl>("tc12").AddHandler(DragDrop.DropEvent, FlowchartControl.sDrop);

            this.KeyDown += (s, e) =>
            {
                KeyEventArgs f = (KeyEventArgs)e;
                if (f.KeyModifiers == PlatformCommandKey)
                {
                    FlowchartControl.ctrl = true;
                }
            };

            this.KeyUp += (s, e) =>
            {
                KeyEventArgs f = (KeyEventArgs)e;
                if (f.KeyModifiers == PlatformCommandKey)
                {
                    FlowchartControl.ctrl = false;
                }
            };

          

            var openCommand = this.FindControl<MenuItem>("OpenCommand");
            var newCommand = this.FindControl<MenuItem>("NewCommand");
            var saveCommand = this.FindControl<MenuItem>("SaveCommand");
            var undoCommand = this.FindControl<MenuItem>("UndoCommand");
            var redoCommand = this.FindControl<MenuItem>("RedoCommand");
            var cutCommand = this.FindControl<MenuItem>("CutCommand");
            var copyCommand = this.FindControl<MenuItem>("CopyCommand");
            var pasteCommand = this.FindControl<MenuItem>("PasteCommand");


            HotKeyManager.SetHotKey(openCommand, new KeyGesture(Key.O, PlatformCommandKey));
            HotKeyManager.SetHotKey(newCommand, new KeyGesture(Key.N, PlatformCommandKey));
            HotKeyManager.SetHotKey(saveCommand, new KeyGesture(Key.S, PlatformCommandKey));
            HotKeyManager.SetHotKey(undoCommand, new KeyGesture(Key.Z, PlatformCommandKey));
            HotKeyManager.SetHotKey(redoCommand, new KeyGesture(Key.Y, PlatformCommandKey));
            HotKeyManager.SetHotKey(cutCommand, new KeyGesture(Key.X, PlatformCommandKey));
            HotKeyManager.SetHotKey(copyCommand, new KeyGesture(Key.C, PlatformCommandKey));
            HotKeyManager.SetHotKey(pasteCommand, new KeyGesture(Key.V, PlatformCommandKey));

        }

        private static readonly KeyModifiers PlatformCommandKey = GetPlatformCommandKey();

        public static void setMainTitle(string s)
        {
            topWindow.Title = s;
        }

        private static OperatingSystemType GetOperatingSystemType()
        {
            return AvaloniaLocator.Current.GetService<IRuntimePlatform>().GetRuntimeInfo().OperatingSystem;
        }

        private static KeyModifiers GetPlatformCommandKey()
        {
            var os = GetOperatingSystemType();

            if (os == OperatingSystemType.OSX)
            {
                return KeyModifiers.Meta;
            }

            return KeyModifiers.Control;
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
