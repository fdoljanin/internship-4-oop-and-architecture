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
        bool _continue = true;
        bool _autoWon = false;
        public Round(Hero inHero)
        {
            Hero = inHero;
            var rand = new Random();
            var monsterType = (MonsterType)rand.Next(0, 3);
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
            Console.WriteLine("NOVA RUNDA! \n \n");
            while (true)
            {
                Hero.Info();
                Monster.Info();
                var heroOption = Hero.GetAction();
                var monsterOption = Monster.Action();
                Console.WriteLine("Čudovište je odabralo: " + monsterOption);
                if (heroOption == monsterOption) continue;
                if (heroOption == ActionType.Direct && monsterOption == ActionType.Side
                    || heroOption == ActionType.Side && monsterOption == ActionType.Counter
                    || heroOption == ActionType.Counter && monsterOption == ActionType.Direct)
                {
                    Console.WriteLine("Hero pobijedio potez!");
                    if (Monster.Suffer(Hero.Attack()));
                    else
                    {
                        Hero.Win(Monster.GetExperience());
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Monster pobijedio potez!");
                    if (Hero.Suffer(Monster.Attack()));
                    else
                    {
                        _continue = false;
                        break;
                    }
                }
            }
        }
        public bool Continue()
        {
            return _continue;
        }
    }
}
