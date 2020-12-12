using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler
{
    class Witch:Monster
    {
        public Witch():base()
        {
            Name = "Witch";
            ExperienceDefined = 20;
            HealthPoints = 35 + ConsoleHelper.RandomSeed.Next(0,8);
            Health = HealthPoints;
            Damage = 20 + ConsoleHelper.RandomSeed.Next(0, 3);
        }
        public override int Attack()
        {
            var rand = new Random();
            if (rand.Next(0, 10) < 2)
            {
                Console.WriteLine("ĐUMBUS!");
                Rounds[0].Hero.Health = rand.Next(0, Rounds[0].Hero.HealthPoints + 1);
                foreach (var round in Rounds)
                {
                    round.Monster.Health = rand.Next(0, round.Monster.HealthPoints + 1);
                }
                return 0;
            }
            return Damage;
        }
        public override bool Die()
        {
            Rounds.Insert(1, new Round(Rounds[0].Hero));
            Rounds.Insert(1, new Round(Rounds[0].Hero));
            return base.Die();
        }
    }
}
