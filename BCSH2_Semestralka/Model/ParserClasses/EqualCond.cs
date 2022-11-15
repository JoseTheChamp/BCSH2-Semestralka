using BCSH2_Semestralka.Model.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Semestralka.Model.ParserClasses
{
    public class EqualCond : BinaryCondition
    {
        public EqualCond(int line, int token, Expression left, Expression right) : base(line, token, left, right)
        {
        }

        public override object Evaluate(MyExecutionContext executionContext)
        {
            object leftValue = Left.Evaluate(executionContext);
            switch (Type.GetTypeCode(leftValue.GetType()))
            {
                case TypeCode.Int32:
                    if (Right.Evaluate(executionContext).GetType() == Left.Evaluate(executionContext).GetType())
                    {
                        if(Convert.ToInt32(leftValue) == Convert.ToInt32(Right.Evaluate(executionContext))) return true;
                        return false;
                    }
                    else
                    {
                        throw new Exception("Line: " + Line + "  Token: " + Token + "  EqualCond: both operands must be of the same datatype.[Interpreting]");
                    }
                case TypeCode.Double:
                    if (Right.Evaluate(executionContext).GetType() == Left.Evaluate(executionContext).GetType())
                    {
                        if (Convert.ToDouble(leftValue) == Convert.ToDouble(Right.Evaluate(executionContext))) return true;
                        return false;
                    }
                    else
                    {
                        throw new Exception("Line: " + Line + "  Token: " + Token + "  EqualCond: both operands must be of the same datatype.[Interpreting]");
                    }
                case TypeCode.String:
                    if (Right.Evaluate(executionContext).GetType() == Left.Evaluate(executionContext).GetType())
                    {
                        if (Convert.ToString(leftValue) == Convert.ToString(Right.Evaluate(executionContext))) return true;
                        return false;
                    }
                    else
                    {
                        throw new Exception("Line: " + Line + "  Token: " + Token + "  EqualCond: both operands must be of the same datatype.[Interpreting]");
                    }
                default:
                    break;
            }
            throw new Exception("Line: " + Line + "  Token: " + Token + "  EqualCond: Unexpected error.[Interpreting]");
        }
    }
}
