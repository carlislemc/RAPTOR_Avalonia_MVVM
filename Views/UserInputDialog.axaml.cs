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
        public UserInputDialog(Parallelogram p, numbers.value v = null, bool compileMode = false)
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            DataContext = new RAPTOR_Avalonia_MVVM.ViewModels.UserInputDialogViewModel(p, v, this, compileMode);

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
