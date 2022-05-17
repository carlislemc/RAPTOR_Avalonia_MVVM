using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace RAPTOR_Avalonia_MVVM.Views
{
    public partial class OutputDialog : Window
    {
        public OutputDialog()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            DataContext = new RAPTOR_Avalonia_MVVM.ViewModels.OutputDialogViewModel();

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
