using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace RAPTOR_Avalonia_MVVM.Views
{
    public partial class MainWindow : Window
    {
        private int x = 0;
        private MasterConsole masterConsole;
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.Closing += (s, e) =>
            {
                e.Cancel = (this.DataContext as RAPTOR_Avalonia_MVVM.ViewModels.MainWindowViewModel).OnClosingCommand();
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
