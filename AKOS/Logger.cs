using System;
using System.IO;

namespace Andy.AKOS
{
    public class Logger
    {
        private FileStream logFileStream;
        public bool logToFile;

        public Logger()
        {
            logFileStream = new FileStream(Directory.GetCurrentDirectory() + $"\\.log", FileMode.Create, FileAccess.Write);
            WriteToFile($"AKOS: [{AKOS.version}+{AKOS.state}]", false);
            logToFile = true;
            Log("Logger Initialised");
        }

        public enum LogLevel { Info, Important, Warning, Error }

        public void Log(string log, LogLevel level = LogLevel.Info)
        {
            ConsoleColor prevBG = Console.BackgroundColor;
            ConsoleColor prevFG = Console.ForegroundColor;

            string logLevelName;
            switch (level)
            {
                default:
                case LogLevel.Info:
                    logLevelName = "INFO";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    break;
                case LogLevel.Important:
                    logLevelName = "IMPO";
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    break;
                case LogLevel.Warning:
                    logLevelName = "WARN";
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    break;
                case LogLevel.Error:
                    logLevelName = "EROR";
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;
            }

            string t = $"\n[{GetTime()}] [{logLevelName}] {log}";
            Console.Write(t);
            if (logToFile)
                WriteToFile(t);

            Console.ForegroundColor = prevFG;
            Console.BackgroundColor = prevBG;

            Console.WriteLine();
        }

        public void LogError(string log, string[] detail)
        {
            ConsoleColor prevBG = Console.BackgroundColor;
            ConsoleColor prevFG = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Red;

            string t = $"\n[{GetTime()}] [EROR] {log}";
            Console.WriteLine(t);
            for (int i = 0; i < detail.Length; i++)
            {
                string tl = $"[{GetTime()}] [EROR] > {detail[i]}";
                Console.WriteLine(tl);
                t += tl;
            }
            if (logToFile)
                WriteToFile(t);

            Console.ForegroundColor = prevFG;
            Console.BackgroundColor = prevBG;

            Console.WriteLine();
        }

        private string GetTime() => $"{DateTime.Now.Hour:00}:{DateTime.Now.Minute:00}:{DateTime.Now.Second:00}.{DateTime.Now.Millisecond:000}";

        public void LogException(Exception exception)
        {
            ConsoleColor prevBG = Console.BackgroundColor;
            ConsoleColor prevFG = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.DarkRed;

            string time = GetTime();
            string t = $"\n[{time}] [EROR] {exception.Message}";
            Console.WriteLine(t);
            string tl = $"> [{time}] [EROR] {exception.StackTrace}";
            Console.WriteLine(tl);
            t += '\n'+tl;
            tl = $"> [{time}] [EROR] {exception.Source}";
            Console.WriteLine(tl);
            t += '\n'+tl;
            if(logToFile)
                WriteToFile(t);

            Console.ForegroundColor = prevFG;
            Console.BackgroundColor = prevBG;

            Console.WriteLine();
        }

        private void WriteToFile(string text, bool flush = true)
        {
            logFileStream.Write(System.Text.Encoding.UTF8.GetBytes(text));
            if(flush)
                logFileStream.Flush();
        }
    }
}
