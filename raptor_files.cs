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
		public static raptor_files myRaptor_files = new raptor_files();

		public raptor_files()
		{

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

	}
}

