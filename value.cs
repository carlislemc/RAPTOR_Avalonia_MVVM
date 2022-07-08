using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace numbers
{
    public enum Value_Kind
    {
        Number_Kind, String_Kind, Character_Kind, Object_Kind,
        Ref_1D, Ref_2D
    };
    public class value
    {
        public double V;
        public string? S;
        public char C;
        public object? Object;
        public Value_Kind Kind;

        public static bool operator ==(value first, value second){
            if(first.Kind != second.Kind){
                return false;
            }
            switch(first.Kind){
                case Value_Kind.Number_Kind:
                    return first.V == second.V;
                case Value_Kind.String_Kind:
                    return first.S == second.S;
                case Value_Kind.Character_Kind:
                    return first.C == second.C;
            }
            return false;
        }

        public static bool operator !=(value first, value second){
            if(first.Kind != second.Kind){
                return false;
            }
            switch(first.Kind){
                case Value_Kind.Number_Kind:
                    return first.V != second.V;
                case Value_Kind.String_Kind:
                    return first.S != second.S;
                case Value_Kind.Character_Kind:
                    return first.C != second.C;
            }
            return false;
        }

        public static bool operator >(value first, value second){
            if(first.Kind != second.Kind){
                return false;
            }
            switch(first.Kind){
                case Value_Kind.Number_Kind:
                    return first.V > second.V;
                case Value_Kind.String_Kind:
                    return first.S.CompareTo(second.S) > 0;
                case Value_Kind.Character_Kind:
                    return first.C > second.C;
            }
            return false;
        }

        public static bool operator <(value first, value second){
            if(first.Kind != second.Kind){
                return false;
            }
            switch(first.Kind){
                case Value_Kind.Number_Kind:
                    return first.V < second.V;
                case Value_Kind.String_Kind:
                    return first.S.CompareTo(second.S) < 0;
                case Value_Kind.Character_Kind:
                    return first.C < second.C;
            }
            return false;
        }

        public static bool operator >=(value first, value second){
            if(first.Kind != second.Kind){
                return false;
            }
            switch(first.Kind){
                case Value_Kind.Number_Kind:
                    return first.V >= second.V;
                case Value_Kind.String_Kind:
                    return first.S.CompareTo(second.S) >= 0;
                case Value_Kind.Character_Kind:
                    return first.C >= second.C;
            }
            return false;
        }

        public static bool operator <=(value first, value second){
            if(first.Kind != second.Kind){
                return false;
            }
            switch(first.Kind){
                case Value_Kind.Number_Kind:
                    return first.V <= second.V;
                case Value_Kind.String_Kind:
                    return first.S.CompareTo(second.S) <= 0;
                case Value_Kind.Character_Kind:
                    return first.C <= second.C;
            }
            return false;
        }


        public numbers.value _deep_clone()
        {
            switch (Kind)
            {
                case Value_Kind.Number_Kind:
                    return new numbers.value() {Kind = Value_Kind.Number_Kind, V = this.V };
                case Value_Kind.String_Kind:
                    return new numbers.value() { Kind = Value_Kind.String_Kind, S = this.S };
                case Value_Kind.Character_Kind:
                    return new numbers.value() { Kind = Value_Kind.Character_Kind, C = this.C };
                case Value_Kind.Ref_1D:
                    throw new NotImplementedException();
                case Value_Kind.Ref_2D:
                    throw new NotImplementedException();
            }
            throw new NotImplementedException();
        }

    }
    public class Numbers
    {
        private const int DEFAULT_PRECISION = 4;
        private static bool Precision_Set = false;
        private static int Precision = DEFAULT_PRECISION;
        static void Set_Precision(int i)
        {
            if (i<0)
            {
                Precision_Set = false;
            }
            else
            {
                Precision_Set = true;
                Precision = i;
            }
        }
        public static value Zero = new value { V = 0.0, C = ' ', S = null, Kind = Value_Kind.Number_Kind, Object = null };
        public static value One = new value { V = 1.0, C = ' ', S = null, Kind = Value_Kind.Number_Kind, Object = null };
        public static value Pi = new value { V = Math.PI, C = ' ', S = null, Kind = Value_Kind.Number_Kind, Object = null };
        public static value E = new value { V = Math.E, C = ' ', S = null, Kind = Value_Kind.Number_Kind, Object = null };
        public static value Two_Pi = new value { V = 2.0*Math.PI, C = ' ', S = null, Kind = Value_Kind.Number_Kind, Object = null };
        public static value Null_Ptr = new value { V = 0.0, C = ' ', S = null, Kind = Value_Kind.Object_Kind, Object = null };
        public static object? object_of(value v)
        {
            return v.Object;
        }
        public static int integer_of(value v)
        {
            if(v.Kind == Value_Kind.Number_Kind)
            {
                return (int)(v.V);
            }
            else
            {
                return (int)(v.C);
            }
            
        }
        public static int character_of(value v)
        {
            return (char)(v.C);
        }
        public static string string_of(value v)
        {
            return (v.S);
        }
        public static double long_float_of(value v)
        {
            return (v.V);
        }
        public static bool is_number(value v)
        {
            return v.Kind == Value_Kind.Number_Kind;
        }
        public static bool is_string(value v)
        {
            return v.Kind == Value_Kind.String_Kind;
        }
        public static bool is_integer (value v)
        {
            double Close_Enough = 0.000000001;
            if (v.Kind==Value_Kind.Number_Kind)
            {
                return Math.Abs(((int) v.V)-v.V)<Close_Enough;
            }
            else
            {
                return false;
            }
        }
        public static string number_string(value v)
        {
            if (is_integer(v) && !Precision_Set)
            {
                return Convert.ToString(((int) (v.V+0.1)));
            }
            else
            {
                return Convert.ToString(v.V);
            }
        }
        public static value make_2d_ref(object o)
        {
            return new value { V = 0.0, C = ' ', S = null, Kind = Value_Kind.Ref_2D, Object = o };
        }
        public static value make_1d_ref(object o)
        {
            return new value { V = 0.0, C = ' ', S = null, Kind = Value_Kind.Ref_1D, Object = o };
        }
        public static string object_image(value v)
        {
            return "[" + v.Object.GetHashCode() + "]";
        }
        public static string msstring_view_image(value v)
        {
            switch(v.Kind)
            {
                case Value_Kind.Number_Kind:
                    return number_string(v);
                case Value_Kind.String_Kind:
                    return "\"" + v.S + "\"";
                case Value_Kind.Character_Kind:
                    return "'" + v.C + "'";
                case Value_Kind.Object_Kind:
                case Value_Kind.Ref_1D:
                case Value_Kind.Ref_2D:
                    return object_image(v);
                default:
                    throw new Exception("bad kind");
            }
        }

        public static value make_value__6(string s)
        {
            try
            {
                if (s.Contains("."))
                {
                    throw new Exception();
                }
                numbers.value ans = new numbers.value();
                ans.V = Convert.ToInt32(s);
                ans.Kind = Value_Kind.Number_Kind;
                return ans;
            }
            catch(Exception e){}

            try
            {
                numbers.value ans = new numbers.value();
                ans.V = Convert.ToDouble(s);
                ans.Kind = Value_Kind.Number_Kind;
                return ans;
            }
            catch (Exception e) { }

            try
            {
                numbers.value ans = new numbers.value();
                ans.S = s;
                ans.Kind = Value_Kind.String_Kind;
                return ans;
            }
            catch (Exception e) { }

            throw new Exception("Bad string");
        }

        public static value make_value__5(string s)
        {
            if(s.Contains(".")){
                numbers.value ans = new numbers.value();
                ans.V = Convert.ToDouble(s);
                ans.Kind = Value_Kind.Number_Kind;
                return ans;
            }else{
                numbers.value ans = new numbers.value();
                ans.V = Convert.ToInt32(s);
                ans.Kind = Value_Kind.Number_Kind;
                return ans;
            }
        }
        public static value make_value__4(bool b)
        {
            if(b){
                return new value { V = 1.0, C = ' ', S = null, Kind = Value_Kind.Number_Kind, Object = null };
            }else{
                return new value { V = 0.0, C = ' ', S = null, Kind = Value_Kind.Number_Kind, Object = null };
            }
            
        }
        public static value make_value__3(int index)
        {
            return new value { V = index, C = ' ', S = null, Kind = Value_Kind.Number_Kind, Object = null };
        }

        public static value make_value__2(double v)
        {
            return new value { V = v, C = ' ', S = null, Kind = Value_Kind.Number_Kind, Object = null };
        }

        public static value? make_object_value(object v)
        {
            return new value { V = 0.0, C = ' ', S = null, Kind = Value_Kind.Object_Kind, Object = v };
        }

        public static bool Oeq(value first, value second)
        {
            if (first.Kind != second.Kind)
            {
                return false;
            }
            switch (first.Kind)
            {
                case Value_Kind.Number_Kind:
                    return first.V == second.V;
                case Value_Kind.String_Kind:
                    return first.S == second.S;
                case Value_Kind.Character_Kind:
                    return first.C == second.C;
            }
            return false;
        }

        public static int length_of(value variable_Value)
        {
            throw new NotImplementedException();
        }

        public static value make_string_value(string new_string)
        {
            return new value { V = 0.0, C = ' ', S = new_string, Kind = Value_Kind.String_Kind, Object = null };
            //throw new NotImplementedException();
        }

        public static value make_character_value(char new_char)
        {
            return new value { V = 0.0, C = new_char, S = "", Kind = Value_Kind.Character_Kind, Object = null };
            //throw new NotImplementedException();
        }


        public static bool is_character(value f)
        {
            return f.Kind == Value_Kind.Character_Kind;
        }

        public static numbers.value addValues(numbers.value first, numbers.value second){
            numbers.value ans = new numbers.value();
            if(first.Kind == numbers.Value_Kind.Number_Kind && second.Kind == numbers.Value_Kind.Number_Kind){
                ans = new numbers.value() {V=first.V + second.V};
            } else if(first.Kind == numbers.Value_Kind.String_Kind && second.Kind == numbers.Value_Kind.String_Kind){
                ans = new numbers.value() {Kind=numbers.Value_Kind.String_Kind, S=first.S.Replace("\"","") + second.S.Replace("\"","")};
            } else if(first.Kind == numbers.Value_Kind.Character_Kind && second.Kind == numbers.Value_Kind.Character_Kind){
                ans = new numbers.value() {V=first.C + second.C};
            }else if(first.Kind == numbers.Value_Kind.String_Kind && second.Kind == numbers.Value_Kind.Number_Kind){
                ans = new numbers.value() {Kind=numbers.Value_Kind.String_Kind, S=first.S.Replace("\"","") + second.V};
            }else if(first.Kind == numbers.Value_Kind.Number_Kind && second.Kind == numbers.Value_Kind.String_Kind){
                ans = new numbers.value() {Kind=numbers.Value_Kind.String_Kind, S=first.V + second.S.Replace("\"","")};
            }else if(first.Kind == numbers.Value_Kind.Number_Kind && second.Kind == numbers.Value_Kind.Character_Kind && is_integer(first)){
                char c = Convert.ToChar((int)first.V + (int)second.C);
                ans = new numbers.value() {Kind=numbers.Value_Kind.Character_Kind, C=c};
            }else if(first.Kind == numbers.Value_Kind.Character_Kind && second.Kind == numbers.Value_Kind.Number_Kind && is_integer(second)){
                char c = Convert.ToChar((int)first.C + (int)second.V);
                ans = new numbers.value() {Kind=numbers.Value_Kind.Character_Kind, C=c};
            }else if(first.Kind == numbers.Value_Kind.String_Kind && second.Kind == numbers.Value_Kind.Character_Kind){
                ans = new numbers.value() {Kind=numbers.Value_Kind.String_Kind, S=first.S + second.C};
            }else if(first.Kind == numbers.Value_Kind.Character_Kind && second.Kind == numbers.Value_Kind.String_Kind){
                ans = new numbers.value() {Kind=numbers.Value_Kind.String_Kind, S=first.C + second.S};
            }
            else {
                throw new Exception("Cannot add type: [" + first.Kind + "] with type: [" + second.Kind + "]");
            }
            return ans;
        }

        public static numbers.value subValues(numbers.value first, numbers.value second){
            numbers.value ans = new numbers.value();
            if(first.Kind == numbers.Value_Kind.Number_Kind && second.Kind == numbers.Value_Kind.Number_Kind){
                ans = new numbers.value() {V=first.V - second.V};
            } else if(first.Kind == numbers.Value_Kind.Number_Kind && second.Kind == numbers.Value_Kind.Character_Kind){
                ans = new numbers.value() {V=first.V - (int)second.C};
            }else if(first.Kind == numbers.Value_Kind.Character_Kind && second.Kind == numbers.Value_Kind.Number_Kind){
                ans = new numbers.value() {V=(int)first.C - second.V};
            }
            else{
                throw new Exception("Cannot subtract type: [" + first.Kind + "] from type: [" + second.Kind + "]");
            }
            return ans;
        }

        public static numbers.value multValues(numbers.value first, numbers.value second){
            numbers.value ans = new numbers.value();
            if(first.Kind == numbers.Value_Kind.Number_Kind && second.Kind == numbers.Value_Kind.Number_Kind){
                ans = new numbers.value() {V=first.V * second.V};
            } else{
                throw new Exception("Cannot multiply type: [" + first.Kind + "] with type: [" + second.Kind + "]");
            }
            return ans;
        }

        public static numbers.value exponValues(numbers.value first, numbers.value second)
        {
            numbers.value ans = new numbers.value();
            if (first.Kind == numbers.Value_Kind.Number_Kind && second.Kind == numbers.Value_Kind.Number_Kind)
            {
                ans = new numbers.value() { V = Math.Pow(first.V, second.V) };
            }
            else
            {
                throw new Exception("Cannot multiply type: [" + first.Kind + "] with type: [" + second.Kind + "]");
            }
            return ans;
        }

        public static numbers.value divValues(numbers.value first, numbers.value second){
            numbers.value ans = new numbers.value();
            if(first.Kind == numbers.Value_Kind.Number_Kind && second.Kind == numbers.Value_Kind.Number_Kind){
                if(second.V == 0){
                    throw new Exception("Cannot divide by 0!");
                }
                ans = new numbers.value() {V=first.V / second.V};
            } else{
                throw new Exception("Cannot divide type: [" + first.Kind + "] with type: [" + second.Kind + "]");
            }
            return ans;
        }

        public static numbers.value modValues(numbers.value first, numbers.value second){
            numbers.value ans = new numbers.value();
            if(first.Kind == numbers.Value_Kind.Number_Kind && second.Kind == numbers.Value_Kind.Number_Kind){
                ans = new numbers.value() {V=first.V % second.V};
            } else{
                throw new Exception("Cannot mod type: [" + first.Kind + "] with type: [" + second.Kind + "]");
            }
            return ans;
        }

        public static numbers.value remValues(numbers.value first, numbers.value second){
            numbers.value ans = new numbers.value();
            if(first.Kind == numbers.Value_Kind.Number_Kind && second.Kind == numbers.Value_Kind.Number_Kind){
                int temp; 
                Math.DivRem((int)first.V, (int)second.V, out temp);
                ans = new numbers.value() {V=temp};
            } else{
                throw new Exception("Cannot mod type: [" + first.Kind + "] with type: [" + second.Kind + "]");
            }
            return ans;
        }

        public static numbers.value findMax(numbers.value first, numbers.value second){
            numbers.value ans = new numbers.value();
            if(first.Kind == numbers.Value_Kind.Number_Kind && second.Kind == numbers.Value_Kind.Number_Kind){
                ans = Math.Max(first.V, second.V) == first.V ? first : second; 
            } else{
                throw new Exception("Cannot find max of type: [" + first.Kind + "] with type: [" + second.Kind + "]");
            }
            return ans;
        }

        public static numbers.value findMin(numbers.value first, numbers.value second){
            numbers.value ans = new numbers.value();
            if(first.Kind == numbers.Value_Kind.Number_Kind && second.Kind == numbers.Value_Kind.Number_Kind){
                ans = Math.Min(first.V, second.V) == first.V ? first : second; 
            } else{
                throw new Exception("Cannot find max of type: [" + first.Kind + "] with type: [" + second.Kind + "]");
            }
            return ans;
        }

        public static numbers.value Sinh(numbers.value first) {
            if (first.Kind != numbers.Value_Kind.Number_Kind)
            {
                throw new Exception("Cannot find sinh of type: [" + first.Kind + "]");
            }
            return new numbers.value() { Kind = Value_Kind.Number_Kind, V = Math.Sinh(first.V)};
        }

        public static numbers.value Tanh(numbers.value first)
        {
            if (first.Kind != numbers.Value_Kind.Number_Kind)
            {
                throw new Exception("Cannot find tanh of type: [" + first.Kind + "]");
            }
            return new numbers.value() { Kind = Value_Kind.Number_Kind, V = Math.Tanh(first.V) };
        }

        public static numbers.value Cosh(numbers.value first)
        {
            if (first.Kind != numbers.Value_Kind.Number_Kind)
            {
                throw new Exception("Cannot find cosh of type: [" + first.Kind + "]");
            }
            return new numbers.value() { Kind = Value_Kind.Number_Kind, V = Math.Cosh(first.V) };
        }

        public static numbers.value ArcSinh(numbers.value first)
        {
            if (first.Kind != numbers.Value_Kind.Number_Kind)
            {
                throw new Exception("Cannot find arcsinh of type: [" + first.Kind + "]");
            }
            return new numbers.value() { Kind = Value_Kind.Number_Kind, V = Math.Asinh(first.V) };
        }

        public static numbers.value ArcTanh(numbers.value first)
        {
            if (first.Kind != numbers.Value_Kind.Number_Kind)
            {
                throw new Exception("Cannot find arctanh of type: [" + first.Kind + "]");
            }
            return new numbers.value() { Kind = Value_Kind.Number_Kind, V = Math.Atanh(first.V) };
        }

        public static numbers.value ArcCosh(numbers.value first)
        {
            if (first.Kind != numbers.Value_Kind.Number_Kind)
            {
                throw new Exception("Cannot find arccosh of type: [" + first.Kind + "]");
            }
            return new numbers.value() { Kind = Value_Kind.Number_Kind, V = Math.Acosh(first.V) };
        }

        public static numbers.value Coth(numbers.value first)
        {
            if (first.Kind != numbers.Value_Kind.Number_Kind)
            {
                throw new Exception("Cannot find coth of type: [" + first.Kind + "]");
            }
            return new numbers.value() { Kind = Value_Kind.Number_Kind, V = 1/Math.Tanh(first.V) };
        }

        public static numbers.value ArcCoth(numbers.value first)
        {
            if (first.Kind != numbers.Value_Kind.Number_Kind)
            {
                throw new Exception("Cannot find arccoth of type: [" + first.Kind + "]");
            }
            return new numbers.value() { Kind = Value_Kind.Number_Kind, V = 1 / Math.Atanh(first.V) };
        }

        public static numbers.value Sqrt(numbers.value first)
        {
            if (first.Kind != numbers.Value_Kind.Number_Kind)
            {
                throw new Exception("Cannot find sqrt of type: [" + first.Kind + "]");
            }
            return new numbers.value() { Kind = Value_Kind.Number_Kind, V = Math.Sqrt(first.V) };
        }

        public static numbers.value Floor(numbers.value first)
        {
            if (first.Kind != numbers.Value_Kind.Number_Kind)
            {
                throw new Exception("Cannot find floor of type: [" + first.Kind + "]");
            }
            return new numbers.value() { Kind = Value_Kind.Number_Kind, V = Math.Floor(first.V) };
        }

        public static numbers.value Ceiling(numbers.value first)
        {
            if (first.Kind != numbers.Value_Kind.Number_Kind)
            {
                throw new Exception("Cannot find ceiling of type: [" + first.Kind + "]");
            }
            return new numbers.value() { Kind = Value_Kind.Number_Kind, V = Math.Ceiling(first.V) };
        }

        public static numbers.value Abs(numbers.value first)
        {
            if (first.Kind != numbers.Value_Kind.Number_Kind)
            {
                throw new Exception("Cannot find abs of type: [" + first.Kind + "]");
            }
            return new numbers.value() { Kind = Value_Kind.Number_Kind, V = Math.Abs(first.V) };
        }

        public static numbers.value Log(numbers.value first, numbers.value second)
        {
            if (first.Kind != numbers.Value_Kind.Number_Kind || second.Kind != numbers.Value_Kind.Number_Kind)
            {
                throw new Exception("Cannot find log of type: [" + first.Kind + "]");
            }
            return new numbers.value() { Kind = Value_Kind.Number_Kind, V = Math.Log(first.V, second.V) };
        }

        public static numbers.value Sin(numbers.value first)
        {
            if (first.Kind != numbers.Value_Kind.Number_Kind)
            {
                throw new Exception("Cannot find sin of type: [" + first.Kind + "]");
            }
            return new numbers.value() { Kind = Value_Kind.Number_Kind, V = Math.Sin(first.V) };
        }

        public static numbers.value Cos(numbers.value first)
        {
            if (first.Kind != numbers.Value_Kind.Number_Kind)
            {
                throw new Exception("Cannot find cos of type: [" + first.Kind + "]");
            }
            return new numbers.value() { Kind = Value_Kind.Number_Kind, V = Math.Cos(first.V) };
        }

        public static numbers.value Tan(numbers.value first)
        {
            if (first.Kind != numbers.Value_Kind.Number_Kind)
            {
                throw new Exception("Cannot find tan of type: [" + first.Kind + "]");
            }
            return new numbers.value() { Kind = Value_Kind.Number_Kind, V = Math.Tan(first.V) };
        }
        public static numbers.value Cot(numbers.value first)
        {
            if (first.Kind != numbers.Value_Kind.Number_Kind)
            {
                throw new Exception("Cannot find cot of type: [" + first.Kind + "]");
            }
            return new numbers.value() { Kind = Value_Kind.Number_Kind, V = 1 / Math.Tan(first.V) };
        }

        public static numbers.value ArcSin(numbers.value first)
        {
            if (first.Kind != numbers.Value_Kind.Number_Kind)
            {
                throw new Exception("Cannot find arcsin of type: [" + first.Kind + "]");
            }
            return new numbers.value() { Kind = Value_Kind.Number_Kind, V = Math.Asin(first.V) };
        }

        public static numbers.value ArcCos(numbers.value first)
        {
            if (first.Kind != numbers.Value_Kind.Number_Kind)
            {
                throw new Exception("Cannot find arccos of type: [" + first.Kind + "]");
            }
            return new numbers.value() { Kind = Value_Kind.Number_Kind, V = Math.Acos(first.V) };
        }

        public static numbers.value ArcTan(numbers.value first)
        {
            if (first.Kind != numbers.Value_Kind.Number_Kind)
            {
                throw new Exception("Cannot find arctan of type: [" + first.Kind + "]");
            }
            return new numbers.value() { Kind = Value_Kind.Number_Kind, V = Math.Atan(first.V) };
        }

        public static numbers.value ArcCot(numbers.value first)
        {
            if (first.Kind != numbers.Value_Kind.Number_Kind)
            {
                throw new Exception("Cannot find arccot of type: [" + first.Kind + "]");
            }
            return new numbers.value() { Kind = Value_Kind.Number_Kind, V = 1 / Math.Atan(first.V) };
        }

    }
}
