using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Practice9
{
    class Program
    {
        static void Main(string[] args)
        {
            string publicPath = "PublicKey.xml";
            string signaturePath = "Signature.dat";
            string dataPath = "Data.dat";
            string WhatDoYouWant = "passAllExams";
            while (WhatDoYouWant == "passAllExams")
            {
                Terminal.WriteLineWithColor(new List<string> { "To create a signature for data from console enter 1", "To verify signature with data from file enter 2", "to exit a program enter e" }, Terminal.inputMessagesColor);
            string answer = Terminal.ReadValue();
            switch (answer) {
                case "1":
                {
                    Terminal.WriteLineWithColor("Enter the text to sign", Terminal.inputMessagesColor);
                    byte[] DataToSign = Encoding.UTF8.GetBytes(Terminal.ReadValue());
                    File.WriteAllBytes(dataPath, DataToSign);
                    byte[] signature = Methods.CreateSign(publicPath, DataToSign);
                    File.WriteAllBytes(signaturePath, signature);
                    Terminal.WriteLineWithColor("Succesfully signed!", Terminal.arrowsColor);
                    break;
                }
            case "2":
                {
                    byte[] DataToSign = File.ReadAllBytes(dataPath);
                    byte[] signature = File.ReadAllBytes(signaturePath);
                    if (Methods.VerifySignature(publicPath, DataToSign, signature))
                    {

                        Terminal.WriteLineWithColor("Data verified", Terminal.arrowsColor);
                    }
                    else
                    {
                        Terminal.WriteLineWithColor("Verification Incorrect", ConsoleColor.Red);
                    }
                    break;
                }
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
                {
                    Terminal.WriteLineWithColor($"\"{answer}\"? This does not look like a task number or exit command. Let's try again - ");
                    break;
                }
                }
            }
            Terminal.WriteLineWithColor("Bye!", Terminal.arrowsColor);

        }

    }
}
