using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler
{
    public static class ConsoleHelper
    {
        public static string GetInput(string message)
        {
            Console.WriteLine(message);
            var input = Console.ReadLine().Trim();
            if (input == "")
            {
                Console.WriteLine("Unos ne može biti prazan!");
                return GetInput(message);
            }
            return input;
        }

        public static (int number, bool isDefault) GetNumber(string message)
        {
            Console.WriteLine(message);
            int number;
            var inputString = Console.ReadLine().Trim();
            if (inputString == "") return (-1, true);
            var success = int.TryParse(inputString, out number);
            if (!success)
            {
                Console.WriteLine("Unos mora biti broj!");
                return GetNumber(message);
            }
            if (number <= 0)
            {
                Console.WriteLine("Unos mora biti veći od 0!");
                return GetNumber(message);
            }
            return (number, false);

        }

        public static string CapitalizeWord(string word)
        {
            if (word.Length > 1)
                return char.ToUpper(word[0]) + word.Substring(1);
            else return "";
        }

        
    }
}
