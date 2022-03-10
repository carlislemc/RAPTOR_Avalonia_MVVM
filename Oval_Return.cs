using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using Avalonia;

using RAPTOR_Avalonia_MVVM;
namespace raptor
{
    [Serializable]
    public class Oval_Return : Oval
    {

        public Oval_Return(int height, int width, String str_name)
            : base(height, width, str_name)
        {
            this.init();
        }

        public Oval_Return(Component Successor, int height, int width, String str_name)
            : base(Successor, height, width, str_name)
        {
            this.init();
        }

        public Oval_Return(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
            result = interpreter_pkg.output_syntax(this.Text, false, this);
            if (result.valid)
            {
                this.parse_tree = result.tree;
            }
            else
            {
                if (!Component.warned_about_error && this.Text != "")
                {
                    MessageBoxClass.Show("Unknown error: \n" +
                        this.Text + "\n" +
                        "is not recognized.");
                    Component.warned_about_error = true;
                }
                this.Text = "";
            }
        }
        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
        }
        public override void collect_variable_names(System.Collections.Generic.IList<string> l,
            System.Collections.Generic.IDictionary<string, string> types)
        {
            if (this.Successor != null)
            {
                this.Successor.collect_variable_names(l,types);
            }
        }
        
        public override void draw(Avalonia.Media.DrawingContext gr, int x, int y)
        {
            base.draw(gr, x, y);
        }
        public override bool SelectRegion(Avalonia.Rect rec)
        {
            bool b = false;
            if (this.Successor != null)
            {
                b = this.Successor.SelectRegion(rec);
            }
            if (this.contains(rec))
            {
                this.selected = true;
                return true;
            }
            else
            {
                this.selected = false;
            }
            return b;
        }
        public override bool editable_selected()
        {
            return this.selected;
        }
        // select me, all my successors and kids
        public override void selectAll()
        {
            this.selected = true;
            if (this.Successor != null)
            {
                this.Successor.selectAll();
            }
        }
        public override bool setText(int x, int y, Visual_Flow_Form form)
        {
            if (contains(x, y))
            {
                //Return_Dlg IOD = new Return_Dlg(this, form);
                //IOD.ShowDialog();
                return true;
            }

            if (this.Successor != null)
            {
                return (this.Successor.setText(x, y, form));
            }

            return false;
        }

        // Check to see if I or my successor or chidren have empty parse trees.
        public override bool has_code()
        {
            bool im_ok = true;
            bool my_successor_ok = true;

            if (this.Successor != null)
            {
                my_successor_ok = this.Successor.has_code();
            }
            if (this.parse_tree == null)
            {
                im_ok = false;
            }

            return (im_ok && my_successor_ok);
        }

        // Mark error if I or my successor or chidren have empty parse trees.
        public override void mark_error()
        {
            if (this.Successor != null)
            {
                this.Successor.mark_error();
            }
            if (this.parse_tree == null)
            {
                this.Text = "Error";
                //Runtime.parent.Show_Text_On_Error();
            }
        }

        public override string getDrawText()
        {
            if (this.Text != null && this.Text != "")
            {
                return "RETURN " + this.Text;
            }
            else
            {
                return "";
            }
        }
        protected override void drawConnector(Avalonia.Media.DrawingContext gr, Avalonia.Media.Pen pen)
        {
            gr.DrawLine(pen, new Point(X, Y + H), new Point(X, Y + H + CL/2)); // draw connector line to successor
            gr.DrawLine(pen, new Point(X, Y + H + CL / 2), new Point(X + W / 2, Y + H + CL / 2));  // draw line to right
            gr.DrawLine(pen, new Point(X+W/2, Y + H + CL/2), new Point(X +W/2- CL / 4, Y + H + CL/2 - CL / 4)); // draw left side of arrow
            gr.DrawLine(pen, new Point(X +W/2, Y + H + CL/2), new Point(X +W/2 -CL / 4, Y + H + CL/2 + CL / 4)); // draw right side of arrow
        }
        /*public override void Emit_Code(generate_interface.typ gen)
        {
            if (this.parse_tree != null)
            {
                parse_tree.expr_output eo = this.parse_tree as parse_tree.expr_output;
                ((generate_interface_oo.typ) gen).start_return();
                interpreter_pkg.emit_code(eo.expr, this.Text, gen);
                ((generate_interface_oo.typ) gen).end_return();
            }
            if (this.Successor != null)
            {
                this.Successor.Emit_Code(gen);
            }
        }*/
     }
}
