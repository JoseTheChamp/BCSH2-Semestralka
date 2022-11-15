using BCSH2_Semestralka.Model.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Semestralka.Model.ParserClasses
{
    public class IdentExpression : Expression
    {
        public IdentExpression(int line, int token, string ident) : base(line, token)
        {
            Ident = ident;
        }

        public string Ident { get; set; }

        public override object Evaluate(MyExecutionContext executionContext)
        {
            return executionContext.Variables.Get(Ident);
        }
    }
}
