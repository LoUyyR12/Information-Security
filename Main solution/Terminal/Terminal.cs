using System;
using System.Collections.Generic;

namespace Console
{
    public class Terminal
    {
        public static readonly ConsoleColor defaultColor = ConsoleColor.White;
        public static readonly ConsoleColor defaultBackgroundColor = ConsoleColor.Black;
        public static readonly ConsoleColor inputColor = ConsoleColor.Magenta;
        public static readonly ConsoleColor arrowsColor = ConsoleColor.Green;
        public static readonly ConsoleColor inputMessagesColor = ConsoleColor.DarkCyan;

        public static void DefaultConsole()
        {
            System.Console.BackgroundColor = defaultBackgroundColor;
            System.Console.ForegroundColor = defaultColor;
        }

        public static void HideText()
        {
            System.Console.ForegroundColor = defaultBackgroundColor;
        }

        public static string ReadValue(List<string> description)
        {
            System.Console.ForegroundColor = inputMessagesColor;
            foreach (string message in description)
            {
                System.Console.WriteLine(message);
            }
            System.Console.ForegroundColor = arrowsColor;
            System.Console.WriteLine("↓ ↓ ↓");
            System.Console.ForegroundColor = inputColor;
            string value = System.Console.ReadLine();
            System.Console.ForegroundColor = arrowsColor;
            System.Console.WriteLine("↑ ↑ ↑");
            DefaultConsole();
            return value;
        }
        public static string ReadValue(string descriptionPhrase = null)
        {
            System.Console.ForegroundColor = inputMessagesColor;
            System.Console.WriteLine(descriptionPhrase);
            System.Console.ForegroundColor = arrowsColor;
            System.Console.WriteLine("↓ ↓ ↓");
            System.Console.ForegroundColor = inputColor;
            string value = System.Console.ReadLine();
            System.Console.ForegroundColor = arrowsColor;
            System.Console.WriteLine("↑ ↑ ↑");
            DefaultConsole();
            return value;
        }
        public static void WriteLineWithColor(string message, ConsoleColor color = ConsoleColor.White)
        {
            System.Console.ForegroundColor = color;
            System.Console.WriteLine(message);
            DefaultConsole();
        }
        public static void WriteLineWithColor(List<string> messages, ConsoleColor color = ConsoleColor.White)
        {
            System.Console.ForegroundColor = color;
            foreach (string message in messages)
            {
                System.Console.WriteLine(message);
            }
            DefaultConsole();
        }
        public static void WriteWithColor(string message, ConsoleColor color = ConsoleColor.White)
        {
            System.Console.ForegroundColor = color;
            System.Console.Write(message);
            DefaultConsole();
        }
        public static void WriteWithColor(List<string> messages, ConsoleColor color = ConsoleColor.White)
        {
            System.Console.ForegroundColor = color;
            foreach (string message in messages)
            {
                System.Console.Write(message);
            }
            DefaultConsole();
        }
    }
}
