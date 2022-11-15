﻿using BCSH2_Semestralka.Model.ParserClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
            try
            {
                program = parser.Parse(tokens);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR PARSER");
                throw ex;
            }
        }

        public void Lexicate(string s) {
            tokens = lexer.Lexicate(s);
        }
        public void ClearInterpreter() {
            tokens = null;
            program = null;
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
