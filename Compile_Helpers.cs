using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RAPTOR_Avalonia_MVVM;
using RAPTOR_Avalonia_MVVM.ViewModels;

namespace raptor
{
    /// <summary>
    /// Summary description for Compile_Helpers.
    /// </summary>
    public class Compile_Helpers
    {
        public Compile_Helpers()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private static System.Collections.Hashtable declarations = new System.Collections.Hashtable();

        // returns true if already declared
        public static bool Start_New_Declaration(string name)
        {
            bool result;
            if (name.ToLower().Equals("this") || name.ToLower().Equals("super"))
                return true;
            result = declarations.ContainsKey(name.ToLower());
            if (!result)
            {
                declarations.Add(name.ToLower(), null);
            }
            return result;
        }
        public static void Do_Compilation(Oval start, Generate_Interface gil,
            ObservableCollection<Subchart> tpc)
        {
            if (Component.Current_Mode != Mode.Expert)
            {
                Do_Compilation_Imperative(start, (CodeGenerators.Imperative_Interface)gil, tpc);
            }
            //else
            //{
            //    try
            //    {
            //        Do_Compilation_OO(start, gil as CodeGenerators.OO_Interface, tpc);
            //    }
            //    catch
            //    {
            //        (gil as CodeGenerators.OO_Interface).abort();
            //        throw;
            //    }
            //}
        }
        //private static void Do_Compilation_OO(Oval start, CodeGenerators.OO_Interface gil, TabControl.TabPageCollection tpc)
        //{
        //    _tpc = tpc;
        //    foreach (NClass.Core.IEntity ie in Runtime.parent.projectCore.Entities)
        //    {
        //        if (ie is NClass.Core.InterfaceType)
        //        {
        //            gil.start_interface(ie as NClass.Core.InterfaceType);
        //            foreach (NClass.Core.Operation o in
        //                (ie as NClass.Core.InterfaceType).Operations)
        //            {
        //                if (o is NClass.Core.Method)
        //                {
        //                    gil.declare_interface_method(o as NClass.Core.Method);
        //                }
        //            }
        //            gil.done_interface(ie as NClass.Core.InterfaceType);
        //        }
        //    }
        //    foreach (ClassTabPage ctp in allClasses(tpc))
        //    {
        //        gil.declare_class(ctp.ct);
        //        foreach (Procedure_Chart pc in allMethods(ctp))
        //        {
        //            NClass.Core.Method method = pc.method;
        //            gil.declare_method(method);
        //        }
        //    }
        //    foreach (ClassTabPage ctp in allClasses(tpc))
        //    {
        //        NClass.Core.ClassType ct = ctp.ct;
        //        gil.start_class(ct);
        //        foreach (NClass.Core.Field f in ct.Fields)
        //        {
        //            gil.declare_field(f);
        //        }

        //        foreach (NClass.Core.Operation o in ct.Operations)
        //        {
        //            if ((o is NClass.Core.Method) &&
        //                o.IsAbstract)
        //            {
        //                gil.declare_abstract_method(o as NClass.Core.Method);
        //            }
        //        }
        //        foreach (Procedure_Chart pc in allMethods(ctp))
        //        {
        //            NClass.Core.Method method = pc.method;
        //            gil.start_method(method);
        //            declarations.Clear();
        //            pc.Start.compile_pass1(gil);
        //            gil.Done_Variable_Declarations();
        //            pc.Start.Emit_Code(gil);
        //            gil.Done_Method();
        //        }
        //        gil.done_class(ctp.ct);
        //    }

        //    gil.Start_Method("Main");
        //    declarations.Clear();
        //    start.compile_pass1(gil);
        //    gil.Done_Variable_Declarations();
        //    start.Emit_Code(gil);
        //    gil.Done_Method();
        //    gil.Finish();
        //}f

        private static void Do_Compilation_Imperative(Oval start,
            CodeGenerators.Imperative_Interface gil, ObservableCollection<Subchart> tpc)
        {
            _tpc = tpc;
            for (int i = 1; i < tpc.Count; i++)
            {
                Procedure_Chart pc;
                if (tpc[i] is Procedure_Chart)
                {
                    pc = tpc[i] as Procedure_Chart;
                    gil.Declare_Procedure(pc.Text.Trim(),
                        ((Oval_Procedure)pc.Start).getArgs(), ((Oval_Procedure)pc.Start).getArgIsInput(), ((Oval_Procedure)pc.Start).getArgIsOutput());
                }
            }


            // Use the utility method to generate the IL instructions that print a string to the console.

            // emit methods for procedures
            for (int i = 1; i < tpc.Count; i++)
            {
                Procedure_Chart pc;
                if (tpc[i] is Procedure_Chart)
                {
                    pc = tpc[i] as Procedure_Chart;
                    gil.Start_Method(pc.Text);
                    declarations.Clear();
                    pc.Start.compile_pass1(gil);
                    gil.Done_Variable_Declarations();
                    pc.Start.Emit_Code(gil);
                    gil.Done_Method();
                }
            }

            gil.Start_Method("Main");
            declarations.Clear();
            start.compile_pass1(gil);
            gil.Done_Variable_Declarations();
            start.Emit_Code(gil);
            gil.Done_Method();
            gil.Finish();


        }
        private static ObservableCollection<Subchart> _tpc;
        public static ObservableCollection<Subchart> get_tpc()
        {
            return _tpc;
        }

        public static IEnumerable<Subchart> allSubcharts(ObservableCollection<Subchart> tabpages)
        {
            foreach (Subchart tp in tabpages)
            {
                if (tp is Subchart)
                {
                    yield return tp as Subchart;
                }
                //else if (tp is ClassTabPage)
                //{
                //    ClassTabPage ctp = tp as ClassTabPage;
                //    for (int j = 0; j < ctp.tabControl1.TabPages.Count; j++)
                //    {
                //        yield return ctp.tabControl1.TabPages[j] as Subchart;
                //    }
                //}
            }
        }

        //public static IEnumerable<ClassTabPage> allClasses(System.Windows.Forms.TabControl.TabPageCollection tabpages)
        //{
        //    foreach (TabPage tp in tabpages)
        //    {
        //        if (tp is ClassTabPage)
        //        {
        //            yield return tp as ClassTabPage;
        //        }
        //    }
        //}

        //public static IEnumerable<Procedure_Chart> allMethods(ClassTabPage ctp)
        //{
        //    foreach (TabPage tp in ctp.tabControl1.TabPages)
        //    {
        //        if (tp is Procedure_Chart)
        //        {
        //            yield return tp as Procedure_Chart;
        //        }
        //    }
        //}
        public static Subchart mainSubchart(ObservableCollection<Subchart> tabpages)
        {
            if (Component.Current_Mode == Mode.Expert)
            {
                return (Subchart)tabpages[1];
            }
            else
            {
                return (Subchart)tabpages[0];
            }
        }

        public static void Compile_Flowchart(ObservableCollection<Subchart> tabpages)
        {
            _tpc = tabpages;
            Oval start = mainSubchart(tabpages).Start;
            foreach (Subchart sbchrt in allSubcharts(tabpages))
            {
                sbchrt.am_compiling = false;
            }
            mainSubchart(tabpages).am_compiling = true;
            Generate_IL gil = new Generate_IL("MyAssembly");


            //Do_Compilation(start, gil, tabpages);

            try
            {
                Do_Compilation(start, gil, tabpages);
            }
            catch
            {
                foreach (Subchart sbchrt in allSubcharts(tabpages))
                {
                    sbchrt.am_compiling = false;
                }
                throw;
            }
            mainSubchart(tabpages).am_compiling = false;
        }
        public static void Compile_Flowchart_To(Oval start, string directory, string filename)
        {
            MainWindowViewModel mw = MainWindowViewModel.GetMainWindowViewModel();
            Generate_IL gil = new Generate_IL(
                System.IO.Path.GetFileNameWithoutExtension(filename));
            Do_Compilation(start, gil, /*Runtime.parent.carlisle.TabPages*/ mw.theTabs);
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }
            string app_dir = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            System.IO.File.Copy(
               Assembly.GetEntryAssembly().Location,
               System.IO.Path.Combine(directory, "raptor.dll"), true);
            System.IO.File.Copy(
               System.IO.Path.Combine(app_dir, "interpreter.dll"),
               System.IO.Path.Combine(directory, "interpreter.dll"), true);
            System.IO.File.Copy(
               System.IO.Path.Combine(app_dir, "dotnetgraph.dll"),
               System.IO.Path.Combine(directory, "dotnetgraph.dll"), true);
            System.IO.File.Copy(
               System.IO.Path.Combine(app_dir, "mgnat.dll"),
               System.IO.Path.Combine(directory, "mgnat.dll"), true);
            System.IO.File.Copy(
               System.IO.Path.Combine(app_dir, "mgnatcs.dll"),
               System.IO.Path.Combine(directory, "mgnatcs.dll"), true);
            string[] plugins = Plugins.Get_Plugin_List();
            string[] assemblies = Plugins.Get_Assembly_Names();
            Runtime.consoleWriteln("DLLs copied");
            for (int j = 0; j < plugins.Length; j++)
            {
                System.IO.File.Copy(
                   plugins[j],
                   System.IO.Path.Combine(directory,
                      assemblies[j]) + ".dll", true);
            }
            Runtime.consoleWriteln("Plugins copied");
            if (System.IO.File.Exists(System.IO.Path.Combine(directory, filename)))
            {
                System.IO.File.Delete(System.IO.Path.Combine(directory, filename));
            }
            Runtime.consoleWriteln("Old file deleted");
            gil.Save_Result(filename);
            Runtime.consoleWriteln("New file saved");
            System.IO.File.Move(filename,
                System.IO.Path.Combine(directory, filename));
            Runtime.consoleWriteln("Process complete");
        }
        static bool from_commandline;
        public static void runCompiledHelper()
        {
            Assembly[] myAssemblies = Thread.GetDomain().GetAssemblies();
            MethodInfo mi = null;
            // Get the dynamic assembly named 'MyAssembly'. 
            for (int i = 0; i < myAssemblies.Length; i++)
            {
                if (myAssemblies[i].GetName().Name == "MyAssembly")
                {
                    MethodInfo new_mi;
                    try
                    {
                        new_mi = myAssemblies[i].GetType("MyType").GetMethod("Main");
                        mi = new_mi;
                    }
                    catch
                    {
                        if (i == myAssemblies.Length - 1)
                        {
                            throw;
                        }
                    }
                }
            }
            if (mi != null)
            {
                try
                {
                    //SDILReader.Globals.LoadOpCodes();
                    //SDILReader.MethodBodyReader mr = new SDILReader.MethodBodyReader(mi);
                    //string msil = mr.GetBodyCode();
                    //Runtime.consoleWriteln(msil);
                    mi.Invoke(null, null);

                }
                catch (System.Threading.ThreadAbortException f)
                {
                    Runtime.consoleWriteln("----compiled run aborted----");
                }
                catch (System.Exception e)
                {
                    if (e.InnerException != null)
                    {
                        Runtime.consoleWriteln("Exception! " + e.InnerException.Message + e.InnerException.StackTrace);
                    }
                    else
                    {
                        Runtime.consoleWriteln("Exception! " + e.Message + e.StackTrace);
                    }
                    //SDILReader.Globals.LoadOpCodes();
                    //SDILReader.MethodBodyReader mr = new SDILReader.MethodBodyReader(mi);
                    //string msil = mr.GetBodyCode();
                    //Runtime.consoleWriteln(msil);

                    // added 3/2/05 by mcc
                    //if (raptor_files.output_redirected() && from_commandline)
                    //{
                    //    raptor_files.writeln("exception occurred! flowchart terminated abnormally\n"
                    //        + e.Message);
                    //    raptor_files.stop_redirect_output();
                    //}
                    //else
                    //{
                    //    if (e.InnerException != null)
                    //    {
                    //        Runtime.consoleWriteln("Exception! " + e.InnerException.Message + e.InnerException.StackTrace);
                    //    }
                    //    else
                    //    {
                    //        Runtime.consoleWriteln("Exception! " + e.Message + e.StackTrace);
                    //    }
                    //}
                }
            }
        }
        public static System.Threading.Thread run_compiled_thread;
        public static void Run_Compiled_NoThread(bool was_from_commandline)
        {
            from_commandline = was_from_commandline;
            runCompiledHelper();
        }
        public static void Run_Compiled(bool was_from_commandline)
        {
            from_commandline = was_from_commandline;
            if (run_compiled_thread != null && run_compiled_thread.ThreadState == ThreadState.Running)
            {
                run_compiled_thread.Abort();
            } 
            run_compiled_thread = new Thread(new ThreadStart(runCompiledHelper));
            run_compiled_thread.Start();
        }
    }
}
