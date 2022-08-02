using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginPowerMod
{
    internal class BigInteger
    {
        public static readonly int[] primesBelow2000 = new int[303]
    {
      2,
      3,
      5,
      7,
      11,
      13,
      17,
      19,
      23,
      29,
      31,
      37,
      41,
      43,
      47,
      53,
      59,
      61,
      67,
      71,
      73,
      79,
      83,
      89,
      97,
      101,
      103,
      107,
      109,
      113,
      (int) sbyte.MaxValue,
      131,
      137,
      139,
      149,
      151,
      157,
      163,
      167,
      173,
      179,
      181,
      191,
      193,
      197,
      199,
      211,
      223,
      227,
      229,
      233,
      239,
      241,
      251,
      257,
      263,
      269,
      271,
      277,
      281,
      283,
      293,
      307,
      311,
      313,
      317,
      331,
      337,
      347,
      349,
      353,
      359,
      367,
      373,
      379,
      383,
      389,
      397,
      401,
      409,
      419,
      421,
      431,
      433,
      439,
      443,
      449,
      457,
      461,
      463,
      467,
      479,
      487,
      491,
      499,
      503,
      509,
      521,
      523,
      541,
      547,
      557,
      563,
      569,
      571,
      577,
      587,
      593,
      599,
      601,
      607,
      613,
      617,
      619,
      631,
      641,
      643,
      647,
      653,
      659,
      661,
      673,
      677,
      683,
      691,
      701,
      709,
      719,
      727,
      733,
      739,
      743,
      751,
      757,
      761,
      769,
      773,
      787,
      797,
      809,
      811,
      821,
      823,
      827,
      829,
      839,
      853,
      857,
      859,
      863,
      877,
      881,
      883,
      887,
      907,
      911,
      919,
      929,
      937,
      941,
      947,
      953,
      967,
      971,
      977,
      983,
      991,
      997,
      1009,
      1013,
      1019,
      1021,
      1031,
      1033,
      1039,
      1049,
      1051,
      1061,
      1063,
      1069,
      1087,
      1091,
      1093,
      1097,
      1103,
      1109,
      1117,
      1123,
      1129,
      1151,
      1153,
      1163,
      1171,
      1181,
      1187,
      1193,
      1201,
      1213,
      1217,
      1223,
      1229,
      1231,
      1237,
      1249,
      1259,
      1277,
      1279,
      1283,
      1289,
      1291,
      1297,
      1301,
      1303,
      1307,
      1319,
      1321,
      1327,
      1361,
      1367,
      1373,
      1381,
      1399,
      1409,
      1423,
      1427,
      1429,
      1433,
      1439,
      1447,
      1451,
      1453,
      1459,
      1471,
      1481,
      1483,
      1487,
      1489,
      1493,
      1499,
      1511,
      1523,
      1531,
      1543,
      1549,
      1553,
      1559,
      1567,
      1571,
      1579,
      1583,
      1597,
      1601,
      1607,
      1609,
      1613,
      1619,
      1621,
      1627,
      1637,
      1657,
      1663,
      1667,
      1669,
      1693,
      1697,
      1699,
      1709,
      1721,
      1723,
      1733,
      1741,
      1747,
      1753,
      1759,
      1777,
      1783,
      1787,
      1789,
      1801,
      1811,
      1823,
      1831,
      1847,
      1861,
      1867,
      1871,
      1873,
      1877,
      1879,
      1889,
      1901,
      1907,
      1913,
      1931,
      1933,
      1949,
      1951,
      1973,
      1979,
      1987,
      1993,
      1997,
      1999
    };
        private uint[] data = (uint[])null;
        private const int maxLength = 70;
        public int dataLength;

        public BigInteger()
        {
            this.data = new uint[70];
            this.dataLength = 1;
        }

        public BigInteger(long value)
        {
            this.data = new uint[70];
            long num = value;
            for (this.dataLength = 0; value != 0L && this.dataLength < 70; ++this.dataLength)
            {
                this.data[this.dataLength] = (uint)((ulong)value & (ulong)uint.MaxValue);
                value >>= 32;
            }
            if (num > 0L)
            {
                if (value != 0L || ((int)this.data[69] & int.MinValue) != 0)
                    throw new ArithmeticException("Positive overflow in constructor.");
            }
            else if (num < 0L && (value != -1L || ((int)this.data[this.dataLength - 1] & int.MinValue) == 0))
                throw new ArithmeticException("Negative underflow in constructor.");
            if (this.dataLength != 0)
                return;
            this.dataLength = 1;
        }

        public BigInteger(ulong value)
        {
            this.data = new uint[70];
            for (this.dataLength = 0; (long)value != 0L && this.dataLength < 70; ++this.dataLength)
            {
                this.data[this.dataLength] = (uint)(value & (ulong)uint.MaxValue);
                value >>= 32;
            }
            if ((long)value != 0L || ((int)this.data[69] & int.MinValue) != 0)
                throw new ArithmeticException("Positive overflow in constructor.");
            if (this.dataLength != 0)
                return;
            this.dataLength = 1;
        }

        public BigInteger(BigInteger bi)
        {
            this.data = new uint[70];
            this.dataLength = bi.dataLength;
            for (int index = 0; index < this.dataLength; ++index)
                this.data[index] = bi.data[index];
        }

        public BigInteger(string value, int radix)
        {
            BigInteger bigInteger1 = new BigInteger(1L);
            BigInteger bigInteger2 = new BigInteger();
            value = value.ToUpper().Trim();
            int num1 = 0;
            if ((int)value[0] == 45)
                num1 = 1;
            for (int index = value.Length - 1; index >= num1; --index)
            {
                int num2 = (int)value[index];
                int num3 = num2 < 48 || num2 > 57 ? (num2 < 65 || num2 > 90 ? 9999999 : num2 - 65 + 10) : num2 - 48;
                if (num3 >= radix)
                    throw new ArithmeticException("Invalid string in constructor.");
                if ((int)value[0] == 45)
                    num3 = -num3;
                bigInteger2 += bigInteger1 * (BigInteger)num3;
                if (index - 1 >= num1)
                    bigInteger1 *= (BigInteger)radix;
            }
            if ((int)value[0] == 45)
            {
                if (((int)bigInteger2.data[69] & int.MinValue) == 0)
                    throw new ArithmeticException("Negative underflow in constructor.");
            }
            else if (((int)bigInteger2.data[69] & int.MinValue) != 0)
                throw new ArithmeticException("Positive overflow in constructor.");
            this.data = new uint[70];
            for (int index = 0; index < bigInteger2.dataLength; ++index)
                this.data[index] = bigInteger2.data[index];
            this.dataLength = bigInteger2.dataLength;
        }

        public BigInteger(byte[] inData)
        {
            this.dataLength = inData.Length >> 2;
            int num = inData.Length & 3;
            if (num != 0)
                ++this.dataLength;
            if (this.dataLength > 70)
                throw new ArithmeticException("Byte overflow in constructor.");
            this.data = new uint[70];
            int index1 = inData.Length - 1;
            int index2 = 0;
            while (index1 >= 3)
            {
                this.data[index2] = (uint)(((int)inData[index1 - 3] << 24) + ((int)inData[index1 - 2] << 16) + ((int)inData[index1 - 1] << 8)) + (uint)inData[index1];
                index1 -= 4;
                ++index2;
            }
            if (num == 1)
                this.data[this.dataLength - 1] = (uint)inData[0];
            else if (num == 2)
                this.data[this.dataLength - 1] = ((uint)inData[0] << 8) + (uint)inData[1];
            else if (num == 3)
                this.data[this.dataLength - 1] = (uint)(((int)inData[0] << 16) + ((int)inData[1] << 8)) + (uint)inData[2];
            while (this.dataLength > 1 && (int)this.data[this.dataLength - 1] == 0)
                --this.dataLength;
        }

        public BigInteger(byte[] inData, int inLen)
        {
            this.dataLength = inLen >> 2;
            int num = inLen & 3;
            if (num != 0)
                ++this.dataLength;
            if (this.dataLength > 70 || inLen > inData.Length)
                throw new ArithmeticException("Byte overflow in constructor.");
            this.data = new uint[70];
            int index1 = inLen - 1;
            int index2 = 0;
            while (index1 >= 3)
            {
                this.data[index2] = (uint)(((int)inData[index1 - 3] << 24) + ((int)inData[index1 - 2] << 16) + ((int)inData[index1 - 1] << 8)) + (uint)inData[index1];
                index1 -= 4;
                ++index2;
            }
            if (num == 1)
                this.data[this.dataLength - 1] = (uint)inData[0];
            else if (num == 2)
                this.data[this.dataLength - 1] = ((uint)inData[0] << 8) + (uint)inData[1];
            else if (num == 3)
                this.data[this.dataLength - 1] = (uint)(((int)inData[0] << 16) + ((int)inData[1] << 8)) + (uint)inData[2];
            if (this.dataLength == 0)
                this.dataLength = 1;
            while (this.dataLength > 1 && (int)this.data[this.dataLength - 1] == 0)
                --this.dataLength;
        }

        public BigInteger(uint[] inData)
        {
            this.dataLength = inData.Length;
            if (this.dataLength > 70)
                throw new ArithmeticException("Byte overflow in constructor.");
            this.data = new uint[70];
            int index1 = this.dataLength - 1;
            int index2 = 0;
            while (index1 >= 0)
            {
                this.data[index2] = inData[index1];
                --index1;
                ++index2;
            }
            while (this.dataLength > 1 && (int)this.data[this.dataLength - 1] == 0)
                --this.dataLength;
        }

        public static implicit operator BigInteger(long value)
        {
            return new BigInteger(value);
        }

        public static implicit operator BigInteger(ulong value)
        {
            return new BigInteger(value);
        }

        public static implicit operator BigInteger(int value)
        {
            return new BigInteger((long)value);
        }

        public static implicit operator BigInteger(uint value)
        {
            return new BigInteger((ulong)value);
        }

        public static BigInteger operator +(BigInteger bi1, BigInteger bi2)
        {
            BigInteger bigInteger = new BigInteger();
            bigInteger.dataLength = bi1.dataLength > bi2.dataLength ? bi1.dataLength : bi2.dataLength;
            long num1 = 0L;
            for (int index = 0; index < bigInteger.dataLength; ++index)
            {
                long num2 = (long)bi1.data[index] + (long)bi2.data[index] + num1;
                num1 = num2 >> 32;
                bigInteger.data[index] = (uint)((ulong)num2 & (ulong)uint.MaxValue);
            }
            if (num1 != 0L && bigInteger.dataLength < 70)
            {
                bigInteger.data[bigInteger.dataLength] = (uint)num1;
                ++bigInteger.dataLength;
            }
            while (bigInteger.dataLength > 1 && (int)bigInteger.data[bigInteger.dataLength - 1] == 0)
                --bigInteger.dataLength;
            int index1 = 69;
            if (((int)bi1.data[index1] & int.MinValue) == ((int)bi2.data[index1] & int.MinValue) && ((int)bigInteger.data[index1] & int.MinValue) != ((int)bi1.data[index1] & int.MinValue))
                throw new ArithmeticException();
            return bigInteger;
        }

        public static BigInteger operator ++(BigInteger bi1)
        {
            BigInteger bigInteger = new BigInteger(bi1);
            long num1 = 1L;
            int index1;
            for (index1 = 0; num1 != 0L && index1 < 70; ++index1)
            {
                long num2 = (long)bigInteger.data[index1] + 1L;
                bigInteger.data[index1] = (uint)((ulong)num2 & (ulong)uint.MaxValue);
                num1 = num2 >> 32;
            }
            if (index1 > bigInteger.dataLength)
            {
                bigInteger.dataLength = index1;
            }
            else
            {
                while (bigInteger.dataLength > 1 && (int)bigInteger.data[bigInteger.dataLength - 1] == 0)
                    --bigInteger.dataLength;
            }
            int index2 = 69;
            if (((int)bi1.data[index2] & int.MinValue) == 0 && ((int)bigInteger.data[index2] & int.MinValue) != ((int)bi1.data[index2] & int.MinValue))
                throw new ArithmeticException("Overflow in ++.");
            return bigInteger;
        }

        public static BigInteger operator -(BigInteger bi1, BigInteger bi2)
        {
            BigInteger bigInteger = new BigInteger();
            bigInteger.dataLength = bi1.dataLength > bi2.dataLength ? bi1.dataLength : bi2.dataLength;
            long num1 = 0L;
            for (int index = 0; index < bigInteger.dataLength; ++index)
            {
                long num2 = (long)bi1.data[index] - (long)bi2.data[index] - num1;
                bigInteger.data[index] = (uint)((ulong)num2 & (ulong)uint.MaxValue);
                num1 = num2 >= 0L ? 0L : 1L;
            }
            if (num1 != 0L)
            {
                for (int index = bigInteger.dataLength; index < 70; ++index)
                    bigInteger.data[index] = uint.MaxValue;
                bigInteger.dataLength = 70;
            }
            while (bigInteger.dataLength > 1 && (int)bigInteger.data[bigInteger.dataLength - 1] == 0)
                --bigInteger.dataLength;
            int index1 = 69;
            if (((int)bi1.data[index1] & int.MinValue) != ((int)bi2.data[index1] & int.MinValue) && ((int)bigInteger.data[index1] & int.MinValue) != ((int)bi1.data[index1] & int.MinValue))
                throw new ArithmeticException();
            return bigInteger;
        }

        public static BigInteger operator --(BigInteger bi1)
        {
            BigInteger bigInteger = new BigInteger(bi1);
            bool flag = true;
            int index1;
            for (index1 = 0; flag && index1 < 70; ++index1)
            {
                long num = (long)bigInteger.data[index1] - 1L;
                bigInteger.data[index1] = (uint)((ulong)num & (ulong)uint.MaxValue);
                if (num >= 0L)
                    flag = false;
            }
            if (index1 > bigInteger.dataLength)
                bigInteger.dataLength = index1;
            while (bigInteger.dataLength > 1 && (int)bigInteger.data[bigInteger.dataLength - 1] == 0)
                --bigInteger.dataLength;
            int index2 = 69;
            if (((int)bi1.data[index2] & int.MinValue) != 0 && ((int)bigInteger.data[index2] & int.MinValue) != ((int)bi1.data[index2] & int.MinValue))
                throw new ArithmeticException("Underflow in --.");
            return bigInteger;
        }

        public static BigInteger operator *(BigInteger bi1, BigInteger bi2)
        {
            int index1 = 69;
            bool flag1 = false;
            bool flag2 = false;
            try
            {
                if (((int)bi1.data[index1] & int.MinValue) != 0)
                {
                    flag1 = true;
                    bi1 = -bi1;
                }
                if (((int)bi2.data[index1] & int.MinValue) != 0)
                {
                    flag2 = true;
                    bi2 = -bi2;
                }
            }
            catch (Exception ex)
            {
            }
            BigInteger bigInteger = new BigInteger();
            try
            {
                for (int index2 = 0; index2 < bi1.dataLength; ++index2)
                {
                    if ((int)bi1.data[index2] != 0)
                    {
                        ulong num1 = 0UL;
                        int index3 = 0;
                        int index4 = index2;
                        while (index3 < bi2.dataLength)
                        {
                            ulong num2 = (ulong)bi1.data[index2] * (ulong)bi2.data[index3] + (ulong)bigInteger.data[index4] + num1;
                            bigInteger.data[index4] = (uint)(num2 & (ulong)uint.MaxValue);
                            num1 = num2 >> 32;
                            ++index3;
                            ++index4;
                        }
                        if ((long)num1 != 0L)
                            bigInteger.data[index2 + bi2.dataLength] = (uint)num1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArithmeticException("Multiplication overflow.");
            }
            bigInteger.dataLength = bi1.dataLength + bi2.dataLength;
            if (bigInteger.dataLength > 70)
                bigInteger.dataLength = 70;
            while (bigInteger.dataLength > 1 && (int)bigInteger.data[bigInteger.dataLength - 1] == 0)
                --bigInteger.dataLength;
            if (((int)bigInteger.data[index1] & int.MinValue) != 0)
            {
                if (flag1 != flag2 && (int)bigInteger.data[index1] == int.MinValue)
                {
                    if (bigInteger.dataLength == 1)
                        return bigInteger;
                    bool flag3 = true;
                    for (int index2 = 0; index2 < bigInteger.dataLength - 1 && flag3; ++index2)
                    {
                        if ((int)bigInteger.data[index2] != 0)
                            flag3 = false;
                    }
                    if (flag3)
                        return bigInteger;
                }
                throw new ArithmeticException("Multiplication overflow.");
            }
            if (flag1 != flag2)
                return -bigInteger;
            return bigInteger;
        }

        public static BigInteger operator <<(BigInteger bi1, int shiftVal)
        {
            BigInteger bigInteger = new BigInteger(bi1);
            bigInteger.dataLength = BigInteger.shiftLeft(bigInteger.data, shiftVal);
            return bigInteger;
        }

        public static BigInteger operator >>(BigInteger bi1, int shiftVal)
        {
            BigInteger bigInteger = new BigInteger(bi1);
            bigInteger.dataLength = BigInteger.shiftRight(bigInteger.data, shiftVal);
            if (((int)bi1.data[69] & int.MinValue) != 0)
            {
                for (int index = 69; index >= bigInteger.dataLength; --index)
                    bigInteger.data[index] = uint.MaxValue;
                uint num = uint.MaxValue;
                for (int index = 0; index < 32 && ((int)bigInteger.data[bigInteger.dataLength - 1] & (int)num) == 0; ++index)
                {
                    bigInteger.data[bigInteger.dataLength - 1] |= num;
                    num >>= 1;
                }
                bigInteger.dataLength = 70;
            }
            return bigInteger;
        }

        public static BigInteger operator ~(BigInteger bi1)
        {
            BigInteger bigInteger = new BigInteger(bi1);
            for (int index = 0; index < 70; ++index)
                bigInteger.data[index] = ~bi1.data[index];
            bigInteger.dataLength = 70;
            while (bigInteger.dataLength > 1 && (int)bigInteger.data[bigInteger.dataLength - 1] == 0)
                --bigInteger.dataLength;
            return bigInteger;
        }

        public static BigInteger operator -(BigInteger bi1)
        {
            if (bi1.dataLength == 1 && (int)bi1.data[0] == 0)
                return new BigInteger();
            BigInteger bigInteger = new BigInteger(bi1);
            for (int index = 0; index < 70; ++index)
                bigInteger.data[index] = ~bi1.data[index];
            long num1 = 1L;
            for (int index = 0; num1 != 0L && index < 70; ++index)
            {
                long num2 = (long)bigInteger.data[index] + 1L;
                bigInteger.data[index] = (uint)((ulong)num2 & (ulong)uint.MaxValue);
                num1 = num2 >> 32;
            }
            if (((int)bi1.data[69] & int.MinValue) == ((int)bigInteger.data[69] & int.MinValue))
                throw new ArithmeticException("Overflow in negation.\n");
            bigInteger.dataLength = 70;
            while (bigInteger.dataLength > 1 && (int)bigInteger.data[bigInteger.dataLength - 1] == 0)
                --bigInteger.dataLength;
            return bigInteger;
        }

        public static bool operator ==(BigInteger bi1, BigInteger bi2)
        {
            return bi1.Equals((object)bi2);
        }

        public static bool operator !=(BigInteger bi1, BigInteger bi2)
        {
            return !bi1.Equals((object)bi2);
        }

        public static bool operator >(BigInteger bi1, BigInteger bi2)
        {
            int index1 = 69;
            if (((int)bi1.data[index1] & int.MinValue) != 0 && ((int)bi2.data[index1] & int.MinValue) == 0)
                return false;
            if (((int)bi1.data[index1] & int.MinValue) == 0 && ((int)bi2.data[index1] & int.MinValue) != 0)
                return true;
            int index2 = (bi1.dataLength > bi2.dataLength ? bi1.dataLength : bi2.dataLength) - 1;
            while (index2 >= 0 && (int)bi1.data[index2] == (int)bi2.data[index2])
                --index2;
            return index2 >= 0 && bi1.data[index2] > bi2.data[index2];
        }

        public static bool operator <(BigInteger bi1, BigInteger bi2)
        {
            int index1 = 69;
            if (((int)bi1.data[index1] & int.MinValue) != 0 && ((int)bi2.data[index1] & int.MinValue) == 0)
                return true;
            if (((int)bi1.data[index1] & int.MinValue) == 0 && ((int)bi2.data[index1] & int.MinValue) != 0)
                return false;
            int index2 = (bi1.dataLength > bi2.dataLength ? bi1.dataLength : bi2.dataLength) - 1;
            while (index2 >= 0 && (int)bi1.data[index2] == (int)bi2.data[index2])
                --index2;
            return index2 >= 0 && bi1.data[index2] < bi2.data[index2];
        }

        public static bool operator >=(BigInteger bi1, BigInteger bi2)
        {
            return bi1 == bi2 || bi1 > bi2;
        }

        public static bool operator <=(BigInteger bi1, BigInteger bi2)
        {
            return bi1 == bi2 || bi1 < bi2;
        }

        public static BigInteger operator /(BigInteger bi1, BigInteger bi2)
        {
            BigInteger outQuotient = new BigInteger();
            BigInteger outRemainder = new BigInteger();
            int index = 69;
            bool flag1 = false;
            bool flag2 = false;
            if (((int)bi1.data[index] & int.MinValue) != 0)
            {
                bi1 = -bi1;
                flag2 = true;
            }
            if (((int)bi2.data[index] & int.MinValue) != 0)
            {
                bi2 = -bi2;
                flag1 = true;
            }
            if (bi1 < bi2)
                return outQuotient;
            if (bi2.dataLength == 1)
                BigInteger.singleByteDivide(bi1, bi2, outQuotient, outRemainder);
            else
                BigInteger.multiByteDivide(bi1, bi2, outQuotient, outRemainder);
            if (flag2 != flag1)
                return -outQuotient;
            return outQuotient;
        }

        public static BigInteger operator %(BigInteger bi1, BigInteger bi2)
        {
            BigInteger outQuotient = new BigInteger();
            BigInteger outRemainder = new BigInteger(bi1);
            int index = 69;
            bool flag = false;
            if (((int)bi1.data[index] & int.MinValue) != 0)
            {
                bi1 = -bi1;
                flag = true;
            }
            if (((int)bi2.data[index] & int.MinValue) != 0)
                bi2 = -bi2;
            if (bi1 < bi2)
                return outRemainder;
            if (bi2.dataLength == 1)
                BigInteger.singleByteDivide(bi1, bi2, outQuotient, outRemainder);
            else
                BigInteger.multiByteDivide(bi1, bi2, outQuotient, outRemainder);
            if (flag)
                return -outRemainder;
            return outRemainder;
        }

        public static BigInteger operator &(BigInteger bi1, BigInteger bi2)
        {
            BigInteger bigInteger = new BigInteger();
            int num1 = bi1.dataLength > bi2.dataLength ? bi1.dataLength : bi2.dataLength;
            for (int index = 0; index < num1; ++index)
            {
                uint num2 = bi1.data[index] & bi2.data[index];
                bigInteger.data[index] = num2;
            }
            bigInteger.dataLength = 70;
            while (bigInteger.dataLength > 1 && (int)bigInteger.data[bigInteger.dataLength - 1] == 0)
                --bigInteger.dataLength;
            return bigInteger;
        }

        public static BigInteger operator |(BigInteger bi1, BigInteger bi2)
        {
            BigInteger bigInteger = new BigInteger();
            int num1 = bi1.dataLength > bi2.dataLength ? bi1.dataLength : bi2.dataLength;
            for (int index = 0; index < num1; ++index)
            {
                uint num2 = bi1.data[index] | bi2.data[index];
                bigInteger.data[index] = num2;
            }
            bigInteger.dataLength = 70;
            while (bigInteger.dataLength > 1 && (int)bigInteger.data[bigInteger.dataLength - 1] == 0)
                --bigInteger.dataLength;
            return bigInteger;
        }

        public static BigInteger operator ^(BigInteger bi1, BigInteger bi2)
        {
            BigInteger bigInteger = new BigInteger();
            int num1 = bi1.dataLength > bi2.dataLength ? bi1.dataLength : bi2.dataLength;
            for (int index = 0; index < num1; ++index)
            {
                uint num2 = bi1.data[index] ^ bi2.data[index];
                bigInteger.data[index] = num2;
            }
            bigInteger.dataLength = 70;
            while (bigInteger.dataLength > 1 && (int)bigInteger.data[bigInteger.dataLength - 1] == 0)
                --bigInteger.dataLength;
            return bigInteger;
        }

        private static int shiftLeft(uint[] buffer, int shiftVal)
        {
            int num1 = 32;
            int length = buffer.Length;
            while (length > 1 && (int)buffer[length - 1] == 0)
                --length;
            int num2 = shiftVal;
            while (num2 > 0)
            {
                if (num2 < num1)
                    num1 = num2;
                ulong num3 = 0UL;
                for (int index = 0; index < length; ++index)
                {
                    ulong num4 = (ulong)buffer[index] << num1 | num3;
                    buffer[index] = (uint)(num4 & (ulong)uint.MaxValue);
                    num3 = num4 >> 32;
                }
                if ((long)num3 != 0L && length + 1 <= buffer.Length)
                {
                    buffer[length] = (uint)num3;
                    ++length;
                }
                num2 -= num1;
            }
            return length;
        }

        private static int shiftRight(uint[] buffer, int shiftVal)
        {
            int num1 = 32;
            int num2 = 0;
            int length = buffer.Length;
            while (length > 1 && (int)buffer[length - 1] == 0)
                --length;
            int num3 = shiftVal;
            while (num3 > 0)
            {
                if (num3 < num1)
                {
                    num1 = num3;
                    num2 = 32 - num1;
                }
                ulong num4 = 0UL;
                for (int index = length - 1; index >= 0; --index)
                {
                    ulong num5 = (ulong)buffer[index] >> num1 | num4;
                    num4 = (ulong)buffer[index] << num2;
                    buffer[index] = (uint)num5;
                }
                num3 -= num1;
            }
            while (length > 1 && (int)buffer[length - 1] == 0)
                --length;
            return length;
        }

        public override bool Equals(object o)
        {
            BigInteger bigInteger = (BigInteger)o;
            if (this.dataLength != bigInteger.dataLength)
                return false;
            for (int index = 0; index < this.dataLength; ++index)
            {
                if ((int)this.data[index] != (int)bigInteger.data[index])
                    return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        private static void multiByteDivide(BigInteger bi1, BigInteger bi2, BigInteger outQuotient, BigInteger outRemainder)
        {
            uint[] numArray = new uint[70];
            int length1 = bi1.dataLength + 1;
            uint[] buffer = new uint[length1];
            uint num1 = uint.MaxValue;
            uint num2 = bi2.data[bi2.dataLength - 1];
            int shiftVal = 0;
            int num3 = 0;
            while ((int)num1 != 0 && ((int)num2 & (int)num1) == 0)
            {
                ++shiftVal;
                num1 >>= 1;
            }
            for (int index = 0; index < bi1.dataLength; ++index)
                buffer[index] = bi1.data[index];
            BigInteger.shiftLeft(buffer, shiftVal);
            bi2 <<= shiftVal;
            int num4 = length1 - bi2.dataLength;
            int index1 = length1 - 1;
            ulong num5 = (ulong)bi2.data[bi2.dataLength - 1];
            ulong num6 = (ulong)bi2.data[bi2.dataLength - 2];
            int length2 = bi2.dataLength + 1;
            uint[] inData = new uint[length2];
            for (; num4 > 0; --num4)
            {
                ulong num7 = ((ulong)buffer[index1] << 32) + (ulong)buffer[index1 - 1];
                ulong num8 = num7 / num5;
                ulong num9 = num7 % num5;
                bool flag = false;
                while (!flag)
                {
                    flag = true;
                    if ((long)num8 == 4294967296L || num8 * num6 > (num9 << 32) + (ulong)buffer[index1 - 2])
                    {
                        --num8;
                        num9 += num5;
                        if (num9 < 4294967296UL)
                            flag = false;
                    }
                }
                for (int index2 = 0; index2 < length2; ++index2)
                    inData[index2] = buffer[index1 - index2];
                BigInteger bigInteger1 = new BigInteger(inData);
                BigInteger bigInteger2 = bi2 * (BigInteger)((long)num8);
                while (bigInteger2 > bigInteger1)
                {
                    --num8;
                    bigInteger2 -= bi2;
                }
                BigInteger bigInteger3 = bigInteger1 - bigInteger2;
                for (int index2 = 0; index2 < length2; ++index2)
                    buffer[index1 - index2] = bigInteger3.data[bi2.dataLength - index2];
                numArray[num3++] = (uint)num8;
                --index1;
            }
            outQuotient.dataLength = num3;
            int index3 = 0;
            int index4 = outQuotient.dataLength - 1;
            while (index4 >= 0)
            {
                outQuotient.data[index3] = numArray[index4];
                --index4;
                ++index3;
            }
            for (; index3 < 70; ++index3)
                outQuotient.data[index3] = 0U;
            while (outQuotient.dataLength > 1 && (int)outQuotient.data[outQuotient.dataLength - 1] == 0)
                --outQuotient.dataLength;
            if (outQuotient.dataLength == 0)
                outQuotient.dataLength = 1;
            outRemainder.dataLength = BigInteger.shiftRight(buffer, shiftVal);
            int index5;
            for (index5 = 0; index5 < outRemainder.dataLength; ++index5)
                outRemainder.data[index5] = buffer[index5];
            for (; index5 < 70; ++index5)
                outRemainder.data[index5] = 0U;
        }

        private static void singleByteDivide(BigInteger bi1, BigInteger bi2, BigInteger outQuotient, BigInteger outRemainder)
        {
            uint[] numArray = new uint[70];
            int num1 = 0;
            for (int index = 0; index < 70; ++index)
                outRemainder.data[index] = bi1.data[index];
            outRemainder.dataLength = bi1.dataLength;
            while (outRemainder.dataLength > 1 && (int)outRemainder.data[outRemainder.dataLength - 1] == 0)
                --outRemainder.dataLength;
            ulong num2 = (ulong)bi2.data[0];
            int index1 = outRemainder.dataLength - 1;
            ulong num3 = (ulong)outRemainder.data[index1];
            if (num3 >= num2)
            {
                ulong num4 = num3 / num2;
                numArray[num1++] = (uint)num4;
                outRemainder.data[index1] = (uint)(num3 % num2);
            }
            ulong num5;
            for (int index2 = index1 - 1; index2 >= 0; outRemainder.data[index2--] = (uint)(num5 % num2))
            {
                num5 = ((ulong)outRemainder.data[index2 + 1] << 32) + (ulong)outRemainder.data[index2];
                ulong num4 = num5 / num2;
                numArray[num1++] = (uint)num4;
                outRemainder.data[index2 + 1] = 0U;
            }
            outQuotient.dataLength = num1;
            int index3 = 0;
            int index4 = outQuotient.dataLength - 1;
            while (index4 >= 0)
            {
                outQuotient.data[index3] = numArray[index4];
                --index4;
                ++index3;
            }
            for (; index3 < 70; ++index3)
                outQuotient.data[index3] = 0U;
            while (outQuotient.dataLength > 1 && (int)outQuotient.data[outQuotient.dataLength - 1] == 0)
                --outQuotient.dataLength;
            if (outQuotient.dataLength == 0)
                outQuotient.dataLength = 1;
            while (outRemainder.dataLength > 1 && (int)outRemainder.data[outRemainder.dataLength - 1] == 0)
                --outRemainder.dataLength;
        }

        public BigInteger max(BigInteger bi)
        {
            if (this > bi)
                return new BigInteger(this);
            return new BigInteger(bi);
        }

        public BigInteger min(BigInteger bi)
        {
            if (this < bi)
                return new BigInteger(this);
            return new BigInteger(bi);
        }

        public BigInteger abs()
        {
            if (((int)this.data[69] & int.MinValue) != 0)
                return -this;
            return new BigInteger(this);
        }

        public override string ToString()
        {
            return this.ToString(10);
        }

        public string ToString(int radix)
        {
            if (radix < 2 || radix > 36)
                throw new ArgumentException("Radix must be >= 2 and <= 36");
            string str1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string str2 = "";
            BigInteger bi1 = this;
            bool flag = false;
            if (((int)bi1.data[69] & int.MinValue) != 0)
            {
                flag = true;
                try
                {
                    bi1 = -bi1;
                }
                catch (Exception ex)
                {
                }
            }
            BigInteger outQuotient = new BigInteger();
            BigInteger outRemainder = new BigInteger();
            BigInteger bi2 = new BigInteger((long)radix);
            if (bi1.dataLength == 1 && (int)bi1.data[0] == 0)
            {
                str2 = "0";
            }
            else
            {
                for (; bi1.dataLength > 1 || bi1.dataLength == 1 && (int)bi1.data[0] != 0; bi1 = outQuotient)
                {
                    BigInteger.singleByteDivide(bi1, bi2, outQuotient, outRemainder);
                    str2 = outRemainder.data[0] >= 10U ? (string)(object)str1[(int)outRemainder.data[0] - 10] + (object)str2 : (string)(object)outRemainder.data[0] + (object)str2;
                }
                if (flag)
                    str2 = "-" + str2;
            }
            return str2;
        }

        public string ToHexString()
        {
            string str = this.data[this.dataLength - 1].ToString("X");
            for (int index = this.dataLength - 2; index >= 0; --index)
                str += this.data[index].ToString("X8");
            return str;
        }

        public BigInteger modPow(BigInteger exp, BigInteger n)
        {
            if (((int)exp.data[69] & int.MinValue) != 0)
                throw new ArithmeticException("Positive exponents only.");
            BigInteger bigInteger1 = (BigInteger)1;
            bool flag = false;
            BigInteger bigInteger2;
            if (((int)this.data[69] & int.MinValue) != 0)
            {
                bigInteger2 = -this % n;
                flag = true;
            }
            else
                bigInteger2 = this % n;
            if (((int)n.data[69] & int.MinValue) != 0)
                n = -n;
            BigInteger bigInteger3 = new BigInteger();
            int index1 = n.dataLength << 1;
            bigInteger3.data[index1] = 1U;
            bigInteger3.dataLength = index1 + 1;
            BigInteger constant = bigInteger3 / n;
            int num1 = exp.bitCount();
            int num2 = 0;
            for (int index2 = 0; index2 < exp.dataLength; ++index2)
            {
                uint num3 = 1U;
                for (int index3 = 0; index3 < 32; ++index3)
                {
                    if (((int)exp.data[index2] & (int)num3) != 0)
                        bigInteger1 = this.BarrettReduction(bigInteger1 * bigInteger2, n, constant);
                    num3 <<= 1;
                    bigInteger2 = this.BarrettReduction(bigInteger2 * bigInteger2, n, constant);
                    if (bigInteger2.dataLength == 1 && (int)bigInteger2.data[0] == 1)
                    {
                        if (flag && ((int)exp.data[0] & 1) != 0)
                            return -bigInteger1;
                        return bigInteger1;
                    }
                    ++num2;
                    if (num2 == num1)
                        break;
                }
            }
            if (flag && ((int)exp.data[0] & 1) != 0)
                return -bigInteger1;
            return bigInteger1;
        }

        private BigInteger BarrettReduction(BigInteger x, BigInteger n, BigInteger constant)
        {
            int num1 = n.dataLength;
            int index1 = num1 + 1;
            int num2 = num1 - 1;
            BigInteger bigInteger1 = new BigInteger();
            int index2 = num2;
            int index3 = 0;
            while (index2 < x.dataLength)
            {
                bigInteger1.data[index3] = x.data[index2];
                ++index2;
                ++index3;
            }
            bigInteger1.dataLength = x.dataLength - num2;
            if (bigInteger1.dataLength <= 0)
                bigInteger1.dataLength = 1;
            BigInteger bigInteger2 = bigInteger1 * constant;
            BigInteger bigInteger3 = new BigInteger();
            int index4 = index1;
            int index5 = 0;
            while (index4 < bigInteger2.dataLength)
            {
                bigInteger3.data[index5] = bigInteger2.data[index4];
                ++index4;
                ++index5;
            }
            bigInteger3.dataLength = bigInteger2.dataLength - index1;
            if (bigInteger3.dataLength <= 0)
                bigInteger3.dataLength = 1;
            BigInteger bigInteger4 = new BigInteger();
            int num3 = x.dataLength > index1 ? index1 : x.dataLength;
            for (int index6 = 0; index6 < num3; ++index6)
                bigInteger4.data[index6] = x.data[index6];
            bigInteger4.dataLength = num3;
            BigInteger bigInteger5 = new BigInteger();
            for (int index6 = 0; index6 < bigInteger3.dataLength; ++index6)
            {
                if ((int)bigInteger3.data[index6] != 0)
                {
                    ulong num4 = 0UL;
                    int index7 = index6;
                    for (int index8 = 0; index8 < n.dataLength && index7 < index1; ++index7)
                    {
                        ulong num5 = (ulong)bigInteger3.data[index6] * (ulong)n.data[index8] + (ulong)bigInteger5.data[index7] + num4;
                        bigInteger5.data[index7] = (uint)(num5 & (ulong)uint.MaxValue);
                        num4 = num5 >> 32;
                        ++index8;
                    }
                    if (index7 < index1)
                        bigInteger5.data[index7] = (uint)num4;
                }
            }
            bigInteger5.dataLength = index1;
            while (bigInteger5.dataLength > 1 && (int)bigInteger5.data[bigInteger5.dataLength - 1] == 0)
                --bigInteger5.dataLength;
            BigInteger bigInteger6 = bigInteger4 - bigInteger5;
            if (((int)bigInteger6.data[69] & int.MinValue) != 0)
            {
                BigInteger bigInteger7 = new BigInteger();
                bigInteger7.data[index1] = 1U;
                bigInteger7.dataLength = index1 + 1;
                bigInteger6 += bigInteger7;
            }
            while (bigInteger6 >= n)
                bigInteger6 -= n;
            return bigInteger6;
        }

        public BigInteger gcd(BigInteger bi)
        {
            BigInteger bigInteger1 = ((int)this.data[69] & int.MinValue) == 0 ? this : -this;
            BigInteger bigInteger2 = ((int)bi.data[69] & int.MinValue) == 0 ? bi : -bi;
            BigInteger bigInteger3 = bigInteger2;
            while (bigInteger1.dataLength > 1 || bigInteger1.dataLength == 1 && (int)bigInteger1.data[0] != 0)
            {
                bigInteger3 = bigInteger1;
                bigInteger1 = bigInteger2 % bigInteger1;
                bigInteger2 = bigInteger3;
            }
            return bigInteger3;
        }

        public void genRandomBits(int bits, Random rand)
        {
            int num1 = bits >> 5;
            int num2 = bits & 31;
            if (num2 != 0)
                ++num1;
            if (num1 > 70)
                throw new ArithmeticException("Number of required bits > maxLength.");
            for (int index = 0; index < num1; ++index)
                this.data[index] = (uint)(rand.NextDouble() * 4294967296.0);
            for (int index = num1; index < 70; ++index)
                this.data[index] = 0U;
            if (num2 != 0)
            {
                uint num3 = (uint)(1 << num2 - 1);
                this.data[num1 - 1] |= num3;
                uint num4 = uint.MaxValue >> 32 - num2;
                this.data[num1 - 1] &= num4;
            }
            else
                this.data[num1 - 1] |= uint.MaxValue;
            this.dataLength = num1;
            if (this.dataLength != 0)
                return;
            this.dataLength = 1;
        }

        public int bitCount()
        {
            while (this.dataLength > 1 && (int)this.data[this.dataLength - 1] == 0)
                --this.dataLength;
            uint num1 = this.data[this.dataLength - 1];
            uint num2 = uint.MaxValue;
            int num3 = 32;
            while (num3 > 0 && ((int)num1 & (int)num2) == 0)
            {
                --num3;
                num2 >>= 1;
            }
            return num3 + (this.dataLength - 1 << 5);
        }

        public bool FermatLittleTest(int confidence)
        {
            BigInteger bigInteger1 = ((int)this.data[69] & int.MinValue) == 0 ? this : -this;
            if (bigInteger1.dataLength == 1)
            {
                if ((int)bigInteger1.data[0] == 0 || (int)bigInteger1.data[0] == 1)
                    return false;
                if ((int)bigInteger1.data[0] == 2 || (int)bigInteger1.data[0] == 3)
                    return true;
            }
            if (((int)bigInteger1.data[0] & 1) == 0)
                return false;
            int num1 = bigInteger1.bitCount();
            BigInteger bigInteger2 = new BigInteger();
            BigInteger exp = bigInteger1 - new BigInteger(1L);
            Random rand = new Random();
            for (int index = 0; index < confidence; ++index)
            {
                bool flag = false;
                while (!flag)
                {
                    int bits = 0;
                    while (bits < 2)
                        bits = (int)(rand.NextDouble() * (double)num1);
                    bigInteger2.genRandomBits(bits, rand);
                    int num2 = bigInteger2.dataLength;
                    if (num2 > 1 || num2 == 1 && (int)bigInteger2.data[0] != 1)
                        flag = true;
                }
                BigInteger bigInteger3 = bigInteger2.gcd(bigInteger1);
                if (bigInteger3.dataLength == 1 && (int)bigInteger3.data[0] != 1)
                    return false;
                BigInteger bigInteger4 = bigInteger2.modPow(exp, bigInteger1);
                int num3 = bigInteger4.dataLength;
                if (num3 > 1 || num3 == 1 && (int)bigInteger4.data[0] != 1)
                    return false;
            }
            return true;
        }

        public bool RabinMillerTest(int confidence)
        {
            BigInteger bigInteger1 = ((int)this.data[69] & int.MinValue) == 0 ? this : -this;
            if (bigInteger1.dataLength == 1)
            {
                if ((int)bigInteger1.data[0] == 0 || (int)bigInteger1.data[0] == 1)
                    return false;
                if ((int)bigInteger1.data[0] == 2 || (int)bigInteger1.data[0] == 3)
                    return true;
            }
            if (((int)bigInteger1.data[0] & 1) == 0)
                return false;
            BigInteger bigInteger2 = bigInteger1 - new BigInteger(1L);
            int num1 = 0;
            for (int index1 = 0; index1 < bigInteger2.dataLength; ++index1)
            {
                uint num2 = 1U;
                for (int index2 = 0; index2 < 32; ++index2)
                {
                    if (((int)bigInteger2.data[index1] & (int)num2) != 0)
                    {
                        index1 = bigInteger2.dataLength;
                        break;
                    }
                    num2 <<= 1;
                    ++num1;
                }
            }
            BigInteger exp = bigInteger2 >> num1;
            int num3 = bigInteger1.bitCount();
            BigInteger bigInteger3 = new BigInteger();
            Random rand = new Random();
            for (int index1 = 0; index1 < confidence; ++index1)
            {
                bool flag1 = false;
                while (!flag1)
                {
                    int bits = 0;
                    while (bits < 2)
                        bits = (int)(rand.NextDouble() * (double)num3);
                    bigInteger3.genRandomBits(bits, rand);
                    int num2 = bigInteger3.dataLength;
                    if (num2 > 1 || num2 == 1 && (int)bigInteger3.data[0] != 1)
                        flag1 = true;
                }
                BigInteger bigInteger4 = bigInteger3.gcd(bigInteger1);
                if (bigInteger4.dataLength == 1 && (int)bigInteger4.data[0] != 1)
                    return false;
                BigInteger bigInteger5 = bigInteger3.modPow(exp, bigInteger1);
                bool flag2 = false;
                if (bigInteger5.dataLength == 1 && (int)bigInteger5.data[0] == 1)
                    flag2 = true;
                for (int index2 = 0; !flag2 && index2 < num1; ++index2)
                {
                    if (bigInteger5 == bigInteger2)
                    {
                        flag2 = true;
                        break;
                    }
                    bigInteger5 = bigInteger5 * bigInteger5 % bigInteger1;
                }
                if (!flag2)
                    return false;
            }
            return true;
        }



        public bool LucasStrongTest()
        {
            BigInteger thisVal = ((int)this.data[69] & int.MinValue) == 0 ? this : -this;
            if (thisVal.dataLength == 1)
            {
                if ((int)thisVal.data[0] == 0 || (int)thisVal.data[0] == 1)
                    return false;
                if ((int)thisVal.data[0] == 2 || (int)thisVal.data[0] == 3)
                    return true;
            }
            if (((int)thisVal.data[0] & 1) == 0)
                return false;
            return this.LucasStrongTestHelper(thisVal);
        }

        private bool LucasStrongTestHelper(BigInteger thisVal)
        {
            long num1 = 5L;
            long num2 = -1L;
            long num3 = 0L;
            bool flag1 = false;
            while (!flag1)
            {
                int num4;
                switch (BigInteger.Jacobi((BigInteger)num1, thisVal))
                {
                    case -1:
                        flag1 = true;
                        goto label_11;
                    case 0:
                        num4 = !((BigInteger)Math.Abs(num1) < thisVal) ? 1 : 0;
                        break;
                    default:
                        num4 = 1;
                        break;
                }
                if (num4 == 0)
                    return false;
                if (num3 == 20L)
                {
                    BigInteger bigInteger = thisVal.sqrt();
                    if (bigInteger * bigInteger == thisVal)
                        return false;
                }
                num1 = (Math.Abs(num1) + 2L) * num2;
                num2 = -num2;
            label_11:
                ++num3;
            }
            long num5 = 1L - num1 >> 2;
            BigInteger bigInteger1 = thisVal + (BigInteger)1;
            int num6 = 0;
            for (int index1 = 0; index1 < bigInteger1.dataLength; ++index1)
            {
                uint num4 = 1U;
                for (int index2 = 0; index2 < 32; ++index2)
                {
                    if (((int)bigInteger1.data[index1] & (int)num4) != 0)
                    {
                        index1 = bigInteger1.dataLength;
                        break;
                    }
                    num4 <<= 1;
                    ++num6;
                }
            }
            BigInteger k = bigInteger1 >> num6;
            BigInteger bigInteger2 = new BigInteger();
            int index3 = thisVal.dataLength << 1;
            bigInteger2.data[index3] = 1U;
            bigInteger2.dataLength = index3 + 1;
            BigInteger constant = bigInteger2 / thisVal;
            BigInteger[] bigIntegerArray1 = BigInteger.LucasSequenceHelper((BigInteger)1, (BigInteger)num5, k, thisVal, constant, 0);
            bool flag2 = false;
            if (bigIntegerArray1[0].dataLength == 1 && (int)bigIntegerArray1[0].data[0] == 0 || bigIntegerArray1[1].dataLength == 1 && (int)bigIntegerArray1[1].data[0] == 0)
                flag2 = true;
            for (int index1 = 1; index1 < num6; ++index1)
            {
                if (!flag2)
                {
                    bigIntegerArray1[1] = thisVal.BarrettReduction(bigIntegerArray1[1] * bigIntegerArray1[1], thisVal, constant);
                    bigIntegerArray1[1] = (bigIntegerArray1[1] - (bigIntegerArray1[2] << 1)) % thisVal;
                    if (bigIntegerArray1[1].dataLength == 1 && (int)bigIntegerArray1[1].data[0] == 0)
                        flag2 = true;
                }
                bigIntegerArray1[2] = thisVal.BarrettReduction(bigIntegerArray1[2] * bigIntegerArray1[2], thisVal, constant);
            }
            if (flag2)
            {
                BigInteger bigInteger3 = thisVal.gcd((BigInteger)num5);
                if (bigInteger3.dataLength == 1 && (int)bigInteger3.data[0] == 1)
                {
                    if (((int)bigIntegerArray1[2].data[69] & int.MinValue) != 0)
                    {
                        BigInteger[] bigIntegerArray2;
                        (bigIntegerArray2 = bigIntegerArray1)[2] = bigIntegerArray2[2] + thisVal;
                    }
                    BigInteger bigInteger4 = (BigInteger)(num5 * (long)BigInteger.Jacobi((BigInteger)num5, thisVal)) % thisVal;
                    if (((int)bigInteger4.data[69] & int.MinValue) != 0)
                        bigInteger4 += thisVal;
                    if (bigIntegerArray1[2] != bigInteger4)
                        flag2 = false;
                }
            }
            return flag2;
        }

        public bool isProbablePrime(int confidence)
        {
            BigInteger bigInteger1 = ((int)this.data[69] & int.MinValue) == 0 ? this : -this;
            for (int index = 0; index < BigInteger.primesBelow2000.Length; ++index)
            {
                BigInteger bigInteger2 = (BigInteger)BigInteger.primesBelow2000[index];
                if (!(bigInteger2 >= bigInteger1))
                {
                    if ((bigInteger1 % bigInteger2).IntValue() == 0)
                        return false;
                }
                else
                    break;
            }
            return bigInteger1.RabinMillerTest(confidence);
        }



        public int IntValue()
        {
            return (int)this.data[0];
        }

        public long LongValue()
        {
            long num = (long)this.data[0];
            try
            {
                num |= (long)this.data[1] << 32;
            }
            catch (Exception ex)
            {
                if (((int)this.data[0] & int.MinValue) != 0)
                    num = (long)(int)this.data[0];
            }
            return num;
        }

        public static int Jacobi(BigInteger a, BigInteger b)
        {
            if (((int)b.data[0] & 1) == 0)
                throw new ArgumentException("Jacobi defined only for odd integers.");
            if (a >= b)
                a %= b;
            if (a.dataLength == 1 && (int)a.data[0] == 0)
                return 0;
            if (a.dataLength == 1 && (int)a.data[0] == 1)
                return 1;
            if (a < (BigInteger)0)
            {
                if (((int)(b - (BigInteger)1).data[0] & 2) == 0)
                    return BigInteger.Jacobi(-a, b);
                return -BigInteger.Jacobi(-a, b);
            }
            int num1 = 0;
            for (int index1 = 0; index1 < a.dataLength; ++index1)
            {
                uint num2 = 1U;
                for (int index2 = 0; index2 < 32; ++index2)
                {
                    if (((int)a.data[index1] & (int)num2) != 0)
                    {
                        index1 = a.dataLength;
                        break;
                    }
                    num2 <<= 1;
                    ++num1;
                }
            }
            BigInteger b1 = a >> num1;
            int num3 = 1;
            if ((num1 & 1) != 0 && (((int)b.data[0] & 7) == 3 || ((int)b.data[0] & 7) == 5))
                num3 = -1;
            if (((int)b.data[0] & 3) == 3 && ((int)b1.data[0] & 3) == 3)
                num3 = -num3;
            if (b1.dataLength == 1 && (int)b1.data[0] == 1)
                return num3;
            return num3 * BigInteger.Jacobi(b % b1, b1);
        }

        public static BigInteger genPseudoPrime(int bits, int confidence, Random rand)
        {
            BigInteger bigInteger = new BigInteger();
            for (bool flag = false; !flag; flag = bigInteger.isProbablePrime(confidence))
            {
                bigInteger.genRandomBits(bits, rand);
                bigInteger.data[0] |= 1U;
            }
            return bigInteger;
        }

        public BigInteger genCoPrime(int bits, Random rand)
        {
            bool flag = false;
            BigInteger bigInteger1 = new BigInteger();
            while (!flag)
            {
                bigInteger1.genRandomBits(bits, rand);
                BigInteger bigInteger2 = bigInteger1.gcd(this);
                if (bigInteger2.dataLength == 1 && (int)bigInteger2.data[0] == 1)
                    flag = true;
            }
            return bigInteger1;
        }

        public BigInteger modInverse(BigInteger modulus)
        {
            BigInteger[] bigIntegerArray1 = new BigInteger[2]
      {
        (BigInteger) 0,
        (BigInteger) 1
      };
            BigInteger[] bigIntegerArray2 = new BigInteger[2];
            BigInteger[] bigIntegerArray3 = new BigInteger[2]
      {
        (BigInteger) 0,
        (BigInteger) 0
      };
            int num = 0;
            BigInteger bi1 = modulus;
            BigInteger bi2 = this;
            while (bi2.dataLength > 1 || bi2.dataLength == 1 && (int)bi2.data[0] != 0)
            {
                BigInteger outQuotient = new BigInteger();
                BigInteger outRemainder = new BigInteger();
                if (num > 1)
                {
                    BigInteger bigInteger = (bigIntegerArray1[0] - bigIntegerArray1[1] * bigIntegerArray2[0]) % modulus;
                    bigIntegerArray1[0] = bigIntegerArray1[1];
                    bigIntegerArray1[1] = bigInteger;
                }
                if (bi2.dataLength == 1)
                    BigInteger.singleByteDivide(bi1, bi2, outQuotient, outRemainder);
                else
                    BigInteger.multiByteDivide(bi1, bi2, outQuotient, outRemainder);
                bigIntegerArray2[0] = bigIntegerArray2[1];
                bigIntegerArray3[0] = bigIntegerArray3[1];
                bigIntegerArray2[1] = outQuotient;
                bigIntegerArray3[1] = outRemainder;
                bi1 = bi2;
                bi2 = outRemainder;
                ++num;
            }
            if (bigIntegerArray3[0].dataLength > 1 || bigIntegerArray3[0].dataLength == 1 && (int)bigIntegerArray3[0].data[0] != 1)
                throw new ArithmeticException("No inverse!");
            BigInteger bigInteger1 = (bigIntegerArray1[0] - bigIntegerArray1[1] * bigIntegerArray2[0]) % modulus;
            if (((int)bigInteger1.data[69] & int.MinValue) != 0)
                bigInteger1 += modulus;
            return bigInteger1;
        }

        public byte[] getBytes()
        {
            int num1 = this.bitCount();
            int length = num1 >> 3;
            if ((num1 & 7) != 0)
                ++length;
            byte[] numArray = new byte[length];
            int index1 = 0;
            uint num2 = this.data[this.dataLength - 1];
            uint num3;
            if ((int)(num3 = num2 >> 24 & (uint)byte.MaxValue) != 0)
                numArray[index1++] = (byte)num3;
            uint num4;
            if ((int)(num4 = num2 >> 16 & (uint)byte.MaxValue) != 0 || index1 > 0)
                numArray[index1++] = (byte)num4;
            uint num5;
            if ((int)(num5 = num2 >> 8 & (uint)byte.MaxValue) != 0 || index1 > 0)
                numArray[index1++] = (byte)num5;
            uint num6;
            if ((int)(num6 = num2 & (uint)byte.MaxValue) != 0 || index1 > 0)
                numArray[index1++] = (byte)num6;
            int index2 = this.dataLength - 2;
            while (index2 >= 0)
            {
                uint num7 = this.data[index2];
                numArray[index1 + 3] = (byte)(num7 & (uint)byte.MaxValue);
                uint num8 = num7 >> 8;
                numArray[index1 + 2] = (byte)(num8 & (uint)byte.MaxValue);
                uint num9 = num8 >> 8;
                numArray[index1 + 1] = (byte)(num9 & (uint)byte.MaxValue);
                uint num10 = num9 >> 8;
                numArray[index1] = (byte)(num10 & (uint)byte.MaxValue);
                --index2;
                index1 += 4;
            }
            return numArray;
        }

        public void setBit(uint bitNum)
        {
            uint num1 = bitNum >> 5;
            uint num2 = 1U << (int)(byte)(bitNum & 31U);
            this.data[num1] |= num2;
            if ((long)num1 < (long)this.dataLength)
                return;
            this.dataLength = (int)num1 + 1;
        }

        public void unsetBit(uint bitNum)
        {
            uint num1 = bitNum >> 5;
            if ((long)num1 >= (long)this.dataLength)
                return;
            uint num2 = uint.MaxValue ^ 1U << (int)(byte)(bitNum & 31U);
            this.data[num1] &= num2;
            if (this.dataLength > 1 && (int)this.data[this.dataLength - 1] == 0)
                --this.dataLength;
        }

        public BigInteger sqrt()
        {
            uint num1 = (uint)this.bitCount();
            uint num2 = ((int)num1 & 1) == 0 ? num1 >> 1 : (num1 >> 1) + 1U;
            uint num3 = num2 >> 5;
            byte num4 = (byte)(num2 & 31U);
            BigInteger bigInteger = new BigInteger();
            uint num5;
            if ((int)num4 == 0)
            {
                num5 = uint.MaxValue;
            }
            else
            {
                num5 = 1U << (int)num4;
                ++num3;
            }
            bigInteger.dataLength = (int)num3;
            for (int index = (int)num3 - 1; index >= 0; --index)
            {
                while ((int)num5 != 0)
                {
                    bigInteger.data[index] ^= num5;
                    if (bigInteger * bigInteger > this)
                        bigInteger.data[index] ^= num5;
                    num5 >>= 1;
                }
                num5 = uint.MaxValue;
            }
            return bigInteger;
        }

        public static BigInteger[] LucasSequence(BigInteger P, BigInteger Q, BigInteger k, BigInteger n)
        {
            if (k.dataLength == 1 && (int)k.data[0] == 0)
                return new BigInteger[3]
        {
          (BigInteger) 0,
          (BigInteger) 2 % n,
          (BigInteger) 1 % n
        };
            BigInteger bigInteger = new BigInteger();
            int index1 = n.dataLength << 1;
            bigInteger.data[index1] = 1U;
            bigInteger.dataLength = index1 + 1;
            BigInteger constant = bigInteger / n;
            int s = 0;
            for (int index2 = 0; index2 < k.dataLength; ++index2)
            {
                uint num = 1U;
                for (int index3 = 0; index3 < 32; ++index3)
                {
                    if (((int)k.data[index2] & (int)num) != 0)
                    {
                        index2 = k.dataLength;
                        break;
                    }
                    num <<= 1;
                    ++s;
                }
            }
            BigInteger k1 = k >> s;
            return BigInteger.LucasSequenceHelper(P, Q, k1, n, constant, s);
        }

        private static BigInteger[] LucasSequenceHelper(BigInteger P, BigInteger Q, BigInteger k, BigInteger n, BigInteger constant, int s)
        {
            BigInteger[] bigIntegerArray = new BigInteger[3];
            if (((int)k.data[0] & 1) == 0)
                throw new ArgumentException("Argument k must be odd.");
            uint num = (uint)(1 << (k.bitCount() & 31) - 1);
            BigInteger bigInteger1 = (BigInteger)2 % n;
            BigInteger bigInteger2 = (BigInteger)1 % n;
            BigInteger bigInteger3 = P % n;
            BigInteger bigInteger4 = bigInteger2;
            bool flag = true;
            for (int index = k.dataLength - 1; index >= 0; --index)
            {
                while ((int)num != 0 && (index != 0 || (int)num != 1))
                {
                    if (((int)k.data[index] & (int)num) != 0)
                    {
                        bigInteger4 = bigInteger4 * bigInteger3 % n;
                        bigInteger1 = (bigInteger1 * bigInteger3 - P * bigInteger2) % n;
                        bigInteger3 = (n.BarrettReduction(bigInteger3 * bigInteger3, n, constant) - (bigInteger2 * Q << 1)) % n;
                        if (flag)
                            flag = false;
                        else
                            bigInteger2 = n.BarrettReduction(bigInteger2 * bigInteger2, n, constant);
                        bigInteger2 = bigInteger2 * Q % n;
                    }
                    else
                    {
                        bigInteger4 = (bigInteger4 * bigInteger1 - bigInteger2) % n;
                        bigInteger3 = (bigInteger1 * bigInteger3 - P * bigInteger2) % n;
                        bigInteger1 = (n.BarrettReduction(bigInteger1 * bigInteger1, n, constant) - (bigInteger2 << 1)) % n;
                        if (flag)
                        {
                            bigInteger2 = Q % n;
                            flag = false;
                        }
                        else
                            bigInteger2 = n.BarrettReduction(bigInteger2 * bigInteger2, n, constant);
                    }
                    num >>= 1;
                }
                num = uint.MaxValue;
            }
            BigInteger bigInteger5 = (bigInteger4 * bigInteger1 - bigInteger2) % n;
            BigInteger bigInteger6 = (bigInteger1 * bigInteger3 - P * bigInteger2) % n;
            if (flag)
                flag = false;
            else
                bigInteger2 = n.BarrettReduction(bigInteger2 * bigInteger2, n, constant);
            BigInteger bigInteger7 = bigInteger2 * Q % n;
            for (int index = 0; index < s; ++index)
            {
                bigInteger5 = bigInteger5 * bigInteger6 % n;
                bigInteger6 = (bigInteger6 * bigInteger6 - (bigInteger7 << 1)) % n;
                if (flag)
                {
                    bigInteger7 = Q % n;
                    flag = false;
                }
                else
                    bigInteger7 = n.BarrettReduction(bigInteger7 * bigInteger7, n, constant);
            }
            bigIntegerArray[0] = bigInteger5;
            bigIntegerArray[1] = bigInteger6;
            bigIntegerArray[2] = bigInteger7;
            return bigIntegerArray;
        }

        //public static void MulDivTest(int rounds)
        //{
        //    Random random = new Random();
        //    byte[] inData1 = new byte[64];
        //    byte[] inData2 = new byte[64];
        //    for (int index1 = 0; index1 < rounds; ++index1)
        //    {
        //        int inLen1 = 0;
        //        while (inLen1 == 0)
        //            inLen1 = (int)(random.NextDouble() * 65.0);
        //        int inLen2 = 0;
        //        while (inLen2 == 0)
        //            inLen2 = (int)(random.NextDouble() * 65.0);
        //        bool flag1 = false;
        //        while (!flag1)
        //        {
        //            for (int index2 = 0; index2 < 64; ++index2)
        //            {
        //                inData1[index2] = index2 >= inLen1 ? (byte)0 : (byte)(random.NextDouble() * 256.0);
        //                if ((int)inData1[index2] != 0)
        //                    flag1 = true;
        //            }
        //        }
        //        bool flag2 = false;
        //        while (!flag2)
        //        {
        //            for (int index2 = 0; index2 < 64; ++index2)
        //            {
        //                inData2[index2] = index2 >= inLen2 ? (byte)0 : (byte)(random.NextDouble() * 256.0);
        //                if ((int)inData2[index2] != 0)
        //                    flag2 = true;
        //            }
        //        }
        //        while ((int)inData1[0] == 0)
        //            inData1[0] = (byte)(random.NextDouble() * 256.0);
        //        while ((int)inData2[0] == 0)
        //            inData2[0] = (byte)(random.NextDouble() * 256.0);
        //        Console.WriteLine(index1);
        //        BigInteger bigInteger1 = new BigInteger(inData1, inLen1);
        //        BigInteger bigInteger2 = new BigInteger(inData2, inLen2);
        //        BigInteger bigInteger3 = bigInteger1 / bigInteger2;
        //        BigInteger bigInteger4 = bigInteger1 % bigInteger2;
        //        BigInteger bigInteger5 = bigInteger3 * bigInteger2 + bigInteger4;
        //        if (bigInteger5 != bigInteger1)
        //        {
        //            Console.WriteLine("Error at " + (object)index1);
        //            Console.WriteLine((string)(object)bigInteger1 + (object)"\n");
        //            Console.WriteLine((string)(object)bigInteger2 + (object)"\n");
        //            Console.WriteLine((string)(object)bigInteger3 + (object)"\n");
        //            Console.WriteLine((string)(object)bigInteger4 + (object)"\n");
        //            Console.WriteLine((string)(object)bigInteger5 + (object)"\n");
        //            break;
        //        }
        //    }
        //}

        //public static void RSATest(int rounds)
        //{
        //    Random random = new Random(1);
        //    byte[] inData = new byte[64];
        //    BigInteger exp1 = new BigInteger("a932b948feed4fb2b692609bd22164fc9edb59fae7880cc1eaff7b3c9626b7e5b241c27a974833b2622ebe09beb451917663d47232488f23a117fc97720f1e7", 16);
        //    BigInteger exp2 = new BigInteger("4adf2f7a89da93248509347d2ae506d683dd3a16357e859a980c4f77a4e2f7a01fae289f13a851df6e9db5adaa60bfd2b162bbbe31f7c8f828261a6839311929d2cef4f864dde65e556ce43c89bbbf9f1ac5511315847ce9cc8dc92470a747b8792d6a83b0092d2e5ebaf852c85cacf34278efa99160f2f8aa7ee7214de07b7", 16);
        //    BigInteger n = new BigInteger("e8e77781f36a7b3188d711c2190b560f205a52391b3479cdb99fa010745cbeba5f2adc08e1de6bf38398a0487c4a73610d94ec36f17f3f46ad75e17bc1adfec99839589f45f95ccc94cb2a5c500b477eb3323d8cfab0c8458c96f0147a45d27e45a4d11d54d77684f65d48f15fafcc1ba208e71e921b9bd9017c16a5231af7f", 16);
        //    Console.WriteLine("e =\n" + exp1.ToString(10));
        //    Console.WriteLine("\nd =\n" + exp2.ToString(10));
        //    Console.WriteLine("\nn =\n" + n.ToString(10) + "\n");
        //    for (int index1 = 0; index1 < rounds; ++index1)
        //    {
        //        int inLen = 0;
        //        while (inLen == 0)
        //            inLen = (int)(random.NextDouble() * 65.0);
        //        bool flag = false;
        //        while (!flag)
        //        {
        //            for (int index2 = 0; index2 < 64; ++index2)
        //            {
        //                inData[index2] = index2 >= inLen ? (byte)0 : (byte)(random.NextDouble() * 256.0);
        //                if ((int)inData[index2] != 0)
        //                    flag = true;
        //            }
        //        }
        //        while ((int)inData[0] == 0)
        //            inData[0] = (byte)(random.NextDouble() * 256.0);
        //        Console.Write("Round = " + (object)index1);
        //        BigInteger bigInteger = new BigInteger(inData, inLen);
        //        if (bigInteger.modPow(exp1, n).modPow(exp2, n) != bigInteger)
        //        {
        //            Console.WriteLine("\nError at round " + (object)index1);
        //            Console.WriteLine((string)(object)bigInteger + (object)"\n");
        //            break;
        //        }
        //        Console.WriteLine(" <PASSED>.");
        //    }
        //}

      //  public static void RSATest2(int rounds)
      //  {
      //      Random rand = new Random();
      //      byte[] inData1 = new byte[64];
      //      byte[] inData2 = new byte[64]
      //{
      //  (byte) 133,
      //  (byte) 132,
      //  (byte) 100,
      //  (byte) 253,
      //  (byte) 112,
      //  (byte) 106,
      //  (byte) 159,
      //  (byte) 240,
      //  (byte) 148,
      //  (byte) 12,
      //  (byte) 62,
      //  (byte) 44,
      //  (byte) 116,
      //  (byte) 52,
      //  (byte) 5,
      //  (byte) 201,
      //  (byte) 85,
      //  (byte) 179,
      //  (byte) 133,
      //  (byte) 50,
      //  (byte) 152,
      //  (byte) 113,
      //  (byte) 249,
      //  (byte) 65,
      //  (byte) 33,
      //  (byte) 95,
      //  (byte) 2,
      //  (byte) 158,
      //  (byte) 234,
      //  (byte) 86,
      //  (byte) 141,
      //  (byte) 140,
      //  (byte) 68,
      //  (byte) 204,
      //  (byte) 238,
      //  (byte) 238,
      //  (byte) 61,
      //  (byte) 44,
      //  (byte) 157,
      //  (byte) 44,
      //  (byte) 18,
      //  (byte) 65,
      //  (byte) 30,
      //  (byte) 241,
      //  (byte) 197,
      //  (byte) 50,
      //  (byte) 195,
      //  (byte) 170,
      //  (byte) 49,
      //  (byte) 74,
      //  (byte) 82,
      //  (byte) 216,
      //  (byte) 232,
      //  (byte) 175,
      //  (byte) 66,
      //  (byte) 244,
      //  (byte) 114,
      //  (byte) 161,
      //  (byte) 42,
      //  (byte) 13,
      //  (byte) 151,
      //  (byte) 177,
      //  (byte) 49,
      //  (byte) 179
      //};
      //      byte[] inData3 = new byte[64]
      //{
      //  (byte) 153,
      //  (byte) 152,
      //  (byte) 202,
      //  (byte) 184,
      //  (byte) 94,
      //  (byte) 215,
      //  (byte) 229,
      //  (byte) 220,
      //  (byte) 40,
      //  (byte) 92,
      //  (byte) 111,
      //  (byte) 14,
      //  (byte) 21,
      //  (byte) 9,
      //  (byte) 89,
      //  (byte) 110,
      //  (byte) 132,
      //  (byte) 243,
      //  (byte) 129,
      //  (byte) 205,
      //  (byte) 222,
      //  (byte) 66,
      //  (byte) 220,
      //  (byte) 147,
      //  (byte) 194,
      //  (byte) 122,
      //  (byte) 98,
      //  (byte) 172,
      //  (byte) 108,
      //  (byte) 175,
      //  (byte) 222,
      //  (byte) 116,
      //  (byte) 227,
      //  (byte) 203,
      //  (byte) 96,
      //  (byte) 32,
      //  (byte) 56,
      //  (byte) 156,
      //  (byte) 33,
      //  (byte) 195,
      //  (byte) 220,
      //  (byte) 200,
      //  (byte) 162,
      //  (byte) 77,
      //  (byte) 198,
      //  (byte) 42,
      //  (byte) 53,
      //  (byte) 127,
      //  (byte) 243,
      //  (byte) 169,
      //  (byte) 232,
      //  (byte) 29,
      //  (byte) 123,
      //  (byte) 44,
      //  (byte) 120,
      //  (byte) 250,
      //  (byte) 184,
      //  (byte) 2,
      //  (byte) 85,
      //  (byte) byte.MaxValue,
      //  (byte) 155,
      //  (byte) 194,
      //  (byte) 165,
      //  (byte) 203
      //};
      //      BigInteger bigInteger1 = new BigInteger(inData2);
      //      BigInteger bigInteger2 = new BigInteger(inData3);
      //      BigInteger modulus = (bigInteger1 - (BigInteger)1) * (bigInteger2 - (BigInteger)1);
      //      BigInteger n = bigInteger1 * bigInteger2;
      //      for (int index1 = 0; index1 < rounds; ++index1)
      //      {
      //          BigInteger exp1 = modulus.genCoPrime(512, rand);
      //          BigInteger exp2 = exp1.modInverse(modulus);
      //          Console.WriteLine("\ne =\n" + exp1.ToString(10));
      //          Console.WriteLine("\nd =\n" + exp2.ToString(10));
      //          Console.WriteLine("\nn =\n" + n.ToString(10) + "\n");
      //          int inLen = 0;
      //          while (inLen == 0)
      //              inLen = (int)(rand.NextDouble() * 65.0);
      //          bool flag = false;
      //          while (!flag)
      //          {
      //              for (int index2 = 0; index2 < 64; ++index2)
      //              {
      //                  inData1[index2] = index2 >= inLen ? (byte)0 : (byte)(rand.NextDouble() * 256.0);
      //                  if ((int)inData1[index2] != 0)
      //                      flag = true;
      //              }
      //          }
      //          while ((int)inData1[0] == 0)
      //              inData1[0] = (byte)(rand.NextDouble() * 256.0);
      //          Console.Write("Round = " + (object)index1);
      //          BigInteger bigInteger3 = new BigInteger(inData1, inLen);
      //          if (bigInteger3.modPow(exp1, n).modPow(exp2, n) != bigInteger3)
      //          {
      //              Console.WriteLine("\nError at round " + (object)index1);
      //              Console.WriteLine((string)(object)bigInteger3 + (object)"\n");
      //              break;
      //          }
      //          Console.WriteLine(" <PASSED>.");
      //      }
      //  }

        //public static void SqrtTest(int rounds)
        //{
        //    Random rand = new Random();
        //    for (int index = 0; index < rounds; ++index)
        //    {
        //        int bits = 0;
        //        while (bits == 0)
        //            bits = (int)(rand.NextDouble() * 1024.0);
        //        Console.Write("Round = " + (object)index);
        //        BigInteger bigInteger1 = new BigInteger();
        //        bigInteger1.genRandomBits(bits, rand);
        //        BigInteger bigInteger2 = bigInteger1.sqrt();
        //        if ((bigInteger2 + (BigInteger)1) * (bigInteger2 + (BigInteger)1) <= bigInteger1)
        //        {
        //            Console.WriteLine("\nError at round " + (object)index);
        //            Console.WriteLine((string)(object)bigInteger1 + (object)"\n");
        //            break;
        //        }
        //        Console.WriteLine(" <PASSED>.");
        //    }
        //}

    }

}
