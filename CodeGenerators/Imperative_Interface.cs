using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerators
{
    public interface Imperative_Interface : raptor.Generate_Interface
    {
        void Done_Method();
        void Declare_Procedure(
            string name,
            string[] args,
            bool[] arg_is_input,
            bool[] arg_is_output);
    }
}
