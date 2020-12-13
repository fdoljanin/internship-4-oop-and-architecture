using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler.Data.Models.Monsters
{
    public class Brute:Monster
    {
        public Brute() : base()
        {
            Name = "Brute";
            ExperienceDefined = 20;
            HealthPoints = 45 + new Random().Next(0, 5);
            Health = HealthPoints;
            Damage = 19 + new Random().Next(0, 3);
        }

        public int PercentDamageChance { get; set; } = 15;
        public override int Attack()
        {
            var rand = new Random();
            if (rand.Next(0, 100) < PercentDamageChance)
            {
                Console.WriteLine("Damage - 25% ukupnog zdravlja!");
                return (int)(0.25 * Storage.EntityList[0].HealthPoints);
            }
            return Damage;
        }
    }
}
