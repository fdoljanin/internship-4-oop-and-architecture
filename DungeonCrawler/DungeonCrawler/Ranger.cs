using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler
{
    class Ranger:Hero
    {
        public Ranger():base()
        {
            if (HealthPoints == 0) HealthPoints = 10;
            if (Damage == 0) Damage = 3;
        }
    }
}
