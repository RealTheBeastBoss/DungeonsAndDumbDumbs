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
        public virtual Tuple<int, int> CalculateHitDice()
        {
            return new Tuple<int, int>(Program.playerLevel, 8);
        }
        public virtual int CalculateHitPoints()
        {
            if (Program.playerLevel == 1)
            {
                return 8 + Program.GetAbilityModifier("Constitution");
            }
            else
            {
                return diceRollLastLevel + (Program.GetAbilityModifier("Constitution") * (Program.playerLevel - 1));
            }
        }
        public virtual int CalculateArmourClass()
        {
            return 10 + Program.GetAbilityModifier("Dexterity");
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
        public override Tuple<int, int> CalculateHitDice()
        {
            return new Tuple<int, int>(Program.playerLevel, 12);
        }
        public override int CalculateHitPoints()
        {
            if (Program.playerLevel == 1)
            {
                return 12 + Program.GetAbilityModifier("Constitution");
            } else
            {
                return diceRollLastLevel + (Program.GetAbilityModifier("Constitution") * (Program.playerLevel - 1));
            }
        }
        public override int CalculateArmourClass()
        {
            return 10 + Program.GetAbilityModifier("Constitution") + Program.GetAbilityModifier("Dexterity"); // TODO: Only when not wearing any armour
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
            Console.Clear();
            Console.WriteLine("Determining Starting Gold: Rolling 5 D4s...\n");
            List<int> diceRolled = Program.RollDice(true, new Tuple<int, int>(5, 4));
            Program.playerGold = diceRolled.Sum() * 10;
            Console.WriteLine($"\nYou will begin with a total of {Program.playerGold} gp.");
            Console.ReadLine();
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
            Console.Clear();
            Console.WriteLine("Determining Starting Gold: Rolling 2 D4s...\n");
            List<int> diceRolled = Program.RollDice(true, new Tuple<int, int>(2, 4));
            Program.playerGold = diceRolled.Sum() * 10;
            Console.WriteLine($"\nYou will begin with a total of {Program.playerGold} gp.");
            Console.ReadLine();
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
            return new Tuple<int, int>(Program.playerLevel, 10);
        }
        public override int CalculateHitPoints()
        {
            if (Program.playerLevel == 1)
            {
                return 10 + Program.GetAbilityModifier("Constitution");
            }
            else
            {
                return diceRollLastLevel + (Program.GetAbilityModifier("Constitution") * (Program.playerLevel - 1));
            }
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
            Console.Clear();
            Console.WriteLine("Determining Starting Gold: Rolling 5 D4s...\n");
            List<int> diceRolled = Program.RollDice(true, new Tuple<int, int>(5, 4));
            Program.playerGold = diceRolled.Sum() * 10;
            Console.WriteLine($"\nYou will begin with a total of {Program.playerGold} gp.");
            Console.ReadLine();
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
            return 10 + Program.GetAbilityModifier("Dexterity") + Program.GetAbilityModifier("Wisdom"); // TODO: Only while not wearing armour or sheild
        }
        public override void PlayerCreation()
        {
            Program.AddProficiency("Simple Weapons");
            Program.AddProficiency("Shortswords");
            Program.AddProficiency("Strength");
            Program.AddProficiency("Dexterity");
            bool selectedArtisanTools = false;
            List<string> artisanTools = new List<string>() { "Alchemist's Supplies", "Brewer's Supplies", "Caligrapher's Supplies", "Carpenter's Tools", "Cartographer's Tools", "Cobbler's Tools",
                "Cook's Utensils", "Glassblower's Tools", "Jeweler's Tools", "Leatherworker's Tools", "Mason's Tools", "Painter's Supplies", "Potter's Tools", "Smith's Tools", "Tinker's Tools",
                "Weaver's Tools", "Woodcarver's Tools" };
            while (!selectedArtisanTools)
            {
                Console.Clear();
                foreach (string toolSet in artisanTools)
                {
                    if (!Program.playerProficiencies.Contains(toolSet))
                    {
                        Console.WriteLine(toolSet);
                    }
                }
                Console.Write("\nWhich tool set would you like to be proficient in?: ");
                string response = Console.ReadLine().ToLower();
                foreach (string toolSet in artisanTools)
                {
                    string shortForm = toolSet.Substring(0, toolSet.IndexOf('\''));
                    if (!Program.playerProficiencies.Contains(toolSet) && (response == shortForm.ToLower() || response == toolSet.ToLower()))
                    {
                        Program.AddProficiency(toolSet);
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
            Console.WriteLine("Determining Starting Gold: Rolling 5 D4s...\n");
            List<int> diceRolled = Program.RollDice(true, new Tuple<int, int>(5, 4));
            Program.playerGold = diceRolled.Sum();
            Console.WriteLine($"\nYou will begin with a total of {Program.playerGold} gp.");
            Console.ReadLine();
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
            return new Tuple<int, int>(Program.playerLevel, 10);
        }
        public override int CalculateHitPoints()
        {
            if (Program.playerLevel == 1)
            {
                return 10 + Program.GetAbilityModifier("Constitution");
            }
            else
            {
                return diceRollLastLevel + (Program.GetAbilityModifier("Constitution") * (Program.playerLevel - 1));
            }
        }
        public override void PlayerCreation()
        {
            Program.AddProficiency("Light Armour");
            Program.AddProficiency("Medium Armour");
            Program.AddProficiency("Heavy Armour");
            Program.AddProficiency("Simple Weapons");
            Program.AddProficiency("Martial Weapons");
            Program.AddProficiency("Wisdom");
            Program.AddProficiency("Charisma");
            int selectedSkills = 0;
            List<Program.Skill> allowedSkills = new List<Program.Skill>() { Program.Skill.ATHLETICS, Program.Skill.INSIGHT, Program.Skill.INTIMIDATION, Program.Skill.MEDICINE, 
                Program.Skill.PERSUASION, Program.Skill.RELIGION };
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
            Console.WriteLine("Determining Starting Gold: Rolling 5 D4s...\n");
            List<int> diceRolled = Program.RollDice(true, new Tuple<int, int>(5, 4));
            Program.playerGold = diceRolled.Sum() * 10;
            Console.WriteLine($"\nYou will begin with a total of {Program.playerGold} gp.");
            Console.ReadLine();
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
            return new Tuple<int, int>(Program.playerLevel, 10);
        }
        public override int CalculateHitPoints()
        {
            if (Program.playerLevel == 1)
            {
                return 10 + Program.GetAbilityModifier("Constitution");
            }
            else
            {
                return diceRollLastLevel + (Program.GetAbilityModifier("Constitution") * (Program.playerLevel - 1));
            }
        }
        public override void PlayerCreation()
        {
            Program.AddProficiency("Light Armour");
            Program.AddProficiency("Medium Armour");
            Program.AddProficiency("Shields");
            Program.AddProficiency("Simple Weapons");
            Program.AddProficiency("Martial Weapons");
            Program.AddProficiency("Strength");
            Program.AddProficiency("Dexterity");
            int selectedSkills = 0;
            List<Program.Skill> allowedSkills = new List<Program.Skill>() { Program.Skill.ANIMALS,  Program.Skill.ATHLETICS, Program.Skill.INSIGHT, Program.Skill.INVESTIGATION, Program.Skill.NATURE,
                Program.Skill.PERCEPTION, Program.Skill.STEALTH, Program.Skill.SURVIVAL };
            while (selectedSkills < 3)
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
            Console.WriteLine("Determining Starting Gold: Rolling 5 D4s...\n");
            List<int> diceRolled = Program.RollDice(true, new Tuple<int, int>(5, 4));
            Program.playerGold = diceRolled.Sum() * 10;
            Console.WriteLine($"\nYou will begin with a total of {Program.playerGold} gp.");
            Console.ReadLine();
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
            Program.playerRace.languages.Add(Program.Language.THIEVESCANT);
            Program.AddProficiency("Light Armour");
            Program.AddProficiency("Simple Weapons");
            Program.AddProficiency("Hand Crossbows");
            Program.AddProficiency("Longswords");
            Program.AddProficiency("Rapiers");
            Program.AddProficiency("Shortswords");
            Program.AddProficiency("Thieve's Tools");
            Program.AddProficiency("Dexterity");
            Program.AddProficiency("Intelligence");
            int selectedSkills = 0;
            List<Program.Skill> allowedSkills = new List<Program.Skill>() { Program.Skill.ACROBATICS,  Program.Skill.ATHLETICS, Program.Skill.DECEPTION, Program.Skill.INSIGHT, 
                Program.Skill.INTIMIDATION, Program.Skill.INVESTIGATION, Program.Skill.PERCEPTION, Program.Skill.PERFORMANCE, Program.Skill.PERSUASION, Program.Skill.SLEIGHTOFHAND, 
                Program.Skill.STEALTH };
            while (selectedSkills < 4)
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
            int selectedExpertises = 0;
            List<string> skillNames = new List<string>();
            foreach (Program.Skill skill in Program.allSkills)
            {
                skillNames.Add(Program.GetDescription(skill));
            }
            while (selectedExpertises < 2)
            {
                Console.Clear();
                foreach (string proficiency in Program.playerProficiencies)
                {
                    if ((proficiency == "Thieve's Tools" || skillNames.Contains(proficiency)) && !doubleProficiency.Contains(proficiency))
                    {
                        Console.WriteLine(proficiency);
                    }
                }
                Console.Write("\nWhich of these proficiencies would you like expertise in?: ");
                string response = Console.ReadLine().ToLower();
                if ((response == "thieve" || response == "thieve's tools") && !doubleProficiency.Contains("Thieve's Tools"))
                {
                    doubleProficiency.Add("Thieve's Tools");
                    selectedExpertises++;
                    continue;
                }
                foreach (string proficiency in Program.playerProficiencies)
                {
                    if (skillNames.Contains(proficiency) && !doubleProficiency.Contains(proficiency) && response == proficiency.ToLower())
                    {
                        doubleProficiency.Add(proficiency);
                        selectedExpertises++;
                        break;
                    }
                }
            }
            Console.Clear();
            Console.WriteLine("Determining Starting Gold: Rolling 4 D4s...\n");
            List<int> diceRolled = Program.RollDice(true, new Tuple<int, int>(4, 4));
            Program.playerGold = diceRolled.Sum() * 10;
            Console.WriteLine($"\nYou will begin with a total of {Program.playerGold} gp.");
            Console.ReadLine();
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
            return new Tuple<int, int>(Program.playerLevel, 6);
        }
        public override int CalculateHitPoints()
        {
            if (Program.playerLevel == 1)
            {
                return 7 + Program.GetAbilityModifier("Constitution");
            }
            else
            {
                return diceRollLastLevel + (Program.GetAbilityModifier("Constitution") * (Program.playerLevel - 1)) + Program.playerLevel;
            }
        }
        public override int CalculateArmourClass()
        {
            return 13 + Program.GetAbilityModifier("Dexterity"); // TODO: Not when wearing armour
        }
        public override void PlayerCreation()
        {
            Program.AddProficiency("Daggers");
            Program.AddProficiency("Darts");
            Program.AddProficiency("Slings");
            Program.AddProficiency("Quaterstaffs");
            Program.AddProficiency("Light Crossbows");
            Program.AddProficiency("Constitution");
            Program.AddProficiency("Charisma");
            int selectedSkills = 0;
            List<Program.Skill> allowedSkills = new List<Program.Skill>() { Program.Skill.ARCANA, Program.Skill.DECEPTION, Program.Skill.INSIGHT,
                Program.Skill.INTIMIDATION, Program.Skill.PERSUASION, Program.Skill.RELIGION };
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
            bool selectedType = false;
            while (!selectedType)
            {
                Console.Clear();
                foreach (Dragonborn.DragonType type in Dragonborn.allTypes)
                {
                    Console.WriteLine($"{type.typeName} dragons are resistant to {type.damageResist.ToLower()} damage.\n");
                }
                Console.Write("Which colour dragon was your ancestors?: ");
                string response = Console.ReadLine();
                foreach (Dragonborn.DragonType type in Dragonborn.allTypes)
                {
                    if (response.ToLower() == type.typeName.ToLower())
                    {
                        Console.Write($"\nDo you want the colour of your dragon ancestors to be {type.typeName}?: ");
                        if (Program.CheckConfirmation())
                        {
                            draconicAncestor = type;
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
                    if (spell.spellLevel == 0 && !Program.playerCantrips.Contains(spell))
                    {
                        Console.WriteLine(spell.spellName);
                    }
                }
                Console.Write("\nWhich of these Cantrips do you want to know?: ");
                string response = Console.ReadLine().ToLower();
                foreach (Spell spell in Program.sorcererSpells)
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
            while (selectedSpells < 2)
            {
                Console.Clear();
                foreach (Spell spell in Program.sorcererSpells)
                {
                    if (spell.spellLevel == 1 && !Program.playerKnownSpells.Contains(spell))
                    {
                        Console.WriteLine(spell.spellName);
                    }
                }
                Console.Write("\nWhich of these Spells do you want to know?: ");
                string response = Console.ReadLine().ToLower();
                foreach (Spell spell in Program.sorcererSpells)
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
            Console.WriteLine("Determining Starting Gold: Rolling 3 D4s...\n");
            List<int> diceRolled = Program.RollDice(true, new Tuple<int, int>(3, 4));
            Program.playerGold = diceRolled.Sum() * 10;
            Console.WriteLine($"\nYou will begin with a total of {Program.playerGold} gp.");
            Console.ReadLine();
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
            Program.AddProficiency("Light Armour");
            Program.AddProficiency("Simple Weapons");
            Program.AddProficiency("Wisdom");
            Program.AddProficiency("Charisma");
            int selectedSkills = 0;
            List<Program.Skill> allowedSkills = new List<Program.Skill>() { Program.Skill.ARCANA, Program.Skill.DECEPTION, Program.Skill.HISTORY,
                Program.Skill.INTIMIDATION, Program.Skill.INVESTIGATION, Program.Skill.NATURE, Program.Skill.RELIGION };
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
                foreach (Spell spell in Program.warlockSpells)
                {
                    if (spell.spellLevel == 0 && !Program.playerCantrips.Contains(spell))
                    {
                        Console.WriteLine(spell.spellName);
                    }
                }
                Console.Write("\nWhich of these Cantrips do you want to know?: ");
                string response = Console.ReadLine().ToLower();
                foreach (Spell spell in Program.warlockSpells)
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
            while (selectedSpells < 2)
            {
                Console.Clear();
                foreach (Spell spell in Program.warlockSpells)
                {
                    if (spell.spellLevel == 1 && !Program.playerKnownSpells.Contains(spell))
                    {
                        Console.WriteLine(spell.spellName);
                    }
                }
                Console.Write("\nWhich of these Spells do you want to know?: ");
                string response = Console.ReadLine().ToLower();
                foreach (Spell spell in Program.warlockSpells)
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
            Console.WriteLine("Determining Starting Gold: Rolling 4 D4s...\n");
            List<int> diceRolled = Program.RollDice(true, new Tuple<int, int>(4, 4));
            Program.playerGold = diceRolled.Sum() * 10;
            Console.WriteLine($"\nYou will begin with a total of {Program.playerGold} gp.");
            Console.ReadLine();
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
            Program.AddProficiency("Daggers");
            Program.AddProficiency("Darts");
            Program.AddProficiency("Slings");
            Program.AddProficiency("Quarterstaffs");
            Program.AddProficiency("Light Crossbows");
            Program.AddProficiency("Intelligence");
            Program.AddProficiency("Wisdom");
            int selectedSkills = 0;
            List<Program.Skill> allowedSkills = new List<Program.Skill>() { Program.Skill.ARCANA, Program.Skill.HISTORY, Program.Skill.INSIGHT, Program.Skill.INVESTIGATION, 
                Program.Skill.MEDICINE, Program.Skill.RELIGION };
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
                foreach (Spell spell in Program.wizardSpells)
                {
                    if (spell.spellLevel == 0 && !Program.playerCantrips.Contains(spell))
                    {
                        Console.WriteLine(spell.spellName);
                    }
                }
                Console.Write("\nWhich of these Cantrips do you want to know?: ");
                string response = Console.ReadLine().ToLower();
                foreach (Spell spell in Program.wizardSpells)
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
            while (selectedSpells < 6)
            {
                Console.Clear();
                foreach (Spell spell in Program.wizardSpells)
                {
                    if (spell.spellLevel == 1 && !Program.playerKnownSpells.Contains(spell))
                    {
                        Console.WriteLine(spell.spellName);
                    }
                }
                Console.Write("\nWhich of these Spells do you want to know?: ");
                string response = Console.ReadLine().ToLower();
                foreach (Spell spell in Program.wizardSpells)
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
            Console.WriteLine("Determining Starting Gold: Rolling 4 D4s...\n");
            List<int> diceRolled = Program.RollDice(true, new Tuple<int, int>(4, 4));
            Program.playerGold = diceRolled.Sum() * 10;
            Console.WriteLine($"\nYou will begin with a total of {Program.playerGold} gp.");
            Console.ReadLine();
        }
        public override Tuple<int, int> CalculateHitDice()
        {
            return new Tuple<int, int>(Program.playerLevel, 6);
        }
        public override int CalculateHitPoints()
        {
            if (Program.playerLevel == 1)
            {
                return 6 + Program.GetAbilityModifier("Constitution");
            }
            else
            {
                return diceRollLastLevel + (Program.GetAbilityModifier("Constitution") * (Program.playerLevel - 1));
            }
        }
    }
}
