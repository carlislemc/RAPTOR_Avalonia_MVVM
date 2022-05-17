using System;
using Avalonia;
using Avalonia.Media;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using RAPTOR_Avalonia_MVVM;

namespace raptor
{
	/// <summary>
	/// Summary description for Loop.
	/// </summary>
	
	[Serializable]
	public class Loop : BinaryComponent
	{
		public int bottom, end_first_connector;
		public int diamond_top = 0, after_bottom = 0;
		public int x_left, y_left, x_right, y_right;
		public int left_connector_y;
		public int right_connector_y;
		public int line_height;
		public bool light_head;
		private bool has_diamond_breakpoint;
		private String LP;
		public Component? before_Child
		{
			get 
			{
				return first_child;
			}
			set
			{
				first_child = value;
			}
		}

		public Component? after_Child
		{
			get 
			{
				return second_child;
			}
			set
			{
				second_child = value;
			}
		}

		public Loop(int height, int width, String str_name)
			: base(height, width, str_name)
		{
			this.init();
		}

		public Loop(Component Successor, int height, int width, String str_name)
			: base(Successor, height, width, str_name)
		{
			this.init();
		}

		// count the ( and ), and if you ever get more ), return true
		private static bool paren_more_right(string txt)
		{
			int paren_count = 0;
			for (int i=0; i<txt.Length; i++)
			{
				switch(txt[i]) 
				{
					case '(':
                        paren_count++;
						break;
					case ')':
					    paren_count--;
						if (paren_count==-1)
						{
							return true;
						}
						break;
				}
			}
			return false;
		}

		public Loop(SerializationInfo info, StreamingContext ctxt)
			: base(info,ctxt)
		{
			//Get the values from info and assign them to the appropriate properties
			before_Child = (Component)info.GetValue("_before_Child", typeof(Component));
			after_Child = (Component)info.GetValue("_after_Child", typeof(Component));
			bottom = (int)info.GetValue("_bottom", typeof(int));
			end_first_connector = (int)info.GetValue("_min_before_bottom", typeof(int));
			diamond_top = (int)info.GetValue("_before_bottom", typeof(int));
			after_bottom = (int)info.GetValue("_after_bottom", typeof(int));
			x_left = (int)info.GetValue("_x_left", typeof(int));
			y_left = (int)info.GetValue("_y_left", typeof(int));
			x_right = (int)info.GetValue("_x_right", typeof(int));
			y_right = (int)info.GetValue("_y_right", typeof(int));
			left_connector_y = (int)info.GetValue("_left_connector_y", typeof(int));
			right_connector_y = (int)info.GetValue("_right_connector_y", typeof(int));
			line_height = (int)info.GetValue("_line_height", typeof(int));
			LP = (string)info.GetValue("_LP", typeof(string));
			if (Component.negate_loops && this.Text!=null && this.Text!="")
			{
				if (this.Text.Length<6 ||
					this.Text.Substring(0,5)!="not (" ||
					this.Text[this.Text.Length-1]!=')' ||
					paren_more_right(this.Text.Substring(5,this.Text.Length-6)))
				{
					this.Text = "not (" + this.Text + ")";
				}
				else
				{
					// remove the "not ("
					this.Text = this.Text.Substring(5,this.Text.Length-6);
				}
			}
			result = interpreter_pkg.conditional_syntax(this.Text,this);

			if (result.valid)
			{
				this.parse_tree = result.tree;
			}
			else
			{
				if (!Component.warned_about_error && this.Text!="")
				{
					MessageBoxClass.Show("Unknown error: \n" +
						this.Text + "\n" +
						"is not recognized.");
					Component.warned_about_error = true;
				}
				this.Text = "";
			}
		}

		//Serialization function.
		public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
		{
			//You can use any custom name for your name-value pair. But make sure you
			// read the values with the same name. For ex:- If you write EmpId as "EmployeeId"
			info.AddValue("_before_Child", before_Child);
			info.AddValue("_after_Child", after_Child);
			info.AddValue("_bottom", bottom);
			info.AddValue("_min_before_bottom", end_first_connector);
			info.AddValue("_before_bottom", diamond_top);
			info.AddValue("_after_bottom", after_bottom);
			info.AddValue("_x_left", x_left);
			info.AddValue("_y_left", y_left);
			info.AddValue("_x_right", x_right);
			info.AddValue("_y_right", y_right);
			info.AddValue("_left_connector_y", left_connector_y);
			info.AddValue("_right_connector_y", right_connector_y);
			info.AddValue("_line_height", line_height);
			info.AddValue("_LP", LP);
			base.GetObjectData(info,ctxt);
		}

		public override bool break_now()
		{
			return (this.has_breakpoint && this.light_head) ||
				(this.has_diamond_breakpoint && !this.light_head);
		}

		public override void Toggle_Breakpoint(int x, int y)
		{
			if (this.over_Diamond(x,y))
			{
				this.has_diamond_breakpoint = 
					!this.has_diamond_breakpoint;
			}
			else
			{
				this.has_breakpoint = !this.has_breakpoint;
			}
		}

		public override void Clear_Breakpoints()
		{
			this.has_breakpoint = false;
			this.has_diamond_breakpoint = false;
			if (this.before_Child!=null)
			{
				this.before_Child.Clear_Breakpoints();
			}
			if (this.after_Child!=null)
			{
				this.after_Child.Clear_Breakpoints();
			}
			if (this.Successor!=null)
			{
				this.Successor.Clear_Breakpoints();
			}
		}

		// warning, the predecessor of a loop's first after child
		// is its last before child.
		public override Component Find_Predecessor(Component c)
		{
			Component pred_before, pred_after;
			if (this.before_Child==c)
			{
				return this;
			}
			if (this.after_Child==c)
			{
				if (this.before_Child!=null)
				{
					return this.before_Child.find_end();
				}
				return this;
			}
			if (this.before_Child!=null)
			{
				pred_before=this.before_Child.Find_Predecessor(c);
				if (pred_before!=null)
				{
					return pred_before;
				}
			}
			if (this.after_Child!=null)
			{
				pred_after=this.after_Child.Find_Predecessor(c);
				if (pred_after!=null)
				{
					return pred_after;
				}
			}
			return base.Find_Predecessor(c);
		}

		public override void draw(Avalonia.Media.DrawingContext gr, int x, int y)
		{
			int right, after_left, before_right, after_right;
			bool draw_text;

			X = x;
			Y = y;
			Avalonia.Media.FormattedText formattedtextYes = new Avalonia.Media.FormattedText(
				"Yes", new Avalonia.Media.Typeface("arial"), 12, Avalonia.Media.TextAlignment.Center,
				Avalonia.Media.TextWrapping.NoWrap, Avalonia.Size.Infinity);
			Avalonia.Media.FormattedText formattedtextNo = new Avalonia.Media.FormattedText(
				"No", new Avalonia.Media.Typeface("arial"), 12, Avalonia.Media.TextAlignment.Center,
				Avalonia.Media.TextWrapping.NoWrap, Avalonia.Size.Infinity);
			height_of_text = (int)Math.Ceiling(formattedtextYes.Bounds.Height);

			Avalonia.Media.FormattedText formattedtext = new Avalonia.Media.FormattedText(
				this.Text + "XX", new Avalonia.Media.Typeface("arial"), 12, Avalonia.Media.TextAlignment.Center,
				Avalonia.Media.TextWrapping.NoWrap, Avalonia.Size.Infinity);
			width_of_text = (int)Math.Ceiling(formattedtext.Bounds.Width);

			


			int length_of_yesStr = (int)Math.Ceiling(formattedtextYes.Bounds.Width);
			int length_of_noStr = (int)Math.Ceiling(formattedtextNo.Bounds.Width);

			//gr.SmoothingMode=System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			if ((this.scale <= .4) || (this.head_heightOrig < 10))
			{
				draw_text = false;
			}
			else
			{
				draw_text = Component.text_visible;
			}

			Avalonia.Media.Pen head_pen = PensBrushes.blue_pen;
			Avalonia.Media.Pen diamond_pen = PensBrushes.blue_pen;
			Avalonia.Media.Pen line_pen = PensBrushes.blue_pen;

			// if the loop is selected make it red and select its children
			if (this.selected)
			{
				head_pen = PensBrushes.red_pen;
				diamond_pen = PensBrushes.red_pen;
				line_pen = PensBrushes.red_pen;
			}

				//Make diamond green for running
			else if (this.running && this.light_head==false)
			{
				head_pen = PensBrushes.blue_pen;
				diamond_pen = PensBrushes.chartreuse_pen;
				line_pen = PensBrushes.blue_pen;
			}
			
				//Make head green for running
			else if (this.running && this.light_head)
			{
				head_pen = PensBrushes.chartreuse_pen;
				diamond_pen = PensBrushes.blue_pen;
				line_pen = PensBrushes.blue_pen;
			}
			else if (this.is_compressed && this.have_child_running())
			{
				head_pen = PensBrushes.chartreuse_pen;
				diamond_pen = PensBrushes.chartreuse_pen;
				line_pen = PensBrushes.blue_pen;
			}

			if (!Component.USMA_mode)
			{
				// Draw the head
				gr.DrawGeometry(Brushes.Transparent, head_pen, new EllipseGeometry(
					new Avalonia.Rect(x - W / 2, y, W, H)));
				end_first_connector = Y+H+H/2;
			}
			else
			{   gr.DrawGeometry(Brushes.Transparent, head_pen, 
				   new EllipseGeometry(new Avalonia.Rect(x - W / 8, y, W / 4, W / 4)));
				end_first_connector = Y+W/4+H/2;
			}

			
			if (this.has_breakpoint)
			{
				StopSign.Draw(gr,x-W/2-W/6-2,y, W/6);
			}

			// Draw arrow at top of the head oval
			if (!Component.USMA_mode)
			{
				gr.DrawLine(head_pen,new Point(x,y),new Point(x+CL/4,y+CL/4)); // draw down arrow
				gr.DrawLine(head_pen,new Point(x, y), new Point(x + CL / 4, y - CL / 4)); // draw up arrow
			}

			//Get the proper width based on the children's width
			// x_left is the location of the vertical connector leaving the loop
			// x_right is the location of the vertical connector going back to the top

			if ((before_Child != null) && (after_Child != null) && !this.is_compressed)
			{
				before_right = before_Child.FP.right;
				after_right = after_Child.FP.right;
				right = (before_right > after_right) ? before_right:after_right;
				x_right = x + W/2 + right;
 
				after_left = after_Child.FP.left;
				x_left = x - W/2 - after_left;
			}
			else if (before_Child != null && !this.is_compressed)
			{
				before_right = before_Child.FP.right;
				x_right = x + W/2 + before_right;
 
				x_left = x - W/2 - W/2;
			}
			else if (after_Child != null && !this.is_compressed)
			{
				after_right = after_Child.FP.right;
				x_right = x + W/2 + after_right;
 
				after_left = after_Child.FP.left;
				x_left = x - W/2 - after_left;
			}
			else
			{
				x_right = x + W/2 + W/2;
				x_left = x - W/2 - W/2;
			}

			if (draw_text && this.Is_Wide_Diamond())
			{
				int temp_right = x+this.drawing_text_width/2+W/2+W/4;
				int temp_left = x-this.drawing_text_width/2-W/2-W/4;

				if (temp_right > x_right)
				{
					x_right = temp_right;
				}
				if (temp_left < x_left)
				{
					x_left = temp_left;
				}
			}

			//Because there is a before child, draw a connector from the head to the
			//before child and then from the before child to the diamond.
			int start_first_connector;  // top y coordinate of first connector
			if (!Component.USMA_mode)
			{
				start_first_connector = y+H;
			}
			else
			{
				start_first_connector = y+W/4;
			}
			
			// draw connector line 
			gr.DrawLine(line_pen,new Point(x,start_first_connector),new Point(x,end_first_connector)); 
			// draw left side of arrow
			gr.DrawLine(line_pen,new Point(x, end_first_connector), new Point(x - CL / 4, end_first_connector - CL / 4)); 
			// draw right side of arrow
			gr.DrawLine(line_pen,new Point(x, end_first_connector), new Point(x + CL / 4, end_first_connector - CL / 4)); 


			if (before_Child != null && !this.is_compressed)
			{
				diamond_top = end_first_connector + before_Child.FP.height+CL;

				//Draw connector from head to before_child

				// Draw the before_Child graphic
				before_Child.draw(gr,x,end_first_connector);
				gr.DrawLine(line_pen,new Point(x,end_first_connector+before_Child.FP.height),new Point(x,diamond_top)); // draw connector line to diamond
				gr.DrawLine(line_pen,new Point(x, diamond_top), new Point(x - CL / 4, diamond_top - CL / 4)); // draw left side of arrow
				gr.DrawLine(line_pen,new Point(x, diamond_top), new Point(x + CL / 4, diamond_top - CL / 4)); // draw right side of arrow
			}
				//Because there is not a before child, draw a connector from the head 
				//to the diamond.
			else
			{
				diamond_top = end_first_connector;
			}

			// Draw the diamond shape
			this.Draw_Diamond_and_Text(gr,x,diamond_top,
				this.Text, diamond_pen,
				draw_text);

			if (this.has_diamond_breakpoint)
			{
				StopSign.Draw(gr,x-W/2-W/6-2,diamond_top, W/6);
			}

			// Draw connector from diamond
			gr.DrawLine(line_pen,new Point(x, diamond_top + H), new Point(x, diamond_top + H + H / 2)); // draw connector line to top of before child
			gr.DrawLine(line_pen,new Point(x, diamond_top + H + H / 2), new Point(x - CL / 4, diamond_top + H + H / 2 - CL / 4)); // draw left side of arrow
			gr.DrawLine(line_pen,new Point(x, diamond_top + H + H / 2), new Point(x + CL / 4, diamond_top + H + H / 2 - CL / 4)); // draw right side of arrow

			//Because there is an after child, draw the after child and then a connector from the 
			//after child to the bottom.
			if (after_Child != null && !this.is_compressed)
			{
				after_bottom = diamond_top + H + H/2 + after_Child.FP.height+CL;
				
				// Draw the after_Child graphic
				after_Child.draw(gr,x,diamond_top+H+H/2);

				//Draw connector from after_child to the bottom
				gr.DrawLine(line_pen,new Point(x, diamond_top + H + H / 2 + after_Child.FP.height), new Point(x, after_bottom)); // draw connector line
				gr.DrawLine(line_pen,new Point(x, after_bottom), new Point(x - CL / 4, after_bottom - CL / 4)); // draw left side of arrow
				gr.DrawLine(line_pen,new Point(x, after_bottom), new Point(x + CL / 4, after_bottom - CL / 4)); // draw right side of arrow
				
			}
				//Because there is not an after child, draw the connector from the diamond
				//to the bottom.
			else
			{
				after_bottom = diamond_top + H + H/2;
				
			}

			//draw line from bottom center to right, then up, then left to head
			gr.DrawLine(line_pen,new Point(x,after_bottom), new Point(x_right,after_bottom)); // draw connector line to right
			if (!Component.USMA_mode)
			{
				gr.DrawLine(line_pen,new Point(x_right,after_bottom), new Point(x_right,y+H/2)); // draw connector line up
				gr.DrawLine(line_pen,new Point(x_right,y+H/2), new Point(x+W/2,y+H/2)); // draw connector line to head
				gr.DrawLine(line_pen,new Point(x+W/2,y+H/2),new Point(x+W/2+CL/4,y+H/2-CL/4)); // draw top side of arrow
				gr.DrawLine(line_pen,new Point(x + W / 2, y + H / 2), new Point(x + W / 2 + CL / 4, y + H / 2 + CL / 4)); // draw bottom side of arrow
			}
			else
			{
				gr.DrawLine(line_pen,new Point(x_right,after_bottom),new Point(x_right,y+W/8)); // draw connector line up
				gr.DrawLine(line_pen,new Point(x_right, y + W / 8), new Point(x + W / 8, y + W / 8)); // draw connector line to head
				gr.DrawLine(line_pen,new Point(x + W / 8, y + W / 8), new Point(x + W / 8 + CL / 4, y + W / 8 - CL / 4)); // draw top side of arrow
				gr.DrawLine(line_pen,new Point(x + W / 8, y + W / 8), new Point(x + W / 8 + CL / 4, y + W / 8 + CL / 4)); // draw bottom side of arrow
			}

			// Draw left line from the diamond 
			if (draw_text && this.Is_Wide_Diamond())
			{
				gr.DrawLine(line_pen,
					new Point(x_left,diamond_top+H/2),
					new Point(x - this.drawing_text_width / 2 - W / 4, diamond_top + H / 2)); // draw left line
			}
			else
			{
				gr.DrawLine(line_pen,
					new Point(x_left,diamond_top+H/2),
					new Point(x-W/2,diamond_top+H/2)); // draw left line
			}
			gr.DrawLine(line_pen,new Point(x_left,diamond_top+H/2),new Point(x_left,after_bottom+H/2)); // draw down line
			gr.DrawLine(line_pen,new Point(x_left,after_bottom+H/2),new Point(x,after_bottom+H/2)); // draw line back to right	
				

			if (this.Successor != null)
			{
				// draw connector line to successor
				gr.DrawLine(line_pen,new Point(x,after_bottom+H/2),new Point(x,after_bottom+H/2+CL)); 
				// draw left side of arrow
				gr.DrawLine(line_pen,new Point(x,after_bottom+H/2+CL),new Point(x-CL/4,after_bottom+H/2+CL-CL/4)); 
				// draw right side of arrow
				gr.DrawLine(line_pen,new Point(x,after_bottom+H/2+CL),new Point(x + CL / 4,after_bottom +H/2+CL-CL/4)); 
				Successor.draw(gr,x,after_bottom+H/2+CL);
			}

			if (this.W > 30)
			{
				LP = "Loop";
			}
			else
			{
				LP = "";
			}
			if (draw_text)
			{

				// swap Yes and No for reversed loop logic
				if (!Component.reverse_loop_logic)
				{
					if (this.Is_Wide_Diamond())
					{
						gr.DrawText(PensBrushes.blackbrush, new Point(x - this.drawing_text_width / 2 - W / 4 - length_of_yesStr / 2,
							diamond_top + H / 2 - 4), formattedtextYes);
					}
					else
					{
						gr.DrawText(PensBrushes.blackbrush, new Point(x - W / 2 - length_of_yesStr,
							diamond_top + H / 2 - 4), formattedtextYes);

					}
					gr.DrawText(PensBrushes.blackbrush, new Point(x + length_of_noStr,
						diamond_top + H + 5), formattedtextNo);

				}
				else
				{
					if (this.Is_Wide_Diamond())
					{
						gr.DrawText(PensBrushes.blackbrush, new Point(x - this.drawing_text_width / 2 - W / 4 - length_of_noStr / 2,
							diamond_top + H / 2 - 4), formattedtextNo);
					}
					else
					{
						gr.DrawText(PensBrushes.blackbrush, new Point(x - W / 2 - length_of_noStr,
							diamond_top + H / 2 - 4), formattedtextNo);
					}
					gr.DrawText(PensBrushes.blackbrush, new Point(x + length_of_yesStr,
						diamond_top + H + 5), formattedtextYes);
				}

				// draw "Loop" inside oval if not USMA mode
				if (!Component.USMA_mode)
				{
					Avalonia.Media.FormattedText formattedtextLP = new Avalonia.Media.FormattedText(
						LP, new Avalonia.Media.Typeface("arial"), 12, Avalonia.Media.TextAlignment.Center,
						Avalonia.Media.TextWrapping.NoWrap, Avalonia.Size.Infinity);
					gr.DrawText(PensBrushes.blackbrush, new Point(x - W / 2, Y + (H * 6) / 16), formattedtextLP);
				}
			}
			if (draw_text) 
			{
				base.draw(gr,x,y);
			}
		}

		public override bool contains(int x, int y)
		{
			int Diamond_bottom = diamond_top+H;
			Diamond_bottom = Diamond_bottom - H/2;

			bool bounded_x;
			bool bounded_y;
			if (!Component.USMA_mode)
			{
				bounded_x = Math.Abs(x-X) <= W/2;
				bounded_y = Math.Abs(y-(Y+H/2)) <= H/2;
			}
			else
			{
				bounded_x = Math.Abs(x-X) <= W/8;
				bounded_y = Math.Abs(y-(Y+W/8)) <= W/8;
			}

			bool bounded_Diamondy = Math.Abs(y-(Diamond_bottom)) <= H/2;
			return ((bounded_x && bounded_y)||
				(this.Diamond_Bounded_X(x) && bounded_Diamondy));
		}

		public override bool overplus (int x, int y)
		{
			bool bounded_y = Math.Abs(y-(diamond_top+H/8)) <= H/8;
			bool bounded_x;
			if (this.Is_Wide_Diamond())
			{
				bounded_x = Math.Abs(x-(this.X-W/4-this.drawing_text_width/2+H/8)) <= H/8;
			}
			else
			{
				bounded_x = Math.Abs(x-(this.X-W/2+H/8)) <= H/8;
			}
			return bounded_y && bounded_x;
		}

		// added a small amount of allowed overlap so that
		// printing can always find a page break
		public override bool contains(Avalonia.Rect rec)
		{
			int Diamond_bottom = diamond_top;

			Avalonia.Rect my_rec;
			if (!Component.USMA_mode)
			{
				my_rec = new Avalonia.Rect(X-W/2+2,Y,W-4,H);
			}
			else
			{
				my_rec = new Avalonia.Rect(X-W/8+1,Y,W/4-2,H);
			}
			Avalonia.Rect my_rec2 = 
				new Avalonia.Rect(
				X-this.Diamond_Width()/2+2,
				Diamond_bottom,
				this.Diamond_Width()-4,H);
			return rec.Intersects(my_rec) || rec.Intersects(my_rec2);
		}


		public bool over_Diamond(int x, int y)
		{
			int Diamond_bottom = diamond_top+H;
			Diamond_bottom = Diamond_bottom - H/2;

			bool bounded_x = Math.Abs(x-X) <= W/2;
			bool bounded_Diamondy = Math.Abs(y-(Diamond_bottom)) <= H/2;
			return (bounded_x && bounded_Diamondy);
		}

		// Is (x, y) over a line connected to an object?
		public override bool overline(int x, int y, int connector_y)
		{
            bool over_left_x = Math.Abs(x - x_left) < this.proximity;
            bool over_left_y = (y < after_bottom + H / 2) && (y > this.diamond_top + H / 2);
			if (this.Successor != null)
			{
				bool over_x = Math.Abs(x-X) < this.proximity;
				bool over_y = (y < after_bottom+H/2+CL) && (y > this.after_bottom);
				return (over_x && over_y) || (over_left_x && over_left_y);
			}
			else
			{
				bool over_x = Math.Abs(x-X) < this.proximity;
				bool over_y = (y < connector_y) && (y > this.after_bottom+H/2);
                return (over_x && over_y) || (over_left_x && over_left_y);
            }
		}

		// Is (x, y) over a line connected to the before test?
		public bool overbefore(int x, int y, int connector_y)
		{
			bool over_x = Math.Abs(x-X) < this.proximity;

			bool over_y;
			if (!Component.USMA_mode && Component.Current_Mode!=Mode.Expert)
			{
				over_y = (y < end_first_connector) && (y > Y+H);
			}
			else
			{
				// over_y = (y < end_first_connector) && (y > Y+W/4);
                over_y = false;
			}
			return over_x && over_y;
		}

		// Is (x, y) over a line connected to the after test?
		public bool overafter(int x, int y, int connector_y)
		{
			bool over_x = Math.Abs(x-X) < this.proximity;

			bool over_y = (y < diamond_top+H+H/2) && (y > diamond_top+H);
			return over_x && over_y;
		}

		//Scale the object
		public override void Scale(float new_scale)
		{

			H = (int) Math.Round(this.scale*this.head_heightOrig);
			W = (int) Math.Round(this.scale*this.head_widthOrig);
			


			if (this.before_Child != null)
			{
				this.before_Child.scale = this.scale;
				this.before_Child.Scale(new_scale);
			}

			if (this.after_Child != null)
			{
				this.after_Child.scale = this.scale;
				this.after_Child.Scale(new_scale);
			}
			if (this.Successor != null)
			{
				this.Successor.scale = this.scale;
				this.Successor.Scale(new_scale);
			}
			base.Scale(new_scale);
		}

		// What are the left and right widths and height of the object 
		// (including its children and successors)? 
		public override void footprint(Avalonia.Media.DrawingContext gr)
		{
			this.init();
			this.Diamond_Footprint(gr, this.scale > 0.4, W/2+W/4);
			int leftbefore, leftafter, rightbefore, rightafter;
			int temp_FP_left=0, temp_FP_right=0;

			if (!Component.USMA_mode)
			{
				FP.height = H + H/2 + H + H/2 + H/2;
			}
			else
			{
				FP.height = W/4 + H/2 + H + H/2 + H/2;
			}

			if ((after_Child != null) && (before_Child != null) && !this.is_compressed)
			{
				this.before_Child.footprint(gr);
				this.after_Child.footprint(gr);
				leftbefore = before_Child.FP.left;
				leftafter = after_Child.FP.left;
				temp_FP_left = ((leftbefore>leftafter)?leftbefore:leftafter)+W/2;
				rightbefore = before_Child.FP.right;
				rightafter = after_Child.FP.right;
				temp_FP_right = ((rightbefore>rightafter)?rightbefore:rightafter)+W/2;
				FP.height += before_Child.FP.height + after_Child.FP.height + CL + CL;
			}
			else if (before_Child != null && !this.is_compressed)
			{
				this.before_Child.footprint(gr);
				temp_FP_left = before_Child.FP.left + W/2;
				temp_FP_right = before_Child.FP.right + W/2;
				FP.height += before_Child.FP.height + CL;
			}
			else if (after_Child != null && !this.is_compressed)
			{
				this.after_Child.footprint(gr);
				temp_FP_left = after_Child.FP.left + W/2;
				temp_FP_right = after_Child.FP.right + W/2;
				FP.height += after_Child.FP.height + CL;
			}

			if (temp_FP_left > FP.left)
			{
				FP.left = temp_FP_left;
			}
			if (temp_FP_right > FP.right)
			{
				FP.right = temp_FP_right;
			}

			if (Successor != null)
			{
				Successor.footprint(gr);
			
				if (FP.left < Successor.FP.left) // successor has larger left footprint
				{
					FP.left = Successor.FP.left;
				}
				if (FP.right < Successor.FP.right) // successor has larger right footprint
				{
					FP.right = Successor.FP.right;
				}
				FP.height = FP.height + CL + Successor.FP.height;
			}
		}

		public override void init()
		{
			FP.left = W;
			FP.right = W;
			FP.height = 2*H+H/2+H/2+H/2;
		}

		// insert clipboard at x,y -- or just test if insertion point
		// if newObj is null
		public override bool insert(Component newObj, int x, int y, int connector_y)
		{
			bool added=false;

			Component end_clipboard;

			if (overline(x,y,connector_y))
			{
				if (newObj != null) 
				{
					end_clipboard = newObj.find_end();

					newObj.set_parent_info(this.is_child,this.is_beforeChild,
						this.is_afterChild,this.parent);
					end_clipboard.Successor = this.Successor;
					this.Successor = newObj;
				}
				return true;
			}
			else if (overbefore(x,y,connector_y)) 
			{
				if (!this.is_compressed)
				{
					if (newObj != null) 
					{
						newObj.set_parent_info(true,true,
							false,this);
						if (this.before_Child != null)
						{
							end_clipboard = newObj.find_end();
							end_clipboard.Successor = this.before_Child;
						}
						this.before_Child = newObj;
					}
					return true;
				}
				else
				{
					if (newObj != null)
					{
						MessageBoxClass.Show("Can't insert in collapsed symbol");
					}
					return false;
				}
			}
			else if (overafter(x,y,connector_y))
			{
				if (!this.is_compressed)
				{
					if (newObj != null) 
					{

						newObj.set_parent_info(true,false,
							true,this);
						if (this.after_Child != null)
						{
							end_clipboard = newObj.find_end();
							end_clipboard.Successor = this.after_Child;
						}
						this.after_Child = newObj;
					}
					return true;
				}
				else
				{
					if (newObj != null)
					{
						MessageBoxClass.Show("Can't insert in collapsed symbol");
					}
					return false;
				}
			}
			else
			{	
				if (this.before_Child != null)
				{
					added = this.before_Child.insert(newObj, x, y, diamond_top);
				}
				if (!added && this.after_Child != null)
				{
					added = this.after_Child.insert(newObj, x, y, after_bottom);
				}
				if (!added && this.Successor != null)
				{
					added = this.Successor.insert(newObj, x, y, connector_y);
				}
			}
			return added;
		}

		// If (x, y) is over the object, delete it?
		public override bool delete()
		{
			bool deleted_successor = false;
			bool deleted_before = false;
			bool deleted_after = false;

			Component end_selection;
			if (this.Successor != null)
			{
				// if my successor is selected, neither of my kids are
				if (this.Successor.editable_selected())
				{
					end_selection = this.Successor.find_selection_end();
	
					if (end_selection.Successor != null)
					{
						this.Successor = end_selection.Successor;
						end_selection.Successor=null;
					}
					else
					{
						this.Successor = null;
					}
					return true;
				}
				else
				{
					deleted_successor = this.Successor.delete();
				}
			}
			if (this.before_Child != null)
			{
				if (this.before_Child.selected)
				{
					end_selection = this.before_Child.find_selection_end();
	
					if (end_selection.Successor != null)
					{
						this.before_Child = end_selection.Successor;
						end_selection.Successor=null;
					}
					else
					{
						this.before_Child = null;
					}
					return true;
				}
				else
				{
					deleted_before = this.before_Child.delete();
				}
			}
			
			if (this.after_Child != null)
			{
				if (this.after_Child.selected)
				{
					end_selection = this.after_Child.find_selection_end();
	
					if (end_selection.Successor != null)
					{
						this.after_Child = end_selection.Successor;
						end_selection.Successor=null;
					}
					else
					{
						this.after_Child = null;
					}
					return true;
				}
				else
				{
					deleted_after = this.after_Child.delete();
				}
			}

			return (deleted_successor || deleted_before || deleted_after);
		}


		// Get the text from a pop-up dialog and then set it?
		public override string getText(int x, int y)
		{
			string txt = "";
			string txt_successor = "";
			string txt_before = "";
			string txt_after = "";

			if (contains(x,y) && !this.over_Diamond(x,y))
			{
				return "Loop";
			}

			if (this.over_Diamond(x,y))
			{
				return this.Text;
			}
			else
			{
				if (this.Successor != null)
				{
					txt_successor = this.Successor.getText(x,y);
				}
				if (this.before_Child != null && !this.is_compressed)
				{
					txt_before = this.before_Child.getText(x,y);
				}
				if (this.after_Child != null && !this.is_compressed)
				{
					txt_after = this.after_Child.getText(x,y);
				}
			}
			txt = txt_successor + txt_before + txt_after;
			return txt;
		}
		
		
		// Get the text from a pop-up dialog and then set it?
		public override bool setText(int x, int y)
		{
			bool successor_settext = false;
			bool before_settext = false;
			bool after_settext = false;

			bool textset = false;
			//if (over_Diamond(x,y))
			if (contains(x,y))
			{
				//Control_Dlg CD = new Control_Dlg(this,form,true);
				//CD.ShowDialog();
				textset = true;
				return textset;
			}
	
			if (this.Successor != null)
			{
				successor_settext = this.Successor.setText(x,y);
			}

			if (this.before_Child != null && !this.is_compressed)
			{
				before_settext = this.before_Child.setText(x,y);
			}
			if (this.after_Child != null && !this.is_compressed)
			{
				after_settext = this.after_Child.setText(x,y);
			}
			
			return (successor_settext || before_settext || after_settext);
		}

		public override Component Clone()
		{
			Loop Result = (Loop) base.Clone();
		
			if (this.before_Child != null)
			{
				Result.before_Child = this.before_Child.Clone();
				Result.before_Child.set_parent_info(true,true,false,Result);
			}

			if (this.after_Child != null)
			{
				Result.after_Child = this.after_Child.Clone();
				Result.after_Child.set_parent_info(true,false,true,Result);
			}
			return Result;
		}

		public override Component cut()
		{
			Component? cut_before = null;
			Component? cut_after = null;
			Component? cut_successor = null;
			Component? start_selection, end_selection;
			if (this.Successor != null)
			{
				// if my successor is selected, neither of my kids are
				if (this.Successor.editable_selected())
				{
					start_selection = this.Successor;
					end_selection = this.Successor.find_selection_end();
	
					if (end_selection.Successor != null)
					{
						this.Successor = end_selection.Successor;
						end_selection.Successor=null;
					}
					else
					{
						this.Successor = null;
					}
					start_selection.reset();
					return start_selection;
				}
				else
				{
					cut_successor = this.Successor.cut();
					if (cut_successor!=null)
                    {
						return cut_successor;
                    }
				}
			}
			if (this.before_Child != null)
			{
				if (this.before_Child.selected)
				{
					start_selection = this.before_Child;
					end_selection = this.before_Child.find_selection_end();
	
					if (end_selection.Successor != null)
					{
						this.before_Child = end_selection.Successor;
						end_selection.Successor=null;
					}
					else
					{
						this.before_Child = null;
					}
					start_selection.reset();
					return start_selection;
				}
				else
				{
					cut_before = this.before_Child.cut();
					if (cut_before != null)
					{
						return cut_before;
					}
				}
			}
			
			if (this.after_Child != null)
			{
				if (this.after_Child.selected)
				{
					start_selection = this.after_Child;
					end_selection = this.after_Child.find_selection_end();
	
					if (end_selection.Successor != null)
					{
						this.after_Child = end_selection.Successor;
						end_selection.Successor=null;
					}
					else
					{
						this.after_Child = null;
					}
					start_selection.reset();
					return start_selection;
				}
				else
				{
					cut_after = this.after_Child.cut();
					if (cut_after != null)
					{
						return cut_successor;
					}
				}
			}
			
			return null;
		}

		// copy selected objects
		public override Component? copy()
		{
			if (this.selected)
			{
				return base.copy();
			}
			else
			{
				if (this.before_Child != null)
				{
					Component result = this.before_Child.copy();
					if (result !=null) 
					{
						return result;
					}
				}
				if (this.after_Child != null)
				{
					Component result = this.after_Child.copy();
					if (result!=null) 
					{
						return result;
					}
				}
				if (this.Successor !=null)
				{
					return this.Successor.copy();
				}
				else
				{
					return null;
				}
			}
		}

		// the first thing to execute is either the first of the
		// before child, or else the exit when part (itself)
		public override Component First_Of()
		{
			this.light_head = true;
			return this;
			//if (this.before_Child!=null) 
			//{
			//	return this.before_Child.First_Of();
			//}
			//else
			//{
			//	return this;
			//}
		}
		/*public override void Emit_Code(generate_interface.typ gen)
		{	
            object o;
            parse_tree.boolean_expression be = this.parse_tree as parse_tree.boolean_expression;
            if (Component.reverse_loop_logic)
            {
                o = gen.Loop_Start(this.before_Child == null, true);
            }
            else
            {
                bool negated = be.top_level_negated();
                o = gen.Loop_Start(this.before_Child == null, 
                    negated);
                if (negated)
                {
                    be = be.remove_negation();
                }
            }
			if (this.before_Child!=null)
			{
				this.before_Child.Emit_Code(gen);
			}
            gen.Loop_Start_Condition(o);

			if (this.parse_tree!=null)
			{
				interpreter_pkg.emit_code(be,this.Text,gen);
			}
            gen.Loop_End_Condition(o);
			if (this.after_Child!=null)
			{
				this.after_Child.Emit_Code(gen);
			}
            gen.Loop_End(o);

			if (this.Successor!=null)
			{
				this.Successor.Emit_Code(gen);
			}
		}*/

	}
}
