using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCSH2_Semestralka.Model.ParserClasses.Context;

namespace BCSH2_Semestralka.Model.ParserClasses.Context
{
    public class Function
    {
        public string Ident { get; set; }
        public DataType? ReturnType { get; set; }
        public List<Parameter> Parameters { get; set; }
        public List<Statement> Statements { get; set; }

        public Function(string ident, DataType? returnType, List<Parameter> parameters, List<Statement> statements)
        {
            Ident = ident;
            ReturnType = returnType;
            Parameters = parameters;
            Statements = statements;
        }

        public object? Execute(MyExecutionContext executionContext,List<Expression> paramss) {
            if (Parameters.Count != paramss.Count)
            {
                throw new Exception("Function: number of parameters does not correspond. [Interpreting]");
            }
            MyExecutionContext innerExecutionContext = (MyExecutionContext)executionContext.Clone();
            for (int i = 0; i < Parameters.Count; i++)
            {
                object? value = paramss[i].Evaluate(executionContext);
                switch (Type.GetTypeCode(value.GetType()))
                {
                    case TypeCode.Int32:
                        if (Parameters[i].DataType == DataType.Int)
                        {
                            innerExecutionContext.Variables.AddVariable(new Variable(Parameters[i].Ident,false,DataType.Int,Convert.ToInt32(value)),true);
                        }
                        else {
                            throw new Exception("Function: Parameter datatype does not correspond. Param:" + i + "[Interpreting]");
                        }
                        break;
                    case TypeCode.Double:
                        if (Parameters[i].DataType == DataType.Double)
                        {
                            innerExecutionContext.Variables.AddVariable(new Variable(Parameters[i].Ident, false, DataType.Double, Convert.ToDouble(value)),true);
                        }
                        else
                        {
                            throw new Exception("Function: Parameter datatype does not correspond. Param:" + i + "[Interpreting]");
                        }
                        break;
                    case TypeCode.String:
                        if (Parameters[i].DataType == DataType.String)
                        {
                            innerExecutionContext.Variables.AddVariable(new Variable(Parameters[i].Ident, false, DataType.String, Convert.ToString(value)),true);
                        }
                        else
                        {
                            throw new Exception("Function: Parameter datatype does not correspond. Param:" + i + "[Interpreting]");
                        }
                        break;
                    default:
                        throw new Exception("Function: Unexpected error.[Interpreting]");
                }
            }


            foreach (Statement statement in Statements)
            {
                object? result = statement.Execute(innerExecutionContext);
                if (result != null)
                {
                    switch (Type.GetTypeCode(result.GetType()))
                    {
                        case TypeCode.Int32:
                            if (ReturnType == DataType.Int)
                            {
                                return Convert.ToInt32(result);
                            }
                            else {
                                throw new Exception("Function: " + Ident + "- this function was supposed to return int.[Interpreting]");
                            }
                        case TypeCode.Double:
                            if (ReturnType == DataType.Double)
                            {
                                return Convert.ToDouble(result);
                            }
                            else
                            {
                                throw new Exception("Function: " + Ident + "- this function was supposed to return double.[Interpreting]");
                            }
                        case TypeCode.String:
                            if (ReturnType == DataType.String)
                            {
                                return Convert.ToString(result);
                            }
                            else
                            {
                                throw new Exception("Function: " + Ident + "- this function was supposed to return string.[Interpreting]");
                            }
                        default:
                            throw new Exception("Function: unexpected error.[Interpreting]");
                    }
                }
            }
            if (ReturnType != null)
            {
                throw new Exception("Function: Expected return statement in function with return type.[Interpreting]");
            }
            return null;
        }
    }
}
