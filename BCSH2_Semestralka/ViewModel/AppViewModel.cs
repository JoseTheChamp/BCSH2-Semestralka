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
using System.Threading;
using System.Windows.Threading;

namespace BCSH2_Semestralka.ViewModel
{
    public class AppViewModel : INotifyPropertyChanged
    {
        private AppModel appModel;
        private IScrollable scrollableOutput;
        private char lastAddedCharacterToOutput = 'p';
        private bool saved;
        private Thread runThread;
        public AppViewModel(IScrollable scrollable)
        {
            appModel = new AppModel();
            appModel.PrintCallBack = this.AddLogCallBack;
            appModel.ReadCallBack = this.ReadCallBack;
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
            Stop = new MyICommand(OnStop,CanStop);
            TextSize = 14;
            OutputReadOnly = true;
            saved = true;
            this.scrollableOutput = scrollable;
            Application.Current.MainWindow.Closing += new CancelEventHandler(MainWindowClosing);


            //open from settings
            if (Preferences.Default.LastFile != "")
            {
                string fileName = Preferences.Default.LastFile;
                try
                {
                    InputText = appModel.LoadFile(fileName);
                    Debug.WriteLine("Open: " + fileName);
                    SaveFilePath = fileName;
                    PromptVisible = "Hidden";
                    CodeReadOnly = false;
                    SaveFile.RaiseCanExecuteChanged();
                    AddLog("OpenFile", "Code succesfully loaded from " + SaveFileName + " | " + SaveFilePath);
                    Compile.RaiseCanExecuteChanged();
                    Run.RaiseCanExecuteChanged();
                    saved = true;
                }
                catch (Exception ex)
                {
                    appModel.SaveFilePath= "none";
                }
            }
        }

        void MainWindowClosing(object sender, CancelEventArgs e) {
            Debug.WriteLine("ZAVIRANI - ViewModel");
            if (!saved)
            {
                string msg = "You have unsaved changes. Do you want to close without saving?";
                MessageBoxResult result =
                  MessageBox.Show(
                    msg,
                    "MiniKotlin - Exit",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }

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
                    saved = false;
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

        private bool isRunning;
        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
                isRunning = value;
                Stop.RaiseCanExecuteChanged();
                Run.RaiseCanExecuteChanged();
                Compile.RaiseCanExecuteChanged();
                RaisePropertyChanged("IsRunning");
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

        private bool outputReadOnly;
        public bool OutputReadOnly
        {
            get { return outputReadOnly; }
            set
            {
                outputReadOnly = value;
                RaisePropertyChanged("OutputReadOnly");
            }
        }


        private string outputText;
        public string OutputText
        {
            get { return outputText; }
            set
            {
                if (value != "")
                {
                    lastAddedCharacterToOutput = value[value.Length - 1];
                }
                outputText = value;
                RaisePropertyChanged("OutputText");
                scrollableOutput.ScrollToEnd(value.Length);
            }
        }
        private void AddLog(string title, string text) {
            OutputText = OutputText + "\n--- LOG: " + DateTime.Now.ToString("HH:mm:ss.ff") + " | " + title + " | " + text;
        }
        public void AddLogCallBack(string text) {
            Application.Current.Dispatcher.Invoke(() => OutputText = OutputText + "\n" + text);
            //OutputText = OutputText + "\n" + text;
        }

        public string ReadCallBack(string? text) {
            Debug.WriteLine("Zacatek");
            if (text != null)
            {
                AddLogCallBack(text);
            }
            OutputReadOnly = false;
            int delka = OutputText.Length;
            while (lastAddedCharacterToOutput != '\n')
            {

            }

            Application.Current.Dispatcher.Invoke(() => OutputText = OutputText.TrimEnd('\n'));
            //Application.Current.Dispatcher.Invoke(() => scrollableOutput.ScrollToEnd(OutputText.Length));
            
            int delkaKonec = OutputText.Length;
            OutputReadOnly = true;
            Debug.WriteLine("konec");
            return OutputText.Substring(delka, delkaKonec - (delka + 1));
        }

        private string inputText;
        public string InputText
        {
            get { return inputText; }
            set
            {
                saved = false;
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
            saved = true;
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
                Preferences.Default.LastFile = saveFileDialog.FileName;
                Preferences.Default.Save();
                saved = true;
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
            saved = true;
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

                Preferences.Default.LastFile = openFileDialog.FileName;
                Preferences.Default.Save();
                saved = true;
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
            return TextSize >= 2;

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
            runThread = new Thread(() => {
                try
                {
                    if (DoCompile("Run"))
                    {
                        DateTime time = DateTime.Now;
                        Application.Current.Dispatcher.Invoke(() => AddLog("Run", "Starting the run process."));
                        try
                        {
                            appModel.Run();
                        }
                        catch (Exception ex)
                        {
                            Application.Current.Dispatcher.Invoke(() => {
                                AddLog("Run", "Run failed. Error msg: " + ex.Message.ToString());
                                IsRunning = false;
                                OutputReadOnly = true;
                                });
                            return;
                        }
                        //AddLog("Run", "Run is completed. Time elapsed: " + Convert.ToInt32((DateTime.Now - time).TotalMilliseconds) + " miliseconds.");
                        Application.Current.Dispatcher.Invoke(() => AddLog("Run", "Run is completed. Time elapsed: " + Convert.ToInt32((DateTime.Now - time).TotalMilliseconds) + " miliseconds."));
                    }
                    else
                    {
                        //AddLog("Run", "Run aborted.");
                        Application.Current.Dispatcher.Invoke(() => AddLog("Run", "Run aborted."));
                    }
                    Application.Current.Dispatcher.Invoke(() => IsRunning = false);
                }
                catch (ThreadInterruptedException ex)
                {

                }
            });
            IsRunning = true;
            runThread.Start();
        }
        private bool CanRun()
        {
            return inputText != null && isRunning == false;
        }

        private bool DoCompile(string from) {
            DateTime time = DateTime.Now;
            //AddLog(from, "Starting the compiling process.");
            Application.Current.Dispatcher.Invoke(() => AddLog(from, "Starting the compiling process."));
            try
            {
                appModel.Lexicate(InputText);
            }
            catch (Exception ex)
            {
                //AddLog(from, "Lexing has failed. Error msg: " + ex.Message.ToString());
                Application.Current.Dispatcher.Invoke(() => AddLog(from, "Lexing has failed. Error msg: " + ex.Message.ToString()));
                return false;
            }
            try
            {
                appModel.Parse();
            }
            catch (Exception ex)
            {
                //AddLog(from, "Parsing has failed. Error msg: " + ex.Message.ToString());
                Application.Current.Dispatcher.Invoke(() => AddLog(from, "Parsing has failed. Error msg: " + ex.Message.ToString()));
                return false;
            }
            //AddLog(from, "Compiling success. Time elapsed: " + Convert.ToInt32((DateTime.Now - time).TotalMilliseconds) + " miliseconds.");
            Application.Current.Dispatcher.Invoke(() => AddLog(from, "Compiling success. Time elapsed: " + Convert.ToInt32((DateTime.Now - time).TotalMilliseconds) + " miliseconds."));
            return true;
        }


        public MyICommand Compile { get; set; }
        private void OnCompile()
        {
            new Thread(() => {
                DoCompile("Compile");
            }).Start();
        }
        private bool CanCompile()
        {
            return inputText != null && isRunning == false;
        }

        public MyICommand Stop { get; set; }
        private void OnStop()
        {
            runThread.Interrupt();
            AddLog("Run", "Run aborted.");
            IsRunning = false;
            OutputReadOnly = true;
        }
        private bool CanStop()
        {
            return IsRunning;
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