using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using raptor;
using RAPTOR_Avalonia_MVVM;
using System.Collections;
using RAPTOR_Avalonia_MVVM.ViewModels;

namespace parse_tree
{
    public abstract class Parseable
    {
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
    }
    public class Minus_Expression : Binary_Expression
    {
        public Minus_Expression(Value_Parseable left) : base(left) { }

        public override numbers.value Execute(Lexer l){

            numbers.value first = left.Execute(l);
            numbers.value second = right.Execute(l);
            return numbers.Numbers.subValues(first, second);
        }

    }

    // Procedure_Call => proc_id(Parameter_List) | plugin_id(Parameter_List) | tab_id(Parameter_List) |
    // lhs msuffix
    public abstract class Procedure_Call : Statement
    {
        public Token? id;
        public Parameter_List? param_list;
        public bool is_tab_call() { return false; }

        public abstract void Execute(Lexer l);
    }

    public class Proc_Call : Procedure_Call
    {
        public override void Execute(Lexer l)
        {
            numbers.value[] ps = param_list.Execute(l);
            
            MainWindowViewModel mw = MainWindowViewModel.GetMainWindowViewModel();
            string head = l.Get_Text(id.start, id.finish);
            Subchart sub = mw.mainSubchart();
            foreach(Subchart s in mw.theTabs){
                if(s.Header == head){
                    sub = s;
                }
            }
            Oval_Procedure st = ((Oval_Procedure)sub.Start);
            string[] paramNames = st.param_names;
            Runtime.Increase_Scope(head);
            mw.decreaseScope = true;
            mw.activeScope = head;
            mw.parentComponent = mw.activeComponent;
            mw.activeComponent = mw.theTabs[mw.theTabs.IndexOf(sub)].Start;
            mw.activeComponent.running = true;
            mw.activeTab = mw.theTabs.IndexOf(sub);
            for(int i = 0; i < ps.Length; i++){
               if(!st.is_input_parameter(i)){
                   break;
               }
               numbers.value num = ps[i];
               Variable v2 = new Variable(paramNames[i], num);
               Variable temp = v2;
               mw.theVariables.RemoveAt(mw.theVariables.IndexOf(temp));
               mw.theVariables.Insert(1, temp);
            }
            return;
        }

    }
    public class Plugin_Proc_Call : Procedure_Call {
        public override void Execute(Lexer l)
        {
            Variable v = new Variable(l.Get_Text(id.start, id.finish), new numbers.value(){V=2});
            return;
        }
     }

    public class Tabid_Proc_Call : Procedure_Call {
        public override void Execute(Lexer l)
        {
            Variable v = new Variable(l.Get_Text(id.start, id.finish), new numbers.value(){V=3});
            return;
        }
     }

    public class Method_Proc_Call : Procedure_Call
    {
        Lhs? lhs;
        Msuffix? msuffix;

        public override void Execute(Lexer l)
        {
            Variable v = new Variable(l.Get_Text(id.start, id.finish), new numbers.value(){V=4});
            return;
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

    }

    // Statement => (Procedure_Call | Assignment) [;] End_Input
    public abstract class Statement : Parseable { }

    // Lhs => id[\[Expression[, Expression]\]]
    public abstract class Lhs {
        public abstract void Execute(Lexer l, numbers.value v);
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
            temp.V = temp.V * -1 - 1;
            return temp;
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
            return new numbers.value(){Kind=numbers.Value_Kind.String_Kind, S=l.Get_Text(s.start, s.finish)};
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
            throw new NotImplementedException();
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
           }
            throw new NotImplementedException();
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
    }

    public class Func_Expon : Id_Expon {
        public Parameter_List parameters;

        public override numbers.value Execute(Lexer l){
            string s = l.Get_Text(id.start, id.finish);
            numbers.value[] ps = parameters.Execute(l);
            switch(s.ToLower()){
                case "sin":
                    return new numbers.value(){V=Math.Sin(ps[0].V)};
                case "cos":
                    return new numbers.value(){V=Math.Cos(ps[0].V)};
                case "tan":
                    return new numbers.value(){V=Math.Tan(ps[0].V)};
                case "cot":
                    return new numbers.value(){V=1/Math.Tan(ps[0].V)};
                case "arcsin":
                    return new numbers.value(){V=Math.Asin(ps[0].V)};
                case "arccos":
                    return new numbers.value(){V=Math.Acos(ps[0].V)};
                case "log":
                    return new numbers.value(){V=Math.Log(ps[0].V)};
                case "arctan":
                    return new numbers.value(){V=Math.Atan(ps[0].V)};
                case "arccot":
                    return new numbers.value(){V=1/Math.Atan(ps[0].V)};
                case "min":
                    return numbers.Numbers.findMin(ps[0],ps[1]);
                case "max":
                    return numbers.Numbers.findMax(ps[0],ps[1]);
                case "sinh":
                    return new numbers.value(){V=Math.Sinh(ps[0].V)};
                case "tanh":
                    return new numbers.value(){V=Math.Tanh(ps[0].V)};
                case "cosh":
                    return new numbers.value(){V=Math.Cosh(ps[0].V)};
                case "arccosh":
                    return new numbers.value(){V=Math.Acosh(ps[0].V)};
                case "arcsinh":
                    return new numbers.value(){V=Math.Asinh(ps[0].V)};
                case "arctanh":
                    return new numbers.value(){V=Math.Atanh(ps[0].V)};
                case "coth":
                    return new numbers.value(){V=1/Math.Tanh(ps[0].V)};;
                case "arccoth":
                    return new numbers.value(){V=1/Math.Atanh(ps[0].V)};
                case "sqrt":
                    return new numbers.value(){V=Math.Sqrt(ps[0].V)};
                case "floor":
                    return new numbers.value(){V=Math.Floor(ps[0].V)};
                case "ceiling":
                    return new numbers.value(){V=Math.Ceiling(ps[0].V)};
                case "to_ascii":
                    throw new NotImplementedException();
                case "to_character":
                    throw new NotImplementedException();
                case "length_of":
                    throw new NotImplementedException();
                case "abs":
                    throw new NotImplementedException();
            }
            throw new NotImplementedException();
        }
    }
    public class Plugin_Func_Expon : Id_Expon
    {
        public Parameter_List? parameters;

        public override numbers.value Execute(Lexer l){
            throw new NotImplementedException();
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
            return numbers.Numbers.multValues(first, second);

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

    }
    public class Boolean0 : Boolean_Parseable
    {
        public Token_Type kind; // must be a Boolean_Func0_Type
        public Boolean0(Token_Type kind)
        {
            this.kind = kind;
        }

        public override bool Execute(Lexer l){
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
    }
    public class Boolean_Plugin : Boolean_Parseable
    {
        public Token? id;
        public Parameter_List? parameters;

        public override bool Execute(Lexer l){
            throw new NotImplementedException();
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

        private int getLen(){
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
            int spot = 0;
            if(parameter == null){
                return ans;
            } else if(next == null){
                ans[spot] = parameter.Execute(l);
            }else{
                ans[spot] = parameter.Execute(l);
                while(spot != getLen()-1){
                    spot++;
                    ans[spot] = next.Execute(l)[spot-1];
                }
                
            }
            return ans;
        }

    }
}
