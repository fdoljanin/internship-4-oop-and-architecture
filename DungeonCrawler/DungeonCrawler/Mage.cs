using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ConsoleColor = System.ConsoleColor;

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
            if (HealthPoints == 0) HealthPoints = 55;
            if (Damage == 0) Damage = 25;
            Health = HealthPoints;
        }

        public override string ToString()
        {
            ConsoleHelper.ColorWord("[Mana: ", ConsoleColor.DarkYellow);
            Console.Write($"{Mana}/{ManaPoints}");
            ConsoleHelper.ColorWord("] ", ConsoleColor.DarkYellow);
            return base.ToString();
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

        public override (ActionType action, bool doesQuit) GetAction()
        {
            var userInput = ConsoleHelper.GetInputOrQuit("Odaberite napad (Direct, Side, Counter):");
            if (userInput.doesQuit) return (0, true);
            var input = userInput.input.Split(" ").ToList();
            var actionInput = ConsoleHelper.CapitalizeWord(input[0].ToLower());
            var success = Enum.TryParse(typeof(ActionType), actionInput, out object action);
            if (!success)
            {
                ConsoleHelper.ColorText("Odabir napada nije ispravan!", ConsoleColor.Yellow);
                return GetAction();
            }
            _manaUsage = (false, -1);
            if (success && input.Count == 1) return ((ActionType)action, false);
            if (input.Count == 2)
            {
                int mana;
                var successInt = int.TryParse(input[1], out mana);
                if (!successInt)
                {
                    ConsoleHelper.ColorText("Mana mora biti broj!", ConsoleColor.Yellow);
                    return GetAction();
                }
                if (mana <= 0)
                {
                    ConsoleHelper.ColorText("Mana mora biti veća od 0!", ConsoleColor.Yellow);
                    return GetAction();
                }
                if (mana > Mana)
                {
                    ConsoleHelper.ColorText("Tolika količina mane nije dostupna!", ConsoleColor.Yellow);
                    return GetAction();
                }
                _manaUsage = (true, mana);
                return ((ActionType)action, false);
            }
            ConsoleHelper.ColorText("Unos argumenata neispravan!", ConsoleColor.Yellow);
            return GetAction();
        }
    }
}
