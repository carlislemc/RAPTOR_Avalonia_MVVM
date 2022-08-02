using System;

namespace pluginStringOperations
{
    public class Class1
    {
        public static int To_Integer(string s)
        {
            return int.Parse(s);
        }
        public static double To_Float(string s)
        {
            return double.Parse(s);
        }
        public static string Get_Nth_String(string input, string separator, int n)
        {
            string[] stringSeparators = new string[] { separator };
            string[] result = input.Split(stringSeparators, StringSplitOptions.None);
            if (n > 0 && n <= result.Length)
            {
                return result[n - 1];
            }
            else if (n == result.Length + 1)
            {
                return separator;
            }
            else
            {
                throw new ArgumentOutOfRangeException("there are only " + result.Length + " strings");
            }
        }
    }
}
