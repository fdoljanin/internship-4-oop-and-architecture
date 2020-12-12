using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler
{
    class Ranger:Hero
    {
        public Ranger():base()
        {
            if (HealthPoints == 0) HealthPoints = 70;
            if (Damage == 0) Damage = 20;
            Health = HealthPoints;
        }

        public int CriticalChance { get; set; } = 20;
        public int StunChance { get; set; } = 5;
        public override int Attack()
        {
            var rand = new Random();
            if (rand.Next(0, 100) < StunChance && Rounds.Count>1) Rounds[1].AutoWin();
            if (rand.Next(0, 100) < CriticalChance)
            {
                Console.WriteLine("Dvostruki damage!");
                return 2 * base.Attack();
            }
            return base.Attack();
        }
    }
}
