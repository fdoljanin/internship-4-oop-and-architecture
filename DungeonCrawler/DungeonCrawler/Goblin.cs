using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler
{
    class Goblin:Monster
    {
        public Goblin() : base()
        {
            Name = "Goblin";
            ExperienceDefined = 10;
            HealthPoints = 30 + ConsoleHelper.RandomSeed.Next(0, 3);
            Health = HealthPoints;
            Damage = 8 + ConsoleHelper.RandomSeed.Next(0, 3);
        }
    }
}
