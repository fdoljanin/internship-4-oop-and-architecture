using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler
{
    public interface IEntityInfo
    {
        public int Health { get; set; }
        public int HealthPoints { get; set; }
        public int Damage { get; set; }
    }
}
