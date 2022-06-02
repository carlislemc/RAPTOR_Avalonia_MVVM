using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using raptor;

namespace RAPTOR_Avalonia_MVVM.Views
{
    public partial class GraphDialog : Window
    {
        public GraphDialog(){
            
        }
        public GraphDialog(int w, int h)
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            DataContext = new RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel(w, h, this);

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
