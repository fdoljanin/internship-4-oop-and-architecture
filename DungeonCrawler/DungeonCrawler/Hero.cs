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
            var healthPoints = ConsoleHelper.GetNumber("Unesite HP, enter za default:");
            var damage = ConsoleHelper.GetNumber("Unesite damage, enter za default:");
            if (!healthPoints.isDefault) HealthPoints = healthPoints.number;
            if (!damage.isDefault) Damage = damage.number;
        }
        public virtual int Attack()
        {
            return Damage;
        }

        public virtual void Info()
        {
            Console.WriteLine($"--HERO -- Health: {Health}, HP: {HealthPoints}, Damage: {Damage}, XP: {Experience}, Level: {Level}");
        }

        public bool Suffer(int damageSuffered)
        {
            Health -= damageSuffered;
            if (Health <= 0) return  Die();
            return true;
        }
        public virtual bool Die()
        {
            Console.WriteLine("Igrač umro!");
            return false;
        }
        public void Win()
        {
            Console.WriteLine("Pobjeda runde!");

        }

        public virtual ActionType  GetAction()
        {
            var actionInput = ConsoleHelper.CapitalizeWord(ConsoleHelper.GetInput("Odaberite napad (Direct, Side, Counter):").ToLower());
            var success = Enum.TryParse(typeof(ActionType), actionInput, out object action);
            if (!success)
            {
                Console.WriteLine("Odabir nije ispravan!");
                return GetAction();
            }
            return (ActionType)action;
        }

        private void LevelUp()
        {
            Console.WriteLine("Level up!");
            Level++;
            HealthPoints += 2;
            Damage += 1;
        }
        public virtual void Win (int experienceUp)
        {
            Console.WriteLine("Pobjeda runde!");
            Experience += experienceUp;
            if (ConsoleHelper.ConfirmAction($"Želite li utrošiti {Experience / 2} XP za obnovu zdravlja?"))
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
