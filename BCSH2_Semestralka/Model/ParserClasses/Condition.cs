using BCSH2_Semestralka.Model.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Semestralka.Model.ParserClasses
{
    public abstract class Condition : Position,IEvaluable
    {
        protected Condition(int line, int token) : base(line, token)
        {
        }

        public abstract object Evaluate(MyExecutionContext executionContext);

    }

}
