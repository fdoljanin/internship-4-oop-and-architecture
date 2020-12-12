using System;
using System.Collections.Generic;

namespace DungeonCrawler
{
    class Program
    {


        static HeroType GetHeroType(string message)
        {
            //HeroType hero;
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
        static void Main(string[] args)
        {
            var welcome = @"--- DUNGEON CRAWL ---
";
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
            for (var i = 0; i < 3; ++i) rounds.Add(new Round(hero));
            for (var i = 0; i < 3; ++i)
            {
                rounds[i].Play();
                if (!rounds[i].Continue()) break;
            }
        }

    }
}