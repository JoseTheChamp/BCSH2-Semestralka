using System;
using System.Threading;

namespace BCSH2_Semestralka.Model.ParserClasses.Context
{
    public class MyExecutionContext : ICloneable
    {
        public ProgramContext ProgramContext { get; set; }
        public Variables Variables { get; set; }
        public MyExecutionContext? UpperExecutionContext { get; set; }

        public MyExecutionContext()
        {
            ProgramContext = new ProgramContext(this);
            Variables = new Variables(this);
            UpperExecutionContext = null;
        }

        public object Clone()
        {
            MyExecutionContext oldExecutionContext = this;
            MyExecutionContext newExecutionContext = new MyExecutionContext();
            newExecutionContext.UpperExecutionContext = oldExecutionContext;
            newExecutionContext.ProgramContext = new ProgramContext(newExecutionContext);
            newExecutionContext.ProgramContext.PrintCallBack = oldExecutionContext.ProgramContext.PrintCallBack;
            newExecutionContext.ProgramContext.ReadCallBack = oldExecutionContext.ProgramContext.ReadCallBack;
            newExecutionContext.Variables = new Variables(newExecutionContext);
            return newExecutionContext;
        }
    }
}