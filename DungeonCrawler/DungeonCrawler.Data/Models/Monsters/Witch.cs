using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler.Data.Models.Monsters
{
    public class Witch:Monster
    {
        public Witch():base()
        {
            Name = "Witch";
            ExperienceDefined = 20;
            HealthPoints = 35 + new Random().Next(0,8);
            Health = HealthPoints;
            Damage = 20 + new Random().Next(0, 3);
        }
        public override int Attack()
        {
            var rand = new Random();
            if (rand.Next(0, 10) < 2)
            {
                Console.WriteLine("ĐUMBUS!");
                foreach (var entity in Storage.EntityList)
                {
                    entity.Health = new Random().Next(1, entity.HealthPoints + 1);
                }
                return 0;
            }
            return Damage;
        }
        public override bool Die()
        {
            return base.Die();
        }
    }
}
