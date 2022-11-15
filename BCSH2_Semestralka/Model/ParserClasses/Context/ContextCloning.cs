using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Semestralka.Model.ParserClasses.Context
{
    public static class ContextCloning
    {
        public static MyExecutionContext Clone(MyExecutionContext oldExecutionContext) {
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
