using System;
using System.Diagnostics;
using System.Text;

namespace Practice5
{
    class Program
    {
        static void Main(string[] args)
        {

            string password1 = Terminal.ReadValue("Enter new password");
            int Start = 140000;
            int step = 50000;
            for (int i = 0; i < 10; i++)
            {
                var sw = new Stopwatch();
                sw.Start();
                byte[] salt1 = PBKDF2.NewSalt();
                var hashedPassword1 = PBKDF2.PassHash(Encoding.UTF8.GetBytes(password1), salt1, Start);
                Terminal.WriteWithColor("Password >>> ", Terminal.inputMessagesColor);
                Terminal.WriteLineWithColor(password1, Terminal.inputColor);
                Terminal.WriteWithColor("Salt >>> ", Terminal.inputMessagesColor);
                Terminal.WriteLineWithColor(Convert.ToBase64String(salt1), Terminal.arrowsColor);
                Terminal.WriteWithColor("Hashed Password >>> ", Terminal.inputMessagesColor);
                Terminal.WriteLineWithColor(Convert.ToBase64String(hashedPassword1), Terminal.arrowsColor);
                Terminal.WriteWithColor("Iteration <", Terminal.inputMessagesColor);
                Terminal.WriteWithColor(Start.ToString(), Terminal.inputColor);
                Terminal.WriteWithColor(">, Time elapsed >>> ", Terminal.inputMessagesColor);
                Terminal.WriteLineWithColor(sw.ElapsedMilliseconds + "ms", Terminal.inputColor);
                Console.WriteLine();
                Start += step;
            }

        }

    }
}
