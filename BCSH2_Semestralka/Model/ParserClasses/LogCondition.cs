using BCSH2_Semestralka.Model.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Semestralka.Model.ParserClasses
{
    public class LogCondition : Condition
    {
        public LogCondition(int line, int token) : base(line, token)
        {
            Conditions = new List<Condition>();
        }

        public List<Condition> Conditions { get; set; }

        public override object Evaluate(MyExecutionContext executionContext)
        {
            bool result = Convert.ToBoolean(Conditions[0].Evaluate(executionContext));
            for (int i = 1; i < Conditions.Count; i++)
            {
                if (Conditions[i].GetType() == typeof(OrCondition))
                {
                    result = result || Convert.ToBoolean(Conditions[i].Evaluate(executionContext));
                }
                else {
                    result = result && Convert.ToBoolean(Conditions[i].Evaluate(executionContext));
                }
            }
            return result;
        }
    }
}
