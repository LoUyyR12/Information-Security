using CryptoRng;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Practice6
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string answer = Terminal.ReadValue("Enter 1 for the first task, 2 for the second or e to exit");
                switch(answer){
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
            var key = Crypto.GetCryptoRng(8);
            var iv = Crypto.GetCryptoRng(8);
            const string original = "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua";
            var encrypted = DesChipher.Encrypt(Encoding.UTF8.GetBytes(original), key, iv);
            var decrypted = DesChipher.Decrypt(encrypted, key, iv);
            var decryptedMessage = Encoding.UTF8.GetString(decrypted);
            Console.WriteLine();
            Terminal.WriteLineWithColor("DES Encryption", Terminal.inputMessagesColor);
            Console.WriteLine();
            Console.WriteLine("Original Text > " + original);
            Console.WriteLine("Encrypted Text > " +
            Convert.ToBase64String(encrypted));
            Console.WriteLine("Decrypted Text > " + decryptedMessage);



            var key2 = Crypto.GetCryptoRng(16);
            var iv2 = Crypto.GetCryptoRng(8);
            const string original2 = "Ultrices dui sapien eget mi proin sed libero";
            var encrypted2 = TripleDesChipher.Encrypt(Encoding.UTF8.GetBytes(original2), key2, iv2);
            var decrypted2 = TripleDesChipher.Decrypt(encrypted2, key2, iv2);
            var decryptedMessage2 = Encoding.UTF8.GetString(decrypted2);
            Console.WriteLine();
            Terminal.WriteLineWithColor("TripleDes Encryption", Terminal.inputMessagesColor);
            Console.WriteLine();
            Console.WriteLine("Original Text > " + original2);
            Console.WriteLine("Encrypted Text > " +
            Convert.ToBase64String(encrypted2));
            Console.WriteLine("Decrypted Text > " + decryptedMessage2);


            var key3 = Crypto.GetCryptoRng(32);
            var iv3 = Crypto.GetCryptoRng(16);
            const string original3 = "Volutpat ac tincidunt vitae semper quis lectus nulla";
            var encrypted3 = AesChipher.Encrypt(Encoding.UTF8.GetBytes(original3), key3, iv3);
            var decrypted3 = AesChipher.Decrypt(encrypted3, key3, iv3);
            var decryptedMessage3 = Encoding.UTF8.GetString(decrypted3);
            Console.WriteLine();
            Terminal.WriteLineWithColor("AES Encryption", Terminal.inputMessagesColor);
            Console.WriteLine();
            Console.WriteLine("Original Text > " + original3);
            Console.WriteLine("Encrypted Text > " +
            Convert.ToBase64String(encrypted3));
            Console.WriteLine("Decrypted Text > " + decryptedMessage3);
            Console.WriteLine();
        }
        static void Task2()
        {
            Console.WriteLine();
            var passw = Terminal.ReadValue("Enter new password");
            const string original = "Lorem ipsum dolor sit amet, consectetur adipiscing elit";
            var key = PBKDF2.HashPassword(Encoding.Unicode.GetBytes(passw), PBKDF2.NewSalt(), 14 * 10000, HashAlgorithmName.SHA256, 32);
            var initVector = PBKDF2.HashPassword(Encoding.Unicode.GetBytes(passw), PBKDF2.NewSalt(), 14 * 10000, HashAlgorithmName.SHA256, 16);
            var encrypted = AesChipher.Encrypt(Encoding.UTF8.GetBytes(original), key, initVector);
            var decrypted = AesChipher.Decrypt(encrypted, key, initVector);
            var decryptedMessage = Encoding.UTF8.GetString(decrypted);
            Console.WriteLine();
            Console.WriteLine("Original Text = " + original);
            Console.WriteLine("Encrypted Text = " + Convert.ToBase64String(encrypted));
            Console.WriteLine("Decrypted Text = " + decryptedMessage);
            Console.WriteLine();
        }
    }
}
