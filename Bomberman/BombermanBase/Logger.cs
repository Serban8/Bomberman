using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanMONO
{
    internal static class Logger
    {
        private static string _logFilePath = "C:\\Users\\mihai\\OneDrive\\Materiale cursuri\\Anul3\\IS\\proiect\\Bomberman\\Bomberman\\BombermanBase";
        public static void Log(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(_logFilePath, "log.txt"), true))
                {
                    outputFile.WriteLine(message);
                }
            }
        }

    }
}
