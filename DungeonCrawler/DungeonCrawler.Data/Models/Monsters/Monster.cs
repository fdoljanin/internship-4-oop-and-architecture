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
        protected int ExperienceDefined { get; set; }

        protected Random Rand = new Random();

        protected Monster()
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
            return (ActionType) Rand.Next(0, 3);
        }

        public bool SurviveSuffer(int damage)
        {
            Health -= damage;
            if (Health <= 0) return false;
            return true;
        }


        public int GetExperience()
        {
            return ExperienceDefined;
        }

    }
}
