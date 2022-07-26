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
		public static raptor_files myRaptor_files;

		public raptor_files()
		{
			myRaptor_files = this;
		}

		public bool Input_Is_Redirected = false;
		public bool Output_Is_Redirected = false;
		private StreamReader input_stream;
		private StreamWriter output_stream;

		public static void writeln(string text)
        {
			myRaptor_files.output_stream.Write(text);
		}
		public static void write(string text)
        {
			myRaptor_files.output_stream.WriteLine(text);
		}
		public static string read()
        {
			return myRaptor_files.input_stream.ReadLine();
        }
		public static void redirect_output(string filename)
        {
			if (myRaptor_files.Output_Is_Redirected)
            {
				Stop_Redirect_Output();
            }
			myRaptor_files.output_stream = new StreamWriter(filename);
        }

		public static void redirect_input(string filename)
		{
			if (myRaptor_files.Input_Is_Redirected)
			{
				Stop_Redirect_Input();
			}
			myRaptor_files.input_stream = new StreamReader(filename);
		}

		public static bool output_redirected()
        {
			return myRaptor_files.Output_Is_Redirected;
        }

		public static bool input_redirected()
		{
			return myRaptor_files.Input_Is_Redirected;
		}

		public static void Stop_Redirect_Input()
		{
			if (myRaptor_files.Input_Is_Redirected)
			{
				myRaptor_files.Input_Is_Redirected = false;
				if (myRaptor_files.input_stream != null)
				{
					myRaptor_files.input_stream.Close();
				}
			}
		}

		public static void Stop_Redirect_Output()
		{
			if (myRaptor_files.Output_Is_Redirected)
			{
				myRaptor_files.Output_Is_Redirected = false;
				if (myRaptor_files.output_stream != null)
				{
					myRaptor_files.output_stream.Close();
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

