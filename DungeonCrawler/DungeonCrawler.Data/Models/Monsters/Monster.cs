using System;
using System.Collections.Generic;
using System.Text;
using DungeonCrawler.Data.Abstractions;
using DungeonCrawler.Data.Enums;

namespace DungeonCrawler.Data.Models.Monsters
{
    public abstract class Monster:IEntityInfo
    {
        public int HealthPoints { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public string Name { get; set; }
        public int ExperienceDefined { get; set; }

        private Random _rand = new Random();

        public Monster()
        {
            Storage.EntityList.Add(this);
        }

        public override string ToString()
        {
            return Name.ToUpper();
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
