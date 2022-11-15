using BCSH2_Semestralka.Model.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Semestralka.Model.ParserClasses
{
    public class Plus : BinaryExpression
    {
        public Plus(int line, int token, Expression left, Expression right) : base(line, token, left, right)
        {
        }

        public override object Evaluate(MyExecutionContext executionContext)
        {
            Console.WriteLine("Substract");
            object leftValue = Left.Evaluate(executionContext);
            switch (Type.GetTypeCode(leftValue.GetType()))
            {
                case TypeCode.Int32:
                    if (Right.Evaluate(executionContext).GetType() == Left.Evaluate(executionContext).GetType())
                    {
                        return (int)(Convert.ToInt32(leftValue) + Convert.ToInt32(Right.Evaluate(executionContext)));
                    }
                    else
                    {
                        throw new Exception("Line: " + Line + "  Token: " + Token + "  Addition: both operands must be of the same datatype.[Interpreting]");
                    }
                case TypeCode.Double:
                    if (Right.Evaluate(executionContext).GetType() == Left.Evaluate(executionContext).GetType())
                    {
                        return Convert.ToDouble(leftValue) + Convert.ToDouble(Right.Evaluate(executionContext));
                    }
                    else
                    {
                        throw new Exception("Line: " + Line + "  Token: " + Token + "  Addition: both operands must be of the same datatype.[Interpreting]");
                    }
                case TypeCode.String:
                    throw new Exception("Line: " + Line + "  Token: " + Token + "  Addition: Adding strings is not supported.[Interpreting]");
                default:
                    break;
            }
            throw new Exception("Line: " + Line + "  Token: " + Token + "  Addition: Unexpected error.[Interpreting]");
        }
    }
}
