using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DungeonCrawler
{
    class Mage : Hero
    {
        public int ManaPoints { get; set; } = 5;
        public int Mana { get; set; } = 5;
        private (bool isManaUsed, int quantity) _manaUsage;
        private bool _hasDied = false;
        public Mage() : base()
        {
            if (HealthPoints == 0) HealthPoints = 8;
            if (Damage == 0) Damage = 4;
            Health = HealthPoints;
        }

        public override void Info()
        {
            Console.WriteLine($"--HERO -- Health: {Health}, HP: {HealthPoints}, Damage: {Damage}, Mana: {Mana} XP: {Experience}, Level: {Level}");
        }

        public override int Attack()
        {
            if (_manaUsage.isManaUsed)
            {
                if(Health+_manaUsage.quantity>HealthPoints)
                {
                    Health = HealthPoints;
                    Mana -= HealthPoints - Health;
                } else
                {
                    Health += _manaUsage.quantity;
                    Mana -= _manaUsage.quantity;
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
                Console.WriteLine("Obnavljam se!");
                Health = HealthPoints;
                Mana = ManaPoints;
                return true;
            }
            return false;
        }

        public override  ActionType GetAction()
        {
            var input = ConsoleHelper.GetInput("Odaberite napad (Direct, Side, Counter); broj za manu, odvojen razmakom:").Split(" ").ToList();
            var actionInput = ConsoleHelper.CapitalizeWord(input[0].ToLower());
            var success = Enum.TryParse(typeof(ActionType), actionInput, out object action);
            if (!success)
            {
                Console.WriteLine("Odabir napada nije ispravan!");
                return GetAction();
            }
            _manaUsage = (false, -1);
            if (success && input.Count == 1) return (ActionType)action;
            if (input.Count == 2)
            {
                int mana;
                var successInt = int.TryParse(input[1], out mana);
                if (!successInt)
                {
                    Console.WriteLine("Mana mora biti broj!");
                    return GetAction();
                }
                if (mana <= 0)
                {
                    Console.WriteLine("Mana mora biti veća od 0!");
                    return GetAction();
                }
                if (mana > Mana)
                {
                    Console.WriteLine("Tolika količina mane nije dostupna!");
                    return GetAction();
                }
                _manaUsage = (true, mana);
                return (ActionType)action;
            }
            Console.WriteLine("Unos argumenata neispravan!");
            return GetAction();
        }
    }
}
