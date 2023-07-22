using System;
using System.Collections.Generic;
using System.Linq;
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
    }
    class Fighter : Class
    {
        public Fighter()
        {
            className = "Fighter";
            classDescription = "A master of martial combat, skilled with a variety of weapons and armor.";
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
