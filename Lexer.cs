using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using raptor;
using RAPTOR_Avalonia_MVVM.ViewModels;

namespace RAPTOR_Avalonia_MVVM
{
    public class Lexer_Exception : Exception
    {
        public Lexer_Exception(string s) : base(s) { }
    }
    public class Bad_Count : Exception
    {
        public Bad_Count(string s) : base(s) { }
    }
    public class Bad_Token : Exception
    {
        public Bad_Token(string s) : base(s) { }
    }
    public class Twice_Ungotten : Exception
    {
    }
    public enum Token_Type
    {
        Plus, Minus, Times, Divide, Exponent, Left_Paren,
        Right_Paren, Id, Number, Character,
        String, Comma,
        Semicolon, Colon_Equal, Left_Bracket, Right_Bracket, End_Input,
        New, Dot,
        // relational operators
        Equal, Greater, Less, Not_Equal, Greater_Equal, Less_Equal,
        And, Or, Xor, Not, Mod, Rem,
        // trig functions (1 or 2 params)
        Sin, Cos, Tan, Cot, Arcsin, Arccos, Log,
        // trig functions(2, or 3 parameters)
        Arctan, Arccot,
        // functions of 2 parameters
        Min, Max,
        // functions of 1 parameter
        Sinh, Tanh, Cosh, Arccosh, Arcsinh, Arctanh,
        Coth, Arccoth, Sqrt, Floor, Ceiling, to_ascii, to_character, Length_Of, Abs,
        // functions of 0 parameters
        E, Pi, Random,
        Black, Blue, Green, Cyan, Red, Magenta, Brown,
        Light_Gray, Dark_Gray, Light_Blue, Light_Green,
        Light_Cyan, Light_Red, Light_Magenta, Yellow, Pink, Purple, White,
        Filled, Unfilled,
        True, False,
        Yes, No,
        Random_Color, Random_Extended_Color,
        Left_Button, Right_Button,
        // adagraph functions 0 parameters
        Get_Max_Width, Get_Max_Height,
        Get_Mouse_X,
        Get_Mouse_Y,
        Get_Window_Width, Get_Window_Height,
        Get_Key, Get_Key_String, Get_Font_Width, Get_Font_Height,
        // adagraph functions 1 parameters
        Load_Bitmap, 
        // functions of 2 parameters
        Get_Pixel,
        // functions of 3 parameters
        Closest_Color,
        // regular procedures no params
        Clear_Console,
        // regular procedures with params
        Delay_For, Redirect_Output_Append, Set_Precision, Redirect_Input, Redirect_Output,
        // adagraph procedures
        Clear_Window, Play_Sound, Play_Sound_Background, Play_Sound_Background_Loop, Display_Number, Display_Text, Draw_Arc,
        // adagraph procs of 0 params
        Close_Graph_Window, Freeze_Graph_Window, Update_Graph_Window,
        Unfreeze_Graph_Window,
        // end adagraph procs of 0 params
        Draw_Bitmap,
        Draw_Box, Draw_Circle, Draw_Ellipse, Draw_Ellipse_Rotate, Draw_Line,
        Flood_Fill, Open_Graph_Window, Wait_For_Key,
        Wait_For_Mouse_Button,
        Get_Mouse_Button,
        Set_Font_Size,
        Put_Pixel, Save_Graph_Window, Set_Window_Title,
        // reflection boolean functions(1 param)
        Is_Number, Is_String, Is_Character, Is_Array, Is_2D_Array,
        // adagraph boolean functions(1 param, then 0 param)
        Mouse_Button_Pressed, Mouse_Button_Down, Key_Down, Mouse_Button_Released, Is_Open, Key_Hit,
        // other boolean functions(0 param)
        End_Of_Input
    }

    public class Token
    {
        public Token_Type kind;
        public int start;
        public int finish;
        public Token(Token_Type kind, int start, int finish)
        {
            this.kind = kind;
            this.start = start;
            this.finish = finish;
        }
    }
    public class Lexer
    {
        public static bool isRelation(Token_Type t)
        {
            return (t >= Token_Type.Equal && t <= Token_Type.Less_Equal);
        }
        public static bool isSubprogram(Token_Type t)
        {
            return (t >= Token_Type.Sin && t <= Token_Type.End_Of_Input);
        }
        public static bool isOther_Proc0(Token_Type t)
        {
            return (t >= Token_Type.Clear_Console && t <= Token_Type.Clear_Console);
        }
        public static bool isGraph_Subprogram(Token_Type t)
        {
            return (t >= Token_Type.Random_Color && t <= Token_Type.Key_Hit);
        }
        public static bool isGraph_Proc0(Token_Type t)
        {
            return (t >= Token_Type.Close_Graph_Window && t <= Token_Type.Unfreeze_Graph_Window);
        }
        public static bool isFunc_Token_Type(Token_Type t)
        {
            return (t >= Token_Type.Sin && t <= Token_Type.Closest_Color);
        }
        public static bool isFunc_Id1(Token_Type t)
        {
            return (t >= Token_Type.Sinh && t <= Token_Type.Abs);
        }
        public static bool isFunc_Id0(Token_Type t)
        {
            return (t >= Token_Type.E && t <= Token_Type.Get_Font_Height);
        }
        public static bool isFunc_Id1or2(Token_Type t)
        {
            return (t >= Token_Type.Sin && t <= Token_Type.Log);
        }
        public static bool isFunc_Id2or3(Token_Type t)
        {
            return (t >= Token_Type.Arctan && t <= Token_Type.Arccot);
        }
        public static bool isGraph_Func_Id2(Token_Type t)
        {
            return (t >= Token_Type.Get_Pixel && t <= Token_Type.Get_Pixel);
        }
        public static bool isOther_Func_Id2(Token_Type t)
        {
            return (t >= Token_Type.Min && t <= Token_Type.Max);
        }
        public static bool isFunc_Id3(Token_Type t)
        {
            return (t >= Token_Type.Closest_Color && t <= Token_Type.Closest_Color);
        }
        public static bool isProc_Token_Type(Token_Type t)
        {
            return (t >= Token_Type.Clear_Console && t <= Token_Type.Set_Window_Title);
        }
        public static bool isBoolean_Func_Type(Token_Type t)
        {
            return (t >= Token_Type.Is_Number && t <= Token_Type.End_Of_Input);
        }
        public static bool isBoolean_Reflection_Type(Token_Type t)
        {
            return (t >= Token_Type.Is_Number && t <= Token_Type.Is_2D_Array);
        }
        public static bool isBoolean_Func0_Type(Token_Type t)
        {
            return (t >= Token_Type.Is_Open && t <= Token_Type.End_Of_Input);
        }
        public static bool isBoolean_Func1_Type(Token_Type t)
        {
            return (t >= Token_Type.Mouse_Button_Pressed && t <= Token_Type.Mouse_Button_Released);
        }
        public static bool Has_Parameters(Token_Type t)
        {
            return !(isBoolean_Func0_Type(t) || isFunc_Id0(t) || isGraph_Proc0(t) || isOther_Proc0(t));
        }
        public static bool Is_Procedure(Token_Type t)
        {
            return isProc_Token_Type(t);
        }
        public static bool Is_Valid_Procedure(string s)
        {
            foreach(Subchart v in MainWindowViewModel.GetMainWindowViewModel().theTabs){
                if(v.Text.Equals(s)){
                    return true;
                }
            }
            return false;
        }
        private string Current_String;
        private int Current_Location;
        private Token? Ungotten_Token;

        public Lexer(string s)
        {
            Current_String = s;
            Current_Location = 0;
            this.Ungotten_Token = null;
        }
        public void Unget_Token(Token p)
        {
            if (this.Ungotten_Token != null)
            {
                throw new Twice_Ungotten();
            }
            else
            {
                this.Ungotten_Token = p;
            }
        }
        public string Get_Text()
        {
            return this.Current_String;
        }
        public string Get_Text(int start, int finish)
        {
            if(Current_String == "")
            {
                return "";
            }
            return this.Current_String.Substring(start, finish - start + 1);
        }
        public void Rewind(Token p)
        {
            Current_Location = p.finish + 1;
            Ungotten_Token = null;
        }
        public static string get_image(Token_Type token)
        {
            if(token==Token_Type.Get_Mouse_Button)
            {
                return "wait_for_mouse_button";
            }
            else
            {
                return token.ToString();
            }
        }
        public int Get_Current_Location()
        {
            return this.Current_Location;
        }
        public Token_Type Id_Kind(string lexeme)
        {
            Token_Type result;
            if (Enum.TryParse<Token_Type>(lexeme,true,out result))
            {
                return result;
            }
            else
            {
                return Token_Type.Id;
            }
        }

        public int Parameter_Count(Token_Type t)
        {
            switch (t)
            {
                case Token_Type.Clear_Console:
                    return 0;
                case Token_Type.Delay_For:
                    return 1;
                case Token_Type.Draw_Bitmap:
                    return 5;
                case Token_Type.Redirect_Output_Append:
                    return 1;
                case Token_Type.Set_Precision:
                    return 1;
                case Token_Type.Redirect_Input:
                    return 1;
                case Token_Type.Redirect_Output:
                    return 1;
                case Token_Type.Clear_Window:
                    return 1;
                case Token_Type.Close_Graph_Window:
                    return 0;
                case Token_Type.Display_Number:
                    return 4;
                case Token_Type.Display_Text:
                    return 4;
                case Token_Type.Freeze_Graph_Window:
                    return 0;
                case Token_Type.Unfreeze_Graph_Window:
                    return 0;
                case Token_Type.Update_Graph_Window:
                    return 0;
                case Token_Type.Draw_Box:
                    return 6;
                case Token_Type.Draw_Arc:
                    return 9;
                case Token_Type.Draw_Circle:
                    return 5;
                case Token_Type.Draw_Ellipse:
                    return 6;
                case Token_Type.Draw_Ellipse_Rotate:
                    return 7;
                case Token_Type.Draw_Line:
                    return 5;
                case Token_Type.Flood_Fill:
                    return 3;
                case Token_Type.Get_Mouse_Button:
                    return 3;
                case Token_Type.Open_Graph_Window:
                    return 2;
                case Token_Type.Wait_For_Key:
                    return 0;
                case Token_Type.Wait_For_Mouse_Button:
                    return 1;
                case Token_Type.Put_Pixel:
                    return 3;
                case Token_Type.Set_Font_Size:
                    return 1;
                case Token_Type.Save_Graph_Window:
                    return 1;
                case Token_Type.Set_Window_Title:
                    return 1;
                case Token_Type.Play_Sound:
                case Token_Type.Play_Sound_Background:
                case Token_Type.Play_Sound_Background_Loop:
                    return 1;
            }
            throw new Lexer_Exception("invalid parameter to Parameter_Count");
        }
        public void Verify_Parameter_Count(string Proc_Name, int count)
        {
            Token_Type t;
            Enum.TryParse<Token_Type>(Proc_Name, true, out t);
            if (count!=Parameter_Count(t))
            {
                throw new Bad_Count(Proc_Name + " should have " + Parameter_Count(t) + " parameters.");
            }
        }

        public Token Get_Token()
        {
            int Start_Location;
            bool Has_Decimal = false;
            bool Has_Digit_After_Decimal = false;
            if (this.Ungotten_Token != null)
            {
                Token result = this.Ungotten_Token;
                this.Ungotten_Token = null;
                return result;
            }
            char Current_Char = ' ';
            while (Char.IsControl(Current_Char) || Current_Char == ' ')
            {
                if (this.Current_Location>=this.Current_String.Length)
                {
                    return new Token(Token_Type.End_Input, this.Current_Location, this.Current_Location);
                }
                Current_Char = this.Current_String[this.Current_Location++];
            }
            Start_Location = this.Current_Location - 1;
            // Current_Char is at Current_Location - 1
            switch (Current_Char)
            {
                case '\'':
                    if (this.Current_Location + 1 < this.Current_String.Length &&
                        this.Current_String[this.Current_Location + 1] == '\'')
                    {
                        this.Current_Location += 2;
                        return new Token(Token_Type.Character, this.Current_Location - 3, this.Current_Location - 1);
                    }
                    else
                    {
                        throw new Bad_Token("Bad characer, expected something like 'x'");
                    }
                case '"':
                    while (this.Current_Location < this.Current_String.Length)
                    {
                        Current_Char = this.Current_String[this.Current_Location];
                        if (Current_Char == '"')
                        {
                            if (this.Current_Location + 1 >= this.Current_String.Length ||
                                this.Current_String[this.Current_Location + 1] != '"')
                            {
                                // skip two on ""
                                break;
                            }
                            this.Current_Location++;
                        }
                        else if (Current_Char == '\\')
                        {
                            if (this.Current_Location + 1 < this.Current_String.Length &&
                                this.Current_String[this.Current_Location + 1] == '"')
                            {
                                // skip two on \"
                                this.Current_Location++;
                            }
                        }
                        this.Current_Location++;
                    }
                    if (this.Current_Location >= this.Current_String.Length)
                    {
                        throw new Bad_Token("missing closing \" for string");
                    }
                    this.Current_Location++;
                    return new Token(Token_Type.String, Start_Location, Current_Location - 1);
                case '[':
                    return new Token(Token_Type.Left_Bracket, Current_Location - 1, Current_Location - 1);
                case ']':
                    return new Token(Token_Type.Right_Bracket, Current_Location - 1, Current_Location - 1);
                case ';':
                    return new Token(Token_Type.Semicolon, Current_Location - 1, Current_Location - 1);
                case '+':
                    return new Token(Token_Type.Plus, Current_Location - 1, Current_Location - 1);
                case '-':
                    return new Token(Token_Type.Minus, Current_Location - 1, Current_Location - 1);
                case ',':
                    return new Token(Token_Type.Comma, Current_Location - 1, Current_Location - 1);
                case '^':
                    return new Token(Token_Type.Exponent, Current_Location - 1, Current_Location - 1);
                case '%':
                    return new Token(Token_Type.Mod, Current_Location - 1, Current_Location - 1);
                case '(':
                    return new Token(Token_Type.Left_Paren, Current_Location - 1, Current_Location - 1);
                case ')':
                    return new Token(Token_Type.Right_Paren, Current_Location - 1, Current_Location - 1);
                case '!':
                    if (this.Current_Location < this.Current_String.Length &&
                        this.Current_String[this.Current_Location] == '=')
                    {
                        this.Current_Location++;
                        return new Token(Token_Type.Not_Equal, Current_Location - 2, Current_Location - 1);
                    }
                    else
                    {
                        throw new Bad_Token("expected !=, found !");
                    }
                case '/':
                    if (this.Current_Location < this.Current_String.Length &&
                        this.Current_String[this.Current_Location] == '=')
                    {
                        this.Current_Location++;
                        return new Token(Token_Type.Not_Equal, Current_Location - 2, Current_Location - 1);
                    }
                    else
                    {
                        return new Token(Token_Type.Divide, Current_Location - 1, Current_Location - 1);
                    }
                case '*':
                    if (this.Current_Location < this.Current_String.Length &&
                        this.Current_String[this.Current_Location] == '*')
                    {
                        this.Current_Location++;
                        return new Token(Token_Type.Exponent, Current_Location - 2, Current_Location - 1);
                    }
                    else
                    {
                        return new Token(Token_Type.Times, Current_Location - 1, Current_Location - 1);
                    }
                case '&':
                    if (this.Current_Location < this.Current_String.Length &&
                        this.Current_String[this.Current_Location] == '&')
                    {
                        this.Current_Location++;
                        return new Token(Token_Type.And, Current_Location - 2, Current_Location - 1);
                    }
                    else
                    {
                        throw new Bad_Token("expected &&, found &");
                    }
                case '|':
                    if (this.Current_Location < this.Current_String.Length &&
                        this.Current_String[this.Current_Location] == '|')
                    {
                        this.Current_Location++;
                        return new Token(Token_Type.Or, Current_Location - 2, Current_Location - 1);
                    }
                    else
                    {
                        throw new Bad_Token("expected ||, found |");
                    }
                case ':':
                    if (this.Current_Location < this.Current_String.Length &&
                        this.Current_String[this.Current_Location] == '=')
                    {
                        this.Current_Location++;
                        return new Token(Token_Type.Colon_Equal, Current_Location - 2, Current_Location - 1);
                    }
                    else
                    {
                        throw new Bad_Token(": is not allowed");
                    }
                case '>':
                    if (this.Current_Location < this.Current_String.Length &&
                        this.Current_String[this.Current_Location] == '=')
                    {
                        this.Current_Location++;
                        return new Token(Token_Type.Greater_Equal, Current_Location - 2, Current_Location - 1);
                    }
                    else
                    {
                        return new Token(Token_Type.Greater, Current_Location - 1, Current_Location - 1);
                    }
                case '<':
                    if (this.Current_Location < this.Current_String.Length &&
                        this.Current_String[this.Current_Location] == '=')
                    {
                        this.Current_Location++;
                        return new Token(Token_Type.Less_Equal, Current_Location - 2, Current_Location - 1);
                    }
                    else
                    {
                        return new Token(Token_Type.Less, Current_Location - 1, Current_Location - 1);
                    }
                case '=':
                    if (this.Current_Location < this.Current_String.Length &&
                        this.Current_String[this.Current_Location] == '=')
                    {
                        this.Current_Location++;
                        return new Token(Token_Type.Equal, Current_Location - 2, Current_Location - 1);
                    }
                    else
                    {
                        return new Token(Token_Type.Equal, Current_Location - 1, Current_Location - 1);
                    }
                case char i when i >= 'a' && i <= 'z':
                case char j when j >= 'A' && j <= 'Z':
                    while (this.Current_Location < this.Current_String.Length)
                    {
                        Current_Char = this.Current_String[this.Current_Location];
                        if (!Char.IsLetterOrDigit(Current_Char) && Current_Char!='_')
                        {
                            break;
                        }
                        this.Current_Location++;
                    }
                    return new Token(Id_Kind(this.Current_String.Substring(Start_Location,Current_Location-Start_Location)),
                        Start_Location, Current_Location - 1);
                case '.':
                case char k when k >= '0' && k <= '9':
                    if (Current_Char=='.')
                    {
                        Has_Decimal = true;
                    }
                    while (this.Current_Location < this.Current_String.Length)
                    {
                        Current_Char = this.Current_String[this.Current_Location];
                        if (Current_Char == '.')
                        {
                            if (Has_Decimal)
                            {
                                throw new Bad_Token("number has two decimal points");
                            }
                            else
                            {
                                Has_Decimal = true;
                            }
                        }
                        else
                        {
                            if (!Char.IsDigit(Current_Char))
                            {
                                break;
                            }
                            else if (Has_Decimal)
                            {
                                Has_Digit_After_Decimal = true;
                            }
                        }
                        this.Current_Location++;
                    }
                    if (Has_Decimal && !Has_Digit_After_Decimal) {
                        if (Current_Location == Start_Location + 1)
                        {
                            return new Token(Token_Type.Dot, Current_Location - 1, Current_Location - 1);
                        }
                        else
                        {
                            throw new Bad_Token("number may not end with decimal point");
                        }
                    }
                    return new Token(Token_Type.Number, Start_Location, Current_Location-1);
                case '_':
                    throw new Bad_Token("identifier can not begin with '_'");
                default:
                    throw new Bad_Token("found invalid character: '" + Current_Char + "'");
            }
        }
    }
}
