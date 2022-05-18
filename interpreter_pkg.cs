using interpreter;
using parse_tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAPTOR_Avalonia_MVVM;

namespace raptor
{
    class interpreter_pkg
    {
        internal static string get_name_input(parse_tree.Input parse_tree, string text)
        {
            return text;
            //throw new NotImplementedException();
        }

        internal static string get_name_call(Procedure_Call? procedure_call, string text)
        {
            return text;
            //throw new NotImplementedException();
        }
        public static Syntax_Result generic_syntax(Lexer lexer, Parser parser, Func<Parseable> parse_method)
        {
            Syntax_Result result = new Syntax_Result();
            try
            {
                result.tree = parse_method();
                result.valid = true;
            }
            catch (Syntax_Error e)
            {
                result.valid = false;
                result.location = parser.Get_Current_Token().start;
                result.message = e.Message;
            }
            catch (Bad_Token e)
            {
                result.valid = false;
                result.location = lexer.Get_Current_Location();
                result.message = e.Message;
            }
            return result;
        }
        public static Syntax_Result assignment_syntax(string text, 
            string text2)
        {
            Lexer lexer = new Lexer(text + ":=" + text2);
            Parser parser = new Parser(lexer);

            return generic_syntax(lexer,parser, parser.Parse_Assignment_Statement);
        }
        public static Syntax_Result call_syntax(string text)
        {
            Lexer lexer = new Lexer(text);
            Parser parser = new Parser(lexer);
            return generic_syntax(lexer, parser, parser.Parse_Call_Statement);
        }

        public static Syntax_Result conditional_syntax(string text)
        {
            Lexer lexer = new Lexer(text);
            Parser parser = new Parser(lexer);
            return generic_syntax(lexer, parser, parser.Parse_Condition);
        }

        public static Syntax_Result input_syntax(string text)
        {
            Lexer lexer = new Lexer(text);
            Parser parser = new Parser(lexer);
            return generic_syntax(lexer, parser, parser.Parse_Input_Statement);
        }
        public static Syntax_Result statement_syntax(string text, bool isCallBox)
        {
            if (isCallBox)
            {
                return call_syntax(text);
            }
            else
            {
                Lexer lexer = new Lexer(text);
                Parser parser = new Parser(lexer);

                return generic_syntax(lexer, parser, parser.Parse_Assignment_Statement);
            }
        }
        public static Syntax_Result output_syntax(string text, bool new_line)
        {
            Lexer lexer = new Lexer(text);
            Parser parser = new Parser(lexer);
            Syntax_Result result = generic_syntax(lexer, parser, parser.Parse_Output_Statement);
            if (result.valid && result.tree != null)
            {
                ((Output)result.tree).new_line = new_line;
            }
            return result;
        }
        internal static string get_name(Assignment parse_tree, string text)
        {
            return text;
        }
    }
}
