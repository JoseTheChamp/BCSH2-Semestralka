using BCSH2_Semestralka.Model.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Semestralka.Model.ParserClasses
{
    public class FunctionStatement : Statement
    {
        public FunctionStatement(int line, int token, string ident) : base(line, token)
        {
            Ident = ident;
            Parameters = new List<Parameter>();
            Statements = new List<Statement>();
        }

        public string Ident { get; set; }
        public List<Parameter> Parameters { get; set; }
        public DataType? ReturnType { get; set; }
        public List<Statement> Statements { get; set; }


        public override object? Execute(MyExecutionContext executionContext)
        {
            try
            {
                executionContext.ProgramContext.AddFunction(new Function(Ident, ReturnType, Parameters, Statements));
            }
            catch (Exception ex)
            {
                throw new Exception("Line: " + Line + "  Token: " + Token + "  " + ex.Message.ToString());
            }
            return null;
        }
    }
}
