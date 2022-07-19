using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using System.IO;
using System.Security.Cryptography;
namespace raptor
{
    public class Generate_Hash : raptor.Generate_Interface, CodeGenerators.Imperative_Interface /*, CodeGenerators.OO_Interface */
    {
        /*public void declare_class(NClass.Core.ClassType ct)
        {
        }
        public void start_class(NClass.Core.ClassType ct)
        {
        }
        public void done_class(NClass.Core.ClassType ct)
        {
        }
        public void declare_method(NClass.Core.Method method)
        {
        }
        public void declare_abstract_method(NClass.Core.Method method)
        {
        }
        public void declare_interface_method(NClass.Core.Method method)
        {
        }
        public void start_interface(NClass.Core.InterfaceType i)
        {
        }
        public void done_interface(NClass.Core.InterfaceType i)
        {
        }
        public void declare_field(NClass.Core.Field f)
        {
        }
        public void start_method(NClass.Core.Method method)
        {
            strings.Append("SM" + method.numberArguments);
        } */
        public void abort()
        {
            strings.Append("ab");
        }
        public object create_object(string s)
        {
            strings.Append("CO");
            return null;
        }
        public object emit_call_oo_method(string s, bool b)
        {
            strings.Append("COO!");
            return null;
        }
        public void emit_dereference()
        {
            strings.Append("->");
        }
        public void start_return()
        {
            strings.Append("SR");
        }
        public void end_return()
        {
            strings.Append("ER");
        }
        public String computeHash(String message, String algo)
        {
            byte[] sourceBytes = Encoding.Default.GetBytes(message);
            byte[] hashBytes = null;
            Console.WriteLine(algo);
            switch (algo.Trim().ToUpper())
            {
                case "MD5":
                    hashBytes = MD5CryptoServiceProvider.Create().ComputeHash(sourceBytes);
                    break;
                case "SHA1":
                    hashBytes = SHA1Managed.Create().ComputeHash(sourceBytes);
                    break;
                case "SHA256":
                    hashBytes = SHA256Managed.Create().ComputeHash(sourceBytes);
                    break;
                case "SHA384":
                    hashBytes = SHA384Managed.Create().ComputeHash(sourceBytes);
                    break;
                case "SHA512":
                    hashBytes = SHA512Managed.Create().ComputeHash(sourceBytes);
                    break;
                default:
                    break;
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; hashBytes != null && i < hashBytes.Length; i++)
            {
                sb.AppendFormat("{0:x2}", hashBytes[i]);
            }
            return sb.ToString();
        }

        private StringBuilder strings;
        private System.IO.StreamWriter stream;
        public Generate_Hash(object x)
        {
            strings = new StringBuilder();
        }
        public string toString()
        {
            return this.computeHash(strings.ToString(), "SHA512");
        }
        public string Get_Menu_Name()
        {
            return "&Hash";
        }
        public void Finish()
        {
        }
        public bool Is_Postfix()
        {
            return true;
        }
        public void Indent()
        {
        }
        public void Declare_Procedure(string name, string[] args, bool[] arg_is_input, bool[] arg_is_output)
        {
            string hash = "DP" + args.Length;
            for (int i = 0; i < arg_is_input.Length; i++)
            {
                if (arg_is_input[i])
                {
                    hash += "T";
                }
                else
                {
                    hash += "F";
                }
                if (arg_is_output[i])
                {
                    hash += "T";
                }
                else
                {
                    hash += "F";
                }
            }
            strings.Append(hash);
        }

        public void Emit_Get_Mouse_Button()
        {
            strings.Append("GMB");
        }
        public void Emit_Get_Click(int x_or_y)
        {
            if (x_or_y == 0)
            {
                strings.Append("GCX");
            }
            else
            {
                strings.Append("GCY");
            }
        }
        public void Emit_Array_Size(string name)
        {
            strings.Append("GAS");
        }
        public void Emit_String_Length()
        {
            strings.Append("ESL");
        }
        public void Emit_Assign_To(string name)
        {
            strings.Append("V:=");
        }
        public void Emit_Assign_To_Array(string name)
        {
            strings.Append("V[]:=");
        }
        public void Emit_Assign_To_Array_2D(string name)
        {
            strings.Append("V[,]:=");
        }
        public void Emit_Load(string name)
        {
            strings.Append("V");
        }
        public void Emit_Load_Array_Start(string name)
        {
            strings.Append("A[");
        }
        public void Emit_Load_Array_After_Index(string name)
        {
            strings.Append("]");
        }
        public void Emit_Load_Array_2D_Start(string name)
        {
            strings.Append("A[");
        }
        public void Emit_Load_Array_2D_Between_Indices() { strings.Append(","); }
        public void Emit_Load_Array_2D_After_Indices(string name)
        {
            strings.Append("]");
        }
        public bool Previously_Declared(string name)
        {
            return false;
        }

        public void Declare_String_Variable(string name)
        {
        }
        public void Declare_As_Variable(string name)
        {
        }
        public void Declare_As_1D_Array(string name)
        {
        }
        public void Declare_As_2D_Array(string name)
        {
        }
        public void Emit_Relation(int relation)
        {
            switch (relation)
            {
                case 1: //gt
                    strings.Append(">");
                    break;
                case 2: //ge
                    strings.Append(">=");
                    break;
                case 3: //lt
                    strings.Append("<");
                    break;
                case 4: //le
                    strings.Append("<=");
                    break;
                case 5: //eq
                    strings.Append("=");
                    break;
                case 6: //ne
                    strings.Append("<>");
                    break;
            }
        }
        public void Emit_Method(string package, string name)
        {
            strings.Append("C");
        }
        public void Emit_Method_Virt(string package, string name)
        {
            strings.Append("CV");
        }
        public void Emit_Sleep()
        {
            strings.Append("Z");
        }
        public void Emit_Past_Sleep()
        {
        }
        public void Emit_And()
        {
            strings.Append("&");
        }
        public void Emit_Or()
        {
            strings.Append("|");
        }
        public void Emit_Not()
        {
            strings.Append("!");
        }
        public void Emit_Xor()
        {
            strings.Append("^");
        }
        public void Emit_To_Integer()
        {
        }
        public void Emit_Load_Boolean(bool val)
        {
            if (val)
            {
                strings.Append("T");
            }
            else
            {
                strings.Append("F");
            }
        }
        public void Emit_Load_Number(double val)
        {
            strings.Append("LD");
        }
        public void Emit_Load_Character(char val)
        {
            strings.Append("LC");
        }
        public void Emit_Load_String(string val)
        {
            strings.Append("LS");
        }
        public void Emit_Load_String_Const(string val)
        {
            strings.Append("LSC");
        }
        public void Emit_Load_Static(string package,
            string field)
        {
        }
        //dotnetgraphlibrary.dotnetgraph.
        public void Emit_Conversion(int o)
        {
        }
        public void Emit_End_Conversion(int o)
        {
        }

        public void Emit_Value_To_Color_Type()
        {
        }
        public void Emit_Value_To_Bool()
        {
        }
        public void Emit_Random()
        {
            strings.Append("RND");
        }
        public void Emit_Random(double first, double last)
        {
            strings.Append("RND2");
        }

        public void Emit_And_Shortcut(parse_tree.Boolean_Parseable left,
            parse_tree.Boolean2 right,
            bool left_negated)
        {
            strings.Append("&&");
        }
        public void Emit_Or_Shortcut(parse_tree.Boolean2 left, parse_tree.Boolean_Expression right)
        {
            strings.Append("||");
        }
        public void Emit_Is_Number(string name)
        {
            strings.Append("?N");
        }
        public void Emit_Is_Character(string name)
        {
            strings.Append("?C");
        }
        public void Emit_Is_String(string name)
        {
            strings.Append("?S");
        }
        public void Emit_Is_Array(string name)
        {
            strings.Append("?A");
        }
        public void Emit_Is_Array2D(string name)
        {
            strings.Append("?A2");
        }

        public void Emit_Plugin_Call(string name, parse_tree.Parameter_List parameters)
        {
            strings.Append("CP");
        }
        public void Emit_Times()
        {
            strings.Append("*");
        }
        public void Emit_Divide()
        {
            strings.Append("/");
        }
        public void Emit_Plus()
        {
            strings.Append("+");
        }
        public void Emit_Unary_Minus()
        {
            strings.Append("-");
        }
        public void Emit_Minus()
        {
            strings.Append("-");
        }
        public void Emit_Mod()
        {
            strings.Append("%");
        }
        public void Emit_Rem()
        {
            strings.Append("%");
        }
        public void Emit_Exponentiation()
        {
            strings.Append("^");
        }
        public void Start_Method(string name)
        {
            // moved to outside if-- should happen always.
            strings.Append("MM");
        }
        public void Done_Variable_Declarations()
        {

        }
        public void Done_Method()
        {
            strings.Append("/MM");
        }
        public object Emit_Call_Method(int name)
        {
            strings.Append("CM(");
            return null;
        }
        public void Emit_Next_Parameter(object o)
        {
            strings.Append(",");
        }
        public void Emit_Last_Parameter(object o)
        {
            strings.Append(")");
        }
        public void Emit_No_Parameters(object o)
        {
            strings.Append("CM");
        }
        public object Emit_Call_Subchart(string name)
        {
            strings.Append("CS");
            return null;

        }
        public object If_Start()
        {
            strings.Append("if");
            return null;
        }
        public void If_Then_Part(object o)
        {
            strings.Append("{t");
        }
        public void If_Else_Part(object o)
        {
            strings.Append("{e");
        }
        public void If_Done(object o)
        {
            strings.Append("}i");
        }

        public object Loop_Start(bool is_while, bool is_negated)
        {
            strings.Append("l{");
            return null;
        }
        public void Loop_Start_Condition(object o)
        {
            strings.Append("(l");
        }
        public void Loop_End_Condition(object o)
        {
            strings.Append(")l");
        }
        public void Loop_End(object o)
        {
            strings.Append("}l");
        }
        public void Emit_Left_Paren() { }
        public void Emit_Right_Paren() { }


        public void Array_2D_Assignment_Start(string name)
        {
            strings.Append("A[");
        }
        public void Array_2D_Assignment_Between_Indices() { strings.Append(","); }
        public void Array_2D_Assignment_After_Indices() { strings.Append("]:="); }
        public void Array_2D_Assignment_PastRHS()
        {
        }

        // things to do with 1D array assignment
        public void Array_1D_Assignment_Start(string name)
        {
            strings.Append("A[");
        }
        public void Array_1D_Assignment_After_Index() { strings.Append("]:="); }
        public void Array_1D_Assignment_PastRHS()
        {
        }

        // things to do with variable assignment
        public void Variable_Assignment_Start(string name)
        {
            strings.Append("V:=");
        }
        public void Variable_Assignment_PastRHS()
        {
        }
        public void Input_Start_Variable(string name)
        {
            strings.Append("INV");
        }
        public void Input_Start_Array_1D(string name, parse_tree.Expression reference)
        {
            strings.Append("INA");
        }
        public void Input_Start_Array_2D(string name, parse_tree.Expression reference,
            parse_tree.Expression reference2)
        {
            strings.Append("INA2");
        }
        public void Input_Past_Prompt()
        {
        }
        public void Output_Start(bool has_newline, bool is_string) { }
        public void Output_Past_Expr(bool has_newline, bool is_string)
        {
            strings.Append("OUT");
        }

    }
}