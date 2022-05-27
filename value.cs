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
        public static int  integer_of(value v)
        {
            return (int) (v.V);
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

        internal static value make_value__5(string s)
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
        internal static value make_value__4(bool b)
        {
            if(b){
                return new value { V = 1.0, C = ' ', S = null, Kind = Value_Kind.Number_Kind, Object = null };
            }else{
                return new value { V = 0.0, C = ' ', S = null, Kind = Value_Kind.Number_Kind, Object = null };
            }
            
        }
        internal static value make_value__3(int index)
        {
            return new value { V = index, C = ' ', S = null, Kind = Value_Kind.Number_Kind, Object = null };
        }

        internal static value make_value__2(double v)
        {
            return new value { V = v, C = ' ', S = null, Kind = Value_Kind.Number_Kind, Object = null };
        }

        internal static value? make_object_value(object v)
        {
            return new value { V = 0.0, C = ' ', S = null, Kind = Value_Kind.Object_Kind, Object = v };
        }

        internal static bool Oeq(value variable_Value1, value variable_Value2)
        {
            throw new NotImplementedException();
        }

        internal static int length_of(value variable_Value)
        {
            throw new NotImplementedException();
        }

        internal static value make_string_value(string new_string)
        {
            return new value { V = 0.0, C = ' ', S = new_string, Kind = Value_Kind.String_Kind, Object = null };
            //throw new NotImplementedException();
        }

        public static bool is_character(value f)
        {
            return f.Kind == Value_Kind.Character_Kind;
        }
    }
}
