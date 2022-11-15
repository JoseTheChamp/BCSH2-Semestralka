using System;

namespace BCSH2_Semestralka.Model.ParserClasses.Context
{
    public class MyExecutionContext : ICloneable
    {
        public ProgramContext ProgramContext { get; set; }
        public Variables Variables { get; set; }
        //public MyExecutionContext UpperExecutionContext { get; set; }

        public MyExecutionContext()
        {
            ProgramContext = new ProgramContext();
            Variables = new Variables();
            //UpperExecutionContext = new MyExecutionContext();
        }

        public object Clone()
        {
            MyExecutionContext oldExecutionContext = this;
            MyExecutionContext newExecutionContext = new MyExecutionContext();
            newExecutionContext.ProgramContext = oldExecutionContext.ProgramContext;
            foreach (Variable variable in oldExecutionContext.Variables.Vars)
            {
                newExecutionContext.Variables.Vars.Add((Variable)variable.Clone());
            }
            return newExecutionContext;
        }
    }
}