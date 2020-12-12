using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DungeonCrawler
{
    class Warrior:Hero
    {
        public Warrior():base()
        {
            if (HealthPoints==0) HealthPoints = 80;
            if (Damage==0) Damage = 15;
            Health = HealthPoints;
        }

        private bool _isAttackFurious;
        public override int Attack()
        {
            if (_isAttackFurious)
            {
                Health -= (int) (0.25 * HealthPoints);
                return Damage * 2;
            }
            return Damage;
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
            _isAttackFurious = false;
            if (success && input.Count == 1) return ((ActionType)action, false);
            if (input.Count==2 && input[1] == "bijes")
            {
                if (Health<=HealthPoints*0.2)
                {
                    ConsoleHelper.ColorText("Health nije veći od 20% HP; napad iz bijesa nije moguć.", ConsoleColor.Yellow);
                    return GetAction();
                }
                
                _isAttackFurious = true;
                return ((ActionType) action, false);
            }
            
            ConsoleHelper.ColorText("Unos argumenata neispravan!", ConsoleColor.Yellow);
            return GetAction();

        }
    }
}
