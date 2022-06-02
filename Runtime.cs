using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
//using System.Windows.Forms;
using System.Data;
//using dotnetgraphlibrary;
//using PlaySound;
using RAPTOR_Avalonia_MVVM.ViewModels;
using ReactiveUI;

namespace raptor
{
	public class RuntimeException : Exception
    {
		public RuntimeException(string s) : base(s) { }
    }
	public class Variable : ReactiveObject
	{
			const int max_array_size = 10000;
			public string Var_Name;
			public string var_name{
				get{
					return Var_Name;
				}
				set{
					Var_Name = value;
				}
			}
			public string text{
				get{
					return Text;
				}
				set{
					Text = value;
				}
			}
			public string Text;
			public string Color = "Black";
			public string color
        	{
				get
				{
					return Color;
				}
				set{
					this.RaiseAndSetIfChanged(ref Color, value);
				}
        	}
			public Runtime.Variable_Kind Kind;
			public numbers.value Variable_Value;

			public ObservableCollection<Arr> values { get; set; } = new ObservableCollection<Arr>();
			private bool isConstant = false;
            //private NClass.Core.ClassType theClass;
            /*internal NClass.Core.ClassType getClass()
            {
                return theClass;
            }*/

			public void addToList(Variable v){
				MainWindowViewModel.GetMainWindowViewModel().theVariables.Add(v);
			}
			public Variable(string name) : base ()
			{
				Var_Name = name;
				Kind = Runtime.Variable_Kind.Scope;
				this.Text = "--" + name + "--";
			}

            // create a value variable
			public Variable(string name, numbers.value Value) : base() 
			{
				Var_Name = name;
				Kind = Runtime.Variable_Kind.Value;
				Variable_Value = Value;
				this.Text = name + ": " + numbers.Numbers.msstring_view_image(Value);
				color = "Red";
				addToList(this);
				Runtime.Add_To_Updated(this);
			}

			/*
            public Variable(NClass.Core.ClassType ct)
            {
                Var_Name = ct.Name;
                Kind = Variable_Kind.Class_Value;
                Variable_Value = numbers.Numbers.make_object_value((object) this);
                this.theClass = ct;
                this.Text = "<" + ct.Name + "> : static vars";
                this.isConstant = true;
                foreach (NClass.Core.Field f in ct.Fields)
                {
                    if (f.IsStatic)
                    {
                        addField(f);
                    }
                }
            }
            private void addField(NClass.Core.Field f)
            {
                if (f.Type.Contains("[][]"))
                {
                    this.Nodes.Add(new Variable(f.Name, 1, 1, numbers.Numbers.zero));
                }
                else if (f.Type.Contains("[]"))
                {
                    this.Nodes.Add(new Variable(f.Name, 1, numbers.Numbers.zero));
                }
                else
                {
                    numbers.value val;
                    if (f.InitialValue != null && f.InitialValue != "")
                    {
                        if (f.InitialValue.Contains("\""))
                        {
                            val = numbers.Numbers.make_string_value(f.InitialValue);
                        }
                        else
                        {
                            val = numbers.Numbers.make_number_value(f.InitialValue);
                        }
                    }
                    else
                    {
                        val = numbers.Numbers.zero;
                    }
                    Variable v = new Variable(f.Name, val);
                    this.Nodes.Add(v);
                    if (f.IsConstant || f.IsReadonly)
                    {
                        v.setConstant();
                    }
                }
            }
			*/
            // create a variable which is an instance of an object
            public Variable(string name, string class_name)
                : base()
            {
                /*Var_Name = name;
                Kind = Variable_Kind.Heap_Object;
                Variable_Value = numbers.Numbers.make_object_value((object) this);
                this.Text = "[" + this.GetHashCode() + "] : <" + class_name + ">";
                if (Component.Current_Mode != Mode.Expert)
                {
                    throw new System.Exception("can't create object unless in OO mode");
                }
                theClass = Runtime.parent.projectCore.findClass(class_name);
                if (theClass == null)
                {
                    throw new System.Exception("can't create object of class: " + class_name);
                }
                if (theClass.Modifier == NClass.Core.ClassModifier.Abstract)
                {
                    throw new System.Exception("can't create object of abstract class: " + class_name);
                }
                NClass.Core.ClassType ct = theClass;
                while (ct != null)
                {
                    foreach (NClass.Core.Field f in ct.Fields)
                    {
                        if (!f.IsStatic)
                        {
                            addField(f);
                        }
                    }
                    ct = ct.BaseClass;
                }
                Add_To_Updated(this);*/
            }


            
            // create a 1D array variable
			public Variable(string name, int index, numbers.value Value) : base() 
			{
				ObservableCollection<Arr> temp = new ObservableCollection<Arr>();

				if (index>max_array_size)
				{
					throw new Exception("array index " + index + " too large.");
				}
				
				Var_Name = name;
				Kind = Runtime.Variable_Kind.One_D_Array;
				
				temp.Add(new Arr(){name="Size", value=numbers.Numbers.make_value__3(index)});
                if (numbers.Numbers.is_string(Value))
                {
                    for (int i = 1; i < index; i++)
                    {
                       temp.Add(new Arr(){name="<" + i + ">", value=numbers.Numbers.make_string_value("")});
                    }
                }else if(numbers.Numbers.is_character(Value)){
					for (int i = 1; i < index; i++)
                    {
                       temp.Add(new Arr(){name="<" + i + ">", value=numbers.Numbers.make_value__3(0)});
                    }
				}
                else
                {
                    for (int i = 1; i < index; i++)
                    {
                        temp.Add(new Arr(){name="<" + i + ">", value=numbers.Numbers.make_value__2(0.0)});
                    }
                }
				temp.Add(new Arr(){name="<"+index+">",value=Value, color="Red"});
				this.Text = name + "[]";
				values = temp;
				color = "Red";
				MainWindowViewModel.GetMainWindowViewModel().theVariables.Add(this);
				Runtime.Add_To_Updated(this);
			}

			public Variable(string name, int index1, int index2,
				numbers.value Value) : base() 
			{
				if (index1*index2 > max_array_size)
				{
					throw new Exception("array indices " + index1 + "," +
						index2 + " too large.");
				}

				ObservableCollection<Arr> temp = new ObservableCollection<Arr>();
				Var_Name = name;
				Kind = Runtime.Variable_Kind.Two_D_Array;
				temp.Add(new Arr(){name="Rows",value=numbers.Numbers.make_value__3(index1)});
                if (numbers.Numbers.is_string(Value))
                {
                    for (int i = 1; i <= index1; i++)
                    {
						if(i != index1){
							ObservableCollection<Arr2> temp2 = new ObservableCollection<Arr2>();
							temp2.Add(new Arr2(){name="Size",value=numbers.Numbers.make_value__3(index2)});
							for(int k = 1; k <= index2; k++){
								temp2.Add(new Arr2(){name="<" + k + ">", value=numbers.Numbers.make_string_value("")});
							}
							temp.Add(new Arr(){name="<" + i + ">", values=temp2});
						} else{
							ObservableCollection<Arr2> temp2 = new ObservableCollection<Arr2>();
							temp2.Add(new Arr2(){name="Size",value=numbers.Numbers.make_value__3(index2)});
							for(int k = 1; k < index2; k++){
								temp2.Add(new Arr2(){name="<" + k + ">",value=numbers.Numbers.make_string_value("")});
							}
							temp2.Add(new Arr2(){name="<" + index2 + ">", value=Value, color="Red"});
							temp.Add(new Arr(){name="<" + i + ">", values=temp2, color="Red"});
						}
                    }
                } else if(numbers.Numbers.is_character(Value)){
					for (int i = 1; i <= index1; i++)
                    {
						if(i != index1){
							ObservableCollection<Arr2> temp2 = new ObservableCollection<Arr2>();
							temp2.Add(new Arr2(){name="Size",value=numbers.Numbers.make_value__3(index2)});
							for(int k = 1; k <= index2; k++){
								temp2.Add(new Arr2(){name="<" + k + ">", value=numbers.Numbers.make_value__3(0)});
							}
							temp.Add(new Arr(){name="<" + i + ">", values=temp2});
						} else{
							ObservableCollection<Arr2> temp2 = new ObservableCollection<Arr2>();
							temp2.Add(new Arr2(){name="Size",value=numbers.Numbers.make_value__3(index2)});
							for(int k = 1; k < index2; k++){
								temp2.Add(new Arr2(){name="<" + k + ">",value=numbers.Numbers.make_value__3(0)});
							}
							temp2.Add(new Arr2(){name="<" + index2 + ">", value=Value, color="Red"});
							temp.Add(new Arr(){name="<" + i + ">", values=temp2, color="Red"});
						}
                    }
				}
                else
                {
                    for (int i = 1; i <= index1; i++)
                    {
						if(i != index1){
							ObservableCollection<Arr2> temp2 = new ObservableCollection<Arr2>();
							temp2.Add(new Arr2(){name="Size",value=numbers.Numbers.make_value__3(index2)});
							for(int k = 1; k <= index2; k++){
								temp2.Add(new Arr2(){name="<" + k + ">",value=numbers.Numbers.make_value__2(0.0)});
							}
							temp.Add(new Arr(){name="<" + i + ">", values=temp2});
						} else{
							ObservableCollection<Arr2> temp2 = new ObservableCollection<Arr2>();
							temp2.Add(new Arr2(){name="Size",value=numbers.Numbers.make_value__3(index2)});
							for(int k = 1; k < index2; k++){
								temp2.Add(new Arr2(){name="<" + k + ">",value=numbers.Numbers.make_value__2(0.0)});
							}
							temp2.Add(new Arr2(){name="<" + index2 + ">", value=Value, color="Red"});
							temp.Add(new Arr(){name="<" + i + ">", values=temp2, color="Red"});
						}
                    }
                }
				this.Text = name + "[,]";
				values = temp;
				color = "Red";
				//MainWindowViewModel.GetMainWindowViewModel().theVariables.Add(new Variable(name, Value){values=temp});
				MainWindowViewModel.GetMainWindowViewModel().theVariables.Add(this);
				Runtime.Add_To_Updated(this);
			}

			public void Add_Rows(int current_count, int new_count, 
				int col_count) 
			{
				/*this.FirstNode.Text = "Rows: " + new_count;
				((Variable) this.FirstNode).Variable_Value =
					numbers.Numbers.make_value__3(new_count);
				Add_To_Updated((Variable) this.FirstNode);
				for (int index1=current_count+1; index1<=new_count;
					index1++) 
				{
					this.Nodes.Add(new Variable("<"+index1+">",
						col_count,numbers.Numbers.make_value__2(0.0)));
				}*/
			}
			public void Add_Cols(int current_count, int new_count, 
				int row_count) 
			{/*
				for (int index1=1; index1<=row_count;
					index1++) 
				{
					this.Nodes[index1].FirstNode.Text =
						"Size: " + new_count;
					((Variable) this.Nodes[index1].FirstNode).Variable_Value =
						numbers.Numbers.make_value__3(new_count);
					Add_To_Updated((Variable) this.Nodes[index1].FirstNode);
					for (int index2=current_count+1; index2<=new_count;
						index2++) 
					{
						this.Nodes[index1].Nodes.Add(
							new Variable("<"+index2+">",
							numbers.Numbers.make_value__2(0.0)));
					}
				}*/
			}
            public void setConstant()
            {
                this.isConstant = true;
            }
			public void set_value(numbers.value v)
			{
                if (!this.isConstant)
                {
                    this.Variable_Value = v;
                    this.Text = this.Var_Name + ": " + numbers.Numbers.msstring_view_image(v);
                    Runtime.Add_To_Updated(this);
                }
                else
                {
                    throw new System.Exception("can't assign to constant variable: " + this.Var_Name);
                }
			}

			public void Extend_1D(int index)
			{
				// how many entries are there now
				/*int count = numbers.Numbers.integer_of (((Variable) 
					(this.FirstNode)).Variable_Value);
				// do we need to extend?
				if (index > count)
				{
					((Variable) this.FirstNode).Variable_Value =
						numbers.Numbers.make_value__3(index);
					this.FirstNode.Text = "Size: " + index;
					Runtime.Add_To_Updated(
						(Variable) this.FirstNode);
					for (int i=count+1; i<=index; i++) 
					{
						this.Nodes.Add(new Variable("<"+i+">",numbers.Numbers.make_value__2(0.0)));
					}
				}*/
				
				int count = numbers.Numbers.integer_of(this.values[0].value);
				bool str = this.values[1].value.Kind == numbers.Value_Kind.String_Kind;
				if(index > count){
					this.values.RemoveAt(0);
					this.values.Insert(0, new Arr(){name="Size",value=numbers.Numbers.make_value__3(index)});
					for(int k = count+1; k <= index; k++){
						if(!str){
							this.values.Add(new Arr(){name="<" + k + ">", value=numbers.Numbers.make_value__2(0.0)});
						} else{
							this.values.Add(new Arr(){name="<" + k + ">", value=numbers.Numbers.make_string_value("")});
						}
					}
				}
				
				
			}
            public Variable? get_field_variable(string name)
            {
                /*foreach (Avalonia.Controls.TreeViewItem n in this.Nodes)
                {
                    if (n.Name == name)
                    {
                        return n as Variable;
                    }
                }*/
                return null;
            }
            public numbers.value get_field_1d(string name, int index)
            {
                Variable v = this.get_field_variable(name);
                if (v.Kind != Runtime.Variable_Kind.One_D_Array)
                {
                    throw new System.Exception("field " + name + " is not a 1D array");
                }
                return v.getArrayElement(index);
            }
            public numbers.value get_field_2d(string name, int index, int index2)
            {
                Variable v = this.get_field_variable(name);
                if (v.Kind != Runtime.Variable_Kind.Two_D_Array)
                {
                    throw new System.Exception("field " + name + " is not a 2D array");
                }
                return v.get2DArrayElement(index,index2);
            }
            public numbers.value get_field(string name)
            {
                /*foreach (Avalonia.Controls.TreeViewItem n in this.Nodes)
                {
                    if (n.Name == name)
                    {
                        if ((n as Variable).Kind == Variable_Kind.Value
                            || (n as Variable).Kind == Variable_Kind.Heap_Object)
                        {
                            return (n as Variable).Variable_Value;
                        }
                        else
                        {
                            throw new System.Exception("field " + name + " is " + (n as Variable).Kind);
                        }
                    }
                }*/
                throw new System.Exception("object doesn't have field: " + name);
            }
            public void set_field(string name, numbers.value f)
            {
                Variable field;
                if (this.Kind != Runtime.Variable_Kind.Heap_Object)
                {
                    throw new Exception("can't set field: " + name + "-- not an object.");
                }
                field = this.get_field_variable(name);
                if (field==null)
                {
                    throw new Exception("can't set field: " + name + "-- no such field.");
                }
                field.Text = name + ": " + numbers.Numbers.msstring_view_image(f);
                field.Variable_Value = f;
                Runtime.Add_To_Updated(this);
                Runtime.Add_To_Updated(field);
            }
			public void set_1D_value(int index, numbers.value f)
			{

				if (index>max_array_size)
				{
					throw new Exception("array index " + index + " too large.");
				}

				if (this.Kind==Runtime.Variable_Kind.Value && numbers.Numbers.is_string(this.Variable_Value))
				{
					int c;
                    if (numbers.Numbers.is_number(f))
                    {
                        c = (int)numbers.Numbers.integer_of(f);
                    }
                    else if (numbers.Numbers.is_character(f))
                    {
                        c = (int)f.C;
                    }
                    else if (numbers.Numbers.is_string(f) && numbers.Numbers.length_of(f) == 1)
                    {
                        c = (int)f.S[0];
                    }
                    else
                    {
                        throw new Exception("Can't assign " + numbers.Numbers.msstring_view_image(f) + " to position " + index);
                    }
                    if (c < 0 || c > 65535) 
					{
						throw new Exception("Character values only 0-65535, not " + c);
					}
					// pad with spaces
					string new_string;
					if (index > numbers.Numbers.length_of(this.Variable_Value))
					{
						new_string = this.Variable_Value.S +
							new String(' ',index-numbers.Numbers.length_of(this.Variable_Value)-1) + (char) c;
					}
					else
					{
						new_string = this.Variable_Value.S.Remove(index-1,1)
							.Insert(index-1,"" + (char) c);
					}
					this.Variable_Value = numbers.Numbers.make_string_value(new_string);
                    this.Text = this.Var_Name + ": " + '"' + new_string + '"';
                    Runtime.Add_To_Updated(this);
                    return;
				}
				else if (this.Kind==Runtime.Variable_Kind.One_D_Array) 
				{
					this.Extend_1D(index);
					ObservableCollection<Arr> var = this.values;
					var.RemoveAt(index);
					var.Insert(index, new Arr(){name="<" + index + ">", value=f, color="Red"});
                    //this.Nodes[index].Text = "<" + index + ">: " + numbers.Numbers.msstring_view_image(f);
					//((Variable) this.Nodes[index]).Variable_Value = f;
					Runtime.Add_To_Updated(this);
					//Add_To_Updated((Variable) this.Nodes[index]);
					return;
				}
				else
				{
					throw new Exception(this.Var_Name + " is not a 1D array");
				}
			}

			public void set_2D_value(int index1, int index2, numbers.value Value)
			{
				if (index1*index2 > max_array_size)
				{
					throw new Exception("array indices " + index1 + "," +
						index2 + " too large.");
				}

				int count = numbers.Numbers.integer_of(this.values[0].value);
				ObservableCollection<Arr> val1 = this.values;
				int curCols = numbers.Numbers.integer_of(val1[1].values[0].value);
				bool str = Value.Kind == numbers.Value_Kind.String_Kind;
				if(index1 > count){
					// handle adding more rows
					val1.RemoveAt(0);
					val1.Insert(0, new Arr(){name="Size",value=numbers.Numbers.make_value__3(index1)});
					for(int k = count+1; k <= index1; k++){
						ObservableCollection<Arr2> oc = new ObservableCollection<Arr2>();
						oc.Add(new Arr2(){name="Size", value=numbers.Numbers.make_value__3(curCols)});
						for(int j = 1; j <= curCols; j++){
							if(!str){
								oc.Add(new Arr2() {name="<" + j + ">", value=numbers.Numbers.make_value__2(0.0)});
							} else{
								oc.Add(new Arr2() {name="<" + j + ">", value=numbers.Numbers.make_string_value("")});
							}
						}
						if(!str){
							if(k == index1){
								val1.Add(new Arr(){name="<" + k + ">",value=numbers.Numbers.make_value__2(0.0), values=oc, color="Red"});
							}else{
								val1.Add(new Arr(){name="<" + k + ">",value=numbers.Numbers.make_value__2(0.0), values=oc});
							}
						} else{
							if(k == index1){
								val1.Add(new Arr(){name="<" + k + ">",value=numbers.Numbers.make_string_value(""), values=oc, color="Red"});
							}else{
								val1.Add(new Arr(){name="<" + k + ">",value=numbers.Numbers.make_string_value(""), values=oc});
							}
						}
					}
					count = index1;
				}
				val1[index1].color = "Red";
				for(int i = 1; i <= count; i++){
					ObservableCollection<Arr2> val2 = val1[i].values;
					int count2 = numbers.Numbers.integer_of(val2[0].value);
					if(index2 > count2){
						// handle adding more columns
						val2.RemoveAt(0);
						val2.Insert(0, new Arr2(){name="Size",value=numbers.Numbers.make_value__3(index2)});
						for(int k = count2+1; k <= index2; k++){
							if(!str){
								val2.Add(new Arr2() {name="<" + k + ">",value=numbers.Numbers.make_value__2(0.0)});
							} else{
								val2.Add(new Arr2() {name="<" + k + ">",value=numbers.Numbers.make_string_value("")});
							}
						}
						count2 = index2;
					}
					if(i == index1){
						val2.RemoveAt(index2);
						val2.Insert(index2, new Arr2(){name="<" + index2 + ">", value=Value, color="Red"});
					}
				}
			}
			public numbers.value get2DArrayElement(int index1, int index2)
			{
				//return numbers.Numbers.Null_Ptr;
				if (this.Kind==Runtime.Variable_Kind.Two_D_Array) 
				{
					ObservableCollection<Arr> val1 = this.values;
					ObservableCollection<Arr2> val2 = val1[1].values;
					int row_count = numbers.Numbers.integer_of(val1[0].value);
					int col_count = numbers.Numbers.integer_of(val2[0].value);
					if (row_count >= index1)
					{
						if (col_count >= index2)
						{
							//Variable newGuy = new Variable("Im new", val1[index1].values[index2].value);
							return (val1[index1].values[index2].value);
						}
						else
						{
							throw new Exception(this.Var_Name + " doesn't have " +
								index2 + " Columns.");
						}
					}
					else
					{
						throw new Exception(this.Var_Name + " doesn't have " +
							index1 + " Rows.");
					}
				}
				else
				{
					throw new Exception(this.Var_Name + 
						" is not a two-dimensional array");
				}
			}
			public numbers.value getArrayElement(int index)
			{
				//return numbers.Numbers.Null_Ptr;
				if (this.Kind==Runtime.Variable_Kind.One_D_Array) 
				{
					ObservableCollection<Arr> val = this.values;
					int count = numbers.Numbers.integer_of(val[0].value);
					if (count >= index)
					{
						return val[index].value;
					}
					else
					{
						throw new Exception(this.Var_Name + " doesn't have " +
							index + " elements.");
					}
				}
				/*else if (this.Kind==Runtime.Variable_Kind.Value && numbers.Numbers.is_string(this.Variable_Value)) 
				{
					if (numbers.Numbers.length_of (this.Variable_Value) >=
						index)
					{
						return numbers.Numbers.make_value__4 (this.Variable_Value.s[index-1]);
					}
					else
					{
						throw new Exception(this.Var_Name + " doesn't have " +
							index + " elements.");
					}
				}*/
				else
				{
					throw new Exception(this.Var_Name + 
						" is not a one-dimensional array");
				}
			}

			public int getArraySize()
			{
				/*if (this.Kind==Variable_Kind.One_D_Array) 
				{
					// first node is size of array
					return numbers.Numbers.integer_of(((Variable) 
						(this.FirstNode)).Variable_Value);
				}
				else if (this.Kind==Variable_Kind.Value && numbers.Numbers.is_string(this.Variable_Value))
				{
					return numbers.Numbers.length_of(this.Variable_Value);
				}
				else*/
				{
					throw new Exception(this.Var_Name + " is not a 1D array.");
				}
			}

			// only use this on a 2D array
			public int row_count()
			{
				return 0;
				//return numbers.Numbers.integer_of(((Variable) 
				//	(this.FirstNode)).Variable_Value);
			}

			// only use this on a 2D array
			public int col_count()
			{
				return 0;
				//return numbers.Numbers.integer_of(((Variable) 
				//	(this.Nodes[1].FirstNode)).Variable_Value);
			}

			public void Overwrite(Variable source) 
			{
				if (this.Kind==source.Kind)
				{
					throw new Exception("can't overwrite " + this.Text +
						" with " + source.Text);
				}
				switch (this.Kind) 
				{
					case Runtime.Variable_Kind.One_D_Array:
						this.Extend_1D(source.getArraySize());
						for (int i=1; i<source.getArraySize(); i++)
						{
							if (this.getArrayElement(i)!=source.getArrayElement(i))
							{
								this.set_1D_value(i,source.getArrayElement(i));
							}
						}
						break;
					case Runtime.Variable_Kind.Value:
                        try
                        {
                            if (!numbers.Numbers.Oeq(this.Variable_Value, source.Variable_Value))
                            {
                                this.set_value(source.Variable_Value);
                            }
                        }
                        catch
                        {
                            this.set_value(source.Variable_Value);
                        }
                        break;
					case Runtime.Variable_Kind.Two_D_Array:
						if (this.row_count()!=source.row_count())
						{
							this.Add_Rows(this.row_count(),source.row_count(),
								this.col_count());
						}
						if (this.col_count()!=source.col_count())
						{
							this.Add_Cols(this.col_count(),source.col_count(),
								this.row_count());
						}
						for (int i=1; i<=this.row_count(); i++)
						{
							for (int j=1; j<=this.col_count(); j++)
							{
								if (this.get2DArrayElement(i,j)!=
									source.get2DArrayElement(i,j))
								{
									this.set_2D_value(i,j,source.get2DArrayElement(i,j));
								}
							}
						}
						break;
				}
			}
		}

	public class Arr : ReactiveObject {
		private string Name;
		private numbers.value Value;

		public numbers.value value{
			get{
				return Value;
			}
			set{
				Value = value;
			}
		}
		public string name
		{
			get
			{
				return Name;
			}
			set
			{
				Name = value;
			}
		}
		public string Color = "Black";
			public string color
        	{
				get
				{
					return Color;
				}
				set{
					this.RaiseAndSetIfChanged(ref Color, value);
				}
        	}
		public ObservableCollection<Arr2> values { get; set;} = new ObservableCollection<Arr2>();
		public string displayStr {
			get {
				if(values.Count > 0){
					return name + "[]";
				} else{
					if(value.Kind == numbers.Value_Kind.Number_Kind){
						return name + ": " + value.V.ToString();
					}else if(value.Kind == numbers.Value_Kind.Character_Kind){
						return name + ": '" + value.C.ToString() + "'";
					} 
					else{
						return name + ": " + '"' + value.S + '"';
					}
				}
			}
		}
	}

	public class Arr2 : ReactiveObject{
		private string Name;
		private numbers.value Value;
		public numbers.value value{
			get{
				return Value;
			}
			set{
				Value = value;
			}
		}
		public string name
		{
			get
			{
				return Name;
			}
			set
			{
				Name = value;
			}
		}
		public string Color = "Black";
			public string color
        	{
				get
				{
					return Color;
				}
				set{
					this.RaiseAndSetIfChanged(ref Color, value);
				}
        	}
		public string displayStr {
			get {
				if(value.Kind == numbers.Value_Kind.Number_Kind){
					return name + ": " + value.V.ToString();
				} else if(value.Kind == numbers.Value_Kind.Character_Kind){
					return name + ": '" + value.C.ToString() + "'";
				} 
				else{
					return name + ": " + '"' + value.S + '"';
				}
			}
		}
	}

	
	/// <summary>
	/// Provides methods to set and get variables.
	/// </summary>
	public class Runtime
	{
		public static void Clear_Variables()
        {

        }
	
		// scoping rules (mcc: 11/25/03)
		// If there is only one scope, it won't have any marker
		// If there is more than one scope, each scope shall have a marker 
		// (variable of type scope)
		// The scope stack shall grow up (i.e. new scopes are added at the front of
		// the list)

		public static void resetColors(){
			ObservableCollection<Variable> var = MainWindowViewModel.GetMainWindowViewModel().theVariables;
			foreach(Variable v in var){
					if(v.Kind == Variable_Kind.One_D_Array){
						foreach(Arr a in v.values){
							a.color = "Black";
						}
					} else if(v.Kind == Variable_Kind.Two_D_Array){
						foreach(Arr a in v.values){
							foreach(Arr2 a2 in a.values){
								a2.color = "Black";
							}
							a.color = "Black";
						}
					}
					v.color = "Black";
				}
		}
        public static bool processing_parameter_list = false;
        private static bool superContext = false;
        public enum Parameter_Kind
        {
            Value, One_D_Array, Two_D_Array
        }
		public enum Variable_Kind 
		{
			Value, Class_Value, One_D_Array, Two_D_Array, Heap_Object, Scope}
		private static ArrayList updated_list = new ArrayList();
        public static bool isObjectOriented() {
            return Component.Current_Mode == Mode.Expert;
        }
        public static numbers.value? method_return_value = null;

		/*private static void Clear_Updated_Delegate()
		{
			IEnumerator enumerator = updated_list.GetEnumerator();
			Variable temp;

			while (enumerator.MoveNext())
			{
				temp = (Variable)enumerator.Current;
				temp.Foreground = Avalonia.Media.Brushes.Black;
			}
			updated_list.Clear();
		}
		
		
		private delegate void Clear_Updated_Delegate_Type();
		private static Clear_Updated_Delegate_Type clear_updated_delegate=
			new Clear_Updated_Delegate_Type(Clear_Updated_Delegate);
		*/
        private static void Parent_Focus_Delegate(Avalonia.Controls.Window form)
        {
            form.Focus();
        }
        private delegate void Parent_Focus_Delegate_Type(Avalonia.Controls.Window form);
        private static Parent_Focus_Delegate_Type parent_focus_delegate =
            new Parent_Focus_Delegate_Type(Parent_Focus_Delegate);
        
        public static void Clear_Updated()
		{
			//watchBox.Invoke(clear_updated_delegate,null);
		}

/*		private static void Add_To_Updated_Delegate(Variable temp)
		{
			temp.Foreground = Avalonia.Media.Brushes.Red;
			updated_list.Add(temp);
		}
		private delegate void Add_To_Updated_Delegate_Type(Variable temp);
		private static Add_To_Updated_Delegate_Type add_to_updated_delegate =
			new Add_To_Updated_Delegate_Type(Add_To_Updated_Delegate);
		*/public static void Add_To_Updated(Variable temp)
		{
			Object[] parameters=new Object[1];
			parameters[0]=temp;
			//watchBox.Invoke(add_to_updated_delegate,parameters);
		}

		public delegate void Add_Node_Delegate(Avalonia.Controls.TreeViewItem node);
		
		public static void Add_Node_To_WatchBox(Avalonia.Controls.TreeViewItem node) 
		{
			/*for (int i=0; i<watchBox.Nodes.Count; i++) 
			{
				// added code here for scopes-- a scope is marked by its kind
				// add if we've found alphabetically, or if we're about to cross
				// a scope boundary (found a scope not in the first position)
				if ((System.String.Compare(watchBox.Nodes[i].Text,node.Text)>0 &&
					   ((Variable) watchBox.Nodes[i]).Kind!=Variable_Kind.Scope) || 
					(((Variable) watchBox.Nodes[i]).Kind==Variable_Kind.Scope && i>0) ||
                    (((Variable)watchBox.Nodes[i]).Kind == Variable_Kind.Scope &&
                       (watchBox.Nodes[i] as Variable).Var_Name.CompareTo("Classes") == 0) ||
                    (((Variable)watchBox.Nodes[i]).Kind == Variable_Kind.Scope && 
                       (watchBox.Nodes[i] as Variable).Var_Name.CompareTo("Heap") == 0))
				{
					watchBox.Nodes.Insert(i,node);
					return;
				}
			}
			watchBox.Nodes.Add(node);*/
		}

		public static Add_Node_Delegate add_delegate =
			new Add_Node_Delegate(Add_Node_To_WatchBox);


		public static void Add_Node_To_Front_Of_WatchBox(Avalonia.Controls.TreeViewItem node) 
		{
			//watchBox.Nodes.Insert(0,node);
		}
        public static int findClassesScope()
        {
           /* for (int i = 0; i < watchBox.Nodes.Count; i++)
            {
                // added code here for scopes-- a scope is marked by its kind
                // add if we've found alphabetically, or if we're about to cross
                // a scope boundary (found a scope not in the first position)
                if (((Variable)watchBox.Nodes[i]).Kind == Variable_Kind.Scope &&
                    (watchBox.Nodes[i] as Variable).Var_Name.CompareTo("Classes") == 0)
                {
                    return i;
                }
            }*/
            return -1;
        }
        public static int findHeapScope()
        {/*
            for (int i = 0; i < watchBox.Nodes.Count; i++)
            {
                // added code here for scopes-- a scope is marked by its kind
                // add if we've found alphabetically, or if we're about to cross
                // a scope boundary (found a scope not in the first position)
                if (((Variable)watchBox.Nodes[i]).Kind == Variable_Kind.Scope && 
                    (watchBox.Nodes[i] as Variable).Var_Name.CompareTo("Heap")==0)
                {
                    return i;
                }
            }*/
            return -1;
        }
        public static void Add_Node_To_Heap(Avalonia.Controls.TreeViewItem node)
        {/*
            int heap_scope = findHeapScope();
            if (heap_scope==-1)
            {
                heap_scope = watchBox.Nodes.Add(new Variable("Heap"));
            }
            int i = heap_scope+1;
            while (i < watchBox.Nodes.Count)
            {
                if (node.GetHashCode() < (numbers.Numbers.object_of((watchBox.Nodes[i] as Variable).
                    Variable_Value).GetHashCode()))
                {
                    watchBox.Nodes.Insert(i, node);
                    return;
                }
                i++;
            }
            watchBox.Nodes.Add(node);*/
        }
        public static void Add_Node_To_Classes(Avalonia.Controls.TreeViewItem node)
        {
           /* int classes_scope = findClassesScope();
            int heap_scope = findHeapScope();
            if (heap_scope == -1)
            {
                heap_scope = watchBox.Nodes.Count;
            }
            if (classes_scope == -1)
            {
                classes_scope = watchBox.Nodes.Add(new Variable("Classes"));
            }
            int i = classes_scope + 1;
            while (i < heap_scope)
            {
                if ((node as Variable).Var_Name.CompareTo((watchBox.Nodes[i] as Variable).Var_Name)<0)
                {
                    watchBox.Nodes.Insert(i, node);
                    return;
                }
                i++;
            }
            watchBox.Nodes.Add(node);*/
        }
        public static Add_Node_Delegate add_at_front_delegate =
			new Add_Node_Delegate(Add_Node_To_Front_Of_WatchBox);
        public static Add_Node_Delegate add_to_heap_delegate =
            new Add_Node_Delegate(Add_Node_To_Heap);
        public static Add_Node_Delegate add_to_classes_delegate =
            new Add_Node_Delegate(Add_Node_To_Classes);
		public delegate void Delete_Scope_Delegate();

		public static void Delete_Scope_From_WatchBox() 
		{
			// delete the first scope marker
			ObservableCollection<Variable> vals = MainWindowViewModel.GetMainWindowViewModel().theVariables;
			vals.RemoveAt(0);
			while (vals[0].Kind!=Variable_Kind.Scope)
			{
				vals.RemoveAt(0);
			}
            //Clear_Updated_Delegate();
            System.Diagnostics.Trace.WriteLine(vals.Count);
		}

		public static Delete_Scope_Delegate delete_scope =
			new Delete_Scope_Delegate(Delete_Scope_From_WatchBox);

		

        internal static void consoleWriteln(string v)
        {
            throw new NotImplementedException();
        }

        // Container holding all variables and their values
        public static Avalonia.Controls.Window parent;
		private static RAPTOR_Avalonia_MVVM.ViewModels.MasterConsoleViewModel console;
		private static Avalonia.Controls.TreeView watchBox;
		public Runtime()
		{

		}
		 

		public static void Init(Avalonia.Controls.Window p, 
			RAPTOR_Avalonia_MVVM.ViewModels.MasterConsoleViewModel MC,
			Avalonia.Controls.TreeView watch) 
		{
			parent = p;
			console = MC;
			watchBox = watch;
			//Clear_Variables();
            //dotnetgraphlibrary.dotnetgraph.Init(p);
			//Runtime.set2DArrayElement("q",3,5,9.9f);
		}


		public static int Count_Symbols(Oval start)
		{
			return start.Count_Symbols();
		}

		public static void updateWatchBox() 
		{
		}

		public static bool getAnyVariable(string s){
			return Runtime.Lookup_Variable(s) != null;
		}

		public static bool getAnyVariable(string s, string sc){
			ObservableCollection<Variable> vars = MainWindowViewModel.GetMainWindowViewModel().theVariables;
			int spot = -1;
			int varCount = 0;
			for(int i = 0; i < vars.Count; i++){
				if(vars[i].Text == "--" + sc + "--"){
					spot = i+1;
					break;
				}
				varCount++;
			}
			for(int i = spot; i < vars.Count; i++){
				if(vars[i].Text.Contains("--")){
					//Variable jdhfg = new Variable(vars[i].Text.Replace("--","") + " found!" ,new numbers.value(){V=666});
					break;
				}else{
					if(vars[i].Text.IndexOf(":") != -1){
						if(s.Substring(0, s.IndexOf(":")) == vars[i].Text.Substring(0, vars[i].Text.IndexOf(":"))){
							return true;
						}
					}
				}
			}
			return false;
		}

		public static Variable getAnyVariableRef(string s, string sc){
			ObservableCollection<Variable> vars = MainWindowViewModel.GetMainWindowViewModel().theVariables;
			int spot = -1;
			int varCount = 0;
			for(int i = 0; i < vars.Count; i++){
				if(vars[i].Text == "--" + sc + "--"){
					spot = i+1;
					break;
				}
				varCount++;
			}
			for(int i = spot; i < vars.Count; i++){
				if(vars[i].Text.Contains("--")){
					//Variable jdhfg = new Variable(vars[i].Text.Replace("--","") + " found!" ,new numbers.value(){V=666});
					break;
				}else{
					if(vars[i].Text.IndexOf(":") != -1 && s.IndexOf(":") != -1){
						if(s.Substring(0, s.IndexOf(":")) == vars[i].Text.Substring(0, vars[i].Text.IndexOf(":"))){
							return vars[i];
						}
					}
				}
			}
			return null;
		}

		//****************************************************
		// getVariable
		// Returns the current value of an existing variable
		// as a double or return the smallest integer if the
		// variable does not currently exist.
		//****************************************************
		public static numbers.value getVariable(string s)
		{
			Variable? temp=Runtime.Lookup_Variable(s);

			if (temp!=null)
			{
				if (temp.Kind==Variable_Kind.Value || 
                    temp.Kind==Variable_Kind.Class_Value) 
				{
					return temp.Variable_Value;  // variable found
				}
				else if (temp.Kind == Variable_Kind.One_D_Array && !processing_parameter_list)
				{
					throw new Exception("1D Array " + s + " must be indexed here (e.g. " + s + "[3]).");
				}
                else if (temp.Kind == Variable_Kind.Two_D_Array && !processing_parameter_list)
				{
					throw new Exception("2D Array " + s + " must be indexed here (e.g. " + s + "[3,3]).");
				}
                else if (temp.Kind == Variable_Kind.One_D_Array && processing_parameter_list)
                {
                    return numbers.Numbers.make_1d_ref(temp);
                }
                else if (temp.Kind == Variable_Kind.Two_D_Array && processing_parameter_list)
                {
                    return numbers.Numbers.make_2d_ref(temp);
                }
            }
            if (temp!=null && temp.Kind == Variable_Kind.Scope &&
                Component.Current_Mode == Mode.Expert)
            {
                Variable? t = findStaticClass(s);
                return t.Variable_Value;
            }
            else
            {
                //variable not found -- throw an exception
                throw new Exception("Variable " + s + " not found!");
            }
            
		}
		/*
		private delegate numbers.value getVariable_Delegate_Type(string s);
		private static getVariable_Delegate_Type getVariable_delegate=
			new getVariable_Delegate_Type(getVariable_Delegate);
		public static numbers.value getVariable(string s)
		{
			Object[] parameters = new Object[1];
			parameters[0]=s;
			return null;
			//return (numbers.value) watchBox.Invoke(getVariable_delegate,parameters);
		}

        public static Object getVariableContext(string s)
        {
            Object[] parameters = new Object[1];
            parameters[0] = s;
			return null;
            //return numbers.Numbers.object_of((numbers.value)watchBox.Invoke(getVariable_delegate, parameters));
        }
		*/
		//****************************************************
		// getVariable
		// Returns the current value of an existing variable
		// as a double or return the smallest integer if the
		// variable does not currently exist.
		//****************************************************
        private static Variable? context;
        public static void setContext(object? c)
        {
            context = c as Variable;
        }
        public static object getContextObject()
        {
            return context as object;
        }
        internal static Variable getContext()
        {
            return context;
        }
        private static Variable? findStaticClass(string s)
        {
            Variable temp;
            /*int j = findClassesScope();
            if (j < 0) return null;
            for (int i = j+1; i < watchBox.Nodes.Count; i++)
            {
                temp = (Variable)watchBox.Nodes[i];
                if (temp.Var_Name.ToLower() == s.ToLower())
                {
                    return temp;
                }
                else if (temp.Kind == Variable_Kind.Scope)
                {
                    return null;
                }
            }*/
            return null;
        }
        public static Variable? Lookup_Variable(string s)
		{
			Variable temp;
			/*
            if (s.ToLower().Equals("super"))
            {
                temp = Lookup_Variable("this");
                Runtime.superContext = true;
                return temp;
            }
			*/
            Runtime.superContext = false;
            if (context == null)
            {
                // loop through the top level looking for s
               for (int i = 0; i < MainWindowViewModel.GetMainWindowViewModel().theVariables.Count; i++)
                {
                    temp = (Variable)MainWindowViewModel.GetMainWindowViewModel().theVariables[i];
                    if (temp.Var_Name.ToLower() == s.ToLower())
                    {
						//temp = new Variable(s, temp.Variable_Value);
                        return MainWindowViewModel.GetMainWindowViewModel().theVariables[i];
                    }
                    else if (temp.Kind == Variable_Kind.Scope && ((i > 0) || (temp.Var_Name=="Classes")))
                    {
                        if (Component.Current_Mode == Mode.Expert)
                        {
                            return findStaticClass(s);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            else // context is not null
            {
                // loop through the top level looking for s
                /*foreach (Avalonia.Controls.TreeViewItem node in context.Nodes)
                {
                    if ((node as Variable).Var_Name.ToLower() == s.ToLower())
                    {
                        return node as Variable;
                    }
                }*/

                throw new Exception(s + ": not found!");

            }
			return null;
		}

		//****************************************************
		// setVariable
		// Sets the current value of an existing variable
		// or adds the variable and its value to the list if
		// it does not currently exist.
		//****************************************************
		public static void setVariable(string s, numbers.value f)
		{
			Variable temp=Runtime.Lookup_Variable(s);

			if (temp!=null)
			{
				temp.color = "Red";
				
				if (temp.Kind==Variable_Kind.Value || temp.Kind==Variable_Kind.Heap_Object) 
				{
					ObservableCollection<Variable> var =MainWindowViewModel.GetMainWindowViewModel().theVariables;
					int index=var.IndexOf(temp);
					var.RemoveAt(index);
					temp.set_value(f);
					resetColors();
					var.Insert(index,temp);
					return;
				}
                else if (temp.Kind == Variable_Kind.Class_Value)
                {
                    throw new Exception("Invalid assignment to class " + s);
                }
                else if (temp.Kind == Variable_Kind.One_D_Array)
                {
                    throw new Exception("Invalid assignment to array " + s + " -- missing index (e.g. " +
                        s + "[3]).");
                }
                else if (temp.Kind == Variable_Kind.Two_D_Array)
                {
                    throw new Exception("Invalid assignment to 2D array " + s + " -- missing indices (e.g. " +
                        s + "[3,3]).");
                }
				
			}
			else
			{
				// variable doesn't currently exist
				// add variable and initial value to variableList
				resetColors();
				temp = new Variable(s,f);
			}
			
		}
		/*
		private delegate void setVariable_Delegate_Type(string s,
			numbers.value f);
		private static setVariable_Delegate_Type setVariable_delegate=
			new setVariable_Delegate_Type(setVariable_Delegate);
		public static void setVariable(string s, numbers.value f)
		{
			Object[] parameters = new Object[2];
			parameters[0]=s;
			parameters[1]=f;
			// added this guard in case you've pushed stop
			//if (parent.runningState || Component.compiled_flowchart || Component.run_compiled_flowchart)
			{
				//watchBox.Invoke(setVariable_delegate,parameters);
			}
		}
		*/
        internal static numbers.value[] getValueArray(Variable temp)
        {
			int count = 2; // numbers.Numbers.integer_of(((Variable)
                //(temp.FirstNode)).Variable_Value);
            numbers.value[] result = new numbers.value[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = temp.getArrayElement(i + 1);
            }
            return result;
        }

        public static numbers.value[] getValueArray(string s)
        {
            Variable temp = Runtime.Lookup_Variable(s);

            if (temp != null)
            {
                if (temp.Kind == Variable_Kind.One_D_Array)
                {
                    return getValueArray(temp);
                }
                else
                {
                    throw new Exception(s +
                        " is not a one-dimensional array");
                }
            }
            throw new Exception(s + " not found.");
        }

		public static numbers.value[] getArray(string s)
		{
			//return null;
			Variable temp=Runtime.Lookup_Variable(s);
			ObservableCollection<Variable> vars = MainWindowViewModel.GetMainWindowViewModel().theVariables;

			if (temp!=null)
			{
				if (temp.Kind==Variable_Kind.One_D_Array) 
				{
					int count = numbers.Numbers.integer_of (temp.values[0].value);
					numbers.value[] result = new numbers.value[count];
					for (int i=0; i<count; i++) 
					{
						result[i] = temp.getArrayElement(i+1);
					}
					return result;
				}
				else
				{
					throw new Exception(s + 
						" is not a one-dimensional array");
				}
			}
			throw new Exception(s + " not found.");
		}

		public static int[] getIntArray(string s)
		{
			/*Variable temp=Runtime.Lookup_Variable(s);

			if (temp!=null)
			{
				if (temp.Kind==Variable_Kind.One_D_Array) 
				{
					int count = numbers.Numbers.integer_of(((Variable) 
						(temp.FirstNode)).Variable_Value);
					int[] result = new int[count];
					for (int i=0; i<count; i++) 
					{
						result[i] = (int) numbers.Numbers.long_float_of(temp.getArrayElement(i+1));
					}
					return result;
				}
				else
				{
					throw new Exception(s + 
						" is not a one-dimensional array");
				}
			}*/
			throw new Exception(s + " not found.");
		}
        public static object getVariableContext1D(string s, int index)
        {
            if (index <= 0)
            {
                throw new Exception("can't use " + index +
                    " as an array index");
            }
            Variable temp = Runtime.Lookup_Variable(s);

            if (temp != null)
            {
                return numbers.Numbers.object_of(temp.getArrayElement(index));
            }
            throw new Exception(s + " not found.");
        }
        public static object getVariableContext2D(string s,
            int index1, int index2)
        {
            if (index1 <= 0)
            {
                throw new Exception("can't use " + index1 +
                    " as an array index");
            }
            if (index2 <= 0)
            {
                throw new Exception("can't use " + index2 +
                    " as an array index");
            }
            Variable temp = Runtime.Lookup_Variable(s);

            if (temp != null)
            {
                return numbers.Numbers.object_of(temp.get2DArrayElement(index1, index2));
            }
            throw new Exception(s + " not found.");
        }

		//****************************************************
		// getArrayElement
		// Returns the current value of an element in an array
		// as a double.
		//****************************************************
		public static numbers.value getArrayElement(string s, int index)
		{
			if (index <= 0) 
			{
				throw new Exception("can't use " + index +
					" as an array index");
			}
			Variable temp=Runtime.Lookup_Variable(s);

			if (temp!=null)
			{
			    return temp.getArrayElement(index);
			}
			throw new Exception(s + " not found.");
		}

		public static double[][] get2DArray(string s)
		{
			Variable temp=Runtime.Lookup_Variable(s);

			if (temp!=null)
			{
				if (temp.Kind==Variable_Kind.Two_D_Array) 
				{
					int row_count = temp.row_count();
					int col_count = temp.col_count();
					double[][] result = new double[row_count][];
					for (int i=0; i<row_count; i++) 
					{
						result[i] = new double[col_count];
						for (int j=0; j<col_count; j++) 
						{
							result[i][j] = numbers.Numbers.long_float_of(temp.get2DArrayElement(i+1,j+1));
						}
					}
					return result;
				}
				else
				{
					throw new Exception(s + 
						" is not a two-dimensional array");
				}
			}
			throw new Exception(s + " not found.");
		}
        public static numbers.value[][] get2DValueArray(string s)
        {
            Variable temp = Runtime.Lookup_Variable(s);

            if (temp != null)
            {
                if (temp.Kind == Variable_Kind.Two_D_Array)
                {
                    return get2DValueArray(temp);
                }
                else
                {
                    throw new Exception(s +
                        " is not a two-dimensional array");
                }
            }
            throw new Exception(s + " not found.");
        }

        internal static numbers.value[][] get2DValueArray(Variable temp)
        {
            int row_count = temp.row_count();
            int col_count = temp.col_count();
            numbers.value[][] result = new numbers.value[row_count][];
            for (int i = 0; i < row_count; i++)
            {
                result[i] = new numbers.value[col_count];
                for (int j = 0; j < col_count; j++)
                {
                    result[i][j] = temp.get2DArrayElement(i + 1, j + 1);
                }
            }
            return result;
        }
        public static int[][] get2DIntArray(string s)
		{
			Variable temp=Runtime.Lookup_Variable(s);

			if (temp!=null)
			{
				if (temp.Kind==Variable_Kind.Two_D_Array) 
				{
					int row_count = temp.row_count();
					int col_count = temp.col_count();
					int[][] result = new int[row_count][];
					for (int i=0; i<row_count; i++) 
					{
						result[i] = new int[col_count];
						for (int j=0; j<col_count; j++) 
						{
							result[i][j] = numbers.Numbers.integer_of(temp.get2DArrayElement(i+1,j+1));
						}
					}
					return result;
				}
				else
				{
					throw new Exception(s + 
						" is not a two-dimensional array");
				}
			}
			throw new Exception(s + " not found.");
		}
		//****************************************************
		// get2DArrayElement
		// Returns the current value of an element in an array
		// as a double.
		//****************************************************
		public static numbers.value get2DArrayElement(string s, 
			int index1, int index2)
		{
			if (index1 <= 0) 
			{
				throw new Exception("can't use " + index1 +
					" as an array index");
			}
			if (index2 <= 0) 
			{
				throw new Exception("can't use " + index2 +
					" as an array index");
			}
			Variable temp=Runtime.Lookup_Variable(s);

			if (temp!=null)
			{
				return temp.get2DArrayElement(index1,index2);
			}
			throw new Exception(s + " not found.");
		}
		//****************************************************
		// getArraySize
		// Returns the current size of an array as a double.
		//****************************************************
		public static int getArraySize(string s)
		{
			Variable temp=Runtime.Lookup_Variable(s);

			if (temp!=null)
			{
				return temp.getArraySize();
			}
			throw new Exception(s + " not found.");
		}

		//****************************************************
		// setArrayElement
		// Sets the current value of an array element
		// or adds the variable and its value to the array if
		// it does not currently exist.
		//****************************************************
		public static void setArrayElement(string s, int index, numbers.value f)
		{
			if (index <= 0) 
			{
				throw new Exception("can't use " + index +
					" as an array index");
			}
			Variable temp = Runtime.Lookup_Variable(s);

			if (temp!=null)
			{
				resetColors();
				temp.color = "Red";
				ObservableCollection<Variable> var=MainWindowViewModel.GetMainWindowViewModel().theVariables;
				int i=var.IndexOf(temp);
				var.RemoveAt(i);
				temp.set_1D_value(index,f);
				var.Insert(i,temp);
				return;
			}

			// variable doesn't currently exist
			// add variable and initial value to variableList
			resetColors();
			temp = new Variable(s, index, f);
			try 
			{
				object[] args = new Object[1];
				args[0] = temp;
				//watchBox.Invoke(add_delegate,args);
			}
			catch (System.Exception e)
			{
				Console.WriteLine(e.Message);
			}


		}
		/*
		private delegate void setArrayElement_Delegate_Type(string s,
			int index, numbers.value f);
		private static setArrayElement_Delegate_Type 
			setArrayElement_delegate=
			new setArrayElement_Delegate_Type(setArrayElement_Delegate);
		public static void setArrayElement(string s, 
			int index, numbers.value f)
		{
			Object[] parameters = new Object[3];
			parameters[0]=s;
			parameters[1]=index;
			parameters[2]=f;
			//watchBox.Invoke(setArrayElement_delegate,parameters);
		}
		*/

		//****************************************************
		// set2DArrayElement
		// Sets the current value of an array element
		// or adds the variable and its value to the array if
		// it does not currently exist.
		//****************************************************
		public static void set2DArrayElement(string s, int index1, 
			int index2, numbers.value f)
		{
			if (index1 <= 0) 
			{
				throw new Exception("can't use " + index1 +
					" as an array index");
			}
			if (index2 <= 0) 
			{
				throw new Exception("can't use " + index2 +
					" as an array index");
			}
			Variable temp=Runtime.Lookup_Variable(s);

			if (temp!=null)
			{
				
				if (temp.Kind==Variable_Kind.Two_D_Array) 
				{
					resetColors();
					temp.color = "Red";
					ObservableCollection<Variable> var=MainWindowViewModel.GetMainWindowViewModel().theVariables;
					int i=var.IndexOf(temp);
					var.RemoveAt(i);
					temp.set_2D_value(index1,index2,f);
					var.Insert(i,temp);
					return;
				}
				else
				{
					throw new Exception(s + " is not a 2D array");
				}
			}

			// variable doesn't currently exist
			// add variable and initial value to variableList
			resetColors();
			temp = new Variable(s, index1, index2, f);
			try 
			{
				object[] args = new Object[1];
				args[0] = temp;
				//watchBox.Invoke(add_delegate,args);
				//watchBox.Nodes.Add(temp);
			}
			catch (System.Exception e)
			{
				Console.WriteLine(e.Message);
			}

		}

/*		private delegate void set2DArrayElement_Delegate_Type(string s,
			int index1, int index2, numbers.value f);
		private static set2DArrayElement_Delegate_Type 
			set2DArrayElement_delegate=
			new set2DArrayElement_Delegate_Type(set2DArrayElement_Delegate);
		public static void set2DArrayElement(string s, 
			int index1, int index2, numbers.value f)
		{
			Object[] parameters = new Object[4];
			parameters[0]=s;
			parameters[1]=index1;
			parameters[2]=index2;
			parameters[3]=f;
			//watchBox.Invoke(set2DArrayElement_delegate,parameters);
		}
*/
		public static void Increase_Scope(string s)
		{
			ObservableCollection<Variable> vars = MainWindowViewModel.GetMainWindowViewModel().theVariables;
			// if there aren't any variables, or the first one isn't a
			// scope (meaning only one scope)
			if (vars.Count==0 || vars[0].Kind!=Variable_Kind.Scope || vars[0].Var_Name.CompareTo("Heap")==0)
			{
				Variable new_scope = new Variable("main");
				vars.Insert(0, new_scope);
			}
			Variable second_scope = new Variable(s);
			vars.Insert(0, second_scope);
		}

		public static void Decrease_Scope()
		{
			Delete_Scope_From_WatchBox();
		}

		private static Variable_Kind Kind_Of(string s)
		{
			Variable temp=Runtime.Lookup_Variable(s);

			if (temp!=null)
			{
				return (temp.Kind);
			}
			throw new System.Exception("not found");
		}

		//****************************************************
		// isArray
		// Returns true if a variable is an array or false
		// otherwise.
		//****************************************************
		public static bool isArray(string s)
		{
			try
			{
				return Kind_Of(s)==Variable_Kind.One_D_Array;
			}
			catch 
			{
				return false;
			}
		}

		//****************************************************
		// is_2D_Array
		// Returns true if a variable is an array or false
		// otherwise.
		//****************************************************
		public static bool is_2D_Array(string s)
		{
			try
			{
				return Kind_Of(s)==Variable_Kind.Two_D_Array;
			}
			catch 
			{
				return false;
			}
		}

		//****************************************************
		// is_Scalar
		// Returns true if a variable is a scalar or false
		// otherwise.
		//****************************************************
		public static bool is_Value(string s)
		{
			Variable temp=Runtime.Lookup_Variable(s);

            if (temp != null)
            {
                return temp.Kind==Variable_Kind.Value;
            }
            else
            {
                return false;
            }
		}

        public static bool is_Character(string s)
        {
            Variable temp = Runtime.Lookup_Variable(s);

            if (temp != null)
            {
                return (temp.Kind == Variable_Kind.Value) &&
                    numbers.Numbers.is_character(temp.Variable_Value);
            }
            else
            {
                return false;
            }
        }
        public static bool is_String(string s)
		{
            Variable temp = Runtime.Lookup_Variable(s);

            if (temp != null)
            {
                return (temp.Kind == Variable_Kind.Value) &&
                    numbers.Numbers.is_string(temp.Variable_Value);
            }
            else
            {
                return false;
            }
        }
        public static bool is_Scalar(string s)
        {
            Variable temp = Runtime.Lookup_Variable(s);

            if (temp != null)
            {
                return (temp.Kind == Variable_Kind.Value) &&
                    numbers.Numbers.is_number(temp.Variable_Value);
            }
            else
            {
                return false;
            }
        }

		public static bool is_Variable(string s)
		{
			try
			{
				Variable_Kind k = Kind_Of(s);
				return true;
			}
			catch 
			{
				return false;
			}
		}
		/*
		//****************************************************
		// promptDialog
		// Prompts the user for an input and returns the input
		// as a double.
		//****************************************************
		public static string promptDialog(string s)
		{
			// guard for helping stop work.  Note can't stop a compiled
			// flowchart (not even marked as running)
			if ((parent!=null && !parent.runningState) && !Component.compiled_flowchart 
                && !Component.run_compiled_flowchart
                && (Compile_Helpers.run_compiled_thread==null ||
                    Compile_Helpers.run_compiled_thread.ThreadState!=System.Threading.ThreadState.Running))
			{
				return "0.0";
			}
			if (!raptor_files_pkg.input_redirected())
			{
				string val;
				PromptForm pd = new PromptForm(s,parent);
				val = pd.Go();
				return val;
			}
			else
			{
				return raptor_files_pkg.get_line();
			}
		}

		public static void consoleMessage(string s)
		{
			console.set_text(s+"\n");
		}

		//****************************************************
		// consoleWrite
		// Writes string s to the GUI console.
		//****************************************************
        public static void ShowConsole()
        {
            Application.Run(console);
        }
		public static void consoleWrite(string s)
		{
			//Console.WriteLine(s);
			if (!raptor_files_pkg.output_redirected())
			{
                Create_Standalone_Console_if_needed();
                if (console != null)
                {
                    console.set_text(s);
                }
				if (console==null || Avalonia.Controls.Window.command_line_run)
				{
					Console.Write(s);
				}
                if (parent != null)
                {
                    System.Threading.Thread.Sleep(1);
                    parent.Invoke(parent_focus_delegate, new object[] { parent });
                }
			}
			else
			{
				raptor_files_pkg.write(s);
			}
		}

		//****************************************************
		// consoleWriteln
		// Writes string s to the GUI console.
		//****************************************************
		public static void consoleWriteln(string s)
		{
			//Console.WriteLine(s);
			if (!raptor_files_pkg.output_redirected())
			{
				consoleWrite(s+'\n');
			}
			else
			{
				raptor_files_pkg.writeln(s);
			}
		}

		public static void consoleClear()
		{
            //Create_Standalone_Console_if_needed();
			console.clear_txt();
		}
		*/
        /*private static void Create_Standalone_Console_if_needed()
        {
            if (console == null && !Avalonia.Controls.Window.command_line_run)
            {
                console = new MasterConsole(true);
                System.Threading.Thread t = new System.Threading.Thread(
                    new System.Threading.ThreadStart(ShowConsole));
                t.Start();
            }
            while (!console.Created)
            {
                System.Threading.Thread.Sleep(100);
            }
        }
		*/
		/*
        private static void Clear_Delegate()
        {
            watchBox.Nodes.Clear();
            watchBox.Update();
            if (dotnetgraphlibrary.dotnetgraph.Is_Open())
            {
                dotnetgraphlibrary.dotnetgraph.Close_Graph_Window_Force();
            }
        }
        private delegate void Clear_Delegate_Type();
        private static Clear_Delegate_Type clear_delegate =
            new Clear_Delegate_Type(Clear_Delegate);


		public static void Clear_Variables() 
		{
            setContext(null);
            if (Compile_Helpers.run_compiled_thread != null && 
                Compile_Helpers.run_compiled_thread.IsAlive)
            {
                Compile_Helpers.run_compiled_thread.Abort();
            }
            if (watchBox != null && watchBox.Created)
            {
                watchBox.Invoke(clear_delegate, null);
            }
            // do this in the clear_delegate to avoid tasking problems
            // otherwise do it here
            else if (dotnetgraphlibrary.dotnetgraph.Is_Open())
            {
                dotnetgraphlibrary.dotnetgraph.Close_Graph_Window_Force();
            }
            PlaySound.Sound.Play_Sound(null);
            raptor_files_pkg.close_files();
		}

		public static bool End_Of_Input()
		{
			return raptor_files_pkg.end_of_input();
		}

        private static string Redirect_Input_Static()
        {
            OpenFileDialog fileChooser = new OpenFileDialog();
            fileChooser.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            fileChooser.CheckFileExists = true;
            DialogResult result = fileChooser.ShowDialog();

            if (result == DialogResult.Cancel)
            {
                return "";
            }
            return fileChooser.FileName;
        }
        private delegate string Redirect_Input_Delegate_Type();
        private static Redirect_Input_Delegate_Type redirect_input_delegate =
            new Redirect_Input_Delegate_Type(Redirect_Input_Static);
        public static void Redirect_Input(int yes_or_no)
		{
			if (raptor_files_pkg.network_is_redirected ||
				Avalonia.Controls.Window.command_line_input_redirect)
			{
				return;
			}
			if (yes_or_no==0)
			{
				raptor_files_pkg.stop_redirect_input();
			}
			else
			{
                
				string dialog_fileName  = (string) watchBox.Invoke(redirect_input_delegate, null);

				if (dialog_fileName == "" || dialog_fileName == null)
				{
					MessageBoxClass.Show("Invalid File Name", "Error",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

				else
				{
					Redirect_Input(dialog_fileName);
				}
			}
		}

		public static void Redirect_Input(string filename)
		{
			if (raptor_files_pkg.network_is_redirected ||
				Avalonia.Controls.Window.command_line_input_redirect)
			{
				return;
			}

            //MessageBoxClass.Show("redirecting to : " + filename + System.IO.File.Exists(filename));
			raptor_files_pkg.redirect_input(filename);
		}
        public static void createStaticVariables()
        {
            object[] args = new Object[1]; 
            foreach (ClassTabPage ctp in parent.allClasses)
            {
                Variable v = new Variable(Runtime.parent.projectCore.findClass(ctp.Text));
                args[0] = v;
                watchBox.Invoke(Runtime.add_to_classes_delegate, args);
            }
        }
        public static numbers.value createObject(string class_name)
        {
            object[] args = new Object[1];
            Variable v = new Variable("object_ref",class_name);
            args[0] = v;
            watchBox.Invoke(Runtime.add_to_heap_delegate,args);
            return v.Variable_Value;
        }
        private static string Redirect_Output_Static()
        {
            SaveFileDialog fileChooser = new SaveFileDialog();
            fileChooser.Reset();
            // I'd like to make this true, but was getting a bug here
            fileChooser.OverwritePrompt = false;
            fileChooser.CheckFileExists = false;
            fileChooser.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            fileChooser.DefaultExt = ".txt";
            DialogResult result = DialogResult.Yes;

            result = fileChooser.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                return "";
            }
            return fileChooser.FileName;
        }
        private delegate string Redirect_Output_Delegate_Type();
        private static Redirect_Output_Delegate_Type redirect_output_delegate =
            new Redirect_Output_Delegate_Type(Redirect_Output_Static);
        private static bool am_appending = false;
        public static void Redirect_Output_Append(int yes_or_no)
        {
            am_appending = true;
            Redirect_Output(yes_or_no);
        }
		
        public static void Redirect_Output(int yes_or_no)
		{
			if (raptor_files_pkg.network_is_redirected ||
				Avalonia.Controls.Window.command_line_output_redirect)
			{
				return;
			}
			if (yes_or_no==0)
			{
				raptor_files_pkg.stop_redirect_output();
			}
			else
			{

                string dialog_fileName = (string)watchBox.Invoke(redirect_input_delegate, null);

				if (dialog_fileName == "" || dialog_fileName == null)
				{
					MessageBoxClass.Show("Invalid File Name", "Error",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				else
				{
					Redirect_Output(dialog_fileName);
				}
			}
		}
        public static void Redirect_Output_Append(string filename)
        {
            am_appending = true;
            Redirect_Output(filename);
        }
		public static void Redirect_Output(string filename)
		{
            bool append = am_appending;
            am_appending = false;

			if (raptor_files_pkg.network_is_redirected ||
				Avalonia.Controls.Window.command_line_output_redirect)
			{
				return;
			}
			raptor_files_pkg.redirect_output(filename,append);
		}
        public static void Set_Running(Procedure_Chart sc)
        {
            parent.running_tab = sc;
        }
        public static Procedure_Chart Find_Method_Set_Running(string methodname, int num_params)
        {
            NClass.Core.ClassType ct = context.getClass();
            if (Runtime.superContext)
            {
                ct = ct.BaseClass;
            }
            NClass.Core.ClassType walk_ct = ct;
            // don't walk up through the object class
            while (walk_ct.BaseClass != null)
            {
                ClassTabPage rt = walk_ct.raptorTab as ClassTabPage;
                foreach (TabPage tp in rt.tabControl1.TabPages)
                {
                    if (tp.Text.ToLower() == methodname.ToLower() &&
                        (tp as Procedure_Chart).num_params == num_params)
                    {
                        Set_Running(tp as Procedure_Chart);
                        return tp as Procedure_Chart;
                    }
                }
                walk_ct = walk_ct.BaseClass;
            }
            throw new System.Exception("can't find method " + methodname + " with " + num_params + " parameters in class " + ct.Name + ".");
        }
		*/
	}
}
