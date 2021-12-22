using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;

namespace Practice11
{
    class Protector
    {
        private static Dictionary<string, User> _users = new Dictionary<string, User>();
        public static User Register()
        {
            Terminal.WriteLineWithColor("Create your Username: ", Terminal.inputMessagesColor);
            string userName = Terminal.ReadValue();
            if (_users.ContainsKey(userName))
            {
                Terminal.WriteLineWithColor("User with this login already exists", ConsoleColor.Red);
                return null;
            }
            Terminal.WriteLineWithColor("Create your password: ", Terminal.inputMessagesColor);
            string password = Terminal.ReadValue();
            Terminal.WriteLineWithColor("Enter up to four assigned roles [Admin, User, Guest, Manager]. If you have more than one role, press Enter after entering each one. Enter \"e\" to finish: ", Terminal.inputMessagesColor);
            string rol = "";
            string[] roles = new string[4];
            int rolesAmount = 0;
            while (rol != "e" && rolesAmount < 4)
            {
                rol = Terminal.ReadValue();
                if (rol == "Admin" || rol == "User" || rol == "Guest" || rol == "Manager")
                {
                    roles[rolesAmount] = rol;
                    rolesAmount++;
                }
                else if (rol == "e")
                {
                    break;
                }
                else
                {
                    Terminal.WriteLineWithColor("Invalid Input", ConsoleColor.Red);
                }
            }
            User a = new User();
            byte[] salt = GenerateSalt();
            byte[] hash = HashPassword(Encoding.Unicode.GetBytes(password), salt);
            a.Login = userName;
            a.PasswordHash = Convert.ToBase64String(hash);
            a.Salt = Convert.ToBase64String(salt);
            a.Roles = roles;
            _users.Add(userName, a);
            Terminal.WriteLineWithColor("You have successfully registered", ConsoleColor.Green);
            return null;
        }
        public static byte[] GenerateSalt()
        {
            const int saltLength = 32;
            using (var randomNumberGenerator =
            new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[saltLength];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }
        public static byte[] HashPassword(byte[] toBeHashed, byte[] salt)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(
            toBeHashed, salt, 100000))
            {
                return rfc2898.GetBytes(20);
            }
        }
        public static void LogIn()
        {
            Terminal.WriteLineWithColor("Enter your Username: ", Terminal.inputMessagesColor);
            string userName = Terminal.ReadValue();
            if (!(_users.ContainsKey(userName)))
            {
                Terminal.WriteLineWithColor("User with this Login does not exist", ConsoleColor.Red);
            }
            else
            {
                Terminal.WriteLineWithColor("Enter your password: ", Terminal.inputMessagesColor);
                string password = Terminal.ReadValue();
                User a = _users.GetValueOrDefault(userName);
                byte[] salt = Convert.FromBase64String(a.Salt);
                string hash = a.PasswordHash;
                string hashCheck = Convert.ToBase64String(HashPassword(Encoding.Unicode.GetBytes(password), salt));
                if (hash != hashCheck)
                {
                    Terminal.WriteLineWithColor("Password Incorrect", ConsoleColor.Red);
                }
                else
                {
                    Terminal.WriteLineWithColor("You have successfully logged in", ConsoleColor.Green);
                    var identity = new GenericIdentity(userName, "OIBAuth");
                    var principal = new GenericPrincipal(identity, _users[userName].Roles);
                    Thread.CurrentPrincipal = principal;
                    try
                    {
                        OnlyForAdminsFeature();
                    }
                    catch (Exception ex)
                    {
                        Terminal.WriteLineWithColor($"{ex.GetType()}: {ex.Message}");
                    }
                }
            }
        }
        public static void OnlyForAdminsFeature()
        {
            if (Thread.CurrentPrincipal == null)
            {
                throw new SecurityException("Thread.CurrentPrincipal cannot be null.");
            }
            else if (!Thread.CurrentPrincipal.IsInRole("Admin"))
            {
                Terminal.WriteLineWithColor("User must be an Admin to access this feature.", ConsoleColor.Red);
            }
            else
            {
                Terminal.WriteLineWithColor("You are an Admin. You now have the access to the secure feature", Terminal.inputColor);
                string[] message = File.ReadAllLines("Admins.txt");
                foreach (string i in message)
                {
                     Console.Write(i);
                }
                Console.WriteLine();
            }

        }
    }
    }
