using BCSH2_Semestralka.Model.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Semestralka.Model.ParserClasses
{
    public class AndCondition : Condition
    {
        public AndCondition(int line, int token,Condition condition) : base(line, token)
        {
            Condition = condition;
        }

        public Condition Condition { get; set; }

        

        public override object Evaluate(MyExecutionContext executionContext)
        {
            return Convert.ToBoolean(Condition.Evaluate(executionContext));
        }
    }
}
