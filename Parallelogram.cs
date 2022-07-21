using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using RAPTOR_Avalonia_MVVM;
using Avalonia;
using RAPTOR_Avalonia_MVVM.Views;

namespace raptor
{
	/// <summary>
	/// Summary description for Parallelogram.
	/// </summary>
	
	[Serializable]
	public class Parallelogram : Component
	{
		public string prompt = "";
		public bool is_input;
		public bool new_line=true;
		public bool input_is_expression=false;
		public parse_tree.Parseable prompt_tree;
		public interpreter.Syntax_Result prompt_result;
		public string assign = "";
		public numbers.value pans = new numbers.value();

		public Parallelogram(int height, int width, String str_name, bool input)
			: base(height, width, str_name)
		{
			this.init();
			this.is_input = input;
		}

		public Parallelogram(Component Successor, int height, int width, String str_name, bool input)
			: base(Successor, height, width, str_name)
		{
			this.is_input = input;
			this.init();
		}

		public Parallelogram(SerializationInfo info, StreamingContext ctxt)
			: base(info,ctxt)
		{
			prompt = (string)info.GetValue("_prompt", typeof(string));
			is_input = (bool)info.GetValue("_is_input", typeof(bool));
			// is prompt an expression or just plain text?
			if (this.incoming_serialization_version >= 9 && this.is_input)
			{
				this.input_is_expression = info.GetBoolean("_input_expression");
			}
			else
			{
				this.input_is_expression = false;
			}

			if (this.incoming_serialization_version >= 5)
			{
				this.new_line = info.GetBoolean("_new_line");
			}
			else
			{
				this.new_line = true;
			}
			if (is_input)
			{
				result = interpreter_pkg.input_syntax(this.Text);
				if (this.input_is_expression)
				{
					prompt_result = interpreter_pkg.output_syntax(this.prompt,false);
				}
			}
			else
			{
				result = interpreter_pkg.output_syntax(this.Text,this.new_line);
			}

			if (this.input_is_expression)
			{
				if (prompt_result.valid)
				{
					this.prompt_tree = prompt_result.tree;
				}
				else
				{
					this.prompt = "";
				}
			}

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
			info.AddValue("_prompt", prompt);
			info.AddValue("_is_input", is_input);
			info.AddValue("_new_line", new_line);
			info.AddValue("_input_expression", input_is_expression);
			base.GetObjectData(info,ctxt);
		}


		public override void draw(Avalonia.Media.DrawingContext gr, int x, int y)
		{
			bool draw_text;
			int box_width;

			if ((this.scale <= .4) || (this.head_heightOrig < 10))
			{
				draw_text = false;
			}
			else
			{
				draw_text = Component.text_visible;
			}
			if (draw_text) 
			{
				base.draw(gr,x,y);
			}

			int delta = W/8;
			X = x;
			Y = y;

			Avalonia.Media.FormattedText formattedtextYes = new Avalonia.Media.FormattedText(
				"Yes", new Avalonia.Media.Typeface("arial"), Oval.textSize, Avalonia.Media.TextAlignment.Center,
				Avalonia.Media.TextWrapping.NoWrap, Avalonia.Size.Infinity);
			height_of_text = (int)Math.Ceiling(formattedtextYes.Bounds.Height);

			Avalonia.Media.FormattedText formattedtextX = new Avalonia.Media.FormattedText(
				this.getDrawText() + " X", new Avalonia.Media.Typeface("arial"), Oval.textSize, Avalonia.Media.TextAlignment.Center,
				Avalonia.Media.TextWrapping.NoWrap, Avalonia.Size.Infinity);
			width_of_text = (int)Math.Ceiling(formattedtextX.Bounds.Width);


			//gr.SmoothingMode=System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			Avalonia.Media.Pen pen;
			if (this.selected)
			{
				pen = PensBrushes.red_pen;
			}
			else if (this.running)
			{
				pen = PensBrushes.chartreuse_pen;
			}
			else
			{
				pen = PensBrushes.blue_pen;
			}
			if (this.drawing_text_width > W)
			{
				box_width = this.drawing_text_width+3*delta/2;
			}
			else
			{
				box_width = W;
			}
			gr.DrawLine(pen,new Avalonia.Point(x-box_width/2+delta,y),
				new Avalonia.Point(x+box_width/2,y)); // draw top line
			gr.DrawLine(pen, new Avalonia.Point(x +box_width/2,y),
				new Avalonia.Point(x +box_width/2-delta,y+H)); // draw right line
			gr.DrawLine(pen, new Avalonia.Point(x -box_width/2,y+H),
				new Avalonia.Point(x +box_width/2-delta,y+H)); // draw bottom line
			gr.DrawLine(pen, new Avalonia.Point(x -box_width/2,y+H),
				new Avalonia.Point(x -box_width/2+delta,y)); // draw bottom line
			if (this.has_breakpoint)
			{
				StopSign.Draw(gr,x-box_width/2-W/6-2,y, W/6);
			}

			if (this.is_input)
			{
				gr.DrawLine(pen, new Avalonia.Point(x -box_width/2+3*W/32-W/4,y+H/4),
					new Avalonia.Point(x -box_width/2+3*W/32,y+H/4)); // input line
				gr.DrawLine(pen, new Avalonia.Point(x -box_width/2+3*W/32,y+H/4),
					new Avalonia.Point(x -box_width/2+3*W/32-W/8,y+H/4-H/8)); // up arrow
				gr.DrawLine(pen, new Avalonia.Point(x -box_width/2+3*W/32,y+H/4),
					new Avalonia.Point(x -box_width/2+3*W/32-W/8,y+H/4+H/8)); // down arrow
/*				gr.DrawLine(pen,x-box_width/2-W/32,y+H/4,
					x-box_width/2-W/32+W/3,y+H/4); // input line
				gr.DrawLine(pen,x-box_width/2-W/32+W/3,y+H/4,
					x-box_width/2-W/32+W/3-W/8,y+H/4-H/8); // up arrow
				gr.DrawLine(pen,x-box_width/2-W/32+W/3,y+H/4,
					x-box_width/2-W/32+W/3-W/8,y+H/4+H/8); // down arrow
					*/
			}
			else
			{
				gr.DrawLine(pen, new Avalonia.Point(x +box_width/2-3*W/32,y+H-H/4),
					new Avalonia.Point(x +box_width/2-3*W/32+W/4,y+H-H/4)); // output line
				gr.DrawLine(pen, new Avalonia.Point(x +box_width/2-3*W/32+W/4,y+H-H/4),
					new Avalonia.Point(x +box_width/2-3*W/32+W/4-W/8,y+H-H/4-H/8)); // up arrow
				gr.DrawLine(pen, new Avalonia.Point(x +box_width/2-3*W/32+W/4,y+H-H/4),
					new Avalonia.Point(x +box_width/2-3*W/32+W/4-W/8,y+H-H/4+H/8)); // down arrow
/*				gr.DrawLine(pen,x+box_width/2-3*W/16,y+H-H/4,
					x+box_width/2-3*W/16+W/3,y+H-H/4); // output line
				gr.DrawLine(pen,x+box_width/2-3*W/16+W/3,y+H-H/4,
					x+box_width/2-3*W/16+W/3-W/8,y+H-H/4-H/8); // up arrow
				gr.DrawLine(pen,x+box_width/2-3*W/16+W/3,y+H-H/4,
					x+box_width/2-3*W/16+W/3-W/8,y+H-H/4+H/8); // down arrow
					*/
			}


			if(draw_text)
			{
				if (Component.full_text)
				{
					// we get rect from footprint
					if (drawing_text_width>0)
					{
						rect = new Avalonia.Rect(x-drawing_text_width/2, Y+(H*1)/32,
							drawing_text_width,this.height_of_text*3);
					}
					else
					{
						rect = new Avalonia.Rect(x-this.width_of_text/2, Y+(H*6)/16,
							this.width_of_text,this.height_of_text);
					}
				}
				else
				{
					rect = new Avalonia.Rect(x-(W*7)/16, Y+(H*6)/16, W-W/8,this.height_of_text);
				}

				if (this.Text == "Error")
				{   Avalonia.Media.FormattedText formattedtext = new Avalonia.Media.FormattedText(
						this.Text, new Avalonia.Media.Typeface("arial"), Oval.textSize, Avalonia.Media.TextAlignment.Center, 
						Avalonia.Media.TextWrapping.NoWrap, Avalonia.Size.Infinity);
					gr.DrawText(PensBrushes.redbrush, rect.TopLeft, formattedtext);
				}
				else
				{
					Avalonia.Media.FormattedText formattedtext = new Avalonia.Media.FormattedText(
						this.getDrawText(), new Avalonia.Media.Typeface("arial"), Oval.textSize, Avalonia.Media.TextAlignment.Center,
						Avalonia.Media.TextWrapping.Wrap, new Avalonia.Size(drawing_text_width,height_of_text));
					gr.DrawText(PensBrushes.blackbrush, rect.TopLeft, formattedtext);
				}
			}

			if (Successor != null)
			{
				if (this.selected)
				{
					pen=PensBrushes.red_pen;
				}
				else
				{
					pen=PensBrushes.blue_pen;
				}
				gr.DrawLine(pen,new Point(x,y+H),new Point(x,y+H+CL)); // draw connector line to successor
				gr.DrawLine(pen, new Point(x,y+H+CL), new Point(x -CL/4,y+H+CL - CL/4)); // draw left side of arrow
				gr.DrawLine(pen, new Point(x,y+H+CL), new Point(x +CL/4,y+H+CL - CL/4)); // draw right side of arrow

				Successor.scale = this.scale;
				Successor.draw(gr,x,y+H+CL);
			}
		}

		public override void wide_footprint(Avalonia.Media.DrawingContext gr)
		{
			int height_of_text, width_of_text=2*W;
			int szHeight, szWidth = 0;


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
				FP.left = (width_of_text-W)/2+W/2;
				FP.right = (width_of_text-W)/2+W/2;
				drawing_text_width = width_of_text;
			}
			else if ((int) szWidth > W)
			{
				width_of_text = W;
				while (width_of_text < (int) szWidth)
				{
					width_of_text += W/2;
				}
				FP.left = (width_of_text-W)/2+W/2;
				FP.right = (width_of_text-W)/2+W/2;
				drawing_text_width = width_of_text;
			}
			else
			{
				drawing_text_width = 0;
			}
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




		
		// Get the text from a pop-up dialog and then set it?
		public override string getText(int x, int y)
		{
			if (contains(x,y))
			{
				return this.Text;
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
				if (this.is_input)
				{
					if(this.text_str == "")
                    {
						InputDialog IOD = new InputDialog(this, false);
						IOD.ShowDialog(MainWindow.topWindow);
                    }
                    else
                    {
						InputDialog IOD = new InputDialog(this, true);
						IOD.ShowDialog(MainWindow.topWindow);
					}
					
				}
				else
				{
					if(this.text_str == "")
                    {
						OutputDialog IOD = new OutputDialog(this, false);
						IOD.ShowDialog(MainWindow.topWindow);
                    }
                    else
                    {
						OutputDialog IOD = new OutputDialog(this, true);
						IOD.ShowDialog(MainWindow.topWindow);
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
			string result;
			string get_string;
			string put_string;

			if (Component.USMA_mode) 
			{
				get_string = "INPUT " + this.Text;
				put_string = "OUTPUT " + this.Text;
			}
			else
			{
				get_string = "GET " + this.Text;
				put_string = "PUT " + this.Text;
			}

			if (this.is_input && this.Text != "")
			{
				if (Component.full_text)
				{
					//result = this.prompt + '\n' +
					//		get_string;
                    if (!this.input_is_expression)
                    {
                        result = '"' + this.prompt + '"' + '\n' +
                            get_string;
                    }
                    else
                    {
                        result = this.prompt + '\n' +
                            get_string;
                    }
                }
				else
				{
					result = get_string;
				}
			}
			else if (!this.is_input && this.Text != "")
			{
				if (!this.new_line)
				{
					result = put_string;
				}
				else
				{
					result = put_string + '\u00B6';
				}
			}
			else
			{
				result = "";
			}
			if (this.is_input || (this.Text!="" && this.Text[0]=='"'))
			{
				return result;
			}
			else
			{
				return Component.unbreakString(result);
			}
		}
		public override void compile_pass1(Generate_Interface gen)
		{
			if (this.parse_tree!=null && this.is_input && this.input_is_expression)
			{
                // maintain with parse_tree.adb
                if (!Compile_Helpers.Start_New_Declaration("raptor_prompt_variable_zzyz"))
                {
                    gen.Declare_String_Variable("raptor_prompt_variable_zzyz");
                }
			}
			base.compile_pass1(gen);
		}

		public override void Emit_Code(Generate_Interface gen)
		{	
			if (this.parse_tree!=null)
			{
				if (this.is_input)
				{
					if (!this.input_is_expression)
					{
						//parse_tree.set_prompt(this.prompt);
					}
					else
					{
						//parse_tree.set_prompt(null);
                        // maintain with parse_tree.adb
                        gen.Variable_Assignment_Start("raptor_prompt_variable_zzyz");
						Component.the_lexer = new Lexer(this.prompt);
						Component.currentTempComponent = this;
                        base.Emit_Code(gen);
                        gen.Variable_Assignment_PastRHS();
					}
				}
				Component.the_lexer = new Lexer(this.Text);
				Component.currentTempComponent = this;
				base.Emit_Code(gen);
			}
            else if (this.Successor != null)
            {

                this.Successor.Emit_Code(gen);
            }
        }
        public override void collect_variable_names(System.Collections.Generic.IList<string> l,
            System.Collections.Generic.IDictionary<string, string> types)
        {
            if (this.is_input && this.parse_tree != null)
            {
                string name = interpreter_pkg.get_name_input((parse_tree.Input)this.parse_tree, this.Text);
                l.Add(name);
            }
            if (this.Successor != null)
            {
                this.Successor.collect_variable_names(l,types);
            }
        }

	}
}
