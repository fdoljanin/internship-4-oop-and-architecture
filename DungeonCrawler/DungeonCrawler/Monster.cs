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
        public string Name { get; set; }
        public int ExperienceDefined { get; set; }

        public List<Round> Rounds;
        private Random _rand = new Random();

        public void SetRounds(List<Round> rounds)
        {
            Rounds = rounds;
        }

        public override string ToString()
        {
            ConsoleHelper.ColorWord($"--{Name.ToUpper()}--", ConsoleColor.DarkGray);
            ConsoleHelper.ColorWord($" Zdravlje: ", ConsoleColor.DarkGreen);
            Console.Write(Health);
            ConsoleHelper.ColorWord($" Damage: ", ConsoleColor.DarkRed);
            Console.Write(Damage);
            Console.WriteLine("");
            return "";
        }
    

        public virtual int Attack()
        {
            return Damage;
        }

        public ActionType Action()
        {
            return (ActionType) _rand.Next(0, 3);
        }

        public bool Suffer(int damage)
        {
            Health -= damage;
            if (Health <= 0) return Die();
            return true;
        }

        public virtual bool Die()
        {
            return false;
        }

        public int GetExperience()
        {
            return ExperienceDefined;
        }

    }
}
