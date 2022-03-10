using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parse_tree
{
    public class parseable
    {
    }
    public class value_parseable : parseable
    {

    }
    public class expression : value_parseable
    {
        public string get_class_decl() { return ""; }
    }
    public class input : parseable
    {

    }
    public class procedure_call : parseable
    {
        public bool is_tab_call() { return false; }
    }
    public class assignment : parseable { }
    public class statement : parseable { }
    public class lhs { }
    public class expr_assignment : assignment {
        public expression expr_part;
    }
}
