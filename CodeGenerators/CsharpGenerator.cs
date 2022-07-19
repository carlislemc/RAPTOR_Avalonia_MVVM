using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using raptor;
using RAPTOR_Avalonia_MVVM;

namespace CodeGenerators
{
    public class CsharpGenerator : raptor.Generate_Interface, CodeGenerators.Imperative_Interface
    {
        private string main_name;
        private string current_method;
        private int indent_level = 0;
        private StreamWriter stream;
        private System.Collections.Hashtable variables = new System.Collections.Hashtable();
        private System.Collections.Hashtable strings = new System.Collections.Hashtable();
        private System.Collections.Hashtable arrays = new System.Collections.Hashtable();
        private System.Collections.Hashtable arrays_2d = new System.Collections.Hashtable();
        private string file_name;
        public CsharpGenerator()
        {
        }
        public CsharpGenerator(string filename)
        {
            main_name = Path.GetFileNameWithoutExtension(filename);
            file_name = System.IO.Path.Combine(
                System.IO.Path.GetDirectoryName(filename),
                main_name + ".cs");
            stream = File.CreateText(file_name);
            stream.WriteLine("using System;");
            stream.WriteLine("using System.IO;");
            stream.WriteLine("using System.Text;");
            stream.WriteLine();
            stream.WriteLine("namespace " + main_name);
            stream.WriteLine("{");
            indent_level = 3;
            Indent();
            stream.WriteLine("public class main_class");
            Indent();
            stream.WriteLine("{");
            indent_level += 3;
            Indent();
            stream.WriteLine("static System.Random random_generator = new System.Random();");
        }
        #region Generate_Interfacee Members

        public bool Is_Postfix()
        {
            return false;
        }

        public string Get_Menu_Name()
        {
            return "C&#";
        }
        private System.Collections.Generic.Dictionary<string, MethodInformation>
            Procedures = new Dictionary<string, MethodInformation>();
        private class MethodInformation
        {
            public bool[] args_are_input;
            public bool[] args_are_output;
            public string[] arg_names;
            public MethodInformation(string[] args, bool[] arg_is_input, bool[] arg_is_output)
            {
                arg_names = args;
                args_are_input = arg_is_input;
                args_are_output = arg_is_output;
            }
        }
        public void Declare_Procedure(string name, string[] args, bool[] arg_is_input, bool[] arg_is_output)
        {
            Procedures.Add(name, new MethodInformation(args, arg_is_input, arg_is_output));
        }
        public void Indent()
        {
            for (int i = 0; i < indent_level; i++)
            {
                stream.Write(" ");
            }
        }
        public void Start_Method(string name)
        {
            if (name != "Main")
            {
                Indent();
                stream.Write("public static void " + name);
                if (Procedures[name].arg_names != null &&
                    Procedures[name].arg_names.Length > 0)
                {
                    stream.WriteLine(" (");
                }
                indent_level += 3;
            }
            else
            {
                Indent();
                stream.WriteLine("public static void Main(string[] args)");
                Indent();
                stream.WriteLine("{");
                indent_level += 3;
            }
            variables.Clear();
            arrays.Clear();
            arrays_2d.Clear();
            strings.Clear();
            current_method = name;
        }

        public void Declare_As_2D_Array(string name)
        {
            arrays_2d.Add(name.ToLower(), null);
        }

        public void Declare_As_1D_Array(string name)
        {
            arrays.Add(name.ToLower(), null);
        }

        public void Declare_As_Variable(string name)
        {
            variables.Add(name.ToLower(), null);
        }

        public void Declare_String_Variable(string name)
        {
            strings.Add(name.ToLower(), null);
        }
        public void WriteParameters()
        {
            MethodInformation mi = Procedures[current_method];
            if (mi.arg_names != null && mi.arg_names.Length > 0)
            {
                indent_level += 3;
                for (int i = 0; i < mi.arg_names.Length; i++)
                {
                    Indent();
                    if (mi.args_are_output[i])
                    {
                        stream.Write("ref ");
                    }
                    if (variables.Contains(mi.arg_names[i].ToLower()))
                    {
                        stream.Write(" ??_Variable");
                        variables.Remove(mi.arg_names[i]);
                    }
                    else if (arrays.Contains(current_method.ToLower()))
                    {
                        stream.Write(" ??_Array");
                        arrays.Remove(mi.arg_names[i]);
                    }
                    else if (arrays_2d.Contains(current_method.ToLower()))
                    {
                        stream.Write(" ??_Array_2D");
                        arrays_2d.Remove(mi.arg_names[i]);
                    }
                    else
                    {
                        stream.Write(" ??");
                    }
                    stream.Write(" " + mi.arg_names[i]);

                    if (i < mi.arg_names.Length - 1)
                    {
                        stream.WriteLine(";");
                    }
                    else
                    {
                        stream.WriteLine(")");
                    }
                }
                indent_level -= 3;

                indent_level -= 3;
                Indent();
                stream.WriteLine("{");
                indent_level += 3;
            }
            else
            {
                stream.WriteLine(" {");
            }
        }
        public void WriteVariables()
        {
            IDictionaryEnumerator e = strings.GetEnumerator();
            e.Reset();
            while (e.MoveNext())
            {
                Indent();
                stream.WriteLine("string " + ((string)e.Key) + ";");
            }
            e = variables.GetEnumerator();
            e.Reset();
            while (e.MoveNext())
            {
                Indent();
                stream.WriteLine("?? " + ((string)e.Key) + ";");
            }
            e = arrays.GetEnumerator();
            e.Reset();
            while (e.MoveNext())
            {
                Indent();
                stream.WriteLine("??[] " + ((string)e.Key) + " = new ??[??+1];");
            }
            e = arrays_2d.GetEnumerator();
            e.Reset();
            while (e.MoveNext())
            {
                Indent();
                stream.WriteLine("??[,] " + ((string)e.Key) + " = new ??[??+1,??+1];");
            }
        }
        public void Done_Variable_Declarations()
        {
            if (current_method != "Main")
            {
                WriteParameters();
            }
            WriteVariables();
            indent_level -= 3;
            Indent();
            stream.WriteLine("");
            indent_level += 3;
        }

        public void Emit_Left_Paren()
        {
            stream.Write("(");
        }

        public void Emit_Right_Paren()
        {
            stream.Write(")");
        }

        private class subprogram
        {
            public bool is_function;
            public subprogram(int o)
            {
                if (Lexer.Is_Procedure((Token_Type)o))
                {
                    is_function = false;
                }
                else
                {
                    is_function = true;
                }
            }
            public subprogram()
            {
                is_function = false;
            }
        }
        public object Emit_Call_Method(int name)
        {
            if (Lexer.Is_Procedure((Token_Type)name))
            {
                Indent();
            }
            stream.Write(Lexer.get_image((Token_Type)name));
            if (Lexer.Has_Parameters((Token_Type)name))
            {
                stream.Write("(");
            }
            return new subprogram(name);
        }

        public object Emit_Call_Subchart(string name)
        {
            Indent();
            stream.Write(name);
            if (Procedures[name].arg_names != null &&
                Procedures[name].arg_names.Length > 0)
            {
                stream.Write("(");
            }
            return new subprogram();
        }

        public void Emit_Next_Parameter(object o)
        {
            stream.Write(",");
        }

        public void Emit_Last_Parameter(object o)
        {
            // somehow check if o is a function here
            if (!(o as subprogram).is_function)
            {
                stream.WriteLine(");");
            }
            else
            {
                stream.Write(")");
            }
        }

        public void Emit_No_Parameters(object o)
        {
            // somehow check if o is a function here
            if (!(o as subprogram).is_function)
            {
                stream.WriteLine(";");
            }
        }

        public void Emit_Is_Array2D(string name)
        {
            if (arrays_2d.Contains(name.ToLower()))
            {
                stream.Write("true");
            }
            else
            {
                stream.Write("false");
            }
        }

        public void Emit_Is_Array(string name)
        {
            if (arrays.Contains(name.ToLower()))
            {
                stream.Write("true");
            }
            else
            {
                stream.Write("false");
            }
        }

        public void Emit_Is_String(string name)
        {
            stream.Write("Is_String(" + name + ")");
        }

        public void Emit_Is_Number(string name)
        {
            stream.Write("Is_Number(" + name + ")");
        }
        public void Emit_Is_Character(string name)
        {
            stream.Write("Is_Character(" + name + ")");
        }

        public void Emit_Or_Shortcut(parse_tree.Boolean2 left, parse_tree.Boolean_Expression right)
        {
            left.Emit_Code(this);
            stream.Write(" || ");
            right.Emit_Code(this);
        }

        public void Emit_And_Shortcut(parse_tree.Boolean_Parseable left,
            parse_tree.Boolean2 right, bool left_negated)
        {
            if (left_negated)
            {
                Emit_Not();
            }
            left.Emit_Code(this);
            stream.Write(" && ");
            right.Emit_Code(this);
        }

        public void Emit_Random()
        {
            stream.Write("random_generator.NextDouble()");
        }

        public void Emit_Random(double first, double last)
        {
            stream.Write("To_Color(random_generator.NextDouble()*" + (last - first) + "+" + first + ")");
        }

        public void Emit_Times()
        {
            stream.Write("*");
        }

        public void Emit_Divide()
        {
            stream.Write("/");
        }

        public void Emit_Plus()
        {
            stream.Write("+");
        }

        public void Emit_Unary_Minus()
        {
            stream.Write("-");
        }

        public void Emit_Minus()
        {
            stream.Write("-");
        }

        public void Emit_Mod()
        {
            stream.Write(" % ");
        }

        public void Emit_Rem()
        {
            stream.Write(" % ");
        }

        public void Emit_Exponentiation()
        {
            stream.Write(" ** ");
        }

        public void Emit_Get_Mouse_Button()
        {
            stream.WriteLine(");");
        }

        public void Emit_Get_Click(int x_or_y)
        {
            if (x_or_y == 0)
            {
                stream.Write("Get_Click_X");
            }
            else
            {
                stream.Write("Get_Click_Y");
            }
        }

        public void Emit_Array_Size(string name)
        {
            stream.Write(name + "'Length");
        }

        public void Emit_String_Length()
        {
            stream.Write("'Length");
        }

        public void Emit_Load(string name)
        {
            stream.Write(name);
        }

        public void Emit_Load_Array_Start(string name)
        {
            stream.Write(name + "(");
        }
        public void Emit_Load_Array_After_Index(string name)
        {
            stream.Write(")");
        }

        public void Emit_Load_Array_2D_Start(string name)
        {
            stream.Write(name + "(");
        }
        public void Emit_Load_Array_2D_Between_Indices()
        {
            stream.Write(",");
        }
        public void Emit_Load_Array_2D_After_Indices(string name)
        {
            stream.Write(")");
        }

        public void Emit_Relation(int relation)
        {
            switch (relation)
            {
                case 1: //gt
                    stream.Write(">");
                    break;
                case 2: //ge
                    stream.Write(">=");
                    break;
                case 3: //lt
                    stream.Write("<");
                    break;
                case 4: //le
                    stream.Write("<=");
                    break;
                case 5: //eq
                    stream.Write("==");
                    break;
                case 6: //ne
                    stream.Write("!=");
                    break;
            }
        }

        public void Emit_Sleep()
        {
            Indent();
            stream.Write("System.Threading.Thread.Sleep(");
        }
        public void Emit_Past_Sleep()
        {
            stream.WriteLine(");");
        }
        public void Emit_And()
        {
            stream.Write(" & ");
        }

        public void Emit_Or()
        {
            stream.Write(" | ");
        }

        public void Emit_Not()
        {
            stream.Write("! ");
        }

        public void Emit_Xor()
        {
            stream.Write(" ^ ");
        }

        public void Emit_To_Integer()
        {
            stream.Write("To_Integer()");
        }

        public void Emit_Load_Boolean(bool val)
        {
            stream.Write(val.ToString());
        }

        public void Emit_Load_Number(double val)
        {
            stream.Write(val.ToString());
        }

        public void Emit_Load_String(string val)
        {
            stream.Write('"' + val + '"');
        }
        public void Emit_Load_Character(char val)
        {
            stream.Write("'" + val + "'");
        }

        public void Emit_Load_String_Const(string val)
        {
            if (val.Length > 0)
            {
                stream.Write("\"" + val + "\"");
            }
            else
            {
                stream.Write("\"\"");
            }
        }

        public void Emit_Conversion(int o)
        {
            // type Conversions is (To_Integer, To_Float, To_String, Number_To_String, To_Bool, To_Color,
            // Char_To_Int, Int_To_Char);
            switch (o)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    stream.Write("(");
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    stream.Write("((int) ");
                    break;
                case 7:
                    stream.Write("((char) ");
                    break;
            }


        }

        public void Emit_End_Conversion(int o)
        {
            switch (o)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    stream.Write(").ToString()");
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    stream.Write(")");
                    break;
                case 7:
                    stream.Write(")");
                    break;
            }
        }

        public void Emit_Plugin_Call(string name, parse_tree.Parameter_List parameters)
        {
            if (Plugins.Is_Procedure(name))
            {
                Indent();
            }
            stream.Write(name);
            if (Plugins.Parameter_Count(name) > 0)
            {
                stream.Write("(");
            }
            parse_tree.Parameter_List pl = parameters;
            while (pl != null)
            {
                ((parse_tree.Expr_Output)pl.parameter).expr.Emit_Code(this);
                pl = pl.next;
                if (pl != null)
                {
                    stream.Write(",");
                }
            }
            if (Plugins.Parameter_Count(name) > 0)
            {
                stream.Write(")");
            }
            if (Plugins.Is_Procedure(name))
            {
                stream.WriteLine(";");
            }
        }

        public object If_Start()
        {
            Indent();
            stream.Write("if (");
            return null;
        }

        public void If_Then_Part(object o)
        {
            stream.WriteLine(")");
            Indent();
            stream.WriteLine("{");
            indent_level += 3;
        }

        public void If_Else_Part(object o)
        {
            indent_level -= 3;
            Indent();
            stream.WriteLine("}");
            Indent();
            stream.WriteLine("else");
            Indent();
            stream.WriteLine("{");
            indent_level += 3;
        }

        public void If_Done(object o)
        {
            indent_level -= 3;
            Indent();
            stream.WriteLine("}");
        }

        public struct loop_flags
        {
            public bool is_while;
            public bool negative_condition;
        }

        public object Loop_Start(bool is_while, bool negative_condition)
        {
            loop_flags flags;

            if (!is_while)
            {
                Indent();
                stream.WriteLine("while (1)");
                Indent();
                stream.WriteLine("{");
            }
            else
            {
                Indent();
                if (!negative_condition)
                {
                    stream.Write("while (!(");
                }
                else
                {
                    stream.Write("while (");
                }
            }
            indent_level += 3;
            flags.is_while = is_while;
            flags.negative_condition = negative_condition;
            return flags as object;
        }

        public void Loop_Start_Condition(object o)
        {
            if ((((loop_flags)o).is_while) == true)
            {
            }
            else
            {
                Indent();
                if (((loop_flags)o).negative_condition)
                {
                    stream.Write("if (!(");
                }
                else
                {
                    stream.Write("if (");
                }
            }
        }

        public void Loop_End_Condition(object o)
        {
            if ((((loop_flags)o).is_while) == true)
            {
                if (!(((loop_flags)o).negative_condition))
                {
                    stream.Write(")");
                }
                stream.WriteLine(")");
                indent_level -= 3;
                Indent();
                stream.WriteLine("{");
                indent_level += 3;
            }
            else
            {
                if (!(((loop_flags)o).negative_condition))
                {
                    stream.Write(")");
                }
                stream.WriteLine(") break;");
            }
        }

        public void Loop_End(object o)
        {
            indent_level -= 3;
            Indent();
            stream.WriteLine("}");
        }

        public void Array_2D_Assignment_Start(string name)
        {
            Indent();
            stream.Write(name + "[");
        }

        public void Array_2D_Assignment_Between_Indices()
        {
            stream.Write(",");
        }

        public void Array_2D_Assignment_After_Indices()
        {
            stream.Write("] = ");
        }

        public void Array_2D_Assignment_PastRHS()
        {
            stream.WriteLine(";");
        }

        public void Array_1D_Assignment_Start(string name)
        {
            Indent();
            stream.Write(name + "[");
        }

        public void Array_1D_Assignment_After_Index()
        {
            stream.Write("] = ");
        }

        public void Array_1D_Assignment_PastRHS()
        {
            stream.WriteLine(";");
        }

        public void Variable_Assignment_Start(string name)
        {
            Indent();
            stream.Write(name + " =");
        }

        public void Variable_Assignment_PastRHS()
        {
            stream.WriteLine(";");
        }
        private enum input_kind { variable, array, array2d }
        private string input_name;
        private input_kind kind_of_input;
        private parse_tree.Expression input_reference;
        private parse_tree.Expression input_reference2;

        public void Input_Start_Variable(string name)
        {
            input_name = name;
            kind_of_input = input_kind.variable;
            Indent();
            stream.Write("Console.WriteLine(");
        }
        public void Input_Start_Array_1D(string name, parse_tree.Expression reference)
        {
            input_name = name;
            kind_of_input = input_kind.array;
            input_reference = reference;
            Indent();
            stream.Write("Console.WriteLine(");
        }
        public void Input_Start_Array_2D(string name, parse_tree.Expression reference,
            parse_tree.Expression reference2)
        {
            input_name = name;
            kind_of_input = input_kind.array2d;
            input_reference = reference;
            input_reference2 = reference2;
            Indent();
            stream.Write("Console.WriteLine(");
        }
        public void Input_Past_Prompt()
        {
            stream.WriteLine(");");
            Indent();
            switch (kind_of_input)
            {
                case input_kind.array:
                    stream.Write(input_name + "(");
                    input_reference.Emit_Code(this);
                    stream.Write(")");
                    break;
                case input_kind.array2d:
                    stream.Write(input_name + "(");
                    input_reference.Emit_Code(this);
                    stream.Write(",");
                    input_reference2.Emit_Code(this);
                    stream.Write(")");
                    break;
                case input_kind.variable:
                    stream.Write(input_name);
                    break;
            }
            stream.WriteLine("= Double.Parse(Console.ReadLine());");
        }

        public void Output_Start(bool has_newline, bool is_string)
        {
            Indent();
            if (!has_newline)
            {
                stream.Write("Console.Write(");
            }
            else
            {
                stream.Write("Console.WriteLine(");
            }
        }

        public void Output_Past_Expr(bool has_newline, bool is_string)
        {
            stream.WriteLine(");");
        }

        public void Done_Method()
        {
            indent_level -= 3;
            Indent();
            stream.WriteLine("}");
        }

        public void Finish()
        {
            indent_level -= 3;
            Indent();
            stream.WriteLine("}");
            stream.WriteLine("}");
            stream.Close();
            try
            {
                System.Diagnostics.Process.Start(file_name);
            }
            catch
            {
                //System.Windows.Forms.MessageBox.Show("generated to: " + file_name);
            }
        }

        #endregion
    }

}