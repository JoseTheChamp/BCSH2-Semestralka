using BCSH2_Semestralka.Model.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Semestralka.Model.ParserClasses
{
    public class PlusUnary : UnaryExpression
    {
        public PlusUnary(int line, int token, Expression expression) : base(line, token, expression)
        {
        }

        public override object Evaluate(MyExecutionContext executionContext)
        {
            object value = Expression.Evaluate(executionContext);
            switch (Type.GetTypeCode(value.GetType()))
            {
                case TypeCode.Int32:
                    return +Convert.ToInt32(value);
                case TypeCode.Double:
                    return +Convert.ToDouble(value);
                case TypeCode.String:
                    throw new Exception("Line: " + Line + "  Token: " + Token + "  PlusUnary: +string.[Interpreting]");
            }
            throw new Exception("Line: " + Line + "  Token: " + Token + "  PlusUnary: Unexpected error.[Interpreting]");
        }
    }
}
