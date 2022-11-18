using BCSH2_Semestralka.Model.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Semestralka.Model.ParserClasses
{
    public class ProgramAST
    {
        public List<Statement> Statements { get; set; }
        public PrintCallBack PrintCallBack { get; set; }

        public ProgramAST()
        {
            Statements = new List<Statement>();
        }

        public void Run() {
            Debug.WriteLine("RUN in ProgramAST");
            MyExecutionContext executionContext = new MyExecutionContext();
            executionContext.ProgramContext.PrintCallBack = PrintCallBack;
            foreach (Statement statement in Statements)
            {
                statement.Execute(executionContext);
            }
            foreach (Statement statement in Statements)
            {
                Debug.WriteLine(statement.GetType());
            }
        }
    }
}
