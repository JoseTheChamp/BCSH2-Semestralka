using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Semestralka.Model.ParserClasses
{
    public abstract class BinaryCondition : Condition
    {
        protected BinaryCondition(int line, int token, Expression left, Expression right) : base(line, token)
        {
            Left = left;
            Right = right;
        }

        public Expression Left { get; set; }
        public Expression Right { get; set; }

       
    }
}
