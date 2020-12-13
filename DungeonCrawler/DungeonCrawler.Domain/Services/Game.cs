using System;
using System.Collections.Generic;
using System.Text;
using DungeonCrawler.Data;
using DungeonCrawler.Data.Models.Heroes;
using DungeonCrawler.Domain.Helpers;

namespace DungeonCrawler.Domain.Services
{
    public class Game
    {
        HeroType GetHeroType(string message)
        {
            var input = ConsoleHelper.CapitalizeWord(ConsoleHelper.GetInput(message).ToLower());
            var success = Enum.TryParse( input, out HeroType hero);
            if (success) return hero;
            ConsoleHelper.ColorText("Odabir nije ispravan!", ConsoleColor.Yellow);
            return GetHeroType(message);
        }

        public Game()
        {
            ConsoleHelper.ColorText("——— DUNGEON CRAWL ———", ConsoleColor.White, ConsoleColor.DarkBlue);
            var heroType = GetHeroType("Unesite tip heroja (Warrior/Mage/Ranger)");
            Hero hero = null;
            switch (heroType)
            {
                case HeroType.Warrior:
                    hero = new Warrior();
                    break;
                case HeroType.Mage:
                    hero = new Mage();
                    break;
                case HeroType.Ranger:
                    hero = new Ranger();
                    break;
            }
            HeroInteraction.ConfigureHero(hero);

            ConsoleHelper.ColorText("Za izlaz tijekom igre unesite quit \n", ConsoleColor.DarkRed);
            var rounds = new List<Round>();
            for (var i = 0; i < 2; ++i) rounds.Add(new Round(hero));

            while (rounds.Count > 0)
            {
                ConsoleHelper.ColorText("PREOSTALO RUNDI: " + rounds.Count, ConsoleColor.White, ConsoleColor.DarkCyan);
                rounds[0].Play(rounds);

                if (!rounds[0].DidWinOrQuit().didWin || rounds[0].DidWinOrQuit().didQuit)
                {
                    if (!rounds[0].DidWinOrQuit().didWin) ConsoleHelper.ColorText("Game over!", ConsoleColor.White, ConsoleColor.DarkRed);
                    if (rounds[0].DidWinOrQuit().didQuit) ConsoleHelper.ColorText("Odustali ste...", ConsoleColor.White, ConsoleColor.DarkMagenta);

                    if (!ConsoleHelper.ConfirmAction("Pokrenuti novu igru?")) return;
                    Console.Clear();
                    new Game();
                    return;
                }
                rounds.RemoveAt(0);
                Storage.EntityList.RemoveAt(1);
            }

            ConsoleHelper.ColorText("Bravo, pobjeda!", ConsoleColor.White, ConsoleColor.DarkGreen);
            Console.ReadLine();
        }

    }
}

