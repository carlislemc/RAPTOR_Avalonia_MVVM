using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using raptor;

namespace RAPTOR_Avalonia_MVVM.Views
{
    public partial class UserInputDialog : Window
    {
        public UserInputDialog(){

        }
        public UserInputDialog(Parallelogram p)
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            DataContext = new RAPTOR_Avalonia_MVVM.ViewModels.UserInputDialogViewModel(p, this);

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
