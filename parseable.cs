using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using raptor;
using RAPTOR_Avalonia_MVVM;

namespace parse_tree
{
    public abstract class Parseable
    {
    }
    public abstract class Value_Parseable : Parseable
    {

    }
    public class Expression : Value_Parseable
    {
        Value_Parseable left;
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
    }
    public abstract class Binary_Expression : Expression
    {
        public Expression right;
        public Binary_Expression(Value_Parseable left) : base(left) { }
    }
    public class Add_Expression : Binary_Expression
    {
        public Add_Expression(Value_Parseable left) : base(left) { }
    }
    public class Minus_Expression : Binary_Expression
    {
        public Minus_Expression(Value_Parseable left) : base(left) { }

    }

    // Procedure_Call => proc_id(Parameter_List) | plugin_id(Parameter_List) | tab_id(Parameter_List) |
    // lhs msuffix
    public abstract class Procedure_Call : Statement
    {
        public Token? id;
        public Parameter_List? param_list;
        public bool is_tab_call() { return false; }
    }

    public class Proc_Call : Procedure_Call
    {

    }
    public class Plugin_Proc_Call : Procedure_Call { }

    public class Tabid_Proc_Call : Procedure_Call { }

    public class Method_Proc_Call : Procedure_Call
    {
        Lhs? lhs;
        Msuffix? msuffix;
    }

    public abstract class Assignment : Statement {
        public Lhs? lhs;
        public Lsuffix? lsuffix;
    }
    public class Expr_Assignment : Assignment
    {
        public Expression expr_part;
    }

    // Statement => (Procedure_Call | Assignment) [;] End_Input
    public abstract class Statement : Parseable { }

    // Lhs => id[\[Expression[, Expression]\]]
    public class Lhs { }
    public class Id_Lhs : Lhs
    {
        public Token id;
        public Id_Lhs(Token id)
        {
            this.id = id;
        }
    }
    public class Array_Ref_Lhs : Id_Lhs
    {
        public Expression reference;
        public Array_Ref_Lhs(Token id, Expression reference) : base(id)
        {
            this.reference = reference;
        }
    }
    public class Array_2D_Ref_Lhs : Array_Ref_Lhs
    {
        public Expression reference2;
        public Array_2D_Ref_Lhs(Token id, Expression reference, Expression ref2) : base(id,reference)
        {
            this.reference2 = ref2;
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

    public abstract class Rhs { }
    public class Id_Rhs : Rhs
    {
        public Token id;
        public Id_Rhs() { }
        public Id_Rhs(Token ident)
        {
            this.id = ident;
        }
    }
    public class Array_Ref_Rhs : Id_Rhs
    {
        public Expression reference;
    }
    public class Array_Ref_2D_Rhs : Array_Ref_Rhs
    {
        public Expression reference2;
    }

    public class Rhs_Method_Call : Id_Rhs
    {
        public Parameter_List parameters;
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

    public abstract class Expon : Value_Parseable { }

    public class Expon_Stub : Expon
    {
        public Component component;
        public int index;
        public Expon expon_parse_tree;
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
    }
    public class Number_Expon : Expon
    {
        public Token number;
        public Number_Expon(Token t)
        {
            this.number = t;
        }
    }
    public class Negative_Expon : Expon
    {
        public Expon e;
        public Negative_Expon(Expon e)
        {
            this.e = e;
        }
    }
    public class String_Expon : Expon
    {
        public Token s;
        public String_Expon(Token s)
        {
            this.s = s;
        }
    }
    public class Paren_Expon : Expon
    {
        public Expression expr_part;
        public Paren_Expon(Expression e)
        {
            this.expr_part = e;
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
    }
    public class Func0_Expon : Id_Expon
    {
        public Func0_Expon(Token id) : base(id) { }
    }
    public class Character_Expon : Expon
    {
        public Token s;
        public Character_Expon(Token s)
        {
            this.s = s;
        }
    }

    public class Func_Expon : Id_Expon {
        public Parameter_List parameters;
    }
    public class Plugin_Func_Expon : Id_Expon
    {
        public Parameter_List parameters;
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
    }
    public class Expon_Mult : Mult
    {
        public Mult right;
        public Expon_Mult(Value_Parseable left, Mult right) : base(left)
        {
            this.right = right;
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
    }
    public class Binary_Add : Add
    {
        public Add right;
        public Binary_Add(Value_Parseable left, Add right) : base(left)
        {
            this.right = right;
        }
    }
    public class Div_Add : Binary_Add
    {
        public Div_Add(Value_Parseable left, Add right) : base(left,right)
        {
        }
    }
    public class Mult_Add : Binary_Add
    {
        public Mult_Add(Value_Parseable left, Add right) : base(left,right)
        {
        }
    }
    public class Mod_Add : Binary_Add
    {
        public Mod_Add(Value_Parseable left, Add right) : base(left,right)
        {
        }
    }
    public class Rem_Add : Binary_Add
    {
        public Rem_Add(Value_Parseable left, Add right) : base(left,right)
        {
        }
    }
    public abstract class Boolean_Parseable : Parseable
    {

    }
    //  Relation => Expression > Expression | >=,<,<=,=,/=
    public class Relation : Boolean_Parseable
    {
        public Expression? left, right;
        public Token_Type? kind; // must be a relation
    }
    public class Boolean0 : Boolean_Parseable
    {
        public Token_Type kind; // must be a Boolean_Func0_Type
        public Boolean0(Token_Type kind)
        {
            this.kind = kind;
        }
    }
    public class Boolean_Constant : Boolean_Parseable
    {
        public bool value;
        public Boolean_Constant(bool val)
        {
            this.value = val;
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
    }
    public class Boolean_Plugin : Boolean_Parseable
    {
        public Token? id;
        public Parameter_List? parameters;
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
    }
    public class And_Boolean2 : Boolean2
    {
        public Boolean2 right;
        public And_Boolean2(bool n, Boolean_Parseable l, Boolean2 r) : base(n,l)
        {
            this.right = r;
        }
    }
    public class Boolean_Expression : Boolean_Parseable
    {
        public Boolean2 left;
        public Boolean_Expression(Boolean2 l)
        {
            this.left = l;
        }
    }
    public class Xor_Boolean : Boolean_Expression
    {
        public Boolean_Expression right;
        public Xor_Boolean(Boolean2 l, Boolean_Expression r) : base(l)
        {
            this.right = r;
        }
    }
    public class Or_Boolean : Boolean_Expression
    {
        public Boolean_Expression right;
        public Or_Boolean(Boolean2 l, Boolean_Expression r) : base(l)
        {
            this.right = r;
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
    }

    abstract public class Output : Parseable
    {
        public bool new_line;
    }
    public class Expr_Output : Output
    {
        public Expression? expr;
        public Expr_Output(Expression e, bool new_line)
        {
            this.expr = e;
            this.new_line = new_line;
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
    }
}
