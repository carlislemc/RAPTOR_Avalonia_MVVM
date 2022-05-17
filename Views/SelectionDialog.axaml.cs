using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace RAPTOR_Avalonia_MVVM.Views
{
    public partial class SelectionDialog : Window
    {
        public SelectionDialog()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            DataContext = new RAPTOR_Avalonia_MVVM.ViewModels.SelectionDialogViewModel();

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
