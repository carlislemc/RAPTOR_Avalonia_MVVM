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


		public ObservableCollection<string> getSuggestions()
        {

            if (variableName)
            {
                ObservableCollection<string> ans = new ObservableCollection<string>();
                ObservableCollection<string> varNames = getVariableNames();
                if (str == "")
                {
                    return ans;
                }
                foreach (string s in varNames)
                {
                    if (str.Length > s.Length)
                    {
                        continue;
                    }
                    if (s.Substring(0, str.Length) == str)
                    {
                        ans.Add(s);
                    }

                }
                return ans;
            }
            else
            {

                ObservableCollection<string> ans = new ObservableCollection<string>();
                ObservableCollection<string> varNames = getVariableNames();
                ObservableCollection<string> strParts = parseInput(str);
                foreach(string st in strParts)
                {
                    if (st == "")
                    {
                        continue;
                    }
                    foreach (string s in varNames)
                    {
                        if (st.Length > s.Length)
                        {
                            continue;
                        }
                        if (s.Substring(0, st.Length) == st)
                        {
                            if (!ans.Contains(s))
                            {
                                ans.Add(s);
                            }
                            
                        }

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
                    
                    for (int i = 0; i < strParts.Count; i++)
                    {
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
                            ans.Add(s);
                        }

                    }

                }
                return ans;
            }

        }

        public ObservableCollection<string> parseInput(string str)
        {
            ObservableCollection<string> ans = new ObservableCollection<string>() { "" };
            ObservableCollection<int> spots = new ObservableCollection<int>() { 0 };
            int quoteCount = 0;
            string temp = "";
            for(int i = 0; i < str.Length; i++)
            {
                char letter = str[i];
                if(letter == '"')
                {
                    quoteCount = (quoteCount + 1) % 2;
                }

                if(quoteCount != 0)
                {
                    continue;
                }

                if(letter == '(')
                {
                    temp += letter;
                    ans[ans.Count - 1] = temp;
                    spots.Add(ans.Count - 1);
                    temp = "";
                    ans.Add(temp);

                }
                else if(letter == ')')
                {
                    temp += letter;
                    ans[spots[spots.Count-1]] = temp;
                    ans.RemoveAt(ans.Count - 1);
                    spots.RemoveAt(spots.Count - 1);
                }
                else if(letter == '+' || letter == '-' || letter == '*' || letter == '/' || letter == '=' || letter == ',')
                {
                    ans[ans.Count - 1] = temp + letter;
                    temp = "";
                    ans.Add(temp);
                }
                else
                {
                    temp += letter;
                    ans[ans.Count - 1] = temp;
                }

            }

            return ans;

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

