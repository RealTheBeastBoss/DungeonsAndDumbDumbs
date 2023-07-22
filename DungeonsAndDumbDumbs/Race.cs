using System;
using System.Collections.Generic;

namespace DungeonsAndDumbDumbs
{
    class Race
    {
        public string raceName;
        public string infoText;
        public bool hasDarkvision = false;
        public List<Program.Language> languages = new List<Program.Language>();
        public virtual void PlayerCreation()
        {

        }
    }
    class Dragonborn : Race
    {
        public class DragonType
        {
            public string typeName;
            public string damageResist;
            public int breathDistance;
            public string breathSaveThrow;
            public DragonType(string name, string damage, int distance, string save)
            {
                typeName = name;
                damageResist = damage;
                breathDistance = distance;
                breathSaveThrow = save;
            }
        }
        public static DragonType black = new DragonType("Black", "Acid", 30, "Dexterity");
        public static DragonType blue = new DragonType("Blue", "Lightning", 30, "Dexterity");
        public static DragonType brass = new DragonType("Brass", "Fire", 30, "Dexterity");
        public static DragonType bronze = new DragonType("Bronze", "Lightning", 30, "Dexterity");
        public static DragonType copper = new DragonType("Copper", "Acid", 30, "Dexterity");
        public static DragonType gold = new DragonType("Gold", "Fire", 15, "Dexterity");
        public static DragonType green = new DragonType("Green", "Poison", 15, "Constitution");
        public static DragonType red = new DragonType("Red", "Fire", 15, "Dexterity");
        public static DragonType silver = new DragonType("Silver", "Cold", 15, "Constitution");
        public static DragonType white = new DragonType("White", "Cold", 15, "Constitution");
        public static List<DragonType> allTypes = new List<DragonType>() { black, blue, brass, bronze, copper, gold, green, red, silver, white };
        public DragonType dragonType;
        public Dragonborn()
        {
            raceName = "Dragonborn";
            infoText = "Your draconic ancestry gives you a specific damage resistance and means you have the\nability to breath an elemental power.";
            languages.Add(Program.Language.COMMON);
            languages.Add(Program.Language.DRACONIC);
        }
        public override void PlayerCreation()
        {
            bool selectedType = false;
            while (!selectedType)
            {
                Console.Clear();
                foreach (DragonType type in allTypes)
                {
                    Console.WriteLine($"{type.typeName} dragons are resistant to {type.damageResist.ToLower()} damage.\nThey can breath {type.damageResist.ToLower()} up to {type.breathDistance}ft away, " +
                        $"this requires enemies to make a {type.breathSaveThrow.ToLower()} saving throw.\n");
                }
                Console.Write("Which colour dragon will you be?: ");
                string response = Console.ReadLine();
                foreach (DragonType type in allTypes)
                {
                    if (response.ToLower() == type.typeName.ToLower())
                    {
                        Console.Write($"\nDo you want the colour of your dragon to be {type.typeName}?: ");
                        if (Program.CheckConfirmation())
                        {
                            Dragonborn player = (Dragonborn)Program.playerRace;
                            player.dragonType = type;
                            selectedType = true;
                            break;
                        }
                    }
                }
            }
        }
    }
    class HillDwarf : Race
    {
        public HillDwarf()
        {
            raceName = "Hill Dwarf";
            infoText = "As you spend long stretches of time in dark places, you have naturally good vision in the dark.\n" +
            "You are particularly resistant to poison, as well as a proficiency with some axes, hammers and tools. \nYou are also naturally tough and are good at working with stone.";
            hasDarkvision = true;
            languages.Add(Program.Language.COMMON);
            languages.Add(Program.Language.DWARVISH);
        }
        public override void PlayerCreation()
        {
            Program.AddAbilityScore("Constitution", 2);
            Program.AddAbilityScore("Wisdom", 1);
            bool selectedProficiency = false;
            while (!selectedProficiency)
            {
                Console.Clear();
                Console.WriteLine("Brewer's Supplies\nSmith's Tools\nMason's Tools\n");
                Console.Write("Which of these would you like proficiency with?: ");
                string response = Console.ReadLine().ToLower();
                if (response == "brewer" || response == "brewer's supplies")
                {
                    Program.AddProficiency("Brewer's Supplies");
                    selectedProficiency = true;
                } else if (response == "mason" || response == "mason's tools")
                {
                    Program.AddProficiency("Mason's Tools");
                    selectedProficiency = true;
                } else if (response == "smith" || response == "smith's tools")
                {
                    Program.AddProficiency("Smith's Tools");
                    selectedProficiency = true;
                }
            }
        }
    }
    class MountainDwarf : Race
    {
        public MountainDwarf()
        {
            raceName = "Mountain Dwarf";
            infoText = "As you spend long stretches of time in dark places, you have naturally good vision in the dark.\n" +
            "You are particularly resistant to poison, as well as a proficiency with some axes, hammers and tools. \nYou are also good at working with stone and armour.";
            hasDarkvision = true;
            languages.Add(Program.Language.COMMON);
            languages.Add(Program.Language.DWARVISH);
        }
        public override void PlayerCreation()
        {
            Program.AddAbilityScore("Strength", 2);
            Program.AddAbilityScore("Constitution", 2);
            bool selectedProficiency = false;
            while (!selectedProficiency)
            {
                Console.Clear();
                Console.WriteLine("Brewer's Supplies\nSmith's Tools\nMason's Tools\n");
                Console.Write("Which of these would you like proficiency with?: ");
                string response = Console.ReadLine().ToLower();
                if (response == "brewer" || response == "brewer's supplies")
                {
                    Program.AddProficiency("Brewer's Supplies");
                    selectedProficiency = true;
                }
                else if (response == "mason" || response == "mason's tools")
                {
                    Program.AddProficiency("Mason's Tools");
                    selectedProficiency = true;
                }
                else if (response == "smith" || response == "smith's tools")
                {
                    Program.AddProficiency("Smith's Tools");
                    selectedProficiency = true;
                }
            }
        }
    }
    class HighElf : Race
    {
        public HighElf()
        {
            raceName = "High Elf";
            infoText = "As you typically live in forests of twilight, you can see well in the dark as well as being perceptive and intelligent.\n" +
            "As an elf, magic doesn't affect you as much as others. Your sleep is only 4 hours a day, rather than 8.\nElves are proficient with some swords and bows, as well as " +
            "being able to have a cantrip available.";
            hasDarkvision = true;
            languages.Add(Program.Language.COMMON);
            languages.Add(Program.Language.ELVISH);
        }
        public override void PlayerCreation() // Selecting a Cantrip
        {
            Program.AddAbilityScore("Dexterity", 2);
            Program.AddAbilityScore("Intelligence", 1);
            bool selectedCantrip = false;
            while (!selectedCantrip)
            {
                Console.Clear();
                foreach (Spell spell in Program.wizardSpells)
                {
                    if (spell.spellLevel == 0) // Only Cantrips
                    {
                        Console.WriteLine(spell.spellName);
                    }
                }
                Console.Write("\nWhich Cantrip do you want to be able to use?: ");
                string response = Console.ReadLine();
                foreach (Spell spell in Program.wizardSpells)
                {
                    if (spell.spellLevel == 0 && response.ToLower() == spell.spellName.ToLower())
                    {
                        Console.WriteLine($"You have selected \"{spell.spellName}\", here is some information on the spell:\n\n{spell.spellDescription}\n");
                        Console.Write($"Do you want to have {spell.spellName} as your Cantrip?: ");
                        if (Program.CheckConfirmation())
                        {
                            Program.playerCantrips.Add(spell);
                            selectedCantrip = true;
                            break;
                        }
                    }
                }
                Console.Clear();
            }
        }
    }
    class WoodElf : Race
    {
        public WoodElf()
        {
            raceName = "Wood Elf";
            infoText = "As you typically live in forests of twilight, you can see well in the dark as well as being perceptive and wise.\n" +
            "As an elf, magic doesn't affect you as much as others. Your sleep is only 4 hours a day, rather than 8.\nElves are proficient with some swords and bows, as well as " +
            "being fast and are good at camoflague.";
            hasDarkvision = true;
            languages.Add(Program.Language.COMMON);
            languages.Add(Program.Language.ELVISH);
        }
        public override void PlayerCreation()
        {
            Program.AddAbilityScore("Dexterity", 2);
            Program.AddAbilityScore("Wisdom", 1);
        }
    }
    class RockGnome : Race
    {
        public RockGnome()
        {
            raceName = "Rock Gnome";
            infoText = "As you spend long stretches of time in dark places, you have naturally good vision in the dark.\n" +
            "You are intelligent, wise, and well-versed with being attacked by magic as well as having a lot of knowledge\non the subject. Gnomes also have a great ability to create " +
            "clockwork devices using gold.";
            hasDarkvision = true;
            languages.Add(Program.Language.COMMON);
            languages.Add(Program.Language.GNOMISH);
            languages.Add(Program.Language.UNDERCOMMON);
        }
    }
    class HalfElf : Race
    {
        public HalfElf()
        {
            raceName = "Half-Elf";
            infoText = "With a natural charisma, you can get your way around a couple of subjects you wouldn't otherwise know.\nThe elf in you " +
            "allows you to see well in the dark and are opposed to magic.";
            hasDarkvision = true;
            languages.Add(Program.Language.COMMON);
            languages.Add(Program.Language.ELVISH);
        }
        public override void PlayerCreation()
        {
            Program.playerCharisma += 2;
            List<string> abilities = new List<string>() { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom" };
            bool selectedAbilities = false;
            while (!selectedAbilities)
            {
                Console.Clear();
                foreach (string ability in abilities)
                {
                    Console.WriteLine(ability);
                }
                Console.Write("\nWhich of these abilities do you want to start better at?: ");
                string response = Console.ReadLine().ToLower();
                foreach (string ability in abilities)
                {
                    if (response == ability.ToLower()) 
                    {
                        Program.AddAbilityScore(ability, 1);
                        abilities.Remove(ability);
                        if (abilities.Count == 3) selectedAbilities = true;
                        break;
                    }
                }
            }
            int selectedSkills = 0;
            while (selectedSkills < 2)
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
                        Program.playerProficiencies.Add(Program.GetDescription(skill));
                        selectedSkills++;
                        break;
                    }
                }
            }
            bool selectedLanguage = false;
            while (!selectedLanguage)
            {
                Console.Clear();
                foreach (Program.Language language in Program.allLanguages)
                {
                    if (!languages.Contains(language))
                    {
                        Console.WriteLine(Program.GetDescription(language));
                    }
                }
                Console.Write("\nWhich of these languages would you want to learn?: ");
                string response = Console.ReadLine().ToLower();
                foreach (Program.Language language in Program.allLanguages)
                {
                    if (!languages.Contains(language) && response == Program.GetDescription(language).ToLower())
                    {
                        languages.Add(language);
                        selectedLanguage = true;
                        break;
                    }
                }
            }
        }
    }
    class HalfOrc : Race
    {
        public HalfOrc()
        {
            raceName = "Half-Orc";
            infoText = "As an Orc-kind, you can see well in the dark. You are also very intimidating and\nhave high endurance. You can savagely attack to " +
            "deal more damage.";
            hasDarkvision = true;
            languages.Add(Program.Language.COMMON);
            languages.Add(Program.Language.ORC);
        }
    }
    class LightHalfling : Race
    {
        public LightHalfling()
        {
            raceName = "Lightfoot Halfling";
            infoText = "Lightfoot Halflings are naturally lucky, brave, nimble, and stealthy.";
            languages.Add(Program.Language.COMMON);
            languages.Add(Program.Language.HALFLING);
        }
        public override void PlayerCreation()
        {
            Program.AddAbilityScore("Dexterity", 2);
            Program.AddAbilityScore("Charisma", 1);
        }
    }
    class StoutHalfling : Race
    {
        public StoutHalfling()
        {
            raceName = "Stout Halfling";
            infoText = "Stout Halflings are naturally lucky, brave, nimble, and resistant to poisons.";
            languages.Add(Program.Language.COMMON);
            languages.Add(Program.Language.HALFLING);
        }
        public override void PlayerCreation()
        {
            Program.AddAbilityScore("Dexterity", 2);
            Program.AddAbilityScore("Constitution", 1);
        }
    }
    class Human : Race
    {
        public Human()
        {
            raceName = "Human";
            infoText = "Humans are very well-rounded creatures and have capacity for languages.";
            languages.Add(Program.Language.COMMON);
        }
        public override void PlayerCreation()
        {
            Program.AddAbilityScore("All", 1);
            bool selectedLanguage = false;
            while (!selectedLanguage)
            {
                Console.Clear();
                foreach (Program.Language language in Program.allLanguages)
                {
                    if (!languages.Contains(language))
                    {
                        Console.WriteLine(Program.GetDescription(language));
                    }
                }
                Console.Write("\nWhich of these languages would you want to learn?: ");
                string response = Console.ReadLine().ToLower();
                foreach (Program.Language language in Program.allLanguages)
                {
                    if (!languages.Contains(language) && response == Program.GetDescription(language).ToLower())
                    {
                        languages.Add(language);
                        selectedLanguage = true;
                        break;
                    }
                }
            }
        }
    }
    class VariantHuman : Race
    {
        public VariantHuman()
        {
            raceName = "Variant Human";
            infoText = "This breed of human has more capacity for mastering a skill. Is good at close-combat with enemies.";
            languages.Add(Program.Language.COMMON);
        }
        public override void PlayerCreation()
        {
            bool selectedLanguage = false;
            while (!selectedLanguage)
            {
                Console.Clear();
                foreach (Program.Language language in Program.allLanguages)
                {
                    if (!languages.Contains(language))
                    {
                        Console.WriteLine(Program.GetDescription(language));
                    }
                }
                Console.Write("\nWhich of these languages would you want to learn?: ");
                string response = Console.ReadLine().ToLower();
                foreach (Program.Language language in Program.allLanguages)
                {
                    if (!languages.Contains(language) && response == Program.GetDescription(language).ToLower())
                    {
                        languages.Add(language);
                        selectedLanguage = true;
                        break;
                    }
                }
            }
            List<string> abilities = new List<string>() { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" };
            bool selectedAbilities = false;
            while (!selectedAbilities)
            {
                Console.Clear();
                foreach (string ability in abilities)
                {
                    Console.WriteLine(ability);
                }
                Console.Write("\nWhich of these abilities do you want to start better at?: ");
                string response = Console.ReadLine().ToLower();
                foreach (string ability in abilities)
                {
                    if (response == ability.ToLower())
                    {
                        Program.AddAbilityScore(ability, 1);
                        abilities.Remove(ability);
                        if (abilities.Count == 4) selectedAbilities = true;
                        break;
                    }
                }
            }
            int selectedSkills = 0;
            while (selectedSkills < 1)
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
        }
    }
    class Tiefling : Race
    {
        public Tiefling()
        {
            raceName = "Tiefling";
            infoText = "Tieflings can see in the dark as well as being fire resistant. You also have some demon-like magic.";
            hasDarkvision = true;
            languages.Add(Program.Language.COMMON);
            languages.Add(Program.Language.INFERNAL);
        }
    }
}
