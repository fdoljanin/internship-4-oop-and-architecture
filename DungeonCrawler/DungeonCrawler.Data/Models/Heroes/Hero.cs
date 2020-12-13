using System;
using System.Collections.Generic;
using System.Text;
using DungeonCrawler.Data.Abstractions;

namespace DungeonCrawler.Data.Models.Heroes
{
    public abstract class Hero:IEntityInfo
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int HealthPoints { get; set; }
        public int Experience { get; set; }
        public int ExperienceLevelUp { get; set; } = 35;
        public int Damage { get; set; }
        public int Health { get; set; }

        public Hero()
        {
            Storage.EntityList.Add(this);
        }
        public virtual int Attack()
        {
            return Damage;
        }


        public override string ToString()
        {
            return Name.ToUpper();
        }

        public bool Suffer(int damageSuffered)
        {
            Health -= damageSuffered;
            if (Health <= 0) return  Die();
            return true;
        }
        public virtual bool Die()
        {
            return false;
        }


        private void LevelUp()
        {
            Level++;
            HealthPoints += 10;
            Damage = (int)(Damage*1.3);
        }
        public bool WinCheckLevelUp(bool doesRegenerate)
        {
            Health = (Health + (int)(HealthPoints * 0.25) > HealthPoints) ? HealthPoints : Health + (int)(HealthPoints * 0.25);
            if (doesRegenerate)
            {
                Experience -= Experience / 2;
                Health = HealthPoints;
            }
            if (Experience < ExperienceLevelUp) return false;
            Experience -= ExperienceLevelUp;
            LevelUp();
            return true;
        }
    }
}
