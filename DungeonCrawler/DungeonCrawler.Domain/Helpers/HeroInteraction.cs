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
            var healthPoints = ConsoleHelper.GetNumber($"Unesite HP, enter za default ({hero.HealthPoints}):");
            var damage = ConsoleHelper.GetNumber($"Unesite damage, enter za default: ({hero.Damage})");
            if (!healthPoints.isDefault) hero.HealthPoints = healthPoints.number;
            if (!damage.isDefault) hero.Damage = damage.number;
        }

        public static (ActionType action, bool doesQuit) GetAction(Hero hero)
        {
            var message = "Odaberite napad(Direct, Side, Counter)";
            if (hero is Warrior) message += ", dodati 'bijes' za napad iz bijesa";
            if (hero is Mage) message += ", dodati broj za korištenje mane";

            var input = ConsoleHelper.GetInputOrQuit(message);
            if (input.doesQuit) return (0, true);

            var inputList = input.input.Split(' ').ToList();
            var actionInput = ConsoleHelper.CapitalizeWord(inputList[0].ToLower());
            var success = Enum.TryParse(actionInput, out ActionType action);

            if (!success)
            {
                ConsoleHelper.ColorText("Odabir akcije nije ispravan!", ConsoleColor.Yellow);
                return GetAction(hero);
            }

            if (inputList.Count == 1) return (action, false);
            if (hero is Warrior warrior)
            {
                if (inputList.Count == 2 && inputList[1].ToLower() == "bijes")
                {
                    if (warrior.Health <= warrior.HealthPoints * 0.2)
                    {
                        ConsoleHelper.ColorText("Health nije veći od 20% HP; napad iz bijesa nije moguć.",
                            ConsoleColor.Yellow);
                        return GetAction(warrior);
                    }

                    warrior.IsAttackFurious = true;
                    return (action, false);
                }
            }

            if (hero is Mage mage)
            {
                mage.ManaUsage = (false, -1);
                if (inputList.Count() == 2)
                {
                    var successInt = int.TryParse(inputList[1], out var mana);
                    if (!successInt)
                    {
                        ConsoleHelper.ColorText("Mana mora biti broj!", ConsoleColor.Yellow);
                        return GetAction(mage);
                    }

                    if (mana <= 0)
                    {
                        ConsoleHelper.ColorText("Mana mora biti veća od 0!", ConsoleColor.Yellow);
                        return GetAction(mage);
                    }

                    if (mana > mage.Mana)
                    {
                        ConsoleHelper.ColorText("Tolika količina mane nije dostupna!", ConsoleColor.Yellow);
                        return GetAction(mage);
                    }

                    mage.ManaUsage = (true, mana);
                }
            }
            ConsoleHelper.ColorText("Unos argumenata neispravan!", ConsoleColor.Yellow);
            return GetAction(hero);
        }

        public static void Win(Hero hero, int experienceUp)
        {
            Console.WriteLine("Pobjeda runde!");
            if (hero is Mage mage) mage.Mana = mage.ManaPoints;
            hero.Experience += experienceUp;
            var newHealth = (hero.Health > 0.75*hero.HealthPoints) ? hero.HealthPoints : (int)(hero.Health + hero.HealthPoints * 0.25);
            var doesRegenerate = ConsoleHelper.ConfirmAction(
                $"Želite li utrošiti {hero.Experience / 2} XP za obnovu zdravlja? [XP: {hero.Experience}, Zdravlje: {newHealth}/{hero.HealthPoints}]");
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
