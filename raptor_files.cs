using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using raptor;
using RAPTOR_Avalonia_MVVM;
using System.Collections;
using RAPTOR_Avalonia_MVVM.ViewModels;
using RAPTOR_Avalonia_MVVM.Views;
using System.Collections.ObjectModel;
using RAPTOR_Avalonia_MVVM.Controls;
using Avalonia.Threading;
using System.IO;


namespace RAPTOR_Avalonia_MVVM
{
	public class raptor_files
	{

		public static bool Input_Is_Redirected = false;
		public static bool Output_Is_Redirected = false;
		private static StreamReader input_stream;
		private static StreamWriter output_stream;

		public static void writeln(string text)
        {
			output_stream.Write(text);
		}
		public static void write(string text)
        {
			output_stream.WriteLine(text);
		}
		public static string read()
        {
			return input_stream.ReadLine();
        }
		public static void redirect_output(string filename)
        {
			if (Output_Is_Redirected)
            {
				Stop_Redirect_Output();
            }
			output_stream = new StreamWriter(filename);
        }

		public static void redirect_input(string filename)
		{
			if (Input_Is_Redirected)
			{
				Stop_Redirect_Input();
			}
			input_stream = new StreamReader(filename);
		}

		public static bool output_redirected()
        {
			return Output_Is_Redirected;
        }

		public static bool input_redirected()
		{
			return Input_Is_Redirected;
		}

		public static void Stop_Redirect_Input()
		{
			if (Input_Is_Redirected)
			{
				Input_Is_Redirected = false;
				if (input_stream != null)
				{
					input_stream.Close();
				}
			}
		}

		public static void Stop_Redirect_Output()
		{
			if (Output_Is_Redirected)
			{
				Output_Is_Redirected = false;
				if (output_stream != null)
				{
					output_stream.Close();
				}
			}
		}

		public static void close_files()
        {
			Stop_Redirect_Input();
			Stop_Redirect_Output();
		}

	}
}

