using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Semestralka.Model
{
    public class AppModel : INotifyPropertyChanged
    {
        private string saveFilepath;
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
        public string Run(string code) {
            return "NOT IMPLEMENTED";
        }

        public string Compile(string code)
        {
            return "NOT IMPLEMENTED";
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
