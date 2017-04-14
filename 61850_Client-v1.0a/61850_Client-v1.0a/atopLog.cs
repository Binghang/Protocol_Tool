using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using NLog;
namespace _61850_Client_v1._0a
{
    public static class atopLog
    {
        public static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static void WriteLog(atopLogMode LogMode, string Message)
        {
            switch (LogMode)
            {
                case atopLogMode.TestFail:
                    Display(ConsoleColor.Black, ConsoleColor.Red, Message + "is Fail");
                    logger.Info(Message);
                    break;
                case atopLogMode.TestSuccess:
                    Display(ConsoleColor.Black, ConsoleColor.Blue, Message + "is Success");
                    logger.Info(Message);
                    break;
                case atopLogMode.XelasCommandError:
                    Display(ConsoleColor.Black, ConsoleColor.Yellow, Message + "is Error");
                    logger.Error(Message);
                    break;
                case atopLogMode.XelasCommandInfo:
                    Display(ConsoleColor.Black, ConsoleColor.Blue, Message + "is Success");
                    logger.Info(Message);
                    break;
                case atopLogMode.SystemInformation:
                    Display(ConsoleColor.Black, ConsoleColor.Blue, Message + "is Success");
                    logger.Info(Message);
                    break;
                case atopLogMode.SystemError:
                    Display(ConsoleColor.Black, ConsoleColor.Gray, Message + "is Error");
                    logger.Error(Message);
                    break;
            }
        }
        
        /// <summary>
        /// Display to Form
        /// </summary>
        /// <param name="Message"></param>
        public static void Display(ConsoleColor BackgroundColor, ConsoleColor ForegroundColor, string Message)
        {
            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = ForegroundColor;
            Console.WriteLine(Message);
        }
    }
}
