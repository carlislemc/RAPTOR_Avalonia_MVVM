using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace RAPTOR_Avalonia_MVVM.Controls
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
