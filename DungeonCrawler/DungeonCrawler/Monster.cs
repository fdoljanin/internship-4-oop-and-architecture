using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler
{
    abstract class Monster
    {
        public int HealthPoints { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public int ExperienceDefined { get; set; } = 10;
        public Monster()
        {
            HealthPoints = 9;
            Health = HealthPoints;
            Damage = 3;
        }

        public void Info()
        {
            Console.WriteLine($"-- MONSTER -- Health: {Health}, Damage: {Damage}");
        }

        public int Attack()
        {
            return Damage;
        }

        public ActionType Action()
        {
            var rand = new Random();
            return (ActionType)rand.Next(0, 3);
        }

        public bool Suffer(int damage)
        {
            Health -= damage;
            if (Health <= 0) return false;
            return true;
        }

        public void Die()
        {
            Console.WriteLine("I died...");
            
        }

        public int GetExperience()
        {
            return ExperienceDefined;
        }

    }
}
