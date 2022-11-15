using BCSH2_Semestralka.Model.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Semestralka.Model.ParserClasses
{
    public class AssignStatement : Statement
    {
        public AssignStatement(int line, int token, string ident, Expression expression) : base(line, token)
        {
            Ident = ident;
            Expression = expression;
        }

        public string Ident { get; set; }
        public Expression Expression { get; set; }
        

        public override object? Execute(MyExecutionContext executionContext)
        {
            try
            {
                executionContext.Variables.Set(Ident, Expression.Evaluate(executionContext));
            }
            catch (Exception ex)
            {
                throw new Exception("Line: " + Line + "  Token: " + Token + "  " + ex.Message.ToString());
            }
            return null;
        }
    }
}
