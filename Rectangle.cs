using System;

using System.Runtime.Serialization;
using System.Diagnostics;
using RAPTOR_Avalonia_MVVM;
using Avalonia;
using RAPTOR_Avalonia_MVVM.Views;

namespace raptor
{
	/// <summary>
	/// Summary description for Rectangle.
	/// </summary>
	/// 
	[Serializable]
	public class Rectangle : Component
	{
		public enum Kind_Of {Assignment, Call};
		public Kind_Of kind = Kind_Of.Assignment;


		public Rectangle(int height, int width, String str_name, Kind_Of the_kind)
			: base(height, width, str_name)
		{
			this.init();
			this.kind = the_kind;
		}

		public Rectangle(Component Successor, int height, int width, String str_name, Kind_Of the_kind)
			: base(Successor, height, width, str_name)
		{
			this.init();
			this.kind = the_kind;
		}

		public Rectangle(SerializationInfo info, StreamingContext ctxt)
			: base(info,ctxt)
		{
			int index1,index2;
			if (this.incoming_serialization_version >= 7)
			{
				kind = (Kind_Of)info.GetValue("_kind", typeof(Kind_Of));
			}
			else
			{
				index1 = this.Text.IndexOf("=");
				if (index1>0)
				{
					this.kind=Kind_Of.Assignment;
					index2 = this.Text.IndexOf(":=");
					// if no :=, replace = with :=
					if (index2<=0)
					{
						this.Text = this.Text.Substring(0,index1) +
							":=" + this.Text.Substring(index1+1,
							this.Text.Length-(index1+1));
					}
				}
				else
				{
					this.kind=Kind_Of.Call;
				}
			}
			result = interpreter_pkg.statement_syntax(this.Text,
				(this.kind==Kind_Of.Call));
			if (result.valid)
			{
				this.parse_tree = result.tree;
			}
			else
			{
				if (!Component.warned_about_error && this.Text!="")
				{
					MessageBoxClass.Show("Error: \n" +
						this.Text + "\n" +
						"is not recognized.  Perhaps a DLL is missing?\n" +
						"Close RAPTOR, save the DLL and then reopen");
					Component.warned_about_error = true;
				}
				this.Text = "";
			}
		}
		public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
		{
			base.GetObjectData(info,ctxt);
			info.AddValue("_kind", this.kind);
		}

		public override bool contains(int x, int y)
		{
			if (this.kind==Kind_Of.Assignment)
			{
				return base.contains(x,y);
			}
			// add a bit for the =>
			if (base.contains(x,y))
			{
				return true;
			}
			Avalonia.Rect rec;
			
			if (drawing_text_width>W)
			{
				rec = new Avalonia.Rect(
					this.X+this.drawing_text_width/2,
					this.Y+this.H/4,
					this.W/4,
					this.H/2);
			}
			else
			{
				rec = new Avalonia.Rect(
					this.X+this.W/2,
					this.Y+this.H/4,
					this.W/4,
					this.H/2);
			}
			return rec.Contains(new Point(x,y));
		}




		//Scale the object
		public override void Scale(float new_scale)
		{

			H = (int) Math.Round(this.scale*this.head_heightOrig);
			W = (int) Math.Round(this.scale*this.head_widthOrig);
			

			if (this.Successor != null)
			{
				this.Successor.scale = this.scale;
				this.Successor.Scale(new_scale);
			}
			base.Scale(new_scale);
		}

		private string getText(bool unicode)
		{
			int index = this.Text.IndexOf(":=");

			if (index>0)
			{
				if (!Component.USMA_mode)
				{
					if (unicode)
					{
						return this.Text.Substring(0,index) +
							' ' + Component.assignmentSymbol + ' ' +
							this.Text.Substring(index+2,
							this.Text.Length-(index+2));
					}
					else
					{
						return "Set " + this.Text.Substring(0,index) +
							" to " +
							this.Text.Substring(index+2,
							this.Text.Length-(index+2));
					}
				}
				else
				{
					return this.Text.Substring(0,index) +
						" = " +
						this.Text.Substring(index+2,
						this.Text.Length-(index+2));
				}

			}
			else
			{
				return this.Text;
			}
		}

		// Get the text from a pop-up dialog and then set it?
		public override string getText(int x, int y)
		{
			if (contains(x,y))
			{
				return this.getText(false);
			}
			else
			{
				if (this.Successor != null)
				{
					return this.Successor.getText(x,y);
				}
				else
				{
					return "";
				}
			}
		}

		// Get the text from a pop-up dialog and then set it?
		public override bool setText(int x, int y)
		{

			bool textset = false;
			if (contains(x,y))
			{
				if (this.kind==Kind_Of.Assignment)
				{
					if(this.text_str == "")
                    {
						AssignmentDialog AD = new AssignmentDialog(this, false);
						AD.ShowDialog(MainWindow.topWindow);
                    }
                    else
                    {
						AssignmentDialog AD = new AssignmentDialog(this, true);
						AD.ShowDialog(MainWindow.topWindow);
					}
					
				}
				else
				{
					if(this.text_str == "")
                    {
						CallDialog CD = new CallDialog(this, false);
						CD.ShowDialog(MainWindow.topWindow);
                    }
                    else
                    {
						CallDialog CD = new CallDialog(this, true);
						CD.ShowDialog(MainWindow.topWindow);
					}
					
				}
				
				
				textset = true;
				return textset;
			}
	
			if (this.Successor != null)
			{
				return(this.Successor.setText(x,y));
			}
			
			return textset;
		}




		// Check to see if I or my successor or chidren have empty parse trees.
		public override bool has_code()
		{
			bool im_ok = true;
			bool my_successor_ok = true;

			if (this.Successor != null)
			{
				my_successor_ok = this.Successor.has_code();
			}
			if (this.parse_tree == null)
			{
				im_ok = false;
			}

			return (im_ok && my_successor_ok);
		}

		// Mark error if I or my successor or chidren have empty parse trees.
		public override void mark_error()
		{
			if (this.Successor != null)
			{
				this.Successor.mark_error();
			}
			if (this.parse_tree == null)
			{
				this.Text = "Error";
				//Runtime.parent.Show_Text_On_Error();
			}
		}
		public override string getDrawText()
		{
			return Component.unbreakString(this.getText(true));
		}
		public override bool Called_Tab(string s)
		{
			try 
			{
				if (this.parse_tree is parse_tree.Procedure_Call &&
                    ((parse_tree.Procedure_Call) this.parse_tree).is_tab_call() &&
                    s.ToLower() == interpreter_pkg.get_name_call(this.parse_tree as parse_tree.Procedure_Call,
                        this.Text).ToLower())
				{
					return true;
				}
				else
				{
					return base.Called_Tab(s);
				}
			}
			catch
			{
				return base.Called_Tab(s);
			}
		}
		public override void Rename_Tab(string from, string to)
		{
            try
            {
                if (this.parse_tree is parse_tree.Procedure_Call)
                {
                    string call_name = interpreter_pkg.get_name_call(this.parse_tree as parse_tree.Procedure_Call,
                        this.Text);
                    if (((parse_tree.Procedure_Call)this.parse_tree).is_tab_call() &&
                        from.ToLower() == call_name.ToLower())
                    {
                        this.Text = this.Text.Replace(call_name, to);
                        this.parse_tree = interpreter_pkg.call_syntax(this.Text).tree;
                    }
                }
            }
            catch
            {
            }
			base.Rename_Tab(from,to);
		}
        private string Recursion_Involves()
        {
            string answer = "";
            /*for (int i = 0; i < Runtime.parent.carlisle.TabPages.Count; i++)
            {
                if (((Subchart)Runtime.parent.carlisle.TabPages[i]).am_compiling)
                {
                    answer = answer + Runtime.parent.carlisle.TabPages[i].Text + " ";
                }
            }*/
            return answer;
        }
		private Subchart Find_Start(string s)
		{
            /*TabControl.TabPageCollection tpc = Compile_Helpers.get_tpc();
			for (int i=0; i<tpc.Count; i++)
			{
				if (tpc[i].Text.ToLower()==s.ToLower())
				{
					return (Subchart) tpc[i];
				}
			}*/
			return null;
		}

        public override void Emit_Code(Generate_Interface gen)
        {
            if (this.kind == Kind_Of.Call &&
                ((parse_tree.Procedure_Call)this.parse_tree).is_tab_call())
            {
                string call_name = interpreter_pkg.get_name_call(this.parse_tree as parse_tree.Procedure_Call,
                    this.Text);
                Subchart called_chart = Find_Start(call_name);
                if (!(called_chart is Procedure_Chart))
                {
                    called_chart.Start.Emit_Code(gen);
                }
                else
                {
                    Procedure_Chart pc = called_chart as Procedure_Chart;
                    parse_tree.Parameter_List walk = ((parse_tree.Procedure_Call)this.parse_tree).param_list;
                    object o = gen.Emit_Call_Subchart(pc.Text);
                    for (int i = 0; i < pc.num_params; i++)
                    {
						parse_tree.emit_parameter_number(walk.parameter, gen, 0);
                        walk = walk.next;
                        if (walk != null)
                        {
                            gen.Emit_Next_Parameter(o);
                        }
                    }
                    gen.Emit_Last_Parameter(o);
                }
            }
            else if (this.parse_tree != null)
            {

				Component.the_lexer = new Lexer(this.Text);
				this.parse_tree.Emit_Code(gen);
			}
            if (this.Successor != null)
            {
                this.Successor.Emit_Code(gen);
            }
        }

        public override void compile_pass1(Generate_Interface gen)
		{	
			if (this.kind==Kind_Of.Call &&
				((parse_tree.Procedure_Call) this.parse_tree).is_tab_call())
			{
                string call_name = interpreter_pkg.get_name_call(this.parse_tree as parse_tree.Procedure_Call,
                    this.Text);
                Subchart sub = Find_Start(call_name);
                if (!(sub is Procedure_Chart))
                {
                    if (sub.am_compiling)
                    {
                        throw new System.Exception("The RAPTOR compiler does not support recursive programs.\n" +
                            "Recursion found at call to: " + this.Text + "\n"  +
                            "Cycle of calls includes: " + Recursion_Involves());
                    }
                    sub.am_compiling = true;
                    sub.Start.compile_pass1(gen);
                    sub.am_compiling = false;
                }
                else
                {
                    Procedure_Chart pc = sub as Procedure_Chart;
                    parse_tree.Parameter_List walk = ((parse_tree.Procedure_Call)this.parse_tree).param_list;
                    for (int i = 0; i < pc.num_params; i++)
                    {
                        // it seems like we would want to call pass1 on our parameters
                        // but this may give us the mistaken impression that an array is a variable
                        // if it is passed

                        //    walk.parameter.compile_pass1(gen);
                        walk = walk.next;
                    }
                }
			}
			else if (this.parse_tree!=null)
			{
				Component.the_lexer = new Lexer(this.Text);
				this.parse_tree.compile_pass1(gen);
			}
			if (this.Successor!=null)
			{
				this.Successor.compile_pass1(gen);
			}
		}
		
		public override void wide_footprint(Avalonia.Media.DrawingContext gr)
		{
			int height_of_text, width_of_text=2*W;
			int szHeight, szWidth=0;


			Avalonia.Media.FormattedText formattedtextYes = new Avalonia.Media.FormattedText(
				"Yes", new Avalonia.Media.Typeface("arial"), 12, Avalonia.Media.TextAlignment.Center,
				Avalonia.Media.TextWrapping.NoWrap, Avalonia.Size.Infinity);
			height_of_text = (int)Math.Ceiling(formattedtextYes.Bounds.Height);


			// loop starting at 2*W until you get on 3 lines.
			while (true) 
			{
				Avalonia.Media.FormattedText formattedtext = new Avalonia.Media.FormattedText(
					this.getDrawText() + "XX", new Avalonia.Media.Typeface("arial"), 12, Avalonia.Media.TextAlignment.Center,
					Avalonia.Media.TextWrapping.Wrap, Avalonia.Size.Infinity.WithWidth(width_of_text));
				szHeight = (int)Math.Ceiling(formattedtext.Bounds.Height);
				szWidth = (int)Math.Ceiling(formattedtext.Bounds.Width);
				if (szHeight<height_of_text*7/2)
				{
					break;
				}
				width_of_text = width_of_text + W/2;
			}

			if (szHeight > height_of_text*3/2) 
			{
				FP.left = (width_of_text-W)/2+3*W/8;
				FP.right = (width_of_text-W)/2+3*W/8;
				drawing_text_width = width_of_text;
			}
			else if ((int) szWidth > W)
			{
				width_of_text = W;
				while (width_of_text < (int) szWidth)
				{
					width_of_text += W/2;
				}
				FP.left = (width_of_text-W)/2+3*W/8;
				FP.right = (width_of_text-W)/2+3*W/8;
				drawing_text_width = width_of_text;
			}
			else
			{
				drawing_text_width = 0;
			}
		}
		public override bool In_Footprint(int x, int y)
		{
			if (this.kind==Kind_Of.Assignment)
			{
				return base.In_Footprint(x,y);
			}
			// add a bit for the =>
			if (base.In_Footprint(x,y))
			{
				return true;
			}
			Avalonia.Rect rec;
			
			if (drawing_text_width>W)
			{
				rec = new Avalonia.Rect(
					this.X+this.drawing_text_width/2,
					this.Y+this.H/4,
					this.W/4,
					this.H/2);
			}
			else
			{
				rec = new Avalonia.Rect(
					this.X+this.W/2,
					this.Y+this.H/4,
					this.W/4,
					this.H/2);
			}
			return rec.Contains(new Point(x,y));
		}

		public override void draw(Avalonia.Media.DrawingContext gr, int x, int y)
		{	
			bool draw_text;
			int box_width;

			// determine whether or not to draw the text
			if ((this.scale <= .4) || (this.head_heightOrig < 10))
			{
				draw_text = false;
			}
			else
			{
				draw_text = Component.text_visible;
			}

			X = x;
			Y = y;

			Avalonia.Media.FormattedText formattedtextYes = new Avalonia.Media.FormattedText(
				"Yes", new Avalonia.Media.Typeface("arial"), 12, Avalonia.Media.TextAlignment.Center,
				Avalonia.Media.TextWrapping.NoWrap, Avalonia.Size.Infinity);
			height_of_text = (int)Math.Ceiling(formattedtextYes.Bounds.Height);

			Avalonia.Media.FormattedText formattedtextX = new Avalonia.Media.FormattedText(
				this.getDrawText() + " X", new Avalonia.Media.Typeface("arial"), 12, Avalonia.Media.TextAlignment.Center,
				Avalonia.Media.TextWrapping.NoWrap, Avalonia.Size.Infinity);
			width_of_text = (int)Math.Ceiling(formattedtextX.Bounds.Width);

			//gr.SmoothingMode=System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			Avalonia.Media.Pen pen_color;
			if (this.selected)
			{
				pen_color = PensBrushes.red_pen;
			}
			else if (this.running)
			{
				pen_color = PensBrushes.chartreuse_pen;
			}
			else
			{
				pen_color = PensBrushes.blue_pen;
			}
			if (this.drawing_text_width > W)
			{
				box_width = this.drawing_text_width;
			}
			else
			{
				box_width = W;
			}
			if (this.has_breakpoint)
			{
				StopSign.Draw(gr,x-box_width/2-W/6-2,y, W/6);
			}

			// draw box if not USMA or not call
			if (this.kind!=Kind_Of.Call || !Component.USMA_mode)
			{
				gr.DrawRectangle(pen_color, new Rect(x-box_width/2, 
					y, box_width, H));
			}
			else 
			{
				// top line
				gr.DrawLine(pen_color,
					new Point(x-box_width/2,y),
					new Point(x+box_width/2,y));
				// left line
				gr.DrawLine(pen_color,
					new Point(x-box_width/2,y),
					new Point(x-box_width/2,y+3*H/4));
				// right line
				gr.DrawLine(pen_color,
					new Point(x+box_width/2,y),
					new Point(x+box_width/2,y+3*H/4));
				// left bottom
				gr.DrawLine(pen_color,
					new Point(x-box_width/2,y+3*H/4),
					new Point(x,y+H));
				// right bottom
				gr.DrawLine(pen_color,
					new Point(x,y+H),
					new Point(x+box_width/2,y+3*H/4));
			}

			if (this.kind==Kind_Of.Call && !Component.USMA_mode)
			{
				gr.DrawLine(pen_color,
					new Point(x+box_width/2,y+5*H/12),
					new Point(x+box_width/2+W/8,y+5*H/12));
				gr.DrawLine(pen_color,
					new Point(x+box_width/2+W/8,y+5*H/12),
					new Point(x+box_width/2+W/8,y+H/4));
				gr.DrawLine(pen_color,
					new Point(x+box_width/2+W/8,y+H/4),
					new Point(x+box_width/2+2*W/8,y+H/2));
				gr.DrawLine(pen_color,
					new Point(x+box_width/2,y+7*H/12),
					new Point(x + box_width / 2 + W / 8, y + 7 * H / 12));
				gr.DrawLine(pen_color,
					new Point(x + box_width / 2 + W / 8, y + 7 * H / 12), 
					new Point(x + box_width / 2 + W / 8, y + 3 * H / 4));
				gr.DrawLine(pen_color,
					new Point(x + box_width / 2 + W / 8, y + 3 * H / 4), 
					new Point(x + box_width / 2 + 2 * W / 8, y + H / 2));
			}


			if ((draw_text) && (this.Text.Length > 0))
			{
				if (Component.full_text)
				{
					// we get rect from footprint
					if (drawing_text_width>W)
					{
						rect = new Avalonia.Rect(x-drawing_text_width/2, Y+(H*1)/32, drawing_text_width,this.height_of_text*3);
					}
					else
					{
						rect = new Avalonia.Rect(x-this.width_of_text/2, Y+(H*6)/16, this.width_of_text,this.height_of_text);
					}
				}
				else
				{
					rect = new Avalonia.Rect(x-W/2, Y+(H*6)/16, W,this.height_of_text);
				}

				if (this.Text == "Error")
				{
					Avalonia.Media.FormattedText formattedtext = new Avalonia.Media.FormattedText(
						this.Text, new Avalonia.Media.Typeface("arial"), 12, Avalonia.Media.TextAlignment.Center,
						Avalonia.Media.TextWrapping.NoWrap, Avalonia.Size.Infinity);
					gr.DrawText(PensBrushes.redbrush, rect.TopLeft, formattedtext);
				}
				else
				{
					Avalonia.Size sz = new Avalonia.Size(drawing_text_width, height_of_text);
					Avalonia.Media.FormattedText formattedtext = new Avalonia.Media.FormattedText(
						this.getDrawText(), new Avalonia.Media.Typeface("arial"), 12, Avalonia.Media.TextAlignment.Center,
						Avalonia.Media.TextWrapping.Wrap, sz);
					gr.DrawText(PensBrushes.blackbrush, rect.TopLeft, formattedtext);
				}
			}

			if (this.Successor != null)
			{
				// color for connector line
				Avalonia.Media.Pen pen;

				if (this.selected)
				{
					pen = PensBrushes.red_pen;
				}
				else
				{
					pen = PensBrushes.blue_pen;
				}
				gr.DrawLine(pen,new Point(x,y+H), new Point(x,y+H+CL)); // draw connector line to successor
				gr.DrawLine(pen, new Point(x,y+H+CL), new Point(x - CL / 4, y +H+CL - CL/4)); // draw left side of arrow
				gr.DrawLine(pen, new Point(x,y+H+CL), new Point(x + CL / 4,y +H+CL - CL/4)); // draw right side of arrow

				Successor.scale = this.scale;
				Successor.draw(gr,x,y+H+CL);
				
			}

			if (draw_text) 
			{
				base.draw(gr,x,y);
			}

		}
        public override void collect_variable_names(System.Collections.Generic.IList<string> l,
            System.Collections.Generic.IDictionary<string, string> types)
        {
            if (this.kind == Kind_Of.Assignment && this.parse_tree != null)
            {
                string name = interpreter_pkg.get_name((parse_tree.Assignment)this.parse_tree, this.Text);
                l.Add(name);
                if (this.parse_tree is parse_tree.Expr_Assignment)
                {
                    string typename = (this.parse_tree as parse_tree.Expr_Assignment).expr_part.get_class_decl();
                    if (typename != null)
                    {
                        if (!types.ContainsKey(name.ToLower()))
                        {
                            types.Add(name.ToLower(), typename);
                        }
                    }
                }
            }
            if (this.Successor != null)
            {
                this.Successor.collect_variable_names(l,types);
            }
        }
	}
}
