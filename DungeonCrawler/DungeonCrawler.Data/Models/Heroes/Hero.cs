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
        private int _experienceLevelUp = 35;
        public int Damage { get; set; }
        public int Health { get; set; }
        protected Random Rand = new Random();

        protected Hero()
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

        public bool SurviveSuffer(int damageSuffered)
        {
            Health -= damageSuffered;
            if (Health <= 0) return  Die();
            return true;
        }

        protected virtual bool Die()
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
            if (Experience < _experienceLevelUp) return false;
            Experience -= _experienceLevelUp;
            LevelUp();
            return true;
        }
    }
}
