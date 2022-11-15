using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Semestralka.Model.ParserClasses
{
    public abstract class UnaryExpression : Expression
    {
        protected UnaryExpression(int line, int token,Expression expression) : base(line, token)
        {
            Expression = expression;
        }

        public Expression Expression { get; set; }
    }
}
