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
        public virtual void PlayerCreation()
        {

        }
        public virtual int CalculateHitDice()
        {
            return 0;
        }
        public virtual int CalculateHitPoints()
        {
            return 0;
        }
        public virtual int CalculateArmourClass()
        {
            return 0;
        }
        public virtual void IncreaseLevel(int diceSize = 8)
        {
            diceRollLastLevel = Program.RollDice(false, new Tuple<int, int>(1, diceSize)).First();
        }
    }
    class Barbarian : Class
    {
        public int timesRagedSinceRest = 0;
        public Barbarian()
        {
            className = "Barbarian";
            classDescription = "A fierce warrior who can enter a battle rage.";
            primaryAbility = "Strength";
        }
        public override void PlayerCreation()
        {
            Program.AddProficiency("Light Armour");
            Program.AddProficiency("Medium Armour");
            Program.AddProficiency("Shields");
            Program.AddProficiency("Simple Weapons");
            Program.AddProficiency("Martial Weapons");
            Program.AddProficiency("Strength");
            Program.AddProficiency("Constitution");
            int selectedSkills = 0;
            List<Program.Skill> allowedSkills = new List<Program.Skill>() { Program.Skill.ANIMALS, Program.Skill.ATHLETICS, Program.Skill.INTIMIDATION, Program.Skill.NATURE, 
                Program.Skill.PERCEPTION, Program.Skill.SURVIVAL };
            while (selectedSkills < 2)
            {
                Console.Clear();
                foreach (Program.Skill skill in allowedSkills)
                {
                    if (Program.playerProficiencies.Contains(Program.GetDescription(skill))) continue;
                    Console.WriteLine(Program.GetDescription(skill));
                }
                Console.Write("\nWhich of these skills do you want to be proficient in?: ");
                string response = Console.ReadLine().ToLower();
                foreach (Program.Skill skill in allowedSkills)
                {
                    if (!Program.playerProficiencies.Contains(Program.GetDescription(skill)) && response == Program.GetDescription(skill).ToLower())
                    {
                        Program.AddProficiency(Program.GetDescription(skill));
                        allowedSkills.Remove(skill);
                        selectedSkills++;
                        break;
                    }
                }
            }
            Console.Clear();
            Console.WriteLine("Determining Starting Gold: Rolling 2 D4s...\n");
            List<int> diceRolled = Program.RollDice(true, new Tuple<int, int>(2, 4));
            Program.playerGold = diceRolled.Sum() * 10;
            Console.WriteLine($"\nYou will begin with a total of {Program.playerGold} gp.");
            Console.ReadLine();
        }
        public override int CalculateHitDice()
        {
            return 12 * Program.playerLevel;
        }
        public override int CalculateHitPoints()
        {
            if (Program.playerLevel == 1)
            {
                return 12 + Program.playerConstitution;
            } else
            {
                return diceRollLastLevel + (Program.playerConstitution * (Program.playerLevel - 1));
            }
        }
        public override int CalculateArmourClass()
        {
            return 10 + Program.playerConstitution + Program.playerDexterity; // TODO: Only when not wearing any armour
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
            Program.AddProficiency("Light Armour");
            Program.AddProficiency("Simple Weapons");
            Program.AddProficiency("Hand Crossbows");
            Program.AddProficiency("Longswords");
            Program.AddProficiency("Rapiers");
            Program.AddProficiency("Dexterity");
            Program.AddProficiency("Charisma");
            int selectedSkills = 0;
            while (selectedSkills < 3)
            {
                Console.Clear();
                foreach (Program.Skill skill in Program.allSkills)
                {
                    if (Program.playerProficiencies.Contains(Program.GetDescription(skill))) continue;
                    Console.WriteLine(Program.GetDescription(skill));
                }
                Console.Write("\nWhich of these skills do you want to be proficient in?: ");
                string response = Console.ReadLine().ToLower();
                foreach (Program.Skill skill in Program.allSkills)
                {
                    if (!Program.playerProficiencies.Contains(Program.GetDescription(skill)) && response == Program.GetDescription(skill).ToLower())
                    {
                        Program.AddProficiency(Program.GetDescription(skill));
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
                    if (spell.spellLevel == 0 && !Program.playerCantrips.Contains(spell))
                    {
                        Console.WriteLine(spell.spellName);
                    }
                }
                Console.Write("\nWhich of these Cantrips do you want to know?: ");
                string response = Console.ReadLine().ToLower();
                foreach (Spell spell in Program.bardSpells)
                {
                    if (spell.spellLevel == 0 && !Program.playerCantrips.Contains(spell) && response == spell.spellName.ToLower())
                    {
                        Console.WriteLine($"You have selected \"{spell.spellName}\", here is some information on the spell:\n\n{spell.spellDescription}\n");
                        Console.Write($"Do you want to have {spell.spellName} as your Cantrip?: ");
                        if (Program.CheckConfirmation())
                        {
                            Program.playerCantrips.Add(spell);
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
                    if (spell.spellLevel == 1 && !Program.playerKnownSpells.Contains(spell))
                    {
                        Console.WriteLine(spell.spellName);
                    }
                }
                Console.Write("\nWhich of these Spells do you want to know?: ");
                string response = Console.ReadLine().ToLower();
                foreach (Spell spell in Program.bardSpells)
                {
                    if (spell.spellLevel == 1 && !Program.playerKnownSpells.Contains(spell) && response == spell.spellName.ToLower())
                    {
                        Console.WriteLine($"You have selected \"{spell.spellName}\", here is some information on the spell:\n\n{spell.spellDescription}\n");
                        Console.Write($"Do you want to have {spell.spellName} as your Spell?: ");
                        if (Program.CheckConfirmation())
                        {
                            Program.playerKnownSpells.Add(spell);
                            selectedSpells++;
                            break;
                        }
                    }
                }
            }
            Console.Clear();
            Console.WriteLine("Determining Starting Gold: Rolling 5 D4s...\n");
            List<int> diceRolled = Program.RollDice(true, new Tuple<int, int>(5, 4));
            Program.playerGold = diceRolled.Sum() * 10;
            Console.WriteLine($"\nYou will begin with a total of {Program.playerGold} gp.");
            Console.ReadLine();
        }
        public override int CalculateHitDice()
        {
            return 8 * Program.playerLevel;
        }
        public override int CalculateHitPoints()
        {
            if (Program.playerLevel == 1)
            {
                return 8 + Program.playerConstitution;
            } else
            {
                return diceRollLastLevel + (Program.playerConstitution * (Program.playerLevel - 1));
            }
        }
        public override int CalculateArmourClass()
        {
            return 10 + Program.playerDexterity;
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
        public override int CalculateHitDice()
        {
            return 8 * Program.playerLevel;
        }
        public override int CalculateHitPoints()
        {
            if (Program.playerLevel == 1)
            {
                return 8 + Program.playerConstitution;
            } else
            {
                return diceRollLastLevel + (Program.playerConstitution * (Program.playerLevel - 1));
            }
        }
        public override int CalculateArmourClass()
        {
            return 10 + Program.playerDexterity;
        }
        public override void PlayerCreation()
        {
            Program.AddProficiency("Light Armour");
            Program.AddProficiency("Medium Armour");
            Program.AddProficiency("Heavy Armour");
            Program.AddProficiency("Shields");
            Program.AddProficiency("Simple Weapons");
            Program.AddProficiency("Wisdom");
            Program.AddProficiency("Charisma");
            int selectedSkills = 0;
            List<Program.Skill> allowedSkills = new List<Program.Skill>() { Program.Skill.HISTORY, Program.Skill.INSIGHT, Program.Skill.MEDICINE, Program.Skill.PERSUASION, Program.Skill.RELIGION };
            while (selectedSkills < 2)
            {
                Console.Clear();
                foreach (Program.Skill skill in allowedSkills)
                {
                    if (Program.playerProficiencies.Contains(Program.GetDescription(skill))) continue;
                    Console.WriteLine(Program.GetDescription(skill));
                }
                Console.Write("\nWhich of these skills do you want to be proficient in?: ");
                string response = Console.ReadLine().ToLower();
                foreach (Program.Skill skill in allowedSkills)
                {
                    if (!Program.playerProficiencies.Contains(Program.GetDescription(skill)) && response == Program.GetDescription(skill).ToLower())
                    {
                        Program.AddProficiency(Program.GetDescription(skill));
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
                    if (spell.spellLevel == 0 && !Program.playerCantrips.Contains(spell))
                    {
                        Console.WriteLine(spell.spellName);
                    }
                }
                Console.Write("\nWhich of these Cantrips do you want to know?: ");
                string response = Console.ReadLine().ToLower();
                foreach (Spell spell in Program.clericSpells)
                {
                    if (spell.spellLevel == 0 && !Program.playerCantrips.Contains(spell) && response == spell.spellName.ToLower())
                    {
                        Console.WriteLine($"You have selected \"{spell.spellName}\", here is some information on the spell:\n\n{spell.spellDescription}\n");
                        Console.Write($"Do you want to have {spell.spellName} as your Cantrip?: ");
                        if (Program.CheckConfirmation())
                        {
                            Program.playerCantrips.Add(spell);
                            selectedCantrips++;
                            break;
                        }
                    }
                }
            }
            Program.playerKnownSpells.Add(Program.bless);
            Program.playerKnownSpells.Add(Program.cureWounds);
            int selectedSpells = 0;
            while (selectedSpells < 3)
            {
                Console.Clear();
                foreach (Spell spell in Program.clericSpells)
                {
                    if (spell.spellLevel == 1 && !Program.playerKnownSpells.Contains(spell))
                    {
                        Console.WriteLine(spell.spellName);
                    }
                }
                Console.Write("\nWhich of these Spells do you want to know?: ");
                string response = Console.ReadLine().ToLower();
                foreach (Spell spell in Program.clericSpells)
                {
                    if (spell.spellLevel == 1 && !Program.playerKnownSpells.Contains(spell) && response == spell.spellName.ToLower())
                    {
                        Console.WriteLine($"You have selected \"{spell.spellName}\", here is some information on the spell:\n\n{spell.spellDescription}\n");
                        Console.Write($"Do you want to have {spell.spellName} as your Spell?: ");
                        if (Program.CheckConfirmation())
                        {
                            Program.playerKnownSpells.Add(spell);
                            selectedSpells++;
                            break;
                        }
                    }
                }
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
        public override int CalculateHitDice()
        {
            return 8 * Program.playerLevel;
        }
        public override int CalculateHitPoints()
        {
            if (Program.playerLevel == 1)
            {
                return 8 + Program.playerConstitution;
            }
            else
            {
                return diceRollLastLevel + (Program.playerConstitution * (Program.playerLevel - 1));
            }
        }
        public override int CalculateArmourClass()
        {
            return 10 + Program.playerDexterity;
        }
        public override void PlayerCreation()
        {
            Program.playerRace.languages.Add(Program.Language.DRUIDIC);
            Program.AddProficiency("Light Armour");
            Program.AddProficiency("Medium Armour");
            Program.AddProficiency("Shields");
            Program.AddProficiency("Clubs");
            Program.AddProficiency("Daggers");
            Program.AddProficiency("Darts");
            Program.AddProficiency("Javelins");
            Program.AddProficiency("Maces");
            Program.AddProficiency("Quarterstaffs");
            Program.AddProficiency("Scimitars");
            Program.AddProficiency("Sickles");
            Program.AddProficiency("Slings");
            Program.AddProficiency("Spears");
            Program.AddProficiency("Herbalism Kit");
            Program.AddProficiency("Intelligence");
            Program.AddProficiency("Wisdom");
            int selectedSkills = 0;
            List<Program.Skill> allowedSkills = new List<Program.Skill>() { Program.Skill.ARCANA, Program.Skill.ANIMALS, Program.Skill.INSIGHT, Program.Skill.MEDICINE, Program.Skill.NATURE,
                Program.Skill.PERCEPTION, Program.Skill.RELIGION, Program.Skill.SURVIVAL };
            while (selectedSkills < 2)
            {
                Console.Clear();
                foreach (Program.Skill skill in allowedSkills)
                {
                    if (Program.playerProficiencies.Contains(Program.GetDescription(skill))) continue;
                    Console.WriteLine(Program.GetDescription(skill));
                }
                Console.Write("\nWhich of these skills do you want to be proficient in?: ");
                string response = Console.ReadLine().ToLower();
                foreach (Program.Skill skill in allowedSkills)
                {
                    if (!Program.playerProficiencies.Contains(Program.GetDescription(skill)) && response == Program.GetDescription(skill).ToLower())
                    {
                        Program.AddProficiency(Program.GetDescription(skill));
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
                    if (spell.spellLevel == 0 && !Program.playerCantrips.Contains(spell))
                    {
                        Console.WriteLine(spell.spellName);
                    }
                }
                Console.Write("\nWhich of these Cantrips do you want to know?: ");
                string response = Console.ReadLine().ToLower();
                foreach (Spell spell in Program.druidSpells)
                {
                    if (spell.spellLevel == 0 && !Program.playerCantrips.Contains(spell) && response == spell.spellName.ToLower())
                    {
                        Console.WriteLine($"You have selected \"{spell.spellName}\", here is some information on the spell:\n\n{spell.spellDescription}\n");
                        Console.Write($"Do you want to have {spell.spellName} as your Cantrip?: ");
                        if (Program.CheckConfirmation())
                        {
                            Program.playerCantrips.Add(spell);
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
                    if (spell.spellLevel == 1 && !Program.playerKnownSpells.Contains(spell))
                    {
                        Console.WriteLine(spell.spellName);
                    }
                }
                Console.Write("\nWhich of these Spells do you want to know?: ");
                string response = Console.ReadLine().ToLower();
                foreach (Spell spell in Program.druidSpells)
                {
                    if (spell.spellLevel == 1 && !Program.playerKnownSpells.Contains(spell) && response == spell.spellName.ToLower())
                    {
                        Console.WriteLine($"You have selected \"{spell.spellName}\", here is some information on the spell:\n\n{spell.spellDescription}\n");
                        Console.Write($"Do you want to have {spell.spellName} as your Spell?: ");
                        if (Program.CheckConfirmation())
                        {
                            Program.playerKnownSpells.Add(spell);
                            selectedSpells++;
                            break;
                        }
                    }
                }
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
        public override int CalculateHitDice()
        {
            return 10 * Program.playerLevel;
        }
        public override int CalculateHitPoints()
        {
            if (Program.playerLevel == 1)
            {
                return 10 + Program.playerConstitution;
            }
            else
            {
                return diceRollLastLevel + (Program.playerConstitution * (Program.playerLevel - 1));
            }
        }
        public override int CalculateArmourClass()
        {
            return 10 + Program.playerDexterity;
        }
        public override void PlayerCreation()
        {
            Program.AddProficiency("Light Armour");
            Program.AddProficiency("Medium Armour");
            Program.AddProficiency("Heavy Armour");
            Program.AddProficiency("Shields");
            Program.AddProficiency("Simple Weapons");
            Program.AddProficiency("Martial Weapons");
            Program.AddProficiency("Strength");
            Program.AddProficiency("Constitution");
            int selectedSkills = 0;
            List<Program.Skill> allowedSkills = new List<Program.Skill>() { Program.Skill.ACROBATICS, Program.Skill.ANIMALS, Program.Skill.ATHLETICS, Program.Skill.HISTORY, Program.Skill.INSIGHT, 
                Program.Skill.INTIMIDATION, Program.Skill.PERCEPTION, Program.Skill.SURVIVAL };
            while (selectedSkills < 2)
            {
                Console.Clear();
                foreach (Program.Skill skill in allowedSkills)
                {
                    if (Program.playerProficiencies.Contains(Program.GetDescription(skill))) continue;
                    Console.WriteLine(Program.GetDescription(skill));
                }
                Console.Write("\nWhich of these skills do you want to be proficient in?: ");
                string response = Console.ReadLine().ToLower();
                foreach (Program.Skill skill in allowedSkills)
                {
                    if (!Program.playerProficiencies.Contains(Program.GetDescription(skill)) && response == Program.GetDescription(skill).ToLower())
                    {
                        Program.AddProficiency(Program.GetDescription(skill));
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
                Console.Write("\nWhich fighting style do you which to use?: ");
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
    }
    class Paladin : Class
    {
        public Paladin()
        {
            className = "Paladin";
            classDescription = "A holy warrior bound to a sacred oath.";
            primaryAbility = "Strength";
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
    }
    class Rogue : Class
    {
        public Rogue()
        {
            className = "Rogue";
            classDescription = "A scoundrel who uses stealth and trickery to overcome obstacles and enemies.";
            primaryAbility = "Dexterity";
        }
    }
    class Sorcerer : Class
    {
        public Sorcerer()
        {
            className = "Sorcerer";
            classDescription = "A spellcaster who draws on inherent magic from a gift or ancestry.";
            primaryAbility = "Charisma";
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
    }
    class Wizard : Class
    {
        public Wizard()
        {
            className = "Wizard";
            classDescription = "A scholarly magic-user capable of manipulating the structures of reality.";
            primaryAbility = "Intelligence";
        }
    }
}
