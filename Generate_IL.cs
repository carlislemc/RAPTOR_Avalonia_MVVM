using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using Avalonia.Input;

namespace raptor
{
    public class Generate_IL : CodeGenerators.Imperative_Interface
    {
        private class procedure_class
        {
            public string name;
            public string[] args;
            public bool[] arg_is_input;
            public bool[] arg_is_output;
            public MethodBuilder subMethodBuilder;
            public procedure_class(string name_in, string[] args_in, bool[] arg_is_input_in,
                bool[] arg_is_output_in)
            {
                name = name_in;
                args = args_in;
                arg_is_input = arg_is_input_in;
                arg_is_output = arg_is_output_in;
            }
        }

        private FieldInfo random_generator;
        private System.Collections.Hashtable variables = new System.Collections.Hashtable();
        private System.Collections.Hashtable arrays = new System.Collections.Hashtable();
        private System.Collections.Hashtable arrays_2d = new System.Collections.Hashtable();
        private System.Collections.Generic.Dictionary<string, procedure_class> procedures =
            new System.Collections.Generic.Dictionary<string, procedure_class>();
        private TypeBuilder myTypeBuilder;
        private MethodBuilder myMethodBuilder;
        // generator for the main method
        private ILGenerator myILGenerator;
        private AssemblyBuilder myAssemblyBuilder;
        // generator for the current method
        public ILGenerator gen;
        // current class
        private procedure_class pc;

        public Generate_IL(string name)
        {
            AssemblyName myAssemblyName = new AssemblyName();
            myAssemblyName.Name = name;
            myAssemblyName.Version = new Version("1.0.0.2001");
            // Get the assembly builder from the application domain associated with the current thread.
            this.myAssemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(myAssemblyName, AssemblyBuilderAccess.Run);
            // Create a dynamic module in the assembly.
            ModuleBuilder myModuleBuilder = this.myAssemblyBuilder.DefineDynamicModule("MyModule" + name + ".exe");
            // Create a type in the module.
            this.myTypeBuilder = myModuleBuilder.DefineType("MyType");
            // Create a method called 'Main'.
            this.myMethodBuilder = myTypeBuilder.DefineMethod("Main", MethodAttributes.Public | MethodAttributes.HideBySig |
                MethodAttributes.Static, typeof(void), null);
            // Get the Intermediate Language generator for the method.
            this.myILGenerator = myMethodBuilder.GetILGenerator();
            // create a random number generator
            System.Type random_type = null;
            Assembly[] myAssemblies = Thread.GetDomain().GetAssemblies();
            for (int i = 0; i < myAssemblies.Length; i++)
            {
                random_type = myAssemblies[i].GetType("System.Random");
                if (random_type != null) break;
            }
            this.variables.Clear();
            this.arrays.Clear();
            this.arrays_2d.Clear();
            this.random_generator = myTypeBuilder.DefineField("random_generator", random_type,
                FieldAttributes.Static);
            //random_generator = myILGenerator.DeclareLocal(random_type);
            //random_generator.SetLocalSymInfo("random_generator");
            ConstructorInfo ci = random_type.GetConstructor(Type.EmptyTypes);
            myILGenerator.Emit(OpCodes.Newobj, ci);
            myILGenerator.Emit(OpCodes.Stsfld, random_generator);
            gen = myILGenerator;
            if (name != "MyAssembly")
            {
                this.Emit_Method("ada_interpreter_pkg", "adainit");
            }
        }
        public string Get_Menu_Name()
        {
            return "&Standalone";
        }
        public void Finish()
        {
            gen = myILGenerator;
            Emit_Method("RAPTOR_Avalonia_MVVM.raptor_files", "close_files");
            // Generate the 'ret' IL instruction.
            myILGenerator.Emit(OpCodes.Ret);

            // End the creation of the type.
            myTypeBuilder.CreateType();
            // Set the method with name 'Main' as the entry point in the assembly.
            
            //myAssemblyBuilder.SetEntryPoint(myMethodBuilder);
            // for debugging
            //myAssemblyBuilder.Save("MyAssembly.exe");
        }
        public void Save_Result(string filename)
        {
            //myAssemblyBuilder.Save(filename);
        }
        public bool Is_Postfix()
        {
            return true;
        }

        public void Declare_Procedure(string name, string[] args, bool[] arg_is_input, bool[] arg_is_output)
        {
            Type[] parameterTypes;
            procedure_class pc = new procedure_class(name, args, arg_is_input, arg_is_output);

            parameterTypes = new Type[args.Length];

            for (int j = 0; j < args.Length; j++)
            {
                parameterTypes[j] = typeof(object);
                //parameterTypes[j] = typeof(object).MakeByRefType();
            }
            pc.subMethodBuilder = myTypeBuilder.DefineMethod(name,
                MethodAttributes.Public | MethodAttributes.HideBySig |
                MethodAttributes.Static, typeof(void), parameterTypes);

            procedures.Add(name.ToLower().Trim(), pc);
        }
        private MethodInfo Get_Method(string package, string name)
        {
            MethodInfo mi;
            Assembly[] myAssemblies = Thread.GetDomain().GetAssemblies();
            System.Type t1 = null;
            for (int i = 0; i < myAssemblies.Length; i++)
            {
                t1 = myAssemblies[i].GetType(package);
                if (t1 != null) break;
            }
            mi = t1.GetMethod(name);
            return mi;
        }

        public void Emit_Get_Mouse_Button()
        {
            MethodInfo mi;
            Assembly[] myAssemblies = Thread.GetDomain().GetAssemblies();
            System.Type t1 = null;
            for (int i = 0; i < myAssemblies.Length; i++)
            {
                t1 = myAssemblies[i].GetType("dotnetgraphlibrary.dotnetgraph");
                if (t1 != null) break;
            }
            System.Type[] types = new System.Type[1];
            //dotnetgraphlibrary.Mouse_Button bob = new dotnetgraphlibrary.Mouse_Button();
            MouseButton bob = new MouseButton();
            types[0] = bob.GetType();
            mi = t1.GetMethod("Get_Mouse_Button", types);
            gen.EmitCall(OpCodes.Call, mi, null);
        }
        public void Emit_Get_Click(int x_or_y)
        {
            if (x_or_y == 0)
            {
                this.Emit_Method("dotnetgraphlibrary.dotnetgraph",
                    "Get_Click_X");
            }
            else
            {
                this.Emit_Method("dotnetgraphlibrary.dotnetgraph",
                    "Get_Click_Y");
            }
            this.Emit_Method("numbers.Numbers",
                "make_value__3");
        }
        public void Emit_Array_Size(string name)
        {
            LocalBuilder local = null;
            if (arrays.ContainsKey(name.ToLower()))
            {
                local = ((LocalBuilder)arrays[name.ToLower()]);
                gen.Emit(OpCodes.Ldloc, local);
                Emit_Method_Virt("raptor.Value_Array", "Get_Length");
            }
            else if (variables.ContainsKey(name.ToLower()))
            {
                local = ((LocalBuilder)variables[name.ToLower()]);
                gen.Emit(OpCodes.Ldloc, local);
                Emit_Method("numbers.Numbers", "string_of");
                Emit_Method_Virt("System.String", "get_Length");
            }
            else
            {
                throw new System.Exception("can only take length_of 1D array or string");
            }
            Emit_Method("numbers.Numbers", "make_value__3");
        }
        public void Emit_String_Length()
        {
            this.Emit_Method_Virt("System.String", "get_Length");
            this.Emit_Method("numbers.Numbers",
               "make_value__3");
        }
        public void Emit_Assign_To(string name)
        {
            LocalBuilder local;
            if (variables.ContainsKey(name.ToLower()))
            {
                local = ((LocalBuilder)variables[name.ToLower()]);
            }
            else
            {
                local = gen.DeclareLocal(numbers.Numbers.Pi.GetType());
                //local.SetLocalSymInfo(name.ToLower());
                variables.Add(name.ToLower(), local);
            }
            Emit_Method_Virt("numbers.value", "_deep_clone");
            gen.Emit(OpCodes.Stloc, local);
        }
        public void Indent()
        {
        }
        public void Emit_Assign_To_Array(string name)
        {
            LocalBuilder local = null;
            // cloning done in Set_Value
            if (arrays.ContainsKey(name.ToLower()))
            {
                local = ((LocalBuilder)arrays[name.ToLower()]);
                Emit_Method_Virt("raptor.Value_Array", "Set_Value");
            }
            else
            {
                Emit_Method("raptor.Runtime_Helpers",
                    "Set_Value_String");
            }
        }
        public void Emit_Assign_To_Array_2D(string name)
        {
            LocalBuilder local = null;
            // cloning done in Set_Value
            if (arrays_2d.ContainsKey(name.ToLower()))
            {
                local = ((LocalBuilder)arrays_2d[name.ToLower()]);
            }
            Emit_Method_Virt("raptor.Value_2D_Array", "Set_Value");
        }
        public void Emit_Load(string name)
        {
            LocalBuilder local;
            if (variables.ContainsKey(name.ToLower()))
            {
                name_variable = name;
                local = ((LocalBuilder)variables[name.ToLower()]);
            }
            else if (arrays.ContainsKey(name.ToLower()))
            {
                local = ((LocalBuilder)arrays[name.ToLower()]);
            }
            else if (arrays_2d.ContainsKey(name.ToLower()))
            {
                local = ((LocalBuilder)arrays_2d[name.ToLower()]);
            }
            else
            {
                throw new System.Exception(name + ": variable used before assigned");
            }
            gen.Emit(OpCodes.Ldloc, local);
        }
        public void Emit_Load_Array_Start(string name)
        {
            this.Emit_Load(name);
        }
        public void Emit_Load_Array_After_Index(string name)
        {
            LocalBuilder local = null;
            if (arrays.ContainsKey(name.ToLower()))
            {
                local = ((LocalBuilder)arrays[name.ToLower()]);
                Emit_Method_Virt(
                    "raptor.Value_Array",
                    "Get_Value");
            }
            else
            {
                Emit_Method("raptor.Runtime_Helpers",
                    "Get_Value_String");
            }
        }
        public void Emit_Load_Array_2D_Start(string name)
        {
            this.Emit_Load(name);
        }
        public void Emit_Load_Array_2D_Between_Indices() { }
        public void Emit_Load_Array_2D_After_Indices(string name)
        {
            LocalBuilder local = null;
            if (arrays_2d.ContainsKey(name.ToLower()))
            {
                local = ((LocalBuilder)arrays_2d[name.ToLower()]);
            }
            Emit_Method_Virt("raptor.Value_2D_Array", "Get_Value");
        }
        public bool Previously_Declared(string name)
        {
            return (variables.ContainsKey(name.ToLower()) ||
                arrays.ContainsKey(name.ToLower()) ||
                arrays_2d.ContainsKey(name.ToLower()));
        }

        public void Declare_String_Variable(string name)
        {
            if (!Previously_Declared(name))
            {
                LocalBuilder local = gen.DeclareLocal(name.GetType());
                variables.Add(name.ToLower(), local);
            }
        }
        public void Declare_As_Variable(string name)
        {
            if (!Previously_Declared(name))
            {
                System.Type t1 = numbers.Numbers.Pi.GetType();
                LocalBuilder local = gen.DeclareLocal(t1);
                variables.Add(name.ToLower(), local);
                gen.Emit(OpCodes.Newobj, t1.GetConstructor(System.Type.EmptyTypes));
                gen.Emit(OpCodes.Stloc, local);
            }
        }
        public void Declare_As_1D_Array(string name)
        {
            System.Diagnostics.Trace.WriteLine("new array " + name);
            if (!Previously_Declared(name))
            {
                System.Type t1 = System.Type.GetType("raptor.Value_Array");
                LocalBuilder local = gen.DeclareLocal(t1);
                arrays.Add(name.ToLower(), local);
                gen.Emit(OpCodes.Newobj, t1.GetConstructor(System.Type.EmptyTypes));
                gen.Emit(OpCodes.Stloc, local);
            }
        }
        public void Declare_As_2D_Array(string name)
        {
            if (!Previously_Declared(name))
            {
                System.Type t1 = System.Type.GetType("raptor.Value_2D_Array");
                LocalBuilder local = gen.DeclareLocal(t1);
                arrays_2d.Add(name.ToLower(), local);
                gen.Emit(OpCodes.Newobj, t1.GetConstructor(System.Type.EmptyTypes));
                gen.Emit(OpCodes.Stloc, local);
            }
        }
        public void Emit_Relation(int relation)
        {
            switch (relation)
            {
                case 1: //gt
                    Emit_Method("numbers.Numbers", "Ogt");
                    break;
                case 2: //ge
                    Emit_Method("numbers.Numbers", "Oge");
                    break;
                case 3: //lt
                    Emit_Method("numbers.Numbers", "Olt");
                    break;
                case 4: //le
                    Emit_Method("numbers.Numbers", "Ole");
                    break;
                case 5: //eq
                    Emit_Method("numbers.Numbers", "Oeq");
                    break;
                case 6: //ne
                    Emit_Method("numbers.Numbers", "Oeq");
                    gen.Emit(OpCodes.Ldc_I4_1);
                    gen.Emit(OpCodes.Xor);
                    break;
            }
        }
        public void Emit_Method(string package, string name)
        {
            gen.EmitCall(OpCodes.Call,
                Get_Method(package, name), null);
        }
        public void Emit_Method_Virt(string package, string name)
        {
            gen.EmitCall(OpCodes.Callvirt,
                Get_Method(package, name), null);
        }
        public void Emit_Sleep()
        {
        }
        public void Emit_Past_Sleep()
        {
            MethodInfo mi;
            Assembly[] myAssemblies = Thread.GetDomain().GetAssemblies();
            System.Type t1 = null;

            Emit_Method("numbers.Numbers", "long_float_of");
            for (int i = 0; i < myAssemblies.Length; i++)
            {
                t1 = myAssemblies[i].GetType("System.Threading.Thread");
                if (t1 != null) break;
            }
            System.Type[] One_Int = new System.Type[1];
            One_Int[0] = System.Type.GetType("System.Int32");
            gen.Emit(OpCodes.Ldc_R8, 1000.0);
            gen.Emit(OpCodes.Mul);
            gen.Emit(OpCodes.Conv_I4);
            mi = t1.GetMethod("Sleep", One_Int);
            gen.EmitCall(OpCodes.Call, mi, null);
        }
        public void Emit_And()
        {
            gen.Emit(OpCodes.And);
        }
        public void Emit_Or()
        {
            gen.Emit(OpCodes.Or);
        }
        public void Emit_Not()
        {
            gen.Emit(OpCodes.Ldc_I4_1);
            gen.Emit(OpCodes.Xor);
        }
        public void Emit_Xor()
        {
            gen.Emit(OpCodes.Xor);
        }
        public void Emit_To_Integer()
        {
            gen.Emit(OpCodes.Conv_I4);
        }
        public void Emit_Load_Boolean(bool val)
        {
            gen.Emit(OpCodes.Ldc_I4, (val) ? 1 : 0);
        }
        public void Emit_Load_Number(double val)
        {
            gen.Emit(OpCodes.Ldc_R8, val);
            Emit_Method("numbers.Numbers", "make_value__2");
        }
        public void Emit_Load_Character(char val)
        {
            gen.Emit(OpCodes.Ldc_I4, val);
            Emit_Method("numbers.Numbers", "make_value__4");
        }
        public void Emit_Load_String(string val)
        {
            gen.Emit(OpCodes.Ldstr, val);
            Emit_Method("numbers.Numbers", "make_string_value");
        }
        public void Emit_Load_String_Const(string val)
        {
            gen.Emit(OpCodes.Ldstr, val);
        }
        public void Emit_Load_Static(string package,
            string field)
        {
            Assembly[] myAssemblies = Thread.GetDomain().GetAssemblies();
            System.Type t1 = null;
            for (int i = 0; i < myAssemblies.Length; i++)
            {
                t1 = myAssemblies[i].GetType(package);
                if (t1 != null) break;
            }
            FieldInfo fi = t1.GetField(field);
            gen.Emit(OpCodes.Ldsfld, fi);
        }
        //dotnetgraphlibrary.dotnetgraph.
        public void Emit_Conversion(int o)
        {
        }
        public void Emit_End_Conversion(int o)
        {
            parse_tree.Emit_Functions.Emit_Conversion(o, this);
        }

        public void Emit_Value_To_Color_Type()
        {
            Emit_Method("numbers.Numbers", "integer_of");
        }
        public void Emit_Value_To_Bool()
        {
            Emit_Method("numbers.Numbers", "integer_of");
        }
        public void Emit_Random()
        {
            gen.Emit(OpCodes.Ldsfld, random_generator);
            gen.EmitCall(OpCodes.Callvirt,
                Get_Method("System.Random", "NextDouble"), null);
            Emit_Method("numbers.Numbers", "make_value__2");
        }
        public void Emit_Random(double first, double last)
        {
            gen.Emit(OpCodes.Ldsfld, random_generator);
            gen.EmitCall(OpCodes.Callvirt,
                Get_Method("System.Random", "NextDouble"), null);
            gen.Emit(OpCodes.Ldc_R8, last - first);
            gen.Emit(OpCodes.Mul);
            gen.Emit(OpCodes.Ldc_R8, first);
            gen.Emit(OpCodes.Add);
            MethodInfo mi;
            Assembly[] myAssemblies = Thread.GetDomain().GetAssemblies();
            System.Type t1 = null;

            for (int i = 0; i < myAssemblies.Length; i++)
            {
                t1 = myAssemblies[i].GetType("System.Math");
                if (t1 != null) break;
            }

            System.Type[] One_Double = new System.Type[1];
            One_Double[0] = System.Type.GetType("System.Double");
            mi = t1.GetMethod("Floor", One_Double);
            gen.EmitCall(OpCodes.Call, mi, null);
            Emit_Method("numbers.Numbers", "make_value__2");
        }

        public void Emit_And_Shortcut(parse_tree.Boolean_Parseable left,
            parse_tree.Boolean2 right,
            bool left_negated)
        {
            System.Reflection.Emit.Label l2 = gen.DefineLabel();
            left.Emit_Code(this);
            if (left_negated)
            {
                Emit_Not();
            }
            gen.Emit(OpCodes.Dup);
            gen.Emit(System.Reflection.Emit.OpCodes.Brfalse, l2);
            right.Emit_Code(this);
            Emit_And();
            gen.MarkLabel(l2);
        }
        public void Emit_Or_Shortcut(parse_tree.Boolean2 left, parse_tree.Boolean_Expression right)
        {
            System.Reflection.Emit.Label l2 = gen.DefineLabel();
            left.Emit_Code(this);
            gen.Emit(OpCodes.Dup);
            gen.Emit(System.Reflection.Emit.OpCodes.Brtrue, l2);
            right.Emit_Code(this);
            Emit_Or();
            gen.MarkLabel(l2);
        }
        public void Emit_Is_Number(string name)
        {
            LocalBuilder local;
            if (variables.ContainsKey(name.ToLower()))
            {
                local = ((LocalBuilder)variables[name.ToLower()]);
                gen.Emit(OpCodes.Ldloc, local);
                Emit_Method("numbers.Numbers", "is_number");
            }
            else if (arrays.ContainsKey(name.ToLower()))
            {
                gen.Emit(OpCodes.Ldc_I4_0);
            }
            else if (arrays_2d.ContainsKey(name.ToLower()))
            {
                gen.Emit(OpCodes.Ldc_I4_0);
            }
            else
            {
                throw new System.Exception(name + ": variable used before assigned");
            }
        }
        public void Emit_Is_Character(string name)
        {
            LocalBuilder local;
            if (variables.ContainsKey(name.ToLower()))
            {
                local = ((LocalBuilder)variables[name.ToLower()]);
                gen.Emit(OpCodes.Ldloc, local);
                Emit_Method("numbers.Numbers", "is_character");
            }
            else if (arrays.ContainsKey(name.ToLower()))
            {
                gen.Emit(OpCodes.Ldc_I4_0);
            }
            else if (arrays_2d.ContainsKey(name.ToLower()))
            {
                gen.Emit(OpCodes.Ldc_I4_0);
            }
            else
            {
                throw new System.Exception(name + ": variable used before assigned");
            }
        }
        public void Emit_Is_String(string name)
        {
            LocalBuilder local;
            if (variables.ContainsKey(name.ToLower()))
            {
                local = ((LocalBuilder)variables[name.ToLower()]);
                gen.Emit(OpCodes.Ldloc, local);
                Emit_Method("numbers.Numbers", "is_string");
            }
            else if (arrays.ContainsKey(name.ToLower()))
            {
                gen.Emit(OpCodes.Ldc_I4_0);
            }
            else if (arrays_2d.ContainsKey(name.ToLower()))
            {
                gen.Emit(OpCodes.Ldc_I4_0);
            }
            else
            {
                throw new System.Exception(name + ": variable used before assigned");
            }
        }
        public void Emit_Is_Array(string name)
        {
            if (variables.ContainsKey(name.ToLower()))
            {
                gen.Emit(OpCodes.Ldc_I4_0);
            }
            else if (arrays.ContainsKey(name.ToLower()))
            {
                gen.Emit(OpCodes.Ldc_I4_1);
            }
            else if (arrays_2d.ContainsKey(name.ToLower()))
            {
                gen.Emit(OpCodes.Ldc_I4_0);
            }
            else
            {
                throw new System.Exception(name + ": variable used before assigned");
            }
        }
        public void Emit_Is_Array2D(string name)
        {
            if (variables.ContainsKey(name.ToLower()))
            {
                gen.Emit(OpCodes.Ldc_I4_0);
            }
            else if (arrays.ContainsKey(name.ToLower()))
            {
                gen.Emit(OpCodes.Ldc_I4_0);
            }
            else if (arrays_2d.ContainsKey(name.ToLower()))
            {
                gen.Emit(OpCodes.Ldc_I4_1);
            }
            else
            {
                throw new System.Exception(name + ": variable used before assigned");
            }
        }

        public void Emit_Plugin_Call(string name, parse_tree.Parameter_List parameters)
        {
            RAPTOR_Avalonia_MVVM.Plugins.Emit_Invoke_Function(name, parameters, this);
        }
        public void Emit_Times()
        {
            this.Emit_Method("numbers.Numbers", "multValues");
        }
        public void Emit_Divide()
        {
            this.Emit_Method("numbers.Numbers", "divValues");
        }
        public void Emit_Plus()
        {
            this.Emit_Method("numbers.Numbers", "addValues");
        }
        public void Emit_Unary_Minus()
        {
            this.Emit_Method("numbers.Numbers", "subValues");
        }
        public void Emit_Minus()
        {
            this.Emit_Method("numbers.Numbers", "subValues");
        }
        public void Emit_Mod()
        {
            this.Emit_Method("numbers.Numbers", "modValues");
        }
        public void Emit_Rem()
        {
            this.Emit_Method("numbers.Numbers", "remValues");
        }
        public void Emit_Exponentiation()
        {
            this.Emit_Method("numbers.Numbers", "exponValues");
        }
        public void Start_Method(string name)
        {
            // moved to outside if-- should happen always.
            variables.Clear();
            arrays.Clear();
            arrays_2d.Clear();

            if (name != "Main")
            {
                pc = procedures[name.ToLower().Trim()];

                gen = pc.subMethodBuilder.GetILGenerator();
            }
            else
            {
                gen = this.myILGenerator;
                pc = new procedure_class("Main", new string[0], new bool[0], new bool[0]);
            }
        }
        public void Done_Variable_Declarations()
        {
            ILGenerator subILGenerator = gen;

            // Get the Intermediate Language generator for the method.
            for (int k = 0; k < pc.args.Length; k++)
            {
                string name = pc.args[k].ToLower();
                LocalBuilder local;
                // arrays are always going to be pass by reference when compiled.
                if (arrays.ContainsKey(name))
                {
                    local = ((LocalBuilder)arrays[name]);
                    subILGenerator.Emit(OpCodes.Ldarg, k);
                    subILGenerator.Emit(OpCodes.Castclass, typeof(Value_Array));
                    subILGenerator.Emit(OpCodes.Stloc, local);
                }
                else if (arrays_2d.ContainsKey(name))
                {
                    local = ((LocalBuilder)arrays_2d[name]);
                    subILGenerator.Emit(OpCodes.Ldarg, k);
                    subILGenerator.Emit(OpCodes.Castclass, typeof(Value_2D_Array));
                    subILGenerator.Emit(OpCodes.Stloc, local);
                }
                else if (variables.ContainsKey(name))
                {
                    if (pc.arg_is_input[k])
                    {
                        local = ((LocalBuilder)variables[name]);
                        subILGenerator.Emit(OpCodes.Ldarg_S, k);
                        subILGenerator.Emit(OpCodes.Castclass, typeof(numbers.value));
                        subILGenerator.Emit(OpCodes.Ldloc, local);
                        subILGenerator.Emit(OpCodes.Castclass, typeof(numbers.value));
                        Emit_Method("numbers.Numbers", "copy");
                    }
                }
                else
                {
                    local = subILGenerator.DeclareLocal(typeof(object));
                    variables.Add(name.ToLower(), local);
                    subILGenerator.Emit(OpCodes.Ldarg_S, k);
                    subILGenerator.Emit(OpCodes.Stloc, local);
                }
            }
        }
        public void Done_Method()
        {
            ILGenerator subILGenerator = gen;

            for (int k = 0; k < pc.args.Length; k++)
            {
                string name = pc.args[k].ToLower();
                LocalBuilder local;
                // arrays are always going to be pass by reference when compiled.
                if (pc.arg_is_output[k])
                {
                    if (variables.ContainsKey(name))
                    {
                        local = ((LocalBuilder)variables[name]);
                        if (local.LocalType == typeof(numbers.value))
                        {
                            subILGenerator.Emit(OpCodes.Ldloc, local);
                            subILGenerator.Emit(OpCodes.Ldarg, k);
                            subILGenerator.Emit(OpCodes.Castclass, typeof(numbers.value));
                            Emit_Method("numbers.Numbers", "copy");
                        }
                    }
                }
            }
            // Generate the 'ret' IL instruction.
            subILGenerator.Emit(OpCodes.Ret);
        }
        private class method_call
        {
            public int name;
            public int param_count;
            public method_call()
            {
            }
            public method_call(int in_name)
            {
                name = in_name;
                param_count = 0;
            }
        }
        private class subchart_call
        {
            public procedure_class proc;
            public subchart_call(procedure_class in_proc)
            {
                proc = in_proc;
            }
        }
        public object Emit_Call_Method(int name)
        {
            return new method_call(name);
        }
        public void Emit_Next_Parameter(object o)
        {
            if (o is method_call)
            {
                ((method_call)o).param_count++;
            }
        }
        public void Emit_Last_Parameter(object o)
        {
            if (o is method_call)
            {
                method_call mc = o as method_call;
                mc.param_count++;
                parse_tree.Emit_Functions.emit_method_call_il(mc.name, mc.param_count, this);
            }
            else if (o is subchart_call)
            {
                subchart_call oc = o as subchart_call;
                gen.EmitCall(System.Reflection.Emit.OpCodes.Call, oc.proc.subMethodBuilder, null);
            }
        }
        public void Emit_No_Parameters(object o)
        {
            method_call mc = o as method_call;
            parse_tree.Emit_Functions.emit_method_call_il(mc.name, mc.param_count, this);
        }
        public object Emit_Call_Subchart(string name)
        {
            procedure_class next = procedures[name.Trim().ToLower()];
            return new subchart_call(next);

        }
        private class label_pair
        {
            public System.Reflection.Emit.Label l2;
            public System.Reflection.Emit.Label l3;
            public label_pair(ILGenerator gen)
            {
                l2 = gen.DefineLabel();
                l3 = gen.DefineLabel();
            }
        }
        public object If_Start()
        {
            return new label_pair(gen);
        }
        public void If_Then_Part(object o)
        {
            gen.Emit(System.Reflection.Emit.OpCodes.Brfalse, ((label_pair)o).l2);
        }
        public void If_Else_Part(object o)
        {
            gen.Emit(System.Reflection.Emit.OpCodes.Br, ((label_pair)o).l3);
            gen.MarkLabel(((label_pair)o).l2);
        }
        public void If_Done(object o)
        {
            gen.MarkLabel(((label_pair)o).l3);
        }

        private struct loop_vars
        {
            public label_pair lp;
            public bool is_negated;
        }
        public object Loop_Start(bool is_while, bool is_negated)
        {
            loop_vars lv;

            label_pair lp = new label_pair(gen);
            gen.MarkLabel(lp.l2);
            lv.lp = lp;
            lv.is_negated = is_negated;
            return lv as object;
        }
        public void Loop_Start_Condition(object o)
        {
        }
        public void Loop_End_Condition(object o)
        {
            if (((loop_vars)o).is_negated)
            {
                gen.Emit(System.Reflection.Emit.OpCodes.Brfalse, ((loop_vars)o).lp.l3);
            }
            else
            {
                gen.Emit(System.Reflection.Emit.OpCodes.Brtrue, ((loop_vars)o).lp.l3);
            }
        }
        public void Loop_End(object o)
        {
            gen.Emit(System.Reflection.Emit.OpCodes.Br, ((loop_vars)o).lp.l2);
            gen.MarkLabel(((loop_vars)o).lp.l3);
        }
        public void Emit_Left_Paren() { }
        public void Emit_Right_Paren() { }

        // things to do with 2D array assignment
        private string name_array_2d;
        public void Array_2D_Assignment_Start(string name)
        {
            name_array_2d = name;
            this.Emit_Load(name);
        }
        public void Array_2D_Assignment_Between_Indices() { }
        public void Array_2D_Assignment_After_Indices() { }
        public void Array_2D_Assignment_PastRHS()
        {
            this.Emit_Assign_To_Array_2D(name_array_2d);
        }

        // things to do with 1D array assignment
        private string name_array_1d;
        public void Array_1D_Assignment_Start(string name)
        {
            name_array_1d = name;
            this.Emit_Load(name);
        }
        public void Array_1D_Assignment_After_Index() { }
        public void Array_1D_Assignment_PastRHS()
        {
            this.Emit_Assign_To_Array(name_array_1d);
        }

        // things to do with variable assignment
        private string name_variable;
        public void Variable_Assignment_Start(string name)
        {
            name_variable = name;
        }
        public void Variable_Assignment_PastRHS()
        {
            // keep with parallelogram.cs, parse_tree.adb
            if (name_variable == "raptor_prompt_variable_zzyz")
            {
                this.Emit_Method("numbers.Numbers", "string_of");
                LocalBuilder local;
                local = ((LocalBuilder)variables[name_variable.ToLower()]);
                gen.Emit(OpCodes.Stloc, local);
            }
            else
            {
                this.Emit_Assign_To(name_variable);
            }
        }
        private bool dest_is_array;
        private enum input_kind { variable, array, array2d };
        private input_kind kind_of_input;
        public void Input_Start_Variable(string name)
        {
            dest_is_array = false;
            this.Variable_Assignment_Start(name);
            kind_of_input = input_kind.variable;
        }
        public void Input_Start_Array_1D(string name, parse_tree.Expression reference)
        {
            dest_is_array = true;
            this.Array_1D_Assignment_Start(name);
            reference.Emit_Code(this);
            this.Array_1D_Assignment_After_Index();
            kind_of_input = input_kind.array;
        }
        public void Input_Start_Array_2D(string name, parse_tree.Expression reference,
            parse_tree.Expression reference2)
        {
            dest_is_array = true;
            this.Array_2D_Assignment_Start(name);
            reference.Emit_Code(this);
            this.Array_2D_Assignment_Between_Indices();
            reference2.Emit_Code(this);
            this.Array_2D_Assignment_After_Indices();
            kind_of_input = input_kind.array2d;
        }
        public void Input_Past_Prompt()
        {
            this.Emit_Load_Boolean(dest_is_array);
            this.Emit_Method(
               "ada_runtime_pkg",
               "prompt_dialog");
            switch (kind_of_input)
            {
                case input_kind.variable:
                    this.Variable_Assignment_PastRHS();
                    break;
                case input_kind.array2d:
                    this.Array_2D_Assignment_PastRHS();
                    break;
                case input_kind.array:
                    this.Array_1D_Assignment_PastRHS();
                    break;
            }
        }
        public void Output_Start(bool has_newline, bool is_string) { }
        public void Output_Past_Expr(bool has_newline, bool is_string)
        {
            if (!is_string)
            {
                this.Emit_Method("numbers.Numbers",
                    "msstring_view_image");
            }
            if (has_newline)
            {
                this.Emit_Method("raptor.Runtime", "consoleWriteln");
            }
            else
            {
                this.Emit_Method("raptor.Runtime", "consoleWrite");
            }
        }

    }
}
