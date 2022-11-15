using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Semestralka.Model.ParserClasses.Context
{
    public class Variable : ICloneable
    {
        public string Ident { get; set; }
        public bool IsVal { get; set; }
        public DataType DataType { get; set; }
        public object Value { get; set; }

        public Variable(string ident, bool isVal, DataType dataType, object value)
        {
            Ident = ident;
            IsVal = isVal;
            DataType = dataType;
            Value = value;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
