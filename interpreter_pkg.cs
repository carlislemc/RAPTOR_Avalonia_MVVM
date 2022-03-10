using interpreter;
using parse_tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raptor
{
    class interpreter_pkg
    {
        internal static syntax_result output_syntax(string text, bool v, Component oval_Return)
        {
            return new syntax_result();
            //throw new NotImplementedException();
        }

        internal static syntax_result input_syntax(string text, Parallelogram parallelogram)
        {
            return new syntax_result();
            //throw new NotImplementedException();
        }

        internal static syntax_result statement_syntax(string text, bool v, Rectangle rectangle)
        {
            return new syntax_result();
            //throw new NotImplementedException();
        }

        internal static syntax_result conditional_syntax(string text, BinaryComponent iF_Control)
        {
            return new syntax_result();
            //throw new NotImplementedException();
        }

        internal static string get_name_input(parse_tree.input parse_tree, string text)
        {
            return text;
            //throw new NotImplementedException();
        }

        internal static string get_name_call(procedure_call? procedure_call, string text)
        {
            return text;
            //throw new NotImplementedException();
        }

        internal static syntax_result call_syntax(string text, Rectangle rectangle)
        {
            return new syntax_result();
            //throw new NotImplementedException();
        }

        internal static string get_name(assignment parse_tree, string text)
        {
            return text;
        }
    }
}
