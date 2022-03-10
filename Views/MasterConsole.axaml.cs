using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RAPTOR_Avalonia_MVVM.ViewModels;

namespace RAPTOR_Avalonia_MVVM
{
    public partial class MasterConsole : Window
    {
        public MasterConsole()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            RAPTOR_Avalonia_MVVM.ViewModels.MasterConsoleViewModel.MC = new MasterConsoleViewModel();
            DataContext = RAPTOR_Avalonia_MVVM.ViewModels.MasterConsoleViewModel.MC;
            this.Closing += (s, e) =>
            {
                e.Cancel = true;
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
