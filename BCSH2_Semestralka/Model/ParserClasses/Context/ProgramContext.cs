using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using BCSH2_Semestralka.Model.ParserClasses.Context;

namespace BCSH2_Semestralka.Model.ParserClasses.Context
{
    public class ProgramContext
    {
        public List<Function> Functions { get; set; }
        public PrintCallBack PrintCallBack { get; set; }
        public ReadCallBack ReadCallBack { get; set; }
        public MyExecutionContext ExecutionContext { get; set; }

        public ProgramContext(MyExecutionContext executionContext)
        {
            Functions = new List<Function>();
            ExecutionContext = executionContext;
        }

        public ProgramContext(PrintCallBack printCallBack)
        {
            Functions = new List<Function>();

        }

        public ProgramContext(List<Function> functions)
        {
            Functions = functions;
        }
        public void AddFunction(Function function) {
            foreach (var fun in Functions)
            {
                if (fun.Ident == function.Ident)
                {
                    throw new Exception("This function is already defined. [" + function.Ident + "].");
                }
            }
            MyExecutionContext? exec = ExecutionContext.UpperExecutionContext;
            while (exec != null)
            {
                foreach (var item in exec.ProgramContext.Functions)
                {
                    if (item.Ident == function.Ident)
                    {
                        throw new Exception("This function is already defined. [" + function.Ident + "].");
                    }
                }
                exec = exec.UpperExecutionContext;
            }
            Functions.Add(function);
        }
        public object? Call(string ident, MyExecutionContext executionContext, List<Expression> paramss) {
            foreach (Function fun in Functions)
            {
                if (fun.Ident == ident)
                {
                    return fun.Execute(executionContext,paramss);  
                }
            }
            MyExecutionContext? exec = executionContext.UpperExecutionContext;
            while (exec != null)
            {
                foreach (Function fun in exec.ProgramContext.Functions)
                {
                    if (fun.Ident == ident)
                    {
                        return fun.Execute(executionContext, paramss);
                    }
                }
                exec = exec.UpperExecutionContext;
            }
            if (ident == "print")
            {
                Debug.WriteLine("AAAAAAAAAAAAAAAAAAAAaa)");
                Debug.WriteLine("Print "  + Convert.ToString(paramss[0]?.Evaluate(executionContext)));
                print(Convert.ToString(paramss[0]?.Evaluate(executionContext)));
                return null;
            }
            if (ident == "read")
            {
                if (paramss.Count < 2)
                {
                     return read(Convert.ToString(paramss[0]?.Evaluate(executionContext)));
                }
                else {
                    throw new Exception("Incorrect number of parameters. Should be 1.");
                }
            }
            throw new Exception("This method was not defined. [" + ident + "]");
        }

        private void print(string str) {
            if (str != null && str != "")
            {
                PrintCallBack.Invoke(str);
            }
            else {
                throw new Exception("Print method requires one argument.");
            }
        }
        private object read(string? str) { 
            string s = ReadCallBack.Invoke(str);
            double numd = 0;
            int num = 0;
            try
            {
                double d = Double.Parse(s, CultureInfo.InvariantCulture);
                if (s.Contains('.'))
                {
                    return d;
                }
                else
                {
                    return Convert.ToInt32(d);
                }
            }
            catch (Exception ex)
            {
                if (Int32.TryParse(s, out num))
                {
                    return num;
                }
                else if (s != "\n")
                {
                    return s;
                }
            }
            return null;
        }
    }
}