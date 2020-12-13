using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DungeonCrawler.Data.Enums;
using DungeonCrawler.Data.Models.Heroes;

namespace DungeonCrawler.Domain.Helpers
{
    public static class HeroInteraction
    {
        public static void ConfigureHero(Hero hero)
        {
            var name = ConsoleHelper.GetInput("Unesite ime heroja:");
            hero.Name = name;
            var healthPoints = ConsoleHelper.GetNumber($"Unesite HP, enter za default:");
            var damage = ConsoleHelper.GetNumber($"Unesite damage, enter za default:");
            if (!healthPoints.isDefault) hero.HealthPoints = healthPoints.number;
            if (!damage.isDefault) hero.Damage = damage.number;
        }

        public static (ActionType action, bool doesQuit) GetAction(Hero hero)
        {
            if (hero is Warrior warrior) return GetWarriorAction(warrior);
            if (hero is Mage mage) return GetMageAction(mage);
            return GetRangerAction();
        }

        public static (ActionType action, bool doesQuit) GetRangerAction()
        {
            var input = ConsoleHelper.GetInputOrQuit("Odaberite napad (Direct, Side, Counter):");
            if (input.doesQuit) return (0, true);

            var actionInput = ConsoleHelper.CapitalizeWord(input.input.ToLower());
            var success = Enum.TryParse(actionInput, out ActionType action);
            if (!success)
            {
                ConsoleHelper.ColorText("Odabir nije ispravan!", ConsoleColor.Yellow);
                return GetRangerAction();
            }
            return ((ActionType)action, false);
        }

        public static (ActionType action, bool doesQuit) GetWarriorAction(Warrior warrior)
        {
            var userInput = ConsoleHelper.GetInputOrQuit("Odaberite napad (Direct, Side, Counter) + bijes:");
            if (userInput.doesQuit) return (0, true);
            var input = userInput.input.Split(' ').ToList();
            var actionInput = ConsoleHelper.CapitalizeWord(input[0].ToLower());
            var success = Enum.TryParse(actionInput, out ActionType action);
            if (!success)
            {
                ConsoleHelper.ColorText("Odabir napada nije ispravan!", ConsoleColor.Yellow);
                return GetWarriorAction(warrior);
            }
            warrior.IsAttackFurious = false;
            if (success && input.Count == 1) return ((ActionType)action, false);
            if (input.Count == 2 && input[1] == "bijes")
            {
                if (warrior.Health <= warrior.HealthPoints * 0.2)
                {
                    ConsoleHelper.ColorText("Health nije veći od 20% HP; napad iz bijesa nije moguć.", ConsoleColor.Yellow);
                    return GetWarriorAction(warrior);
                }

                warrior.IsAttackFurious = true;
                return ((ActionType)action, false);
            }

            ConsoleHelper.ColorText("Unos argumenata neispravan!", ConsoleColor.Yellow);
            return GetWarriorAction(warrior);

        }

        public static (ActionType action, bool doesQuit) GetMageAction(Mage mage)
        {
            var userInput = ConsoleHelper.GetInputOrQuit("Odaberite napad (Direct, Side, Counter):");
            if (userInput.doesQuit) return (0, true);
            var input = userInput.input.Split(' ').ToList();
            var actionInput = ConsoleHelper.CapitalizeWord(input[0].ToLower());
            var success = Enum.TryParse(actionInput, out ActionType action);
            if (!success)
            {
                ConsoleHelper.ColorText("Odabir napada nije ispravan!", ConsoleColor.Yellow);
                return GetMageAction(mage);
            }
            mage.ManaUsage = (false, -1);
            if (success && input.Count == 1) return ((ActionType)action, false);
            if (input.Count == 2)
            {
                int mana;
                var successInt = int.TryParse(input[1], out mana);
                if (!successInt)
                {
                    ConsoleHelper.ColorText("Mana mora biti broj!", ConsoleColor.Yellow);
                    return GetMageAction(mage);
                }
                if (mana <= 0)
                {
                    ConsoleHelper.ColorText("Mana mora biti veća od 0!", ConsoleColor.Yellow);
                    return GetMageAction(mage);
                }
                if (mana > mage.Mana)
                {
                    ConsoleHelper.ColorText("Tolika količina mane nije dostupna!", ConsoleColor.Yellow);
                    return GetMageAction(mage);
                }
                mage.ManaUsage = (true, mana);
                return ((ActionType)action, false);
            }
            ConsoleHelper.ColorText("Unos argumenata neispravan!", ConsoleColor.Yellow);
            return GetMageAction(mage);
        }

        public static void Win(Hero hero, int experienceUp)
        {
            Console.WriteLine("Pobjeda runde!");
            if (hero is Mage mage) mage.Mana = mage.ManaPoints;
            hero.Experience += experienceUp;
            var doesRegenerate = ConsoleHelper.ConfirmAction(
                $"Želite li utrošiti {hero.Experience / 2} XP za obnovu zdravlja? [XP: {hero.Experience}, Zdravlje: {hero.Health}/{hero.HealthPoints}]");
            if (hero.WinCheckLevelUp(doesRegenerate))
                    ConsoleHelper.ColorText("Level up!", ConsoleColor.White, ConsoleColor.DarkGreen);
        }

        public static void HeroInfo(Hero hero)
        {
            ConsoleHelper.ColorWord($"--{hero}--", ConsoleColor.DarkGray);
            ConsoleHelper.ColorWord($" Zdravlje: ", ConsoleColor.DarkGreen);
            Console.Write(hero.Health);
            ConsoleHelper.ColorWord($" HP: ", ConsoleColor.DarkGreen);
            Console.Write(hero.HealthPoints);
            ConsoleHelper.ColorWord($" Damage: ", ConsoleColor.DarkRed);
            Console.Write(hero.Damage);
            ConsoleHelper.ColorWord($" XP: ", ConsoleColor.DarkCyan);
            Console.Write(hero.Experience);
            ConsoleHelper.ColorWord($" Level: ", ConsoleColor.DarkCyan);
            Console.Write(hero.Level);
            if (hero is Mage mage)
            {
                ConsoleHelper.ColorWord(" Mana: ", ConsoleColor.DarkYellow);
                Console.Write($"{mage.Mana}/{mage.ManaPoints}");
            }
            Console.WriteLine("");
        }



    }
}
