using BCSH2_Semestralka.Model.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Semestralka.Model.ParserClasses
{
    public class DoubleExpression : LitExpression
    {
        public DoubleExpression(int line, int token, object value) : base(line, token, value)
        {
        }

        public override object Evaluate(MyExecutionContext executionContext)
        {
            string sd = Value.ToString();
            sd = sd.Replace('.', ',');
            return Convert.ToDouble(sd);
        }
    }
}
