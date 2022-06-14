using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using MessageBox.Avalonia.DTO;
using Avalonia.Controls;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using raptor;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Avalonia.Input;
using ReactiveUI;
using System.Reactive;
using interpreter;
using numbers;
using parse_tree;
using System.Timers;
using System.Threading;
using RAPTOR_Avalonia_MVVM.Views;

using Avalonia.Markup.Xaml;
using RAPTOR_Avalonia_MVVM.ViewModels;
using System.Collections;
using Avalonia.Threading;

namespace raptor
{
	public class Suggestions
	{

		public bool variableName;
		public string str;
		public Subchart sub;
        public Component comp;
		public Suggestions(Component c, string s, bool varName, Subchart sub)
		{
            comp = c;
			str = s.Replace(" ","");
			variableName = varName;
			this.sub = sub;
		}

		public string getSuggestions()
        {

            if (variableName)
            {
                string ans = "";
                ObservableCollection<string> varNames = getVariableNames();
                if (str == "")
                {
                    return "";
                }
                foreach (string s in varNames)
                {
                    if (str.Length > s.Length)
                    {
                        continue;
                    }
                    if (s.Substring(0, str.Length) == str)
                    {
                        ans += s + '\n';
                    }

                }
                return ans;
            }
            else
            {

                string ans = "";
                ObservableCollection<string> varNames = getVariableNames();
                if (str == "")
                {
                    return "";
                }
                foreach (string s in varNames)
                {
                    if (str.Length > s.Length)
                    {
                        continue;
                    }
                    if (s.Substring(0, str.Length) == str)
                    {
                        ans += s + '\n';
                    }

                }

                foreach (string s in specialWords)
                {
                    if (comp.GetType() == typeof(Rectangle))
                    {
                        Rectangle tempRect = (Rectangle)comp;
                        if(tempRect.kind == Rectangle.Kind_Of.Assignment)
                        {
                            ObservableCollection<string> blacklist = new ObservableCollection<string>()
                            {
                                "redirect_input(yes/no or \"filename\")",
                                "redirect_output(yes/no or \"filename\")",
                                "redirect_output_append(yes/no or \"filename\")",
                                "freeze_graph_window",
                                "unfreeze_graph_window",
                                "update_graph_window",
                                "delay_for(seconds)",
                                "set_precision(precision)",
                                "clear_window(color)",
                                "clear_console",
                                "close_graph_window",
                                "display_text(x,y,\"text\",color)",
                                "key_down(\"key\")",
                                "display_number(x,y,number,color)",
                                "draw_arc(x1,y1,x2,y2,startx,starty,endx,endy,color)",
                                "draw_box(x1,y1,x2,y2,color,filled)",
                                "draw_circle(x,y,radius,color,filled)",
                                "draw_ellipse(x1,y1,x2,y2,color,filled)",
                                "draw_ellipse_rotate(x1,y1,x2,y2,angle,color,filled)",
                                "draw_line(x1,y1,x2,y2,color)",
                                "flood_fill(x,y,color)",
                                "draw_bitmap(bitmap,x,y,width,height)",
                                "open_graph_window(width,height)",
                                "wait_for_key",
                                "wait_for_mouse_button(which_button)",
                                "put_pixel(x,y,color)",
                                "set_window_title(\"title\")",
                                "save_graph_window(filename)",
                                "mouse_button_pressed(which_button)",
                                "mouse_button_down(which_button)",
                                "mouse_button_released(which_button)",
                                "set_font_size(size)"
                            };

                            if (blacklist.Contains(s))
                            {
                                continue;
                            }
                        }
                    }

                    int quoteCount = 0;
                    int commaCount = 0;
                    int bracketCount = 0;
                    int parenCount = 0;

                    int stepper = str.Length - 1;
                    while(stepper > -1)
                    {
                        if (str[stepper] == ',' && parenCount == 0 && quoteCount == 0 && bracketCount == 0)
                        {
                            commaCount++;
                        }
                        else if(str[stepper] == '[' && quoteCount == 0)
                        {
                            bracketCount++;
                        }
                        else if(str[stepper] == ']' && quoteCount == 0)
                        {
                            bracketCount--;
                        }
                        else if(str[stepper] == '(' && quoteCount == 0)
                        {
                            parenCount--;
                        }
                        else if(str[stepper] == ')' && quoteCount == 0)
                        {
                            parenCount++;
                        }
                        else if(str[stepper] == '"')
                        {
                            quoteCount = (quoteCount + 1) % 2;
                        }
                        stepper--;
                    }

                    if(parenCount != 0)
                    {
                        if(commaCount != 0)
                        {
                            string[] strParts = str.Split("(");
                            for (int i = 0; i < strParts.Length; i++)
                            {
                                if (i != strParts.Length - 1)
                                {
                                    strParts[i] += "(";
                                }
                                if (strParts[i] == "")
                                {
                                    continue;
                                }
                                if (strParts[i].Length > s.Length)
                                {
                                    continue;
                                }
                                if (s.Substring(0, strParts[i].Length) == strParts[i].ToLower())
                                {

                                    ans += s + '\n';
                                }
                            }
                        }
                        else
                        {
                            string[] strParts = str.Split("(");
                            for (int i = 0; i < strParts.Length; i++)
                            {
                                if (i != strParts.Length - 1)
                                {
                                    strParts[i] += "(";
                                }
                                if (strParts[i] == "")
                                {
                                    continue;
                                }
                                if (strParts[i].Length > s.Length)
                                {
                                    continue;
                                }
                                if (s.Substring(0, strParts[i].Length) == strParts[i].ToLower())
                                {

                                    ans += s + '\n';
                                }
                            }
                        }
                        

                    }
                    else
                    {
                        if (str.Length > s.Length)
                        {
                            continue;
                        }
                        if (s.Substring(0, str.Length) == str.ToLower())
                        {

                            ans += s + '\n';
                        }
                    }
                    


                }
                return ans;
            }

        }

        public ObservableCollection<string> specialWords = new ObservableCollection<string>()
        {
            "sin(x)",
            "cos(x)",
            "tan(x)",
            "cot(x)",
            "arcsin(x)",
            "arccos(x)",
            "log(x)",
            "arctan(y,x)",
            "arccot(y,x)",
            "min(x,y)",
            "max(x,y)",
            "sinh(x)",
            "tanh(x)",
            "cosh(x)",
            "arccosh(x)",
            "arcsinh(x)",
            "arctanh(x)",
            "coth(x)",
            "arccoth(x)",
            "sqrt(x)",
            "floor(x)",
            "ceiling(x)",
            "to_ascii(character)",
            "to_character(ascii)",
            "length_of(array)",
            "abs(x)",
            "e",
            "pi",
            "random",
            "black",
            "blue",
            "green",
            "cyan",
            "red",
            "magenta",
            "brown",
            "light_gray",
            "dark_gray",
            "light_blue",
            "light_green",
            "light_cyan",
            "light_red",
            "light_magenta",
            "yellow",
            "pink",
            "purple",
            "white",
            "filled",
            "unfilled",
            "true",
            "false",
            "yes",
            "no",
            "random_color",
            "random_extended_color",
            "left_button",
            "right_button",
            "closest_color(red, green, blue)",
            "redirect_input(yes/no or \"filename\")",
            "redirect_output(yes/no or \"filename\")",
            "redirect_output_append(yes/no or \"filename\")",
            "freeze_graph_window",
            "unfreeze_graph_window",
            "update_graph_window",
            "delay_for(seconds)",
            "set_precision(precision)",
            "get_max_width",
            "get_max_height",
            "get_mouse_x",
            "get_mouse_y",
            "get_font_width",
            "get_font_height",
            "get_window_width",
            "get_window_height",
            "get_key",
            "key_down(\"key\")",
            "get_key_string",
            "get_mouse_button(which_button,x,y)",
            "get_pixel(x,y)",
            "clear_window(color)",
            "clear_console",
            "close_graph_window",
            "display_text(x,y,\"text\",color)",
            "display_number(x,y,number,color)",
            "draw_arc(x1,y1,x2,y2,startx,starty,endx,endy,color)",
            "draw_box(x1,y1,x2,y2,color,filled)",
            "draw_circle(x,y,radius,color,filled)",
            "draw_ellipse(x1,y1,x2,y2,color,filled)",
            "draw_ellipse_rotate(x1,y1,x2,y2,angle,color,filled)",
            "draw_line(x1,y1,x2,y2,color)",
            "flood_fill(x,y,color)",
            "load_bitmap(filename)",
            "draw_bitmap(bitmap,x,y,width,height)",
            "open_graph_window(width,height)",
            "wait_for_key",
            "wait_for_mouse_button(which_button)",
            "put_pixel(x,y,color)",
            "set_window_title(\"title\")",
            "save_graph_window(filename)",
            "mouse_button_pressed(which_button)",
            "mouse_button_down(which_button)",
            "mouse_button_released(which_button)",
            "set_font_size(size)",
            "is_array(variable)",
            "is_character(variable)",
            "is_string(variable)",
            "is_2d_array(variable)",
            "is_number(variable)",
            "is_open",
            "key_hit",
            "end_of_input"
        };


		public ObservableCollection<string> getVariableNames()
        {
			MainWindowViewModel mw = MainWindowViewModel.GetMainWindowViewModel();
			ObservableCollection<string> answer = new ObservableCollection<string>();
			if(sub.Start.GetType() == typeof(Oval_Procedure))
            {

            } else
            {
                foreach (Subchart s in mw.theTabs)
                {
					if(s.Start.GetType() == typeof(Oval_Procedure))
                    {
						continue;
                    }

                    Component ans = s.Start;
                    ObservableCollection<int> loopPass = new ObservableCollection<int>() { 0 };
                    ObservableCollection<int> selectionPass = new ObservableCollection<int>() { 0 };
                    ObservableCollection<Component> p = new ObservableCollection<Component>();
                    while (ans.Successor != null || p.Count != 0)
                    {
                        if(ans.GetType() == typeof(Rectangle) && ans.text_str != "")
                        {
                            
                            Rectangle tempRec = (Rectangle)ans;
                            
                            if(tempRec.kind == Rectangle.Kind_Of.Assignment)
                            {
                                string stringy = tempRec.text_str.Substring(0, tempRec.text_str.IndexOf(":="));
                                if (stringy.Contains("["))
                                {
                                    stringy = stringy.Substring(0, stringy.IndexOf("["));
                                }
                                if (!answer.Contains(stringy))
                                {
                                    answer.Add(stringy);
                                }
                                
                            }
                        } else if(ans.GetType() == typeof(Parallelogram) && ans.text_str != "")
                        {
                            Parallelogram tempRec = (Parallelogram)ans;

                            if (tempRec.is_input)
                            {
                                string stringy = tempRec.text_str;
                                if (stringy.Contains("["))
                                {
                                    stringy = stringy.Substring(0, stringy.IndexOf("["));
                                }
                                if (!answer.Contains(stringy))
                                {
                                    answer.Add(stringy);
                                }

                            }
                        }

                        if (ans.Successor == null && p.Count != 0 && loopPass[loopPass.Count - 1] == 0 && selectionPass[selectionPass.Count - 1] == 0)
                        {
                            ans = p[p.Count - 1];
                            p.RemoveAt(p.Count - 1);
                        }

                        else if (ans.Successor == null && p.Count != 0 && ans.GetType() != typeof(Loop) && ans.GetType() != typeof(IF_Control))
                        {
                            ans = p[p.Count - 1];
                        }

                        if (ans.GetType() == typeof(Loop))
                        {
                            Loop temp = (Loop)ans;
                            if (!p.Contains(temp))
                            {
                                loopPass.Add(1);
                                p.Add(temp);
                            }

                            if (loopPass[loopPass.Count - 1] == 1)
                            {
                                if (temp.before_Child != null)
                                {
                                    ans = temp.before_Child;
                                }

                                loopPass[loopPass.Count - 1]++;
                            }
                            else if (loopPass[loopPass.Count - 1] == 2)
                            {
                                if (temp.after_Child != null)
                                {
                                    ans = temp.after_Child;
                                }
                                loopPass[loopPass.Count - 1]++;
                            }
                            else if (loopPass[loopPass.Count - 1] == 3)
                            {

                                p.RemoveAt(p.Count - 1);
                                if (temp.Successor != null)
                                {
                                    ans = temp.Successor;
                                }
                                else
                                {
                                    ans = p[p.Count - 1];
                                }
                                loopPass.RemoveAt(loopPass.Count - 1);
                            }
                        }

                        else if (ans.GetType() == typeof(IF_Control))
                        {
                            IF_Control temp = (IF_Control)ans;
                            if (!p.Contains(temp))
                            {
                                selectionPass.Add(1);
                                p.Add(temp);
                            }

                            if (selectionPass[selectionPass.Count - 1] == 1)
                            {
                                if (temp.left_Child != null)
                                {
                                    ans = temp.left_Child;
                                }
                                selectionPass[selectionPass.Count - 1]++;
                            }
                            else if (selectionPass[selectionPass.Count - 1] == 2)
                            {
                                if (temp.right_Child != null)
                                {
                                    ans = temp.right_Child;
                                }
                                selectionPass[selectionPass.Count - 1]++;
                            }
                            else if (selectionPass[selectionPass.Count - 1] == 3)
                            {
                                p.RemoveAt(p.Count - 1);
                                if (temp.Successor != null)
                                {
                                    ans = temp.Successor;
                                }
                                else
                                {
                                    ans = p[p.Count - 1];
                                }
                                selectionPass.RemoveAt(selectionPass.Count - 1);
                            }
                        }

                        else
                        {
                            ans = ans.Successor;
                        }

                    }



                }
            }


			return answer;
        }

	}
}

