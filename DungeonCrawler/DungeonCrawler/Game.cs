using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler
{
    class Game
    {
        HeroType GetHeroType(string message)
        {
            var input = ConsoleHelper.CapitalizeWord(ConsoleHelper.GetInput(message).ToLower());
            var success = Enum.TryParse(typeof(HeroType), input, out object hero);
            if (!success)
            {
                Console.WriteLine("Odabir nije ispravan!");
                return GetHeroType(message);
            }
            return (HeroType)hero;
        }

        enum HeroType
        {
            Warrior, Mage, Ranger
        }
        public Game()
        {
            var welcome = @"--- DUNGEON CRAWL ---
Unesite quit za izlaz!";
            Console.WriteLine(welcome);
            var heroType = GetHeroType("Unesite tip heroja (Warrior/Mage/Ranger)");
            Hero hero = null;
            Goblin monster = new Goblin();
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
                default:
                    break;
            }
            var rounds = new List<Round>();
            for (var i = 0; i < 5; ++i) rounds.Add(new Round(hero));
            while (rounds.Count > 0)
            {
                Console.WriteLine("PREOSTALO RUNDI: " + rounds.Count);
                rounds[0].Play(rounds);
                if (!rounds[0].DidWin())
                {
                    Console.WriteLine("Game over!");
                    if (ConsoleHelper.ConfirmAction("Pokrenuti novu igru?"))
                    {
                        Console.Clear();
                        new Game();
                    }
                    return;
                }
                rounds.RemoveAt(0);
            }

            Console.WriteLine("Bravo, pobjeda!");
        }

    }
}

