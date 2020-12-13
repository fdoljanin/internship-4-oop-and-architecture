using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler.Data.Models.Monsters
{
    public class Witch:Monster
    {
        public Witch()
        {
            Name = "Witch";
            ExperienceDefined = 20;
            HealthPoints = 35 + Rand.Next(0,8);
            Health = HealthPoints;
            Damage = 20 + Rand.Next(0, 3);
        }

        public override int Attack()
        {
            if (Rand.Next(0, 10) < 2)
            {
                Console.WriteLine("ĐUMBUS!");
                foreach (var entity in Storage.EntityList)
                {
                    entity.Health = Rand.Next(1, entity.HealthPoints + 1);
                }
                return 0;
            }
            return Damage;
        }
    }
}
