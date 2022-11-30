﻿using BCSH2_Semestralka.Model.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Semestralka.Model.ParserClasses
{
    public class Multiply : BinaryExpression
    {
        public Multiply(int line, int token, Expression left, Expression right) : base(line, token, left, right)
        {
        }

        public override object Evaluate(MyExecutionContext executionContext)
        {
            object leftValue = Left.Evaluate(executionContext);
            object rightValue = Right.Evaluate(executionContext);
            switch (Type.GetTypeCode(leftValue.GetType()))
            {
                case TypeCode.Int32:
                    if (rightValue.GetType() == leftValue.GetType())
                    {
                        return (int)(Convert.ToInt32(leftValue) * Convert.ToInt32(rightValue));
                    }
                    else
                    {
                        throw new Exception("Line: " + Line + "  Token: " + Token + "  Multiplying: both operands must be of the same datatype.[Interpreting]");
                    }
                case TypeCode.Double:
                    if (rightValue.GetType() == leftValue.GetType())
                    {
                        return Convert.ToDouble(leftValue) * Convert.ToDouble(rightValue);
                    }
                    else
                    {
                        throw new Exception("Line: " + Line + "  Token: " + Token + "  Multiplying: both operands must be of the same datatype.[Interpreting]");
                    }
                case TypeCode.String:
                    throw new Exception("Line: " + Line + "  Token: " + Token + "  Multiplying: Dividing strings is not supported.[Interpreting]");
                default:
                    break;
            }
            throw new Exception("Line: " + Line + "  Token: " + Token + "  Multiplying: Unexpected error.[Interpreting]");
        }
    }
}