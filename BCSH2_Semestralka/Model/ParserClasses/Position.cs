using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Semestralka.Model.ParserClasses
{
    public class Position
    {
        public int Line { get; set; }
        public int Token { get; set; }

        public Position(int line, int token)
        {
            Line = line;
            Token = token;
        }
    }
}
