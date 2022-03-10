using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Avalonia;

namespace raptor
{
	/// <summary>
	/// Summary description for BinaryComponent.
	/// </summary>
	public abstract class BinaryComponent : Component
	{
		protected Component first_child, second_child;
		protected bool is_compressed = false;

		public BinaryComponent(int h, int w, String str_name) : base(h,w,str_name)
		{
		}

		public BinaryComponent(Component S, int h, int w, 
			String str_name)	: base (S,h,w,str_name)
		{
		}
        
		//Deserialization constructor.
		public BinaryComponent(SerializationInfo info, 
			StreamingContext ctxt) : base(info,ctxt)
		{
			if (incoming_serialization_version>=10)
			{
				is_compressed = (bool)info.GetValue("_is_compressed", typeof(bool));
			}
			else
			{
				is_compressed = false;
			}
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
		{
			info.AddValue("_is_compressed", this.is_compressed);
			base.GetObjectData(info,ctxt);
		}

		protected bool Diamond_Bounded_X(int x)
		{
			bool bounded_x = Math.Abs(x-X) <= W/2;
			if (this.Is_Wide_Diamond())
			{
				bounded_x = Math.Abs(x-X) <= this.drawing_text_width/2+3*W/8;
			}
			return bounded_x;
		}

		protected int Diamond_Width()
		{
			if (this.Is_Wide_Diamond())
			{
				return this.drawing_text_width+6*W/8;
			}
			else
			{
				return W;
			}
		}

		public override int Count_Symbols()
		{
			int count_first = 0, count_second = 0, count_succ = 0;
			if (this.Successor!=null)
			{
				count_succ = this.Successor.Count_Symbols();
			}
			if (this.first_child!=null)
			{
				count_first = this.first_child.Count_Symbols();
			}
			if (this.second_child!=null)
			{
				count_second = this.second_child.Count_Symbols();
			}
			return 1 + count_first + count_second + count_succ;
		}

		public override bool SelectRegion(Avalonia.Rect rec)
		{
			bool succ_selected=false;
			bool first_selected=false;
			bool second_selected=false;
			if (this.Successor!=null)
			{
				succ_selected = this.Successor.SelectRegion(rec);
			}
			if (this.first_child!=null && !this.is_compressed)
			{
				first_selected = this.first_child.SelectRegion(rec);
			}
			if (this.second_child!=null && !this.is_compressed)
			{
				second_selected = this.second_child.SelectRegion(rec);
			}
			if (this.contains(rec)|| (first_selected && second_selected)
				|| (succ_selected && (first_selected || second_selected)))
			{
				this.selected = true;
				if (this.first_child!=null)
				{
					this.first_child.selectAll();
				}
				if (this.second_child!=null)
				{
					this.second_child.selectAll();
				}
				return true;
			}
			else
			{
				this.selected = false;
			}
			return succ_selected || first_selected || second_selected;
		}
		public override void selectAll()
		{
			this.selected = true;
			if (this.Successor != null)
			{
				this.Successor.selectAll();
			}
			if (this.first_child != null)
			{
				this.first_child.selectAll();
			}

			if (this.second_child != null)
			{
				this.second_child.selectAll();
			}
		}

		// If (x, y) is over the object color it red?
		public override Component select(int x, int y)
		{
			Component succ_selected = null; 
			Component first_selected = null;
			Component second_selected= null;

			this.selected = false;
			
			if (this.first_child != null && !this.is_compressed)
			{
				first_selected = this.first_child.select(x,y);
			}

            if (this.second_child != null && !this.is_compressed)
			{
				second_selected = this.second_child.select(x,y);
			}

			if (this.Successor != null)
			{
				succ_selected = this.Successor.select(x,y);
			}

			if (this.contains(x,y))
			{
				this.selected = true;
				if (this.second_child!=null)
				{
					this.second_child.selectAll();
				}
				if (this.first_child!=null)
				{
					this.first_child.selectAll();
				}
				return this;
			}
			else if (first_selected != null)
			{
				return first_selected;
			}
			else if (second_selected != null)
			{
				return second_selected;
			}
			else if (succ_selected != null)
			{
				return succ_selected;
			}
			else
			{
				return null; 
			}
		}

        // Find Component containing (x, y)
        public override Component Find_Component(int x, int y)
        {
            Component succ_selected = null;
            Component first_selected = null;
            Component second_selected = null;

            if (this.first_child != null && !this.is_compressed)
            {
                first_selected = this.first_child.Find_Component(x, y);
            }

            if (this.second_child != null && !this.is_compressed)
            {
                second_selected = this.second_child.Find_Component(x, y);
            }

            if (this.Successor != null)
            {
                succ_selected = this.Successor.Find_Component(x, y);
            }

            if (this.contains(x, y))
            {
                return this;
            }
            else if (first_selected != null)
            {
                return first_selected;
            }
            else if (second_selected != null)
            {
                return second_selected;
            }
            else if (succ_selected != null)
            {
                return succ_selected;
            }
            else
            {
                return null;
            }
        }

        // If (x, y) is over the object color it red?
		public override CommentBox selectComment(int x, int y)
		{
			CommentBox succ_selected = null; 
			CommentBox first_selected = null;
			CommentBox second_selected= null;

            if (this.first_child != null && !this.is_compressed)
			{
				first_selected = this.first_child.selectComment(x,y);
			}

            if (this.second_child != null && !this.is_compressed)
			{
				second_selected = this.second_child.selectComment(x,y);
			}

			if (this.Successor != null)
			{
				succ_selected = this.Successor.selectComment(x,y);
			}

			if (this.My_Comment != null)
			{
				this.My_Comment.selected = false;
				if (this.My_Comment.contains(x,y))
				{
					this.My_Comment.selected = true;

					return this.My_Comment;
				}
			}

			if (first_selected != null)
			{
				return first_selected;
			}
			else if (second_selected != null)
			{
				return second_selected;
			}
			else if (succ_selected != null)
			{
				return succ_selected;
			}
			else
			{
				return null; 
			}
		}
        public override void reset_number_method_expressions_run()
        {
            this.reset_this_method_expressions_run();
            if (this.Successor != null)
            {
                this.Successor.reset_number_method_expressions_run();
            }

            if (this.first_child != null)
            {
                this.Successor.reset_number_method_expressions_run();
            }

            if (this.second_child != null)
            {
                this.Successor.reset_number_method_expressions_run();
            }
        }
		// Check to see if I or my successor or chidren have empty parse trees.
		public override bool has_code()
		{
			bool im_ok = true;
			bool my_successor_ok = true;
			bool my_firstChildren_ok = true;
			bool my_secondChildren_ok = true;


			if (this.Successor != null)
			{
				my_successor_ok = this.Successor.has_code();
			}

			if (this.first_child != null)
			{
				my_firstChildren_ok = this.first_child.has_code();
			}

			if (this.second_child != null)
			{
				my_secondChildren_ok = this.second_child.has_code();
			}

			if (this.parse_tree == null)
			{
				im_ok = false;
			}

			return (im_ok && my_successor_ok && my_firstChildren_ok && my_secondChildren_ok);
		}

		public override void Show_Guids()
		{
			this.Show_Guid();
			if (this.first_child!=null)
			{
				this.first_child.Show_Guids();
			}
			if (this.second_child!=null)
			{
				this.second_child.Show_Guids();
			}
			if (this.Successor!=null)
			{
				this.Successor.Show_Guids();
			}

		}

		// Mark error if I or my successor or chidren have empty parse trees.
		public override void mark_error()
		{
			if (this.Successor != null)
			{
				this.Successor.mark_error();
			}

			if (this.first_child != null)
			{
				this.first_child.mark_error();
			}

			if (this.second_child != null)
			{
				this.second_child.mark_error();
			}

			if (this.parse_tree == null)
			{
				this.Text = "Error";
				//Runtime.parent.Show_Text_On_Error();
			}
		}

		protected void Draw_Diamond_and_Text(Avalonia.Media.DrawingContext gr,
			int x, int diamond_top, string text,
			Avalonia.Media.Pen diamond_pen,
			bool draw_text)
		{
			Avalonia.Media.Pen line_pen;

			if (this.selected)
			{
				line_pen = PensBrushes.red_pen;
			}
			else
			{
				line_pen = PensBrushes.blue_pen;
			}

			if (this.Is_Wide_Diamond() && draw_text)
			{
				// draw the + or - to expand
				gr.DrawRectangle(line_pen,
					new Avalonia.Rect(x-W/4-this.drawing_text_width/2,
					diamond_top,H/4,H/4));
				if (this.is_compressed)
				{
					gr.DrawLine(line_pen,
						new Point(x-W/4-this.drawing_text_width/2+H/8,
						diamond_top),
						new Point(x-W/4-this.drawing_text_width/2+H/8,
						diamond_top+H/4));
				}
				gr.DrawLine(line_pen,
					new Point(x-W/4-this.drawing_text_width/2,
					diamond_top+H/8),
					new Point(x-W/4-this.drawing_text_width/2+H/4,
					diamond_top+H/8));

				// Draw the ^
				gr.DrawLine(diamond_pen,new Point(x,diamond_top),
					new Point(x+W/8,diamond_top+H/8));
				gr.DrawLine(diamond_pen,new Point(x,diamond_top),
					new Point(x-W/8,diamond_top+H/8));
				// Draw the bottom ^
				gr.DrawLine(diamond_pen,new Point(x,diamond_top+H),
					new Point(x+W/8,diamond_top+7*H/8));
				gr.DrawLine(diamond_pen,new Point(x,diamond_top+H),
					new Point(x-W/8,diamond_top+7*H/8));
				// draw horizontal lines on bottom
				gr.DrawLine(PensBrushes.blue_dash_pen,
					new Point(x-this.drawing_text_width/2,
					diamond_top+7*H/8),
					new Point(x-W/8,diamond_top+7*H/8));
				gr.DrawLine(PensBrushes.blue_dash_pen,
					new Point(x + this.drawing_text_width / 2,
					diamond_top + 7 * H / 8),
					new Point(x + W / 8, diamond_top + 7 * H / 8)); 
				// draw horizontal lines on top
				gr.DrawLine(PensBrushes.blue_dash_pen,
					new Point(x-this.drawing_text_width/2,
					diamond_top+H/8),
					new Point(x-W/8,diamond_top+H/8)); 
				gr.DrawLine(PensBrushes.blue_dash_pen,
					new Point(x+this.drawing_text_width/2,
					diamond_top+H/8),
					new Point(x+W/8,diamond_top+H/8)); 


				// draw the <

				gr.DrawLine(diamond_pen,
					new Point(x-W/4-this.drawing_text_width/2,
					diamond_top+H/2),
					new Point(x-this.drawing_text_width/2,
					diamond_top+H/8)); 
				gr.DrawLine(diamond_pen,
					new Point(x-W/4-this.drawing_text_width/2,
					diamond_top+H/2),
					new Point(x-this.drawing_text_width/2,
					diamond_top+7*H/8));
				// draw the >
				gr.DrawLine(diamond_pen,
					new Point(x+this.drawing_text_width/2+W/4,
					diamond_top+H/2),
					new Point(x+this.drawing_text_width/2,
					diamond_top+H/8)); // draw right top line
				gr.DrawLine(diamond_pen,
					new Point(x+this.drawing_text_width/2+W/4,
					diamond_top+H/2),
					new Point(x+this.drawing_text_width/2,
					diamond_top+7*H/8)); // draw right bottom line
			}
			else
			{
				// draw the + or - to expand
				gr.DrawRectangle(line_pen,
					new Rect(x-W/2,diamond_top,H/4,H/4));
				if (this.is_compressed)
				{
					gr.DrawLine(line_pen,
						new Point(x-W/2+H/8,diamond_top),new Point(x-W/2+H/8,diamond_top+H/4));
				}
				gr.DrawLine(line_pen,
					new Point(x-W/2,diamond_top+H/8),new Point(x-W/2+H/4,diamond_top+H/8));

				gr.DrawLine(diamond_pen,new Point(x,diamond_top),
					new Point(x+W/2,diamond_top+H/2)); // draw top right line
				gr.DrawLine(diamond_pen,new Point(x+W/2,
					diamond_top+H/2),new Point(x,diamond_top+H)); // draw bottom right line
				gr.DrawLine(diamond_pen,new Point(x,diamond_top+H),
					new Point(x-W/2,diamond_top+H/2)); // draw bottom left line
				gr.DrawLine(diamond_pen,new Point(x-W/2,diamond_top+H/2),
					new Point(x,diamond_top)); // draw top left line
			}

			if (draw_text) 
			{

				if (this.Text.Length > 0)
				{
					if (Component.full_text)
					{
						if (this.drawing_text_width>W)
						{
							rect = new Avalonia.Rect(
								x-this.drawing_text_width/2,
								diamond_top+H/2-this.height_of_text,
								this.drawing_text_width,
								this.height_of_text*2);
						}
						else
						{
							rect = new Avalonia.Rect(
								x-this.width_of_text/2,
								diamond_top+H/2-this.height_of_text/2,
								this.width_of_text,
								this.height_of_text);
						}
					}
					else
					{
						rect = new Avalonia.Rect(x-W/2, diamond_top+(H*7)/16, W-W/8,this.height_of_text);
					}

					if (this.Text == "Error")
					{
						Avalonia.Media.FormattedText formattedtextError = new Avalonia.Media.FormattedText(
							"Error", new Avalonia.Media.Typeface("arial"), 12, Avalonia.Media.TextAlignment.Center,
							Avalonia.Media.TextWrapping.NoWrap, Avalonia.Size.Infinity);
						gr.DrawText(PensBrushes.redbrush, rect.TopLeft, formattedtextError);
					}
					else
					{
						Avalonia.Media.FormattedText formattedtextText = new Avalonia.Media.FormattedText(
							this.getDrawText(), new Avalonia.Media.Typeface("arial"), 12, Avalonia.Media.TextAlignment.Center,
							Avalonia.Media.TextWrapping.NoWrap, Avalonia.Size.Infinity);
						gr.DrawText(PensBrushes.blackbrush, rect.TopLeft, formattedtextText);
					}
				}
			}
		}

		protected bool Is_Wide_Diamond()
		{
			return (Component.full_text && this.drawing_text_width > 5*W/7);
		}

		protected void Diamond_Footprint(
			Avalonia.Media.DrawingContext gr, 
			bool draw_text,
			int buffer)
		{
			if (Component.full_text && draw_text)
			{
				int height_of_text, width_of_text=5*W/7;


				int szHeight;


				Avalonia.Media.FormattedText formattedtextYes = new Avalonia.Media.FormattedText(
					"Yes", new Avalonia.Media.Typeface("arial"), 12, Avalonia.Media.TextAlignment.Center,
					Avalonia.Media.TextWrapping.NoWrap, Avalonia.Size.Infinity);
				height_of_text = (int)Math.Ceiling(formattedtextYes.Bounds.Height);

				// loop starting at 3*W until you get on 2 lines.
				while (true) 
				{

					Avalonia.Media.FormattedText formattedtext = new Avalonia.Media.FormattedText(
						this.Text + "XXXXX", new Avalonia.Media.Typeface("arial"), 12, Avalonia.Media.TextAlignment.Center,
						Avalonia.Media.TextWrapping.Wrap, Avalonia.Size.Infinity.WithWidth(width_of_text));
					szHeight = (int)Math.Ceiling(formattedtext.Bounds.Height);

					if (szHeight<height_of_text*5/2)
					{
						break;
					}
					width_of_text = width_of_text + W/2;
				}

				if (szHeight > height_of_text*3/2 || width_of_text > 5*W/7) 
				{
					FP.left = width_of_text/2+buffer;
					FP.right = width_of_text/2+buffer;
					drawing_text_width = width_of_text;
				}
				else
				{
					drawing_text_width = 5*W/7;
				}
			}
			else
			{
				drawing_text_width = 0;
			}
		}

		public override void change_compressed(bool compressed)
		{
			this.is_compressed=compressed;
			if (this.first_child!=null)
			{
				this.first_child.change_compressed(compressed);
			}
			if (this.second_child!=null)
			{
				this.second_child.change_compressed(compressed);
			}
			if (this.Successor!=null)
			{
				this.Successor.change_compressed(compressed);
			}
		}
		public override bool child_running()
		{
			bool first_running=false, second_running=false, successor_running=false;

			if (this.running)
			{
				return true;
			}
			if (this.first_child!=null)
			{
				first_running=this.first_child.child_running();
			}
			if (this.second_child!=null)
			{
				second_running=this.second_child.child_running();
			}
			if (this.Successor!=null)
			{
				successor_running=this.Successor.child_running();
			}
			return (first_running | second_running | successor_running);
		}

		public bool have_child_running()
		{
			if (this.first_child!=null && this.first_child.child_running())
			{
				return true;
			}
			if (this.second_child!=null && this.second_child.child_running())
			{
				return true;
			}
			return false;
		}

		public abstract bool overplus (int x, int y);

		public override bool check_expansion_click (int x, int y)
		{
			if (this.overplus(x,y))
			{
				this.is_compressed = !this.is_compressed;
				return true;
			}
			if (this.first_child!=null && !this.is_compressed && this.first_child.check_expansion_click(x,y))
			{
				return true;
			}
			if (this.second_child!=null && !this.is_compressed && this.second_child.check_expansion_click(x,y))
			{
				return true;
			}
			if (this.Successor!=null)
			{
				return this.Successor.check_expansion_click(x,y);
			}
			return false;
		}	

		public override bool Called_Tab(string s)
		{
			if (this.Successor!=null && this.Successor.Called_Tab(s))
			{
				return true;
			}
			else if (this.first_child!=null && this.first_child.Called_Tab(s))
			{
				return true;
			}
			else if (this.second_child!=null && this.second_child.Called_Tab(s))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		public override void Rename_Tab(string from, string to)
		{
			if (this.first_child!=null)
			{
				this.first_child.Rename_Tab(from,to);
			}
			if (this.second_child!=null)
			{
				this.second_child.Rename_Tab(from,to);
			}
			base.Rename_Tab(from,to);
		}
		/*public override void compile_pass1(generate_interface.typ gen)
		{	
			if (this.parse_tree!=null)
			{
				interpreter_pkg.compile_pass1(this.parse_tree,this.Text,gen);
			}
			if (this.first_child!=null)
			{
				this.first_child.compile_pass1(gen);
			}
			if (this.second_child!=null)
			{
				this.second_child.compile_pass1(gen);
			}
			if (this.Successor!=null)
			{
				this.Successor.compile_pass1(gen);
			}
		}*/
        public override void collect_variable_names(System.Collections.Generic.IList<string> l,
            System.Collections.Generic.IDictionary<string, string> types)
        {
            if (this.first_child != null)
            {
                this.first_child.collect_variable_names(l,types);
            }
            if (this.second_child != null)
            {
                this.second_child.collect_variable_names(l,types);
            }
            if (this.Successor != null)
            {
                this.Successor.collect_variable_names(l,types);
            }
        }

	}
}

