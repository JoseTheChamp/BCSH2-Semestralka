using BCSH2_Semestralka.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace BCSH2_Semestralka.ViewModel
{
    public class AppViewModel : INotifyPropertyChanged,IClosing
    {
        private AppModel appModel;
        private IScrollable scrollableOutput;

        public AppViewModel(IScrollable scrollable)
        {
            appModel = new AppModel();
            appModel.PrintCallBack = this.AddLogCallBack;
            PromptVisible = "Visible";
            CodeReadOnly = true;
            OpenFile = new MyICommand(OnOpenFile, CanOpenFile);
            SaveFile = new MyICommand(OnSaveFile, CanSaveFile);
            SaveFileAs = new MyICommand(OnSaveFileAs, CanSaveFileAs);
            NewFile = new MyICommand(OnNewFile, CanNewFile);
            ChangeSizeMinus = new MyICommand(OnChangeSizeMinus, CanChangeSizeMinus);
            ChangeSizePlus = new MyICommand(OnChangeSizePlus, CanChangeSizePlus);
            Run = new MyICommand(OnRun, CanRun);
            Compile = new MyICommand(OnCompile, CanCompile);
            TextSize = 14;
            this.scrollableOutput = scrollable;
        }

        public string SaveFilePath
        {
            get { return appModel.SaveFilePath; }
            set { 
                appModel.SaveFilePath = value;
                RaisePropertyChanged("SaveFilePath");
                RaisePropertyChanged("SaveFileName");
            }
        }
        public string SaveFileName
        {
            get {
                string str = appModel.SaveFilePath;
                string[] strings = str.Split('\\');
                return strings[strings.Length-1]; 
            }
        }

        private int textSize;
        public int TextSize
        {
            get { return textSize; }
            set
            {
                if (value > 0)
                {
                    textSize = value;
                    RaisePropertyChanged("TextSize");
                    ChangeSizeMinus.RaiseCanExecuteChanged();
                }
            }
        }


        private string promptVisible;
        public string PromptVisible
        {
            get { return promptVisible; }
            set
            {
                promptVisible = value;
                RaisePropertyChanged("PromptVisible");
            }
        }


        private bool codeReadOnly;
        public bool CodeReadOnly
        {
            get { return codeReadOnly; }
            set
            {
                codeReadOnly = value;
                RaisePropertyChanged("CodeReadOnly");
            }
        }


        private string outputText;
        public string OutputText
        {
            get { return outputText; }
            set
            {
                outputText = value;
                RaisePropertyChanged("OutputText");
                scrollableOutput.ScrollToEnd();
            }
        }
        private void AddLog(string title, string text) {
            OutputText = OutputText + "\n--- LOG: " + DateTime.Now.ToString("HH:mm:ss.ff") + " | " + title + " | " + text;
        }
        public void AddLogCallBack(string text) { 
            OutputText = OutputText + "\n" + text;
        }


        private string inputText;
        public string InputText
        {
            get { return inputText; }
            set
            {
                inputText = value;
                RaisePropertyChanged("InputText");
            }
        }



        public MyICommand SaveFile { get; set; }
        private void OnSaveFile()
        {
            Debug.WriteLine("SAVING");
            appModel.SaveFile(InputText);
            AddLog("SaveFile","Code succesfully saved into " + SaveFileName + " | " + SaveFilePath);
        }

        private bool CanSaveFile()
        {
            Debug.WriteLine("CAN SAVE");
            if (SaveFilePath != "none")
            {
                return true;
            }
            return false;
        }


        public MyICommand SaveFileAs { get; set; }
        private void OnSaveFileAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt";
            Nullable<bool> result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                Debug.WriteLine("SAVING AS");
                SaveFilePath = saveFileDialog.FileName;
                appModel.SaveFileAs(saveFileDialog.FileName,InputText);
                SaveFile.RaiseCanExecuteChanged();
                AddLog("SaveFileAs", "Code succesfully saved into " + SaveFileName + " | " + SaveFilePath);
                PromptVisible = "Hidden";
                CodeReadOnly = false;
                if (inputText == null)
                {
                    inputText = "";
                    Compile.RaiseCanExecuteChanged();
                    Run.RaiseCanExecuteChanged();
                }      
            }
            else {
                AddLog("SaveFileAs", "Operation aborted.");
            }
        }

        private bool CanSaveFileAs()
        {
            return true;
        }


        public MyICommand NewFile { get; set; }
        private void OnNewFile()
        {
            SaveFilePath = "none";
            InputText = "";
            OutputText = "";
            SaveFile.RaiseCanExecuteChanged();
            Compile.RaiseCanExecuteChanged();
            Run.RaiseCanExecuteChanged();
            PromptVisible = "Hidden";
            CodeReadOnly = false;
        }

        private bool CanNewFile()
        {
            return true;
        }


        public MyICommand OpenFile { get; set; }
        private void OnOpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            Nullable<bool> result = openFileDialog.ShowDialog();
            if (result == true)
            {
                Debug.WriteLine("Open: " + openFileDialog.FileName);
                SaveFilePath = openFileDialog.FileName;
                InputText = appModel.LoadFile(openFileDialog.FileName);
                PromptVisible = "Hidden";
                CodeReadOnly = false;
                SaveFile.RaiseCanExecuteChanged();
                AddLog("OpenFile", "Code succesfully loaded from " + SaveFileName + " | " + SaveFilePath);
                Compile.RaiseCanExecuteChanged();
                Run.RaiseCanExecuteChanged();
            }
            else {
                AddLog("OpenFile", "Operation aborted.");
            }
        }
        private bool CanOpenFile()
        {
            return true;
        }


        public MyICommand ChangeSizeMinus { get; set; }
        private void OnChangeSizeMinus()
        {
            TextSize = TextSize - 1;
        }
        private bool CanChangeSizeMinus()
        {
            if (TextSize < 2)
            {
                return false;
            }
            return true;
        }

        public MyICommand ChangeSizePlus { get; set; }
        private void OnChangeSizePlus()
        {
            TextSize = TextSize + 1;
        }
        private bool CanChangeSizePlus()
        {
            return true;
        }

        public MyICommand Run { get; set; }
        private void OnRun()
        {
            DateTime time = DateTime.Now;
            if (DoCompile("Run"))
            {
                AddLog("Run", "Starting the run process.");
                try
                {
                    appModel.Run();
                }
                catch (Exception ex)
                {
                    AddLog("Run", "Run failed. Error msg: " + ex.Message.ToString());
                    return;
                }
                AddLog("Run", "Run is completed. Time elapsed: " + Convert.ToInt32((DateTime.Now - time).TotalMilliseconds) + " miliseconds.");
            }
            else {
                AddLog("Run", "Run aborted.");
            }
        }
        private bool CanRun()
        {
            return inputText != null;
        }

        private bool DoCompile(string from) {
            DateTime time = DateTime.Now;
            AddLog(from, "Starting the compiling process.");
            try
            {
                appModel.Lexicate(InputText);
            }
            catch (Exception ex)
            {
                AddLog(from, "Lexing has failed. Error msg: " + ex.Message.ToString());
                return false;
            }
            try
            {
                appModel.Parse();
            }
            catch (Exception ex)
            {
                AddLog(from, "Parsing has failed. Error msg: " + ex.Message.ToString());
                return false;
            }
            AddLog(from, "Compiling success. Time elapsed: " + Convert.ToInt32((DateTime.Now - time).TotalMilliseconds) + " miliseconds.");
            return true;
        }


        public MyICommand Compile { get; set; }
        private void OnCompile()
        {
            DoCompile("Compile");
        }
        private bool CanCompile()
        {
            return inputText != null;
        }



        public event PropertyChangedEventHandler? PropertyChanged;
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public bool OnClosing()
        {
            bool close = true;

            Debug.WriteLine("CLOSING");
            //Ask whether to save changes och cancel etc
            //close = false; //If you want to cancel close
            return close;
        }
    }
}