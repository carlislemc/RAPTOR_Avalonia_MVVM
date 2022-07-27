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
using Avalonia.Controls;

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
		public static void redirect_output(numbers.value filename)
        {
			if (Output_Is_Redirected)
            {
				Stop_Redirect_Output();
            }

			if (filename.Kind == numbers.Value_Kind.String_Kind)
			{
				output_stream = new StreamWriter(filename.S);
				Output_Is_Redirected = true;
			}
			else
			{
				if (filename.V == 1)
				{
					SaveFileDialog fileChooser = new SaveFileDialog();
					List<FileDialogFilter> Filters = new List<FileDialogFilter>();
					FileDialogFilter filter = new FileDialogFilter();
					List<string> extension = new List<string>();
					extension.Add("txt");
					filter.Extensions = extension;
					filter.Name = "Text Files";
					Filters.Add(filter);
					fileChooser.Filters = Filters;

					fileChooser.DefaultExtension = "txt";

					string ans = "";
					Dispatcher.UIThread.InvokeAsync(async () =>
					{
						ans = await fileChooser.ShowAsync(MainWindow.topWindow);

					}).Wait(-1);

					if (ans == null || ans == "")
					{
						return;
					}

					output_stream = new StreamWriter(ans);
				}
				else
				{
					Stop_Redirect_Output();
					Output_Is_Redirected = false;
				}

			}

		}

		public static void redirect_output_append(numbers.value filename)
		{
			if (Output_Is_Redirected)
			{
				Stop_Redirect_Output();
			}

			if (filename.Kind == numbers.Value_Kind.String_Kind)
			{
				output_stream = new StreamWriter(filename.S);
				Output_Is_Redirected = true;
			}
			else
			{
				if (filename.V == 1)
				{
					OpenFileDialog dialog = new OpenFileDialog();
					dialog.Filters.Add(new FileDialogFilter() { Name = "Text Files", Extensions = { "txt" } });
					dialog.Filters.Add(new FileDialogFilter() { Name = "All Files", Extensions = { "*" } });
					dialog.AllowMultiple = false;

					string[] result = { };

					Dispatcher.UIThread.InvokeAsync(async () =>
					{
						result = await dialog.ShowAsync(MainWindow.topWindow);

					}).Wait(-1);

					if (result == null || result[0] == "")
					{
						return;
					}

					output_stream = new StreamWriter(result[0]);
					Output_Is_Redirected = true;
				}
				else
				{
					Stop_Redirect_Output();
					Output_Is_Redirected = false;
				}

			}

		}

		public static void redirect_input(numbers.value filename)
		{
            if (Input_Is_Redirected)
            {
                Stop_Redirect_Input();
            }

            if (filename.Kind == numbers.Value_Kind.String_Kind)
			{
				input_stream = new StreamReader(filename.S);
				Input_Is_Redirected = true;
			}
            else
            {
				if(filename.V == 1) { 
					OpenFileDialog dialog = new OpenFileDialog();
					dialog.Filters.Add(new FileDialogFilter() { Name = "Text Files", Extensions = { "txt" } });
					dialog.Filters.Add(new FileDialogFilter() { Name = "All Files", Extensions = { "*" } });
					dialog.AllowMultiple = false;

					string[] result = { };

					Dispatcher.UIThread.InvokeAsync(async () =>
					{
						result = await dialog.ShowAsync(MainWindow.topWindow);

					}).Wait(-1);

					if (result == null || result[0] == "")
					{
						return;
					}

					input_stream = new StreamReader(result[0]);
					Input_Is_Redirected = true;
				}
				else
				{
					Stop_Redirect_Input();
					Input_Is_Redirected = false;
				}

			}
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

