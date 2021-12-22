using System;
using System.Text;

namespace Practice9
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string answer = Terminal.ReadValue("Enter 1 for the first task, 2 for the second or e to exit");
                switch (answer)
                {
                    default:
                        {
                            Terminal.WriteLineWithColor($"\"{answer}\"? I don't understand what you mean.", ConsoleColor.Red);
                            break;
                        }
                    case "1":
                        {
                            Task1();
                            break;
                        }
                    case "2":
                        {
                            Task2();
                            break;
                        }
                    case "e":
                        {
                            Console.WriteLine();
                            Terminal.WriteLineWithColor("Bye!", ConsoleColor.Green);
                            return;
                        }
                }
            }
        }
        static void Task1()
        {
            var rsaParams = new RSAParameters();
            const string original = "Lorem ipsum dolor sit amet, consectetur adipiscing elit";
            rsaParams.AssignNewKey();
            var encrypted = rsaParams.EncryptData(Encoding.UTF8.GetBytes(original));
            var decrypted = rsaParams.DecryptData(encrypted);
            Console.WriteLine();
            Terminal.WriteWithColor("Original Text > ", Terminal.inputMessagesColor);
            Terminal.WriteLineWithColor(original, Terminal.inputColor);
            Console.WriteLine();
            Terminal.WriteWithColor("Encrypted Text > ", Terminal.inputMessagesColor);
            Terminal.WriteLineWithColor(Convert.ToBase64String(encrypted), Terminal.arrowsColor);
            Console.WriteLine();
            Terminal.WriteWithColor("Decrypted Text > ", Terminal.inputMessagesColor);
            Terminal.WriteLineWithColor(Encoding.Default.GetString(decrypted), Terminal.inputColor);
            Console.WriteLine();
        }
        static void Task2()
        {
            string mess = "Volutpat ac tincidunt vitae semper quis lectus nulla";
            Task2Methods.AssignNewKey("Public.xml");
            var encrypted = Task2Methods.EncryptData("Public.xml", Encoding.Unicode.GetBytes(mess));
            var decrypted = Task2Methods.DecryptData(encrypted);
            Console.WriteLine();
            Terminal.WriteWithColor("Message > ", Terminal.inputMessagesColor);
            Terminal.WriteLineWithColor(mess, Terminal.inputColor);
            Console.WriteLine();
            Terminal.WriteWithColor("Encrypted message > ", Terminal.inputMessagesColor);
            Terminal.WriteLineWithColor(Convert.ToBase64String(encrypted), Terminal.arrowsColor);
            Console.WriteLine();
            Terminal.WriteWithColor("Decrypted message > ", Terminal.inputMessagesColor);
            Terminal.WriteLineWithColor(Encoding.Unicode.GetString(decrypted), Terminal.inputColor);
            Console.WriteLine();
        }

    }
}
