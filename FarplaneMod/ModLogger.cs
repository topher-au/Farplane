using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Farplane.FarplaneMod
{
    public static class ModLogger
    {
        internal const string ModLogFile = "mods\\logs\\FarplaneMods-{0}.log";
        internal static readonly string LatestLogFile = string.Format(ModLogFile, "latest");
        private static string _currentLogFile = string.Empty;

        internal static void StartNewLog()
        {
            if (!Directory.Exists("mods"))
                Directory.CreateDirectory("mods");

            if (!Directory.Exists("mods\\logs"))
                Directory.CreateDirectory("mods\\logs");

            // Check for old log files
            var logFiles = Directory.GetFiles("mods\\logs", "*.log");

            // remove all but the latest 5
            for(int i=0; i<logFiles.Length-5; i++)
                File.Delete(logFiles[i]);

            // Remove existing latest.log
            if(File.Exists(LatestLogFile))
                File.Delete(LatestLogFile);

            // Generate filename for new log file
            var time = DateTime.Now;
            var timeStamp = string.Format("{0}-{1}-{2}.{3}.{4}.{5}", time.Year.ToString("D4"), time.Month.ToString("D2"), time.Day.ToString("D2"), time.Hour.ToString("D2"), time.Minute.ToString("D2"), time.Second.ToString("D2"));
            _currentLogFile = string.Format(ModLogFile, timeStamp);
        }

        internal static void CloseLog()
        {
            _currentLogFile = string.Empty;
        }

        public static void WriteLine(string format, params string[] args)
        {
            if(_currentLogFile == string.Empty) StartNewLog();

            var callingAsm = Assembly.GetCallingAssembly().GetName().Name;

            var formatLine = string.Format("[" + callingAsm + "] " + format + Environment.NewLine, args);
            

            File.AppendAllText(_currentLogFile, formatLine);
            File.AppendAllText(LatestLogFile, formatLine);
            Console.Write(formatLine);
        }
        public static void NewLine()
        {
            if (_currentLogFile == string.Empty) StartNewLog();

            var formatLine = Environment.NewLine;

            File.AppendAllText(_currentLogFile, formatLine);
            File.AppendAllText(LatestLogFile, formatLine);
            Console.Write(formatLine);
        }

        public static void Write(string format, params string[] args)
        {
            if (_currentLogFile == string.Empty) StartNewLog();

            var formatLine = string.Format(format, args);

            File.AppendAllText(_currentLogFile, formatLine);
            File.AppendAllText(LatestLogFile, formatLine);
            Console.Write(formatLine);
        }
    }
}
