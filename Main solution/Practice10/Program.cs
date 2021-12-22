using System;

namespace Practice11
{
    class Program
    {
        static void Main(string[] args)
        {
            string answer;
            string WhatDoYouWant = "passAllExams";
            while (WhatDoYouWant == "passAllExams")
            {
                Terminal.WriteLineWithColor("Sign up - 1; Log in - 2; Exit - e", Terminal.inputMessagesColor);
                answer = Terminal.ReadValue();
                switch (answer)
                {
                    case "1":
                        Protector.Register();
                        break;
                    case "2":
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
                        Terminal.WriteLineWithColor("Invalid Answer", ConsoleColor.Red);
                        break;
                }
            }
            Terminal.WriteLineWithColor("Bye!", Terminal.arrowsColor);
        }

    }
}
