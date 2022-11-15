using BCSH2_Semestralka.Model.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Semestralka.Model.ParserClasses
{
    public class CallStatement : Statement
    {
        public string Ident { get; set; }
        public List<Expression> Params { get; set; }

        public CallStatement(int line, int token, string ident) : base(line, token)
        {
            Ident = ident;
            Params = new List<Expression>();
        }

        public override object? Execute(MyExecutionContext executionContext)
        {
            try
            {
                executionContext.ProgramContext.Call(Ident, executionContext, Params);
            }
            catch (Exception ex)
            {
                throw new Exception("Line: " + Line + "  Token: " + Token + "  " + ex.Message.ToString());
            }
            return null;
        }
    }
}
