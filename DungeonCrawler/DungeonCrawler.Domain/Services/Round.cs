using System;
using System.Collections.Generic;
using System.Text;
using DungeonCrawler.Data;
using DungeonCrawler.Data.Enums;
using DungeonCrawler.Data.Models.Heroes;
using DungeonCrawler.Data.Models.Monsters;
using DungeonCrawler.Domain.Helpers;

namespace DungeonCrawler.Domain.Services
{
    public class Round
    {
        private Monster Monster;
        private Hero Hero;
        private bool _continue = true;
        private bool _quits = false;
        public Round(Hero inHero)
        {
            Hero = inHero;
            MonsterType monsterType = MonsterType.Goblin;
            var random = ConsoleHelper.RandomSeed.Next(0, 100);
            if (random >= 55) monsterType = MonsterType.Brute;
            if (random >= 93) monsterType = MonsterType.Witch;

            switch (monsterType)
            {
                case MonsterType.Goblin:
                    Monster = new Goblin();
                    break;
                case MonsterType.Brute:
                    Monster = new Brute();
                    break;
                case MonsterType.Witch:
                    Monster = new Witch();
                    break;
            }

        }

        public bool HeroWins(ActionType heroOption, ActionType monsterOption)
        {
            if (heroOption == ActionType.Direct && monsterOption == ActionType.Side
                || heroOption == ActionType.Side && monsterOption == ActionType.Counter
                || heroOption == ActionType.Counter && monsterOption == ActionType.Direct)
                return true;

            return false;
        }

        public void Play(List <Round> rounds)
        {
            if (Storage.EntityList[1].Health==0)
            {
                Console.WriteLine("Runda automatski osvojena...");
                HeroInteraction.Win(Hero, Monster.GetExperience());
                return;
            }

            Console.WriteLine("NOVA RUNDA! \n");
            while (true)
            {
                HeroInteraction.HeroInfo(Hero);
                MonsterInteraction.MonsterInfo(Monster);
                var heroInput = HeroInteraction.GetAction(Hero);
                if (heroInput.doesQuit)
                {
                    _quits = true;
                    return;
                }

                var heroOption = heroInput.action;
                var monsterOption = Monster.Action();
                Console.WriteLine("Monster je odabrao: " + monsterOption);

                if (heroOption == monsterOption)
                {
                    ConsoleHelper.ColorText("Odabrali ste istu akciju! \n", ConsoleColor.DarkGray);
                    continue;
                }

                if (HeroWins(heroOption, monsterOption))
                {
                    ConsoleHelper.ColorText($"{Hero.Name} pobijedi potez!\n", ConsoleColor.Green);
                    if (Monster.SurviveSuffer(Hero.Attack())) continue;
                    if (Monster is Witch)
                    {
                        ConsoleHelper.ColorText("Stvorila se 2 nova monstera!", ConsoleColor.DarkRed);
                        new Round(Hero);
                        new Round(Hero);
                    }
                    if (rounds.Count>1) HeroInteraction.Win(Hero,Monster.GetExperience());
                    break;
                }
                else
                {
                    ConsoleHelper.ColorText("Monster pobijedi potez!\n", ConsoleColor.Red);
                    if (Hero.SurviveSuffer(Monster.Attack())) continue;
                    _continue = false;
                     break;
                }
            }
        }
        public (bool didWin, bool didQuit) DidWinOrQuit()
        {
            return (_continue, _quits);
        }
    }
}
