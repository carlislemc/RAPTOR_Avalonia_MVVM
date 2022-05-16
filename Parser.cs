﻿using parse_tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAPTOR_Avalonia_MVVM
{
    public class Syntax_Error : Exception
    {
        public Syntax_Error(string message) : base(message) { }
    }
    public class Parser
    {
        private Lexer lexer;
        public Parser(Lexer l)
        {
            this.lexer = l;
        }
        public static Token Current_Token;
        public static Token Get_Current_Token()
        {
            return Current_Token;
        }
        public static void Raise_Exception(Token t, string message)
        {
            Current_Token = t;
            throw new Syntax_Error(message);
        }
        public Lhs Parse_Lhs(Token ident)
        {
            if (Plugins.Is_Function(lexer.Get_Text(ident.start, ident.finish))) {
                Raise_Exception(ident, "Invalid name, already defined as a function");
            }
            Token t;
            Expression e, e2;
            Array_Ref_Lhs lhsref;
            t = lexer.Get_Token();
            if (t.kind==Token_Type.Left_Bracket)
            {
                e = Parse_Expression();
                Expression.Fix_Associativity(ref e);
                t = lexer.Get_Token();
                if (t.kind == Token_Type.Comma)
                {
                    e2 = Parse_Expression();
                    Expression.Fix_Associativity(ref e2);
                    lhsref = new Array_2D_Ref_Lhs(ident, e, e2);
                    t = lexer.Get_Token();
                }
                else
                {
                    lhsref = new Array_Ref_Lhs(ident, e);
                }
                if (t.kind!=Token_Type.Right_Bracket)
                {
                    Raise_Exception(t, "missing ]");
                }
                return lhsref;
            }
            else
            {
                lexer.Unget_Token(t);
                return new Id_Lhs(ident);           
            }
        }
        public Lsuffix Parse_Lsuffix()
        {
            Token t = lexer.Get_Token();
            if (t.kind == Token_Type.Dot)
            {
                if (!raptor.Runtime.isObjectOriented())
                {
                    Raise_Exception(t, "Can't use '.' in variable name");
                }
                t = lexer.Get_Token();
                if (t.kind != Token_Type.Id)
                {
                    Raise_Exception(t, "expected name");
                }
                return new Full_Lsuffix(Parse_Lhs(t), Parse_Lsuffix());
            }
            else
            {
                lexer.Unget_Token(t);
                return new Empty_Lsuffix();
            }
        }
        // Assignment => Lhs LSuffix[=|:=] Expression
        public assignment Parse_Assignment()
        {
            expr_assignment result = new expr_assignment();
            Expression e;
            Token t = lexer.Get_Token();
            if (t.kind!=Token_Type.Id)
            {
                Raise_Exception(t, "assignment must begin with a variable");
            }
            result.lhs = Parse_Lhs(t);
            result.lsuffix = Parse_Lsuffix();
            t = lexer.Get_Token();
            if (t.kind!=Token_Type.Equal && t.kind!=Token_Type.Colon_Equal)
            {
                Raise_Exception(t, "can only set 1 variable, found: " + t.kind.ToString());
            }
            e = Parse_Expression();
            Expression.Fix_Associativity(ref e);
            result.expr_part = e;
            return result;
        }

        int Count_Parameters(Parameter_List l)
        {
            if (l==null)
            {
                return 0;
            }
            else
            {
                return 1 + Count_Parameters(l.next);
            }
        }

        // Rhs => id[\[Expression[, Expression]\]] | id(Expression_List)
        public Rhs Parse_Rhs(Token ident)
        {
            Token t = lexer.Get_Token();
            Expression e, e2;
            Array_Ref_Rhs reference;
            if (t.kind==Token_Type.Left_Paren)
            {
                if (!raptor.Runtime.isObjectOriented())
                {
                    Raise_Exception(t, lexer.Get_Text(ident.start, ident.finish) +
                        " is not a function.");
                }
                Rhs_Method_Call result = new Rhs_Method_Call();
                result.id = ident;
                result.parameters = Parse_Parameter_List(lexer.Get_Text(ident.start, ident.finish), true);
                t = lexer.Get_Token();
                if (t.kind!=Token_Type.Right_Paren)
                {
                    Raise_Exception(t, "missing right parenthesis");
                }
                return result;
            }
            else if (t.kind==Token_Type.Left_Bracket)
            {
                e = Parse_Expression();
                Expression.Fix_Associativity(ref e);
                t = lexer.Get_Token();
                if (t.kind==Token_Type.Comma)
                {
                    e2 = Parse_Expression();
                    Expression.Fix_Associativity(ref e2);
                    reference = new Array_Ref_2D_Rhs();
                    reference.id = ident;
                    reference.reference = e;
                    ((Array_Ref_2D_Rhs)reference).reference2 = e2;
                    t = lexer.Get_Token();
                }
                else
                {
                    reference = new Array_Ref_Rhs();
                    reference.id = ident;
                    reference.reference = e;
                }
                if (t.kind!=Token_Type.Right_Bracket)
                {
                    Raise_Exception(t, "missing ]");
                    return null; // shut up compiler
                }
                return reference;
            }
            else
            {
                lexer.Unget_Token(t);
                return new Id_Rhs(ident);
            }
        }

        // Rsuffix => . Rhs Rsuffix | lambda
        Rsuffix Parse_Rsuffix()
        {
            Token t = lexer.Get_Token();
            if (t.kind==Token_Type.Dot)
            {
                if (!raptor.Runtime.isObjectOriented())
                {
                    Raise_Exception(t, "can't use '.' in variable name");
                }
                t = lexer.Get_Token();
                if (t.kind!=Token_Type.Id)
                {
                    Raise_Exception(t, "expected name");
                }
                return new Full_Rsuffix(Parse_Rhs(t), Parse_Rsuffix());
            }
            else
            {
                lexer.Unget_Token(t);
                return new Empty_Rsuffix();
            }
        }

        // Expon => Rhs RSuffix | num | (Expression)
        //          | new id | new id(Expression_List)
        //          | func_id(Expression_List) | func_id0 | -Expon
        //          | plugin_func_id(Expression_List)
        public Expon Parse_Expon()
        {
            Token t = lexer.Get_Token();
            if (t.kind==Token_Type.Id)
            {
                Token ident = t;
                t = lexer.Get_Token();
                string func_name = lexer.Get_Text(ident.start, ident.finish);
                if (t.kind==Token_Type.Left_Paren)
                {
                    if (Plugins.Is_Function(func_name))
                    {
                        Plugin_Func_Expon _result = new Plugin_Func_Expon();
                        int count, correct_count;
                        _result.id = ident;
                        _result.parameters = Parse_Parameter_List(func_name, false);
                        count = Count_Parameters(_result.parameters);
                        correct_count = Plugins.Parameter_Count(func_name);
                        if (count!=correct_count)
                        {
                            Raise_Exception(t, func_name + " should have " + correct_count +
                                "parameters.");
                        }
                        t = lexer.Get_Token();
                        if (t.kind!=Token_Type.Right_Paren)
                        {
                            Raise_Exception(t, "missing right parenthesis");
                        }
                        return _result;
                    }
                }
                lexer.Unget_Token(t);
                if (Plugins.Is_Function(func_name))
                {
                    if (Plugins.Parameter_Count(func_name)!=0)
                    {
                        Raise_Exception(ident, func_name + " should have " + 
                            Plugins.Parameter_Count(func_name) + " parameters.");
                    }
                    Plugin_Func_Expon _result = new Plugin_Func_Expon();
                    _result.id = ident;
                    _result.parameters = null;
                    return _result;
                }
                Rhs_Expon result;
                result = new Rhs_Expon(Parse_Rhs(ident), Parse_Rsuffix);
                if (!result.is_method_call())
                {
                    return result;
                }
                else
                {
                    /*
                     return new Expon_Stub'(
                        Component => Expression_Object,
                        Index => raptor.ParseHelpers.addExpression(Expression_Object, toObject(Result)),
                        Expon_Parse_Tree => expon_pointer(Result));
                     */
                    Raise_Exception(t, "unimplemented");
                }
            }
            else if (t.kind==Token_Type.New)
            {
                t = lexer.Get_Token();
                if (t.kind != Token_Type.Id)
                {
                    Raise_Exception(t, "Expected name of class, found: " +
                        lexer.Get_Text(t.start, t.finish));
                }
                Raise_Exception(t,"OO mode not yet implemented");
                /*          -- mcc: TODO check that this is really a class
               -- this will require modifying the stuff for class deletion/renaming also
              declare
                Ident : Token_Pointer := T;
                Result : Expon_Pointer;
                Retval : Expon_Pointer;
                function to_object is new ada.Unchecked_Conversion(expon_Pointer, mssyst.object.ref);
              begin
                 T := Get_Token;
                 if T.Kind = Left_Paren then
                    Result := new Class_Constructor_Expon'(Ident,Parse_Parameter_List(
                    Lexer.Get_Text(Ident.Start..Ident.Finish),True));
                    T := Get_Token;
                    if T.Kind /= Right_Paren then
                        Raise_Exception(T,
                     "missing right parenthesis");
                    end if;
                else
                    Unget_Token(T);
                    Result := new Class_Expon;
                    Class_Expon_Pointer(Result).Id := Ident;
                end if;
                Retval := new Expon_Stub'(
                    Component => Expression_Object,
                    Index => Raptor.ParseHelpers.AddExpression(Expression_Object, to_object(Result)),
                    Expon_Parse_Tree => Result);
                return Retval;
            end;
*/
            }
            else if (t.kind==Token_Type.Character)
            {
                return new Character_Expon(t);
            }
            else if (t.kind==Token_Type.String)
            {
                return new String_Expon(t);
            }
            else if (t.kind==Token_Type.Number)
            {
                return new Number_Expon(t);
            }
            else if (t.kind==Token_Type.Left_Paren)
            {
                Expression e = Parse_Expression();
                Expression.Fix_Associativity(ref e);
                t = lexer.Get_Token();
                if (t.kind!=Token_Type.Right_Paren)
                {
                    Raise_Exception(t, "missing right parenthesis");
                }
                return new Paren_Expon(e);
            }
            else if (Lexer.isFunc_Id0(t.kind))
            {
                return new Func0_Expon(t);
            }
            else if (t.kind==Token_Type.Minus)
            {
                return new Negative_Expon(Parse_Expon());
            }
            else if (Lexer.isFunc_Token_Type(t.kind))
            {
                Token_Type func_kind = t.kind;
                Func_Expon result = new Func_Expon();
                int count;
                result.id = t;
                t = lexer.Get_Token();
                if (t.kind!=Token_Type.Left_Paren)
                {
                    Raise_Exception(t, "missing left parenthesis");
                }
                string func_name = lexer.Get_Text(result.id.start, result.id.finish);
                result.parameters = Parse_Parameter_List(func_name, false);
                count = Count_Parameters(result.parameters);
                if (Lexer.isFunc_Id0(func_kind)) { 
                    if (count!=0)
                    {
                        Raise_Exception(t, func_name + " should not have parameters.");
                    }
                }
                else if (Lexer.isFunc_Id1(func_kind)||func_kind==Token_Type.Load_Bitmap)
                {
                    if (count!=1)
                    {
                        Raise_Exception(t, func_name + " should have 1 parameter, not " + count);
                    }
                }
                else if (Lexer.isFunc_Id1or2(func_kind))
                {
                    if (count!=1 && count !=2)
                    {
                        Raise_Exception(t, func_name + " should have 1 or 2 parameters, not " + count);
                    }
                }
                else if (Lexer.isFunc_Id2or3(func_kind))
                {
                    if (count != 2 && count != 3)
                    {
                        Raise_Exception(t, func_name + " should have 2 or 3 parameters, not " + count);
                    }
                }
                else if (Lexer.isOther_Func_Id2(func_kind) || Lexer.isGraph_Func_Id2(func_kind))
                {
                    if (count != 2)
                    {
                        Raise_Exception(t, func_name + " should have two parameters, not " + count);
                    }
                }
                else if (Lexer.isFunc_Id3(func_kind))
                {
                    if (count != 3)
                    {
                        Raise_Exception(t, func_name + " should have three parameters, not " + count);
                    }
                }
                t = lexer.Get_Token();
                if (t.kind!=Token_Type.Right_Paren)
                {
                    Raise_Exception(t, "missing right parenthesis");
                }
                return result;
            }
            else
            {
                Raise_Exception(t, "expected number or variable, found " + t.kind.ToString());
            }
            Raise_Exception(t, "should never reach this");
            return null; // shut up compiler
        }

        // Mult => Expon ^ Mult | Expon
        public Mult Parse_Mult()
        {
            Expon e = Parse_Expon();
            Token t = lexer.Get_Token();
            if (t.kind == Token_Type.Exponent)
            {
                Expon_Mult result = new Expon_Mult();
                result.left = e;
                result.right = Parse_Mult();
                return result;
            }
            else
            {
                lexer.Unget_Token(t);
                return new Mult(e);
            }
        }
        // Add => Mult* Add | Mult / Add | Mult mod Add | Mult
        public Add Parse_Add()
        {
            Mult m = Parse_Mult();
            Mult.Fix_Associativity(ref m);
            Token t = lexer.Get_Token();
            if (t.kind==Token_Type.Times)
            {
                return new Mult_Add(m, Parse_Add());
            }
            else if (t.kind == Token_Type.Divide)
            {
                return new Div_Add(m, Parse_Add());
            }
            else if (t.kind == Token_Type.Mod)
            {
                return new Mod_Add(m, Parse_Add());
            }
            else if (t.kind == Token_Type.Rem)
            {
                return new Rem_Add(m, Parse_Add());
            }
            else
            {
                lexer.Unget_Token(t);
                return new Add(m);
            }
        }

        // Expression => Add + Expression | Add - Expression | Add
        public Expression Parse_Expression()
        {
            Add a;
            Token t;
            a = Parse_Add();
            Add.Fix_Associativity(ref a);
            t = lexer.Get_Token();
            if (t.kind==Token_Type.Plus)
            {
                Add_Expression result = new Add_Expression(a);
                result.right = Parse_Expression();
                return result;
            }
            else if (t.kind== Token_Type.Minus)
            {
                Minus_Expression result = new Minus_Expression(a);
                result.right = Parse_Expression();
                return result;
            }
            else
            {
                lexer.Unget_Token(t);
                return new Expression(a);
            }
        }

        // Relation => Expression > Expression | >=,<,<=,=
        public Relation Parse_Relation()
        {
            Relation result = new Relation();
            Token t = lexer.Get_Token();
            if (t.kind == Token_Type.Wait_For_Key)
            {
                Raise_Exception(t, "Did you mean Key_Hit?");
                return null;
            }
            else if (t.kind == Token_Type.Wait_For_Mouse_Button)
            {
                Raise_Exception(t, "Did you mean Mouse_Button_Pressed?");
                return null;
            }
            else if (Lexer.isProc_Token_Type(t.kind))
            {
                Raise_Exception(t, t.kind.ToString() + " should be in call");
                return null;
            }
            lexer.Unget_Token(t);
            result.left = Parse_Expression();
            Expression.Fix_Associativity(ref result.left);
            t = lexer.Get_Token();
            if (t.kind == Token_Type.Greater_Equal ||
                t.kind == Token_Type.Greater ||
                t.kind == Token_Type.Less_Equal ||
                t.kind == Token_Type.Equal ||
                t.kind == Token_Type.Not_Equal ||
                t.kind == Token_Type.Less)
            {
                result.kind = t.kind;
            }
            else
            {
                Raise_Exception(t,
                   "Expected relational operator (<,>,=,...) not " +
                      t.kind.ToString());
            }
            result.right = Parse_Expression();
            Expression.Fix_Associativity(ref result.right);
            t = lexer.Get_Token();
            if (t.kind == Token_Type.Greater_Equal ||
                t.kind == Token_Type.Greater ||
                t.kind == Token_Type.Less_Equal ||
                t.kind == Token_Type.Equal ||
                t.kind == Token_Type.Not_Equal ||
                t.kind == Token_Type.Less)
            {

                if (result.kind == Token_Type.Less &&
                    t.kind == Token_Type.Less)
                {
                    Raise_Exception(t,
                        "X<Y<Z should be X<Y and Y<Z");
                }
                else if (result.kind == Token_Type.Less &&
                    t.kind == Token_Type.Less_Equal)
                {
                    Raise_Exception(t,
                        "X<Y<=Z should be X<Y and Y<=Z");
                }
                else if (result.kind == Token_Type.Less_Equal &&
                    t.kind == Token_Type.Less)
                {
                    Raise_Exception(t,
                        "X<=Y<Z should be X<=Y and Y<Z");
                }
                else if (result.kind == Token_Type.Less_Equal &&
                    t.kind == Token_Type.Less_Equal)
                {
                    Raise_Exception(t,
                        "X<=Y<=Z should be X<=Y and Y<=Z");
                }
                else if (result.kind == Token_Type.Greater &&
                    t.kind == Token_Type.Greater)
                {
                    Raise_Exception(t,
                        "X>Y>Z should be X>Y and Y>Z");
                }
                else if (result.kind == Token_Type.Greater &&
                    t.kind == Token_Type.Greater_Equal)
                {
                    Raise_Exception(t,
                         "X>Y>=Z should be X>Y and Y>=Z");
                }
                else if (result.kind == Token_Type.Greater_Equal &&
                    t.kind == Token_Type.Greater)
                {
                    Raise_Exception(t,
                         "X>=Y>Z should be X>=Y and Y>Z");
                }
                else if (result.kind == Token_Type.Greater_Equal &&
                    t.kind == Token_Type.Greater_Equal)
                {
                    Raise_Exception(t,
                         "X>=Y>=Z should be X>=Y and Y>=Z");
                }
            }
            lexer.Unget_Token(t);
            return result;
        }

        //Boolean2 => [ (BE) | Relation | Boolean_Func]
        // [and BE2 | lambda]
        public Boolean2 Parse_Boolean2()
        {
            Boolean_Parseable left;
            bool negated;
            Expression e;
            Token_Type kind;
            Token t = lexer.Get_Token();
            if (t.kind==Token_Type.Not)
            {
                t = lexer.Get_Token();
                negated = true;
            }
            else
            {
                negated = false;
            }
            if (t.kind==Token_Type.Left_Paren)
            {
                // ok, so here we break being an LL(1) grammar
                // in favor of not having a type checker
                // I first guess that the paren is a boolean expression
                // (thus not allowing paren_expon to generate a boolean)
                //  but if wrong, try again as just a relation
                try
                {
                    left = Parse_Boolean_Expression();
                    t = lexer.Get_Token();
                    if (t.kind!=Token_Type.Right_Paren)
                    {
                        Raise_Exception(t, "Expected right parenthesis");
                    }
                }
                catch (Syntax_Error exc)
                {
                    lexer.Rewind(T);
                    lexer.Unget_Token(T);
                    left = Parse_Relation();
                }
            }
            else if (Lexer.isBoolean_Func0_Type(t.kind))
            {
                left = new Boolean0(t.kind);
            }
            else if (t.kind == Token_Type.True)
            {
                left = new Boolean_Constant(true);
            }
            else if (t.kind == Token_Type.False)
            {
                left = new Boolean_Constant(false);
            }
            else if (Lexer.isBoolean_Reflection_Type(t.kind))
            {
                kind = t.kind;
                t = lexer.Get_Token();
                if (t.kind!=Token_Type.Left_Paren)
                {
                    Raise_Exception(t, "Expected left parenthesis");
                }
                t = lexer.Get_Token();
                if (t.kind!=Token_Type.Id)
                {
                    Raise_Exception(t, "Must have variable name here");
                }
                left = new Boolean_Reflection(kind, t);
                t = lexer.Get_Token();
                if (t.kind != Token_Type.Right_Paren)
                {
                    Raise_Exception(t, "Expected right parenthesis");
                }
            }
            else if (Lexer.isBoolean_Func1_Type(t.kind))
            {
                kind = t.kind;
                t = lexer.Get_Token();
                if (t.kind!=Token_Type.Left_Paren)
                {
                    Raise_Exception(t, "Expected left parenthesis");
                }
                e = Parse_Expression();
                Expression.Fix_Associativity(ref e);
                left = new Boolean1(kind, e);
                t = lexer.Get_Token();
                if (t.kind != Token_Type.Right_Paren)
                {
                    Raise_Exception(t, "Expected right parenthesis");
                }
            }
            else if (t.kind==Token_Type.Id)
            {
                if (Plugins.Is_Boolean_Function(lexer.Get_Text(t.start,t.finish)))
                {
                    Boolean_Plugin result = new Boolean_Plugin();
                    int count, correct_count;
                    result.id = t;
                    t = lexer.Get_Token();
                    if (t.kind != Token_Type.Left_Paren)
                    {
                        Raise_Exception(t, "Expected left parenthesis");
                    }
                    result.parameters = Parse_Parameter_List(
                        lexer.Get_Text(result.id.start, result.id.finish), false);
                    count = Count_Parameters(result.parameters);
                    correct_count = Plugins.Parameter_Count(
                        lexer.Get_Text(result.id.start,result.id.finish));
                    if (count!=correct_count)
                    {
                        Raise_Exception(t,
                            lexer.Get_Text(result.id.start, result.id.finish) +
                            " should have" + correct_count +
                            " parameters.");
                    }
                    t = lexer.Get_Token();
                    if (t.kind != Token_Type.Right_Paren)
                    {
                        Raise_Exception(t, "Expected right parenthesis");
                    }
                    left = result;
                }
                else
                {
                    lexer.Unget_Token(t);
                    left = Parse_Relation();
                }
            }
            else
            {
                lexer.Unget_Token(t);
                left = Parse_Relation();
            }
            t = lexer.Get_Token();
            if (t.kind == Token_Type.And)
            {
                return new And_Boolean2(negated, left, Parse_Boolean2());
            }
            else
            {
                lexer.Unget_Token(t);
                return new Boolean2(negated, left);
            }
        }

        // Boolean_Expression => Boolean2 or Boolean_Expression |
        //                       Boolean2 xor Boolean_Expression | Boolean2
        public Boolean_Expression Parse_Boolean_Expression()
        {
            Boolean2 left=Parse_Boolean2();
            Token t = lexer.Get_Token();
            if (t.kind==Token_Type.Or)
            {
                return new Or_Boolean(left, Parse_Boolean_Expression());
            }
            else if (t.kind==Token_Type.Xor)
            {
                return new Xor_Boolean(left, Parse_Boolean_Expression());
            }
            else
            {
                lexer.Unget_Token(t);
                return new Boolean_Expression(left);
            }
        }





    }

}
