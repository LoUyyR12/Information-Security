using System;
using System.Collections.Generic;

namespace Practice3
{
    class MainProgram
    {
        static void Main()
        {
            Terminal.DefaultConsole();
            Console.WriteLine("Hi! There are some code for tasks 1-4 laying around. Do you want to test it?");
            string WhatDoYouWant = "chocolate";
            while (WhatDoYouWant == "chocolate")
            {
                Terminal.DefaultConsole();
                Console.WriteLine("Choose what do you want to do right now:");
                string it_is_DEFINETLY_a_number = Terminal.ReadValue(new List<string> { "Enter a number from 1 to 4 for corresponding tasks" , "Enter \"e\" to exit a program" });
                switch (it_is_DEFINETLY_a_number)
                {
                    case "1":
                        {
                            Tasks.Task1();
                            break;
                        }
                    case "2":
                        {
                            Tasks.Task2();
                            break;
                        }
                    case "3":
                        {
                            Tasks.Task3();
                            break;
                        }
                    case "4":
                        {
                            Tasks.Task4();
                            break;
                        }
                    case "e":
                        {
                            WhatDoYouWant = "Get some sleep";
                            break;
                        }
                    case "":
                        {
                            Console.WriteLine("I know Enter is a good key, but in this case you should type something before pressing it.");
                            break;
                        }
                    default:
                        {
                            Console.WriteLine($"\"{it_is_DEFINETLY_a_number}\"? This does not look like a task number or exit command. Let's try again - ");
                            break;
                        }
                }
            }

            Console.WriteLine();
            Terminal.WriteLineWithColor("Bye!", ConsoleColor.Green);
        }
    }
}
