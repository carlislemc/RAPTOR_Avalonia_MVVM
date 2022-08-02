using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pluginsTime
{
    public class TimeClass
    {
        private static DateTime NineteenNinety = new DateTime(1990, 1, 1);
        public static int Current_Day()
        {
            return DateTime.Now.Day;
        }
        public static int Current_Year()
        {
            return DateTime.Now.Year;
        }
        public static int Current_Month()
        {
            return DateTime.Now.Month;
        }
        public static int Current_Minute()
        {
            return DateTime.Now.Minute;
        }
        public static int Current_Hour()
        {
            return DateTime.Now.Hour;
        }
        public static int Current_Second()
        {
            return DateTime.Now.Second;
        }
        public static int Current_Millisecond()
        {
            return DateTime.Now.Millisecond;
        }
        public static double Current_Time()
        {
            TimeSpan ts = (DateTime.Now - NineteenNinety);

            return (double)(ts.Ticks / 10000);
        }
    }
}
