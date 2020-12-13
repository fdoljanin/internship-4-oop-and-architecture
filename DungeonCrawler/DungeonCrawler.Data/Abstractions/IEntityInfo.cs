using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler.Data.Abstractions
{
    public interface IEntityInfo
    {
        int Health { get; set; }
        int HealthPoints { get; set; }
        int Damage { get; set; }
    }
}
