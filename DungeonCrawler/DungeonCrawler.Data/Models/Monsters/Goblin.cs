using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler.Data.Models.Monsters
{
    public class Goblin:Monster
    {
        public Goblin() : base()
        {
            Name = "Goblin";
            ExperienceDefined = 10;
            HealthPoints = 30 + new Random().Next(0, 3);
            Health = HealthPoints;
            Damage = 8 + new Random().Next(0, 3);
        }
    }
}
