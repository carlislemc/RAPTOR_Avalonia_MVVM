using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;


namespace raptor
{
    [Serializable]
    [DataContract]
    class Oval_Procedure : Oval
    {
        [DataMember]
        protected int num_params;
        [DataMember]
        public string[] param_names;
        [DataMember]
        protected bool[] param_is_input;
        [DataMember]
        public bool[] param_is_output;
        public void changeParameters(int num_params, string[] param_names, bool[] param_is_input, bool[] param_is_output)
        {
            this.num_params = num_params;
            this.param_names = param_names;
            this.param_is_input = param_is_input;
            this.param_is_output = param_is_output;
            this.SetText();
        }
        /*public string RunDialog(string name, Visual_Flow_Form form)
        {
            string result;
            result = Procedure_name.RunDialog(name, ref param_names, ref param_is_input, ref param_is_output,
                form);
            if (result != null && result != "")
            {
                num_params = param_is_output.Length;
                this.SetText();
            }
            return result;
        }*/
        public string[] getArgs()
        {
            return param_names;
        }
        public bool[] getArgIsInput()
        {
            return param_is_input;
        }
        public bool[] getArgIsOutput()
        {
            return param_is_output;
        }
        public int Parameter_Count
        {
            get
            {
                return num_params;
            }
            set{
                num_params = value;
            }
        }
        public string Parameter_String
        {
            get
            {
                // skip past "Start "
                return this.Text.Substring(6);
            }
        }
        public string Param_Name(int i)
        {
            return param_names[i];
        }
        public string Param_String(int i)
        {
            string result = "";
            if (param_is_input[i])
            {
                result += "in ";
            }
            if (param_is_output[i])
            {
                result += "out ";
            }
            result += param_names[i];
            return result;
        }
        public bool is_input_parameter(int i)
        {
            return param_is_input[i];
        }
        public bool is_output_parameter(int i)
        {
            return param_is_output[i];
        }
        public Oval_Procedure(Component Successor, int height, int width, String str_name, int param_count)
			: base(Successor, height, width, str_name)
		{
            // placeholder until full allocation later
            // this is not for long-term use
            num_params = param_count;
		}
        public Oval_Procedure(Component Successor, int height, int width, String str_name,
            string[] incoming_param_names,
            bool[] is_input, bool[] is_output)
            : base(Successor, height, width, str_name)
        {
            param_names = incoming_param_names;
            num_params = incoming_param_names.Length;
            param_is_input = is_input;
            param_is_output = is_output;
            SetText();
        }

        private void SetText()
        {
            this.Text = "Start (";
            for (int i = 0; i < num_params; i++)
            {
                if (i > 0)
                {
                    this.Text = this.Text + ",";
                }
                if (param_is_input[i])
                {
                    this.Text = this.Text + "in ";
                }
                if (param_is_output[i])
                {
                    this.Text = this.Text + "out ";
                }
                this.Text = this.Text + param_names[i];
            }
            this.Text = this.Text + ")";
        }
        public Oval_Procedure(SerializationInfo info, StreamingContext ctxt)
			: base(info,ctxt)
		{
            this.num_params = info.GetInt32("_numparams");
            this.param_names = new string[this.num_params];
            this.param_is_input = new bool[this.num_params];
            this.param_is_output = new bool[this.num_params];
            for (int i = 0; i < num_params; i++)
            {
                this.param_names[i] = info.GetString("_paramname" + i);
                this.param_is_input[i] = info.GetBoolean("_paraminput" + i);
                this.param_is_output[i] = info.GetBoolean("_paramoutput" + i);
            }
        }
        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
            info.AddValue("_numparams", this.num_params);
            for (int i = 0; i < num_params; i++)
            {
                info.AddValue("_paramname" + i, this.param_names[i]);
                info.AddValue("_paraminput" + i, this.param_is_input[i]);
                info.AddValue("_paramoutput" + i, this.param_is_output[i]);
            }
        }
        public override void collect_variable_names(System.Collections.Generic.IList<string> l,
            System.Collections.Generic.IDictionary<string, string> types)
        {
            if (this.param_names != null)
            {
                for (int i = 0; i < this.param_names.Length; i++)
                {
                    l.Add(this.param_names[i]);
                }
            }
            if (this.Successor != null)
            {
                this.Successor.collect_variable_names(l,types);
            }
        }

        public override void draw(Avalonia.Media.DrawingContext gr, int x, int y)
        {
            base.draw(gr, x, y);
        }
        public override bool setText(int x, int y)
        {
            bool textset = false;
            if (contains(x, y))
            {
                textset = true;
                if (Component.Current_Mode != Mode.Expert)
                {
                    //form.menuRenameSubchart_Click(null, null);
                }
                return textset;
            }

            if (this.Successor != null)
            {
                return (this.Successor.setText(x, y));
            }

            return textset;
        }

    }
}
