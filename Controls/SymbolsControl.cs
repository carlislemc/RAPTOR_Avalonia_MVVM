using System;
using System.Collections.Generic;
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

    class SymbolsControl : Control
    {

        public const int assignment_fig = 0;
        public const int call_fig = 1;
        public const int input_fig = 2;
        public const int output_fig = 3;
        public const int if_control_fig = 4;
        public const int loop_fig = 5;
        public const int return_fig = 6;
        public int control_figure_selected = -1;
        private Rectangle ASGN, CALL;
        private Parallelogram INPUT, OUTPUT;
        private Oval_Return RETURN;
        private IF_Control IFC;
        private Loop LP;
		public const int control_height = 24, control_width = 36;
		// location of controls in left panel
		public const int control_X = 65;
		public static SymbolsControl theControl;

		static SymbolsControl()
		{
			AffectsRender<SymbolsControl>(versionProperty);
		}
		public int version
		{
			get => GetValue(versionProperty);
			set => SetValue(versionProperty, value);
		}
		public static readonly StyledProperty<int> versionProperty =
				AvaloniaProperty.Register<SymbolsControl, int>(nameof(version));
		public SymbolsControl()
        {
			theControl = this;
            ASGN = new Rectangle(control_height, control_width,
                "Rectangle", Rectangle.Kind_Of.Assignment);
            CALL = new Rectangle(control_height, control_width,
                "Rectangle", Rectangle.Kind_Of.Call);
            INPUT = new Parallelogram(control_height, control_width,
                "Parallelogram", true);
            OUTPUT = new Parallelogram(control_height, control_width,
                "Parallelogram", false);
            RETURN = new Oval_Return(control_height, control_width,
                "Return");
            IFC = new IF_Control(control_height - 15, control_width - 15, "IF_Control");
            LP = new Loop(control_height - 15, control_width - 15, "Loop");
			this.PointerPressed += mouseDownEvent;
			//this.Parent.PointerPressed += mouseDownEvent;
			version = 0;

        }
		public void mouseDownEvent(object? sender, PointerPressedEventArgs e)
        {
			int mouse_x = (int) e.GetCurrentPoint(this).Position.X;
			int mouse_y = (int) e.GetCurrentPoint(this).Position.Y;

			if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed) //)
			{
				ASGN.selected = false;
				CALL.selected = false;
				INPUT.selected = false;
				OUTPUT.selected = false;
				IFC.selected = false;
				LP.selected = false;

				if (ASGN.In_Footprint(mouse_x, mouse_y))
				{
					control_figure_selected = assignment_fig;
					version++;
					//this.DoDragDrop("raptor_ASGN", DragDropEffects.Copy | DragDropEffects.Link);
				}
				else if (CALL.In_Footprint(mouse_x, mouse_y))
				{
					control_figure_selected = call_fig;
					version++;

					//this.DoDragDrop("raptor_CALL", DragDropEffects.Copy | DragDropEffects.Link);
				}
				else if (INPUT.In_Footprint(mouse_x, mouse_y))
				{
					control_figure_selected = input_fig;
					version++;
					//this.DoDragDrop("raptor_INPUT", DragDropEffects.Copy | DragDropEffects.Link);
				}
				else if (OUTPUT.In_Footprint(mouse_x, mouse_y))
				{
					control_figure_selected = output_fig;
					version++;
					//this.DoDragDrop("raptor_OUTPUT", DragDropEffects.Copy | DragDropEffects.Link);
				}
				else if (IFC.In_Footprint(mouse_x, mouse_y))
				{
					control_figure_selected = if_control_fig;
					version++;
					//this.DoDragDrop("raptor_SELECTION", DragDropEffects.Copy | DragDropEffects.Link);
				}
				else if (LP.In_Footprint(mouse_x, mouse_y))
				{
					control_figure_selected = loop_fig;
					version++;
					//this.DoDragDrop("raptor_LOOP", DragDropEffects.Copy | DragDropEffects.Link);
				}
				else if (RETURN.In_Footprint(mouse_x, mouse_y))
				{
					control_figure_selected = return_fig;
					version++;
					//this.DoDragDrop("raptor_RETURN", DragDropEffects.Copy | DragDropEffects.Link);
				}
				else
				{
					control_figure_selected = -1;
					version++;
				}
			}
		}

        public override void Render(DrawingContext drawingContext)
        {
			drawingContext.DrawRectangle(PensBrushes.whitebrush, PensBrushes.white_pen,
				new Rect(new Point(0, 0), new Point(2*control_X, this.Height)));
			Avalonia.Media.FormattedText symbolsText = new Avalonia.Media.FormattedText(
				"Symbols", new Avalonia.Media.Typeface("arial",FontStyle.Normal,FontWeight.Bold), 
				10, Avalonia.Media.TextAlignment.Center,
				Avalonia.Media.TextWrapping.NoWrap, Avalonia.Size.Infinity);
			//symbolsText.
			drawingContext.DrawText(PensBrushes.blackbrush,
				new Point(control_X - ASGN.W / 2 - 4, 5),
				symbolsText);
			ASGN.selected = false;
			CALL.selected = false;
			INPUT.selected = false;
			OUTPUT.selected = false;
			IFC.selected = false;
			LP.selected = false;
			RETURN.selected = false;

			if (control_figure_selected == assignment_fig)
			{
				ASGN.selected = true;
			}
			else if (control_figure_selected == call_fig)
			{
				CALL.selected = true;
			}
			else if (control_figure_selected == input_fig)
			{
				INPUT.selected = true;
			}
			else if (control_figure_selected == output_fig)
			{
				OUTPUT.selected = true;
			}
			else if (control_figure_selected == if_control_fig)
			{
				IFC.selected = true;
			}
			else if (control_figure_selected == loop_fig)
			{
				LP.selected = true;
			}
			else if (control_figure_selected == return_fig)
			{
				RETURN.selected = true;
			}
			ASGN.draw(drawingContext, control_X, 25);
			ASGN.X = control_X;
			ASGN.Y = 25;
			if (!Component.USMA_mode)
			{
				Avalonia.Media.FormattedText assignmentText = new Avalonia.Media.FormattedText(
					"Assignment", new Avalonia.Media.Typeface("arial"), 10, Avalonia.Media.TextAlignment.Center,
					Avalonia.Media.TextWrapping.NoWrap, Avalonia.Size.Infinity);
				drawingContext.DrawText(PensBrushes.blackbrush,
					new Point(control_X - ASGN.W / 2 - 8, 50),
					assignmentText);
			}
			else
			{
				Avalonia.Media.FormattedText processText = new Avalonia.Media.FormattedText(
					"Process", new Avalonia.Media.Typeface("arial"), 10, Avalonia.Media.TextAlignment.Center,
					Avalonia.Media.TextWrapping.NoWrap, Avalonia.Size.Infinity);
				drawingContext.DrawText(PensBrushes.blackbrush,
					new Point(control_X - ASGN.W / 2 - 5, 50),
					processText);

			}
			CALL.Y = 68;
			if (Component.Current_Mode == Mode.Expert)
			{
				CALL.X = control_X - 3 * ASGN.W / 4;
				RETURN.X = control_X + 3 * ASGN.W / 4 + 3;
				RETURN.Y = CALL.Y;
				RETURN.draw(drawingContext, RETURN.X, RETURN.Y);
				Avalonia.Media.FormattedText returnText = new Avalonia.Media.FormattedText(
					"Return", new Avalonia.Media.Typeface("arial"), 10, Avalonia.Media.TextAlignment.Center,
					Avalonia.Media.TextWrapping.NoWrap, Avalonia.Size.Infinity);
				drawingContext.DrawText(PensBrushes.blackbrush,
					new Point(control_X + ASGN.W / 2 - 8, 93),
					returnText);
			}
			else
			{
				CALL.X = control_X;
			}
			CALL.draw(drawingContext, CALL.X, 68);

			if (!Component.USMA_mode)
			{
				Avalonia.Media.FormattedText callText = new Avalonia.Media.FormattedText(
					"Call", new Avalonia.Media.Typeface("arial"), 10, Avalonia.Media.TextAlignment.Center,
					Avalonia.Media.TextWrapping.NoWrap, Avalonia.Size.Infinity);
				drawingContext.DrawText(PensBrushes.blackbrush,
					new Point(CALL.X - 10, 93),
					callText);
			}
			else
			{
				Avalonia.Media.FormattedText flowTransferText = new Avalonia.Media.FormattedText(
					"Flow transfer", new Avalonia.Media.Typeface("arial"), 10, Avalonia.Media.TextAlignment.Center,
					Avalonia.Media.TextWrapping.NoWrap, Avalonia.Size.Infinity);
				drawingContext.DrawText(PensBrushes.blackbrush,
					new Point(CALL.X - ASGN.W / 2 - 20, 93),
					flowTransferText); 
			}
			INPUT.draw(drawingContext, control_X - 3 * ASGN.W / 4, 111);
			INPUT.X = control_X - 3 * ASGN.W / 4;
			INPUT.Y = 111;
			Avalonia.Media.FormattedText inputText = new Avalonia.Media.FormattedText(
				"Input", new Avalonia.Media.Typeface("arial"), 10, Avalonia.Media.TextAlignment.Center,
				Avalonia.Media.TextWrapping.NoWrap, Avalonia.Size.Infinity);
			drawingContext.DrawText(PensBrushes.blackbrush,
				new Point(control_X - INPUT.W - 4, 136),
				inputText); 

			OUTPUT.draw(drawingContext, control_X + 3 * ASGN.W / 4 + 3, 111);
			OUTPUT.X = control_X + 3 * ASGN.W / 4 + 3;
			OUTPUT.Y = 111;
			Avalonia.Media.FormattedText outputText = new Avalonia.Media.FormattedText(
				"Output", new Avalonia.Media.Typeface("arial"), 10, Avalonia.Media.TextAlignment.Center,
				Avalonia.Media.TextWrapping.NoWrap, Avalonia.Size.Infinity);
			drawingContext.DrawText(PensBrushes.blackbrush,
				new Point(control_X + ASGN.W / 2 - 4, 136),
				outputText); 

			IFC.draw(drawingContext, control_X, 153);
			IFC.X = control_X;
			IFC.Y = 153;
			Avalonia.Media.FormattedText selectionText = new Avalonia.Media.FormattedText(
				"Selection", new Avalonia.Media.Typeface("arial"), 10, Avalonia.Media.TextAlignment.Center,
				Avalonia.Media.TextWrapping.NoWrap, Avalonia.Size.Infinity);
			drawingContext.DrawText(PensBrushes.blackbrush,
				new Point(control_X - ASGN.W / 2 - 4, 168),
				selectionText); 
			LP.draw(drawingContext, control_X, 183);
			LP.X = control_X;
			LP.Y = 183;
			if (!Component.USMA_mode)
			{
				Avalonia.Media.FormattedText loopText = new Avalonia.Media.FormattedText(
					"Loop", new Avalonia.Media.Typeface("arial"), 10, Avalonia.Media.TextAlignment.Center,
					Avalonia.Media.TextWrapping.NoWrap, Avalonia.Size.Infinity);
				drawingContext.DrawText(PensBrushes.blackbrush,
					new Point(control_X - ASGN.W / 2 + 8, 218),
					loopText); 

			}
			else
			{
				Avalonia.Media.FormattedText iterationText = new Avalonia.Media.FormattedText(
					"Iteration", new Avalonia.Media.Typeface("arial"), 10, Avalonia.Media.TextAlignment.Center,
					Avalonia.Media.TextWrapping.NoWrap, Avalonia.Size.Infinity);
				drawingContext.DrawText(PensBrushes.blackbrush,
					new Point(control_X - ASGN.W / 2 - 8, 218),
					iterationText); 

			}
		}
    }
}

