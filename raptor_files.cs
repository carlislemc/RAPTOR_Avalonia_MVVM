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


		public static bool output_redirected()
        {
			return myRaptor_files.Output_Is_Redirected;
        }

		public static bool input_redirected()
		{
			return myRaptor_files.Input_Is_Redirected;
		}

		public void Stop_Redirect_Input()
        {
            if (Input_Is_Redirected)
            {
				Input_Is_Redirected = false;
            }
        }

		public void Stop_Redirect_Output()
		{
			if (Output_Is_Redirected)
			{
				Output_Is_Redirected = false;
			}
		}

		public static void close_files()
        {
			myRaptor_files.Stop_Redirect_Input();
			myRaptor_files.Stop_Redirect_Output();
		}

	}
}

