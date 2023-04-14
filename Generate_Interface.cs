using System;
using System.Collections.Generic;
using System.Text;
using parse_tree;

namespace raptor
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // WARNING!  This file is for historical use-- keep synched with generate_interface.ads
    // you really want generate_interface_pkg.typ
    // CHANGES MADE HERE DON'T AFFECT ANYTHING!!!
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public interface Generate_Interface
    {
        // any implementation of this interface should have two constructors
        // a no arg constructor which is used for calling the informational methods, and
        // then a 1 arg constructor (string) which takes in a filename

        #region informational_methods
        // Should postfix order (e.g. "6 3 +") be done instead of infix? ("6 + 3")
        bool Is_Postfix();

        // provide an & in front of character to underline, as "&Ada", "C&++", "&Java", "C&#"
        string Get_Menu_Name();
        #endregion

        // called multiple times at the beginning (once for each procedure)
        void Declare_Procedure(string name, string[] args, bool[] arg_is_input,
           bool[] arg_is_output);

        // called at the start of coding this method
        void Start_Method(string name);

        #region emitvariabledeclarations
        // RAPTOR has detected that this name refers to a 2D array, 1D array, variable (number or string)
        // name may refer to a parameter
        void Declare_As_2D_Array(string name);
        void Declare_As_1D_Array(string name);
        void Declare_As_Variable(string name);
        void Declare_String_Variable(string name);
        void Done_Variable_Declarations();
        #endregion

        #region emitmethodbody

        void Emit_Left_Paren();
        void Emit_Right_Paren();



        // RAPTOR does Call_Method 1st for Infix, last for Postfix
        // Emit_Next_Parameter called between parameters
        // Emit_Last_Parameter called after last parameter (Infix only)
        // name comes from lexer.ads
        object Emit_Call_Method(int name);
        object Emit_Call_Subchart(string name);
        void Emit_Next_Parameter(object o);
        void Emit_Last_Parameter(object o);
        void Emit_No_Parameters(object o);

        void Emit_Is_Array2D(string name);
        void Emit_Is_Array(string name);
        void Emit_Is_String(string name);
        void Emit_Is_Number(string name);
        void Emit_Is_Character(string name);
        // Boolean operators
        void Emit_Or_Shortcut(Boolean2 left, Boolean_Expression right);
        void Emit_And_Shortcut(Boolean_Parseable left, Boolean2 right, bool left_negated);


        // Generate a random number in [0,1)
        void Emit_Random();
        // Generate a random number in [first,last)
        void Emit_Random(double first, double last);

        // Operators (see Is_Postfix)
        void Emit_Times();
        void Emit_Divide();
        void Emit_Plus();
        void Emit_Unary_Minus();
        void Emit_Minus();
        void Emit_Mod();
        void Emit_Rem();
        void Emit_Exponentiation();

        void Emit_Get_Mouse_Button();
        // x is 0, y is 1
        void Emit_Get_Click(int x_or_y);
        void Emit_Array_Size(string name);
        void Emit_String_Length();
        void Emit_Load(string name);

        // 1 gt, 2 ge, 3 lt, 4 le, 5 eq, 6 ne
        void Emit_Relation(int relation);
        void Emit_Sleep();
        void Emit_Past_Sleep();
        void Emit_And();
        void Emit_Or();
        void Emit_Not();
        void Emit_Xor();
        void Emit_To_Integer();
        void Emit_Load_Boolean(bool val);
        void Emit_Load_Number(double val);
        void Emit_Load_String(string val);
        void Emit_Load_String_Const(string val);
        void Emit_Load_Character(char val);

        // Conversions (numbered in parse_tree.adb)
        void Emit_Conversion(int o);
        void Emit_End_Conversion(int o);

        void Emit_Plugin_Call(string name, Parameter_List parameters);

        // things to do with IF statements
        // returns an object which will be passed to things belonging to the same IF
        object If_Start();
        void If_Then_Part(object o);
        void If_Else_Part(object o);
        void If_Done(object o);

        // things to do with LOOP statments
        // returns an object which will be passed to things belonging to the same IF
        // is_while is true if there's nothing before the condition
        // (can be naturally expressed as while)
        // is_negated expresses if the condition started with a not (which will have
        // been removed).
        object Loop_Start(bool is_while, bool is_negated);
        void Loop_Start_Condition(object o);
        void Loop_End_Condition(object o);
        void Loop_End(object o);

        // may also be called with name of a string variable
        void Emit_Load_Array_Start(string name);
        void Emit_Load_Array_After_Index(string name);

        void Emit_Load_Array_2D_Start(string name);
        void Emit_Load_Array_2D_Between_Indices();
        void Emit_Load_Array_2D_After_Indices(string name);

        // things to do with 2D array assignment
        void Array_2D_Assignment_Start(string name);
        void Array_2D_Assignment_Between_Indices();
        void Array_2D_Assignment_After_Indices();
        void Array_2D_Assignment_PastRHS();

        // things to do with 1D array assignment
        void Array_1D_Assignment_Start(string name);
        void Array_1D_Assignment_After_Index();
        void Array_1D_Assignment_PastRHS();

        // things to do with variable assignment
        void Variable_Assignment_Start(string name);
        void Variable_Assignment_PastRHS();

        // input parallelogram (does start, then loads prompt, then does past_prompt)
        void Input_Start_Variable(string name);
        void Input_Start_Array_1D(string name, parse_tree.Expression reference);
        void Input_Start_Array_2D(string name, parse_tree.Expression reference,
            parse_tree.Expression reference2);
        void Input_Past_Prompt();

        // output parallelogram
        void Output_Start(bool has_newline, bool is_string);
        void Output_Past_Expr(bool has_newline, bool is_string);
        #endregion

        // called at end of method body
        void Done_Method();

        // called at the end to do cleanup
        void Finish();
    }
}
