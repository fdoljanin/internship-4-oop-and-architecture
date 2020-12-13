using System;
using System.Collections.Generic;
using System.Text;
using DungeonCrawler.Data.Abstractions;

namespace DungeonCrawler.Data
{
    public static class Storage
    {
        public static List<IEntityInfo> EntityList { get; set; } = new List<IEntityInfo>();
    }
}
