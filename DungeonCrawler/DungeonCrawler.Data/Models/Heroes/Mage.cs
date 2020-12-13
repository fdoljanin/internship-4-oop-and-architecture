using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ConsoleColor = System.ConsoleColor;

namespace DungeonCrawler.Data.Models.Heroes
{
    public class Mage : Hero
    {
        public int ManaPoints { get; set; } = 15;
        public int Mana { get; set; } = 15;
        public (bool isManaUsed, int quantity) ManaUsage;
        private bool _hasDied = false;
        public Mage()
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
                    Mana -= HealthPoints - Health;
                    Health = HealthPoints;
                } else
                {
                    Health += ManaUsage.quantity;
                    Mana -= ManaUsage.quantity;
                }
                return 0;
            }
            return Damage;
        }

        protected override bool Die()
        {
            if (_hasDied) return false;
            Console.WriteLine("Respawn...");
            _hasDied = true;
            Health = HealthPoints;
            Mana = ManaPoints;
            return true;
        }

        
    }
}
