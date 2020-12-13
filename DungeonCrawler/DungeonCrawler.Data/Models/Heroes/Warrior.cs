using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DungeonCrawler.Data.Models.Heroes
{
    public class Warrior : Hero
    {
        public Warrior() : base()
        {
            HealthPoints = 80;
            Damage = 15;
            Health = HealthPoints;
        }

        public bool IsAttackFurious;

        public override int Attack()
        {
            if (IsAttackFurious)
            {
                Health -= (int) (0.25 * HealthPoints);
                return Damage * 2;
            }

            return Damage;
        }

    }
}
