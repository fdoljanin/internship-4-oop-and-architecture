using System;
using System.Collections.Generic;
using System.Text;
using DungeonCrawler.Data.Models.Monsters;

namespace DungeonCrawler.Domain.Helpers
{
    public static class MonsterInteraction
    {
        public static void MonsterInfo(Monster monster)
        {
            ConsoleHelper.ColorWord($"--{monster}--", ConsoleColor.DarkGray);
            ConsoleHelper.ColorWord($" Zdravlje: ", ConsoleColor.DarkGreen);
            Console.Write(monster.Health);
            ConsoleHelper.ColorWord($" Damage: ", ConsoleColor.DarkRed);
            Console.Write(monster.Damage);
            Console.WriteLine("");
        }
    }
}
