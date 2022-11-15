using BCSH2_Semestralka.Model.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Semestralka.Model.ParserClasses
{
    public class ProgramAST
    {
        public List<Statement> Statements { get; set; }

        public ProgramAST()
        {
            Statements = new List<Statement>();
        }

        public void Run() {
            MyExecutionContext executionContext = new MyExecutionContext();
            foreach (Statement statement in Statements)
            {
                statement.Execute(executionContext);
            }
        }
    }
}
