using BCSH2_Semestralka.Model.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Semestralka.Model.ParserClasses
{
    public class StringExpression : LitExpression
    {
        public StringExpression(int line, int token, object value) : base(line, token, value)
        {
        }

        public override object Evaluate(MyExecutionContext executionContext)
        {
            return Convert.ToString(Value);
        }
    }
}
