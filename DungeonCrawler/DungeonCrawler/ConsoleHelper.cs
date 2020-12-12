using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler
{
    public static class ConsoleHelper
    {
        public static Random RandomSeed { get; set; } = new Random();

        public static void ColorText(string message, ConsoleColor foregroundColor, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(message);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void ColorWord(string word, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(word);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static string GetInput(string message)
        {
            Console.WriteLine(message);
            var input = Console.ReadLine().Trim();
            if (input == "")
            {
                ColorText("Unos ne može biti prazan!", ConsoleColor.Yellow);
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
                ColorText("Unos mora biti broj!", ConsoleColor.Yellow);
                return GetNumber(message);
            }
            if (number <= 0)
            {
                ColorText("Unos mora biti veći od 0!", ConsoleColor.Yellow);
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

        public static bool ConfirmAction(string message)
        {
            ColorText(message, ConsoleColor.Magenta);
            var input = Console.ReadLine().Trim().ToLower();
            if (input == "da") return true;
            if (input == "ne") return false;
            else
            {
                ColorText("Neispravan odabir!", ConsoleColor.Yellow);
                return ConfirmAction(message);
            }
        }

        public static (bool doesQuit, string input) GetInputOrQuit(string message)
        {
            var input = GetInput(message);
            if (input == "quit")
            {
                if (!ConfirmAction("Jeste li sigurni da želite odustati?")) return GetInputOrQuit(message);
                return (true, "");
            }
            return (false, input);
        }

        
    }
}
