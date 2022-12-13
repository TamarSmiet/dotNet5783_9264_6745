// See https://aka.ms/new-console-template for more information
using System;

namespace testProject
{
    partial class Program
    {
        static void Main(string[]args)
        {
            Welcome6745();
            Welcome9264();
            Console.ReadKey();

        }

        static partial void Welcome9264();
        private static void Welcome6745()
        {
            Console.WriteLine("Enter your name:");
            string userName = Console.ReadLine();
            Console.WriteLine("{0},welcome to my first application", userName);
            
        }
    }
}

