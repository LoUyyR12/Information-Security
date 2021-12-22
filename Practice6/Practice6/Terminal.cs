using System;
using System.Collections.Generic;
using System.Text;

namespace Practice6
{
    class Terminal
    {
        public static readonly ConsoleColor defaultColor = ConsoleColor.White;
        public static readonly ConsoleColor defaultBackgroundColor = ConsoleColor.Black;
        public static readonly ConsoleColor inputColor = ConsoleColor.Magenta;
        public static readonly ConsoleColor arrowsColor = ConsoleColor.Green;
        public static readonly ConsoleColor inputMessagesColor = ConsoleColor.DarkCyan;

        public static void DefaultConsole()
        {
            Console.BackgroundColor = defaultBackgroundColor;
            Console.ForegroundColor = defaultColor;
        }

        public static void HideText()
        {
            Console.ForegroundColor = defaultBackgroundColor;
        }

        public static string ReadValue(List<string> description)
        {
            Console.ForegroundColor = inputMessagesColor;
            foreach (string message in description)
            {
                Console.WriteLine(message);
            }
            Console.ForegroundColor = arrowsColor;
            Console.WriteLine("↓ ↓ ↓");
            Console.ForegroundColor = inputColor;
            string value = Console.ReadLine();
            Console.ForegroundColor = arrowsColor;
            Console.WriteLine("↑ ↑ ↑");
            DefaultConsole();
            return value;
        }
        public static string ReadValue(string descriptionPhrase = null)
        {
            Console.ForegroundColor = inputMessagesColor;
            Console.WriteLine(descriptionPhrase);
            Console.ForegroundColor = arrowsColor;
            Console.WriteLine("↓ ↓ ↓");
            Console.ForegroundColor = inputColor;
            string value = Console.ReadLine();
            Console.ForegroundColor = arrowsColor;
            Console.WriteLine("↑ ↑ ↑");
            DefaultConsole();
            return value;
        }
        public static void WriteLineWithColor(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            DefaultConsole();
        }
        public static void WriteLineWithColor(List<string> messages, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            foreach (string message in messages)
            {
                Console.WriteLine(message);
            }
            DefaultConsole();
        }
        public static void WriteWithColor(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            DefaultConsole();
        }
        public static void WriteWithColor(List<string> messages, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            foreach (string message in messages)
            {
                Console.Write(message);
            }
            DefaultConsole();
        }
    }
}
