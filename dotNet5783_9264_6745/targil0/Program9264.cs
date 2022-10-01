// See https://aka.ms/new-console-template for more information

using System;

namespace targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            
            Welcome9264();
            Welcome6745();
            Console.ReadKey();

        }

        static partial void Welcome6745();
        private static void Welcome9264()
        {
            Console.WriteLine("Enter your name:");
            string userName = Console.ReadLine();
            Console.WriteLine("{0},welcome to my first application", userName);

        }
    }
}
