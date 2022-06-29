using System;
using System.Collections.Generic;
using System.Text;

namespace raptor
{
    public class Runtime_Helpers
    {
        public static numbers.value Get_Value_String(
             numbers.value s, numbers.value value_index)
        {
            if (!numbers.Numbers.is_integer(value_index))
            {
                throw new Exception(numbers.Numbers.msstring_view_image(value_index) +
                    " is not a valid string index.");
            }
            int index = numbers.Numbers.integer_of(value_index);
            char c = s.S[index - 1];
            return numbers.Numbers.make_character_value(c);
        }
        public static void Set_Value_String(
            numbers.value s,
            numbers.value value_index,
            numbers.value v)
        {
            if (!numbers.Numbers.is_integer(value_index))
            {
                throw new Exception(numbers.Numbers.msstring_view_image(value_index) +
                    " is not a valid string index.");
            }
            int index = numbers.Numbers.integer_of(value_index);
            if (index > s.S.Length)
            {
                s.S = s.S +
                    new String(' ', index - s.S.Length - 1) +
                    (char)numbers.Numbers.integer_of(v);
            }
            else
            {
                s.S = s.S.Remove(index - 1, 1)
                    .Insert(index - 1, "" +
                    (char)numbers.Numbers.integer_of(v));
            }
        }

    }
    public class Value_Array
    {
        private System.Collections.ArrayList values;
        public Value_Array()
        {
            values = new System.Collections.ArrayList();
        }
        public int Get_Length()
        {
            return values.Count;
        }
        public void Set_Value(numbers.value value_index, numbers.value v)
        {
            if (!numbers.Numbers.is_integer(value_index))
            {
                throw new Exception(numbers.Numbers.msstring_view_image(value_index) +
                    " is not a valid array index.");
            }
            int index = numbers.Numbers.integer_of(value_index);

            if (index > values.Count)
            {
                for (int i = values.Count; i <= index - 1; i++)
                {
                    values.Add(numbers.Numbers.make_value__2(0.0));
                }
            }
            try
            {
                //values[index - 1] = v._deep_clone();
                values[index - 1] = Runtime.getValueArray((Variable)v.Object);

            }
            catch
            {
                System.Exception f = new System.Exception("can't do array assign to: " +
                    (index - 1));
                throw f;
            }
        }
        public numbers.value Get_Value(numbers.value value_index)
        {
            if (!numbers.Numbers.is_integer(value_index))
            {
                throw new Exception(numbers.Numbers.msstring_view_image(value_index) +
                    " is not a valid array index.");
            }
            int index = numbers.Numbers.integer_of(value_index);
            return (numbers.value)values[index - 1];
        }
        public double[] get_Doublea()
        {
            int row_count = values.Count;
            double[] result = new double[row_count];
            for (int i = 0; i < row_count; i++)
            {
                result[i] = (double)((numbers.value)(values[i])).V;
            }
            return result;
        }
        public float[] get_Singlea()
        {
            int row_count = values.Count;
            float[] result = new float[row_count];
            for (int i = 0; i < row_count; i++)
            {
                result[i] = (float)((numbers.value)(values[i])).V;
            }
            return result;
        }
        public int[] get_Int32a()
        {
            int row_count = values.Count;
            int[] result = new int[row_count];
            for (int i = 0; i < row_count; i++)
            {
                result[i] = numbers.Numbers.integer_of(
                    (numbers.value)(values[i]));
            }
            return result;
        }
        public void set_Int32a(int[] values)
        {
            for (int i = values.Length - 1; i >= 0; i--)
            {
                this.Set_Value(numbers.Numbers.make_value__3(i + 1),
                    numbers.Numbers.make_value__3(values[i]));
            }
        }
        public void set_Singlea(float[] values)
        {
            for (int i = values.Length - 1; i >= 0; i--)
            {
                this.Set_Value(numbers.Numbers.make_value__3(i + 1),
                    numbers.Numbers.make_value__2(values[i]));
            }
        }
        public void set_Doublea(double[] values)
        {
            for (int i = values.Length - 1; i >= 0; i--)
            {
                this.Set_Value(numbers.Numbers.make_value__3(i + 1),
                    numbers.Numbers.make_value__2(values[i]));
            }
        }
    }
    public class Value_2D_Array
    {
        private System.Collections.ArrayList values;
        public Value_2D_Array()
        {
            values = new System.Collections.ArrayList();
        }
        public void Set_Value(numbers.value value_index1, numbers.value value_index2, numbers.value v)
        {
            if (!numbers.Numbers.is_integer(value_index1))
            {
                throw new Exception(numbers.Numbers.msstring_view_image(value_index1) +
                    " is not a valid array index.");
            }
            if (!numbers.Numbers.is_integer(value_index2))
            {
                throw new Exception(numbers.Numbers.msstring_view_image(value_index2) +
                    " is not a valid array index.");
            }
            int index1 = numbers.Numbers.integer_of(value_index1);
            int index2 = numbers.Numbers.integer_of(value_index2);
            int num_rows = values.Count;
            int num_cols = 0;

            if (num_rows > 0)
            {
                num_cols = ((System.Collections.ArrayList)values[0]).Count;
            }
            // add rows as needed
            if (index1 > num_rows)
            {
                for (int i = num_rows; i <= index1 - 1; i++)
                {
                    values.Add(new System.Collections.ArrayList());
                    for (int j = 0; j < num_cols; j++)
                    {
                        ((System.Collections.ArrayList)values[i]).Add(numbers.Numbers.make_value__2(0.0));
                    }
                }
                num_rows = index1;
            }
            // add columns as needed
            if (index2 > num_cols)
            {
                for (int i = 0; i < num_rows; i++)
                {
                    for (int j = num_cols; j <= index2 - 1; j++)
                    {
                        ((System.Collections.ArrayList)values[i]).Add(numbers.Numbers.make_value__2(0.0));
                    }
                }
            }
            try
            {
                //((System.Collections.ArrayList)values[index1 - 1])[index2 - 1] = v._deep_clone();
                ((System.Collections.ArrayList)values[index1 - 1])[index2 - 1] = Runtime.getValueArray((Variable)v.Object);
            }
            catch
            {
                System.Exception f = new System.Exception("can't do 2D assign to: " +
                    (index1 - 1) + "," + (index2 - 1));
                throw f;
            }
        }
        public numbers.value Get_Value(numbers.value value_index1, numbers.value value_index2)
        {
            if (!numbers.Numbers.is_integer(value_index1))
            {
                throw new Exception(numbers.Numbers.msstring_view_image(value_index1) +
                    " is not a valid array index.");
            }
            if (!numbers.Numbers.is_integer(value_index2))
            {
                throw new Exception(numbers.Numbers.msstring_view_image(value_index2) +
                    " is not a valid array index.");
            }
            int index1 = numbers.Numbers.integer_of(value_index1);
            int index2 = numbers.Numbers.integer_of(value_index2);
            return (numbers.value)((System.Collections.ArrayList)values[index1 - 1])[index2 - 1];
        }
        public double[][] get_Doubleaa()
        {
            int row_count = values.Count;
            int col_count = ((System.Collections.ArrayList)values[0]).Count;
            double[][] result = new double[row_count][];
            for (int i = 0; i < row_count; i++)
            {
                result[i] = new double[col_count];
                for (int j = 0; j < col_count; j++)
                {
                    result[i][j] = (double)((numbers.value)((System.Collections.ArrayList)values[i])[j]).V;
                }
            }
            return result;
        }
        public float[][] get_Singleaa()
        {
            int row_count = values.Count;
            int col_count = ((System.Collections.ArrayList)values[0]).Count;
            float[][] result = new float[row_count][];
            for (int i = 0; i < row_count; i++)
            {
                result[i] = new float[col_count];
                for (int j = 0; j < col_count; j++)
                {
                    result[i][j] = (float)((numbers.value)((System.Collections.ArrayList)values[i])[j]).V;
                }
            }
            return result;
        }
        public int[][] get_Int32aa()
        {
            int row_count = values.Count;
            int col_count = ((System.Collections.ArrayList)values[0]).Count;
            int[][] result = new int[row_count][];
            for (int i = 0; i < row_count; i++)
            {
                result[i] = new int[col_count];
                for (int j = 0; j < col_count; j++)
                {
                    result[i][j] = numbers.Numbers.integer_of(
                        (numbers.value)((System.Collections.ArrayList)values[i])[j]);
                }
            }
            return result;
        }
        public void set_Int32aa(int[][] values)
        {
            for (int i = values.Length - 1; i >= 0; i--)
            {
                for (int j = values[0].Length - 1; j >= 0; j--)
                {
                    this.Set_Value(numbers.Numbers.make_value__3(i + 1),
                        numbers.Numbers.make_value__3(j + 1),
                        numbers.Numbers.make_value__3(values[i][j]));
                }
            }
        }
        public void set_Singleaa(float[][] values)
        {
            for (int i = values.Length - 1; i >= 0; i--)
            {
                for (int j = values[0].Length - 1; j >= 0; j--)
                {
                    this.Set_Value(numbers.Numbers.make_value__3(i + 1),
                        numbers.Numbers.make_value__3(j + 1),
                        numbers.Numbers.make_value__2(values[i][j]));
                }
            }
        }
        public void set_Doubleaa(double[][] values)
        {
            for (int i = values.Length - 1; i >= 0; i--)
            {
                for (int j = values[0].Length - 1; j >= 0; j--)
                {
                    this.Set_Value(numbers.Numbers.make_value__3(i + 1),
                        numbers.Numbers.make_value__3(j + 1),
                        numbers.Numbers.make_value__2(values[i][j]));
                }
            }
        }
    }

}
