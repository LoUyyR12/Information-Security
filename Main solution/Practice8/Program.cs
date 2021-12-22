using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Practice9
{
    class Program
    {
        private readonly static string CspContainerName = "RsaContainer";
        static void Main(string[] args)
        {
            string WhatDoYouWant = "passAllExams";
            while (WhatDoYouWant == "passAllExams")
            {
                Terminal.WriteLineWithColor(new List<string> { "To encrypt message from console enter 1", "To decrypt encrypted message from file enter 2", "To assign a new pair of keys enter 3", "to exit a program enter e" }, Terminal.inputMessagesColor);
                string answer = Terminal.ReadValue();
                switch (answer)
                {
                    case "1":
                        {
                            Terminal.WriteLineWithColor("Please enter the data to encrypt: ", Terminal.inputMessagesColor);
                            byte[] textToEncrypt = Encoding.UTF8.GetBytes(Terminal.ReadValue());
                            Terminal.WriteLineWithColor("Please enter the name of the file to read the open key: ", Terminal.inputMessagesColor);
                            string publicPath = Terminal.ReadValue();
                            var encText = Encryption.EncryptData(textToEncrypt, publicPath + ".xml");
                            Terminal.WriteLineWithColor("Please enter the name of the file to write the message: ", Terminal.inputMessagesColor);
                            string filename = Terminal.ReadValue();
                            File.WriteAllBytes(filename + ".dat", encText);
                            Terminal.WriteLineWithColor("Encrypted message: " + Convert.ToBase64String(encText), Terminal.inputMessagesColor);
                            break;
                        }
                    case "2":
                        {
                            Terminal.WriteLineWithColor("Please enter the name of the file with the message: ", Terminal.inputMessagesColor);
                            string filename = Terminal.ReadValue();
                            string publicPath = filename + ".dat";
                            var encText = File.ReadAllBytes(publicPath);
                            var decText = Encryption.DecryptData(encText);
                            Terminal.WriteLineWithColor("Decrypted message: " + Encoding.UTF8.GetString(decText), Terminal.inputMessagesColor);
                            break;
                        }
                    case "3":
                        {
                            Terminal.WriteLineWithColor("Please enter the name of the file to write the open key: ", Terminal.inputMessagesColor);
                            string filename = Terminal.ReadValue();
                            string publicPath = filename + "PublicKey.xml";
                            Keys.AssignNewKey(publicPath);
                            break;
                        }
                    case "e":
                        {
                            WhatDoYouWant = "Some sleep";
                            break;
                        }
                    case "":
                        {
                            Console.WriteLine("I know Enter is a good key, but in this case you should type something before pressing it.");
                            break;
                        }
                    default:
                        {
                            Console.WriteLine($"\"{answer}\"? This does not look like a task number or exit command. Let's try again - ");
                            break;
                        }
                }
            }
        }

        public static string GetCspContainerName()
        {
            return CspContainerName;
        }
    }
}
