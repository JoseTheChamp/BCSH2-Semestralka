using BCSH2_Semestralka.Model.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Semestralka.Model.ParserClasses
{
    public class CallExpression : Expression
    {
        public CallExpression(int line, int token,string ident) : base(line, token)
        {
            Ident = ident;
            Parameters = new List<Expression>();
        }

        public string Ident { get; set; }
        public List<Expression> Parameters { get; set; }

        

        public override object Evaluate(MyExecutionContext executionContext)
        {
            try
            {
                return executionContext.ProgramContext.Call(Ident, executionContext, Parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Line: " + Line + "  Token: " + Token + "  " + ex.Message.ToString());
            }
        }
    }
}
