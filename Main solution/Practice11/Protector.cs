using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.IO;
using NLog;
using NLog.Config;
using NLog.Targets;
using Microsoft.Extensions.Logging;

namespace Practice11
{
    class Protector
    {
        private static Dictionary<string, User> _users = new Dictionary<string, User>();
        public static User Register()
        {
            Logger nLogger = LogManager.GetLogger("log1");
            string pr = "Practice 11 class Protector method Register |";
            Terminal.WriteLineWithColor("Create your Username: ", Terminal.inputMessagesColor);
            string userName = Terminal.ReadValue();
            if (_users.ContainsKey(userName))
            {
                Terminal.WriteLineWithColor("User with this login already exists", ConsoleColor.Red);
                nLogger.Trace(pr + "Username that was entered already exists");
                return null;
            }
            Terminal.WriteLineWithColor("Create your password: ", Terminal.inputMessagesColor);
            string password = Terminal.ReadValue();
            nLogger.Debug(pr + "Password was entered");
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
                    nLogger.Debug(pr + "A role was entered");
                }
                else if (rol == "e")
                {
                    nLogger.Trace(pr + "Finished entering roles");
                    break;
                    nLogger.Debug(pr + "A role was entered incorrectly");
                }
                else
                {
                    Terminal.WriteLineWithColor("Invalid Input", ConsoleColor.Red);
                }
            }
            User a = new User();
            nLogger.Trace(pr + "A new User class is created");
            byte[] salt = GenerateSalt();
            byte[] hash = HashPassword(Encoding.Unicode.GetBytes(password), salt);
            a.Login = userName;
            a.PasswordHash = Convert.ToBase64String(hash);
            a.Salt = Convert.ToBase64String(salt);
            a.Roles = roles;
            _users.Add(userName, a);
            nLogger.Debug("A new User is added to the dictionary");
            Terminal.WriteLineWithColor("You have successfully registered", ConsoleColor.Green);
            nLogger.Info("A new user has been successfully registered");
            return null;
        }
        public static byte[] GenerateSalt()
        {
            Logger nLogger = LogManager.GetLogger("log1");
            string pr = "Practice 11 class Protector method GenerateSalt |";
            nLogger.Trace(pr + "Salt is being generated randomly");
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
            Logger nLogger = LogManager.GetLogger("log1");
            string pr = "Practice 11 class Protector method HashPassword |";
            nLogger.Trace(pr + "Password and salt are being hashed");
            using (var rfc2898 = new Rfc2898DeriveBytes(
            toBeHashed, salt, 100000))
            {
                return rfc2898.GetBytes(20);
            }
        }
        public static void LogIn()
        {
            Logger nLogger = LogManager.GetLogger("log1");
            string pr = "Practice 11 class Protector method LogIn |";
            Terminal.WriteLineWithColor("Enter your Username: ", Terminal.inputMessagesColor);
            string userName = Terminal.ReadValue();
            nLogger.Debug(pr + "A username is entered");
            if (!(_users.ContainsKey(userName)))
            {
                Terminal.WriteLineWithColor("User with this Login does not exist", ConsoleColor.Red);
                nLogger.Warn(pr + "A Username has been entered incorrectly");
            }
            else
            {
                Terminal.WriteLineWithColor("Enter your password: ", Terminal.inputMessagesColor);
                string password = Terminal.ReadValue();
                nLogger.Debug(pr + "Password is entered");
                User a = _users.GetValueOrDefault(userName);
                nLogger.Trace(pr + "User's " + userName + " salt and hash are being checked from the User dictioany");
                byte[] salt = Convert.FromBase64String(a.Salt);
                string hash = a.PasswordHash;
                nLogger.Trace(pr + "User's " + userName + " entered password is being hashed and compared to the hash from the dictionary");
                string hashCheck = Convert.ToBase64String(HashPassword(Encoding.Unicode.GetBytes(password), salt));
                if (hash != hashCheck)
                {
                    nLogger.Warn(pr + "User's " + userName + " password was entered incorrectly");
                    Terminal.WriteLineWithColor("Password Incorrect", ConsoleColor.Red);
                }
                else
                {
                    Terminal.WriteLineWithColor("You have successfully logged in", ConsoleColor.Green);
                    nLogger.Info(pr + "User " + userName + " has successfully logged in");
                    var identity = new GenericIdentity(userName, "OIBAuth");
                    var principal = new GenericPrincipal(identity, _users[userName].Roles);
                    nLogger.Trace(pr + "Method GenericPrincipal is being used to give or deny user the acces to a part of program based on the roles");
                    Thread.CurrentPrincipal = principal;
                    try
                    {
                        nLogger.Trace(pr + "Trying to gain access to restricted method");
                        OnlyForAdminsFeature();
                    }
                    catch (Exception ex)
                    {
                        nLogger.Error(pr + "Thread.CurrentPrincipal cannot be null");
                        Terminal.WriteLineWithColor($"{ex.GetType()}: {ex.Message}");
                    }
                }
            }
        }
        public static void OnlyForAdminsFeature()
        {
            Logger nLogger = LogManager.GetLogger("log1");

            string pr = "Practice 11 class Protector method OnlyForAdminsFeature |";
            if (Thread.CurrentPrincipal == null)
            {
                throw new SecurityException("Thread.CurrentPrincipal cannot be null.");
            }
            else if (!Thread.CurrentPrincipal.IsInRole("Admin"))
            {
                nLogger.Warn(pr + "Access to the secure feature is denied");
                Terminal.WriteLineWithColor("User must be an Admin to access this feature.", ConsoleColor.Red);
            }
            else
            {
                nLogger.Trace(pr + "Given access to the secure feature");
                Terminal.WriteLineWithColor("You are an Admin. You now have the access to the secure feature", Terminal.inputColor);
                string[] message = File.ReadAllLines("Admins.txt");
                foreach (string i in message)
                {
                     Console.Write(i);
                }
                Console.WriteLine();
                nLogger.Debug(pr + "Secure feature");
            }

        }
    }
    }
