using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler.Data.Models.Heroes
{
    public class Ranger:Hero
    {
        public Ranger():base()
        {
            HealthPoints = 70;
            Damage = 20;
            Health = HealthPoints;
        }

        public int CriticalChance { get; set; } = 20;
        public int StunChance { get; set; } = 5;
        public override int Attack()
        {
            var rand = new Random();
            if (rand.Next(0, 100) < StunChance && Storage.EntityList.Count>=2) Storage.EntityList[2].Health = 0;
            if (rand.Next(0, 100) < CriticalChance)
            {
                Console.WriteLine("Double damage!");
                return 2 * base.Attack();
            }
            return base.Attack();
        }
    }
}
