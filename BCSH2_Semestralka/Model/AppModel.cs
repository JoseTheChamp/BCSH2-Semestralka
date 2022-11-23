using BCSH2_Semestralka.Model.ParserClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BCSH2_Semestralka.Model
{
    public class AppModel : INotifyPropertyChanged
    {
        private string saveFilepath;

        private List<Token> tokens;
        private ProgramAST program;
        Lexer lexer;
        Parser parser;
        public PrintCallBack PrintCallBack { get; set; }
        public ReadCallBack ReadCallBack { get; set; }

        public string SaveFilePath
        {
            get { return saveFilepath; }
            set
            {
                saveFilepath = value;
                RaisePropertyChanged("SaveFilePath");
            }
        }

        public AppModel()
        {
            SaveFilePath = "none";
            tokens = new List<Token>();
            lexer = new Lexer();
            parser = new Parser();
        }


        public string LoadFile(string filePath) { 
            SaveFilePath = filePath;
            return Persistence.ReadFromFile(filePath);
        }

        public void SaveFile(string text)
        {
            Persistence.WriteToFile(saveFilepath, text);
        }
        public void SaveFileAs(string filePath, string text)
        {
            SaveFilePath = filePath;
            Persistence.WriteToFile(filePath, text);
        }
        public void Run() {
            program.Run();
        }

        public void Parse() 
        {
            program = parser.Parse(tokens);
            program.PrintCallBack = PrintCallBack;
            program.ReadCallBack = ReadCallBack;
        }

        public void Lexicate(string s) {
            tokens = lexer.Lexicate(s);
            foreach (Token token in tokens)
            {
                if (token.Value != null)
                {
                    Debug.WriteLine("Token: L: " + token.Line + "  t: " + token.LineToken + "   " + token.Type + " " + token.Value);
                }
                else {
                    Debug.WriteLine("Token: L: " + token.Line + "  t: " + token.LineToken + "   " + token.Type + " ");
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
