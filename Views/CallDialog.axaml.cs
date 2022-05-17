using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace RAPTOR_Avalonia_MVVM.Views
{
    public partial class CallDialog : Window
    {
        public CallDialog()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            DataContext = new RAPTOR_Avalonia_MVVM.ViewModels.CallDialogViewModel();

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
