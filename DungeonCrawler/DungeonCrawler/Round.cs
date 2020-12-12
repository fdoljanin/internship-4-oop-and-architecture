using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler
{
    enum MonsterType
    {
        Goblin, Brute, Witch
    }
    class Round
    {
        public Monster Monster;
        public Hero Hero;
        private bool _continue = true;
        private bool _quits = false;
        private bool _autoWon = false;
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
                default:
                    break;
            }

        }

        public void AutoWin()
        {
            _autoWon = true;
        }
        public void Play(List <Round> rounds)
        {
            if (_autoWon)
            {
                Console.WriteLine("Runda automatski osvojena...");
                Hero.Win(Monster.GetExperience());
                return;
            }
            Hero.SetRounds(rounds);
            Monster.SetRounds(rounds);
            Console.WriteLine("NOVA RUNDA! \n");
            while (true)
            {
                Console.Write(Hero);
                Console.Write(Monster);
                var heroInput = Hero.GetAction();
                if (heroInput.doesQuit)
                {
                    _quits = true;
                    return;
                }
                var heroOption = heroInput.action;
                var monsterOption = Monster.Action();
                Console.WriteLine("Čudovište je odabralo: " + monsterOption);
                if (heroOption == monsterOption)
                {
                    ConsoleHelper.ColorText("Odabrali ste istu akciju! \n", ConsoleColor.DarkGray);
                    continue;
                }
                if (heroOption == ActionType.Direct && monsterOption == ActionType.Side
                    || heroOption == ActionType.Side && monsterOption == ActionType.Counter
                    || heroOption == ActionType.Counter && monsterOption == ActionType.Direct)
                {
                    ConsoleHelper.ColorText("Hero pobijedio potez!\n", ConsoleColor.Green);
                    if (Monster.Suffer(Hero.Attack()));
                    else
                    {
                        if (rounds.Count>1) Hero.Win(Monster.GetExperience());
                        break;
                    }
                }
                else
                {
                    ConsoleHelper.ColorText("Monster pobijedio potez!\n", ConsoleColor.Red);
                    if (Hero.Suffer(Monster.Attack()));
                    else
                    {
                        _continue = false;
                        break;
                    }
                }
            }
        }
        public (bool didWin, bool didQuit) DidWinOrQuit()
        {
            return (_continue, _quits);
        }
    }
}
