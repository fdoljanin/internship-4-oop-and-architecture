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
            if (HealthPoints==0) HealthPoints = 12;
            if (Damage==0) Damage = 2;
            Health = HealthPoints;
        }

        private bool _isAttackFurious;
        public override int Attack()
        {
            if (_isAttackFurious)
            {
                Health -= (int) (0.25 * HealthPoints);
                return Damage *= 2;
            }
            return Damage;
        }

        public override  ActionType GetAction()
        {
            var input = ConsoleHelper.GetInput("Odaberite napad (Direct, Side, Counter); 'bijes' za napad iz bijesa, odvojen razmakom:").Split(" ").ToList();
            var actionInput = ConsoleHelper.CapitalizeWord(input[0].ToLower());
            var success = Enum.TryParse(typeof(ActionType), actionInput, out object action);
            if (!success)
            {
                Console.WriteLine("Odabir napada nije ispravan!");
                return GetAction();
            }
            _isAttackFurious = false;
            if (success && input.Count == 1) return (ActionType)action;
            if (input.Count==2 && input[1] == "bijes")
            {
                if (Health<=HealthPoints*0.2)
                {
                    Console.WriteLine("Health nije veći od 20% HP; napad iz bijesa nije moguć.");
                    return GetAction();
                }
                else
                {
                    _isAttackFurious = true;
                    return (ActionType)action;
                }
            }
            
            Console.WriteLine("Unos argumenata neispravan!");
            return GetAction();
        }
    }
}
