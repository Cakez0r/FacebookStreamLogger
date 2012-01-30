using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookToDB
{
    public static class Logger
    {
        public enum LogLevel
        {
            INFO = 1,
            WARNING = 2,
            ERROR = 3,

            OFF = 4
        }

        static LogLevel s_currentLogLevel = LogLevel.INFO;
        public static LogLevel CurrentLogLevel
        {
            get { return s_currentLogLevel; }

            set
            {
                Info(string.Format("Log level changed from {0} to {1}", s_currentLogLevel, value));
                s_currentLogLevel = value;
            }
        }


        public static void Info(string msg)
        {
            Output(LogLevel.INFO, msg);
        }

        public static void Warn(string msg)
        {
            Output(LogLevel.WARNING, msg);
        }

        public static void Error(string msg)
        {
            Output(LogLevel.ERROR, msg);
        }

        static void Output(LogLevel level, string output)
        {
            if ((int)level >= (int)CurrentLogLevel)
            {
                string msg = string.Format("[{0}] [{1} {2}]: {3}", level.ToString(), DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString(), output);

                ConsoleColor restoreColor = Console.ForegroundColor;

                switch (level)
                {
                    case LogLevel.INFO:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                    case LogLevel.WARNING:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;

                    case LogLevel.ERROR:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                }

                Console.WriteLine(msg);

                Console.ForegroundColor = restoreColor;
            }
        }
    }
}
