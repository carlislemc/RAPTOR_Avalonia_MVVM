using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using raptor;
using RAPTOR_Avalonia_MVVM;
using System.Collections;
using RAPTOR_Avalonia_MVVM.ViewModels;
using RAPTOR_Avalonia_MVVM.Views;
using System.Collections.ObjectModel;
using RAPTOR_Avalonia_MVVM.Controls;
using Avalonia.Threading;
using Avalonia.Controls;

namespace parse_tree
{
    public class Emit_Functions
    {
        public static void Emit_Conversion(int o, Generate_Interface gen)
        {
            Generate_IL gil = (Generate_IL)gen;
            switch ((Parseable.Conversions)o)
            {
                case Parseable.Conversions.To_Integer:
                    gil.Emit_Method("numbers.Numbers", "integer_of");
                    break;
                case Parseable.Conversions.To_Float:
                    gil.Emit_Method("numbers.Numbers", "long_float_of");
                    break;
                case Parseable.Conversions.To_String:
                    gil.Emit_Method("numbers.Numbers", "string_of");
                    break;
                case Parseable.Conversions.Number_To_String:
                    gil.Emit_Method("numbers.Numbers", "msstring_console_view_image");
                    break;
                case Parseable.Conversions.To_Bool:
                    gil.Emit_Method("numbers.Numbers", "integer_of");
                    break;
                case Parseable.Conversions.To_Color:
                    gil.Emit_Method("numbers.Numbers", "integer_of");
                    break;
                case Parseable.Conversions.Char_To_Int:
                    gil.Emit_Method("numbers.Numbers", "character_of");
                    gil.Emit_Method("numbers.Numbers", "make_value__3");
                    break;
                case Parseable.Conversions.Int_To_Char:
                    gil.Emit_Method("numbers.Numbers", "integer_of");
                    gil.Emit_Method("numbers.Numbers", "make_character_value");
                    break;

            }
        }
        public static void emit_method_call_il(int n, int i, Generate_Interface gen)
        {
            Generate_IL gil = (Generate_IL)gen;
            string s = (Token_Type)n + "";
            s = s.ToLower();
            switch (s)
            {
                case "random":
                    gen.Emit_Random();
                    break;
                case "random_color":
                    gen.Emit_Random(0.0, 16.0);
                    break;
                case "random_extended_color":
                    gen.Emit_Random(0.0, 242.0);
                    break;
                case "redirect_output_append":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.raptor_files", "redirect_output");
                    break;
                case "set_precision":
                    gil.Emit_Method("numbers.Numbers", "Set_Precision");
                    break;
                case "redirect_input":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.raptor_files", "redirect_input");
                    break;
                case "redirect_output":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.raptor_files", "redirect_output");
                    break;
                case "clear_console":
                    gil.Emit_Method("raptor.Runtime", "clearConsole");
                    break;
                case "clear_window":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "ClearWindow");
                    break;
                case "close_graph_window":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "CloseGraphWindow");
                    break;
                case "freeze_graph_window":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "FreezeGraphWindow");
                    break;
                case "unfreeze_graph_window":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "UnFreezeGraphWindow");
                    break;
                case "update_graph_window":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "UpdateGraphWindow");
                    break;
                case "set_font_size":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "FontSize");
                    break;
                case "display_number":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "DisplayNumber");
                    break;
                case "display_text":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "DisplayText");
                    break;
                case "draw_bitmap":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "DrawBitmap");
                    break;
                case "draw_arc":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "DrawArc");
                    break;
                case "draw_box":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "DrawBox");
                    break;
                case "draw_circle":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "DrawCircle");
                    break;
                case "draw_ellipse":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "DrawEllipse");
                    break;
                case "draw_ellipse_rotate":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "DrawEllipseRotate");
                    break;
                case "draw_line":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "DrawLine");
                    break;
                case "flood_fill":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "FloodFill");
                    break;
                case "open_graph_window":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "OpenGraphWindow");
                    break;
                case "wait_for_key":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "WaitForKey");
                    break;
                case "wait_for_mouse_button":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "WaitForMouseButton");
                    break;
                case "put_pixel":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "PutPixel");
                    break;
                case "set_window_title":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "SetWindowTitle");
                    break;
                case "save_graph_window":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "SaveGraphWindow");
                    break;
                case "pi":
                    gil.Emit_Load_Static("numbers.Numbers", "Pi");
                    break;
                case "e":
                    gil.Emit_Load_Static("numbers.Numbers", "E");
                    break;
                case "black":
                case "blue":
                case "green":
                case "cyan":
                case "red":
                case "magenta":
                case "brown":
                case "light_gray":
                case "dark_gray":
                case "light_blue":
                case "light_green":
                case "light_cyan":
                case "light_red":
                case "light_magenta":
                case "yellow":
                case "pink":
                case "purple":
                case "white":
                    gil.Emit_Load_Number(n - (int)Token_Type.Black);
                    break;
                case "left_button":
                case "unfilled":
                case "no":
                case "false":
                    gil.Emit_Load_Number(0.0);
                    break;
                case "right_button":
                case "filled":
                case "yes":
                case "true":
                    gil.Emit_Load_Number(1.0);
                    break;
                case "get_window_height":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "GetWindowHeight");
                    gil.Emit_Method("numbers.Numbers", "make_value__3");
                    break;
                case "get_window_width":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "GetWindowWidth");
                    gil.Emit_Method("numbers.Numbers", "make_value__3");
                    break;
                case "get_font_height":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "GetFontHeight");
                    gil.Emit_Method("numbers.Numbers", "make_value__3");
                    break;
                case "get_font_width":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "GetFontWidth");
                    gil.Emit_Method("numbers.Numbers", "make_value__3");
                    break;
                case "get_max_width":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "GetMaxWidth");
                    gil.Emit_Method("numbers.Numbers", "make_value__3");
                    break;
                case "get_max_height":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "GetMaxHeight");
                    gil.Emit_Method("numbers.Numbers", "make_value__3");
                    break;
                case "get_mouse_x":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "GetMouseX");
                    gil.Emit_Method("numbers.Numbers", "make_value__3");
                    break;
                case "get_mouse_y":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "GetMouseY");
                    gil.Emit_Method("numbers.Numbers", "make_value__3");
                    break;
                case "get_key":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "GetKey");
                    gil.Emit_Method("numbers.Numbers", "integer_of");
                    gil.Emit_Method("numbers.Numbers", "make_value__3");
                    break;
                case "get_key_string":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "GetKey");
                    gil.Emit_Method("numbers.Numbers", "make_string_value");
                    break;
                case "get_pixel":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "GetPixel");
                    break;
                case "load_bitmap":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "LoadBitmap");
                    break;
                case "closest_color":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "GetClosestColor");
                    break;
                case "sinh":
                    gil.Emit_Method("numbers.Numbers", "Sinh");
                    break;
                case "tanh":
                    gil.Emit_Method("numbers.Numbers", "Tanh");
                    break;
                case "cosh":
                    gil.Emit_Method("numbers.Numbers", "Cosh");
                    break;
                case "arcsinh":
                    gil.Emit_Method("numbers.Numbers", "ArcSinh");
                    break;
                case "arctanh":
                    gil.Emit_Method("numbers.Numbers", "ArcTanh");
                    break;
                case "arccosh":
                    gil.Emit_Method("numbers.Numbers", "ArcCosh");
                    break;
                case "coth":
                    gil.Emit_Method("numbers.Numbers", "Coth");
                    break;
                case "arccoth":
                    gil.Emit_Method("numbers.Numbers", "ArcCoth");
                    break;
                case "sqrt":
                    gil.Emit_Method("numbers.Numbers", "Sqrt");
                    break;
                case "floor":
                    gil.Emit_Method("numbers.Numbers", "Floor");
                    break;
                case "ceiling":
                    gil.Emit_Method("numbers.Numbers", "Ceiling");
                    break;
                case "abs":
                    gil.Emit_Method("numbers.Numbers", "Abs");
                    break;
                case "log":
                    if(i == 1)
                    {
                        gil.Emit_Load_Static("numbers.Numbers", "E");
                    }
                    gil.Emit_Method("numbers.Numbers", "Log");
                    break;
                case "min":
                    gil.Emit_Method("numbers.Numbers", "findMin");
                    break;
                case "max":
                    gil.Emit_Method("numbers.Numbers", "findMax");
                    break;
                case "sin":
                    if (i == 1)
                    {
                        gil.Emit_Load_Static("numbers.Numbers", "Two_Pi");
                    }
                    gil.Emit_Method("numbers.Numbers", "Sin");
                    break;
                case "cos":
                    if (i == 1)
                    {
                        gil.Emit_Load_Static("numbers.Numbers", "Two_Pi");
                    }
                    gil.Emit_Method("numbers.Numbers", "Cos");
                    break;
                case "tan":
                    if (i == 1)
                    {
                        gil.Emit_Load_Static("numbers.Numbers", "Two_Pi");
                    }
                    gil.Emit_Method("numbers.Numbers", "Tan");
                    break;
                case "cot":
                    if (i == 1)
                    {
                        gil.Emit_Load_Static("numbers.Numbers", "Two_Pi");
                    }
                    gil.Emit_Method("numbers.Numbers", "Cot");
                    break;
                case "arcsin":
                    if(i == 1)
                    {
                        gil.Emit_Load_Static("numbers.Numbers", "Two_Pi");
                    }
                    gil.Emit_Method("numbers.Numbers", "ArcSin");
                    break;
                case "arccos":
                    if (i == 1)
                    {
                        gil.Emit_Load_Static("numbers.Numbers", "Two_Pi");
                    }
                    gil.Emit_Method("numbers.Numbers", "ArcCos");
                    break;
                case "arctan":
                    if (i == 1)
                    {
                        gil.Emit_Load_Static("numbers.Numbers", "Two_Pi");
                    }
                    gil.Emit_Method("numbers.Numbers", "ArcTan");
                    break;
                case "arccot":
                    if (i == 1)
                    {
                        gil.Emit_Load_Static("numbers.Numbers", "Two_Pi");
                    }
                    gil.Emit_Method("numbers.Numbers", "ArcCot");
                    break;
                case "is_open":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "IsOpen");
                    break;
                case "key_hit":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "KeyHit");
                    break;
                case "key_down":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "KeyDown");
                    break;
                case "end_of_input":
                    //NEED TO DO
                    throw new NotImplementedException();
                    break;
                case "mouse_button_down":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "MouseButtonDown");
                    break;
                case "mouse_button_pressed":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "MouseButtonPressed");
                    break;
                case "mouse_button_released":
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "MouseButtonReleased");
                    break;
                case "play_sound":
                    gil.Emit_Method("numbers.Numbers", "string_of");
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "PlaySound");
                    break;
                case "play_sound_background":
                    gil.Emit_Method("numbers.Numbers", "string_of");
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "PlaySoundBackground");
                    break;
                case "play_sound_background_loop":
                    gil.Emit_Method("numbers.Numbers", "string_of");
                    gil.Emit_Method("RAPTOR_Avalonia_MVVM.ViewModels.GraphDialogViewModel", "PlaySoundBackgroundLoop");
                    break;
            }
            
        }
    }
    public abstract class Parseable
    {
        public abstract void Emit_Code(Generate_Interface gen);
        public abstract void compile_pass1(Generate_Interface gen);
        public enum Variable_Kind { Value, Array_1D, Array_2D };
        public enum Conversions { To_Integer, To_Float, To_String, Number_To_String, To_Bool, To_Color, Char_To_Int, Int_To_Char };
        public enum Context_Type { Assign_Context, Call_Context, Input_Context };
        public static Variable_Kind Emit_Kind = Variable_Kind.Value;
        public static Context_Type Emit_Context = Context_Type.Assign_Context;

        public void emit_parameter_number(Output p, Generate_Interface gen, int o = 0)
        {
            ((Expr_Output)p).expr.Emit_Code(gen);
        }

        public void emit_string_parameter(Output p, Generate_Interface gen)
        {
            ((Expr_Output)p).expr.Emit_Code(gen);
        }
    }
    public abstract class Value_Parseable : Parseable
    {
        public abstract numbers.value Execute(Lexer l);
    }
    public class Expression : Value_Parseable
    {
        public Value_Parseable left;
        public Expression(Value_Parseable left)
        {
            this.left = left;
        }
        public string get_class_decl() { return ""; }
        public static void Fix_Associativity(ref Expression e)
        {
            if (e is Binary_Expression)
            {
                if (((Binary_Expression) e).right is Binary_Expression)
                {
                    Binary_Expression temp = ((Binary_Expression)e).right as Binary_Expression;
                    ((Binary_Expression)e).right = new Expression(temp.left);
                    temp.left = e;
                    e = temp;
                    Fix_Associativity(ref e);
                }

            }
        }

        public override numbers.value Execute(Lexer l){
            return left.Execute(l);
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            left.Emit_Code(gen);
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            left.compile_pass1(gen);
        }

    }
    public abstract class Binary_Expression : Expression
    {
        public Expression right;
        public Binary_Expression(Value_Parseable left) : base(left) { }

    }
    public class Add_Expression : Binary_Expression
    {
        public Add_Expression(Value_Parseable left) : base(left) { }

        public override numbers.value Execute(Lexer l){

            numbers.value first = left.Execute(l);
            numbers.value second = right.Execute(l);
            return numbers.Numbers.addValues(first, second);
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            if (gen.Is_Postfix())
            {
                left.Emit_Code(gen);
                right.Emit_Code(gen);
                gen.Emit_Plus();
            }
            else
            {
                left.Emit_Code(gen);
                gen.Emit_Plus();
                right.Emit_Code(gen);
            }
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            left.compile_pass1(gen);
            right.compile_pass1(gen);
        }

    }
    public class Minus_Expression : Binary_Expression
    {
        public Minus_Expression(Value_Parseable left) : base(left) { }

        public override numbers.value Execute(Lexer l){

            numbers.value first = left.Execute(l);
            numbers.value second = right.Execute(l);
            return numbers.Numbers.subValues(first, second);
        }


        public override void Emit_Code(Generate_Interface gen)  
        {
            if (gen.Is_Postfix())
            {
                left.Emit_Code(gen);
                right.Emit_Code(gen);
                gen.Emit_Minus();
            }
            else
            {
                left.Emit_Code(gen);
                gen.Emit_Minus();
                right.Emit_Code(gen);
            }
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            left.compile_pass1(gen);
            right.compile_pass1(gen);
        }

    }

    // Procedure_Call => proc_id(Parameter_List) | plugin_id(Parameter_List) | tab_id(Parameter_List) |
    // lhs msuffix
    public abstract class Procedure_Call : Statement
    {
        public Token? id;
        public Parameter_List? param_list;

        public abstract bool is_tab_call();

        public abstract void Execute(Lexer l);

    }

    public class Proc_Call : Procedure_Call
    {

        public override bool is_tab_call()
        {
            return false;
        }

        public static void checkOpenGraph()
        {
            if (DotnetGraphControl.dngw == null)
            {
                throw new Exception("Graph window not open!");
            }
        }

        public override void Execute(Lexer l)
        {   
            MainWindowViewModel mw = MainWindowViewModel.GetMainWindowViewModel();
            string head = l.Get_Text(id.start, id.finish);
            Subchart sub = mw.mainSubchart();
            foreach(Subchart s in mw.theTabs){
                if(s.Header == head){
                    sub = s;
                }
            }
            string str = l.Get_Text(id.start, id.finish);
            numbers.value[] ps = new numbers.value[0];
            if (param_list != null)
            {
                Runtime.processing_parameter_list = true;
                ps = param_list.Execute(l);
            }

            if (str.ToLower() == "open_graph_window")
            {
                //Dispatcher.UIThread.InvokeAsync(() =>
                //{
                    int w = numbers.Numbers.integer_of(ps[0]);
                    int h = numbers.Numbers.integer_of(ps[1]);
                    GraphDialogViewModel.OpenGraphWindow(w, h);
                //}).Wait(-1);
                    
            }
            else if (str.ToLower() == "close_graph_window")
            {
                checkOpenGraph();
                Dispatcher.UIThread.Post(() =>
                {
                    GraphDialogViewModel.CloseGraphWindow();
                }, DispatcherPriority.Background);

            }
            else if (str.ToLower() == "freeze_graph_window")
            {
                checkOpenGraph();
                Dispatcher.UIThread.Post(() =>
                {
                    GraphDialogViewModel.FreezeGraphWindow();
                }, DispatcherPriority.Background);

            }
            else if (str.ToLower() == "unfreeze_graph_window")
            {
                checkOpenGraph();
                Dispatcher.UIThread.Post(() =>
                {
                    GraphDialogViewModel.UnFreezeGraphWindow();
                }, DispatcherPriority.Background);

            }
            else if (str.ToLower() == "update_graph_window")
            {
                checkOpenGraph();
                Dispatcher.UIThread.Post(() =>
                {
                    GraphDialogViewModel.UpdateGraphWindow();
                }, DispatcherPriority.Background);

            }
            else if (str.ToLower() == "draw_line")
            {
                checkOpenGraph();
                Dispatcher.UIThread.Post(() =>
                {
                    int x1 = numbers.Numbers.integer_of(ps[0]);
                    int y1 = numbers.Numbers.integer_of(ps[1]);
                    int x2 = numbers.Numbers.integer_of(ps[2]);
                    int y2 = numbers.Numbers.integer_of(ps[3]);
                    int c = numbers.Numbers.integer_of(ps[4]);
                    GraphDialogViewModel.DrawLine(x1, y1, x2, y2, (Color_Type)c);
                        
                }, DispatcherPriority.Background);
            }
            else if (str.ToLower() == "draw_box")
            {
                checkOpenGraph();
                Dispatcher.UIThread.Post(() =>
                {
                    int x1 = numbers.Numbers.integer_of(ps[0]);
                    int y1 = numbers.Numbers.integer_of(ps[1]);
                    int x2 = numbers.Numbers.integer_of(ps[2]);
                    int y2 = numbers.Numbers.integer_of(ps[3]);
                    int c = numbers.Numbers.integer_of(ps[4]);
                    bool fill = numbers.Numbers.integer_of(ps[5]) == 1;    
                    GraphDialogViewModel.DrawBox(x1, y1, x2, y2, (Color_Type)c, fill);
                }, DispatcherPriority.Background);
                
            }
            else if (str.ToLower() == "draw_circle")
            {
                checkOpenGraph();
                Dispatcher.UIThread.Post(() =>
                {
                    int x1 = numbers.Numbers.integer_of(ps[0]);
                    int y1 = numbers.Numbers.integer_of(ps[1]);
                    int rad = numbers.Numbers.integer_of(ps[2]);
                    int c = numbers.Numbers.integer_of(ps[3]);
                    bool fill = numbers.Numbers.integer_of(ps[4]) == 1;
                    GraphDialogViewModel.DrawCircle(x1, y1, rad, (Color_Type)c, fill);
                }, DispatcherPriority.Background);
            }
            else if (str.ToLower() == "draw_ellipse")
            {
                checkOpenGraph();
                Dispatcher.UIThread.Post(() =>
                {
                    int x1 = numbers.Numbers.integer_of(ps[0]);
                    int y1 = numbers.Numbers.integer_of(ps[1]);
                    int x2 = numbers.Numbers.integer_of(ps[2]);
                    int y2 = numbers.Numbers.integer_of(ps[3]);
                    int c = numbers.Numbers.integer_of(ps[4]);
                    bool fill = numbers.Numbers.integer_of(ps[5]) == 1;
                    GraphDialogViewModel.DrawEllipse(x1, y1, x2, y2, (Color_Type)c, fill);
                }, DispatcherPriority.Background);
            }
            else if (str.ToLower() == "draw_arc")
            {
                checkOpenGraph();
                Dispatcher.UIThread.Post(() =>
                {
                    int x1 = numbers.Numbers.integer_of(ps[0]);
                    int y1 = numbers.Numbers.integer_of(ps[1]);
                    int x2 = numbers.Numbers.integer_of(ps[2]);
                    int y2 = numbers.Numbers.integer_of(ps[3]);
                    int startx = numbers.Numbers.integer_of(ps[4]);
                    int starty = numbers.Numbers.integer_of(ps[5]);
                    int endx = numbers.Numbers.integer_of(ps[6]);
                    int endy = numbers.Numbers.integer_of(ps[7]);
                    int c = numbers.Numbers.integer_of(ps[8]);
                    GraphDialogViewModel.DrawArc(x1, y1, x2, y2, startx, starty, endx, endy, (Color_Type)c);
                }, DispatcherPriority.Background);
            }
            else if (str.ToLower() == "draw_ellipse_rotate")
            {
                checkOpenGraph();
                Dispatcher.UIThread.Post(() =>
                {
                    int x1 = numbers.Numbers.integer_of(ps[0]);
                    int y1 = numbers.Numbers.integer_of(ps[1]);
                    int x2 = numbers.Numbers.integer_of(ps[2]);
                    int y2 = numbers.Numbers.integer_of(ps[3]);
                    double angle = numbers.Numbers.long_float_of(ps[4]);
                    int c = numbers.Numbers.integer_of(ps[5]);
                    bool fill = numbers.Numbers.integer_of(ps[6]) == 1;
                    GraphDialogViewModel.DrawEllipseRotate(x1, y1, x2, y2, angle, (Color_Type)c, fill);
                }, DispatcherPriority.Background);
            }
            else if (str.ToLower() == "display_text")
            {
                checkOpenGraph();
                Dispatcher.UIThread.Post(() =>
                {
                    int x1 = numbers.Numbers.integer_of(ps[0]);
                    int y1 = numbers.Numbers.integer_of(ps[1]);
                    string t = "";
                    try
                    {
                        t = ps[2].S.Replace("\"", "");
                    }
                    catch
                    {
                        t = ps[2].C + "";
                    }
                    int c = numbers.Numbers.integer_of(ps[3]);
                    GraphDialogViewModel.DisplayText(x1, y1, t, (Color_Type)c);
                }, DispatcherPriority.Background);
            }
            else if (str.ToLower() == "display_number")
            {
                checkOpenGraph();
                Dispatcher.UIThread.Post(() =>
                {
                    int x1 = numbers.Numbers.integer_of(ps[0]);
                    int y1 = numbers.Numbers.integer_of(ps[1]);
                    double n = numbers.Numbers.long_float_of(ps[2]);
                    int c = numbers.Numbers.integer_of(ps[3]);
                    GraphDialogViewModel.DisplayNumber(x1, y1, n, (Color_Type)c);
                }, DispatcherPriority.Background);
            }
            else if (str.ToLower() == "set_font_size")
            {
                checkOpenGraph();
                Dispatcher.UIThread.Post(() =>
                {
                    int size = numbers.Numbers.integer_of(ps[0]);
                    GraphDialogViewModel.FontSize(size);
                }, DispatcherPriority.Background);
            }
            else if(str.ToLower() == "wait_for_key")
            {
                WaitForKey();
            }
            else if (str.ToLower() == "wait_for_mouse_button")
            {
                checkOpenGraph();
                mw.waitingForMouse = true;
                mw.mouseWait = ps[0].V == 86 ? Avalonia.Input.MouseButton.Left : Avalonia.Input.MouseButton.Right;
                Dispatcher.UIThread.Post(() =>
                {
                    Avalonia.Input.MouseButton b = ps[0].V == 86 ? Avalonia.Input.MouseButton.Left : Avalonia.Input.MouseButton.Right;
                    GraphDialogViewModel.WaitForMouseButton(b);
                    if (mw.myTimer != null)
                    {
                        mw.myTimer.Stop();
                    }
                }, DispatcherPriority.Background);

            }
            else if (str.ToLower() == "set_window_title")
            {
                checkOpenGraph();
                Dispatcher.UIThread.Post(() =>
                {
                    string t = numbers.Numbers.msstring_view_image(ps[0]).Replace("\"","");
                    GraphDialogViewModel.SetWindowTitle(t);
                }, DispatcherPriority.Background);

            }
            else if (str.ToLower() == "clear_window")
            {
                checkOpenGraph();
                Dispatcher.UIThread.Post(() =>
                {
                    int c = numbers.Numbers.integer_of(ps[0]);
                    GraphDialogViewModel.ClearWindow((Color_Type)c);
                }, DispatcherPriority.Background);

            }
            else if (str.ToLower() == "delay_for")
            { 
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    int s = numbers.Numbers.integer_of(ps[0]);
                    MainWindowViewModel mw = MainWindowViewModel.GetMainWindowViewModel();
                    if(mw.myTimer != null)
                    {
                        mw.myTimer.Stop();
                    }
                    GraphDialogViewModel.DelayFor(s);
                    if (mw.myTimer != null)
                    {
                        mw.myTimer.Start();
                    }
                }, DispatcherPriority.Background).Wait(-1);

            }
            else if (str.ToLower() == "flood_fill")
            {
                checkOpenGraph();
                Dispatcher.UIThread.Post(() =>
                {
                    int x = numbers.Numbers.integer_of(ps[0]);
                    int y = numbers.Numbers.integer_of(ps[1]);
                    int c = numbers.Numbers.integer_of(ps[2]);
                    GraphDialogViewModel.FloodFill(x,y,(Color_Type)c);
                }, DispatcherPriority.Background);

            }
            else if (str.ToLower() == "put_pixel")
            {
                checkOpenGraph();
                Dispatcher.UIThread.Post(() =>
                {
                    int x = numbers.Numbers.integer_of(ps[0]);
                    int y = numbers.Numbers.integer_of(ps[1]);
                    int c = numbers.Numbers.integer_of(ps[2]);
                    GraphDialogViewModel.PutPixel(x, y, (Color_Type)c);
                }, DispatcherPriority.Background);

            }
            else if (str.ToLower() == "draw_bitmap")
            {
                checkOpenGraph();
                Dispatcher.UIThread.Post(() =>
                {
                    int i = numbers.Numbers.integer_of(ps[0]);
                    int x = numbers.Numbers.integer_of(ps[1]);
                    int y = numbers.Numbers.integer_of(ps[2]);
                    int w = numbers.Numbers.integer_of(ps[3]);
                    int h = numbers.Numbers.integer_of(ps[4]);
                    GraphDialogViewModel.DrawBitmap(i,x,y,w,h);
                }, DispatcherPriority.Background);
            }
            else if (str.ToLower() == "save_graph_window")
            {
                checkOpenGraph();
                Dispatcher.UIThread.Post(() =>
                {
                    string f = ps[0].S;
                    GraphDialogViewModel.SaveGraphWindow(f);
                }, DispatcherPriority.Background);
            }
            else if(str.ToLower() == "get_mouse_button")
            {
                checkOpenGraph();
                Dispatcher.UIThread.InvokeAsync(async () =>
                {
                    Avalonia.Input.MouseButton m = ps[0].V == 86 ? Avalonia.Input.MouseButton.Left : Avalonia.Input.MouseButton.Right;
                    await GraphDialogViewModel.GetMouseButton(m);

                    numbers.value x = new numbers.value() { Kind = numbers.Value_Kind.Number_Kind, V = DotnetGraphControl.xLoc };
                    numbers.value y = new numbers.value() { Kind = numbers.Value_Kind.Number_Kind, V = DotnetGraphControl.yLoc };

                    ((Expr_Output)param_list.next.parameter).Assign_To(l, x);
                    ((Expr_Output)param_list.next.next.parameter).Assign_To(l, y);

                    DotnetGraphControl.mb = new Avalonia.Input.MouseButton();

                }, DispatcherPriority.Background).Wait(-1);
            }
            else if(str.ToLower() == "redirect_output")
            {
                raptor_files.redirect_output(ps[0]);

            }
            else if(str.ToLower() == "redirect_input")
            {
                raptor_files.redirect_input(ps[0]);

            }
            else if (str.ToLower() == "redirect_output_append")
            {
                raptor_files.redirect_output_append(ps[0]);

            }
            else if (str.ToLower() == "play_sound")
            {
                Dispatcher.UIThread.Post(() =>
                {
                    string soundFile = ps[0].S;
                    GraphDialogViewModel.PlaySound(soundFile);
                }, DispatcherPriority.Background);

            }
            else if (str.ToLower() == "play_sound_background")
            {
                Dispatcher.UIThread.Post(() =>
                {
                    string soundFile = ps[0].S;
                    GraphDialogViewModel.PlaySoundBackground(soundFile);
                }, DispatcherPriority.Background);

            }
            else if (str.ToLower() == "play_sound_background_loop")
            {
                Dispatcher.UIThread.Post(() =>
                {
                    string soundFile = ps[0].S;
                    GraphDialogViewModel.PlaySoundBackgroundLoop(soundFile);
                }, DispatcherPriority.Background);

            }
            return;
            
        }

        public static void WaitForKey()
        {
            MainWindowViewModel mw = MainWindowViewModel.GetMainWindowViewModel();
            mw.waitingForKey = true;
            Dispatcher.UIThread.Post(() =>
            {
                GraphDialogViewModel.WaitForKey();
                if (mw.myTimer != null)
                {
                    mw.myTimer.Stop();
                }
            }, DispatcherPriority.Background);
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            string s = Component.the_lexer.Get_Text(id.start, id.finish).ToLower();
            int functionName = (int)id.kind;
            Object o = new Object();
            if(s != "delay_for")
            {
                o = gen.Emit_Call_Method(functionName);
            }

            switch (s)
            {
                case "delay_for":
                    gen.Emit_Sleep();
                    emit_parameter_number(param_list.parameter, gen);
                    gen.Emit_Past_Sleep();
                    break;
                case "play_sound":
                case "play_sound_background":
                case "play_sound_background_loop":
                case "redirect_output":
                case "redirect_output_append":
                case "redirect_input":
                    emit_parameter_number(param_list.parameter, gen);
                    gen.Emit_Last_Parameter(o);
                    break;
                case "set_precision":
                case "wait_for_mouse_button":
                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Last_Parameter(o);
                    break;
                case "clear_console":
                case "close_graph_window":
                case "freeze_graph_window":
                case "unfreeze_graph_window":
                case "wait_for_key":
                case "update_graph_window":
                    gen.Emit_No_Parameters(o);
                    break;
                case "clear_window":
                    gen.Emit_Conversion((int)Conversions.To_Color);
                    emit_parameter_number(param_list.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Color);
                    gen.Emit_Last_Parameter(o);
                    break;
                case "set_font_size":
                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Last_Parameter(o);
                    break;
                case "display_text":
                case "display_number":
                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.Number_To_String);
                    emit_parameter_number(param_list.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.Number_To_String);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Color);
                    emit_parameter_number(param_list.next.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Color);
                    gen.Emit_Last_Parameter(o);
                    break;
                case "draw_bitmap":
                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.next.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.next.next.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Last_Parameter(o);
                    break;
                case "draw_arc":
                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.next.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.next.next.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.next.next.next.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.next.next.next.next.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.next.next.next.next.next.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Color);
                    emit_parameter_number(param_list.next.next.next.next.next.next.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Color);
                    gen.Emit_Last_Parameter(o);
                    break;
                case "draw_box":
                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.next.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Color);
                    emit_parameter_number(param_list.next.next.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Color);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Bool);
                    emit_parameter_number(param_list.next.next.next.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Bool);
                    gen.Emit_Last_Parameter(o);
                    break;
                case "draw_circle":
                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Color);
                    emit_parameter_number(param_list.next.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Color);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Bool);
                    emit_parameter_number(param_list.next.next.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Bool);
                    gen.Emit_Last_Parameter(o);
                    break;
                case "draw_ellipse":
                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.next.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Color);
                    emit_parameter_number(param_list.next.next.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Color);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Bool);
                    emit_parameter_number(param_list.next.next.next.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Bool);
                    gen.Emit_Last_Parameter(o);
                    break;
                case "draw_ellipse_rotate":
                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.next.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Float);
                    emit_parameter_number(param_list.next.next.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Float);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Color);
                    emit_parameter_number(param_list.next.next.next.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Color);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Bool);
                    emit_parameter_number(param_list.next.next.next.next.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Bool);
                    gen.Emit_Last_Parameter(o);
                    break;
                case "draw_line":
                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.next.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Color);
                    emit_parameter_number(param_list.next.next.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Color);
                    gen.Emit_Last_Parameter(o);
                    break;
                case "flood_fill":
                case "put_pixel":
                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Color);
                    emit_parameter_number(param_list.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Color);
                    gen.Emit_Last_Parameter(o);
                    break;
                case "open_graph_window":
                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);

                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Last_Parameter(o);
                    break;
                case "set_window_title":
                case "save_graph_window":
                    gen.Emit_Conversion((int)Conversions.To_String);
                    emit_parameter_number(param_list.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_String);
                    gen.Emit_Last_Parameter(o);
                    break;
                case "get_mouse_button":
                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(param_list.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Get_Mouse_Button();
                    ((Expr_Output)this.param_list.next.parameter).Emit_Store_Dotnetgraph_Int_Func(gen, 0);
                    ((Expr_Output)this.param_list.next.next.parameter).Emit_Store_Dotnetgraph_Int_Func(gen, 1);
                    break;
            }
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            string str = Component.the_lexer.Get_Text(id.start, id.finish);
            if(str.ToLower() == "delay_for")
            {
                param_list.parameter.compile_pass1(gen);
            }
            else if (str.ToLower() == "set_precision")
            {
                param_list.parameter.compile_pass1(gen);
            }
            else if (str.ToLower() == "play_sound" || str.ToLower()=="play_sound_background" ||
                str.ToLower()=="play_sound_background_loop")
            {
                param_list.parameter.compile_pass1(gen);
            }
            else if (str.ToLower() == "redirect_input")
            {
                param_list.parameter.compile_pass1(gen);
            }
            else if (str.ToLower() == "redirect_output" || str.ToLower() == "redirect_output_append")
            {
                param_list.parameter.compile_pass1(gen);
            }
            else if (str.ToLower() == "clear_window")
            {
                param_list.parameter.compile_pass1(gen);
            }
            else if (str.ToLower() == "set_font_size")
            {
                param_list.parameter.compile_pass1(gen);
            }
            else if (str.ToLower() == "display_number")
            {
                param_list.parameter.compile_pass1(gen);
                param_list.next.parameter.compile_pass1(gen);
                param_list.next.next.parameter.compile_pass1(gen);
                param_list.next.next.next.parameter.compile_pass1(gen);
            }
            else if (str.ToLower() == "draw_bitmap")
            {
                param_list.parameter.compile_pass1(gen);
                param_list.next.parameter.compile_pass1(gen);
                param_list.next.next.parameter.compile_pass1(gen);
                param_list.next.next.next.parameter.compile_pass1(gen);
                param_list.next.next.next.next.parameter.compile_pass1(gen);
            }
            else if (str.ToLower() == "display_text")
            {
                param_list.parameter.compile_pass1(gen);
                param_list.next.parameter.compile_pass1(gen);
                param_list.next.next.parameter.compile_pass1(gen);
                param_list.next.next.next.parameter.compile_pass1(gen);
            }
            else if (str.ToLower() == "draw_arc")
            {
                param_list.parameter.compile_pass1(gen);
                param_list.next.parameter.compile_pass1(gen);
                param_list.next.next.parameter.compile_pass1(gen);
                param_list.next.next.next.parameter.compile_pass1(gen);
                param_list.next.next.next.next.parameter.compile_pass1(gen);
                param_list.next.next.next.next.next.parameter.compile_pass1(gen);
                param_list.next.next.next.next.next.next.parameter.compile_pass1(gen);
                param_list.next.next.next.next.next.next.next.parameter.compile_pass1(gen);
                param_list.next.next.next.next.next.next.next.next.parameter.compile_pass1(gen);
            }
            else if (str.ToLower() == "draw_box")
            {
                param_list.parameter.compile_pass1(gen);
                param_list.next.parameter.compile_pass1(gen);
                param_list.next.next.parameter.compile_pass1(gen);
                param_list.next.next.next.parameter.compile_pass1(gen);
                param_list.next.next.next.next.parameter.compile_pass1(gen);
                param_list.next.next.next.next.next.parameter.compile_pass1(gen);
            }
            else if (str.ToLower() == "draw_circle")
            {
                param_list.parameter.compile_pass1(gen);
                param_list.next.parameter.compile_pass1(gen);
                param_list.next.next.parameter.compile_pass1(gen);
                param_list.next.next.next.parameter.compile_pass1(gen);
                param_list.next.next.next.next.parameter.compile_pass1(gen);
            }
            else if (str.ToLower() == "draw_ellipse")
            {
                param_list.parameter.compile_pass1(gen);
                param_list.next.parameter.compile_pass1(gen);
                param_list.next.next.parameter.compile_pass1(gen);
                param_list.next.next.next.parameter.compile_pass1(gen);
                param_list.next.next.next.next.parameter.compile_pass1(gen);
                param_list.next.next.next.next.next.parameter.compile_pass1(gen);
            }
            else if (str.ToLower() == "draw_ellipse_rotate")
            {
                param_list.parameter.compile_pass1(gen);
                param_list.next.parameter.compile_pass1(gen);
                param_list.next.next.parameter.compile_pass1(gen);
                param_list.next.next.next.parameter.compile_pass1(gen);
                param_list.next.next.next.next.parameter.compile_pass1(gen);
                param_list.next.next.next.next.next.parameter.compile_pass1(gen);
                param_list.next.next.next.next.next.next.parameter.compile_pass1(gen);
            }
            else if (str.ToLower() == "draw_line")
            {
                param_list.parameter.compile_pass1(gen);
                param_list.next.parameter.compile_pass1(gen);
                param_list.next.next.parameter.compile_pass1(gen);
                param_list.next.next.next.parameter.compile_pass1(gen);
                param_list.next.next.next.next.parameter.compile_pass1(gen);
            }
            else if (str.ToLower() == "flood_fill")
            {
                param_list.parameter.compile_pass1(gen);
                param_list.next.parameter.compile_pass1(gen);
                param_list.next.next.parameter.compile_pass1(gen);
            }
            else if (str.ToLower() == "open_graph_window")
            {
                param_list.parameter.compile_pass1(gen);
                param_list.next.parameter.compile_pass1(gen);
            }
            else if (str.ToLower() == "wait_for_mouse_button")
            {
                param_list.parameter.compile_pass1(gen);
            }
            else if (str.ToLower() == "put_pixel")
            {
                param_list.parameter.compile_pass1(gen);
                param_list.next.parameter.compile_pass1(gen);
                param_list.next.next.parameter.compile_pass1(gen);
            }
            else if (str.ToLower() == "set_window_title" || str.ToLower() == "save_graph_window")
            {
                param_list.parameter.compile_pass1(gen);
            }
            else if (str.ToLower() == "get_mouse_button")
            {
                param_list.parameter.compile_pass1(gen);
                param_list.next.parameter.compile_pass1(gen);
                param_list.next.next.parameter.compile_pass1(gen);
            }
        }

    }
    public class Plugin_Proc_Call : Procedure_Call
    {
        public override void Execute(Lexer l)
        {
            //Variable v = new Variable(l.Get_Text(id.start, id.finish), new numbers.value() { V = 22222 });
            MainWindowViewModel mw = MainWindowViewModel.GetMainWindowViewModel();
            numbers.value[] ps = new numbers.value[0];
            if (param_list != null)
            {
                Runtime.processing_parameter_list = true;
                ps = param_list.Execute(l);
            }

            string proc = l.Get_Text(id.start, id.finish);
            if (mw.myTimer != null)
            {
                mw.myTimer.Stop();
            }
            
            Plugins.Invoke(proc, param_list);

            Runtime.processing_parameter_list = false;
            if (mw.myTimer != null)
            {
                mw.myTimer.Start();
            }
            return;
        }


        public override void Emit_Code(Generate_Interface gen)  
        {
            string proc = Component.the_lexer.Get_Text(id.start, id.finish);
            gen.Emit_Plugin_Call(proc, param_list);

        }

        public override void compile_pass1(Generate_Interface gen)
        {
            //throw new NotImplementedException();
        }

        public override bool is_tab_call()
        {
            return false;
        }
    }

    public class Tabid_Proc_Call : Procedure_Call
    {

        public override bool is_tab_call()
        {
            return true;
        }

        public override void Execute(Lexer l)
        {
            MainWindowViewModel mw = MainWindowViewModel.GetMainWindowViewModel();
            string head = l.Get_Text(id.start, id.finish);
            Subchart sub = mw.mainSubchart();
            foreach (Subchart s in mw.theTabs)
            {
                if (s.Header == head)
                {
                    sub = s;
                }
            }
            //if(sub == mw.mainSubchart())
            //{
            //    throw new Exception("Subchart [" + head + "] not found!");
            //}

            if (sub.Start.GetType() == typeof(Oval_Procedure))
            { //if its a user procedure
                Runtime.processing_parameter_list = true;
                numbers.value[] ps = param_list.Execute(l);

                Oval_Procedure st = ((Oval_Procedure)sub.Start);
                string[] paramNames = st.param_names;
                Runtime.Increase_Scope(head);
                mw.decreaseScope++;
                mw.activeScopes.Add(head);
                mw.parentComponent = mw.activeComponent;
                mw.parentCount.Add(mw.activeComponent);
                mw.activeComponent = mw.theTabs[mw.theTabs.IndexOf(sub)].Start;
                mw.activeComponent.running = true;
                mw.activeTab = mw.theTabs.IndexOf(sub);
                mw.activeTabs.Add(mw.theTabs.IndexOf(sub));
                for (int i = 0; i < ps.Length; i++)
                {
                    if (!st.is_input_parameter(i))
                    {
                        break;
                    }
                    numbers.value num = ps[i];
                    if (num.Kind == numbers.Value_Kind.Ref_1D)
                    {

                        Variable oneD = (Variable)num.Object;
                        ObservableCollection<Arr> a = oneD.values;
                        int size = numbers.Numbers.integer_of(oneD.values[0].value);
                        Variable v2 = new Variable(paramNames[i], size, new numbers.value());
                        Variable temp = v2;
                        mw.theVariables.RemoveAt(mw.theVariables.IndexOf(temp));
                        mw.theVariables.Insert(1, temp);
                        for (int j = 1; j < a.Count; j++)
                        {
                            Runtime.setArrayElement(v2.Var_Name, j, a[j].value);
                        }

                    }
                    else if (num.Kind == numbers.Value_Kind.Ref_2D)
                    {

                        Variable twoD = (Variable)num.Object;
                        ObservableCollection<Arr> a = twoD.values;
                        int rows = numbers.Numbers.integer_of(a[0].value);
                        int cols = numbers.Numbers.integer_of(a[1].values[0].value);

                        Variable v2 = new Variable(paramNames[i], rows, cols, new numbers.value());
                        Variable temp = v2;
                        mw.theVariables.RemoveAt(mw.theVariables.IndexOf(temp));
                        mw.theVariables.Insert(1, temp);

                        for (int r = 1; r < rows + 1; r++)
                        {
                            ObservableCollection<Arr2> a2 = a[r].values;
                            for (int c = 1; c < cols + 1; c++)
                            {
                                Runtime.set2DArrayElement(v2.Var_Name, r, c, a2[c].value);
                            }
                        }

                    }
                    else
                    {
                        Variable v2 = new Variable(paramNames[i], num);
                        Variable temp = v2;
                        mw.theVariables.RemoveAt(mw.theVariables.IndexOf(temp));
                        mw.theVariables.Insert(1, temp);
                    }
                }
                Runtime.processing_parameter_list = false;
                mw.setViewTab = mw.theTabs.IndexOf(sub);
                return;


            }
            else if (sub.Start.GetType() == typeof(Oval))
            { // if its a user subchart
                mw.parentComponent = mw.activeComponent;
                mw.parentCount.Add(mw.activeComponent);
                mw.activeComponent = mw.theTabs[mw.theTabs.IndexOf(sub)].Start;
                mw.activeComponent.running = true;
                mw.activeTab = mw.theTabs.IndexOf(sub);
                mw.activeTabs.Add(mw.theTabs.IndexOf(sub));
                mw.setViewTab = mw.theTabs.IndexOf(sub);
                mw.decreaseSub++;

            }
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            //throw new NotImplementedException();
            MainWindowViewModel mw = MainWindowViewModel.GetMainWindowViewModel();
            string head = Component.the_lexer.Get_Text(id.start, id.finish);
            Subchart sub = mw.mainSubchart();
            foreach (Subchart s in mw.theTabs)
            {
                if (s.Header == head)
                {
                    sub = s;
                }
            }
            mw.theTabs[mw.theTabs.IndexOf(sub)].Start.Emit_Code(gen);
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            //throw new NotImplementedException();
            MainWindowViewModel mw = MainWindowViewModel.GetMainWindowViewModel();
            string head = Component.the_lexer.Get_Text(id.start, id.finish);
            Subchart sub = mw.mainSubchart();
            foreach (Subchart s in mw.theTabs)
            {
                if (s.Header == head)
                {
                    sub = s;
                }
            }

            if (sub.Start.GetType() == typeof(Oval_Procedure))
            {
                Oval_Procedure tempStart = (Oval_Procedure)sub.Start;
                for (int k = 0; k < tempStart.param_names.Length; k++)
                {
                    if (tempStart.is_input_parameter(k))
                    {
                        string tempName = tempStart.param_names[k];
                        gen.Declare_As_Variable(tempName);
                    }
                }
            }
            mw.theTabs[mw.theTabs.IndexOf(sub)].Start.compile_pass1(gen);

        }
    }

    public class Method_Proc_Call : Procedure_Call
    {
        Lhs? lhs;
        Msuffix? msuffix;

        public override void Execute(Lexer l)
        {
            Variable v = new Variable(l.Get_Text(id.start, id.finish), new numbers.value() { V = 44444 });
            return;
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            //throw new NotImplementedException();
        }

        public override bool is_tab_call()
        {
            return false;
        }

    }

    public abstract class Assignment : Statement {
        public Lhs? lhs;
        public Lsuffix? lsuffix;

        // execute the lhs of an assignment, takes in a value v --> what the rhs produced
        public abstract numbers.value Execute(Lexer l);

    }
    public class Expr_Assignment : Assignment
    {
        public Expression expr_part;

        // execute expr_part (rhs) return the value of expr_part
        public override numbers.value Execute(Lexer l){
            numbers.value val = expr_part.Execute(l); /* get rhs into a value */
            this.lhs.Execute(l, val);
            return val;
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            Emit_Context = Context_Type.Assign_Context;
            lhs.Emit_Code(gen);
            expr_part.Emit_Code(gen);

            switch (Emit_Kind)
            {
                case Variable_Kind.Value:
                    gen.Variable_Assignment_PastRHS();
                    break;
                case Variable_Kind.Array_1D:
                    gen.Array_1D_Assignment_PastRHS();
                    break;
                case Variable_Kind.Array_2D:
                    gen.Array_2D_Assignment_PastRHS();
                    break;
            }
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            lhs.compile_pass1(gen);
            expr_part.compile_pass1(gen);
        }

    }

    // Statement => (Procedure_Call | Assignment) [;] End_Input
    public abstract class Statement : Parseable { }

    // Lhs => id[\[Expression[, Expression]\]]
    public abstract class Lhs : Parseable {

        public abstract void Execute(Lexer l, numbers.value v);

        public override abstract void Emit_Code(Generate_Interface gen);

        public override abstract void compile_pass1(Generate_Interface gen);

    }
    public class Id_Lhs : Lhs
    {
        public Token id;
        public Id_Lhs(Token id)
        {
            this.id = id;
        }

        public override void Execute(Lexer l, numbers.value v){
           string varname = l.Get_Text(id.start, id.finish);
           Runtime.setVariable(varname, v);
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            string t = Component.the_lexer.Get_Text(id.start, id.finish);

            switch (Emit_Context)
            {
                case Context_Type.Assign_Context:
                    gen.Variable_Assignment_Start(t);
                    Emit_Kind = Variable_Kind.Value;
                    break;
                case Context_Type.Input_Context:
                    gen.Input_Start_Variable(t);
                    break;
                case Context_Type.Call_Context:
                    Object o = numbers.Numbers.make_object_value(t);
                    gen.Emit_No_Parameters(o);
                    break;
            }
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            if(!Compile_Helpers.Start_New_Declaration(Component.the_lexer.Get_Text(id.start, id.finish)))
            {
                gen.Declare_As_Variable(Component.the_lexer.Get_Text(id.start, id.finish));
            }
        }

    }
    public class Array_Ref_Lhs : Id_Lhs
    {
        public Expression reference;
        public Array_Ref_Lhs(Token id, Expression reference) : base(id)
        {
            this.reference = reference;
        }

        public override void Execute(Lexer l, numbers.value v){
            numbers.value ref_val = reference.Execute(l);
            string varname = l.Get_Text(id.start, id.finish);
            if (!numbers.Numbers.is_integer(ref_val))
            {
                throw new raptor.RuntimeException(numbers.Numbers.msstring_view_image(ref_val) +
                    " not a valid array location--must be integer");
            }
            int i = numbers.Numbers.integer_of(ref_val);
            Runtime.setArrayElement(varname, i, v);
        }

        private void Emit_Method(Generate_Interface gen)
        {
            string s = Component.the_lexer.Get_Text(id.start, id.finish);
            gen.Emit_Load_Array_Start(s);
            reference.Emit_Code(gen);
            gen.Emit_Load_Array_After_Index(s);

        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            string t = Component.the_lexer.Get_Text(id.start, id.finish);
            //gen.Emit_Load_Array_Start(s);
            //reference.Emit_Code(gen);
            //gen.Emit_Load_Array_After_Index(s);

            switch (Emit_Context)
            {
                case Context_Type.Assign_Context:
                    gen.Array_1D_Assignment_Start(t);
                    reference.Emit_Code(gen);
                    gen.Array_1D_Assignment_After_Index();
                    Emit_Kind = Variable_Kind.Array_1D;
                    break;
                case Context_Type.Input_Context:
                    gen.Input_Start_Array_1D(t, reference);
                    break;
                case Context_Type.Call_Context:
                    throw new Exception("Bad Call!");
                    break;
            }

        }

        public override void compile_pass1(Generate_Interface gen)
        {
            reference.compile_pass1(gen);
            if (!Compile_Helpers.Start_New_Declaration(Component.the_lexer.Get_Text(id.start, id.finish)))
            {
                gen.Declare_As_1D_Array(Component.the_lexer.Get_Text(id.start, id.finish));
            }
        }

    }
    public class Array_2D_Ref_Lhs : Array_Ref_Lhs
    {
        public Expression reference2;
        public Array_2D_Ref_Lhs(Token id, Expression reference, Expression ref2) : base(id,reference)
        {
            this.reference2 = ref2;
        }

        public override void Execute(Lexer l, numbers.value v){
            numbers.value ref_val1 = reference.Execute(l);
            if (!numbers.Numbers.is_integer(ref_val1))
            {
                throw new raptor.RuntimeException(numbers.Numbers.msstring_view_image(ref_val1) +
                    " not a valid array location--must be integer");
            }
            numbers.value ref_val2 = reference2.Execute(l);
            if (!numbers.Numbers.is_integer(ref_val2))
            {
                throw new raptor.RuntimeException(numbers.Numbers.msstring_view_image(ref_val2) +
                    " not a valid array location--must be integer");
            }
            string varname = l.Get_Text(id.start, id.finish);
            int i1 = numbers.Numbers.integer_of(ref_val1);
            int i2 = numbers.Numbers.integer_of(ref_val2);
            Runtime.set2DArrayElement(varname, i1, i2, v);
        }

        private void Emit_Method(Generate_Interface gen)
        {
            string s = Component.the_lexer.Get_Text(id.start, id.finish);
            gen.Emit_Load_Array_2D_Start(s);
            reference.Emit_Code(gen);
            gen.Emit_Load_Array_2D_Between_Indices();
            reference2.Emit_Code(gen);
            gen.Emit_Load_Array_2D_After_Indices(s);

        }
        public override void Emit_Code(Generate_Interface gen)  
        {
            string t = Component.the_lexer.Get_Text(id.start, id.finish);
            switch (Emit_Context)
            {
                case Context_Type.Assign_Context:
                    gen.Array_2D_Assignment_Start(t);
                    reference.Emit_Code(gen);
                    gen.Array_2D_Assignment_Between_Indices();
                    reference2.Emit_Code(gen);
                    gen.Array_2D_Assignment_After_Indices();
                    Emit_Kind = Variable_Kind.Array_2D;
                    break;
                case Context_Type.Input_Context:
                    gen.Input_Start_Array_2D(t, reference, reference2);
                    break;
                case Context_Type.Call_Context:
                    throw new Exception("Bad Call!");
                    break;
            }
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            reference.compile_pass1(gen);
            reference2.compile_pass1(gen);
            if (!Compile_Helpers.Start_New_Declaration(Component.the_lexer.Get_Text(id.start, id.finish)))
            {
                gen.Declare_As_2D_Array(Component.the_lexer.Get_Text(id.start, id.finish));
            }
        }

    }

    // Msuffix => . Lhs Msuffix | .id | .id(Parameter_list)
    public class Msuffix { }

    public class Full_Msuffix : Msuffix
    {
        public Lhs lhs;
        public Msuffix msuffix;
    }
    public class Noparam_Msuffix : Msuffix
    {
        public Token id;
    }

    public class Lsuffix { }
    public class Full_Lsuffix : Lsuffix
    {
        Lhs lhs;
        Lsuffix lsuffix;
        public Full_Lsuffix(Lhs lhs, Lsuffix lsuffix)
        {
            this.lhs = lhs;
            this.lsuffix = lsuffix;
        }
    }
    public class Empty_Lsuffix : Lsuffix { }

    public abstract class Rhs { 
        public abstract numbers.value Execute(Lexer l);

        public abstract void Emit_Code(Generate_Interface gen);

        public abstract void compile_pass1(Generate_Interface gen);
    }
    public class Id_Rhs : Rhs
    {
        public Token id;
        public Id_Rhs() { }
        public Id_Rhs(Token ident)
        {
            this.id = ident;
        }

        public override numbers.value Execute(Lexer l){
            string varname = l.Get_Text(id.start, id.finish);
            return Runtime.getVariable(varname);
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            gen.Emit_Load(Component.the_lexer.Get_Text(id.start, id.finish));

        }


        public override void compile_pass1(Generate_Interface gen)
        {
            //string asdfasdfnas = Component.the_lexer.Get_Text();
            if (!Compile_Helpers.Start_New_Declaration(Component.the_lexer.Get_Text(id.start, id.finish)))
            {
                gen.Declare_As_Variable(Component.the_lexer.Get_Text(id.start, id.finish));
            }
        }
    }
    public class Array_Ref_Rhs : Id_Rhs
    {
        public Expression reference;

        public override numbers.value Execute(Lexer l){
            numbers.value ref_val = reference.Execute(l);
            string varname = l.Get_Text(id.start, id.finish);
            if (!numbers.Numbers.is_integer(ref_val))
            {
                throw new raptor.RuntimeException(numbers.Numbers.msstring_view_image(ref_val) +
                    " not a valid array location--must be integer");
            }
            int i = numbers.Numbers.integer_of(ref_val);
            return Runtime.getArrayElement(varname, i);
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            string s = Component.the_lexer.Get_Text(id.start, id.finish);
            gen.Emit_Load_Array_Start(s);
            reference.Emit_Code(gen);
            gen.Emit_Load_Array_After_Index(s);
        }


        public override void compile_pass1(Generate_Interface gen)
        {
            reference.compile_pass1(gen);
            if (!Compile_Helpers.Start_New_Declaration(Component.the_lexer.Get_Text(id.start, id.finish)))
            {
                gen.Declare_As_1D_Array(Component.the_lexer.Get_Text(id.start, id.finish));
            }
        }
    }
    public class Array_Ref_2D_Rhs : Array_Ref_Rhs
    {
        public Expression reference2;

        public override numbers.value Execute(Lexer l){
            numbers.value ref_val = reference.Execute(l);
            numbers.value ref_val2 = reference2.Execute(l);
            string varname = l.Get_Text(id.start, id.finish);
            if (!numbers.Numbers.is_integer(ref_val))
            {
                throw new raptor.RuntimeException(numbers.Numbers.msstring_view_image(ref_val) +
                    " not a valid array location--must be integer");
            }
            if (!numbers.Numbers.is_integer(ref_val2))
            {
                throw new raptor.RuntimeException(numbers.Numbers.msstring_view_image(ref_val2) +
                    " not a valid array location--must be integer");
            }
            int i = numbers.Numbers.integer_of(ref_val);
            int i2 = numbers.Numbers.integer_of(ref_val2);
            return Runtime.get2DArrayElement(varname, i, i2);
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            string s = Component.the_lexer.Get_Text(id.start, id.finish);
            gen.Emit_Load_Array_2D_Start(s);
            reference.Emit_Code(gen);
            gen.Emit_Load_Array_2D_Between_Indices();
            reference2.Emit_Code(gen);
            gen.Emit_Load_Array_2D_After_Indices(s);
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            reference.compile_pass1(gen);
            reference2.compile_pass1(gen);
            if (!Compile_Helpers.Start_New_Declaration(Component.the_lexer.Get_Text(id.start, id.finish)))
            {
                gen.Declare_As_2D_Array(Component.the_lexer.Get_Text(id.start, id.finish));
            }
        }

    }

    public class Rhs_Method_Call : Id_Rhs
    {
        public Parameter_List? parameters;
    }

    public abstract class Rsuffix { }
    public class Full_Rsuffix : Rsuffix
    {
        public Rhs rhs;
        public Rsuffix rsuffix;
        public Full_Rsuffix(Rhs rhs, Rsuffix rsuffix)
        {
            this.rhs = rhs;
            this.rsuffix = rsuffix;
        }
    }
    public class Empty_Rsuffix : Rsuffix { }

    public abstract class Expon : Value_Parseable {

        public override abstract numbers.value Execute(Lexer l);

    }

    public class Expon_Stub : Expon
    {
        public Component component;
        public int index;
        public Expon expon_parse_tree;

        public override numbers.value Execute(Lexer l){
            return new numbers.value(){V=666};
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            expon_parse_tree.Emit_Code(gen);
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            expon_parse_tree.compile_pass1(gen);
        }
    }

    public class Rhs_Expon : Expon
    {
        public Rhs rhs;
        public Rsuffix rsuffix;
        public Rhs_Expon(Rhs rhs, Rsuffix rsuffix)
        {
            this.rhs = rhs;
            this.rsuffix = rsuffix;
        }
        public bool is_method_call()
        {
            return false;
        }

        public override numbers.value Execute(Lexer l){
            return rhs.Execute(l);
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            rhs.Emit_Code(gen);
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            this.rhs.compile_pass1(gen);
            //throw new NotImplementedException();
        }

    }
    public class Number_Expon : Expon
    {
        public Token number;
        public Number_Expon(Token t)
        {
            this.number = t;
        }

        public override numbers.value Execute(Lexer l){
            string s = l.Get_Text(number.start, number.finish);
            return numbers.Numbers.make_value__5(s);
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            string s = Component.the_lexer.Get_Text(number.start, number.finish);
            double val = (double)(numbers.Numbers.make_value__5(s).V);
            gen.Emit_Load_Number(val);
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            //string num = Component.the_lexer.Get_Text(number.start, number.finish);
            //double val = (double)(numbers.Numbers.make_value__5(num).V);
            //throw new NotImplementedException();
        }

    }
    public class Negative_Expon : Expon
    {
        public Expon e;
        public Negative_Expon(Expon e)
        {
            this.e = e;
        }
        
        public override numbers.value Execute(Lexer l){
            Number_Expon ne = (Number_Expon)e;
            numbers.value temp = ne.Execute(l);
            temp.V = temp.V * -1;
            return temp;
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            if (gen.Is_Postfix())
            {
                e.Emit_Code(gen);
                gen.Emit_Unary_Minus();
            }
            else
            {
                gen.Emit_Unary_Minus();
                e.Emit_Code(gen);
            }
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            e.compile_pass1(gen);
            //throw new NotImplementedException();
        }

    }
    public class String_Expon : Expon
    {
        public Token s;
        public String_Expon(Token s)
        {
            this.s = s;
        }

        public override numbers.value Execute(Lexer l){
            return new numbers.value(){Kind=numbers.Value_Kind.String_Kind, S=l.Get_Text(s.start+1, s.finish-1)};
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            gen.Emit_Load_String(Component.the_lexer.Get_Text(s.start+1, s.finish-1).Replace("\"", "\\\""));
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            //throw new NotImplementedException();
        }

    }
    public class Paren_Expon : Expon
    {
        public Expression expr_part;
        public Paren_Expon(Expression e)
        {
            this.expr_part = e;
        }

        public override numbers.value Execute(Lexer l){
            return expr_part.Execute(l);
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            gen.Emit_Left_Paren();
            expr_part.Emit_Code(gen);
            gen.Emit_Right_Paren();

        }

        public override void compile_pass1(Generate_Interface gen)
        {
            expr_part.compile_pass1(gen);
        }
    }
    public class Id_Expon : Expon
    {
        public Token id;
        public Id_Expon() { }
        public Id_Expon(Token id)
        {
            this.id = id;
        }

        public override numbers.value Execute(Lexer l){
            //return new numbers.value() { V = 666 };
            throw new NotImplementedException();
        }


        public override void Emit_Code(Generate_Interface gen)  
        {
            throw new NotImplementedException();
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            //throw new NotImplementedException();
        }

    }
    public class Func0_Expon : Id_Expon
    {
        public Func0_Expon(Token id) : base(id) { }

        public override numbers.value Execute(Lexer l){
           string s = l.Get_Text(id.start, id.finish);
           switch(s.ToLower()){
                case "true":
                    return new numbers.value(){V=1};
                case "false":
                    return new numbers.value(){V=0};
                case "pi":
                    return new numbers.value(){V=3.14159};
                case "e":
                    return new numbers.value(){V=2.71828};
                case "random":
                    Random r = new Random();
                    return new numbers.value(){V=r.NextDouble()};
                case "black":
                    return new numbers.value(){V=0};
                case "blue":
                    return new numbers.value(){V=1};
                case "green":
                    return new numbers.value(){V=2};
                case "cyan":
                    return new numbers.value(){V=3};
                case "red":
                    return new numbers.value(){V=4};
                case "magenta":
                    return new numbers.value(){V=5};
                case "brown":
                    return new numbers.value(){V=6};
                case "light_gray":
                    return new numbers.value(){V=7};
                case "dark_gray":
                    return new numbers.value(){V=8};
                case "light_blue":
                    return new numbers.value(){V=9};
                case "light_green":
                    return new numbers.value(){V=10};
                case "light_cyan":
                    return new numbers.value(){V=11};
                case "light_red":
                    return new numbers.value(){V=12};
                case "light_magenta":
                    return new numbers.value(){V=13};
                case "yellow":
                    return new numbers.value(){V=14};
                case "pink":
                    return new numbers.value(){V=15};
                case "purple":
                    return new numbers.value(){V=16};
                case "white":
                    return new numbers.value(){V=17};
                case "unfilled":
                    return new numbers.value(){V=0};
                case "filled":
                    return new numbers.value(){V=1};
                case "yes":
                    return new numbers.value(){V=1};
                case "no":
                    return new numbers.value(){V=0};
                case "left_button":
                    return new numbers.value() { V = 86 };
                case "right_button":
                    return new numbers.value() { V = 87 };
                case "get_mouse_x":
                    Proc_Call.checkOpenGraph();
                    return GraphDialogViewModel.GetMouseX();
                case "get_mouse_y":
                    Proc_Call.checkOpenGraph();
                    return GraphDialogViewModel.GetMouseX();
                case "get_key":
                    Proc_Call.checkOpenGraph();
                    return GraphDialogViewModel.GetKey();
                case "get_key_string":
                    Proc_Call.checkOpenGraph();
                    return GraphDialogViewModel.GetKeyString();
                case "get_max_height":
                    return GraphDialogViewModel.GetMaxHeight();
                case "get_max_width":
                    return GraphDialogViewModel.GetMaxWidth();
                case "get_window_height":
                    Proc_Call.checkOpenGraph();
                    double h = 0;
                    Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        h = GraphDialogViewModel.GetWindowHeight();
                    }).Wait(-1);
                    return new numbers.value() { Kind = numbers.Value_Kind.Number_Kind, V = h };
                case "get_window_width":
                    Proc_Call.checkOpenGraph();
                    double w = 0;
                    Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        w = GraphDialogViewModel.GetWindowWidth();
                    }).Wait(-1);
                    return new numbers.value() { Kind = numbers.Value_Kind.Number_Kind, V = w };
                case "get_font_height":
                    Proc_Call.checkOpenGraph();
                    return GraphDialogViewModel.GetFontHeight();
                case "get_font_width":
                    Proc_Call.checkOpenGraph();
                    return GraphDialogViewModel.GetFontWidth();
            }
            //return new numbers.value() { V = 9999 };
            throw new NotImplementedException();
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            if(id.kind == Token_Type.Random)
            {
                gen.Emit_Random();
                return;
            }
            else if(id.kind == Token_Type.Random_Color)
            {
                gen.Emit_Random(0.0, 16.0);
                return;
            }
            else if(id.kind == Token_Type.Random_Extended_Color)
            {
                gen.Emit_Random(0.0, 242.0);
                return;
            }
            else
            {
                int t = (int)id.kind;
                object o = gen.Emit_Call_Method(t);
                gen.Emit_No_Parameters(o); 
            }

        }

        public override void compile_pass1(Generate_Interface gen)
        {
            //throw new NotImplementedException();
        }

    }
    public class Character_Expon : Expon
    {
        public Token s;
        public Character_Expon(Token s)
        {
            this.s = s;
        }

        public override numbers.value Execute(Lexer l){
            char ans = l.Get_Text(s.start, s.finish)[1];
            return new numbers.value(){C=ans, Kind=numbers.Value_Kind.Character_Kind};
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            gen.Emit_Load_Character(Component.the_lexer.Get_Text(s.start, s.finish)[1]);
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            //throw new NotImplementedException();
        }
    }

    public class Func_Expon : Id_Expon {
        public Parameter_List parameters;

        public override numbers.value Execute(Lexer l){
            string s = l.Get_Text(id.start, id.finish);
            Runtime.processing_parameter_list = true;
            numbers.value[] ps = parameters.Execute(l);
            Runtime.processing_parameter_list = false;
            switch (s.ToLower()) {
                case "sin":
                    if (ps.Length == 1)
                    { return numbers.Numbers.Sin(ps[0], numbers.Numbers.Two_Pi); }
                    else
                    { return numbers.Numbers.Sin(ps[0], ps[1]); }
                case "cos":
                    if (ps.Length == 1)
                    { return numbers.Numbers.Cos(ps[0], numbers.Numbers.Two_Pi); }
                    else
                    { return numbers.Numbers.Cos(ps[0], ps[1]); }
                case "tan":
                    if (ps.Length == 1)
                    { return numbers.Numbers.Tan(ps[0], numbers.Numbers.Two_Pi); }
                    else
                    { return numbers.Numbers.Tan(ps[0], ps[1]); }
                case "cot":
                    if (ps.Length == 1)
                    { return numbers.Numbers.Cot(ps[0], numbers.Numbers.Two_Pi); }
                    else
                    { return numbers.Numbers.Cot(ps[0], ps[1]); }
                case "arcsin":
                    if (ps.Length == 1)
                    { return numbers.Numbers.ArcSin(ps[0], numbers.Numbers.Two_Pi); }
                    else
                    { return numbers.Numbers.ArcSin(ps[0], ps[1]); }
                case "arccos":
                    if (ps.Length == 1)
                    { return numbers.Numbers.ArcCos(ps[0], numbers.Numbers.Two_Pi); }
                    else
                    { return numbers.Numbers.ArcCos(ps[0], ps[1]); }
                case "log":
                    return new numbers.value() { V = Math.Log(ps[0].V) };
                case "arctan":
                    if (ps.Length == 1)
                    { return numbers.Numbers.ArcTan(ps[0], numbers.Numbers.Two_Pi); }
                    else
                    { return numbers.Numbers.ArcTan(ps[0], ps[1]); }
                case "arccot":
                    if (ps.Length == 1)
                    { return numbers.Numbers.ArcCot(ps[0], numbers.Numbers.Two_Pi); }
                    else
                    { return numbers.Numbers.ArcCot(ps[0], ps[1]); }
                case "min":
                    return numbers.Numbers.findMin(ps[0], ps[1]);
                case "max":
                    return numbers.Numbers.findMax(ps[0], ps[1]);
                case "sinh":
                    return new numbers.value() { V = Math.Sinh(ps[0].V) };
                case "tanh":
                    return new numbers.value() { V = Math.Tanh(ps[0].V) };
                case "cosh":
                    return new numbers.value() { V = Math.Cosh(ps[0].V) };
                case "arccosh":
                    return new numbers.value() { V = Math.Acosh(ps[0].V) };
                case "arcsinh":
                    return new numbers.value() { V = Math.Asinh(ps[0].V) };
                case "arctanh":
                    return new numbers.value() { V = Math.Atanh(ps[0].V) };
                case "coth":
                    return new numbers.value() { V = 1 / Math.Tanh(ps[0].V) }; ;
                case "arccoth":
                    return new numbers.value() { V = 1 / Math.Atanh(ps[0].V) };
                case "sqrt":
                    return new numbers.value() { V = Math.Sqrt(ps[0].V) };
                case "floor":
                    return new numbers.value() { V = Math.Floor(ps[0].V) };
                case "ceiling":
                    return new numbers.value() { V = Math.Ceiling(ps[0].V) };
                case "to_ascii":
                    return new numbers.value() { V = ps[0].C };
                case "to_character":
                    return new numbers.value() { Kind = numbers.Value_Kind.Character_Kind, C = (char)ps[0].V };
                case "length_of":
                    if (ps[0].Kind == numbers.Value_Kind.String_Kind)
                    {
                        return new numbers.value() { Kind = numbers.Value_Kind.Number_Kind, V = ps[0].S.Length };
                    }
                    return ((Variable)ps[0].Object).values[0].value;
                case "abs":
                    return new numbers.value() { V = Math.Abs(ps[0].V) };
                case "load_bitmap":
                    Proc_Call.checkOpenGraph();
                    string f = ps[0].S;
                    return GraphDialogViewModel.LoadBitmap(f);
                case "get_pixel":
                    Proc_Call.checkOpenGraph();
                    int x = numbers.Numbers.integer_of(ps[0]);
                    int y = numbers.Numbers.integer_of(ps[1]);
                    return GraphDialogViewModel.GetPixel(x, y);
                case "closest_color":
                    int r = numbers.Numbers.integer_of(ps[0]);
                    int g = numbers.Numbers.integer_of(ps[1]);
                    int b = numbers.Numbers.integer_of(ps[2]);
                    return GraphDialogViewModel.GetClosestColor(r, g, b);

            }
            //return new numbers.value() { V = 333 };
            throw new NotImplementedException();
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            int functionName = (int)id.kind;
            string s = Component.the_lexer.Get_Text(id.start, id.finish).ToLower();
            Object o = new Object();
            if (s != "length_of" && s != "to_ascii" && s != "to_character")
            {
                o = gen.Emit_Call_Method(functionName);
            }


            switch (s)
            {
                case "to_ascii":
                    gen.Emit_Conversion((int)Conversions.Char_To_Int);
                    emit_parameter_number(parameters.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.Char_To_Int);
                    break;
                case "to_character":
                    gen.Emit_Conversion((int)Conversions.Int_To_Char);
                    emit_parameter_number(parameters.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.Int_To_Char);
                    break;
                case "length_of":
                    ((Expr_Output)parameters.parameter).Emit_Length_Of(gen);
                    break;
                case "get_pixel":
                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(parameters.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);
                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(parameters.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Last_Parameter(o);
                    break;
                case "load_bitmap":
                    gen.Emit_Conversion((int)Conversions.To_String);
                    emit_parameter_number(parameters.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_String);
                    gen.Emit_Last_Parameter(o);
                    break;
                case "closest_color":
                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(parameters.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);
                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(parameters.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Next_Parameter(o);
                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    emit_parameter_number(parameters.next.next.parameter, gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    gen.Emit_Last_Parameter(o);
                    break;
                case "sinh":
                case "cosh":
                case "tanh":
                case "arccosh":
                case "arcsinh":
                case "arctanh":
                case "coth":
                case "arccoth":
                case "sqrt":
                case "floor":
                case "ceiling:":
                case "abs":
                    emit_parameter_number(parameters.parameter, gen);
                    gen.Emit_Last_Parameter(o);
                    break;
                case "min":
                case "max":
                    emit_parameter_number(parameters.parameter, gen);
                    gen.Emit_Next_Parameter(o);
                    emit_parameter_number(parameters.next.parameter, gen);
                    gen.Emit_Last_Parameter(o);
                    break;
                case "log":
                case "sin":
                case "cos":
                case "tan":
                case "cot":
                case "arcsin":
                case "arccos":
                    if(parameters.getLen() == 1)
                    {
                        emit_parameter_number(parameters.parameter, gen);
                    }
                    else if (parameters.getLen() == 2)
                    {
                        emit_parameter_number(parameters.parameter, gen);
                        gen.Emit_Next_Parameter(o);
                        emit_parameter_number(parameters.next.parameter, gen);
                    }
                    gen.Emit_Last_Parameter(o);
                    break;
                case "arctan":
                case "arccot":
                    if (parameters.getLen() == 1)
                    {
                        emit_parameter_number(parameters.parameter, gen);
                    }
                    else if (parameters.getLen() == 2)
                    {
                        emit_parameter_number(parameters.parameter, gen);
                        gen.Emit_Next_Parameter(o);
                        emit_parameter_number(parameters.next.parameter, gen);
                    }
                    else if (parameters.getLen() == 3)
                    {
                        emit_parameter_number(parameters.parameter, gen);
                        gen.Emit_Next_Parameter(o);
                        emit_parameter_number(parameters.next.parameter, gen);
                        gen.Emit_Next_Parameter(o);
                        emit_parameter_number(parameters.next.next.parameter, gen);
                    }
                    gen.Emit_Last_Parameter(o);
                    break;
            }

        }

        public override void compile_pass1(Generate_Interface gen)
        {
            string s = Component.the_lexer.Get_Text(id.start, id.finish);
            switch(s.ToLower()){
                case "to_ascii":
                case "to_character":
                case "load_bitmap":
                    parameters.parameter.compile_pass1(gen);
                    break;
                case "get_pixel":
                    parameters.parameter.compile_pass1(gen);
                    parameters.next.parameter.compile_pass1(gen);
                    break;
                case "closest_color":
                    parameters.parameter.compile_pass1(gen);
                    parameters.next.parameter.compile_pass1(gen);
                    parameters.next.next.parameter.compile_pass1(gen);
                    break;
                case "sinh":
                case "tanh":
                case "cosh":
                case "arccosh":
                case "arcsinh":
                case "arctanh":
                case "coth":
                case "arccoth":
                case "sqrt":
                case "floor":
                case "ceiling":
                case "abs":
                    parameters.parameter.compile_pass1(gen);
                    break;
                case "min":
                case "max":
                    parameters.parameter.compile_pass1(gen);
                    parameters.next.parameter.compile_pass1(gen);
                    break;
                case "sin":
                case "cos":
                case "tan":
                case "cot":
                case "arcsin":
                case "arccos":
                case "log":
                    if(parameters.getLen() == 1)
                    {
                        parameters.parameter.compile_pass1(gen);
                    }
                    else if (parameters.getLen() == 2)
                    {
                        parameters.parameter.compile_pass1(gen);
                        parameters.next.parameter.compile_pass1(gen);
                    }
                    break;
                case "arccot":
                case "arctan":
                    if (parameters.getLen() == 1)
                    {
                        parameters.parameter.compile_pass1(gen);
                    }
                    else if (parameters.getLen() == 2)
                    {
                        parameters.parameter.compile_pass1(gen);
                        parameters.next.parameter.compile_pass1(gen);
                    }
                    else if (parameters.getLen() == 3)
                    {
                        parameters.parameter.compile_pass1(gen);
                        parameters.next.parameter.compile_pass1(gen);
                        parameters.next.next.parameter.compile_pass1(gen);
                    }
                    break;

            }
        }

    }
    public class Plugin_Func_Expon : Id_Expon
    {
        public Parameter_List? parameters;

        public override numbers.value Execute(Lexer l){
            if(parameters == null)
            {
                return Plugins.Invoke_Function(l.Get_Text().Substring(l.Get_Text().IndexOf(":=") + 2).Trim(), parameters);
            }
            else
            {
                string text = l.Get_Text();
                string t = text.Substring(text.IndexOf(":=")+2).Trim();
                return Plugins.Invoke_Function(text, parameters);
            }
            
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            string text = Component.the_lexer.Get_Text(id.start, id.finish);
            gen.Emit_Plugin_Call(text, parameters);
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            //throw new NotImplementedException();
        }
    }

    public class Mult : Value_Parseable
    {
        public Value_Parseable left;
        public Mult(Value_Parseable left)
        {
            this.left = left;
        }
        public static void Fix_Associativity(ref Mult e)
        {
            if (e is Expon_Mult)
            {
                if (((Expon_Mult)e).right is Expon_Mult)
                {
                    Expon_Mult temp = ((Expon_Mult)e).right as Expon_Mult;
                    ((Expon_Mult)e).right = new Mult(temp.left);
                    temp.left = e;
                    e = temp;
                    Fix_Associativity(ref e);
                }

            }
        }

        public override numbers.value Execute(Lexer l){
            return left.Execute(l);
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            left.Emit_Code(gen);
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            left.compile_pass1(gen);
        }

    }
    public class Expon_Mult : Mult
    {
        public Mult right;
        public Expon_Mult(Value_Parseable left, Mult right) : base(left)
        {
            this.right = right;
        }

        public override numbers.value Execute(Lexer l){
            numbers.value first = left.Execute(l);
            numbers.value second = right.Execute(l);
            return numbers.Numbers.exponValues(first, second);

        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            if (gen.Is_Postfix())
            {
                left.Emit_Code(gen);
                right.Emit_Code(gen);
                gen.Emit_Exponentiation();
            }
            else
            {
                left.Emit_Code(gen);
                gen.Emit_Exponentiation();
                right.Emit_Code(gen);
            }
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            left.compile_pass1(gen);
            right.compile_pass1(gen);
        }
    }
    public class Add : Value_Parseable
    {
        public Value_Parseable left;
        public Add(Value_Parseable left)
        {
            this.left = left;
        }
        public static void Fix_Associativity(ref Add e)
        {
            if (e is Binary_Add)
            {
                if (((Binary_Add)e).right is Binary_Add)
                {
                    Binary_Add temp = ((Binary_Add)e).right as Binary_Add;
                    ((Binary_Add)e).right = new Add(temp.left);
                    temp.left = e;
                    e = temp;
                    Fix_Associativity(ref e);
                }

            }
        }

        public override numbers.value Execute(Lexer l){
            return left.Execute(l);
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            left.Emit_Code(gen);
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            left.compile_pass1(gen);
        }

    }
    public class Binary_Add : Add
    {
        public Add right;
        public Binary_Add(Value_Parseable left, Add right) : base(left)
        {
            this.right = right;
        }

        public override numbers.value Execute(Lexer l){
            numbers.value first = left.Execute(l);
            numbers.value second = right.Execute(l);
            return numbers.Numbers.addValues(first, second);
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            throw new NotImplementedException();
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            left.compile_pass1(gen);
            right.compile_pass1(gen);
        }
    }
    public class Div_Add : Binary_Add
    {
        public Div_Add(Value_Parseable left, Add right) : base(left,right)
        {
        }

        public override numbers.value Execute(Lexer l){
            numbers.value first = left.Execute(l);
            numbers.value second = right.Execute(l);
            return numbers.Numbers.divValues(first, second);
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            if (gen.Is_Postfix())
            {
                left.Emit_Code(gen);
                right.Emit_Code(gen);
                gen.Emit_Divide();
            }
            else
            {
                left.Emit_Code(gen);
                gen.Emit_Divide();
                right.Emit_Code(gen);
            }
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            left.compile_pass1(gen);
            right.compile_pass1(gen);
        }

    }
    public class Mult_Add : Binary_Add
    {
        public Mult_Add(Value_Parseable left, Add right) : base(left,right)
        {
        }
        public override numbers.value Execute(Lexer l){
            numbers.value first = left.Execute(l);
            numbers.value second = right.Execute(l);
            return numbers.Numbers.multValues(first, second);
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            if (gen.Is_Postfix())
            {
                left.Emit_Code(gen);
                right.Emit_Code(gen);
                gen.Emit_Times();
            }
            else
            {
                left.Emit_Code(gen);
                gen.Emit_Times();
                right.Emit_Code(gen);
            }
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            left.compile_pass1(gen);
            right.compile_pass1(gen);
        }
    }
    public class Mod_Add : Binary_Add
    {
        public Mod_Add(Value_Parseable left, Add right) : base(left,right)
        {
        }

        public override numbers.value Execute(Lexer l){
            numbers.value first = left.Execute(l);
            numbers.value second = right.Execute(l);
            return numbers.Numbers.modValues(first, second);
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            if (gen.Is_Postfix())
            {
                left.Emit_Code(gen);
                right.Emit_Code(gen);
                gen.Emit_Mod();
            }
            else
            {
                left.Emit_Code(gen);
                gen.Emit_Mod();
                right.Emit_Code(gen);
            }
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            left.compile_pass1(gen);
            right.compile_pass1(gen);
        }
    }
    public class Rem_Add : Binary_Add
    {
        public Rem_Add(Value_Parseable left, Add right) : base(left,right)
        {
        }

        public override numbers.value Execute(Lexer l){
            numbers.value first = left.Execute(l);
            numbers.value second = right.Execute(l);
            return numbers.Numbers.remValues(first, second);
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            if (gen.Is_Postfix())
            {
                left.Emit_Code(gen);
                right.Emit_Code(gen);
                gen.Emit_Rem();
            }
            else
            {
                left.Emit_Code(gen);
                gen.Emit_Rem();
                right.Emit_Code(gen);
            }
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            left.compile_pass1(gen);
            right.compile_pass1(gen);
        }
    }
    public abstract class Boolean_Parseable : Parseable
    {
        public abstract bool Execute(Lexer l);
    }
    //  Relation => Expression > Expression | >=,<,<=,=,/=
    public class Relation : Boolean_Parseable
    {
        public Expression? left, right;
        public Token_Type? kind; // must be a relation

        public override bool Execute(Lexer l)
        {
            //Variable v = new Variable(left.Execute(l).V + "", new numbers.value(){S=right.Execute(l).V + "", Kind=numbers.Value_Kind.String_Kind});
            switch(kind){
                case Token_Type.Equal:
                    return left.Execute(l) == right.Execute(l);
                case Token_Type.Not_Equal:
                    return left.Execute(l) != right.Execute(l);
                case Token_Type.Greater:
                    return left.Execute(l) > right.Execute(l);
                case Token_Type.Greater_Equal:
                    return left.Execute(l) >= right.Execute(l);
                case Token_Type.Less:
                    return left.Execute(l) < right.Execute(l);
                case Token_Type.Less_Equal:
                    return left.Execute(l) <= right.Execute(l);
            }
            return false;
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            left.Emit_Code(gen);
            if (gen.Is_Postfix())
            {
                right.Emit_Code(gen);
            }
            switch (kind)
            {
                case Token_Type.Equal:
                    gen.Emit_Relation(5);
                    break;
                case Token_Type.Not_Equal:
                    gen.Emit_Relation(6);
                    break;
                case Token_Type.Greater:
                    gen.Emit_Relation(1);
                    break;
                case Token_Type.Greater_Equal:
                    gen.Emit_Relation(2);
                    break;
                case Token_Type.Less:
                    gen.Emit_Relation(3);
                    break;
                case Token_Type.Less_Equal:
                    gen.Emit_Relation(4);
                    break;
            }
            if (!gen.Is_Postfix())
            {
                right.Emit_Code(gen);
            }
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            left.compile_pass1(gen);
            right.compile_pass1(gen);
        }

    }
    public class Boolean0 : Boolean_Parseable
    {
        public Token_Type kind; // must be a Boolean_Func0_Type
        public Boolean0(Token_Type kind)
        {
            this.kind = kind;
        }

        public override bool Execute(Lexer l){
            switch (kind)
            {
                case Token_Type.Key_Hit :
                    Proc_Call.checkOpenGraph();
                    return GraphDialogViewModel.KeyHit();
                case Token_Type.Is_Open :
                    return GraphDialogViewModel.IsOpen();
            }

            return false;
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            int t = (int)kind;
            Object o = gen.Emit_Call_Method(t);
            gen.Emit_No_Parameters(o);
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            //throw new NotImplementedException();
        }

    }
    public class Boolean_Constant : Boolean_Parseable
    {
        public bool value;
        public Boolean_Constant(bool val)
        {
            this.value = val;
        }

        public override bool Execute(Lexer l){
            return value;
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            gen.Emit_Load_Boolean(value);
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            //throw new NotImplementedException();
        }
    }
    public class Boolean1 : Boolean_Parseable
    {
        public Token_Type kind; // must be Boolean_Func1_Type
        public Expression parameter;
        public Boolean1 (Token_Type k, Expression e)
        {
            this.kind = k;
            this.parameter = e;
        }

        public override bool Execute(Lexer l){

            Runtime.processing_parameter_list = true;
            numbers.value ps = parameter.Execute(l);
            Runtime.processing_parameter_list = false;
            switch (kind)
            {
                case Token_Type.Key_Down:
                    Proc_Call.checkOpenGraph();
                    Avalonia.Input.Key k = (Avalonia.Input.Key)ps.S[0] - 53;
                    return GraphDialogViewModel.KeyDown(k);
                case Token_Type.Mouse_Button_Down:
                    Proc_Call.checkOpenGraph();
                    Avalonia.Input.MouseButton b = (Avalonia.Input.MouseButton)ps.V - 85;
                    return GraphDialogViewModel.MouseButtonDown(b);
                case Token_Type.Mouse_Button_Pressed:
                    Proc_Call.checkOpenGraph();
                    Avalonia.Input.MouseButton b2 = (Avalonia.Input.MouseButton)ps.V - 85;
                    return GraphDialogViewModel.MouseButtonPressed(b2);
                case Token_Type.Mouse_Button_Released:
                    Proc_Call.checkOpenGraph();
                    Avalonia.Input.MouseButton b3 = (Avalonia.Input.MouseButton)ps.V - 85;
                    return GraphDialogViewModel.MouseButtonReleased(b3);
            }

            throw new NotImplementedException();
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            int t = (int)kind;
            Object o = gen.Emit_Call_Method(t);
            switch (kind)
            {
                case Token_Type.Key_Down:
                    gen.Emit_Conversion((int)Conversions.To_String);
                    parameter.Emit_Code(gen);
                    gen.Emit_End_Conversion((int)Conversions.To_String);
                    break;
                case Token_Type.Mouse_Button_Down:
                    gen.Emit_Conversion((int)Conversions.To_Integer);
                    parameter.Emit_Code(gen);
                    gen.Emit_End_Conversion((int)Conversions.To_Integer);
                    break;
                case Token_Type.Mouse_Button_Pressed:
                    gen.Emit_Conversion((int)Conversions.To_String);
                    parameter.Emit_Code(gen);
                    gen.Emit_End_Conversion((int)Conversions.To_String);
                    break;
                case Token_Type.Mouse_Button_Released:
                    gen.Emit_Conversion((int)Conversions.To_String);
                    parameter.Emit_Code(gen);
                    gen.Emit_End_Conversion((int)Conversions.To_String);
                    break;
            }
            gen.Emit_Last_Parameter(o);

        }

        public override void compile_pass1(Generate_Interface gen)
        {
            parameter.compile_pass1(gen);
        }
    }
    public class Boolean_Reflection : Boolean_Parseable
    {
        public Token_Type kind; // must be boolean_reflection_type
        public Token id;
        public Boolean_Reflection(Token_Type k, Token t)
        {
            this.kind = k;
            this.id = t;
        }

        public override bool Execute(Lexer l){
            throw new NotImplementedException();
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            throw new NotImplementedException();
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            //throw new NotImplementedException();
        }
    }
    public class Boolean_Plugin : Boolean_Parseable
    {
        public Token? id;
        public Parameter_List? parameters;

        public override bool Execute(Lexer l){

            string boolPlug = l.Get_Text(id.start, id.finish);
            numbers.value ans = Plugins.Invoke_Function(boolPlug, parameters);
            return numbers.Numbers.long_float_of(ans) > 0.5;

        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            string proc = Component.the_lexer.Get_Text(id.start, id.finish);
            gen.Emit_Plugin_Call(proc, parameters);
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            //throw new NotImplementedException();
        }
    }
    public class Boolean2 : Boolean_Parseable
    {
        public bool negated = false;
        public Boolean_Parseable left;
        public Boolean2(bool n, Boolean_Parseable l)
        {
            this.negated = n;
            this.left = l;
        }

        public override bool Execute(Lexer l){
            if(negated){
                return !left.Execute(l);
            }else{
                return left.Execute(l);
            }
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            if (gen.Is_Postfix())
            {
                left.Emit_Code(gen);
                if (negated)
                {
                    gen.Emit_Not();
                }
            }
            else
            {
                if (negated)
                {
                    gen.Emit_Not();
                }
                left.Emit_Code(gen);
            }
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            left.compile_pass1(gen);
        }

        public Boolean2 remove_negation()
        {
            return new Boolean2(false, this.left);
        }

        public bool top_level_negated()
        {
            return negated;
        }
    }
    public class And_Boolean2 : Boolean2
    {
        public Boolean2 right;
        public And_Boolean2(bool n, Boolean_Parseable l, Boolean2 r) : base(n,l)
        {
            this.right = r;
        }

        public override bool Execute(Lexer l){
            return left.Execute(l) && right.Execute(l);
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            gen.Emit_And_Shortcut(left, right, negated);
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            left.compile_pass1(gen);
            right.compile_pass1(gen);
        }
    }
    public class Boolean_Expression : Boolean_Parseable
    {
        public Boolean2 left;
        public Boolean_Expression(Boolean2 l)
        {
            this.left = l;
        }

        public override bool Execute(Lexer l){
            return left.Execute(l);
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            left.Emit_Code(gen);
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            left.compile_pass1(gen);
        }

        public bool top_level_negated()
        {
            return this.left.top_level_negated();
        }

        public Boolean_Expression remove_negation()
        {
            return new Boolean_Expression(left.remove_negation());
        }

        
    }
    public class Xor_Boolean : Boolean_Expression
    {
        public Boolean_Expression right;
        public Xor_Boolean(Boolean2 l, Boolean_Expression r) : base(l)
        {
            this.right = r;
        }

        public override bool Execute(Lexer l){
            return left.Execute(l) ^ right.Execute(l);
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            left.Emit_Code(gen);
            right.Emit_Code(gen);
            gen.Emit_Xor();
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            left.compile_pass1(gen);
            right.compile_pass1(gen);
        }

    }
    public class Or_Boolean : Boolean_Expression
    {
        public Boolean_Expression right;
        public Or_Boolean(Boolean2 l, Boolean_Expression r) : base(l)
        {
            this.right = r;
        }

        public override bool Execute(Lexer l){
            return left.Execute(l) || right.Execute(l);
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            gen.Emit_Or_Shortcut(left, right);
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            left.compile_pass1(gen);
            right.compile_pass1(gen);
        }
    }
    public class Input : Parseable
    {
        public Lhs? lhs;
        public Lsuffix? lsuffix;
        public Input(Lhs l, Lsuffix s)
        {
            this.lhs = l;
            this.lsuffix = s;
        }
        public void Execute(Lexer l, numbers.value v){
            lhs.Execute(l, v);
        }

        private void Emit_Load_Prompt(Generate_Interface gen)
        {
            /*string prompt = ((Parallelogram)Component.currentTempComponent).prompt;
            if(prompt != null)
            {
                gen.Emit_Load_String_Const(prompt);
            }
            else {*/
                gen.Emit_Load("raptor_prompt_variable_zzyz");
            //}

        }

        public override void Emit_Code(Generate_Interface gen)
        {
            Emit_Context = Context_Type.Input_Context;
            lhs.Emit_Code(gen);
            Emit_Load_Prompt(gen); 
            gen.Input_Past_Prompt();
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            lhs.compile_pass1(gen);
        }
    }

    abstract public class Output : Parseable
    {
        public bool new_line;

        public abstract numbers.value Execute(Lexer l);
        
    }
    public class Expr_Output : Output
    {
        public Expression? expr;
        public Expr_Output(Expression e, bool new_line)
        {
            this.expr = e;
            this.new_line = new_line;
        }

        public override numbers.value Execute(Lexer l){
            numbers.value v =  expr.Execute(l);
            return v;
        }

        public override void Emit_Code(Generate_Interface gen)  
        {
            bool nl = ((Parallelogram)Component.currentTempComponent).new_line;
            gen.Output_Start(nl, false);
            expr.Emit_Code(gen);
            gen.Output_Past_Expr(nl, false);
        }

        public override void compile_pass1(Generate_Interface gen)
        {
            expr.compile_pass1(gen);
        }

        public void Assign_To(Lexer l, numbers.value val)
        {
            if (expr.GetType() == typeof(Add_Expression) || expr.GetType() == typeof(Minus_Expression))
            {
                throw new Exception("Cannot have arithmetic operation here!");
            }

            Add a1 = (Add)expr.left;
            if (a1.GetType() == typeof(Div_Add) || a1.GetType() == typeof(Rem_Add) || a1.GetType() == typeof(Mod_Add) || a1.GetType() == typeof(Mult_Add))
            {
                throw new Exception("Cannot have arithmetic operation here!");
            }

            Mult m1 = (Mult)a1.left;
            if (m1.GetType() == typeof(Expon_Mult))
            {
                throw new Exception("Cannot have arithmetic operation here!");
            }

            Expon e1 = (Expon)m1.left;
            if (e1.GetType() != typeof(Rhs_Expon))
            {
                throw new Exception("Can only use a local var");
            }


            Rhs tempRHS = ((Rhs_Expon)e1).rhs;

            if (tempRHS.GetType() == typeof(Array_Ref_2D_Rhs))
            {
                Array_Ref_2D_Rhs r = (Array_Ref_2D_Rhs)tempRHS;
                string str = Component.the_lexer.Get_Text(r.id.start, r.id.finish);
                numbers.value v = r.reference.Execute(l);
                numbers.value v2 = r.reference2.Execute(l);
                if (!numbers.Numbers.is_integer(v))
                {
                    throw new Exception("Index must be an integer!");
                }
                if (!numbers.Numbers.is_integer(v2))
                {
                    throw new Exception("Index must be an integer!");
                }
                Runtime.set2DArrayElement(str, numbers.Numbers.integer_of(v), numbers.Numbers.integer_of(v2), val);
                
            }
            else if (tempRHS.GetType() == typeof(Array_Ref_Rhs))
            {
                Array_Ref_Rhs r = (Array_Ref_Rhs)tempRHS;
                string str = Component.the_lexer.Get_Text(r.id.start, r.id.finish);
                numbers.value v = r.reference.Execute(l);
                if (!numbers.Numbers.is_integer(v))
                {
                    throw new Exception("Index must be an integer!");
                }
                Runtime.setArrayElement(str, numbers.Numbers.integer_of(v), val);

            }
            else if (tempRHS.GetType() == typeof(Id_Rhs))
            {
                Id_Rhs r = (Id_Rhs)tempRHS;
                string str = l.Get_Text(r.id.start, r.id.finish);
                Runtime.setVariable(str, val);
            }
            else
            {
                throw new Exception("Not a variable!");
            }

        }

        public void Emit_Store_Dotnetgraph_Int_Func(Generate_Interface gen, int func)
        {
            if(expr.GetType() == typeof(Add_Expression) || expr.GetType() == typeof(Minus_Expression))
            {
                throw new Exception("Cannot have arithmetic operation here!");
            }

            Add a1 = (Add)expr.left;
            if (a1.GetType() == typeof(Div_Add) || a1.GetType() == typeof(Rem_Add) || a1.GetType() == typeof(Mod_Add) || a1.GetType() == typeof(Mult_Add))
            {
                throw new Exception("Cannot have arithmetic operation here!");
            }

            Mult m1 = (Mult)a1.left;
            if (m1.GetType() == typeof(Expon_Mult))
            {
                throw new Exception("Cannot have arithmetic operation here!");
            }

            Expon e1 = (Expon)m1.left;
            if (e1.GetType() == typeof(Func0_Expon) || e1.GetType() == typeof(Func_Expon) || e1.GetType() == typeof(Plugin_Func_Expon))
            {
                throw new Exception("Cannot have a function here!");
            }

            if (e1.GetType() != typeof(Rhs_Expon))
            {
                throw new Exception("Can only compile dotnetgraph to store to a local var");
            }

            Rhs tempRHS = ((Rhs_Expon)e1).rhs;

            if(tempRHS.GetType() == typeof(Array_Ref_2D_Rhs))
            {
                Array_Ref_2D_Rhs r = (Array_Ref_2D_Rhs)tempRHS;
                string str = Component.the_lexer.Get_Text(r.id.start, r.id.finish);
                gen.Array_2D_Assignment_Start(str);
                r.reference.Emit_Code(gen);
                gen.Array_2D_Assignment_Between_Indices();
                r.reference2.Emit_Code(gen);
                gen.Array_2D_Assignment_After_Indices();
                gen.Emit_Get_Click(func);
                gen.Array_2D_Assignment_PastRHS();
            }
            else if(tempRHS.GetType() == typeof(Array_Ref_Rhs))
            {
                Array_Ref_Rhs r = (Array_Ref_Rhs)tempRHS;
                string str = Component.the_lexer.Get_Text(r.id.start, r.id.finish);
                gen.Array_1D_Assignment_Start(str);
                r.reference.Emit_Code(gen);
                gen.Array_1D_Assignment_After_Index();
                gen.Emit_Get_Click(func);
                gen.Array_1D_Assignment_PastRHS();
            }
            else if(tempRHS.GetType() == typeof(Id_Rhs))
            {
                Id_Rhs r = (Id_Rhs)tempRHS;
                string str = Component.the_lexer.Get_Text(r.id.start, r.id.finish);
                gen.Variable_Assignment_Start(str);
                gen.Emit_Get_Click(func);
                gen.Variable_Assignment_PastRHS();
            }
            else
            {
                throw new Exception("Not a variable!");
            }

        }

        public void Emit_Length_Of(Generate_Interface gen)
        {
            if(expr.left.GetType() == typeof(Add_Expression) || expr.left.GetType() == typeof(Minus_Expression))
            {
                gen.Emit_Conversion((int)Conversions.To_String);
                expr.Emit_Code(gen);
                gen.Emit_End_Conversion((int)Conversions.To_String);
                gen.Emit_String_Length();
                return;
            }

            Add a1 = (Add)expr.left;
            if (a1.GetType() == typeof(Div_Add) || a1.GetType() == typeof(Rem_Add) || a1.GetType() == typeof(Mod_Add) || a1.GetType() == typeof(Mult_Add))
            {
                throw new Exception("Can only take length of string or 1D array!");
            }

            Mult m1 = (Mult)a1.left;
            if(m1.GetType() == typeof(Expon_Mult))
            {
                throw new Exception("Can only take length of string or 1D array!");
            }

            Expon e1 = (Expon)m1.left;
            if(e1.GetType() == typeof(String_Expon))
            {
                String_Expon tempExpon = (String_Expon)e1;
                string s = Component.the_lexer.Get_Text(tempExpon.s.start,  tempExpon.s.finish);
                gen.Emit_Load_Number(Convert.ToInt32(s));
                return;
            }

            if(e1.GetType() == typeof(Func0_Expon) || e1.GetType() == typeof(Func_Expon) || e1.GetType() == typeof(Plugin_Func_Expon))
            {
                throw new Exception("Can only take length of string or 1D array!");
            }

            if(e1.GetType() != typeof(Rhs_Expon))
            {
                throw new Exception("Can only take length of string or 1D array!");
            }

            Id_Rhs tempRHS = (Id_Rhs)((Rhs_Expon)e1).rhs;

            string tempStr = Component.the_lexer.Get_Text(tempRHS.id.start, tempRHS.id.finish);
            gen.Emit_Array_Size(tempStr);
        }

    }
    //Parameter_List => Output[, Parameter_List | Lambda]
    public class Parameter_List
    {
        public Output? parameter;
        public Parameter_List? next;
        public Parameter_List(Output p, Parameter_List? n)
        {
            this.parameter = p;
            this.next = n;
        }

        public int getLen(){
            if(parameter == null){
                return 0;
            } else if(next == null){
                return 1;
            }else{
                return 1 + next.getLen();
            }
        }

        public numbers.value[] Execute(Lexer l){
            numbers.value[] ans = new numbers.value[getLen()];
            Output tempParam = parameter;
            Parameter_List tempNext = next;

            for(int i = 0; i < ans.Length; i++){
                ans[i] = tempParam.Execute(l);
                if(tempNext != null){
                    tempParam = tempNext.parameter;
                    if(tempNext.next != null){
                        tempNext = tempNext.next;
                    }
                }
            }

            return ans;
        }

        public void Emit_Code(Generate_Interface gen) 
        {
            throw new NotImplementedException();
        }

        public void compile_pass1(Generate_Interface gen)
        {
            //throw new NotImplementedException();
        }

    }
}
