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
        internal static Syntax_Result output_syntax(string text, bool v, Component oval_Return)
        {
            return new Syntax_Result();
            //throw new NotImplementedException();
        }

        internal static Syntax_Result input_syntax(string text, Parallelogram parallelogram)
        {
            return new Syntax_Result();
            //throw new NotImplementedException();
        }

        internal static Syntax_Result statement_syntax(string text, bool v, Rectangle rectangle)
        {
            return new Syntax_Result();
            //throw new NotImplementedException();
        }

        internal static Syntax_Result conditional_syntax(string text, BinaryComponent iF_Control)
        {
            return new Syntax_Result();
            //throw new NotImplementedException();
        }

        internal static string get_name_input(parse_tree.Input parse_tree, string text)
        {
            return text;
            //throw new NotImplementedException();
        }

        internal static string get_name_call(procedure_call? procedure_call, string text)
        {
            return text;
            //throw new NotImplementedException();
        }

        internal static Syntax_Result call_syntax(string text, Rectangle rectangle)
        {
            Lexer lexer = new Lexer(text);
            Parser parser = new Parser(lexer);
            Syntax_Result result = new Syntax_Result();
            try
            {
                result.tree = parser.Parse_Call_Statement();
                result.valid = true;
            }
            catch (Bad_Token e)
            {
                result.valid = false;
                result.location = 1;
                result.message = e.Message;
            }
            return result;
            //throw new NotImplementedException();
        }

        internal static string get_name(Assignment parse_tree, string text)
        {
            return text;
        }
    }
}
