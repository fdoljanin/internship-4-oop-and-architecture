using System;
using DungeonCrawler.Domain.Helpers;
using DungeonCrawler.Domain.Services;
namespace DungeonCrawler.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            new Game();
            Console.Clear();
            ConsoleHelper.ColorText("Gašenje...", ConsoleColor.White, ConsoleColor.DarkBlue);
        }
    }
}
