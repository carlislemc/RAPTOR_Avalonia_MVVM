using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using raptor;

namespace RAPTOR_Avalonia_MVVM.Views
{
    public partial class DotnetGraph : Window
    {
        public DotnetGraph()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }
        public DotnetGraph(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.CanResize = false;
            InitializeComponent();
            
#if DEBUG
            this.AttachDevTools();
#endif
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

