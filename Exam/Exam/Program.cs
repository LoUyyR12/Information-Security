using System;
using NLog;
using NLog.Config;
using NLog.Targets;
using Microsoft.Extensions.Logging;

namespace Exam
{
    class Program
    {
        static void Main(string[] args)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {

            });
            var conf = new LoggingConfiguration();
            var consoleTarget = new ColoredConsoleTarget("target1");
            conf.AddTarget(consoleTarget);
            var fileTarget = new FileTarget("target2")
            {
                FileName = "MasterLog.log"
            };
            conf.AddTarget(fileTarget);
            conf.AddRuleForAllLevels(consoleTarget);
            conf.AddRuleForOneLevel(NLog.LogLevel.Warn, fileTarget);
            conf.AddRuleForOneLevel(NLog.LogLevel.Error, fileTarget);
            conf.AddRuleForOneLevel(NLog.LogLevel.Fatal, fileTarget);

            LogManager.Configuration = conf;
            Logger nLogger = LogManager.GetLogger("log1");
            string prefix = "Exam Main method | ";
            Console.WriteLine("Enter \"hi\"");
            string hello = Console.ReadLine();
            if (hello != "hi")
            {
                Terminal.WriteLineWithColor("nLogger.Warn | " + prefix + "Incorrect input", ConsoleColor.Yellow);
                nLogger.Warn(prefix + "Incorrect input");
            }
            else
            {
                Terminal.WriteLineWithColor("Hello!", ConsoleColor.Green);
            }
            Console.WriteLine("Enter a number");
            string number = Console.ReadLine();
            try
            {
                int integer = Int32.Parse(number);
                Terminal.WriteLineWithColor("This number was very useful, thanks!", ConsoleColor.Green);
            }
            catch (FormatException)
            {
                Terminal.WriteLineWithColor("nLogger.Error | " + prefix + "\"" + number + "\"" + " is not a number, Parse failed", ConsoleColor.Red);
                nLogger.Error(prefix + number + "is not a number, Parse failed");
            }

            Console.WriteLine("Enter a password of at least 8 characters");
            string password = Console.ReadLine();
            if (password.Length < 8)
            {
                Terminal.WriteLineWithColor("nLogger.Fatal | " + prefix + password + " is less than 8 characters! Don't do that.", ConsoleColor.DarkRed);
                nlogger.fatal(prefix + password + " is less than 8 characters! don't do that.");
            }
            else
            {
                Terminal.WriteLineWithColor("Password saved", ConsoleColor.Green);
            }

        }
    }
}
