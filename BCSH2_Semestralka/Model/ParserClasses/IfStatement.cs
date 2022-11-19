using BCSH2_Semestralka.Model.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Semestralka.Model.ParserClasses
{
    public class IfStatement : Statement
    {
        public IfStatement(int line, int token,Condition condition) : base(line, token)
        {
            Condition = condition;
            Statements = new List<Statement>();
            ElseStatements = new List<Statement>();
        }

        public Condition Condition { get; set; }
        public List<Statement> Statements { get; set; }
        public List<Statement> ElseStatements { get; set; }


        public override object? Execute(MyExecutionContext executionContext)
        {
            //MyExecutionContext innerExecutionContext = (MyExecutionContext)executionContext.Clone();
            if (Convert.ToBoolean(Condition.Evaluate(executionContext)))
            {
                foreach (Statement statement in Statements)
                {
                    object? result = statement.Execute(executionContext);
                    if (result != null) return result;
                }
            }
            else {
                foreach (Statement statement in ElseStatements)
                {
                    object? result = statement.Execute(executionContext);
                    if (result != null) return result;
                }
            }
            return null;
        }
    }
}
