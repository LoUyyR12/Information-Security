using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CryptoRng;
using System.Text;

namespace Practice3
{
    class Tasks
    {
        public static void Task1()
        {
            byte[] Data = File.ReadAllBytes("data.dat").ToArray();
            Console.WriteLine("Hashing with Md5:");
            Console.WriteLine("Hash: " + Convert.ToBase64String(Hashing.Md5(Data)));
            Guid guid1 = new Guid(Hashing.Md5(Data));
            Console.WriteLine("GUID: " + guid1);
            Console.WriteLine("Hashing with SHA1:");
            Console.WriteLine(Hashing.Sha1.Hash(Data));
            Console.WriteLine("Hashing with SHA256:");
            Console.WriteLine(Hashing.Sha256(Data));
            Console.WriteLine("Hashing with SHA384:");
            Console.WriteLine(Hashing.Sha384(Data));
            Console.WriteLine("Hashing with SHA512:");
            Console.WriteLine(Hashing.Sha512(Data));

            Console.WriteLine();
            Console.WriteLine("This is the end of the first task.");
            Console.WriteLine();
        }
        public static void Task2()
        {
            Console.WriteLine();
            string[] yes = { "y", "Y", "yep", "Yep", "yea", "Yea", "ok", "+", "sure", "Sure" };
            string answer = Terminal.ReadValue("Would you like to see debug messages while the program is calculating the answer?");
            bool debug = false;
            if (answer == "yes" || answer == "Yes")
            {
                Console.WriteLine("Okay, let's begin.");
                Console.WriteLine();
                debug = true;
            }
            if (yes.Contains(answer))
            {
                Console.WriteLine($"\"{answer}\"? I'll take it as yes.");
                debug = true;
            }
            if (!debug)
                Console.WriteLine($"\"{answer}\"? I'll take it as no.");

            string targetGuid = "564c8da6-0440-88ec-d453-0bbad57c6036";
            string targetHash = "po1MVkAE7IjUUwu61XxgNg==";
            string password;
            for (int i = 0; i <= 99999999; i++)
            {
                string pass = i.ToString();
                while (pass.Length < 8)
                {
                    pass = "0" + pass;
                }
                if (i % 100000 == 0 & debug) Console.WriteLine($"Debug => passing breakpoint \"{pass}\"");
                var hashPass = Hashing.Md5(Encoding.Unicode.GetBytes(pass));
                string hashStr = Convert.ToBase64String(hashPass);
                Guid guidPass = new Guid(hashPass);
                string guidStr = guidPass.ToString();
                if ((hashStr == targetHash) && (guidStr == targetGuid))
                {
                    password = pass;
                    Console.WriteLine();
                    Console.WriteLine("Password founded => " + password); // password was "20192020"
                    break;
                }
            }
            Console.WriteLine();
            Console.WriteLine("This is the end of the second task.");
            Console.WriteLine();
        }
        public static void Task3()
        {
            byte[] key = Crypto.GetCryptoRng(32);
            byte[] message = Encoding.UTF8.GetBytes(Terminal.ReadValue("Enter your stunning phrase for the first message"));
            var HMACkey = Hashing.Sha1.HMAC(message, key);
            string HMACkeyString = Convert.ToBase64String(HMACkey);
            Console.WriteLine($"SHA1 HMAC hash => {HMACkeyString}");
            byte[] secondMessage = Encoding.UTF8.GetBytes(Terminal.ReadValue("Enter your witty second message:"));
            var HMACkey2 = Hashing.Sha1.HMAC(secondMessage, key);
            string HMACkey2String = Convert.ToBase64String(HMACkey2);
            Console.WriteLine($"SHA1 HMAC hash => {HMACkey2String}");
            if (HMACkey2String == HMACkeyString)
            {
                Console.WriteLine("Btw, your messages are authentic");
            }
            else
            {
                Console.WriteLine("Great to know - your messages are not authentic");
            }
            Console.WriteLine();
            Console.WriteLine("This is the end of the third task.");
            Console.WriteLine();
        }
        public static void Task4()
        {
            Console.WriteLine();
            Console.WriteLine("Choose what do you want to do now:");
            string LetMeIn = Terminal.ReadValue(new List<string> { "Enter \"l\" to log in with an existing account", "Enter \"r\" to register a new account", "Enter anything else to return to the main menu" });
            if (LetMeIn == "/eraseData") Authorization.ClearFiles();
            if (LetMeIn == "/showData") Authorization.ShowData();
            if (LetMeIn == "l") Authorization.LogIn();
            if (LetMeIn == "r") Authorization.SignUp();
            Authorization.returnToMenu();
        }
    }
}
