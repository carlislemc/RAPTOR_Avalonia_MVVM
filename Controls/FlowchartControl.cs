using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using raptor;

namespace RAPTOR_Avalonia_MVVM.Controls
{

    class FlowchartControl : Control
    {
        double mouse_x, mouse_y;
        static FlowchartControl()
        {
            AffectsRender<FlowchartControl>(AngleProperty);
        }
        private Subchart sc;

        // This code seems to only run when the mouse is over the flowchart itself
        private void onclick(object? sender, PointerEventArgs e)
        {
            this.mouse_x = e.GetPosition(this).X; 
            this.mouse_y = e.GetPosition(this).Y;
        }
        public FlowchartControl()
        {
            sc = new Subchart();
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1 / 60.0);
            timer.Tick += (sender, e) => Angle += Math.PI / 360;
            timer.Start();
            this.PointerMoved += this.onclick;
        }

        public static readonly StyledProperty<double> AngleProperty =
            AvaloniaProperty.Register<FlowchartControl, double>(nameof(Angle));

        public double Angle
        {
            get => GetValue(AngleProperty);
            set => SetValue(AngleProperty, value);
        }
        public ObservableCollection<MenuItem> ContextMenuItemsFunction()
        {
            return new ObservableCollection<MenuItem> { new MenuItem() { Header = "Hello" }, new MenuItem() { Header = "World" } };
        }
        public override void Render(DrawingContext drawingContext)
        {
            int x1, y1;

            if (this.Tag!=null)
            {
                this.sc = ((Func<Subchart>) this.Tag)();
            }
            sc.Start.footprint(drawingContext);
            int draw_width = sc.Start.FP.right + sc.Start.FP.left + Visual_Flow_Form.flow_width;
            int draw_height = sc.Start.FP.height + Visual_Flow_Form.flow_height;
            (this.Parent as UserControl).Width = draw_width;
            (this.Parent as UserControl).Height = draw_height;
            Point pt1 = new Point(50, 50);
            Point pt2 = new Point(100, 100);
            x1 = (int)sc.Start.FP.left + Visual_Flow_Form.flow_width/2;
            y1 = (int)Math.Round(1.0 * 30);
            drawingContext.DrawRectangle(PensBrushes.whitebrush, PensBrushes.white_pen,
                new Rect(new Point(0, 0), new Point(draw_width, draw_height)));
            sc.Start.draw(drawingContext, x1, y1);

        }
    }
}

