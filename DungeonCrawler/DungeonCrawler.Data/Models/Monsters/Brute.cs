using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler.Data.Models.Monsters
{
    public class Brute:Monster
    {
        public Brute()
        {
            Name = "Brute";
            ExperienceDefined = 20;
            HealthPoints = 45 + new Random().Next(0, 5);
            Health = HealthPoints;
            Damage = 19 + new Random().Next(0, 3);
        }

        private int _damageChance = 15;
        public override int Attack()
        {
            var rand = new Random();
            if (rand.Next(0, 100) < _damageChance)
            {
                Console.WriteLine("Damage - 25% ukupnog zdravlja!");
                return (int)(0.25 * Storage.EntityList[0].HealthPoints);
            }
            return Damage;
        }
    }
}
