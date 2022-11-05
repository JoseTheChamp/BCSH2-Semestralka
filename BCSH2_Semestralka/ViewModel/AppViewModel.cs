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
    public class AppViewModel : INotifyPropertyChanged
    {
        private AppModel appModel;

        public AppViewModel()
        {
            appModel = new AppModel();
            PromptVisible = "Visible";
            CodeReadOnly = true;
            OpenFile = new MyICommand(OnOpenFile, CanOpenFile);
            SaveFile = new MyICommand(OnSaveFile, CanSaveFile);
            SaveFileAs = new MyICommand(OnSaveFileAs, CanSaveFileAs);
            NewFile = new MyICommand(OnNewFile, CanNewFile);
            ChangeSizeMinus = new MyICommand(OnChangeSizeMinus, CanChangeSizeMinus);
            ChangeSizePlus = new MyICommand(OnChangeSizePlus, CanChangeSizePlus);
            Interpret = new MyICommand(OnInterpret, CanInterpret);
            TextSize = 14;
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
            }
        }
        private void AddLog(string title, string text) {
            OutputText = OutputText + "\n--- LOG: " + DateTime.Now.ToString("HH:mm:ss,mm") + " | " + title + " | " + text;
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



        public MyICommand Interpret { get; set; }
        private void OnInterpret()
        {
            DateTime time = DateTime.Now;
            AddLog("Interpret","Starting the interpreting process.");
            OutputText = OutputText + "\n" + appModel.Interpret(InputText);
            AddLog("Interpret", "Interpreting is completed. Time elapsed: " + Convert.ToInt32((DateTime.Now - time).TotalMilliseconds) + " miliseconds.");
        }
        private bool CanInterpret()
        {
            return true;
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