﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ConsoleColor = System.ConsoleColor;

namespace DungeonCrawler
{
    public class Mage : Hero
    {
        public int ManaPoints { get; set; } = 5;
        public int Mana { get; set; } = 5;
        public (bool isManaUsed, int quantity) ManaUsage;
        private bool _hasDied = false;
        public Mage() : base()
        {
            HealthPoints = 55;
            Damage = 25;
            Health = HealthPoints;
        }

        public override int Attack()
        {
            if (ManaUsage.isManaUsed)
            {
                if(Health+ManaUsage.quantity>HealthPoints)
                {
                    Health = HealthPoints;
                    Mana -= HealthPoints - Health;
                } else
                {
                    Health += ManaUsage.quantity;
                    Mana -= ManaUsage.quantity;
                }
                return 0;
            }
            return Damage;
        }

        public override bool Die()
        {
            if (!_hasDied)
            {
                _hasDied = true;
                Health = HealthPoints;
                Mana = ManaPoints;
                return true;
            }
            return false;
        }

        
    }
}
