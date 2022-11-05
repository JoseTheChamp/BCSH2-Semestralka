using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Semestralka.Model
{
    public static class Persistence
    {
        public static string ReadFromFile(string filePath) {
            return File.ReadAllText(filePath);
        }

        public static void WriteToFile(string filePath,string text) { 
            File.WriteAllText(filePath, text);
        }
    }
}
