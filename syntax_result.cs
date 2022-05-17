using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interpreter
{
    public class Syntax_Result
    {
        public bool valid = true;
        public string? message;
        public int location;
        public parse_tree.Parseable? tree;
    }

    public class Suggestion_Result
    {
        public int bold_start;
        public int bold_finish;
        public List<string>? suggestions;
    }
}
