using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler
{
    class Brute:Monster
    {
        public Brute() : base()
        {
            Name = "Brute";
            ExperienceDefined = 20;
            HealthPoints = 45 + ConsoleHelper.RandomSeed.Next(0, 5);
            Health = HealthPoints;
            Damage = 19 + ConsoleHelper.RandomSeed.Next(0, 3);
        }

        public int PercentDamageChance { get; set; } = 15;
        public override int Attack()
        {
            var rand = new Random();
            if (rand.Next(0, 100) < PercentDamageChance)
            {
                Console.WriteLine("Jači damage - 25% zdravlja!");
                return (int)(0.25 * Rounds[0].Hero.Health);
            }
            return Damage;
        }
    }
}
