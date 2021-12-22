using System;
namespace Practice11
{
    class Program
    {
        static void Main(string[] args)
        {
            var conf = new LoggingConfiguration();
            var consoleTarget = new ColoredConsoleTarget("target1");
            conf.AddTarget(consoleTarget);
            var fileTarget = new FileTarget("target2")
            {
                FileName = "logging.log"
            };
            conf.AddTarget(fileTarget);
            conf.AddRuleForAllLevels(consoleTarget);
            conf.AddRuleForOneLevel(NLog.LogLevel.Warn, fileTarget);
            conf.AddRuleForOneLevel(NLog.LogLevel.Error, fileTarget);

            LogManager.Configuration = conf;
            Logger nLogger = LogManager.GetLogger("log1");

            string pr = "Practice 10 class Programm method Main |";
            string answer;
            string WhatDoYouWant = "passAllExams";
            while (WhatDoYouWant == "passAllExams")
            {
                Terminal.WriteLineWithColor("Sign up - 1; Log in - 2; Exit - e", Terminal.inputMessagesColor);
                answer = Terminal.ReadValue();
                nLogger.Info(pr + "Answer was entered");
                switch (answer)
                {
                    case "1":
                        nLogger.Debug(pr + "Register method was selected");
                        Protector.Register();
                        break;
                    case "2":
                        nLogger.Debug(pr + "Login method was selected");
                        Protector.LogIn();
                        break;
                    case "e":
                        {
                            WhatDoYouWant = "Some sleep";
                            break;
                        }
                    case "":
                        {
                            Terminal.WriteLineWithColor("I know Enter is a good key, but in this case you should type something before pressing it.");
                            break;
                        }
                    default:
                        nLogger.Debug(pr + "Invalid answer was entered");
                        Terminal.WriteLineWithColor("Invalid Answer", ConsoleColor.Red);
                        break;
                }
            }
            Terminal.WriteLineWithColor("Bye!", Terminal.arrowsColor);
            nLogger.Debug(pr + "Program exited");
        }

    }
}
