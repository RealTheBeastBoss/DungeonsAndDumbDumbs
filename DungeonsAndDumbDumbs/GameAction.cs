using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDumbDumbs
{
    class GameAction
    {
        public enum ActionType
        {
            ANY,
            ATTACK,
            NOTATTACK
        }
        public Action<LivingCreature, List<string>> actionMethod;
        public ActionType actionType;
        public GameAction(Action<LivingCreature, List<string>> action, ActionType type = ActionType.ANY)
        {
            actionType = type;
            actionMethod = action;
        }
        public static void UseMagic(LivingCreature creature, List<string> response)
        {
            if (creature == Program.player)
            {
                if (response.Contains("cast"))
                {
                    Program.foundAction = true;
                    if (creature.usedSpellThisAction && Program.currentlyBonusAction)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Clear();
                        Console.WriteLine("You cannot use magic as a bonus action if you used a spell already this turn.");
                        return;
                    }
                    if (Program.currentlyBonusAction)
                    {
                        int nonBonusAction = 0;
                        foreach (Spell spell in creature.knownSpells)
                        {
                            if (spell.spellType != Spell.Type.BONUS) nonBonusAction++;
                        }
                        if (nonBonusAction == creature.knownSpells.Count)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Clear();
                            Console.WriteLine("You do not have any spells that can be used as a bonus action.");
                            return;
                        }
                    }
                    if (creature.cantrips.Count == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Clear();
                        Console.WriteLine("You do not have any magic to cast.");
                        return;
                    }
                    Spell chosenSpell = null;
                    foreach (Spell spell in creature.cantrips)
                    {
                        if (response.Contains(spell.spellName.ToLower()) && !Program.currentlyBonusAction)
                        {
                            chosenSpell = spell;
                            goto GotChosenSpell;
                        }
                    }
                    foreach (Spell spell in creature.knownSpells)
                    {
                        if (response.Contains(spell.spellName.ToLower()) && spell.spellType != Spell.Type.REACTION)
                        {
                            if ((spell.spellType == Spell.Type.ACTION && !Program.currentlyBonusAction) || (spell.spellType == Spell.Type.BONUS && Program.currentlyBonusAction))
                            {
                                chosenSpell = spell;
                                goto GotChosenSpell;
                            }
                        }
                    }
                    while (true)
                    {
                        Console.Clear();
                        if (!Program.currentlyBonusAction)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Cantrips:\n");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            foreach (Spell spell in creature.cantrips)
                            {
                                Console.WriteLine(spell.spellName);
                            }
                        }
                        if (creature.knownSpells.Count != 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\n1st Level Spells:\n");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            foreach (Spell spell in creature.knownSpells)
                            {
                                if (spell.spellLevel == 1 && spell.spellType != Spell.Type.REACTION && (spell.spellType == Spell.Type.ACTION && !Program.currentlyBonusAction) || 
                                    (spell.spellType == Spell.Type.BONUS && Program.currentlyBonusAction)) Console.WriteLine(spell.spellName);
                            }
                        } // TODO: Add the other Spell Levels
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\nWhich spell do you want to cast?: ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        string choice = Console.ReadLine().ToLower();
                        if (choice == "cancel") return;
                        foreach (Spell spell in creature.cantrips)
                        {
                            if (choice == spell.spellName.ToLower())
                            {
                                if (spell.allowedStates.Contains(Program.currentState))
                                {
                                    chosenSpell = spell;
                                    goto GotChosenSpell;
                                } else
                                {
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    Console.WriteLine("\nYou are not currently in the correct situation for this spell.\n");
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.Write("Press Enter to Continue: ");
                                    Console.ReadLine();
                                }
                            }
                        }
                        foreach (Spell spell in creature.knownSpells)
                        {
                            if (choice == spell.spellName.ToLower() && spell.spellType != Spell.Type.REACTION && (spell.spellType == Spell.Type.ACTION && !Program.currentlyBonusAction) || 
                                (spell.spellType == Spell.Type.BONUS && Program.currentlyBonusAction))
                            {
                                if (spell.allowedStates.Contains(Program.currentState))
                                {
                                    chosenSpell = spell;
                                    goto GotChosenSpell;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    Console.WriteLine("\nYou are not currently in the correct situation for this spell.\n");
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.Write("Press Enter to Continue: ");
                                    Console.ReadLine();
                                }
                            }
                        }
                    }
                GotChosenSpell:
                    int castLevel = 0;
                    if (chosenSpell.spellLevel == 0)
                    {
                        Console.Clear();
                        creature.usedSpellThisAction = true;
                        chosenSpell.castSpell(castLevel, creature);
                        return;
                    }
                    if (chosenSpell.isRitual && creature.canRitual)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Clear();
                        Console.WriteLine("This spell can be cast as a ritual, would you like to use it as such?: ");
                        if (Program.CheckConfirmation())
                        {
                            Console.Clear();
                            creature.usedSpellThisAction = true;
                            chosenSpell.castSpell(chosenSpell.spellLevel, creature);
                            return;
                        }
                    }
                    while (castLevel == 0)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"The chosen spell is a level {chosenSpell.spellLevel} spell.");
                        if (chosenSpell.spellLevel == 1 && creature.firstLevelSlotsRemaining > 0)
                        {
                            Console.WriteLine($"\nFirst Level Spell Slots: {creature.firstLevelSlotsRemaining}");
                        }
                        if (chosenSpell.spellLevel > 1 && creature.secondLevelSlotsRemaining > 0)
                        {
                            Console.WriteLine($"\nSecond Level Spell Slots: {creature.secondLevelSlotsRemaining}");
                        }
                        if (chosenSpell.spellLevel > 2 && creature.thirdLevelSlotsRemaining > 0)
                        {
                            Console.WriteLine($"\nThird Level Spell Slots: {creature.thirdLevelSlotsRemaining}");
                        }
                        if (chosenSpell.spellLevel > 3 && creature.fourthLevelSlotsRemaining > 0)
                        {
                            Console.WriteLine($"\nFourth Level Spell Slots: {creature.fourthLevelSlotsRemaining}");
                        }
                        if (chosenSpell.spellLevel > 4 && creature.fifthLevelSlotsRemaining > 0)
                        {
                            Console.WriteLine($"\nFifth Level Spell Slots: {creature.fifthLevelSlotsRemaining}");
                        }
                        if (chosenSpell.spellLevel > 5 && creature.sixthLevelSlotsRemaining > 0)
                        {
                            Console.WriteLine($"\nSixth Level Spell Slots: {creature.sixthLevelSlotsRemaining}");
                        }
                        if (chosenSpell.spellLevel > 6 && creature.seventhLevelSlotsRemaining > 0)
                        {
                            Console.WriteLine($"\nSeventh Level Spell Slots: {creature.seventhLevelSlotsRemaining}");
                        }
                        if (chosenSpell.spellLevel > 7 && creature.eighthLevelSlotsRemaining > 0)
                        {
                            Console.WriteLine($"\nEighth Level Spell Slots: {creature.eighthLevelSlotsRemaining}");
                        }
                        if (chosenSpell.spellLevel > 8 && creature.ninthLevelSlotsRemaining > 0)
                        {
                            Console.WriteLine($"\nNinth Level Spell Slots: {creature.ninthLevelSlotsRemaining}");
                        }
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\nWhat level do you want to cast the spell at?: ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        string answer = Console.ReadLine().ToLower();
                        if (answer == "cancel") return;
                        switch (answer)
                        {
                            case "first":
                            case "1st":
                            case "1":
                                if (chosenSpell.spellLevel == 1 && creature.firstLevelSlotsRemaining > 0)
                                {
                                    castLevel = 1;
                                    creature.firstLevelSlotsRemaining--;
                                }
                                break;
                            case "second":
                            case "2nd":
                            case "2":
                                if (chosenSpell.spellLevel > 1 && creature.secondLevelSlotsRemaining > 0)
                                {
                                    castLevel = 2;
                                    creature.secondLevelSlotsRemaining--;
                                }
                                break;
                            case "third":
                            case "3rd":
                            case "3":
                                if (chosenSpell.spellLevel > 2 && creature.thirdLevelSlotsRemaining > 0)
                                {
                                    castLevel = 3;
                                    creature.thirdLevelSlotsRemaining--;
                                }
                                break;
                            case "fourth":
                            case "4th":
                            case "4":
                                if (chosenSpell.spellLevel > 3 && creature.fourthLevelSlotsRemaining > 0)
                                {
                                    castLevel = 4;
                                    creature.fourthLevelSlotsRemaining--;
                                }
                                break;
                            case "fifth":
                            case "5th":
                            case "5":
                                if (chosenSpell.spellLevel > 4 && creature.fifthLevelSlotsRemaining > 0)
                                {
                                    castLevel = 5;
                                    creature.fifthLevelSlotsRemaining--;
                                }
                                break;
                            case "sixth":
                            case "6th":
                            case "6":
                                if (chosenSpell.spellLevel > 5 && creature.sixthLevelSlotsRemaining > 0)
                                {
                                    castLevel = 6;
                                    creature.sixthLevelSlotsRemaining--;
                                }
                                break;
                            case "seventh":
                            case "7th":
                            case "7":
                                if (chosenSpell.spellLevel > 6 && creature.seventhLevelSlotsRemaining > 0)
                                {
                                    castLevel = 7;
                                    creature.seventhLevelSlotsRemaining--;
                                }
                                break;
                            case "eighth":
                            case "8th":
                            case "8":
                                if (chosenSpell.spellLevel > 7 && creature.eighthLevelSlotsRemaining > 0)
                                {
                                    castLevel = 8;
                                    creature.eighthLevelSlotsRemaining--;
                                }
                                break;
                            case "ninth":
                            case "9th":
                            case "9":
                                if (chosenSpell.spellLevel > 8 && creature.ninthLevelSlotsRemaining > 0)
                                {
                                    castLevel = 9;
                                    creature.ninthLevelSlotsRemaining--;
                                }
                                break;
                        }
                    }
                    Console.Clear();
                    creature.usedSpellThisAction = true;
                    chosenSpell.castSpell(castLevel, creature);
                }
            } else
            {

            }
        }
        public static void ShowCharacterSheet(LivingCreature creature, List<string> response)
        {
            if (response.Contains("cs") || (response.Contains("character") && response.Contains("sheet")))
            {
                Player player = (Player)creature;
                player.ShowCharacterSheet();
                Program.foundAction = true;
            }
        }
        public static void ShowInventory(LivingCreature creature, List<string> response)
        {
            if (response.Contains("i") || response.Contains("inventory"))
            {
                Program.foundAction = true;
                Console.Clear();
                Console.WriteLine($"Inventory of {creature.name}:");
                Player player = (Player)creature;
                player.inventory.Sort(Program.SortInventory);
                int skipAmount = 0;
                foreach (Equipment equipment in player.inventory)
                {
                    if (skipAmount > 0)
                    {
                        skipAmount--;
                        continue;
                    }
                    int valueCount = 0;
                    foreach (Equipment check in player.inventory)
                    {
                        if (equipment == check) valueCount++;
                    }
                    Console.Write("\n" + equipment.equipmentName);
                    if (valueCount > 1)
                    {
                        Console.Write($" x{valueCount}");
                        skipAmount = valueCount - 1;
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
