using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Practice3
{
    class Authorization
    {
        static List<string> hashedLogins = File.ReadLines("logins.dat").ToList();
        static List<string> hashedPasswords = File.ReadLines("passwords.dat").ToList();
        public static void SignUp()
        {
            hashedLogins = File.ReadLines("logins.dat").ToList();
            string enteredPassword = null;
            string enteredLogin = null;
            bool loginCreated = false;
            while (!loginCreated)
            {
                enteredLogin = Terminal.ReadValue("Enter a login for your new account");
                if (enteredLogin == "")
                {
                    Terminal.WriteLineWithColor("Login must consist of at least 1 character", ConsoleColor.Red);
                    returnToMenu();
                    return;
                }
                if (hashedLogins.Contains(Hashing.Sha512(Encoding.UTF8.GetBytes(enteredLogin))))
                {
                    Terminal.WriteLineWithColor("This login is already in use, please enter another", ConsoleColor.Red);
                    Console.WriteLine();
                    continue;
                }
                loginCreated = true;
            }
            bool passwordCreated = false;
            while (!passwordCreated)
            {
                enteredPassword = Terminal.ReadValue("Enter a password for your new account");
                if (enteredPassword == "")
                {
                    Terminal.WriteLineWithColor("Password must consist of at least 1 character", ConsoleColor.Red);
                    Console.WriteLine();
                    continue;
                }
                if (Terminal.ReadValue("Repeat your password") != enteredPassword)
                {
                    Terminal.WriteLineWithColor("Passwords do not match!", ConsoleColor.Red);
                    Console.WriteLine();
                    continue;
                }
                passwordCreated = true;
            }
            if (loginCreated && passwordCreated) Terminal.WriteLineWithColor("You have successfully created a new account \""+ enteredLogin+'"', ConsoleColor.Green);
            File.AppendAllLines("logins.dat", new[] { Hashing.Sha512(Encoding.UTF8.GetBytes(enteredLogin)) });
            File.AppendAllLines("passwords.dat", new[] { Hashing.Sha512(Encoding.UTF8.GetBytes(enteredPassword)) });
            Tasks.Task4();
        }

        public static void LogIn()
        {
            hashedPasswords = File.ReadLines("passwords.dat").ToList();
            hashedLogins = File.ReadLines("logins.dat").ToList();
            bool loginEntering = true;
            bool passwordEntering = false;
            int loginID = 0;
            while (loginEntering)
            {
                string enteredLogin = Terminal.ReadValue("Enter your login or press Enter to return to the main menu");
                if (enteredLogin == "")
                {
                    returnToMenu();
                    return;
                }
                string loginHash = Hashing.Sha512(Encoding.UTF8.GetBytes(enteredLogin));
                if (!hashedLogins.Contains(loginHash))
                {
                    Terminal.WriteLineWithColor("User with this login does not exist", ConsoleColor.Red);
                    Console.WriteLine();
                    continue;
                }
                loginEntering = false;
                passwordEntering = true;
                loginID = hashedLogins.IndexOf(loginHash);
            }
            while (passwordEntering)
            {
                string enteredPassword = Terminal.ReadValue("Enter your password or press Enter to return to the main menu");
                if (enteredPassword == "")
                {
                    returnToMenu();
                    return;
                }
                string passwordHash = Hashing.Sha512(Encoding.UTF8.GetBytes(enteredPassword));
                if (hashedPasswords[loginID] != passwordHash)
                {
                    Terminal.WriteLineWithColor("Entered password is incorrect", ConsoleColor.Red);
                    Console.WriteLine();
                    continue;
                }
                passwordEntering = false;
            }
            Terminal.WriteLineWithColor("You have successfully logged in", ConsoleColor.Green);
        }
        public static void ClearFiles()
        {
            File.WriteAllText("logins.dat", string.Empty);
            File.WriteAllText("passwords.dat", string.Empty);
            Console.WriteLine();
            Terminal.WriteLineWithColor("All stored hashes were successfully deleted", ConsoleColor.Green);
            Tasks.Task4();
        }
        public static void ShowData()
        {
            List<string> hashedLogins = File.ReadLines("logins.dat").ToList();
            List<string> hashedPasswords = File.ReadLines("passwords.dat").ToList();
            int accountsNumber = 0;
            Terminal.WriteLineWithColor("Logins:", Terminal.inputMessagesColor);
            foreach (string login in hashedLogins)
            {
                accountsNumber++;
                Console.WriteLine(login);
                Console.WriteLine();
            }
            Terminal.WriteLineWithColor("Passwords:", Terminal.inputMessagesColor);
            foreach (string pass in hashedPasswords)
            {
                Console.WriteLine(pass);
                Console.WriteLine();
            }
            if (accountsNumber > 0)
            {
                Terminal.WriteWithColor("A total of ", Terminal.inputMessagesColor);
                Terminal.WriteWithColor($"{accountsNumber}", Terminal.arrowsColor);
                Terminal.WriteLineWithColor(" accounts have been created", Terminal.inputMessagesColor);
            }
            else
            {
                Terminal.WriteLineWithColor("No accounts has been created yet", ConsoleColor.Red);
            }

            Tasks.Task4();
        }
        public static void returnToMenu()
        {
            Console.WriteLine();
            Terminal.WriteLineWithColor("Returning you to the main menu...", ConsoleColor.Green);
            Terminal.HideText();
            Console.WriteLine();
        }
    }
}
