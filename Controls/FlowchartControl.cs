using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using raptor;
using System.Diagnostics;
using RAPTOR_Avalonia_MVVM.ViewModels;

namespace RAPTOR_Avalonia_MVVM.Controls
{

    class FlowchartControl : Control
    {
        static FlowchartControl()
        {
            AffectsRender<FlowchartControl>(AngleProperty);
        }
        public Subchart sc;
        public static bool ctrl = false;
       

        // This code seems to only run when the mouse is over the flowchart itself
        private void onMouseMove(object? sender, PointerEventArgs e)
        {
            this.sc.positionX = (int) e.GetPosition(this).X;
            this.sc.positionY = (int) e.GetPosition(this).Y;
        }

        DataObject dragData = new DataObject();
        public Component dragComp = null;
        public void mouseDownDragEvent(object? sender, PointerPressedEventArgs e)
        {
            if(dragComp == null || dragComp.GetType() == typeof(Oval) || ctrl || e.MouseButton == MouseButton.Right)
            {
                return;
            }
            dragData.Set(DataFormats.Text, dragComp);
            DragDrop.DoDragDrop((PointerPressedEventArgs)e, dragData, DragDropEffects.Move);
        }

        private void onClick(object? sender, RoutedEventArgs e)
        {
            PointerPressedEventArgs f = (PointerPressedEventArgs)e;
            this.sc.positionX = (int)f.GetPosition(this).X;
            this.sc.positionY = (int)f.GetPosition(this).Y;
            this.sc.positionXTapped = (int)f.GetPosition(this).X;
            this.sc.positionYTapped = (int)f.GetPosition(this).Y;
            if (f.MouseButton != MouseButton.Left)
            {
                return;
            }

            this.sc.Start.select(this.sc.positionX, this.sc.positionY, ctrl);
            dragComp = this.sc.Start.copy();
            

            if(this.sc.Start.check_expansion_click(this.sc.positionX, this.sc.positionY))
            {
                return;
            }
            

            if (SymbolsControl.control_figure_selected==SymbolsControl.assignment_fig)
            {
                this.sc.OnInsertAssignmentCommand();
            }
            else if (SymbolsControl.control_figure_selected == SymbolsControl.call_fig)
            {
                this.sc.OnInsertCallCommand();
            }
            else if (SymbolsControl.control_figure_selected == SymbolsControl.input_fig)
            {
                this.sc.OnInsertInputCommand();
            }
            else if (SymbolsControl.control_figure_selected == SymbolsControl.output_fig)
            {
                this.sc.OnInsertOutputCommand();
            }
            else if (SymbolsControl.control_figure_selected == SymbolsControl.if_control_fig)
            {
                this.sc.OnInsertSelectionCommand();
            }
            else if (SymbolsControl.control_figure_selected == SymbolsControl.loop_fig)
            {
                this.sc.OnInsertLoopCommand();
            }

        }

        private void Drop(object? sender, DragEventArgs e)
        {

            this.sc.positionX = (int)e.GetPosition(this).X;
            this.sc.positionY = (int)e.GetPosition(this).Y;
            this.sc.positionXTapped = (int)e.GetPosition(this).X;
            this.sc.positionYTapped = (int)e.GetPosition(this).Y;
            
            string r = e.Data.GetText();
            if (r!=null || dragComp!=null)
            {
                if (e.Data.GetText() == SymbolsControl.assignment_fig)
                {
                    this.sc.OnInsertAssignmentCommand();
                    //Debug.WriteLine("Dropped");
                }
                else if (e.Data.GetText() == SymbolsControl.call_fig)
                {
                    this.sc.OnInsertCallCommand();
                }
                else if (e.Data.GetText() == SymbolsControl.input_fig)
                {
                    this.sc.OnInsertInputCommand();
                }
                else if (e.Data.GetText() == SymbolsControl.output_fig)
                {
                    this.sc.OnInsertOutputCommand();
                }
                else if (e.Data.GetText() == SymbolsControl.if_control_fig)
                {
                    this.sc.OnInsertSelectionCommand();
                }
                else if (e.Data.GetText() == SymbolsControl.loop_fig)
                {
                    this.sc.OnInsertLoopCommand();
                }
                else if (dragComp != null)
                {
                    MainWindowViewModel mw = MainWindowViewModel.GetMainWindowViewModel();
                    if (mw.theTabs[mw.viewTab].Start.insert(dragComp, this.sc.positionX, this.sc.positionY, 0))
                    {
                        this.sc.Start.delete();
                        Undo_Stack.Make_Undoable(mw.theTabs[mw.viewTab]);
                    }
                }
                dragComp = null;
                this.sc.Start.select(this.sc.positionX, this.sc.positionY, ctrl);
                e.Handled = true;
            }

        }
        private void doubleClick(object? sender, RoutedEventArgs e)
        {
            TappedEventArgs f = (TappedEventArgs)e;
            this.sc.positionX = (int)f.GetPosition(this).X;
            this.sc.positionY = (int)f.GetPosition(this).Y;
            _ = this.sc.Start.setText(this.sc.positionX, this.sc.positionY);
        }
        
        public async static void sDrop(object? sender, DragEventArgs e)
        {
            if (e.Data.GetFileNames() != null)
            {
                string file;
                try
                {
                    file = e.Data.GetFileNames().First();
                }
                catch
                {
                    return;
                }

                //Debug.WriteLine(file);

                await MainWindowViewModel.GetMainWindowViewModel().OnNewCommand();
                
                string name = (String)file.Clone();

                file.ToCharArray();
                int i = 1;
                foreach (char c in file)
                {

                    if (c == '\\')
                    {
                        name = name.Insert(i, "\\");
                        i++;
                    }
                    i++;
                }

                //Debug.WriteLine(name);
                MainWindowViewModel.GetMainWindowViewModel().Load_File(name);

                e.Handled = true;
                
            }
            
        }
        
        private void released(object? sender, RoutedEventArgs e)
        {
            dragComp = null;
        }


        public static FlowchartControl fcc;
        public FlowchartControl()
        {
            sc = new Subchart();
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1 / 60.0);
            timer.Tick += (sender, e) => Angle += Math.PI / 360;
            timer.Start();
            this.PointerMoved += this.onMouseMove;
            //this.Tapped += this.onClick;
            this.PointerPressed += this.onClick;
            AddHandler(DragDrop.DropEvent, Drop);
            this.DoubleTapped += this.doubleClick;
            AddHandler(DragDrop.DropEvent, sDrop);
            dragData.Set(DataFormats.Text, SymbolsControl.noSelect);
            this.PointerPressed += mouseDownDragEvent;
            this.PointerReleased += released;
            fcc = this;
        }


        public static readonly StyledProperty<double> AngleProperty =
            AvaloniaProperty.Register<FlowchartControl, double>(nameof(Angle));

        public double Angle
        {
            get => GetValue(AngleProperty);
            set => SetValue(AngleProperty, value);
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

            Avalonia.Rect comment_rec = sc.Start.comment_footprint();
            if (comment_rec.Height > draw_height)
            {
                draw_height = (int)comment_rec.Height + 20;
            }
            if (comment_rec.Width > draw_width)
            {
               draw_width = (int)comment_rec.Width + 20;
            }

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

