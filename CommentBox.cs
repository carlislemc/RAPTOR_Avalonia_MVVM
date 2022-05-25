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
	public class CommentBox : ISerializable //Set this attribute to all the classes that want to serialize
	{
		public static int current_serialization_version = 1;
		public int incoming_serialization_version;
		public string[] Text_Array;
		private int num_lines;
		public Component parent;
		private int x_location, y_location;
		private int draw_x, draw_y;
		private int height_of_text;
		private int width_of_text;
		private int height;
		private int width;
		//		private Avalonia.Rect rect;
		public bool selected = false;
		public bool text_change = true;


		public CommentBox(Component my_parent)
		{
			parent = my_parent;
			this.Text_Array = new string[1];
			this.Text_Array[0] = "";
		}

		public CommentBox(){
			parent = new Rectangle(10, 10, "test", Rectangle.Kind_Of.Assignment);
			this.Text_Array = new string[1];
			this.Text_Array[0] = "";
		}

		//Deserialization constructor.
		public CommentBox(SerializationInfo info, StreamingContext ctxt)
		{
			//Get the values from info and assign them to the appropriate properties
			
			incoming_serialization_version = (int)info.GetValue(
				"_version", typeof(int));
			num_lines = (int)info.GetValue("_num_lines", typeof(int));
			x_location = (int)info.GetValue("_x_location", typeof(int));
			y_location = (int)info.GetValue("_y_location", typeof(int));
			// parent is taken care of in Component
			this.Text_Array = new string[num_lines];
			for (int i = 0; i < num_lines; i++)
			{
				Text_Array[i] = (string)info.GetValue("_line"+i, typeof(string));
			}
		}

		//Serialization function.
		public virtual void GetObjectData(SerializationInfo info, StreamingContext ctxt)
		{
			//You can use any custom name for your name-value pair. But make sure you
			// read the values with the same name. For ex:- If you write EmpId as "EmployeeId"
			// then you should read the same with "EmployeeId"

			num_lines = Text_Array.Length;
			info.AddValue("_num_lines", num_lines); 
			info.AddValue("_x_location", x_location);
			info.AddValue("_y_location", y_location);
			// parent is taken care of in Component
			info.AddValue("_version",current_serialization_version);
			for (int i= 0; i < num_lines; i++)
			{
				info.AddValue("_line" + i, Text_Array[i]);
			}
		}

		public int X	//get or set the x location of object
		{
			set 
			{ 
				x_location = value; 
				draw_x = (int) Math.Round(parent.scale*this.X);
			}
			get { return x_location; }
		} 
        
		public int Y	//get or set the x location of object
		{
			set 
			{
				y_location = value; 
				draw_y = (int) Math.Round(parent.scale*this.Y);
			}
		get { return y_location; }
		} 

		public int H	//get the head height of object
		{
			set {height = value; }
			get {return height; }
		} 

		public int W	//get the head width of object
		{
			set {width = value; }
			get {return width; }
		}
 
		private void resize(Avalonia.Media.DrawingContext gr)
		{
			int max_width = 0;


			Avalonia.Media.FormattedText formattedtext0 = new Avalonia.Media.FormattedText(
				this.Text_Array[0], new Avalonia.Media.Typeface("arial"), 12, Avalonia.Media.TextAlignment.Center,
				Avalonia.Media.TextWrapping.NoWrap, Avalonia.Size.Infinity);

			this.height_of_text = (int)Math.Ceiling(formattedtext0.Bounds.Height);

			this.num_lines = this.Text_Array.Length;
			if (this.Text_Array[this.num_lines-1].Length > 0)
			{
			}
			else
			{
				this.num_lines--;
			}

			for (int i = 0; i < this.num_lines; i++)
			{

				Avalonia.Media.FormattedText formattedtextI = new Avalonia.Media.FormattedText(
					this.Text_Array[i], new Avalonia.Media.Typeface("arial"), 12, Avalonia.Media.TextAlignment.Center,
					Avalonia.Media.TextWrapping.NoWrap, Avalonia.Size.Infinity);

				this.width_of_text = (int)Math.Ceiling(formattedtextI.Bounds.Width);

				if (width_of_text > max_width)
				{
					max_width = this.width_of_text;
				}
			}

			this.width_of_text = max_width;
			W = width_of_text + 4;
			H = height_of_text * this.num_lines + 4;
		}

		public void draw(Avalonia.Media.DrawingContext gr, int parent_x, int parent_y)
		{
			Avalonia.Rect rect;

			if (this.Text_Array==null || this.Text_Array.Length==0)
			{
				return;
			}
			if (this.text_change || Component.Inside_Print || Component.Just_After_Print)
			{
				this.resize(gr);
				this.text_change=false;
				Component.Just_After_Print = false;
			}
			
			//gr.SmoothingMode=System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			Balloon.Corner corner;
			if ((draw_x+W/2 < 0) && (draw_y+H/2 < this.parent.H/2))
			{
				corner = Balloon.Corner.Lower_Right;
			}
			else if (draw_x+W/2 < 0)
			{
				corner = Balloon.Corner.Upper_Right;
			}
			else if (draw_y+H/2 > this.parent.H/2)
			{
				corner = Balloon.Corner.Upper_Left;
			}
			else
			{
				corner = Balloon.Corner.Lower_Left;
			}
			if (this.selected)
			{

				gr.DrawRectangle(PensBrushes.green_pen, new Avalonia.Rect(parent_x+draw_x, parent_y+draw_y, W, H));
				/*gr.DrawPath(PensBrushes.red_pen, Balloon.Make_Path(
					new Avalonia.Rect(parent_x+draw_x, parent_y+draw_y, W+10, H),
					corner));*/
			}
			else
			{

				gr.DrawRectangle(PensBrushes.green_pen, new Avalonia.Rect(parent_x + draw_x, parent_y + draw_y, W, H));
				/*gr.DrawPath(PensBrushes.green_pen, Balloon.Make_Path(
					new Avalonia.Rect(parent_x+draw_x, parent_y+draw_y, W+10, H),
					corner));*/
			}

			for (int i = 0; i < this.num_lines; i++)
			{	rect = new Avalonia.Rect(parent_x+draw_x+6,parent_y+draw_y+this.height_of_text*i,W,this.height_of_text);
				Avalonia.Media.FormattedText formattedtextDraw = new Avalonia.Media.FormattedText(
					this.Text_Array[i], new Avalonia.Media.Typeface("arial"), 12, Avalonia.Media.TextAlignment.Center,
					Avalonia.Media.TextWrapping.NoWrap, Avalonia.Size.Infinity); 
				gr.DrawText(PensBrushes.blackbrush, rect.TopLeft, formattedtextDraw);

			}
		}


		public bool contains(int x, int y)
		{
			bool bounded_x = (x >= this.parent.X+draw_x) && 
				(x <= this.parent.X+draw_x+this.W);
			bool bounded_y = (y >= this.parent.Y+draw_y) && 
				(y <= this.parent.Y+draw_y+this.H);
			
			return bounded_x && bounded_y;
		}

		public Avalonia.Rect Get_Bounds()
		{
			return new Avalonia.Rect(
				this.parent.X+this.draw_x,
				this.parent.Y+this.draw_y,
				this.W,
				this.H);
		}

		public static Avalonia.Rect Union(
			Avalonia.Rect l,
			Avalonia.Rect r)
		{
			int x,y,right,bottom;
			if (l.X < r.X)
			{
				x = (int)l.X;
			}
			else
			{
				x = (int)r.X;
			}
			if (l.Y < r.Y)
			{
				y = (int)l.Y;
			}
			else
			{
				y = (int)r.Y;
			}
			if (l.Right > r.Right)
			{
				right = (int)l.Right;
			}
			else
			{
				right = (int)r.Right;
			}
			if (l.Bottom > r.Bottom)
			{
				bottom = (int)l.Bottom;
			}
			else
			{
				bottom = (int)r.Bottom;
			}
			return new Avalonia.Rect(x,y,right-x,bottom-y);
		}


		//Scale the object
		public void Scale(float new_scale)
		{	
			draw_x = (int) Math.Round(new_scale*this.X);
			draw_y = (int) Math.Round(new_scale*this.Y);

			this.text_change = true;
		}

		// If (x, y) is over the object color it red?
		public bool select(int x, int y)
		{
			this.selected = false;

			if (contains(x,y))
			{
				this.selected = true;
			}
	
			return this.selected;
		}

		// Get the text from a pop-up dialog and then set it?
		public string[] getText()
		{
			return this.Text_Array;
		}

		// Get the text from a pop-up dialog and then set it?
		public void setText(/*Visual_Flow_Form form*/)
		{
			CommentDialog CD = new CommentDialog(this);
			CD.ShowDialog(MainWindow.topWindow);
			if (this.text_change)
			{
				//this.resize(form.CreateGraphics());
				this.text_change = false;
			}
		}

		public CommentBox Clone()
		{
			CommentBox Result = (CommentBox)this.MemberwiseClone();
			return Result;
		}
	}
}
