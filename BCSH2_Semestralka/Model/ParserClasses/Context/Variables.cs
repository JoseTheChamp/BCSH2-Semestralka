using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Semestralka.Model.ParserClasses.Context
{
    public class Variables
    {
        public List<Variable> Vars { get; set; }

        public Variables()
        {
            Vars = new List<Variable>();
        }
        public Variables(List<Variable> vars)
        {
            Vars = vars;
        }
        public void AddVariable(Variable variable, bool newContext) {
            foreach (var item in Vars.ToList())
            {
                if (item.Ident == variable.Ident)
                {
                    if (newContext)
                    {
                        Vars.Remove(item);
                    }
                    else {
                        throw new Exception("This variable was already defined. [" + variable.Ident + "].");
                    }
                }
            }
            Vars.Add(variable);
        }
        public object Get(string ident)
        {

            foreach (var var in Vars)
            {
                if (var.Ident == ident)
                {
                    return var.Value;
                }
            }
            throw new Exception("This variable was already defined. [" + ident + "].");
        }

        public void Set(string ident, object value)
        {
            foreach (var var in Vars)
            {
                if (var.Ident == ident)
                {
                    if (var.IsVal == false)
                    {
                        switch (var.DataType)
                        {
                            case DataType.String:
                                var.Value = value.ToString();
                                return;
                            case DataType.Int:
                                if (value.GetType() == typeof(Int32) || value.GetType() == typeof(double))
                                {
                                    var.Value = Convert.ToInt32(value);
                                    return;
                                }
                                else
                                {
                                    throw new Exception("You cannot enter this value into int: " + value.ToString());
                                }
                            case DataType.Double:
                                if (value.GetType() == typeof(double))
                                {
                                    var.Value = value;
                                    return;
                                }
                                else
                                {
                                    throw new Exception("You cannot enter this value into Dounle: " + value.ToString());
                                }
                            default:
                                throw new Exception("Unexpected exception.");
                        }
                    }
                    else
                    {
                        throw new Exception("You cannot write into Val.");
                    }
                }
            }
            throw new Exception("This variable was already defined: " + ident);
        }
    }
}
