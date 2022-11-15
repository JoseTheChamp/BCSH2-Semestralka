using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Semestralka.Model.ParserClasses
{
    public abstract class LitExpression : Expression
    {
        protected LitExpression(int line, int token, object value) : base(line, token)
        {
            Value = value;
        }

        public object Value { get; set; }
    }
}
