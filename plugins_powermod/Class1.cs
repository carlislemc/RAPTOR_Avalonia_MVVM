// Decompiled with JetBrains decompiler
// Type: GetString.frmText
// Assembly: plugins.cs, Version=1.0.3018.30501, Culture=neutral, PublicKeyToken=null
// MVID: 6AD1BD11-0F64-4DB8-913E-976C16B50C25
// Assembly location: C:\d\My Documents\Visual Studio 2008\Projects\raptor\plugins_powermod.dll


using System;
using System.Text;


namespace pluginPowerMod
{
    public class Class1
    {

        public static double PowerMod(double b, double e, double N)
        {
            long num1 = Convert.ToInt64(b);
            long num2 = Convert.ToInt64(e);
            long num3 = Convert.ToInt64(N);
            BigInteger bigInteger = new BigInteger(num1);
            BigInteger n = new BigInteger(num3);
            BigInteger exp = new BigInteger(num2);
            return Convert.ToDouble(bigInteger.modPow(exp, n).LongValue());
        }
    }
}
