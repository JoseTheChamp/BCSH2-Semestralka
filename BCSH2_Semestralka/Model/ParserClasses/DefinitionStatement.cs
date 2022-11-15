using BCSH2_Semestralka.Model.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Semestralka.Model.ParserClasses
{
    public class DefinitionStatement : Statement
    {
        public DefinitionStatement(int line, int token) : base(line, token)
        {
        }

        public bool IsVal { get; set; }
        public string Ident { get; set; }
        public DataType DataType { get; set; }
        public object Value { get; set; }

        public override object? Execute(MyExecutionContext executionContext)
        {
            try
            {
                executionContext.Variables.AddVariable(new Variable(Ident, IsVal, DataType, Value));
            }
            catch (Exception ex)
            {
                throw new Exception("Line: " + Line + "  Token: " + Token + "  " + ex.Message.ToString());
            }
            return null;
        }
    }
}
