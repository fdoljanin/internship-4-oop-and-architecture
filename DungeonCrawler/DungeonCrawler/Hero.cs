using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler
{
    abstract class Hero
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int HealthPoints { get; set; }
        public int Experience { get; set; }
        public int ExperienceLevelUp { get; set; } = 35;
        public int Damage { get; set; }
        public int Health { get; set; }
        public List<Round> Rounds;


        public void SetRounds(List<Round> rounds)
        {
            Rounds = rounds;
        }

        public Hero()
        {
            var name = ConsoleHelper.GetInput("Unesite ime heroja:");
            Name = name;
            var healthPoints = ConsoleHelper.GetNumber($"Unesite HP, enter za default:");
            var damage = ConsoleHelper.GetNumber($"Unesite damage, enter za default:");
            if (!healthPoints.isDefault) HealthPoints = healthPoints.number;
            if (!damage.isDefault) Damage = damage.number;
        }
        public virtual int Attack()
        {
            return Damage;
        }


        public override string ToString()
        {
            ConsoleHelper.ColorWord($"--{Name.ToUpper()}--", ConsoleColor.DarkGray);
            ConsoleHelper.ColorWord($" Zdravlje: ", ConsoleColor.DarkGreen);
            Console.Write(Health);
            ConsoleHelper.ColorWord($" HP: ", ConsoleColor.DarkGreen);
            Console.Write(HealthPoints);
            ConsoleHelper.ColorWord($" Damage: ", ConsoleColor.DarkRed);
            Console.Write(Damage);
            ConsoleHelper.ColorWord($" XP: ", ConsoleColor.DarkCyan);
            Console.Write(Experience);
            ConsoleHelper.ColorWord($" Level: ", ConsoleColor.DarkCyan);
            Console.Write(Level);
            Console.WriteLine("");
            return "";
        }

        public bool Suffer(int damageSuffered)
        {
            Health -= damageSuffered;
            if (Health <= 0) return  Die();
            return true;
        }
        public virtual bool Die()
        {
            return false;
        }
        public void Win()
        {
            Console.WriteLine("Pobjeda runde!");

        }

        public virtual (ActionType action, bool doesQuit) GetAction()
        {
            var input = ConsoleHelper.GetInputOrQuit("Odaberite napad (Direct, Side, Counter):");
            if (input.doesQuit) return (0, true);

            var actionInput = ConsoleHelper.CapitalizeWord(input.input.ToLower());
            var success = Enum.TryParse(typeof(ActionType), actionInput, out object action);
            if (!success)
            {
                ConsoleHelper.ColorText("Odabir nije ispravan!", ConsoleColor.Yellow);
                return GetAction();
            }
            return ((ActionType)action, false);
        }

        private void LevelUp()
        {
            ConsoleHelper.ColorText("Level up!", ConsoleColor.White, ConsoleColor.DarkGreen);
            Level++;
            HealthPoints += 10;
            Damage = (int)(Damage*1.3);
        }
        public virtual void Win (int experienceUp)
        {
            Console.WriteLine("Pobjeda runde!");
            Experience += experienceUp;
            if (ConsoleHelper.ConfirmAction($"Želite li utrošiti {Experience / 2} XP za obnovu zdravlja? [XP: {Experience}, Zdravlje: {Health}/{HealthPoints}]"))
            {
                Experience -= Experience / 2;
                Health = HealthPoints;
            }
            if (Experience >= ExperienceLevelUp)
            {
                Experience -= ExperienceLevelUp;
                LevelUp();
            }
            Health = (Health + (int)(HealthPoints * 0.25) > HealthPoints) ? HealthPoints : Health + (int)(HealthPoints * 0.25);
        }
    }
}
