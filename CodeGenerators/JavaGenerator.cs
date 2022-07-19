using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CodeGenerators
{
    class JavaGenerator /* :  OO_Interface */
    {
        //private string main_name;
        //private NClass.Core.Method current_method;
        //private NClass.Core.ClassType current_class;
        //private string file_name;
        //private string directory_name;
        //private int indent_level = 0;
        //private StreamWriter stream, main_stream;
        //private System.Collections.Hashtable variables = new System.Collections.Hashtable();
        //private System.Collections.Hashtable strings = new System.Collections.Hashtable();
        //private System.Collections.Hashtable arrays = new System.Collections.Hashtable();
        //private System.Collections.Hashtable arrays_2d = new System.Collections.Hashtable();
        //private System.Collections.Hashtable classes = new System.Collections.Hashtable();
        //private string returnStatement;

        //private Boolean turnOff = false;  // used to add return assignment to user method calls
        //private string holdMethod = "";   // holds the method that has a a return assignment
        //private string holdVar = "";      // holds the return assignment variable
        //private Boolean readFile = false; // file read access in progress
        //private Boolean writeFile = false; // file write access in progress
        //private System.Collections.Generic.Dictionary<string, MethodInformation>
        //    Procedures = new Dictionary<string, MethodInformation>();

        //private class MethodInformation
        //{
        //    public bool[] args_are_input;
        //    public bool[] args_are_output;
        //    public string[] arg_names;
        //    public MethodInformation(string[] args, bool[] arg_is_input, bool[] arg_is_output)
        //    {
        //        arg_names = args;
        //        args_are_input = arg_is_input;
        //        args_are_output = arg_is_output;
        //    }
        //}

        //// not used but required
        //public JavaGenerator()
        //{
        //}

        //public void declare_class(NClass.Core.ClassType ct)
        //{
        //    try
        //    {
        //        classes.Add(ct.Name, ct);
        //    }
        //    catch
        //    {
        //    }
        //    current_class = ct;
        //}
        //public void start_class(NClass.Core.ClassType ct)
        //{
        //    //int i,int_count;
        //    string class_file_name;
        //    current_class = ct;
        //    class_file_name = System.IO.Path.Combine(
        //        directory_name,
        //        ct.Name + ".java");
        //    stream = File.CreateText(class_file_name);
        //    CreateFileComment();
        //    stream.WriteLine(ct.GetDeclarationLine());
        //    /*stream.Write("public class " + ct.Name);
        //    if (!ct.BaseClass.Name.Equals("Object"))
        //    {
        //        stream.Write(" extends " + ct.BaseClass.Name);
        //    }
        //    int_count = 0;
        //    foreach (NClass.Core.InterfaceType intrfce in ct.Interfaces)
        //    {
        //        int_count++;
        //    }
        //    if (int_count > 0)
        //    {
        //        i = 0;
        //        stream.Write(" implements ");
        //        foreach (NClass.Core.InterfaceType intrfce in ct.Interfaces)
        //        {
        //            i++;
        //            stream.Write(intrfce.Name);
        //            if (i < int_count)
        //            {
        //                stream.Write(", ");
        //            }
        //        }
        //    }*/
        //    stream.WriteLine();
        //    stream.WriteLine("{");
        //    // sets current depth of an indent
        //    indent_level = 3;
        //}
        //public void declare_interface_method(NClass.Core.Method method)
        //{
        //    Indent();
        //    stream.WriteLine(method.GetDeclarationLine());
        //}
        //public void start_interface(NClass.Core.InterfaceType ct)
        //{
        //    int i, int_count;
        //    string class_file_name;
        //    //current_class = ct;
        //    class_file_name = System.IO.Path.Combine(
        //        directory_name,
        //        ct.Name + ".java");
        //    stream = File.CreateText(class_file_name);
        //    CreateFileComment();
        //    stream.Write("public interface " + ct.Name);
        //    int_count = 0;
        //    if (ct.Bases != null)
        //    {
        //        foreach (NClass.Core.InterfaceType intrfce in ct.Bases)
        //        {
        //            int_count++;
        //        }
        //    }
        //    if (int_count > 0)
        //    {
        //        i = 0;
        //        stream.Write(" implements ");
        //        foreach (NClass.Core.InterfaceType intrfce in ct.Bases)
        //        {
        //            i++;
        //            stream.Write(intrfce.Name);
        //            if (i < int_count)
        //            {
        //                stream.Write(", ");
        //            }
        //        }
        //    }
        //    stream.WriteLine();
        //    stream.WriteLine("{");
        //    // sets current depth of an indent
        //    indent_level = 3;
        //}
        //public void done_interface(NClass.Core.InterfaceType i)
        //{
        //    stream.WriteLine("} // close " + i.Name);
        //    stream.Close();
        //}
        //public void done_class(NClass.Core.ClassType ct)
        //{
        //    stream.WriteLine("} // close " + ct.Name);
        //    stream.Close();
        //}
        //public void declare_method(NClass.Core.Method method)
        //{
        //}
        //public void start_method(NClass.Core.Method method)
        //{
        //    Indent();
        //    stream.WriteLine(method.GetDeclarationLine());
        //    Indent();
        //    stream.WriteLine("{");
        //    indent_level += 3;
        //    variables.Clear();
        //    arrays.Clear();
        //    arrays_2d.Clear();
        //    strings.Clear();
        //    current_method = method;
        //}
        //public void abort()
        //{
        //    if (stream != null)
        //    {
        //        try
        //        {
        //            stream.Close();
        //        }
        //        catch { }
        //    }
        //    if (main_stream != null)
        //    {
        //        try
        //        {
        //            main_stream.Close();
        //        }
        //        catch { }
        //    }
        //}
        //// used, creates and opens java file
        //public JavaGenerator(string filename)
        //{
        //    main_name = Path.GetFileNameWithoutExtension(filename);
        //    directory_name = System.IO.Path.Combine(
        //        System.IO.Path.GetDirectoryName(filename),
        //        main_name + "_java");
        //    directory_name = DirectoryCheck(directory_name);
        //    if (!Directory.Exists(directory_name))
        //    {
        //        Directory.CreateDirectory(directory_name);
        //    }
        //    file_name = System.IO.Path.Combine(
        //        directory_name,
        //        main_name + "_main.java");
        //    stream = File.CreateText(file_name);
        //    main_stream = stream;
        //    CreateFileComment();
        //    stream.WriteLine("public class " + main_name + "_main");
        //    stream.WriteLine("{");
        //    // sets current depth of an indent
        //    indent_level = 3;
        //}

        //public void CreateFileComment() 
        //{
        //    stream.WriteLine("/**");
        //    stream.WriteLine("  * NAME:");
        //    stream.WriteLine("  * DATE:");
        //    stream.WriteLine("  * FILE:");
        //    stream.WriteLine("  * COMMENTS:");
        //    stream.WriteLine("  */");
        //    stream.WriteLine();
        //}


        //// gives option to not overwrite an existing java file
        //public string DirectoryCheck(string directory_to_check)
        //{
        //    string new_directory = directory_to_check;

        //    //Does foo.java exist?  If it does, we create a backup of the file to be generated
        //    if (Directory.Exists(directory_to_check))
        //    {
        //        if (Directory.Exists(directory_to_check + ".bak0"))
        //        {
        //            if (Directory.Exists(directory_to_check + ".bak1"))
        //            {
        //                if (Directory.Exists(directory_to_check + ".bak2"))
        //                {
        //                    if (Directory.Exists(directory_to_check + ".bak3"))
        //                    {
        //                        if (Directory.Exists(directory_to_check + ".bak4"))
        //                        {
        //                            new_directory = directory_to_check + ".bak0";
        //                        }
        //                    }
        //                    else { new_directory = directory_to_check + ".bak3"; }
        //                }
        //                else { new_directory = directory_to_check + ".bak2"; }
        //            }
        //            else { new_directory = directory_to_check + ".bak1"; }
        //        }
        //        else { new_directory = directory_to_check + ".bak0"; }

        //        if (DialogResult.Yes == 
        //            System.Windows.Forms.MessageBox.Show("Directory " + directory_to_check + 
        //               " already exists.  Click Yes ONLY if you want to overwrite " + 
        //               directory_to_check + ". Click No to generate " + new_directory + 
        //               " instead.", 
        //            "Overwrite Warning!", 
        //            System.Windows.Forms.MessageBoxButtons.YesNo, 
        //            System.Windows.Forms.MessageBoxIcon.Exclamation))
        //        {
        //            new_directory = directory_to_check;
        //        }
        //    }
        //    return new_directory;
        //}

        //#region generate_interface.typ Members

        //public bool Is_Postfix()
        //{
        //    return false;
        //}

        //// Sets menu label for this generator 
        //public string Get_Menu_Name()
        //{
        //    return "&Java";
        //}

        //private void debug(String text)
        //{
        //    System.Windows.Forms.MessageBox.Show(text);
        //}


        //public void start_return()
        //{
        //    Indent();
        //    stream.Write("return ");
        //}
        //public void end_return()
        //{
        //    stream.WriteLine(";");
        //}
        //public void Indent()
        //{
        //    for (int i = 0; i < indent_level; i++)
        //    {
        //        stream.Write(" ");
        //    }
        //}

        //public void Declare_As_2D_Array(string name)
        //{
        //    arrays_2d.Add(name, null);
        //}

        //public void Declare_As_1D_Array(string name)
        //{
        //    arrays.Add(name, null);
        //}

        //public void Declare_As_Variable(string name)
        //{
        //    if (!classes.Contains(name))
        //    {
        //        variables.Add(name, null);
        //    }
        //}

        //public void Declare_String_Variable(string name)
        //{
        //    strings.Add(name, null);
        //}

        //public void Start_Method(string name)
        //{
        //    current_method = null;
        //    if (name == "Main")
        //    {
        //        stream = main_stream;
        //        // I'm not sure how to work globals into the output.  This appears to be an abstract class that is 
        //        // called by other methods in Martin's code.  I'll proceed with all local variables for now . . .
        //        Indent();
        //        stream.WriteLine("public static void main(String[] args)");
        //        Indent();
        //        stream.WriteLine("{");
        //    }
        //    else
        //    {
        //        Indent();
        //        // Without globals, we can't use static, or class methods; we can only use instance methods
        //        // Class methods can only act on global (static) variables
        //        MethodInformation mi = Procedures[name];
        //        if (hasReturnValue(mi, name))
        //        {
        //             stream.Write("public static ??" ); 
        //            // finish writing method name with parameters if has return value
        //            // have to test for array return, cannot do here
        //        }
        //        else
        //        {
        //            stream.Write("public static void " + name+"(" );
        //        }
        //    }
        //    variables.Clear();
        //    arrays.Clear();
        //    arrays_2d.Clear();
        //    strings.Clear();
        //    indent_level += 3;
        //}

        //// java methods only have one return value
        //// convention in raptor is to use the first parameter for that if needed
        //private Boolean hasReturnValue(MethodInformation checkMi, String name)
        //{
        //    Boolean hasReturn = false;

        //    if (Procedures[name].arg_names != null && Procedures[name].arg_names.Length > 0)
        //        if (checkMi.args_are_output[0])
        //            hasReturn = true;

        //    return hasReturn;
        //}


        //private Boolean haveVariables()
        //{
        //    Boolean isVariables = false;
        //    if (strings.Count > 0)
        //        isVariables = true;
        //    if (variables.Count > 0)
        //        isVariables = true;
        //    if (arrays.Count > 0)
        //        isVariables = true;
        //    if (arrays_2d.Count > 0)
        //        isVariables = true;
        //    return isVariables;
        //}

        //// We write variables below, but these should be local variables for each method.
        //public void WriteVariables()
        //{
        //    IDictionaryEnumerator e = strings.GetEnumerator();
        //    e.Reset();
        //    while (e.MoveNext())
        //    {
        //        Indent();
        //        String var = (String)e.Key;
        //        stream.WriteLine("String " + var + " = null;");
        //    }
        //    e = variables.GetEnumerator();
        //    e.Reset();
        //    while (e.MoveNext())
        //    {
        //        Indent();
        //        String var = (String)e.Key;
        //        stream.WriteLine("?? " + var + " = ??;");
        //    }
        //    e = arrays.GetEnumerator();
        //    e.Reset();
        //    while (e.MoveNext())
        //    {
        //        Indent();
        //        String var = (String)e.Key;
        //        stream.WriteLine("??[] " + var + " = new ??[??];");
        //    }
        //    e = arrays_2d.GetEnumerator();
        //    e.Reset();
        //    while (e.MoveNext())
        //    {
        //        Indent();
        //        String var = (String)e.Key;
        //        stream.WriteLine("??[][] " + var + " = new ??[??][??];");
        //    }
        //}

        //public void declare_abstract_method(NClass.Core.Method method)
        //{
        //    Indent();
        //    stream.WriteLine(method.GetDeclarationLine());
        //}
        //public void declare_field(NClass.Core.Field f)
        //{
        //    Indent();
        //    stream.WriteLine(f.GetDeclarationLine());
        //}
        //public void Done_Variable_Declarations()
        //{
        //    if (haveVariables())
        //    {
        //        Indent();
        //        stream.WriteLine("// declare variables");
        //    }                
        //    WriteVariables();            
        //    if (haveVariables())
        //    {
        //        Indent();
        //        stream.WriteLine("");
        //    }
        //}

        //public void Emit_Left_Paren()
        //{
        //    stream.Write("(");
        //}

        //public void Emit_Right_Paren()
        //{
        //    stream.Write(")");
        //}

        //private class subprogram
        //{
        //    public bool is_function;
        //    public subprogram(int o)
        //    {
        //        if (lexer_pkg.is_procedure(o))
        //        {
        //            is_function = false;
        //        }
        //        else
        //        {
        //            is_function = true;
        //        }
        //    }
        //    public subprogram(bool is_function)
        //    {
        //        this.is_function = is_function;
        //    }
        //    public subprogram()
        //    {
        //        is_function = false;
        //    }
        //}
        //// if there is an equivalent Java Math. method we prepend Math. and fix the name
        //// if file IO we fix to be java equivalent
        //public object Emit_Call_Method(int name)
        //{
        //    string javamethod = lexer_pkg.get_image(name);

        //    if (lexer_pkg.is_procedure(name))
        //    {
        //        Indent();
        //    }


        //    if (isMathPackage(javamethod))
        //    {
        //        javamethod = getJavaName(javamethod);
        //        javamethod = "Math." + javamethod;
        //        stream.Write(javamethod);
        //    }
        //    else if (isJavaMethod(javamethod))
        //    {
        //        javamethod = getJavaName(javamethod); ;
        //        stream.Write(javamethod);
        //        holdMethod = javamethod;
        //    }
        //    else
        //    {
        //        stream.Write(lexer_pkg.get_image(name));
        //    }
        //    if (lexer_pkg.has_parameters(name))
        //    {
        //        stream.Write("(");
        //    }
        //    return new subprogram(name);
        //}

        //private string colorToUpper(string name)
        //{
        //    string color = name;
        //    string[] colors = { "black", "blue", "green", "cyan", "red", "magenta", "brown", "light_gray", "dark_gray", 
        //        "light_blue", "light_green", "light_cyan", "light_red", "light_magenta", "yellow", "white", "filled", "unfilled" };


        //    for (int i = 0; i < colors.Length; i++)
        //    {
        //        if (colors[i].CompareTo(name.ToLower()) == 0)
        //        {                    
        //            color = colors[i].ToUpper();
        //            debug("to upper:" +color);
        //        }
        //    }
        //    return color;
        //}

        //// a read/writeFile is expected to have an open and close on a file
        //// in between all input/output is redirected to the file
        //private string getJavaName(string name)
        //{
        //    string mathmethod = name;
        //    string[] raptor = { "abs_f", "ceiling", "arctan", "arccos", "arcsin", "end_of_input", 
        //        "clear_window",	"display_number", "display_text", "draw_box", "draw_circle", 
        //        "draw_ellipse", "draw_line", "open_graph_window", "put_pixel", "set_font_size", 
        //        "set_window_title", "load_bitmap", "draw_bitmap" };
        //    string[] java   = { "abs"  , "ceil"   , "atan"  , "acos"  , "asin"   , "!moreToRead()", 
        //        "clearWindow", "displayNumber", "displayText", "drawBox", "drawCircle", 
        //        "drawEllipse", "drawLine", "openGraphWindow", "putPixel", "setFontSize", 
        //        "setWindowTitle", "loadBitmap", "drawImage" };

        //    for (int i = 0; i < raptor.Length; i++)
        //    {
        //        if (raptor[i].CompareTo(name.ToLower()) == 0)
        //            mathmethod = java[i];
        //    }
        //    if (name.ToLower().CompareTo("redirect_input") == 0)
        //    {
        //        if (readFile)
        //            mathmethod = "closeReadFile";
        //        else
        //            mathmethod = "openReadFile";
        //        readFile = !readFile;
        //    }
        //    else if (name.ToLower().CompareTo("redirect_output") == 0)
        //    {
        //        if (writeFile)
        //            mathmethod = "closeWriteFile";
        //        else
        //            mathmethod = "openWriteFile";
        //        writeFile = !writeFile;
        //    }
        //    return mathmethod;
        //}


        //private Boolean isMathPackage(string name)
        //{
        //    Boolean ismath = false;
        //    string[] math = { "max", "min", "sqrt", "abs_f", "sin", "cos", "tan", 
        //        "log", "arctan", "arccos", "arcsin" };

        //    for (int i = 0; i < math.Length; i++)
        //    {
        //        if (math[i].CompareTo(name.ToLower()) == 0)
        //            ismath = true;
        //    }
        //    return ismath;
        //}

        //private Boolean isJavaMethod(string name)
        //{
        //    Boolean isJava = false;
        //    string[] methodName = { "redirect_input", "redirect_output", "end_of_input", 
        //        "clear_window",	"display_number", "display_text", "draw_box", "draw_circle", 
        //        "draw_ellipse", "draw_line", "open_graph_window", "put_pixel", "set_font_size", 
        //        "set_window_title", "load_bitmap", "draw_bitmap" };

        //    for (int i = 0; i < methodName.Length; i++)
        //    {
        //        if (methodName[i].CompareTo(name.ToLower()) == 0)
        //            isJava = true;
        //    }
        //    return isJava;
        //}

        ///* if first parameter is a return value then hold printing call for method
        // * until get that first parameter (wait for stub generator)
        // * else print out called method
        // */
        //public object Emit_Call_Subchart(string name)
        //{

        //    Indent();
        //    MethodInformation mi = Procedures[name];
        //    holdMethod = name;
        //    if (hasReturnValue(mi, name))
        //    {
        //        turnOff = true;
        //    } else {
        //        stream.Write(name+"(");
        //    }
        //    return new subprogram();
        //}

        ///* if method with return also has parameters then need to print methd at this time
        // * else just print the comma
        // * assumption: first parameter might be a return value
        // */
        //public void Emit_Next_Parameter(object o)
        //{
        //    if (turnOff)
        //    {
        //        turnOff = false;
        //        stream.Write(holdVar + " = " + holdMethod + "(");
        //    }
        //    else
        //    {
        //        stream.Write(",");
        //    }
        //}

        ///* if method with return but no parameters then need to print assignment from method
        // * else just close method call 
        // */
        //public void Emit_Last_Parameter(object o)
        //{
        //    // somehow check if	o is a function	here
        //    if (!(o as subprogram).is_function)
        //    {
        //        if (turnOff)
        //        {
        //            turnOff = false;
        //            stream.Write(holdVar + " = " + holdMethod + "(");
        //        }
        //        stream.WriteLine(");");
        //    }
        //    else
        //    {
        //        stream.Write(")");
        //    }
        //}

        //public void Emit_No_Parameters(object o)
        //{
        //    // somehow check if	o is a function	here
        //    if (!(o as subprogram).is_function)
        //    {
        //        stream.WriteLine(");");
        //    }
        //    else
        //    {
        //        stream.Write(")");
        //    }
        //}

        //public void Emit_Is_Array2D(string name)
        //{
        //    if (arrays_2d.Contains(name.ToLower()))
        //    {
        //        stream.Write("true");
        //    }
        //    else
        //    {
        //        stream.Write("false");
        //    }
        //}

        //public void Emit_Is_Array(string name)
        //{
        //    if (arrays.Contains(name.ToLower()))
        //    {
        //        stream.Write("true");
        //    }
        //    else
        //    {
        //        stream.Write("false");
        //    }
        //}

        //public void Emit_Is_String(string name)
        //{
        //    stream.Write("new String(" + name + ")");
        //}

        //public void Emit_Is_Number(string name)
        //{
        //    stream.Write("new String(" + name + ")");
        //}

        //public void Emit_Or_Shortcut(parse_tree.boolean2 left, parse_tree.boolean_expression right)
        //{
        //    left.emit_code(this, 0);
        //    stream.Write(" || ");
        //    right.emit_code(this, 0);
        //}

        //public void Emit_And_Shortcut(parse_tree.boolean_parseable left,
        //    parse_tree.boolean2 right, bool left_negated)
        //{
        //    if (left_negated)
        //    {
        //        Emit_Not();
        //    }
        //    left.emit_code(this, 0);
        //    stream.Write(" && ");
        //    right.emit_code(this, 0);
        //}

        //public void Emit_Random()
        //{
        //    stream.Write("Math.random()");
        //}

        //public void Emit_Random_2(double first, double last)
        //{
        //    stream.Write("To_Color(Math.random() * " + (last - first) + " + " + first + ")");
        //}

        //public void Emit_Times()
        //{
        //    stream.Write(" * ");
        //}

        //public void Emit_Divide()
        //{
        //    stream.Write(" / ");
        //}

        //public void Emit_Plus()
        //{
        //    stream.Write(" + ");
        //}

        //public void Emit_Unary_Minus()
        //{
        //    stream.Write(" - ");
        //}

        //public void Emit_Minus()
        //{
        //    stream.Write(" - ");
        //}

        //public void Emit_Mod()
        //{
        //    stream.Write(" % ");
        //}

        //public void Emit_Rem()
        //{
        //    stream.Write(" % ");
        //}

        //public void Emit_Exponentiation()
        //{
        //    stream.Write(" Math.pow(??, ??) ");
        //}

        //public void Emit_Get_Mouse_Button()
        //{
        //    stream.WriteLine(");");
        //}

        //public void Emit_Get_Click(int x_or_y)
        //{
        //    if (x_or_y == 0)
        //    {
        //        stream.Write("Get_Click_X");
        //    }
        //    else
        //    {
        //        stream.Write("Get_Click_Y");
        //    }
        //}

        //public void Emit_Array_Size(string name)
        //{
        //    stream.Write(name + ".length");
        //}

        //public void Emit_String_Length()
        //{
        //    stream.Write(".length");
        //}

        ///* if return value then hold the variable to use as assignment 
        // * otherwise print out variable as part of parameter list
        // * assumption: this is first arg on parameter list and it is a return value
        // */
        //public void Emit_Load(string name)
        //{
        //    if (turnOff == false)
        //    {
        //        stream.Write(name);
        //    }
        //    else
        //    {
        //        holdVar = name;
        //    }
        //}

        //public void Emit_Load_Array_Start(string name)
        //{
        //    stream.Write(name + "[");
        //}

        //public void Emit_Load_Array_After_Index(string name)
        //{
        //    stream.Write("]");
        //}

        //public void Emit_Load_Array_2D_Start(string name)
        //{
        //    stream.Write(name + "[");
        //}

        //public void Emit_Load_Array_2D_Between_Indices()
        //{
        //    stream.Write("][");
        //}

        //public void Emit_Load_Array_2D_After_Indices(string name)
        //{
        //    stream.Write("]");
        //}

        //public void Emit_Relation(int relation)
        //{
        //    switch (relation)
        //    {
        //        case 1:	//gt
        //            stream.Write(" > ");
        //            break;
        //        case 2:	//ge
        //            stream.Write(" >= ");
        //            break;
        //        case 3:	//lt
        //            stream.Write(" < ");
        //            break;
        //        case 4:	//le
        //            stream.Write(" <= ");
        //            break;
        //        case 5:	//eq
        //            stream.Write(" == ");
        //            break;
        //        case 6:	//ne
        //            stream.Write(" != ");
        //            break;
        //    }
        //}

        //public void Emit_Sleep()
        //{
        //    Indent();
        //    stream.Write("sleep(");
        //}

        //public void Emit_Past_Sleep()
        //{
        //    stream.WriteLine(");");
        //}

        //public void Emit_And()
        //{
        //    stream.Write(" && ");
        //}

        //public void Emit_Or()
        //{
        //    stream.Write(" || ");
        //}

        //public void Emit_Not()
        //{
        //    stream.Write("!");
        //}

        //public void Emit_Xor()
        //{
        //    stream.Write(" ^ ");
        //}

        //public void Emit_To_Integer()
        //{
        //    stream.Write("intValue()");
        //}

        //public void Emit_Load_Boolean(Boolean val)
        //{
        //    stream.Write(val.ToString());
        //}

        //public void Emit_Load_Number(double val)
        //{
        //    stream.Write(val.ToString());
        //}

        //public void Emit_Load_String(string val)
        //{
        //    stream.Write('"' + val + '"');
        //}

        //// not used if reading file else string to be displayed
        //public void Emit_Load_String_Const(string val)
        //{
        //    if (!readFile)
        //        if (val.Length > 0)
        //        {
        //            stream.Write("\""+val+"\"");
        //        }
        //        else
        //        {
        //            stream.Write("\"\"");
        //        }
        //}

        //public void Emit_Conversion(int o)
        //{
        //    // type	Conversions	is (To_Integer,	To_Float, To_String, Number_To_String, To_Bool,	To_Color);
        //    switch (o)
        //    {
        //        case 0:
        //            break;
        //        case 1:
        //            break;
        //        case 2:
        //            break;
        //        case 3:
        //            //stream.Write("(");
        //            break;
        //        case 4:
        //            break;
        //        case 5:
        //            break;
        //    }
        //}

        //public void Emit_End_Conversion(int o)
        //{
        //    switch (o)
        //    {
        //        case 0:
        //            break;
        //        case 1:
        //            break;
        //        case 2:
        //            break;
        //        case 3:
        //            //stream.Write(").toString()");
        //            break;
        //        case 4:
        //            break;
        //        case 5:
        //            break;
        //    }
        //}

        //public void Emit_Plugin_Call(string name, parse_tree.parameter_list parameters)
        //{
        //    if (interpreter_pkg.is_plugin_procedure(name))
        //    {
        //        Indent();
        //    }
        //    stream.Write(name);
        //    if (interpreter_pkg.plugin_parameter_count(name) > 0)
        //    {

        //        stream.Write("(");
        //    }
        //    parse_tree.parameter_list pl = parameters;
        //    while (pl != null)
        //    {
        //        ((parse_tree.expr_output)pl.parameter).expr.emit_code(this, 0);
        //        pl = pl.next;
        //        if (pl != null)
        //        {
        //            stream.Write(",");
        //        }
        //    }
        //    if (interpreter_pkg.plugin_parameter_count(name) > 0)
        //    {
        //        stream.Write(")");
        //    }
        //    if (interpreter_pkg.is_plugin_procedure(name))
        //    {
        //        stream.WriteLine(";");
        //    }
        //}
        //// If statements are backwards right now; this may be fixed in the new version of RAPTOR
        //public object If_Start()
        //{
        //    Indent();
        //    stream.Write("if (");
        //    return null;
        //}

        //public void If_Then_Part(object o)
        //{
        //    stream.WriteLine(")");
        //    Indent();
        //    stream.WriteLine("{");
        //    indent_level += 3;
        //}

        //public void If_Else_Part(object o)
        //{
        //    indent_level -= 3;
        //    Indent();
        //    stream.WriteLine("}");
        //    Indent();
        //    stream.WriteLine("else");
        //    Indent();
        //    stream.WriteLine("{");
        //    indent_level += 3;
        //}

        //public void If_Done(object o)
        //{
        //    indent_level -= 3;
        //    Indent();
        //    stream.WriteLine("}");
        //}

        //public object Loop_Start(bool is_while, bool notUsed)
        //{
        //    // We don't need this - it should be turned off in the new version of RAPTOR
        //    if (!is_while)
        //    {
        //        Indent();
        //        stream.WriteLine("while	(1)");
        //        Indent();
        //        stream.WriteLine("{");
        //    }
        //    // But we do need this
        //    else
        //    {
        //        Indent();
        //        stream.Write("while (");
        //    }
        //    indent_level += 3;
        //    return is_while as object;
        //}

        //public void Loop_Start_Condition(object o)
        //{
        //    if (((bool)o) == true)
        //    {
        //    }
        //    else
        //    {
        //        Indent();
        //        stream.Write("if (");
        //    }
        //}

        //public void Loop_End_Condition(object o)
        //{
        //    if (((bool)o) == true)
        //    {
        //        stream.WriteLine(")");
        //        indent_level -= 3;
        //        Indent();
        //        stream.WriteLine("{");
        //        indent_level += 3;
        //    }
        //    else
        //    {
        //        stream.WriteLine(")	break;");
        //    }
        //}

        //public void Loop_End(object o)
        //{
        //    indent_level -= 3;
        //    Indent();
        //    stream.WriteLine("}");
        //}

        //public void Array_2D_Assignment_Start(string name)
        //{
        //    stream.Write(name + "[");
        //}

        //public void Array_2D_Assignment_Between_Indices()
        //{
        //    stream.Write("][");
        //}

        //public void Array_2D_Assignment_After_Indices()
        //{
        //    stream.Write("] = ");
        //}

        //public void Array_2D_Assignment_PastRHS()
        //{
        //    stream.WriteLine(";");
        //}

        //public void Array_1D_Assignment_Start(string name)
        //{
        //    stream.Write(name + "[");
        //}

        //public void Array_1D_Assignment_After_Index()
        //{
        //    stream.Write("]	= ");
        //}

        //public void Array_1D_Assignment_PastRHS()
        //{
        //    stream.WriteLine(";");
        //}

        //public void Variable_Assignment_Start(string name)
        //{
        //    stream.Write(name + " = ");
        //}

        //public void Variable_Assignment_PastRHS()
        //{
        //    stream.WriteLine(";");
        //}

        //private enum input_kind { variable, array, array2d }

        //private string input_name;

        //private input_kind kind_of_input;

        //private parse_tree.expression input_reference;

        //private parse_tree.expression input_reference2;


        //// if file then readLine else display input dialog
        //public void Input_Start_Variable(string name)
        //{
        //    input_name = name;
        //    kind_of_input = input_kind.variable;
        //    Indent();
        //    if (readFile)
        //        stream.WriteLine(input_name + " = read???();");
        //    else
        //        stream.Write(input_name + " = get???(");
        //    // indent_level -= 3;
        //}

        //public void Input_Start_Array_1D(string name, parse_tree.expression reference)
        //{
        //    input_name = name;
        //    kind_of_input = input_kind.array;
        //    input_reference = reference;
        //    Indent();
        //    stream.Write(input_name + "[");
        //    input_reference.emit_code(this, 0);
        //    if (readFile)
        //        stream.Write("] = read???();");
        //    else
        //        stream.Write("] = get???(");
        //}

        //public void Input_Start_Array_2D(string name, parse_tree.expression reference,
        //    parse_tree.expression reference2)
        //{
        //    input_name = name;
        //    kind_of_input = input_kind.array2d;
        //    input_reference = reference;
        //    input_reference2 = reference2;
        //    Indent();
        //    stream.Write(input_name + "[");
        //    input_reference.emit_code(this, 0);
        //    stream.Write("][");
        //    input_reference2.emit_code(this, 0);
        //    if (readFile)
        //        stream.Write("] = read???();");
        //    else
        //        stream.Write("] = get???(");
        //}

        //public void Input_Past_Prompt()
        //{
        //    switch (kind_of_input)
        //    {
        //        case input_kind.array:
        //            if (!readFile)
        //                stream.WriteLine(");");
        //        break;

        //        case input_kind.array2d:
        //            if (!readFile)
        //                stream.WriteLine(");");
        //        break;

        //        case input_kind.variable:
        //            if (!readFile)
        //                stream.WriteLine(");");
        //        break;
        //    }            
        //}

        //public void Output_Start(bool has_newline, bool is_string)
        //{
        //    Indent();
        //    if (!has_newline)
        //    {
        //        if (writeFile)
        //            stream.Write("writeLine(");
        //        else
        //            stream.Write("print(");
        //    }

        //    else
        //    {
        //        if (writeFile)
        //            stream.Write("writeLine(");
        //        else
        //            stream.Write("printLine(");
        //    }
        //}

        //public void Output_Past_Expr(bool has_newline, bool is_string)
        //{
        //    stream.WriteLine(");");
        //}

        //// dependent on getting returnStatement from parameter building
        //public void Done_Method()
        //{

        //        // We write return statements here
        //        indent_level -= 3;
        //        Indent();
        //        if (returnStatement != null)
        //        {
        //            Indent();
        //            stream.WriteLine(returnStatement);
        //            Indent();
        //        }
        //        returnStatement = null;

        //        if (current_method == null)
        //        {
        //            stream.WriteLine("} // close main");
        //        }
        //        else
        //        {
        //            stream.WriteLine("} // close " + current_method.Name);
        //            stream.WriteLine(" ");
        //        }
        //}

        //public void Emit_Load_Character(char val)
        //{
        //    stream.Write("'" + val + "'");
        //}

        //public void Emit_Is_Character(string name)
        //{
        //    stream.Write("Is_Character(" + name + ")");
        //}
        //public void emit_dereference()
        //{
        //    stream.Write(".");
        //}
        //public void Finish()
        //{
        //    indent_level -= 3;
        //    Indent();
        //    stream.WriteLine("} // close " + main_name);
        //    stream.Close();
        //    try
        //    {
        //        System.Diagnostics.Process.Start(file_name);
        //    }
        //    catch
        //    {
        //        System.Windows.Forms.MessageBox.Show("generated	to:	" + file_name);
        //    }
        //}
        //public object create_object(string name)
        //{
        //    stream.Write("new " + name + "(");
        //    return new subprogram(true);
        //}
        //public object emit_call_oo_method(string name, bool is_function)
        //{
        //    stream.Write(name + "(");
        //    return new subprogram(is_function);
        //}
        //#endregion

    }
}