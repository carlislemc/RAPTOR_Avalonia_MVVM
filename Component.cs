using System;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using RAPTOR_Avalonia_MVVM;
using Avalonia;

namespace raptor
{
	/// <summary>
	/// Summary description for Component.
	/// </summary>

    [Serializable()]  //Set this attribute to all the classes that want to serialize
    public enum Mode { Novice, Intermediate, Expert }
	public abstract class Component : ISerializable //Set this attribute to all the classes that want to serialize

	{
		[Serializable()]
			public class FootPrint
		{
			public int left, right, height;
			public FootPrint Clone() 
			{
				return (FootPrint) this.MemberwiseClone();
			}
		}
        private static bool MONO_INITIALIZED = false;
        private static bool MONO_VALUE=false;
        public static bool MONO 
        {
            get {
                if (!MONO_INITIALIZED)
                {
                    MONO_INITIALIZED = true;
                    Type t = Type.GetType("Mono.Runtime");
                    if (t != null)
                        MONO_VALUE=true;
                    else
                        MONO_VALUE=false;

                }

                return MONO_VALUE;
            }
        }
        public static char assignmentSymbol
        {
            get
            {
                if (Component.MONO)
                {
                    return '=';
                }
                else if (Component.BARTPE)
                {
                    return (char) 171;
                }
                else
                {
                    return '\u2190';
                }
            }
        }
		internal CommentBox My_Comment;
        public static Mode Current_Mode = Mode.Intermediate;
		public static bool USMA_mode = false;
        public static bool _reverse_loop_logic = false;
        public static bool reverse_loop_logic
        {
            get
            {
                return _reverse_loop_logic || (Current_Mode == Mode.Expert);
            }
            set
            {
                if (Current_Mode != Mode.Expert)
                {
                    _reverse_loop_logic = value;
                }
            }
        }
        public static bool BARTPE = false;
        public static string BARTPE_ramdrive_path = "x:\\";
        public static string BARTPE_partition_path = "y:\\";
        public static bool VM = true;
		public static bool negate_loops = false;
		public static int current_serialization_version 
        {
            get {
                if (Current_Mode == Mode.Expert)
                {
                    return 18;
                }
                else
                {
                    return 17;
                }
            }
        }
		public static bool compiled_flowchart = false;
        public static bool run_compiled_flowchart = false;
		public static bool warned_about_newer_version = false;
		public static bool warned_about_error = false;
		internal int incoming_serialization_version;
        internal static int last_incoming_serialization_version;
		protected bool has_breakpoint = false;
		public bool is_child = false;
		public bool is_beforeChild = false;
		public bool is_afterChild = false;
		public bool my_selected = false;
		public static bool text_visible = true;
		public static bool full_text = true;
		public static bool view_comments = true;
		// fonts for printing and screen report different sizes (yuck!)
		public static bool Inside_Print = false;
		public static bool Just_After_Print =false;

		public int height_of_text, width_of_text;
		public int char_length;
		internal Avalonia.Rect rect;
        public System.Collections.ArrayList method_expressions = new System.Collections.ArrayList();
        public System.Collections.ArrayList values = new System.Collections.ArrayList();
        public int number_method_expressions_run;
        internal bool need_to_decrease_scope;
        public int addExpression(object e)
        {
            return method_expressions.Add(e);
        }
        internal void addValue(numbers.value v)
        {
            values.Add(v);
        }
        internal numbers.value getValue(int i)
        {
            if (values[i] == null)
            {
                throw new System.Exception("method failed to return a value");
            }
            return values[i] as numbers.value;
        }
        public virtual void reset_this_method_expressions_run()
        {
            Runtime.setContext(null);
            this.number_method_expressions_run = 0;
            this.need_to_decrease_scope = false; 
            this.values.Clear();
        }
        public virtual void reset_number_method_expressions_run()
        {
            this.reset_this_method_expressions_run();

            if (this.Successor != null)
            {
                this.Successor.reset_number_method_expressions_run();
            }
        }
		public bool selected 
		{
			get 
			{
				return my_selected;
			}
			set
			{
				my_selected = value;
			}
		}
		public bool running = false;
		public float scale = 1.0f;
		public int head_height, head_width;
		public int head_heightOrig, head_widthOrig;
		public int connector_length;
		public int x_location, y_location;
		public Component Successor;
		public Component parent;
		public FootPrint FP;
		public String text_str = "";
		public String name = "";
		public int proximity = 10;
		public parse_tree.parseable parse_tree;
		public interpreter.syntax_result result;
		protected int drawing_text_width;
		public System.Guid created_guid;
		public System.Guid changed_guid;

		// Empty Class constructor
		public Component(int h, int w, String str_name)
		{
			head_height = h;     
			head_width = w;
			head_heightOrig = h;     
			head_widthOrig = w;
			name = str_name;
			FP = new FootPrint();
        	created_guid = System.Guid.NewGuid();
			changed_guid = created_guid;
		}   
  
		// Class constructor given the Successor
		public Component(Component S, int h, int w, String str_name)
		{
			Successor = S;
			head_height = h;
			head_width = w;
			head_heightOrig = h;     
			head_widthOrig = w;
			name = str_name;
			FP = new FootPrint();
			created_guid = System.Guid.NewGuid();
			changed_guid = created_guid;
		}
        
		//Deserialization constructor.
		public Component(SerializationInfo info, StreamingContext ctxt)
		{
			//Get the values from info and assign them to the appropriate properties
			incoming_serialization_version = (int)info.GetValue("_serialization_version", typeof(int));
			FP = (FootPrint)info.GetValue("_FP", typeof(FootPrint));
			text_str = (String)info.GetValue("_text_str", typeof(string));
			name = (String)info.GetValue("_name", typeof(string));
			proximity = (int)info.GetValue("_proximity", typeof(int));
			//running = (bool)info.GetValue("_running", typeof(bool));
			running = false;
			//scale = (float)info.GetValue("_scale", typeof(float));
			head_height = (int)info.GetValue("_head_height", typeof(int));
			head_width = (int)info.GetValue("_head_width", typeof(int));
			head_heightOrig = (int)info.GetValue("_head_heightOrig", typeof(int));
			head_widthOrig = (int)info.GetValue("_head_widthOrig", typeof(int));
			connector_length = (int)info.GetValue("_connector_length", typeof(int));
			x_location = (int)info.GetValue("_x_location", typeof(int));
			y_location = (int)info.GetValue("_y_location", typeof(int));
			height_of_text = (int)info.GetValue("_height_of_text", typeof(int));
			char_length = (int)info.GetValue("_char_length", typeof(int));
			Successor = (Component)info.GetValue("_Successor", typeof(Component));
			parent = (Component)info.GetValue("_parent", typeof(Component));
			is_child = (bool)info.GetValue("_is_child", typeof(bool));
			is_beforeChild = (bool)info.GetValue("_is_beforeChild", typeof(bool));
			is_afterChild = (bool)info.GetValue("_is_afterChild", typeof(bool));
			//my_selected = (bool)info.GetValue("_my_selected", typeof(bool));
			my_selected = false;
			full_text = (bool)info.GetValue("_full_text", typeof(bool));
			//rect = (Avalonia.Rect)info.GetValue("_rect", typeof(Avalonia.Rect));
			// here's a sample of how to extend into a later version
			// we need to:
			// 1) update current_serialization_version
			// 2) update writing below (or in whatever component)
			// 3) update reading below (or in whatever version) -- might
			//    add an else to provide a default initial value as needed.
			if (incoming_serialization_version>=3)
			{
				this.My_Comment = (CommentBox)info.GetValue("_comment", typeof(CommentBox));
				if (this.My_Comment!=null)
				{
					this.My_Comment.parent=this;
				}
			}
			if (incoming_serialization_version>=12)
			{
				created_guid = (System.Guid)info.GetValue("_created_guid", typeof(System.Guid));
				changed_guid = (System.Guid)info.GetValue("_changed_guid", typeof(System.Guid));
			}
			else
			{
				created_guid = System.Guid.NewGuid();
				changed_guid = created_guid;
			}
			if (incoming_serialization_version>current_serialization_version &&
				!warned_about_newer_version)
			{
				warned_about_newer_version = true;
				MessageBoxClass.Show("File created with newer version.\n"
					+"Not all features of this flowchart may work.\n"
					+"Suggest downloading latest version.", "Warning",
					MessageBoxButtons.OK, MessageBoxIcon.Warning);

			}
		}

		public void changed()
		{
			this.changed_guid = System.Guid.NewGuid();
		}

		public virtual bool break_now()
		{
			return this.has_breakpoint;
		}

		public virtual void Toggle_Breakpoint(int x, int y)
		{
			this.has_breakpoint = !this.has_breakpoint;
		}

		public virtual void Clear_Breakpoints()
		{
			this.has_breakpoint = false;
			if (this.Successor!=null)
			{
				this.Successor.Clear_Breakpoints();
			}
		}

		public virtual int Count_Symbols()
		{
			if (this.Successor!=null)
			{
				return 1 + this.Successor.Count_Symbols();
			}
			else
			{
				return 1;
			}
		}

		public virtual Component Find_Predecessor(Component c)
		{
			if (this.Successor==null)
			{
				return null;
			}
			else if (this.Successor==c)
			{
				return this;
			}
			else
			{
				return this.Successor.Find_Predecessor(c);
			}
	}

		//Serialization function.
		public virtual void GetObjectData(SerializationInfo info, StreamingContext ctxt)
		{
			//You can use any custom name for your name-value pair. But make sure you
			// read the values with the same name. For ex:- If you write EmpId as "EmployeeId"
			// then you should read the same with "EmployeeId"
			info.AddValue("_serialization_version", current_serialization_version);
			info.AddValue("_FP", FP);
			info.AddValue("_text_str", text_str);
			info.AddValue("_name", name);
			info.AddValue("_proximity", proximity);
			//info.AddValue("_running", running);
			//info.AddValue("_scale ", scale);
			info.AddValue("_head_height", head_height);
			info.AddValue("_head_width", head_width);
			info.AddValue("_head_heightOrig", head_heightOrig);
			info.AddValue("_head_widthOrig", head_widthOrig);
			info.AddValue("_connector_length", connector_length);
			info.AddValue("_x_location", x_location);
			info.AddValue("_y_location", y_location);
			info.AddValue("_Successor", Successor);
			info.AddValue("_parent", parent);
			info.AddValue("_is_child", is_child);
			info.AddValue("_is_beforeChild", is_beforeChild);
			info.AddValue("_is_afterChild", is_afterChild);
			//info.AddValue("_my_selected", my_selected);
			info.AddValue("_full_text", full_text);
			info.AddValue("_height_of_text", height_of_text);
			info.AddValue("_char_length", char_length);
			//info.AddValue("_rect", rect);
			info.AddValue("_comment", this.My_Comment);
			info.AddValue("_created_guid", this.created_guid);
			info.AddValue("_changed_guid", this.changed_guid);
		}
		
		public int X	//get or set the x location of object
		{
			set { x_location = value; }
			get { return x_location; }
		} 
        
		public int Y	//get or set the x location of object
		{
			set { y_location = value; }
			get { return y_location; }
		} 

		
		public int H	//get the head height of object
		{
			set {head_height = value; }
			get {return head_height; }
		} 

		public int W	//get the head width of object
		{
			set {head_width = value; }
			get {return head_width; }
		} 

		public String Text	//get or set the label of the object
		{
			set {text_str = value; }
			get {return text_str;}
		}

		public int CL	//get connector length of object
		{
			get 
			{
				connector_length = H/4;
				return connector_length; }
		} 

		public String Name	//get the name of the object
		{
			get {return name;}
		}

		public void reset()
		{
			this.is_child = false;
			this.is_beforeChild = false;
			this.is_afterChild = false;
			this.selected = false;
			this.running = false;
			this.parent = null;
			if (this.Successor!=null)
			{
				this.Successor.reset();
			}
		}

		// return the first thing of a component object.
		// It is itself for everything but loops
		public virtual Component First_Of()
		{
			return this;
		}

		// Get the comment text from a pop-up dialog and then set it?
		public virtual void addComment(Visual_Flow_Form form)
		{
			if (this.My_Comment == null)
			{
				form.Make_Undoable();
				this.My_Comment = new CommentBox(this);
				if (this.drawing_text_width > this.W)
				{
					this.My_Comment.X = (int) (((float) (this.drawing_text_width/2 + 20))/form.scale);
				}
				else
				{
					this.My_Comment.X = (int) (((float) (this.W/2+20))/form.scale);
				}
				this.My_Comment.Y = 0;
				this.My_Comment.Scale(form.scale);
			}
			this.My_Comment.setText(form);
		}


		// Get a copy of the object
		public virtual Component Clone()
		{
			Component Result = (Component)this.MemberwiseClone();
			Result.FP = this.FP.Clone();
			Result.selected=false;
			if (this.My_Comment != null)
			{
				Result.My_Comment = this.My_Comment.Clone();
				Result.My_Comment.parent = Result;
			}

			if (this.Successor != null)
			{
				Result.Successor = this.Successor.Clone();
			}

			return Result;
		}

		// Is (x, y) inside the object?
		public virtual bool contains(int x, int y)
		{
			int box_width;
			if (this.drawing_text_width>W)
			{
				box_width = this.drawing_text_width;
			}
			else
			{
				box_width = W;
			}
			bool bounded_x = Math.Abs(x-X) <= box_width/2;
			bool bounded_y = Math.Abs(y-(Y+H/2)) <= H/2;
			return bounded_x && bounded_y;
		}

		// added a small amount of allowed overlap so that
		// printing can always find a page break
		public virtual bool contains(Avalonia.Rect rec)
		{
			int box_width;
			if (this.drawing_text_width>W)
			{
				box_width = this.drawing_text_width;
			}
			else
			{
				box_width = W;
			}
			Avalonia.Rect my_rec = new Avalonia.Rect(X-box_width/2+2,Y,box_width-4,H);
			return rec.Intersects(my_rec);
		}

		public virtual bool editable_selected()
		{
			return this.selected;
		}

		public virtual bool SelectRegion(Avalonia.Rect rec)
		{
			// can't select if compiled
			if (Component.compiled_flowchart)
			{
				return false;
			}
			bool b=false;
			if (this.Successor!=null)
			{
				b = this.Successor.SelectRegion(rec);
			}
			if (this.contains(rec))
			{
				this.selected = true;
				return true;
			}
			else
			{
				this.selected = false;
			}
			return b;
		}

		public Component find_selection_end()
		{
			if (this.Successor!=null && this.Successor.selected)
			{
				return this.Successor.find_selection_end();
			}
			else
			{
				return this;
			}
		}

		// cut selected objects
		public virtual bool cut(Visual_Flow_Form VF)
		{
			// can't cut if compiled
			if (Component.compiled_flowchart)
			{
				return false;
			}
			Component start_selection, end_selection;
			if (this.Successor != null)
			{
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
					VF.clipboard = start_selection;
					VF.clipboard.reset();
					return true;
				}
				else
				{
					return this.Successor.cut(VF);
				}
			}
			else
			{
				return false;
			}
		}

		// copy selected objects
		public virtual bool copy(Visual_Flow_Form VF)
		{
			// can't copy if compiled
			if (Component.compiled_flowchart)
			{
				return false;
			}
			Component end_selection;
			if (this.selected)
			{
				end_selection = this.find_selection_end();
				if (end_selection.Successor != null)
				{
					Component temp = end_selection.Successor;
					end_selection.Successor = null;
					
					Component tempobj = this.Clone();
					tempobj.reset();
					
					VF.clipboard = tempobj;
					end_selection.Successor = temp;
					return true;
				}
				else
				{
					Component tempobj = this.Clone();
					tempobj.reset();
					
					VF.clipboard = tempobj;
					return true;
				}
			}
			else
			{
				if (this.Successor !=null)
				{
					return this.Successor.copy(VF);
				}
				else
				{
					return false;
				}
			}
		}

		// delete selected objects
		public virtual bool delete()
		{
			// can't delete if compiled
			if (Component.compiled_flowchart)
			{
				return false;
			}
			Component end_selection;
			if (this.Successor != null)
			{
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
					return this.Successor.delete();
				}
			}
			else
			{
				return false;
			}
		}


		// Draw the object
		public virtual void draw(Avalonia.Media.DrawingContext gr, int x, int y)
		{
			if (this.My_Comment != null && Component.view_comments)
			{
					this.My_Comment.draw(gr,x,y);
			}
		}

		public virtual Avalonia.Rect comment_footprint()
		{
			Avalonia.Rect result,succ_rec;
			if (Successor != null)
			{
				succ_rec = Successor.comment_footprint();
			}
			else
			{
				succ_rec = new Avalonia.Rect(0,0,0,0);
			}
			if (this.My_Comment != null && Component.view_comments)
			{
				result = this.My_Comment.Get_Bounds();
				return CommentBox.Union(result,succ_rec);
			}
			else
			{
				return succ_rec;
			}
		}

		public virtual void init()
		{
			FP.left = W/2;
			FP.right = W/2;
			FP.height = H;
		}
		public virtual void wide_footprint(Avalonia.Media.DrawingContext gr)
		{
			drawing_text_width = 0;
		}

		// What are the left and right widths and height of the object 
		// (including its children and successors)?
		public virtual void footprint(Avalonia.Media.DrawingContext gr)
		{
			this.init();
			if (Component.full_text && this.scale > 0.4)
			{
				this.wide_footprint(gr);
			}
			else
			{
				drawing_text_width = 0;
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
				FP.height = H + CL + Successor.FP.height;
			}
		}

		// Does the object and its successor/children have valid code
		public abstract bool has_code();

		// return the end of a linked list of components
		public Component find_end()
		{
			if (this.Successor==null)
			{
				return this;
			}
			else
			{
				return this.Successor.find_end();
			}
		}

		// copy all parent_info down the linked list
		public void set_parent_info(bool is_child, bool is_before_child,
			bool is_after_child, Component parent)
		{
			this.is_child = is_child;
			this.is_beforeChild = is_before_child;
			this.is_afterChild = is_after_child;
			this.parent = parent;
			if (this.Successor!=null)
			{
				this.Successor.set_parent_info(is_child,is_before_child,
					is_after_child,parent);
			}
		}

		// insert clipboard at a given x,y?
		// if newObj == null, then just check to see if can insert at this location
		public virtual bool insert(Component newObj, int x, int y, int connector_y)
		{
			// can't insert if compiled
			if (Component.compiled_flowchart)
			{
				return false;
			}
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
			else if (this.Successor != null)
			{
				return this.Successor.insert(newObj, x, y, connector_y);
			}
			else
			{
				return false;
			}
		}

		// Does the object and its successor/children have valid code
		public abstract void mark_error();

		public void Show_Guid()
		{
			Runtime.consoleWriteln(this.GetType().ToString() + " created: " +
				this.created_guid);
			Runtime.consoleWriteln(this.GetType().ToString() + " changed: " +
				this.changed_guid);
		}

		public virtual void Show_Guids()
		{
			this.Show_Guid();
			if (this.Successor!=null)
			{
				this.Successor.Show_Guids();
			}
		}

		// Is (x, y) over a line connected to an object?
		// connector_y is where we connect to if we don't have a
		// successor (provided by if_control or loop)
		public virtual bool overline(int x, int y, int connector_y)
		{
			if (this.Successor != null)
			{
				bool over_x = Math.Abs(x-X) < this.proximity;
				bool over_y = (y < Y+H+CL) && (y > Y+H);
				return over_x && over_y;
			}
			else
			{
				bool over_x = Math.Abs(x-X) < this.proximity;
				bool over_y = (y < connector_y) && (y > Y+H);
				return over_x && over_y;
			}

		}

		// Scale the object
		public virtual void Scale(float new_scale)
		{
			if (this.My_Comment != null)
			{
				this.My_Comment.Scale(new_scale);
			}
		}
		public virtual bool In_Footprint(int x, int y)
		{
			Avalonia.Rect rec =
				new Avalonia.Rect(
				this.X-this.FP.left,
				this.Y,
				this.FP.left+this.FP.right,
				this.FP.height);
			return rec.Contains(new Point(x,y));
		}

		// If (x, y) is over the object color it red?
		// If (x, y) is over the object color it red?
		public virtual Component select(int x, int y)
		{
			// can't select if compiled
			if (Component.compiled_flowchart)
			{
				return null;
			}
			this.selected = false;
			Component succ_selected=null;

			if (this.Successor != null)
			{
				succ_selected = this.Successor.select(x,y);
			}
			if (this.contains(x,y))
			{
				this.selected = true;
				return this;
			}
			return succ_selected;
		}

        public virtual Component Find_Component(int x, int y)
        {
            // can't find if compiled
            if (Component.compiled_flowchart)
            {
                return null;
            }
            Component succ_selected = null;

            if (this.Successor != null)
            {
                succ_selected = this.Successor.Find_Component(x, y);
            }
            if (this.contains(x, y))
            {
                return this;
            }
            return succ_selected;
        }

		// select me, all my successors and kids
		public virtual void selectAll()
		{
			this.selected = true;
			if (this.Successor != null)
			{
				this.Successor.selectAll();
			}
		}

		// If (x, y) is over the object color it red?
		public virtual CommentBox selectComment(int x, int y)
		{
			CommentBox succ_selected=null;

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
			
			if (succ_selected != null)
			{
				return succ_selected;
			}
			else
			{
				return null;
			}
		}
	
		// Get the text from a pop-up dialog and then set it?
		public abstract bool setText(int x, int y, Visual_Flow_Form form);

		// Get the text from a component?
		public abstract string getText(int x, int y);
	
		public static string unbreakString(string s)
		{
			string result = s.Replace("+"," + ").Replace("-"," - ").
				Replace("*"," * ").Replace("^"," ^ ").Replace(
				"/"," / ").Replace(",",", ");
			// watch out for the ** operator, which gets
			// mangled
			return result.Replace("*  *","**");
				
			//return s.Replace(' ','\u00A0').Replace('(','\uFD3E').
			//	Replace(')','\uFD3F').Replace('-','\u2212');
		}

		public virtual string getDrawText()
		{
			return unbreakString(this.Text);
		}

		public virtual void change_compressed(bool compressed)
		{
			if (this.Successor!=null)
			{
				this.Successor.change_compressed(compressed);
			}
		}

		public virtual bool child_running()
		{
			if (this.running)
			{
				return true;
			}
			if (this.Successor!=null)
			{
				return this.Successor.child_running();
			}
			return false;
		}

		public virtual bool check_expansion_click (int x, int y)
		{
			if (this.Successor!=null)
			{
				return this.Successor.check_expansion_click(x,y);
			}
			else
			{
				return false;
			}
		}

		public virtual bool Called_Tab(string s)
		{
			if (this.Successor!=null)
			{
				return this.Successor.Called_Tab(s);
			}
			else
			{
				return false;
			}
		}
		public virtual void Rename_Tab(string from, string to)
		{
			if (this.Successor!=null)
			{
				this.Successor.Rename_Tab(from,to);
			}
		}
		/*public virtual void Emit_Code(generate_interface.typ gen)
		{	
			if (this.parse_tree!=null)
			{
				interpreter_pkg.emit_code(this.parse_tree,this.Text,gen);
			}
			if (this.Successor!=null)
			{
				this.Successor.Emit_Code(gen);
			}
		}
		public virtual void compile_pass1(generate_interface.typ gen)
		{	
			if (this.parse_tree!=null)
			{
				interpreter_pkg.compile_pass1(this.parse_tree,this.Text,gen);
			}
			if (this.Successor!=null)
			{
				this.Successor.compile_pass1(gen);
			}
		}*/
        public virtual void collect_variable_names(System.Collections.Generic.IList<string> l,
            System.Collections.Generic.IDictionary<string,string> types)
        {
            if (this.Successor != null)
            {
                this.Successor.collect_variable_names(l,types);
            }
        }
	}
}
