using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDumbDumbs
{
    class Class
    {
        public string className;
        public string primaryAbility;
        public int diceRollLastLevel;
        public string classDescription;
        public int classLevel = 1;
        public virtual void PlayerCreation()
        {

        }
        public virtual Tuple<int, int> CalculateHitDice()
        {
            return new Tuple<int, int>(classLevel, 8);
        }
        public virtual int CalculateHitPoints()
        {
            if (classLevel == 1)
            {
                return 8 + Program.GetAbilityModifier(Program.player, "Constitution");
            }
            else
            {
                return diceRollLastLevel + (Program.GetAbilityModifier(Program.player, "Constitution") * (classLevel - 1));
            }
        }
        public virtual int CalculateArmourClass()
        {
            return 10 + Program.GetAbilityModifier(Program.player, "Dexterity");
        }
        public virtual void IncreaseLevel(int diceSize = 8)
        {
            diceRollLastLevel = Program.RollDice(false, new Tuple<int, int>(1, diceSize)).First();
        }
    }
    class Barbarian : Class
    {
        public int timesRagedSinceRest = 0;
        public int totalRagesPerRest = 2;
        public int rageDamage = 2;
        public Barbarian()
        {
            className = "Barbarian";
            classDescription = "A fierce warrior who can enter a battle rage.";
            primaryAbility = "Strength";
        }
        public override void PlayerCreation()
        {
            Program.AddProficiency(Program.player, "Light Armour");
            Program.AddProficiency(Program.player, "Medium Armour");
            Program.AddProficiency(Program.player, "Shields");
            Program.AddProficiency(Program.player, "Simple Weapons");
            Program.AddProficiency(Program.player, "Martial Weapons");
            Program.AddProficiency(Program.player, "Strength");
            Program.AddProficiency(Program.player, "Constitution");
            int selectedSkills = 0;
            List<Program.Skill> allowedSkills = new List<Program.Skill>() { Program.Skill.ANIMALS, Program.Skill.ATHLETICS, Program.Skill.INTIMIDATION, Program.Skill.NATURE, 
                Program.Skill.PERCEPTION, Program.Skill.SURVIVAL };
            while (selectedSkills < 2)
            {
                Console.Clear();
                foreach (Program.Skill skill in allowedSkills)
                {
                    if (Program.player.proficiencies.Contains(Program.GetDescription(skill))) continue;
                    Console.WriteLine(Program.GetDescription(skill));
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nWhich of these skills do you want to be proficient in?: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string response = Console.ReadLine().ToLower();
                foreach (Program.Skill skill in allowedSkills)
                {
                    if (!Program.player.proficiencies.Contains(Program.GetDescription(skill)) && response == Program.GetDescription(skill).ToLower())
                    {
                        Program.AddProficiency(Program.player, Program.GetDescription(skill));
                        allowedSkills.Remove(skill);
                        selectedSkills++;
                        break;
                    }
                }
            }
            NotDecidedStartingGear:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Do you want to start with gold or equipment?: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            string answer = Console.ReadLine().ToLower();
            if (answer == "gold")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Determining Starting Gold: Rolling 2 D4s...\n");
                List<int> diceRolled = Program.RollDice(true, new Tuple<int, int>(2, 4));
                Program.player.gold = diceRolled.Sum() * 10;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nYou will begin with a total of {Program.player.gold} gp.");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nPress Enter to Continue: ");
                Console.ReadLine();
            } else if (answer == "equipment")
            {
                bool selectedMartialMeleeWeapon = false;
                while (!selectedMartialMeleeWeapon)
                {
                    Console.Clear();
                    foreach (Weapon weapon in Program.allMartialMeleeWeapons)
                    {
                        Console.WriteLine(weapon.equipmentName);
                    }
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\nWhich weapon do you wish to own?: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string choice = Console.ReadLine().ToLower();
                    foreach (Weapon weapon in Program.allMartialMeleeWeapons)
                    {
                        if (choice == weapon.equipmentName.ToLower())
                        {
                            Program.player.inventory.Add(weapon);
                            selectedMartialMeleeWeapon = true;
                            break;
                        }
                    }
                }
                bool selectedSimpleWeapon = false;
                while (!selectedSimpleWeapon)
                {
                    Console.Clear();
                    foreach (Weapon weapon in Program.allSimpleWeapons)
                    {
                        if (weapon != Program.handAxe)
                        {
                            Console.WriteLine(weapon.equipmentName);
                        } else
                        {
                            Console.WriteLine("2 Hand Axes");
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\nWhich weapon do you want to have?: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string choice = Console.ReadLine().ToLower();
                    foreach (Weapon weapon in Program.allSimpleWeapons)
                    {
                        if (choice == weapon.equipmentName.ToLower())
                        {
                            Program.player.inventory.Add(weapon);
                            selectedSimpleWeapon = true;
                            if (weapon == Program.handAxe)
                            {
                                Program.player.inventory.Add(weapon);
                            }
                            break;
                        }
                    }
                }
                for (int i = 0; i < 10; i++)
                {
                    Program.player.inventory.Add(Program.javelin);
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nYou also gained 10 Javelins");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nPress Enter to Continue: ");
                Console.ReadLine();
            } else
            {
                goto NotDecidedStartingGear;
            }
        }
        public override Tuple<int, int> CalculateHitDice()
        {
            return new Tuple<int, int>(classLevel, 12);
        }
        public override int CalculateHitPoints()
        {
            if (classLevel == 1)
            {
                return 12 + Program.GetAbilityModifier(Program.player, "Constitution");
            } else
            {
                return diceRollLastLevel + (Program.GetAbilityModifier(Program.player, "Constitution") * (classLevel - 1));
            }
        }
        public override int CalculateArmourClass()
        {
            return 10 + Program.GetAbilityModifier(Program.player, "Constitution") + Program.GetAbilityModifier(Program.player, "Dexterity"); // TODO: Only when not wearing any armour
        }
    }
    class Bard : Class
    {
        public int inspirationDie = 6;
        public Bard()
        {
            className = "Bard";
            classDescription = "An inspiring magician whose power echoes the music of creation.";
            primaryAbility = "Charisma";
        }
        public override void PlayerCreation()
        {
            Program.AddProficiency(Program.player, "Light Armour");
            Program.AddProficiency(Program.player, "Simple Weapons");
            Program.AddProficiency(Program.player, "Hand Crossbows");
            Program.AddProficiency(Program.player, "Longswords");
            Program.AddProficiency(Program.player, "Rapiers");
            Program.AddProficiency(Program.player, "Dexterity");
            Program.AddProficiency(Program.player, "Charisma");
            int selectedSkills = 0;
            while (selectedSkills < 3)
            {
                Console.Clear();
                foreach (Tuple<Program.Skill, string> skill in Program.allSkillsAbilities)
                {
                    if (Program.player.proficiencies.Contains(Program.GetDescription(skill.Item1))) continue;
                    Console.WriteLine(Program.GetDescription(skill.Item1));
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nWhich of these skills do you want to be proficient in?: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string response = Console.ReadLine().ToLower();
                foreach (Tuple<Program.Skill, string> skill in Program.allSkillsAbilities)
                {
                    if (!Program.player.proficiencies.Contains(Program.GetDescription(skill.Item1)) && response == Program.GetDescription(skill.Item1).ToLower())
                    {
                        Program.player.proficiencies.Add(Program.GetDescription(skill.Item1));
                        selectedSkills++;
                        break;
                    }
                }
            }
            int selectedCantrips = 0;
            while (selectedCantrips < 2)
            {
                Console.Clear();
                foreach (Spell spell in Program.bardSpells)
                {
                    if (spell.spellLevel == 0 && !Program.player.cantrips.Contains(spell))
                    {
                        Console.WriteLine(spell.spellName);
                    }
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nWhich of these Cantrips do you want to know?: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string response = Console.ReadLine().ToLower();
                foreach (Spell spell in Program.bardSpells)
                {
                    if (spell.spellLevel == 0 && !Program.player.cantrips.Contains(spell) && response == spell.spellName.ToLower())
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nYou have selected \"{spell.spellName}\", here is some information on the spell:\n\n{spell.spellDescription}\n");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write($"Do you want to have {spell.spellName} as your Cantrip?: ");
                        if (Program.CheckConfirmation())
                        {
                            Program.player.cantrips.Add(spell);
                            selectedCantrips++;
                            break;
                        }
                    }
                }
            }
            int selectedSpells = 0;
            while (selectedSpells < 4)
            {
                Console.Clear();
                foreach (Spell spell in Program.bardSpells)
                {
                    if (spell.spellLevel == 1 && !Program.player.knownSpells.Contains(spell))
                    {
                        Console.WriteLine(spell.spellName);
                    }
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nWhich of these Spells do you want to know?: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string response = Console.ReadLine().ToLower();
                foreach (Spell spell in Program.bardSpells)
                {
                    if (spell.spellLevel == 1 && !Program.player.knownSpells.Contains(spell) && response == spell.spellName.ToLower())
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nYou have selected \"{spell.spellName}\", here is some information on the spell:\n\n{spell.spellDescription}\n");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write($"Do you want to have {spell.spellName} as your Spell?: ");
                        if (Program.CheckConfirmation())
                        {
                            Program.player.knownSpells.Add(spell);
                            selectedSpells++;
                            break;
                        }
                    }
                }
            }
        NotDecidedStartingGear:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Do you want to start with gold or equipment?: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            string answer = Console.ReadLine().ToLower();
            if (answer == "gold")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Determining Starting Gold: Rolling 5 D4s...\n");
                List<int> diceRolled = Program.RollDice(true, new Tuple<int, int>(5, 4));
                Program.player.gold = diceRolled.Sum() * 10;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nYou will begin with a total of {Program.player.gold} gp.");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nPress Enter to Continue: ");
                Console.ReadLine();
            }
            else if (answer == "equipment")
            {
                bool selectedSimpleWeapon = false;
                while (!selectedSimpleWeapon)
                {
                    Console.Clear();
                    foreach (Weapon weapon in Program.allSimpleWeapons)
                    {
                        Console.WriteLine(weapon.equipmentName);
                    }
                    Console.WriteLine(Program.rapier.equipmentName);
                    Console.WriteLine(Program.longSword.equipmentName);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\nWhich weapon would you want to have?: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string choice = Console.ReadLine().ToLower();
                    if (choice == Program.rapier.equipmentName.ToLower())
                    {
                        Program.player.inventory.Add(Program.rapier);
                        break;
                    } else if (choice == Program.longSword.equipmentName.ToLower())
                    {
                        Program.player.inventory.Add(Program.longSword);
                        break;
                    } else
                    {
                        foreach (Weapon weapon in Program.allSimpleWeapons)
                        {
                            if (choice == weapon.equipmentName.ToLower())
                            {
                                Program.player.inventory.Add(weapon);
                                selectedSimpleWeapon = true;
                            }
                        }
                    }
                }
                Program.player.inventory.Add(Program.leather);
                Program.player.inventory.Add(Program.dagger);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nYou also gained Leather Armour and a Dagger");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nPress Enter to Continue: ");
                Console.ReadLine();
            }
            else
            {
                goto NotDecidedStartingGear;
            }
        }
    }
    class Cleric : Class
    {
        public Cleric()
        {
            className = "Cleric";
            classDescription = "A priestly champion who wields divine magic in service of a higher power.";
            primaryAbility = "Wisdom";
        }
        public override void PlayerCreation()
        {
            Program.AddProficiency(Program.player, "Light Armour");
            Program.AddProficiency(Program.player, "Medium Armour");
            Program.AddProficiency(Program.player, "Heavy Armour");
            Program.AddProficiency(Program.player, "Shields");
            Program.AddProficiency(Program.player, "Simple Weapons");
            Program.AddProficiency(Program.player, "Wisdom");
            Program.AddProficiency(Program.player, "Charisma");
            int selectedSkills = 0;
            List<Program.Skill> allowedSkills = new List<Program.Skill>() { Program.Skill.HISTORY, Program.Skill.INSIGHT, Program.Skill.MEDICINE, Program.Skill.PERSUASION, Program.Skill.RELIGION };
            while (selectedSkills < 2)
            {
                Console.Clear();
                foreach (Program.Skill skill in allowedSkills)
                {
                    if (Program.player.proficiencies.Contains(Program.GetDescription(skill))) continue;
                    Console.WriteLine(Program.GetDescription(skill));
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nWhich of these skills do you want to be proficient in?: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string response = Console.ReadLine().ToLower();
                foreach (Program.Skill skill in allowedSkills)
                {
                    if (!Program.player.proficiencies.Contains(Program.GetDescription(skill)) && response == Program.GetDescription(skill).ToLower())
                    {
                        Program.AddProficiency(Program.player, Program.GetDescription(skill));
                        allowedSkills.Remove(skill);
                        selectedSkills++;
                        break;
                    }
                }
            }
            int selectedCantrips = 0;
            while (selectedCantrips < 3)
            {
                Console.Clear();
                foreach (Spell spell in Program.clericSpells)
                {
                    if (spell.spellLevel == 0 && !Program.player.cantrips.Contains(spell))
                    {
                        Console.WriteLine(spell.spellName);
                    }
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nWhich of these Cantrips do you want to know?: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string response = Console.ReadLine().ToLower();
                foreach (Spell spell in Program.clericSpells)
                {
                    if (spell.spellLevel == 0 && !Program.player.cantrips.Contains(spell) && response == spell.spellName.ToLower())
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nYou have selected \"{spell.spellName}\", here is some information on the spell:\n\n{spell.spellDescription}\n");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write($"Do you want to have {spell.spellName} as your Cantrip?: ");
                        if (Program.CheckConfirmation())
                        {
                            Program.player.cantrips.Add(spell);
                            selectedCantrips++;
                            break;
                        }
                    }
                }
            }
            Program.player.knownSpells.Add(Program.bless);
            Program.player.knownSpells.Add(Program.cureWounds);
            int selectedSpells = 0;
            while (selectedSpells < 3)
            {
                Console.Clear();
                foreach (Spell spell in Program.clericSpells)
                {
                    if (spell.spellLevel == 1 && !Program.player.knownSpells.Contains(spell))
                    {
                        Console.WriteLine(spell.spellName);
                    }
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nWhich of these Spells do you want to know?: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                string response = Console.ReadLine().ToLower();
                foreach (Spell spell in Program.clericSpells)
                {
                    if (spell.spellLevel == 1 && !Program.player.knownSpells.Contains(spell) && response == spell.spellName.ToLower())
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nYou have selected \"{spell.spellName}\", here is some information on the spell:\n\n{spell.spellDescription}\n");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write($"Do you want to have {spell.spellName} as your Spell?: ");
                        if (Program.CheckConfirmation())
                        {
                            Program.player.knownSpells.Add(spell);
                            selectedSpells++;
                            break;
                        }
                    }
                }
            }
        NotDecidedStartingGear:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Do you want to start with gold or equipment?: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            string answer = Console.ReadLine().ToLower();
            if (answer == "gold")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Determining Starting Gold: Rolling 5 D4s...\n");
                List<int> diceRolled = Program.RollDice(true, new Tuple<int, int>(5, 4));
                Program.player.gold = diceRolled.Sum() * 10;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nYou will begin with a total of {Program.player.gold} gp.");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nPress Enter to Continue: ");
                Console.ReadLine();
            } else if (answer == "equipment")
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine(Program.scaleMail.equipmentName);
                    Console.WriteLine(Program.leather.equipmentName);
                    Console.WriteLine(Program.chainMail.equipmentName);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\nWhich armour do you want to have?: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string choice = Console.ReadLine().ToLower();
                    if (choice == Program.scaleMail.equipmentName.ToLower())
                    {
                        Program.player.inventory.Add(Program.scaleMail);
                        break;
                    } else if (choice == Program.leather.equipmentName.ToLower())
                    {
                        Program.player.inventory.Add(Program.leather);
                        break;
                    } else if (choice == Program.chainMail.equipmentName.ToLower())
                    {
                        Program.player.inventory.Add(Program.chainMail);
                        break;
                    }
                }
                bool selectedSimpleWeapon = false;
                while (!selectedSimpleWeapon)
                {
                    Console.Clear();
                    foreach (Weapon weapon in Program.allSimpleWeapons)
                    {
                        if (weapon == Program.lightCrossbow)
                        {
                            Console.WriteLine($"{weapon.equipmentName} + 20 Crossbow Bolts");
                        } else
                        {
                            Console.WriteLine(weapon.equipmentName);
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\nWhich weapon do you want to have?: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string choice = Console.ReadLine().ToLower();
                    foreach (Weapon weapon in Program.allSimpleWeapons)
                    {
                        if (choice == weapon.equipmentName.ToLower())
                        {
                            Program.player.inventory.Add(weapon);
                            selectedSimpleWeapon = true;
                            if (weapon == Program.lightCrossbow)
                            {
                                for (int i = 0; i < 20; i++)
                                {
                                    Program.player.inventory.Add(Program.crossbowBolt);
                                }
                            }
                        }
                    }
                }
                Program.player.inventory.Add(Program.mace);
                Program.player.inventory.Add(Program.shield);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nYou also gained a Mace and a Shield");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nPress Enter to Continue: ");
                Console.ReadLine();
            } else
            {
                goto NotDecidedStartingGear;
            }
        }
    }
    class Druid : Class
    {
        public Druid()
        {
            className = "Druid";
            classDescription = "A priest of the Old Faith, wielding the powers of nature and adopting animal forms.";
            primaryAbility = "Wisdom";
        }
        public override void PlayerCreation()
        {
            Program.player.languages.Add(Program.Language.DRUIDIC);
            Program.AddProficiency(Program.player, "Light Armour");
            Program.AddProficiency(Program.player, "Medium Armour");
            Program.AddProficiency(Program.player, "Shields");
            Program.AddProficiency(Program.player, "Clubs");
            Program.AddProficiency(Program.player, "Daggers");
            Program.AddProficiency(Program.player, "Darts");
            Program.AddProficiency(Program.player, "Javelins");
            Program.AddProficiency(Program.player, "Maces");
            Program.AddProficiency(Program.player, "Quarterstaffs");
            Program.AddProficiency(Program.player, "Scimitars");
            Program.AddProficiency(Program.player, "Sickles");
            Program.AddProficiency(Program.player, "Slings");
            Program.AddProficiency(Program.player, "Spears");
            Program.AddProficiency(Program.player, "Herbalism Kit");
            Program.AddProficiency(Program.player, "Intelligence");
            Program.AddProficiency(Program.player, "Wisdom");
            int selectedSkills = 0;
            List<Program.Skill> allowedSkills = new List<Program.Skill>() { Program.Skill.ARCANA, Program.Skill.ANIMALS, Program.Skill.INSIGHT, Program.Skill.MEDICINE, Program.Skill.NATURE,
                Program.Skill.PERCEPTION, Program.Skill.RELIGION, Program.Skill.SURVIVAL };
            while (selectedSkills < 2)
            {
                Console.Clear();
                foreach (Program.Skill skill in allowedSkills)
                {
                    if (Program.player.proficiencies.Contains(Program.GetDescription(skill))) continue;
                    Console.WriteLine(Program.GetDescription(skill));
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nWhich of these skills do you want to be proficient in?: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string response = Console.ReadLine().ToLower();
                foreach (Program.Skill skill in allowedSkills)
                {
                    if (!Program.player.proficiencies.Contains(Program.GetDescription(skill)) && response == Program.GetDescription(skill).ToLower())
                    {
                        Program.AddProficiency(Program.player, Program.GetDescription(skill));
                        allowedSkills.Remove(skill);
                        selectedSkills++;
                        break;
                    }
                }
            }
            int selectedCantrips = 0;
            while (selectedCantrips < 2)
            {
                Console.Clear();
                foreach (Spell spell in Program.druidSpells)
                {
                    if (spell.spellLevel == 0 && !Program.player.cantrips.Contains(spell))
                    {
                        Console.WriteLine(spell.spellName);
                    }
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nWhich of these Cantrips do you want to know?: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string response = Console.ReadLine().ToLower();
                foreach (Spell spell in Program.druidSpells)
                {
                    if (spell.spellLevel == 0 && !Program.player.cantrips.Contains(spell) && response == spell.spellName.ToLower())
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nYou have selected \"{spell.spellName}\", here is some information on the spell:\n\n{spell.spellDescription}\n");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write($"Do you want to have {spell.spellName} as your Cantrip?: ");
                        if (Program.CheckConfirmation())
                        {
                            Program.player.cantrips.Add(spell);
                            selectedCantrips++;
                            break;
                        }
                    }
                }
            }
            int selectedSpells = 0;
            while (selectedSpells < 3)
            {
                Console.Clear();
                foreach (Spell spell in Program.druidSpells)
                {
                    if (spell.spellLevel == 1 && !Program.player.knownSpells.Contains(spell))
                    {
                        Console.WriteLine(spell.spellName);
                    }
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nWhich of these Spells do you want to know?: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string response = Console.ReadLine().ToLower();
                foreach (Spell spell in Program.druidSpells)
                {
                    if (spell.spellLevel == 1 && !Program.player.knownSpells.Contains(spell) && response == spell.spellName.ToLower())
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nYou have selected \"{spell.spellName}\", here is some information on the spell:\n\n{spell.spellDescription}\n");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write($"Do you want to have {spell.spellName} as your Spell?: ");
                        if (Program.CheckConfirmation())
                        {
                            Program.player.knownSpells.Add(spell);
                            selectedSpells++;
                            break;
                        }
                    }
                }
            }
        NotDecidedStartingGear:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Do you want to start with gold or equipment?: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            string answer = Console.ReadLine().ToLower();
            if (answer == "gold")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Determining Starting Gold: Rolling 2 D4s...\n");
                List<int> diceRolled = Program.RollDice(true, new Tuple<int, int>(2, 4));
                Program.player.gold = diceRolled.Sum() * 10;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nYou will begin with a total of {Program.player.gold} gp.");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nPress Enter to Continue: ");
                Console.ReadLine();
            }
            else if (answer == "equipment")
            {
                bool selectedSimpleWeapon = false;
                while (!selectedSimpleWeapon)
                {
                    Console.Clear();
                    foreach (Weapon weapon in Program.allSimpleWeapons)
                    {
                        Console.WriteLine(weapon.equipmentName);
                    }
                    Console.WriteLine(Program.shield.equipmentName);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\nWhich weapon do you want to have?: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string choice = Console.ReadLine().ToLower();
                    if (choice == Program.shield.equipmentName.ToLower())
                    {
                        Program.player.inventory.Add(Program.shield);
                        break;
                    }
                    foreach (Weapon weapon in Program.allSimpleWeapons)
                    {
                        if (choice == weapon.equipmentName.ToLower())
                        {
                            Program.player.inventory.Add(weapon);
                            selectedSimpleWeapon = true;
                        }
                    }
                }
                bool selectedSimpleMeleeWeapon = false;
                while (!selectedSimpleMeleeWeapon)
                {
                    Console.Clear();
                    foreach (Weapon weapon in Program.allSimpleMeleeWeapons)
                    {
                        Console.WriteLine(weapon.equipmentName);
                    }
                    Console.WriteLine(Program.scimitar.equipmentName);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\nWhich weapon do you want to have?: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string choice = Console.ReadLine().ToLower();
                    if (choice == Program.scimitar.equipmentName.ToLower())
                    {
                        Program.player.inventory.Add(Program.scimitar);
                        break;
                    }
                    foreach (Weapon weapon in Program.allSimpleMeleeWeapons)
                    {
                        if (choice == weapon.equipmentName.ToLower())
                        {
                            Program.player.inventory.Add(weapon);
                            selectedSimpleMeleeWeapon = true;
                        }
                    }
                }
                Program.player.inventory.Add(Program.leather);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nYou also gained Leather Armour");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nPress Enter to Continue: ");
                Console.ReadLine();
            }
            else
            {
                goto NotDecidedStartingGear;
            }
        }
    }
    class Fighter : Class
    {
        public bool usedSecondWind = false;
        public enum FightingStyle
        {
            [Description("Archery:\nYou gain +2 to attack rolls with ranged weapons.")]
            ARCHERY,
            [Description("Defense:\nWhile wearing armour, gain +1 AC.")]
            DEFENSE,
            [Description("Dueling:\nWhen holding a melee weapon in one hand, gain +2 bonus to damage rolls.")]
            DUELING,
            [Description("Great Weapon Fighting:\nIf rolling a 1 or 2 in damage with a two-handed weapon, you can reroll it and use that value.")]
            GREATWEAPON,
            [Description("Protection:\nIf holding a shield and you can see a nearby friend getting attacked, you can use a reaction to make it a disadvantage.")]
            PROTECTION, 
            [Description("Two-Weapon Fighting:\nWhen fighting with two weapons, you add your ability modifier to the second attack's damage.")]
            DUALWEAPON
        }
        public List<FightingStyle> allFightStyles = new List<FightingStyle>() { FightingStyle.ARCHERY, FightingStyle.DEFENSE, FightingStyle.DUELING, FightingStyle.GREATWEAPON, 
            FightingStyle.PROTECTION, FightingStyle.DUALWEAPON };
        public FightingStyle fightingStyle;
        public Fighter()
        {
            className = "Fighter";
            classDescription = "A master of martial combat, skilled with a variety of weapons and armor.";
        }
        public override Tuple<int, int> CalculateHitDice()
        {
            return new Tuple<int, int>(classLevel, 10);
        }
        public override int CalculateHitPoints()
        {
            if (classLevel == 1)
            {
                return 10 + Program.GetAbilityModifier(Program.player, "Constitution");
            }
            else
            {
                return diceRollLastLevel + (Program.GetAbilityModifier(Program.player, "Constitution") * (classLevel - 1));
            }
        }
        public override void PlayerCreation()
        {
            Program.AddProficiency(Program.player, "Light Armour");
            Program.AddProficiency(Program.player, "Medium Armour");
            Program.AddProficiency(Program.player, "Heavy Armour");
            Program.AddProficiency(Program.player, "Shields");
            Program.AddProficiency(Program.player, "Simple Weapons");
            Program.AddProficiency(Program.player, "Martial Weapons");
            Program.AddProficiency(Program.player, "Strength");
            Program.AddProficiency(Program.player, "Constitution");
            int selectedSkills = 0;
            List<Program.Skill> allowedSkills = new List<Program.Skill>() { Program.Skill.ACROBATICS, Program.Skill.ANIMALS, Program.Skill.ATHLETICS, Program.Skill.HISTORY, Program.Skill.INSIGHT, 
                Program.Skill.INTIMIDATION, Program.Skill.PERCEPTION, Program.Skill.SURVIVAL };
            while (selectedSkills < 2)
            {
                Console.Clear();
                foreach (Program.Skill skill in allowedSkills)
                {
                    if (Program.player.proficiencies.Contains(Program.GetDescription(skill))) continue;
                    Console.WriteLine(Program.GetDescription(skill));
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nWhich of these skills do you want to be proficient in?: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string response = Console.ReadLine().ToLower();
                foreach (Program.Skill skill in allowedSkills)
                {
                    if (!Program.player.proficiencies.Contains(Program.GetDescription(skill)) && response == Program.GetDescription(skill).ToLower())
                    {
                        Program.AddProficiency(Program.player, Program.GetDescription(skill));
                        allowedSkills.Remove(skill);
                        selectedSkills++;
                        break;
                    }
                }
            }
            bool selectedFightStyle = false;
            while (!selectedFightStyle)
            {
                Console.Clear();
                foreach (FightingStyle fightStyle in allFightStyles)
                {
                    Console.WriteLine(Program.GetDescription(fightStyle) + "\n");
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nWhich fighting style do you which to use?: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string response = Console.ReadLine().ToLower();
                foreach (FightingStyle fightStyle in allFightStyles)
                {
                    string fightStyleDescription = Program.GetDescription(fightStyle);
                    string fightStyleName = fightStyleDescription.Substring(0, fightStyleDescription.IndexOf(':'));
                    if (response == fightStyleName.ToLower())
                    {
                        fightingStyle = fightStyle;
                        selectedFightStyle = true;
                        break;
                    }
                }
            }
        NotDecidedStartingGear:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Do you want to start with gold or equipment?: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            string answer = Console.ReadLine().ToLower();
            if (answer == "gold")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Determining Starting Gold: Rolling 5 D4s...\n");
                List<int> diceRolled = Program.RollDice(true, new Tuple<int, int>(5, 4));
                Program.player.gold = diceRolled.Sum() * 10;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nYou will begin with a total of {Program.player.gold} gp.");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nPress Enter to Continue: ");
                Console.ReadLine();
            }
            else if (answer == "equipment")
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine(Program.chainMail.equipmentName);
                    Console.WriteLine($"{Program.leather.equipmentName}, {Program.longBow.equipmentName}, and 20 Arrows");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\nWhich option do you want to choose?: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string choice = Console.ReadLine().ToLower();
                    if (choice == Program.chainMail.equipmentName.ToLower())
                    {
                        Program.player.inventory.Add(Program.chainMail);
                        break;
                    }
                    else if (choice == Program.leather.equipmentName.ToLower() || choice == Program.longBow.equipmentName.ToLower())
                    {
                        Program.player.inventory.Add(Program.leather);
                        Program.player.inventory.Add(Program.longBow);
                        for (int i = 0; i < 20; i++)
                        {
                            Program.player.inventory.Add(Program.arrow);
                        }
                    }
                }
                int selectedMartialWeapons = 0;
                while (selectedMartialWeapons < 2)
                {
                    Console.Clear();
                    foreach (Weapon weapon in Program.allMartialWeapons)
                    {
                        Console.WriteLine(weapon.equipmentName);
                    }
                    if (selectedMartialWeapons == 1) Console.WriteLine(Program.shield.equipmentName);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\nWhich would you like to have?: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string choice = Console.ReadLine().ToLower();
                    if (selectedMartialWeapons == 1 && choice == Program.shield.equipmentName.ToLower()) 
                    { 
                        Program.player.inventory.Add(Program.shield);
                        break;
                    }
                    foreach (Weapon weapon in Program.allMartialWeapons)
                    {
                        if (choice == weapon.equipmentName.ToLower())
                        {
                            Program.player.inventory.Add(weapon);
                            selectedMartialWeapons++;
                            break;
                        }
                    }
                }
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Two Handaxes");
                    Console.WriteLine($"{Program.lightCrossbow.equipmentName} + 20 Crossbow Bolts");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\nWhich option do you want to choose?: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string choice = Console.ReadLine().ToLower();
                    if (choice == Program.handAxe.equipmentName.ToLower() || choice == "handaxes")
                    {
                        Program.player.inventory.Add(Program.handAxe);
                        Program.player.inventory.Add(Program.handAxe);
                        break;
                    }
                    else if (choice == Program.lightCrossbow.equipmentName.ToLower() || choice == "crossbow")
                    {
                        Program.player.inventory.Add(Program.lightCrossbow);
                        for (int i = 0; i < 20; i++)
                        {
                            Program.player.inventory.Add(Program.crossbowBolt);
                        }
                    }
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nPress Enter to Continue: ");
                Console.ReadLine();
            }
            else
            {
                goto NotDecidedStartingGear;
            }
        }
    }
    class Monk : Class
    {
        public Monk()
        {
            className = "Monk";
            classDescription = "A master of martial arts, harnessing the power of the body in pursuit of physical and spiritual perfection.";
            primaryAbility = "Dexterity";
        }
        public override int CalculateArmourClass()
        {
            return 10 + Program.GetAbilityModifier(Program.player, "Dexterity") + Program.GetAbilityModifier(Program.player, "Wisdom"); // TODO: Only while not wearing armour or sheild
        }
        public override void PlayerCreation()
        {
            Program.AddProficiency(Program.player, "Simple Weapons");
            Program.AddProficiency(Program.player, "Shortswords");
            Program.AddProficiency(Program.player, "Strength");
            Program.AddProficiency(Program.player, "Dexterity");
            bool selectedArtisanTools = false;
            List<string> artisanTools = new List<string>() { "Alchemist's Supplies", "Brewer's Supplies", "Caligrapher's Supplies", "Carpenter's Tools", "Cartographer's Tools", "Cobbler's Tools",
                "Cook's Utensils", "Glassblower's Tools", "Jeweler's Tools", "Leatherworker's Tools", "Mason's Tools", "Painter's Supplies", "Potter's Tools", "Smith's Tools", "Tinker's Tools",
                "Weaver's Tools", "Woodcarver's Tools" };
            while (!selectedArtisanTools)
            {
                Console.Clear();
                foreach (string toolSet in artisanTools)
                {
                    if (!Program.player.proficiencies.Contains(toolSet))
                    {
                        Console.WriteLine(toolSet);
                    }
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nWhich tool set would you like to be proficient in?: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string response = Console.ReadLine().ToLower();
                foreach (string toolSet in artisanTools)
                {
                    string shortForm = toolSet.Substring(0, toolSet.IndexOf('\''));
                    if (!Program.player.proficiencies.Contains(toolSet) && (response == shortForm.ToLower() || response == toolSet.ToLower()))
                    {
                        Program.AddProficiency(Program.player, toolSet);
                        selectedArtisanTools = true;
                        break;
                    }
                }
            }
            int selectedSkills = 0;
            List<Program.Skill> allowedSkills = new List<Program.Skill>() { Program.Skill.ACROBATICS, Program.Skill.ATHLETICS, Program.Skill.HISTORY, Program.Skill.INSIGHT,
                Program.Skill.RELIGION, Program.Skill.STEALTH };
            while (selectedSkills < 2)
            {
                Console.Clear();
                foreach (Program.Skill skill in allowedSkills)
                {
                    if (Program.player.proficiencies.Contains(Program.GetDescription(skill))) continue;
                    Console.WriteLine(Program.GetDescription(skill));
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nWhich of these skills do you want to be proficient in?: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string response = Console.ReadLine().ToLower();
                foreach (Program.Skill skill in allowedSkills)
                {
                    if (!Program.player.proficiencies.Contains(Program.GetDescription(skill)) && response == Program.GetDescription(skill).ToLower())
                    {
                        Program.AddProficiency(Program.player, Program.GetDescription(skill));
                        allowedSkills.Remove(skill);
                        selectedSkills++;
                        break;
                    }
                }
            }
        NotDecidedStartingGear:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Do you want to start with gold or equipment?: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            string answer = Console.ReadLine().ToLower();
            if (answer == "gold")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Determining Starting Gold: Rolling 5 D4s...\n");
                List<int> diceRolled = Program.RollDice(true, new Tuple<int, int>(5, 4));
                Program.player.gold = diceRolled.Sum();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nYou will begin with a total of {Program.player.gold} gp.");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nPress Enter to Continue: ");
                Console.ReadLine();
            }
            else if (answer == "equipment")
            {
                bool selectedSimpleWeapon = false;
                while (!selectedSimpleWeapon)
                {
                    Console.Clear();
                    foreach (Weapon weapon in Program.allSimpleWeapons)
                    {
                        Console.WriteLine(weapon.equipmentName);
                    }
                    Console.WriteLine(Program.shortSword.equipmentName);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\nWhich weapon do you want to have?: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string choice = Console.ReadLine().ToLower();
                    if (choice == Program.shortSword.equipmentName.ToLower())
                    {
                        Program.player.inventory.Add(Program.shortSword);
                        break;
                    }
                    foreach (Weapon weapon in Program.allSimpleWeapons)
                    {
                        if (choice == weapon.equipmentName.ToLower())
                        {
                            Program.player.inventory.Add(weapon);
                            selectedSimpleWeapon = true;
                        }
                    }
                }
                for (int i = 0; i < 10; i++)
                {
                    Program.player.inventory.Add(Program.dart);
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nYou also gained 10 Darts");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nPress Enter to Continue: ");
                Console.ReadLine();
            }
            else
            {
                goto NotDecidedStartingGear;
            }
        }
    }
    class Paladin : Class
    {
        public int divineSenseUses = 0;
        public int layOnHandsHitPool = 5;
        public Paladin()
        {
            className = "Paladin";
            classDescription = "A holy warrior bound to a sacred oath.";
            primaryAbility = "Strength";
        }
        public override Tuple<int, int> CalculateHitDice()
        {
            return new Tuple<int, int>(classLevel, 10);
        }
        public override int CalculateHitPoints()
        {
            if (classLevel == 1)
            {
                return 10 + Program.GetAbilityModifier(Program.player, "Constitution");
            }
            else
            {
                return diceRollLastLevel + (Program.GetAbilityModifier(Program.player, "Constitution") * (classLevel - 1));
            }
        }
        public override void PlayerCreation()
        {
            Program.AddProficiency(Program.player, "Light Armour");
            Program.AddProficiency(Program.player, "Medium Armour");
            Program.AddProficiency(Program.player, "Heavy Armour");
            Program.AddProficiency(Program.player, "Simple Weapons");
            Program.AddProficiency(Program.player, "Martial Weapons");
            Program.AddProficiency(Program.player, "Wisdom");
            Program.AddProficiency(Program.player, "Charisma");
            int selectedSkills = 0;
            List<Program.Skill> allowedSkills = new List<Program.Skill>() { Program.Skill.ATHLETICS, Program.Skill.INSIGHT, Program.Skill.INTIMIDATION, Program.Skill.MEDICINE, 
                Program.Skill.PERSUASION, Program.Skill.RELIGION };
            while (selectedSkills < 2)
            {
                Console.Clear();
                foreach (Program.Skill skill in allowedSkills)
                {
                    if (Program.player.proficiencies.Contains(Program.GetDescription(skill))) continue;
                    Console.WriteLine(Program.GetDescription(skill));
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nWhich of these skills do you want to be proficient in?: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string response = Console.ReadLine().ToLower();
                foreach (Program.Skill skill in allowedSkills)
                {
                    if (!Program.player.proficiencies.Contains(Program.GetDescription(skill)) && response == Program.GetDescription(skill).ToLower())
                    {
                        Program.AddProficiency(Program.player, Program.GetDescription(skill));
                        allowedSkills.Remove(skill);
                        selectedSkills++;
                        break;
                    }
                }
            }
        NotDecidedStartingGear:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Do you want to start with gold or equipment?: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            string answer = Console.ReadLine().ToLower();
            if (answer == "gold")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Determining Starting Gold: Rolling 5 D4s...\n");
                List<int> diceRolled = Program.RollDice(true, new Tuple<int, int>(5, 4));
                Program.player.gold = diceRolled.Sum() * 10;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nYou will begin with a total of {Program.player.gold} gp.");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nPress Enter to Continue: ");
                Console.ReadLine();
            }
            else if (answer == "equipment")
            {
                int selectedMartialWeapons = 0;
                while (selectedMartialWeapons < 2)
                {
                    Console.Clear();
                    foreach (Weapon weapon in Program.allMartialWeapons)
                    {
                        Console.WriteLine(weapon.equipmentName);
                    }
                    if (selectedMartialWeapons == 1) Console.WriteLine(Program.shield.equipmentName);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\nWhich would you like to have?: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string choice = Console.ReadLine().ToLower();
                    if (selectedMartialWeapons == 1 && choice == Program.shield.equipmentName.ToLower())
                    {
                        Program.player.inventory.Add(Program.shield);
                        break;
                    }
                    foreach (Weapon weapon in Program.allMartialWeapons)
                    {
                        if (choice == weapon.equipmentName.ToLower())
                        {
                            Program.player.inventory.Add(weapon);
                            selectedMartialWeapons++;
                            break;
                        }
                    }
                }
                bool selectedSimpleMeleeWeapon = false;
                while (!selectedSimpleMeleeWeapon)
                {
                    Console.Clear();
                    foreach (Weapon weapon in Program.allSimpleMeleeWeapons)
                    {
                        if (weapon == Program.javelin)
                        {
                            Console.WriteLine("Five Javelins");
                            continue;
                        }
                        Console.WriteLine(weapon.equipmentName);
                    }
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\nWhich weapon do you want to have?: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string choice = Console.ReadLine().ToLower();
                    if (choice == Program.javelin.equipmentName.ToLower() || choice == "javelins" || choice == "five javelins")
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            Program.player.inventory.Add(Program.javelin);
                        }
                        break;
                    }
                    foreach (Weapon weapon in Program.allSimpleMeleeWeapons)
                    {
                        if (choice == weapon.equipmentName.ToLower())
                        {
                            Program.player.inventory.Add(weapon);
                            selectedSimpleMeleeWeapon = true;
                        }
                    }
                }
                Program.player.inventory.Add(Program.chainMail);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nYou also gained Chain Mail Armour");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nPress Enter to Continue: ");
                Console.ReadLine();
            }
            else
            {
                goto NotDecidedStartingGear;
            }
        }
    }
    class Ranger : Class
    {
        public Ranger()
        {
            className = "Ranger";
            classDescription = "A warrior who combats threats on the edges of civilization.";
            primaryAbility = "Dexterity";
        }
        public override Tuple<int, int> CalculateHitDice()
        {
            return new Tuple<int, int>(classLevel, 10);
        }
        public override int CalculateHitPoints()
        {
            if (classLevel == 1)
            {
                return 10 + Program.GetAbilityModifier(Program.player, "Constitution");
            }
            else
            {
                return diceRollLastLevel + (Program.GetAbilityModifier(Program.player, "Constitution") * (classLevel - 1));
            }
        }
        public override void PlayerCreation()
        {
            Program.AddProficiency(Program.player, "Light Armour");
            Program.AddProficiency(Program.player, "Medium Armour");
            Program.AddProficiency(Program.player, "Shields");
            Program.AddProficiency(Program.player, "Simple Weapons");
            Program.AddProficiency(Program.player, "Martial Weapons");
            Program.AddProficiency(Program.player, "Strength");
            Program.AddProficiency(Program.player, "Dexterity");
            int selectedSkills = 0;
            List<Program.Skill> allowedSkills = new List<Program.Skill>() { Program.Skill.ANIMALS,  Program.Skill.ATHLETICS, Program.Skill.INSIGHT, Program.Skill.INVESTIGATION, Program.Skill.NATURE,
                Program.Skill.PERCEPTION, Program.Skill.STEALTH, Program.Skill.SURVIVAL };
            while (selectedSkills < 3)
            {
                Console.Clear();
                foreach (Program.Skill skill in allowedSkills)
                {
                    if (Program.player.proficiencies.Contains(Program.GetDescription(skill))) continue;
                    Console.WriteLine(Program.GetDescription(skill));
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nWhich of these skills do you want to be proficient in?: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string response = Console.ReadLine().ToLower();
                foreach (Program.Skill skill in allowedSkills)
                {
                    if (!Program.player.proficiencies.Contains(Program.GetDescription(skill)) && response == Program.GetDescription(skill).ToLower())
                    {
                        Program.AddProficiency(Program.player, Program.GetDescription(skill));
                        allowedSkills.Remove(skill);
                        selectedSkills++;
                        break;
                    }
                }
            }
        NotDecidedStartingGear:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Do you want to start with gold or equipment?: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            string answer = Console.ReadLine().ToLower();
            if (answer == "gold")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Determining Starting Gold: Rolling 5 D4s...\n");
                List<int> diceRolled = Program.RollDice(true, new Tuple<int, int>(5, 4));
                Program.player.gold = diceRolled.Sum() * 10;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nYou will begin with a total of {Program.player.gold} gp.");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nPress Enter to Continue: ");
                Console.ReadLine();
            }
            else if (answer == "equipment")
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine(Program.scaleMail.equipmentName);
                    Console.WriteLine(Program.leather.equipmentName);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\nWhich armour do you want to have?: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string choice = Console.ReadLine().ToLower();
                    if (choice == Program.scaleMail.equipmentName.ToLower())
                    {
                        Program.player.inventory.Add(Program.scaleMail);
                        break;
                    }
                    else if (choice == Program.leather.equipmentName.ToLower())
                    {
                        Program.player.inventory.Add(Program.leather);
                        break;
                    }
                }
                int selectedSimpleMeleeWeapons = 0;
                while (selectedSimpleMeleeWeapons < 2)
                {
                    Console.Clear();
                    if (selectedSimpleMeleeWeapons == 0) Console.WriteLine("Two Shortswords");
                    foreach (Weapon weapon in Program.allSimpleMeleeWeapons)
                    {
                        Console.WriteLine(weapon.equipmentName);
                    }
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\nWhich weapon do you wish to start with?: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string choice = Console.ReadLine().ToLower();
                    if (selectedSimpleMeleeWeapons == 1 && (choice == "shortsword" || choice == "shortswords" || choice == "two shortswords"))
                    {
                        Program.player.inventory.Add(Program.shortSword);
                        Program.player.inventory.Add(Program.shortSword);
                        break;
                    }
                    foreach (Weapon weapon in Program.allSimpleMeleeWeapons)
                    {
                        if (choice == weapon.equipmentName.ToLower())
                        {
                            Program.player.inventory.Add(weapon);
                            selectedSimpleMeleeWeapons++;
                            break;
                        }
                    }
                }
                Program.player.inventory.Add(Program.longBow);
                for (int i = 0; i < 20; i++)
                {
                    Program.player.inventory.Add(Program.arrow);
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nYou also gained a Longbow and 20 Arrows");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nPress Enter to Continue: ");
                Console.ReadLine();
            }
            else
            {
                goto NotDecidedStartingGear;
            }
        }
    }
    class Rogue : Class
    {
        public Tuple<int, int> sneakAttackDice = new Tuple<int, int>(1, 6);
        public List<string> doubleProficiency = new List<string>();
        public Rogue()
        {
            className = "Rogue";
            classDescription = "A scoundrel who uses stealth and trickery to overcome obstacles and enemies.";
            primaryAbility = "Dexterity";
        }
        public override void PlayerCreation()
        {
            Program.player.languages.Add(Program.Language.THIEVESCANT);
            Program.AddProficiency(Program.player, "Light Armour");
            Program.AddProficiency(Program.player, "Simple Weapons");
            Program.AddProficiency(Program.player, "Hand Crossbows");
            Program.AddProficiency(Program.player, "Longswords");
            Program.AddProficiency(Program.player, "Rapiers");
            Program.AddProficiency(Program.player, "Shortswords");
            Program.AddProficiency(Program.player, "Thieve's Tools");
            Program.AddProficiency(Program.player, "Dexterity");
            Program.AddProficiency(Program.player, "Intelligence");
            int selectedSkills = 0;
            List<Program.Skill> allowedSkills = new List<Program.Skill>() { Program.Skill.ACROBATICS,  Program.Skill.ATHLETICS, Program.Skill.DECEPTION, Program.Skill.INSIGHT, 
                Program.Skill.INTIMIDATION, Program.Skill.INVESTIGATION, Program.Skill.PERCEPTION, Program.Skill.PERFORMANCE, Program.Skill.PERSUASION, Program.Skill.SLEIGHTOFHAND, 
                Program.Skill.STEALTH };
            while (selectedSkills < 4)
            {
                Console.Clear();
                foreach (Program.Skill skill in allowedSkills)
                {
                    if (Program.player.proficiencies.Contains(Program.GetDescription(skill))) continue;
                    Console.WriteLine(Program.GetDescription(skill));
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nWhich of these skills do you want to be proficient in?: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string response = Console.ReadLine().ToLower();
                foreach (Program.Skill skill in allowedSkills)
                {
                    if (!Program.player.proficiencies.Contains(Program.GetDescription(skill)) && response == Program.GetDescription(skill).ToLower())
                    {
                        Program.AddProficiency(Program.player, Program.GetDescription(skill));
                        allowedSkills.Remove(skill);
                        selectedSkills++;
                        break;
                    }
                }
            }
            int selectedExpertises = 0;
            List<string> skillNames = new List<string>();
            foreach (Tuple<Program.Skill, string> skill in Program.allSkillsAbilities)
            {
                skillNames.Add(Program.GetDescription(skill.Item1));
            }
            while (selectedExpertises < 2)
            {
                Console.Clear();
                foreach (string proficiency in Program.player.proficiencies)
                {
                    if ((proficiency == "Thieve's Tools" || skillNames.Contains(proficiency)) && !doubleProficiency.Contains(proficiency))
                    {
                        Console.WriteLine(proficiency);
                    }
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nWhich of these proficiencies would you like expertise in?: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string response = Console.ReadLine().ToLower();
                if ((response == "thieve" || response == "thieve's tools") && !doubleProficiency.Contains("Thieve's Tools"))
                {
                    doubleProficiency.Add("Thieve's Tools");
                    selectedExpertises++;
                    continue;
                }
                foreach (string proficiency in Program.player.proficiencies)
                {
                    if (skillNames.Contains(proficiency) && !doubleProficiency.Contains(proficiency) && response == proficiency.ToLower())
                    {
                        doubleProficiency.Add(proficiency);
                        selectedExpertises++;
                        break;
                    }
                }
            }
        NotDecidedStartingGear:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Do you want to start with gold or equipment?: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            string answer = Console.ReadLine().ToLower();
            if (answer == "gold")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Determining Starting Gold: Rolling 4 D4s...\n");
                List<int> diceRolled = Program.RollDice(true, new Tuple<int, int>(4, 4));
                Program.player.gold = diceRolled.Sum() * 10;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nYou will begin with a total of {Program.player.gold} gp.");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nPress Enter to Continue: ");
                Console.ReadLine();
            }
            else if (answer == "equipment")
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine(Program.rapier.equipmentName);
                    Console.WriteLine(Program.shortSword.equipmentName);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\nWhich weapon do you want to have?: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string choice = Console.ReadLine().ToLower();
                    if (choice == Program.rapier.equipmentName.ToLower())
                    {
                        Program.player.inventory.Add(Program.rapier);
                        break;
                    }
                    else if (choice == Program.shortSword.equipmentName.ToLower())
                    {
                        Program.player.inventory.Add(Program.shortSword);
                        break;
                    }
                }
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Shortbow and 20 Arrows");
                    Console.WriteLine(Program.shortSword.equipmentName);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\nWhich weapon do you want to have?: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string choice = Console.ReadLine().ToLower();
                    if (choice == "shortbow" || choice == "shortbow and 20 arrows" || choice == "arrows")
                    {
                        Program.player.inventory.Add(Program.shortbow);
                        for (int i = 0; i < 20; i++)
                        {
                            Program.player.inventory.Add(Program.arrow);
                        }
                        break;
                    }
                    else if (choice == Program.shortSword.equipmentName.ToLower())
                    {
                        Program.player.inventory.Add(Program.shortSword);
                        break;
                    }
                }
                Program.player.inventory.Add(Program.leather);
                Program.player.inventory.Add(Program.dagger);
                Program.player.inventory.Add(Program.dagger);
                Program.player.inventory.Add(Program.thievesTools);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nYou also gained Leather Armour, Two Daggers, and a set of Thieve's Tools");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nPress Enter to Continue: ");
                Console.ReadLine();
            }
            else
            {
                goto NotDecidedStartingGear;
            }
        }
    }
    class Sorcerer : Class
    {
        public int sorceryPoints = 0;
        public Dragonborn.DragonType draconicAncestor;
        public Sorcerer()
        {
            className = "Sorcerer";
            classDescription = "A spellcaster who draws on inherent magic from a gift or ancestry.";
            primaryAbility = "Charisma";
        }
        public override Tuple<int, int> CalculateHitDice()
        {
            return new Tuple<int, int>(classLevel, 6);
        }
        public override int CalculateHitPoints()
        {
            if (classLevel == 1)
            {
                return 7 + Program.GetAbilityModifier(Program.player, "Constitution");
            }
            else
            {
                return diceRollLastLevel + (Program.GetAbilityModifier(Program.player, "Constitution") * (classLevel - 1)) + classLevel;
            }
        }
        public override int CalculateArmourClass()
        {
            return 13 + Program.GetAbilityModifier(Program.player, "Dexterity"); // TODO: Not when wearing armour
        }
        public override void PlayerCreation()
        {
            Program.AddProficiency(Program.player, "Daggers");
            Program.AddProficiency(Program.player, "Darts");
            Program.AddProficiency(Program.player, "Slings");
            Program.AddProficiency(Program.player, "Quaterstaffs");
            Program.AddProficiency(Program.player, "Light Crossbows");
            Program.AddProficiency(Program.player, "Constitution");
            Program.AddProficiency(Program.player, "Charisma");
            int selectedSkills = 0;
            List<Program.Skill> allowedSkills = new List<Program.Skill>() { Program.Skill.ARCANA, Program.Skill.DECEPTION, Program.Skill.INSIGHT,
                Program.Skill.INTIMIDATION, Program.Skill.PERSUASION, Program.Skill.RELIGION };
            while (selectedSkills < 2)
            {
                Console.Clear();
                foreach (Program.Skill skill in allowedSkills)
                {
                    if (Program.player.proficiencies.Contains(Program.GetDescription(skill))) continue;
                    Console.WriteLine(Program.GetDescription(skill));
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nWhich of these skills do you want to be proficient in?: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string response = Console.ReadLine().ToLower();
                foreach (Program.Skill skill in allowedSkills)
                {
                    if (!Program.player.proficiencies.Contains(Program.GetDescription(skill)) && response == Program.GetDescription(skill).ToLower())
                    {
                        Program.AddProficiency(Program.player, Program.GetDescription(skill));
                        allowedSkills.Remove(skill);
                        selectedSkills++;
                        break;
                    }
                }
            }
            bool selectedType = false;
            while (!selectedType)
            {
                Console.Clear();
                foreach (Dragonborn.DragonType type in Dragonborn.allTypes)
                {
                    Console.WriteLine($"{type.typeName} dragons are resistant to {Program.GetDescription(type.damageResist).ToLower()} damage.\n");
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Which colour dragon was your ancestors?: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string response = Console.ReadLine();
                foreach (Dragonborn.DragonType type in Dragonborn.allTypes)
                {
                    if (response.ToLower() == type.typeName.ToLower())
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write($"\nDo you want the colour of your dragon ancestors to be {type.typeName}?: ");
                        if (Program.CheckConfirmation())
                        {
                            draconicAncestor = type;
                            Program.player.resistances.Add(type.damageResist);
                            selectedType = true;
                            break;
                        }
                    }
                }
            }
            int selectedCantrips = 0;
            while (selectedCantrips < 4)
            {
                Console.Clear();
                foreach (Spell spell in Program.sorcererSpells)
                {
                    if (spell.spellLevel == 0 && !Program.player.cantrips.Contains(spell))
                    {
                        Console.WriteLine(spell.spellName);
                    }
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nWhich of these Cantrips do you want to know?: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string response = Console.ReadLine().ToLower();
                foreach (Spell spell in Program.sorcererSpells)
                {
                    if (spell.spellLevel == 0 && !Program.player.cantrips.Contains(spell) && response == spell.spellName.ToLower())
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nYou have selected \"{spell.spellName}\", here is some information on the spell:\n\n{spell.spellDescription}\n");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write($"Do you want to have {spell.spellName} as your Cantrip?: ");
                        if (Program.CheckConfirmation())
                        {
                            Program.player.cantrips.Add(spell);
                            selectedCantrips++;
                            break;
                        }
                    }
                }
            }
            int selectedSpells = 0;
            while (selectedSpells < 2)
            {
                Console.Clear();
                foreach (Spell spell in Program.sorcererSpells)
                {
                    if (spell.spellLevel == 1 && !Program.player.knownSpells.Contains(spell))
                    {
                        Console.WriteLine(spell.spellName);
                    }
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nWhich of these Spells do you want to know?: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string response = Console.ReadLine().ToLower();
                foreach (Spell spell in Program.sorcererSpells)
                {
                    if (spell.spellLevel == 1 && !Program.player.knownSpells.Contains(spell) && response == spell.spellName.ToLower())
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nYou have selected \"{spell.spellName}\", here is some information on the spell:\n\n{spell.spellDescription}\n");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write($"Do you want to have {spell.spellName} as your Spell?: ");
                        if (Program.CheckConfirmation())
                        {
                            Program.player.knownSpells.Add(spell);
                            selectedSpells++;
                            break;
                        }
                    }
                }
            }
        NotDecidedStartingGear:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Do you want to start with gold or equipment?: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            string answer = Console.ReadLine().ToLower();
            if (answer == "gold")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Determining Starting Gold: Rolling 3 D4s...\n");
                List<int> diceRolled = Program.RollDice(true, new Tuple<int, int>(3, 4));
                Program.player.gold = diceRolled.Sum() * 10;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nYou will begin with a total of {Program.player.gold} gp.");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nPress Enter to Continue: ");
                Console.ReadLine();
            }
            else if (answer == "equipment")
            {
                bool selectedSimpleWeapon = false;
                while (!selectedSimpleWeapon)
                {
                    Console.Clear();
                    foreach (Weapon weapon in Program.allSimpleWeapons)
                    {
                        if (weapon == Program.lightCrossbow)
                        {
                            Console.WriteLine("Light Crossbow and 20 Crossbow Bolts");
                            continue;
                        }
                        Console.WriteLine(weapon.equipmentName);
                    }
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\nWhich weapon do you want to have?: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string choice = Console.ReadLine().ToLower();
                    foreach (Weapon weapon in Program.allSimpleWeapons)
                    {
                        if (choice == weapon.equipmentName.ToLower())
                        {
                            Program.player.inventory.Add(weapon);
                            if (weapon == Program.lightCrossbow)
                            {
                                for (int i = 0; i < 20; i++)
                                {
                                    Program.player.inventory.Add(Program.arrow);
                                }
                            }
                            selectedSimpleWeapon = true;
                        }
                    }
                }
                Program.player.inventory.Add(Program.dagger);
                Program.player.inventory.Add(Program.dagger);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nYou also gained two Daggers");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nPress Enter to Continue: ");
                Console.ReadLine();
            }
            else
            {
                goto NotDecidedStartingGear;
            }
        }
    }
    class Warlock : Class
    {
        public Warlock()
        {
            className = "Warlock";
            classDescription = "A wielder of magic that is derived from a bargain with an extraplanar entity.";
            primaryAbility = "Charisma";
        }
        public override void PlayerCreation()
        {
            Program.AddProficiency(Program.player, "Light Armour");
            Program.AddProficiency(Program.player, "Simple Weapons");
            Program.AddProficiency(Program.player, "Wisdom");
            Program.AddProficiency(Program.player, "Charisma");
            int selectedSkills = 0;
            List<Program.Skill> allowedSkills = new List<Program.Skill>() { Program.Skill.ARCANA, Program.Skill.DECEPTION, Program.Skill.HISTORY,
                Program.Skill.INTIMIDATION, Program.Skill.INVESTIGATION, Program.Skill.NATURE, Program.Skill.RELIGION };
            while (selectedSkills < 2)
            {
                Console.Clear();
                foreach (Program.Skill skill in allowedSkills)
                {
                    if (Program.player.proficiencies.Contains(Program.GetDescription(skill))) continue;
                    Console.WriteLine(Program.GetDescription(skill));
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nWhich of these skills do you want to be proficient in?: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string response = Console.ReadLine().ToLower();
                foreach (Program.Skill skill in allowedSkills)
                {
                    if (!Program.player.proficiencies.Contains(Program.GetDescription(skill)) && response == Program.GetDescription(skill).ToLower())
                    {
                        Program.AddProficiency(Program.player, Program.GetDescription(skill));
                        allowedSkills.Remove(skill);
                        selectedSkills++;
                        break;
                    }
                }
            }
            int selectedCantrips = 0;
            while (selectedCantrips < 2)
            {
                Console.Clear();
                foreach (Spell spell in Program.warlockSpells)
                {
                    if (spell.spellLevel == 0 && !Program.player.cantrips.Contains(spell))
                    {
                        Console.WriteLine(spell.spellName);
                    }
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nWhich of these Cantrips do you want to know?: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string response = Console.ReadLine().ToLower();
                foreach (Spell spell in Program.warlockSpells)
                {
                    if (spell.spellLevel == 0 && !Program.player.cantrips.Contains(spell) && response == spell.spellName.ToLower())
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nYou have selected \"{spell.spellName}\", here is some information on the spell:\n\n{spell.spellDescription}\n");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write($"Do you want to have {spell.spellName} as your Cantrip?: ");
                        if (Program.CheckConfirmation())
                        {
                            Program.player.cantrips.Add(spell);
                            selectedCantrips++;
                            break;
                        }
                    }
                }
            }
            int selectedSpells = 0;
            while (selectedSpells < 2)
            {
                Console.Clear();
                foreach (Spell spell in Program.warlockSpells)
                {
                    if (spell.spellLevel == 1 && !Program.player.knownSpells.Contains(spell))
                    {
                        Console.WriteLine(spell.spellName);
                    }
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nWhich of these Spells do you want to know?: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string response = Console.ReadLine().ToLower();
                foreach (Spell spell in Program.warlockSpells)
                {
                    if (spell.spellLevel == 1 && !Program.player.knownSpells.Contains(spell) && response == spell.spellName.ToLower())
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nYou have selected \"{spell.spellName}\", here is some information on the spell:\n\n{spell.spellDescription}\n");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write($"Do you want to have {spell.spellName} as your Spell?: ");
                        if (Program.CheckConfirmation())
                        {
                            Program.player.knownSpells.Add(spell);
                            selectedSpells++;
                            break;
                        }
                    }
                }
            }
        NotDecidedStartingGear:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Do you want to start with gold or equipment?: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            string answer = Console.ReadLine().ToLower();
            if (answer == "gold")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Determining Starting Gold: Rolling 4 D4s...\n");
                List<int> diceRolled = Program.RollDice(true, new Tuple<int, int>(4, 4));
                Program.player.gold = diceRolled.Sum() * 10;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nYou will begin with a total of {Program.player.gold} gp.");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nPress Enter to Continue: ");
                Console.ReadLine();
            }
            else if (answer == "equipment")
            {
                bool selectedSimpleWeapon = false;
                while (!selectedSimpleWeapon)
                {
                    Console.Clear();
                    foreach (Weapon weapon in Program.allSimpleWeapons)
                    {
                        if (weapon == Program.lightCrossbow)
                        {
                            Console.WriteLine("Light Crossbow and 20 Crossbow Bolts");
                            continue;
                        }
                        Console.WriteLine(weapon.equipmentName);
                    }
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\nWhich weapon do you want to have?: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string choice = Console.ReadLine().ToLower();
                    foreach (Weapon weapon in Program.allSimpleWeapons)
                    {
                        if (choice == weapon.equipmentName.ToLower())
                        {
                            Program.player.inventory.Add(weapon);
                            if (weapon == Program.lightCrossbow)
                            {
                                for (int i = 0; i < 20; i++)
                                {
                                    Program.player.inventory.Add(Program.arrow);
                                }
                            }
                            selectedSimpleWeapon = true;
                        }
                    }
                }
                bool selectedSimpleWeapons = false;
                while (!selectedSimpleWeapons)
                {
                    Console.Clear();
                    foreach (Weapon weapon in Program.allSimpleWeapons)
                    {
                        Console.WriteLine(weapon.equipmentName);
                    }
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\nWhich weapon would you want to have?: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string choice = Console.ReadLine().ToLower();
                    foreach (Weapon weapon in Program.allSimpleWeapons)
                    {
                        if (choice == weapon.equipmentName.ToLower())
                        {
                            Program.player.inventory.Add(weapon);
                            selectedSimpleWeapon = true;
                        }
                    }
                }
                Program.player.inventory.Add(Program.dagger);
                Program.player.inventory.Add(Program.dagger);
                Program.player.inventory.Add(Program.leather);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nYou also gained Leather Armour and two Daggers");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nPress Enter to Continue: ");
                Console.ReadLine();
            }
            else
            {
                goto NotDecidedStartingGear;
            }
        }
    }
    class Wizard : Class
    {
        public List<Spell> spellbook = new List<Spell>();
        public Wizard()
        {
            className = "Wizard";
            classDescription = "A scholarly magic-user capable of manipulating the structures of reality.";
            primaryAbility = "Intelligence";
        }
        public override void PlayerCreation()
        {
            Program.AddProficiency(Program.player, "Daggers");
            Program.AddProficiency(Program.player, "Darts");
            Program.AddProficiency(Program.player, "Slings");
            Program.AddProficiency(Program.player, "Quarterstaffs");
            Program.AddProficiency(Program.player, "Light Crossbows");
            Program.AddProficiency(Program.player, "Intelligence");
            Program.AddProficiency(Program.player, "Wisdom");
            int selectedSkills = 0;
            List<Program.Skill> allowedSkills = new List<Program.Skill>() { Program.Skill.ARCANA, Program.Skill.HISTORY, Program.Skill.INSIGHT, Program.Skill.INVESTIGATION, 
                Program.Skill.MEDICINE, Program.Skill.RELIGION };
            while (selectedSkills < 2)
            {
                Console.Clear();
                foreach (Program.Skill skill in allowedSkills)
                {
                    if (Program.player.proficiencies.Contains(Program.GetDescription(skill))) continue;
                    Console.WriteLine(Program.GetDescription(skill));
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nWhich of these skills do you want to be proficient in?: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string response = Console.ReadLine().ToLower();
                foreach (Program.Skill skill in allowedSkills)
                {
                    if (!Program.player.proficiencies.Contains(Program.GetDescription(skill)) && response == Program.GetDescription(skill).ToLower())
                    {
                        Program.AddProficiency(Program.player, Program.GetDescription(skill));
                        allowedSkills.Remove(skill);
                        selectedSkills++;
                        break;
                    }
                }
            }
            int selectedCantrips = 0;
            while (selectedCantrips < 3)
            {
                Console.Clear();
                foreach (Spell spell in Program.wizardSpells)
                {
                    if (spell.spellLevel == 0 && !Program.player.cantrips.Contains(spell))
                    {
                        Console.WriteLine(spell.spellName);
                    }
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nWhich of these Cantrips do you want to know?: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string response = Console.ReadLine().ToLower();
                foreach (Spell spell in Program.wizardSpells)
                {
                    if (spell.spellLevel == 0 && !Program.player.cantrips.Contains(spell) && response == spell.spellName.ToLower())
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nYou have selected \"{spell.spellName}\", here is some information on the spell:\n\n{spell.spellDescription}\n");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write($"Do you want to have {spell.spellName} as your Cantrip?: ");
                        if (Program.CheckConfirmation())
                        {
                            Program.player.cantrips.Add(spell);
                            selectedCantrips++;
                            break;
                        }
                    }
                }
            }
            int selectedSpells = 0;
            while (selectedSpells < 6)
            {
                Console.Clear();
                foreach (Spell spell in Program.wizardSpells)
                {
                    if (spell.spellLevel == 1 && !Program.player.knownSpells.Contains(spell))
                    {
                        Console.WriteLine(spell.spellName);
                    }
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nWhich of these Spells do you want to know?: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string response = Console.ReadLine().ToLower();
                foreach (Spell spell in Program.wizardSpells)
                {
                    if (spell.spellLevel == 1 && !Program.player.knownSpells.Contains(spell) && response == spell.spellName.ToLower())
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nYou have selected \"{spell.spellName}\", here is some information on the spell:\n\n{spell.spellDescription}\n");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write($"Do you want to have {spell.spellName} as your Spell?: ");
                        if (Program.CheckConfirmation())
                        {
                            Program.player.knownSpells.Add(spell);
                            selectedSpells++;
                            break;
                        }
                    }
                }
            }
        NotDecidedStartingGear:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Do you want to start with gold or equipment?: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            string answer = Console.ReadLine().ToLower();
            if (answer == "gold")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Determining Starting Gold: Rolling 4 D4s...\n");
                List<int> diceRolled = Program.RollDice(true, new Tuple<int, int>(4, 4));
                Program.player.gold = diceRolled.Sum() * 10;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nYou will begin with a total of {Program.player.gold} gp.");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nPress Enter to Continue: ");
                Console.ReadLine();
            }
            else if (answer == "equipment")
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine(Program.quarterStaff.equipmentName);
                    Console.WriteLine(Program.dagger.equipmentName);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\nWhich armour do you want to have?: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string choice = Console.ReadLine().ToLower();
                    if (choice == Program.quarterStaff.equipmentName.ToLower())
                    {
                        Program.player.inventory.Add(Program.quarterStaff);
                        break;
                    }
                    else if (choice == Program.dagger.equipmentName.ToLower())
                    {
                        Program.player.inventory.Add(Program.dagger);
                        break;
                    }
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nPress Enter to Continue: ");
                Console.ReadLine();
            }
            else
            {
                goto NotDecidedStartingGear;
            }
        }
        public override Tuple<int, int> CalculateHitDice()
        {
            return new Tuple<int, int>(classLevel, 6);
        }
        public override int CalculateHitPoints()
        {
            if (classLevel == 1)
            {
                return 6 + Program.GetAbilityModifier(Program.player, "Constitution");
            }
            else
            {
                return diceRollLastLevel + (Program.GetAbilityModifier(Program.player, "Constitution") * (classLevel - 1));
            }
        }
    }
}
