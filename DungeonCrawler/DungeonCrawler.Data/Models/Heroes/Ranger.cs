using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler.Data.Models.Heroes
{
    public class Ranger:Hero
    {
        public Ranger()
        {
            HealthPoints = 70;
            Damage = 20;
            Health = HealthPoints;
        }

        int _criticalChance { get; set; } = 20;
        int _stunChance { get; set; } = 5;
        public override int Attack()
        {
            if (Rand.Next(0, 100) < _stunChance && Storage.EntityList.Count>=2) Storage.EntityList[2].Health = 0;
            if (Rand.Next(0, 100) < _criticalChance)
            {
                Console.WriteLine("Double damage!");
                return 2 * base.Attack();
            }
            return base.Attack();
        }
    }
}
