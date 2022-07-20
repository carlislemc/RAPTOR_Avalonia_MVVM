using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;
using raptor;

namespace RAPTOR_Avalonia_MVVM
{
    public class Plugins
    {

        static System.Reflection.MethodInfo[] methods;
        static string[] Suggestions;

        private static MethodInfo GetMethod(string name)
        {
            if (methods == null)
            {
                return null;
            }
            for (int i = 0; i < methods.Length; i++)
            {
                if (methods[i].IsStatic && methods[i].Name.ToLower() != "Main" &&
                    methods[i].Name.ToLower() == name.ToLower())
                {
                    return methods[i];
                }
            }
            return null;
        }

        public static numbers.value Invoke_Function(string name,
            parse_tree.Parameter_List parameters)
        {
			parse_tree.Parameter_List walk;
			int num_parameters;
			string tempName = name;
            if (name.Contains(":="))
            {
				tempName = name.Substring(name.IndexOf(":=") + 2).Trim();
				if (tempName.Contains("("))
				{
					tempName = tempName.Substring(0, tempName.IndexOf("("));
				}
			}
			
			MethodInfo method = GetMethod(tempName);
			ParameterInfo[] parameter_info = method.GetParameters();
			num_parameters = parameter_info.Length;
			Object[] passed_parameters = new Object[num_parameters];
			Object[] clone_passed_parameters =
				new Object[num_parameters];
			walk = parameters;
			for (int i = 0; i < num_parameters; i++)
			{
				numbers.value the_value;
				string array_name;
				if (parameter_info[i].ParameterType.Name == "Int32" ||
					parameter_info[i].ParameterType.Name == "Int32&")
				{
					the_value = ((parse_tree.Expr_Output)walk.parameter).expr.Execute(new Lexer(name));
					passed_parameters[i] = ((int)numbers.Numbers.integer_of(the_value));
					clone_passed_parameters[i] = ((int)numbers.Numbers.integer_of(the_value));
				}
				else if (parameter_info[i].ParameterType.Name == "Double" ||
					parameter_info[i].ParameterType.Name == "Double&")
				{
					the_value = ((parse_tree.Expr_Output)walk.parameter).expr.Execute(new Lexer(name));
					passed_parameters[i] = (numbers.Numbers.long_float_of(the_value));
					clone_passed_parameters[i] = (numbers.Numbers.long_float_of(the_value));
				}
				else if (parameter_info[i].ParameterType.Name == "Single" ||
					parameter_info[i].ParameterType.Name == "Single&")
				{
					the_value = ((parse_tree.Expr_Output) walk.parameter).expr.Execute(new Lexer(name));
					passed_parameters[i] = ((float)numbers.Numbers.long_float_of(the_value));
					clone_passed_parameters[i] = ((float)numbers.Numbers.long_float_of(the_value));
				}
				else if (parameter_info[i].ParameterType.Name == "String")
				{
					the_value = ((parse_tree.Expr_Output) walk.parameter).expr.Execute(new Lexer(name));
					passed_parameters[i] = (numbers.Numbers.msstring_view_image(the_value).Replace("\"", ""));
					//passed_parameters[i] = ((parse_tree.string_output) 
					//	walk.parameter).get_string__2();
				}
				else if (parameter_info[i].ParameterType.Name == "Int32[]")
				{
					//array_name = ((parse_tree.Expr_Output) walk.parameter).get_string();
					array_name = parameter_info[i].Name;
					int[] parameter = Runtime.getIntArray(array_name);

					passed_parameters[i] = parameter;
					clone_passed_parameters[i] = parameter.Clone();
				}
				else if (parameter_info[i].ParameterType.Name == "Int32[]&")
				{
					//array_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
					array_name = parameter_info[i].Name;
					if (Runtime.is_Variable(array_name))
					{
						int[] parameter = Runtime.getIntArray(array_name);

						passed_parameters[i] = parameter;
						clone_passed_parameters[i] = parameter.Clone();
					}
					else
					{
						passed_parameters[i] = null;
						clone_passed_parameters[i] = null;
					}
				}
				else if (parameter_info[i].ParameterType.Name == "Single[]")
				{
					//array_name = ((parse_tree.expr_output) walk.parameter).get_string();
					array_name = parameter_info[i].Name;
					double[] parameter = Runtime.getArrayDouble(array_name);

					passed_parameters[i] = parameter;
					clone_passed_parameters[i] = parameter.Clone();
				}
				else if (parameter_info[i].ParameterType.Name == "Single[]&")
				{
					//array_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
					array_name = parameter_info[i].Name;
					if (Runtime.is_Variable(array_name))
					{
						double[] parameter = Runtime.getArrayDouble(array_name);

						passed_parameters[i] = parameter;
						clone_passed_parameters[i] = parameter.Clone();
					}
					else
					{
						passed_parameters[i] = null;
						clone_passed_parameters[i] = null;
					}
				}
				else if (parameter_info[i].ParameterType.Name == "Double[]")
				{
					//array_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
					array_name = parameter_info[i].Name;
					double[] parameter = Runtime.getArrayDouble(array_name);

					passed_parameters[i] = parameter;
					clone_passed_parameters[i] = parameter.Clone();
				}
				else if (parameter_info[i].ParameterType.Name == "Double[]&")
				{
					//array_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
					array_name = parameter_info[i].Name;
					if (Runtime.is_Variable(array_name))
					{
						double[] parameter = Runtime.getArrayDouble(array_name);

						passed_parameters[i] = parameter;
						clone_passed_parameters[i] = parameter.Clone();
					}
					else
					{
						passed_parameters[i] = null;
						clone_passed_parameters[i] = null;
					}
				}
				else if (parameter_info[i].ParameterType.Name == "Int32[][]")
				{
					//array_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
					array_name = parameter_info[i].Name;
					int[][] parameter = Runtime.get2DIntArray(array_name);
					passed_parameters[i] = parameter;
					int[][] clone_parameter = Runtime.get2DIntArray(array_name);
					clone_passed_parameters[i] = clone_parameter;
				}
				else if (parameter_info[i].ParameterType.Name == "Int32[][]&")
				{
					//array_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
					array_name = parameter_info[i].Name;
					if (Runtime.is_Variable(array_name))
					{
						int[][] parameter = Runtime.get2DIntArray(array_name);
						passed_parameters[i] = parameter;
						int[][] clone_parameter = Runtime.get2DIntArray(array_name);
						clone_passed_parameters[i] = clone_parameter;
					}
					else
					{
						passed_parameters[i] = null;
						clone_passed_parameters[i] = null;
					}
				}
				else if (parameter_info[i].ParameterType.Name == "Single[][]")
				{
					//array_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
					array_name = parameter_info[i].Name;
					double[][] parameter = Runtime.get2DArray(array_name);

					passed_parameters[i] = parameter;
					double[][] clone_parameter = Runtime.get2DArray(array_name);
					clone_passed_parameters[i] = clone_parameter;
				}
				else if (parameter_info[i].ParameterType.Name == "Single[][]&")
				{
					//array_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
					array_name = parameter_info[i].Name;
					if (Runtime.is_Variable(array_name))
					{
						double[][] parameter = Runtime.get2DArray(array_name);

						passed_parameters[i] = parameter;
						double[][] clone_parameter = Runtime.get2DArray(array_name);
						clone_passed_parameters[i] = clone_parameter;
					}
					else
					{
						passed_parameters[i] = null;
						clone_passed_parameters[i] = null;
					}
				}
				else if (parameter_info[i].ParameterType.Name == "Double[][]")
				{
					//array_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
					array_name = parameter_info[i].Name;
					double[][] parameter = Runtime.get2DArray(array_name);

					passed_parameters[i] = parameter;
					double[][] clone_parameter = Runtime.get2DArray(array_name);
					clone_passed_parameters[i] = clone_parameter;
				}
				else if (parameter_info[i].ParameterType.Name == "Double[][]&")
				{
					//array_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
					array_name = parameter_info[i].Name;
					if (Runtime.is_Variable(array_name))
					{
						double[][] parameter = Runtime.get2DArray(array_name);

						passed_parameters[i] = parameter;
						double[][] clone_parameter = Runtime.get2DArray(array_name);
						clone_passed_parameters[i] = clone_parameter;
					}
					else
					{
						passed_parameters[i] = null;
						clone_passed_parameters[i] = null;
					}
				}
				walk = walk.next;
			}
			object result = method.Invoke(null, passed_parameters);
			walk = parameters;
			for (int i = 0; i < num_parameters; i++)
			{
				//Console.WriteLine("p:" + i + parameter_info[i].ParameterType.Name);
				if (parameter_info[i].ParameterType.Name == "Int32&")
				{

					//if ((int)passed_parameters[i] !=
					//	(int)clone_passed_parameters[i])
					//{
					//	parse_tree_pkg.ms_assign_to(
					//		walk.parameter, numbers.Numbers.make_value__3((int)passed_parameters[i]),
					//		"parameter for " +
					//		parameter_info[i].Name);
					//}
					Variable ansdufba = new Variable("Fail", new numbers.value() { V = 1 });
				}
				else if (parameter_info[i].ParameterType.Name == "Double&")
				{
					//if ((double)passed_parameters[i] !=
					//	(double)clone_passed_parameters[i])
					//{
					//	parse_tree_pkg.ms_assign_to(
					//		walk.parameter, numbers.Numbers.make_value__2((double)passed_parameters[i]),
					//		"parameter for " +
					//		parameter_info[i].Name);
					//}
					Variable ansdufba = new Variable("Fail", new numbers.value() { V = 2 });
				}
				else if (parameter_info[i].ParameterType.Name == "Single&")
				{
					//if ((float)passed_parameters[i] !=
					//	(float)clone_passed_parameters[i])
					//{
					//	parse_tree_pkg.ms_assign_to(
					//		walk.parameter, numbers.Numbers.make_value__2((double)((float)passed_parameters[i])),
					//		"parameter for " +
					//		parameter_info[i].Name);
					//}
					Variable ansdufba = new Variable("Fail", new numbers.value() { V = 3 });
				}
				else if (parameter_info[i].ParameterType.Name == "Int32[]" ||
					(parameter_info[i].ParameterType.Name == "Int32[]&" &&
					passed_parameters[i] == clone_passed_parameters[i]))
				{
					for (int k = 0;
						k < ((int[])passed_parameters[i]).Length;
						k++)
					{
						if (((int[])passed_parameters[i])[k] !=
							((int[])clone_passed_parameters[i])[k])
						{
							//string array_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
							string array_name = parameter_info[i].Name;
							Runtime.setArrayElement(
								array_name,
								k + 1,
								numbers.Numbers.make_value__3(((int[])passed_parameters[i])[k]));
						}
					}
				}
				else if (parameter_info[i].ParameterType.Name == "Int32[]&")
				{
					for (int k = 0;
						k < ((int[])passed_parameters[i]).Length;
						k++)
					{
						//string array_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
						string array_name = parameter_info[i].Name;
						Runtime.setArrayElement(
							array_name,
							k + 1,
							numbers.Numbers.make_value__3(((int[])passed_parameters[i])[k]));
					}
				}
				else if (parameter_info[i].ParameterType.Name == "Single[]" ||
					(parameter_info[i].ParameterType.Name == "Single[]&" &&
					passed_parameters[i] == clone_passed_parameters[i]))
				{
					for (int k = 0;
						k < ((float[])passed_parameters[i]).Length;
						k++)
					{
						if (((float[])passed_parameters[i])[k] !=
							((float[])clone_passed_parameters[i])[k])
						{
							//string array_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
							string array_name = parameter_info[i].Name;
							Runtime.setArrayElement(
								array_name,
								k + 1,
								numbers.Numbers.make_value__2((double)((float[])passed_parameters[i])[k]));
						}
					}
				}
				else if (parameter_info[i].ParameterType.Name == "Single[]&")
				{
					for (int k = 0;
						k < ((float[])passed_parameters[i]).Length;
						k++)
					{
						//string array_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
						string array_name = parameter_info[i].Name;
						Runtime.setArrayElement(
							array_name,
							k + 1,
							numbers.Numbers.make_value__2((double)((float[])passed_parameters[i])[k])) ;
					}
				}
				else if (parameter_info[i].ParameterType.Name == "Double[]" ||
					(parameter_info[i].ParameterType.Name == "Double[]&" &&
					passed_parameters[i] == clone_passed_parameters[i]))
				{
					for (int k = 0;
						k < ((double[])passed_parameters[i]).Length;
						k++)
					{
						if (((double[])passed_parameters[i])[k] !=
							((double[])clone_passed_parameters[i])[k])
						{
							//string array_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
							string array_name = parameter_info[i].Name;
							Runtime.setArrayElement(
								array_name,
								k + 1,
								numbers.Numbers.make_value__2(((double[])passed_parameters[i])[k]));
						}
					}
				}
				else if (parameter_info[i].ParameterType.Name == "Double[]&")
				{
					for (int k = 0;
						k < ((double[])passed_parameters[i]).Length;
						k++)
					{
						//string array_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
						string array_name = parameter_info[i].Name;
						Runtime.setArrayElement(
							array_name,
							k + 1,
							numbers.Numbers.make_value__2(((double[])passed_parameters[i])[k]));
					}
				}
				else if (parameter_info[i].ParameterType.Name == "Int32[][]" ||
					(parameter_info[i].ParameterType.Name == "Int32[][]&" &&
					passed_parameters[i] == clone_passed_parameters[i]))
				{
					for (int k = 0;
						k < ((int[][])passed_parameters[i]).Length;
						k++)
					{
						for (int l = 0;
							l < ((int[][])passed_parameters[i])[0].Length;
							l++)
						{
							if (((int[][])passed_parameters[i])[k][l] !=
								((int[][])clone_passed_parameters[i])[k][l])
							{
								//string array_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
								string array_name = parameter_info[i].Name;
								Runtime.set2DArrayElement(
									array_name,
									k + 1,
									l + 1,
									numbers.Numbers.make_value__2((double)(float)((int[][])passed_parameters[i])[k][l]));
							}
						}
					}
				}
				else if (parameter_info[i].ParameterType.Name == "Int32[][]&")
				{
					for (int k = 0;
						k < ((int[][])passed_parameters[i]).Length;
						k++)
					{
						for (int l = 0;
							l < ((int[][])passed_parameters[i])[0].Length;
							l++)
						{
							//string array_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
							string array_name = parameter_info[i].Name;
							Runtime.set2DArrayElement(
								array_name,
								k + 1,
								l + 1,
								numbers.Numbers.make_value__2((double)(float)((int[][])passed_parameters[i])[k][l]));
						}
					}
				}
				else if (parameter_info[i].ParameterType.Name == "Single[][]" ||
					(parameter_info[i].ParameterType.Name == "Single[][]&" &&
					passed_parameters[i] == clone_passed_parameters[i]))
				{
					for (int k = 0;
						k < ((float[][])passed_parameters[i]).Length;
						k++)
					{
						for (int l = 0;
							l < ((float[][])passed_parameters[i])[0].Length;
							l++)
						{
							if (((float[][])passed_parameters[i])[k][l] !=
								((float[][])clone_passed_parameters[i])[k][l])
							{
								//string array_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
								string array_name = parameter_info[i].Name;
								Runtime.set2DArrayElement(
									array_name,
									k + 1,
									l + 1,
									numbers.Numbers.make_value__2((double)((float[][])passed_parameters[i])[k][l]));
							}
						}
					}
				}
				else if (parameter_info[i].ParameterType.Name == "Single[][]&")
				{
					for (int k = 0;
						k < ((float[][])passed_parameters[i]).Length;
						k++)
					{
						for (int l = 0;
							l < ((float[][])passed_parameters[i])[0].Length;
							l++)
						{
							//string array_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
							string array_name = parameter_info[i].Name;
							Runtime.set2DArrayElement(
								array_name,
								k + 1,
								l + 1,
								numbers.Numbers.make_value__2((double)((float[][])passed_parameters[i])[k][l]));
						}
					}
				}
				else if (parameter_info[i].ParameterType.Name == "Double[][]" ||
					(parameter_info[i].ParameterType.Name == "Double[][]&" &&
					passed_parameters[i] == clone_passed_parameters[i]))
				{
					for (int k = 0;
						k < ((double[][])passed_parameters[i]).Length;
						k++)
					{
						for (int l = 0;
							l < ((double[][])passed_parameters[i])[0].Length;
							l++)
						{
							if (((double[][])passed_parameters[i])[k][l] !=
								((double[][])clone_passed_parameters[i])[k][l])
							{
								//string array_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
								string array_name = parameter_info[i].Name;
								Runtime.set2DArrayElement(
									array_name,
									k + 1,
									l + 1,
									numbers.Numbers.make_value__2((double)((double[][])passed_parameters[i])[k][l]));
							}
						}
					}
				}
				else if (parameter_info[i].ParameterType.Name == "Double[][]&")
				{
					for (int k = 0;
						k < ((double[][])passed_parameters[i]).Length;
						k++)
					{
						for (int l = 0;
							l < ((double[][])passed_parameters[i])[0].Length;
							l++)
						{
							//string array_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
							string array_name = parameter_info[i].Name;
							Runtime.set2DArrayElement(
								array_name,
								k + 1,
								l + 1,
								numbers.Numbers.make_value__2(((double[][])passed_parameters[i])[k][l]));
						}
					}
				}
				walk = walk.next;
			}
			if (result == null)
			{
				return numbers.Numbers.make_value__2(0.0);
			}
			if (result.GetType().Name == "Single")
			{
				return numbers.Numbers.make_value__2((double)result);
			}
			else if (result.GetType().Name == "Double")
			{
				return numbers.Numbers.make_value__2((double)result);
			}
			else if (result.GetType().Name == "Int32")
			{
				return numbers.Numbers.make_value__3((int)result);
			}
			else if (result.GetType().Name == "String")
			{
				return numbers.Numbers.make_string_value((string)result);
			}
			else if (result.GetType().Name == "Boolean")
			{
				bool b = ((bool)result);
				if (b)
				{
					return numbers.Numbers.make_value__2(1.0);
				}
				else
				{
					return numbers.Numbers.make_value__2(0.0);
				}
			}
			return numbers.Numbers.make_value__2(0.0);
        }


		public static void Invoke(string name,
			parse_tree.Parameter_List parameters)
		{
			numbers.value dummy = Invoke_Function(name, parameters);
		}

		public static int Parameter_Count(string name)
		{
			try
			{
				MethodInfo method = GetMethod(name);
				if (method == null) return 0;
				return method.GetParameters().Length;
			}
			catch
			{
				return 0;
			}
		}


		public static bool Is_Function(string name)
        {
			try
			{
				MethodInfo method = GetMethod(name);
				if (method == null) return false;
				return method.ReturnType.Name == "Single" ||
					method.ReturnType.Name == "String" ||
					method.ReturnType.Name == "Double" ||
					method.ReturnType.Name == "Int32";
			}
			catch
			{
				return false;
			}
		}

        public static bool Is_Procedure(string name)
        {
			try
			{
				MethodInfo method = GetMethod(name);
				if (method == null) return false;
				return method.ReturnType.Name == "Void";
			}
			catch
			{
				return false;
			}
		}

		public static bool Is_Boolean_Function(string name)
		{
			try
			{
				MethodInfo method = GetMethod(name);
				if (method == null) return false;
				return method.ReturnType.Name == "Boolean";
			}
			catch
			{
				return false;
			}
		}


		public static bool Is_Pluginable_Method(System.Reflection.MethodInfo m)
		{
			return m.IsStatic &&
								(m.ReturnType.Name == "Void" ||
								 m.ReturnType.Name == "Int32" ||
								 m.ReturnType.Name == "Single" ||
								 m.ReturnType.Name == "String" ||
								 m.ReturnType.Name == "Double" ||
								 m.ReturnType.Name == "Boolean");
		}


		public static void Process_Assembly(System.Reflection.Assembly assembly,
			System.Collections.ArrayList method_list)
		{
			System.Reflection.MethodInfo[] part_methods;
			System.Type[] Types = assembly.GetTypes();
			for (int k = 0; k < Types.Length; k++)
			{
				if (Types[k].IsPublic)
				{
					part_methods = Types[k].GetMethods();
					for (int j = 0; j < part_methods.Length; j++)
					{
						if (Is_Pluginable_Method(part_methods[j]))
						{
							method_list.Add(part_methods[j]);
							string addMe = part_methods[j].Name.ToLower();
                            if (part_methods[j].GetParameters().Length != 0)
                            {
								addMe += "(";
								for(int n = 0; n < part_methods[j].GetParameters().Length; n++)
                                {
									addMe += part_methods[j].GetParameters()[n].Name;
									if(n != part_methods[j].GetParameters().Length - 1)
                                    {
										addMe += ", ";
                                    }
                                }
								addMe += ")";
                            }
							raptor.Suggestions.specialWords.Add(addMe);
						}
					}
				}
			}
		}


		internal static void Emit_Invoke_Function(string name,
			parse_tree.Parameter_List parameters,
			Generate_IL gil)
		{
			numbers.value dummyValue = new numbers.value();
			Type numbersValueType = dummyValue.GetType();
			ILGenerator gen = gil.gen;
			parse_tree.Parameter_List walk;
			int num_parameters;
			MethodInfo method = GetMethod(name);
			ParameterInfo[] parameter_info = method.GetParameters();
			num_parameters = parameter_info.Length;
			LocalBuilder[] local_parameters = new LocalBuilder[num_parameters];
			walk = parameters;
			for (int i = 0; i < num_parameters; i++)
			{

				if (parameter_info[i].ParameterType.Name == "Int32")
				{
					((parse_tree.Expr_Output)walk.parameter).expr.Emit_Code(gil);
					gil.Emit_Method("numbers_pkg", "integer_of");
				}
				else if (parameter_info[i].ParameterType.Name == "Single")
				{
					((parse_tree.Expr_Output)walk.parameter).expr.Emit_Code(gil);
					gil.Emit_Method("numbers_pkg", "long_float_of");
					gen.Emit(OpCodes.Conv_R4);
				}
				else if (parameter_info[i].ParameterType.Name == "Double")
				{
					((parse_tree.Expr_Output)walk.parameter).expr.Emit_Code(gil);
					gil.Emit_Method("numbers_pkg", "long_float_of");
				}
				else if (parameter_info[i].ParameterType.Name == "String")
				{
					try
					{
						//string var_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
						FieldInfo field = numbersValueType.GetField("s");
						// mcc: moved this emit load after the GetType, because we don't
						// want this instruction emitted if the GetType throws an exception
						gil.Emit_Load(name);
						gen.Emit(OpCodes.Ldfld, field);
					}
					catch
					{
						((parse_tree.Expr_Output)walk.parameter).expr.Emit_Code(gil);
						gil.Emit_Method("numbers_pkg", "string_of");
					}
				}
				else if (parameter_info[i].ParameterType.Name == "Int32&")
				{
					throw new System.Exception("parameter type \"ref int\" of method " + method.Name + " not supported.");
				}
				else if (parameter_info[i].ParameterType.Name == "Single&")
				{
					throw new System.Exception("parameter type \"ref float\" of method " + method.Name + " not supported.");
				}
				else if (parameter_info[i].ParameterType.Name == "Double&")
				{
					//string var_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
					gil.Emit_Load(name);

					FieldInfo field = numbersValueType.GetField("v");
					gen.Emit(OpCodes.Ldflda, field);
				}
				else if (parameter_info[i].ParameterType.Name == "Int32[]" ||
					parameter_info[i].ParameterType.Name == "Int32[]&")
				{
					//string var_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
					gil.Emit_Load(name);
					gil.Emit_Method_Virt("raptor.Value_Array", "get_Int32a");
					local_parameters[i] = gen.DeclareLocal(System.Type.GetType("System.Int32[]"));
					gen.Emit(OpCodes.Stloc, local_parameters[i]);
					if (parameter_info[i].ParameterType.Name == "Int32[]&")
					{
						gen.Emit(OpCodes.Ldloca, local_parameters[i]);
					}
					else
					{
						gen.Emit(OpCodes.Ldloc, local_parameters[i]);
					}
				}
				else if (parameter_info[i].ParameterType.Name == "Single[]" ||
					parameter_info[i].ParameterType.Name == "Single[]&")
				{
					//string var_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
					gil.Emit_Load(name);
					gil.Emit_Method_Virt("raptor.Value_Array", "get_Singlea");
					local_parameters[i] = gen.DeclareLocal(System.Type.GetType("System.Single[]"));
					gen.Emit(OpCodes.Stloc, local_parameters[i]);
					if (parameter_info[i].ParameterType.Name == "Single[]&")
					{
						gen.Emit(OpCodes.Ldloca, local_parameters[i]);
					}
					else
					{
						gen.Emit(OpCodes.Ldloc, local_parameters[i]);
					}
				}
				else if (parameter_info[i].ParameterType.Name == "Double[]" ||
					parameter_info[i].ParameterType.Name == "Double[]&")
				{
					//string var_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
					gil.Emit_Load(name);
					gil.Emit_Method_Virt("raptor.Value_Array", "get_Doublea");
					local_parameters[i] = gen.DeclareLocal(System.Type.GetType("System.Double[]"));
					gen.Emit(OpCodes.Stloc, local_parameters[i]);
					if (parameter_info[i].ParameterType.Name == "Double[]&")
					{
						gen.Emit(OpCodes.Ldloca, local_parameters[i]);
					}
					else
					{
						gen.Emit(OpCodes.Ldloc, local_parameters[i]);
					}
				}
				else if (parameter_info[i].ParameterType.Name == "Int32[][]" ||
					parameter_info[i].ParameterType.Name == "Int32[][]&")
				{
					//string var_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
					gil.Emit_Load(name);
					gil.Emit_Method_Virt("raptor.Value_2D_Array", "get_Int32aa");
					local_parameters[i] = gen.DeclareLocal(System.Type.GetType("System.Int32[][]"));
					gen.Emit(OpCodes.Stloc, local_parameters[i]);
					if (parameter_info[i].ParameterType.Name == "Int32[][]&")
					{
						gen.Emit(OpCodes.Ldloca, local_parameters[i]);
					}
					else
					{
						gen.Emit(OpCodes.Ldloc, local_parameters[i]);
					}
				}
				else if (parameter_info[i].ParameterType.Name == "Single[][]" ||
					parameter_info[i].ParameterType.Name == "Single[][]&")
				{
					//string var_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
					gil.Emit_Load(name);
					gil.Emit_Method_Virt("raptor.Value_2D_Array", "get_Singleaa");
					local_parameters[i] = gen.DeclareLocal(System.Type.GetType("System.Single[][]"));
					gen.Emit(OpCodes.Stloc, local_parameters[i]);
					if (parameter_info[i].ParameterType.Name == "Single[][]&")
					{
						gen.Emit(OpCodes.Ldloca, local_parameters[i]);
					}
					else
					{
						gen.Emit(OpCodes.Ldloc, local_parameters[i]);
					}
				}
				else if (parameter_info[i].ParameterType.Name == "Double[][]" ||
					parameter_info[i].ParameterType.Name == "Double[][]&")
				{
					//string var_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
					gil.Emit_Load(name);
					gil.Emit_Method_Virt("raptor.Value_2D_Array", "get_Doubleaa");
					local_parameters[i] = gen.DeclareLocal(System.Type.GetType("System.Double[][]"));
					gen.Emit(OpCodes.Stloc, local_parameters[i]);
					if (parameter_info[i].ParameterType.Name == "Double[][]&")
					{
						gen.Emit(OpCodes.Ldloca, local_parameters[i]);
					}
					else
					{
						gen.Emit(OpCodes.Ldloc, local_parameters[i]);
					}
				}
				walk = walk.next;
			}
			Set_Is_Used(method.DeclaringType.Assembly.GetName().Name);
			gen.Emit(OpCodes.Call, method);
			walk = parameters;

			for (int i = 0; i < num_parameters; i++)
			{
				if (parameter_info[i].ParameterType.Name == "Int32&")
				{
				}
				else if (parameter_info[i].ParameterType.Name == "Single&")
				{
				}
				else if (parameter_info[i].ParameterType.Name == "Int32[]" ||
					parameter_info[i].ParameterType.Name == "Int32[]&")
				{
					//string var_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
					gil.Emit_Load(name);
					gen.Emit(OpCodes.Ldloc, local_parameters[i]);
					gil.Emit_Method_Virt("raptor.Value_Array", "set_Int32a");
				}
				else if (parameter_info[i].ParameterType.Name == "Single[]" ||
					parameter_info[i].ParameterType.Name == "Single[]&")
				{
					//string var_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
					gil.Emit_Load(name);
					gen.Emit(OpCodes.Ldloc, local_parameters[i]);
					gil.Emit_Method_Virt("raptor.Value_Array", "set_Singlea");
				}
				else if (parameter_info[i].ParameterType.Name == "Double[]" ||
					parameter_info[i].ParameterType.Name == "Double[]&")
				{
					//string var_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
					gil.Emit_Load(name);
					gen.Emit(OpCodes.Ldloc, local_parameters[i]);
					gil.Emit_Method_Virt("raptor.Value_Array", "set_Doublea");
				}
				else if (parameter_info[i].ParameterType.Name == "Int32[][]" ||
					parameter_info[i].ParameterType.Name == "Int32[][]&")
				{
					//string var_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
					gil.Emit_Load(name);
					gen.Emit(OpCodes.Ldloc, local_parameters[i]);
					gil.Emit_Method_Virt("raptor.Value_2D_Array", "set_Int32aa");
				}
				else if (parameter_info[i].ParameterType.Name == "Single[][]" ||
					parameter_info[i].ParameterType.Name == "Single[][]&")
				{
					//string var_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
					gil.Emit_Load(name);
					gen.Emit(OpCodes.Ldloc, local_parameters[i]);
					gil.Emit_Method_Virt("raptor.Value_2D_Array", "set_Singleaa");
				}
				else if (parameter_info[i].ParameterType.Name == "Double[][]" ||
					parameter_info[i].ParameterType.Name == "Double[][]&")
				{
					//string var_name = ((parse_tree.Expr_Output)walk.parameter).get_string();
					gil.Emit_Load(name);
					gen.Emit(OpCodes.Ldloc, local_parameters[i]);
					gil.Emit_Method_Virt("raptor.Value_2D_Array", "set_Doubleaa");
				}
				walk = walk.next;
			}

			if (method.ReturnType.Name == "Single")
			{
				gen.Emit(OpCodes.Conv_R8);
				gil.Emit_Method("numbers_pkg", "make_value__2");
			}
			else if (method.ReturnType.Name == "Double")
			{
				gil.Emit_Method("numbers_pkg", "make_value__2");
			}
			else if (method.ReturnType.Name == "Int32")
			{
				gil.Emit_Method("numbers_pkg", "make_value__3");
			}
			else if (method.ReturnType.Name == "String")
			{
				gil.Emit_Method("numbers_pkg", "make_string_value");
			}
			else if (method.ReturnType.Name == "Boolean")
			{
				// don't need to do anything on a Boolean result
			}
		}

		private static System.Collections.Generic.List<string> plugins =
			new System.Collections.Generic.List<string>();
		private static System.Collections.Generic.List<string> assemblies =
			new System.Collections.Generic.List<string>();
		private static System.Collections.Generic.Dictionary<string, bool> is_used =
			new System.Collections.Generic.Dictionary<string, bool>();
		public static string[] Get_Plugin_List()
		{
			int count = 0;
			for (int i = 0; i < assemblies.Count; i++)
			{
				if (is_used[assemblies[i]])
				{
					count++;
				}
			}
			string[] result = new string[count];
			for (int i = 0; i < plugins.Count; i++)
			{
				if (is_used[assemblies[i]])
				{
					result[--count] = plugins[i];
				}
			}
			return result;
		}
		public static string[] Get_Assembly_Names()
		{
			int count = 0;
			for (int i = 0; i < assemblies.Count; i++)
			{
				if (is_used[assemblies[i]])
				{
					count++;
				}
			}
			string[] result = new string[count];
			for (int i = 0; i < assemblies.Count; i++)
			{
				if (is_used[assemblies[i]])
				{
					result[--count] = assemblies[i];
				}
			}
			return result;
		}

		public static void Set_Is_Used(string name)
		{
			is_used[name] = true;
		}

		public static void Load_Plugins(string filename)
		{
			System.Reflection.Assembly assembly;
			System.Collections.ArrayList method_list = new System.Collections.ArrayList();
			//System.IO.DirectoryInfo exe_path = System.IO.Directory.GetParent(System.Windows.Forms.Application.ExecutablePath);
			System.IO.DirectoryInfo exe_path = System.IO.Directory.GetParent(Assembly.GetEntryAssembly().Location);
			plugins.Clear();
			assemblies.Clear();
			/*try
            {
                System.Reflection.Assembly as2;
                as2 = Assembly.LoadFrom(exe_path + "\\CodeProjectComBigIntegerPackage.dll");
            }
            catch
            {
            }*/
			System.IO.FileInfo[] files = exe_path.GetFiles("plugin*.dll");
			for (int i = 0; i < files.Length; i++)
			{
				try
				{
					assembly = Assembly.LoadFrom(files[i].FullName);
					Process_Assembly(assembly, method_list);
					plugins.Add(files[i].FullName);
					assemblies.Add(assembly.GetName().Name);
					if (!is_used.ContainsKey(assembly.GetName().Name))
					{
						is_used.Add(assembly.GetName().Name, false);
					}
				}
				catch
				{
				}
			}
			if (filename != "")
			{
				try
				{
					System.IO.DirectoryInfo file_path = System.IO.Directory.GetParent(filename);
					files = file_path.GetFiles("plugin*.dll");
					for (int i = 0; i < files.Length; i++)
					{
						try
						{
							assembly = Assembly.LoadFrom(files[i].FullName);
							Process_Assembly(assembly, method_list);
							plugins.Add(files[i].FullName);
							assemblies.Add(assembly.GetName().Name);
							is_used.Add(assembly.GetName().Name, false);
						}
						catch
						{
						}
					}
				}
				catch
				{
				}
			}
			if (method_list.Count > 0)
			{
				methods = new System.Reflection.MethodInfo[method_list.Count];
				Suggestions = new string[methods.Length];
				for (int j = 0; j < methods.Length; j++)
				{
					methods[j] = (System.Reflection.MethodInfo)method_list[j];
					Suggestions[j] = methods[j].Name;

					ParameterInfo[] parameters = methods[j].GetParameters();
					if (parameters.Length >= 1)
					{
						Suggestions[j] = Suggestions[j] + "(";
					}
					for (int k = 0; k < parameters.Length - 1; k++)
					{
						Suggestions[j] = Suggestions[j] +
							parameters[k].Name + ",";
					}
					if (parameters.Length >= 1)
					{
						Suggestions[j] = Suggestions[j] +
							parameters[parameters.Length - 1].Name;
						Suggestions[j] = Suggestions[j] + ")";
					}
				}
			}

		}

		static char[] separators = new char[] { ',', ')' };

	}
}
