using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using Avalonia.Controls;
using RAPTOR_Avalonia_MVVM.ViewModels;
using ReactiveUI;
using System.Reactive;

namespace raptor
{
    class Generators
    {
        private static System.Collections.Generic.Dictionary<string, System.Type> Generator_List =
            new Dictionary<string, System.Type>();
        //public static bool Handles_OO(string name)
        //{
        //    System.Type type = Generator_List[name];
        //    return type.GetInterface(
        //        typeof(GeneratorAda.OO_Interface).FullName) != null;
        //}
        public static bool Handles_Imperative(string name)
        {
            System.Type type = Generator_List[name];
            return type.GetInterface(
                typeof(CodeGenerators.Imperative_Interface).FullName) != null;
        }
        public static Generate_Interface Create_From_Menu(string name, string filename)
        {

            System.Type type = Generator_List[name];
            System.Type[] param_types = new Type[1];
            Generate_Interface result = null;

            object[] parameters = new object[1];
            param_types[0] = typeof(string);
            System.Reflection.ConstructorInfo constructor = type.GetConstructor(param_types);
            parameters[0] = filename;
            result = constructor.Invoke(parameters) as Generate_Interface;


            return result;
        }

        public static void generateCodeCommand()
        {
            MainWindowViewModel mw = MainWindowViewModel.GetMainWindowViewModel();
            if (mw.fileName == null || mw.fileName == "" || lang == "")
            {
                return;
            }
            Generate_Interface gi = raptor.Generators.Create_From_Menu(lang, mw.fileName);
            Compile_Helpers.Do_Compilation(mw.mainSubchart().Start, gi, mw.theTabs);

        }

        public static string lang = "";

        public static ReactiveCommand<Unit, Unit> genCodeCommand { get; set; }

        public static void Process_Assembly(System.Reflection.Assembly assembly)
        {
            System.Type[] Types = assembly.GetTypes();
            int placement = 0;
            for (int k = 0; k < Types.Length; k++)
            {
                if (Types[k].GetInterface(typeof(Generate_Interface).FullName) != null)
                {
                    try
                    {
                        object obj;
                        MethodInfo mi = Types[k].GetMethod("Get_Menu_Name");
                        System.Reflection.ConstructorInfo constructor = Types[k].GetConstructor(System.Type.EmptyTypes);
                        obj = constructor.Invoke(null);
                        string name = mi.Invoke(obj, null) as string;

                        lang = name;

                        MainWindowViewModel mw = MainWindowViewModel.GetMainWindowViewModel();

                        genCodeCommand = ReactiveCommand.Create(generateCodeCommand);
                        MenuItem menu_item = new MenuItem() { Header = name.Replace("&", ""), Command = ReactiveCommand.Create(generateCodeCommand) };

                            /*(name, new EventHandler(
                            form.handle_click));*/
                        if (mw.GenerateMenuItems.Count > 1)
                        {
                            // add this in sorted order to the list
                            // make sure to get rid of the "&", which messes up alpha order
                            while ((placement < mw.GenerateMenuItems.Count) &&
                                (name.Replace("&", "").CompareTo(
                                  ((string)mw.GenerateMenuItems[placement].Header).Replace("&", "")) > 0))
                            {
                                placement++;
                            }
                        }
                        mw.GenerateMenuItems.Insert(placement, menu_item); 

                        Generator_List.Add(name, Types[k]);
                    }
                    catch
                    {
                    }
                }
            }
        }

        public static void Load_Generators()
        {

            System.IO.DirectoryInfo exe_path = System.IO.Directory.GetParent(Assembly.GetEntryAssembly().Location);
            System.IO.FileInfo[] files = exe_path.GetFiles("CodeGenerators*.dll");
            Assembly assembly;
            for (int i = 0; i < files.Length; i++)
            {
                try
                {
                    assembly = Assembly.LoadFrom(files[i].FullName);
                    Process_Assembly(assembly);
                }
                catch(Exception e)
                {
                    Runtime.consoleWriteln(e.Message);
                }
            }
        }
    }
}
