using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace DungeonsAndDumbDumbs
{
    class Program
    {
        // Meta-game Variables:
        public static Random RNG = new Random();
        public static List<string> confirmations = new List<string>() { "yes", "yep", "ok", "sure", "alright", "yeah", "y" };
        public static List<Tuple<string, Type>> allRaces = new List<Tuple<string, Type>>() { new Tuple<string, Type>("Dragonborn", typeof(Dragonborn)), 
            new Tuple<string, Type>("Hill Dwarf", typeof(HillDwarf)), new Tuple<string, Type>("Mounain Dwarf", typeof(MountainDwarf)), new Tuple<string, Type>("High Elf", typeof(HighElf)),
            new Tuple<string, Type>("Wood Elf", typeof(WoodElf)), new Tuple<string, Type>("Rock Gnome", typeof(RockGnome)), new Tuple<string, Type>("Half-Elf", typeof(HalfElf)),
            new Tuple<string, Type>("Half-Orc", typeof(HalfOrc)), new Tuple<string, Type>("Lightfoot Halfling", typeof(LightHalfling)), 
            new Tuple<string, Type>("Stout Halfling", typeof(StoutHalfling)), new Tuple<string, Type>("Human", typeof(Human)), new Tuple<string, Type>("Variant Human", typeof(VariantHuman)),
            new Tuple<string, Type>("Tiefling", typeof(Tiefling)) };
        public static List<Tuple<string, Type>> allClasses = new List<Tuple<string, Type>>() { new Tuple<string, Type>("Barbarian", typeof(Barbarian)), new Tuple<string, Type>("Bard", typeof(Bard)),
            new Tuple<string, Type>("Cleric", typeof(Cleric)), new Tuple<string, Type>("Druid", typeof(Druid)), new Tuple<string, Type>("Fighter", typeof(Fighter)),
            new Tuple<string, Type>("Monk", typeof(Monk)), new Tuple<string, Type>("Paladin", typeof(Paladin)), new Tuple<string, Type>("Ranger", typeof(Ranger)),
            new Tuple<string, Type>("Rogue", typeof(Rogue)), new Tuple<string, Type>("Sorcerer", typeof(Sorcerer)), new Tuple<string, Type>("Warlock", typeof(Warlock)),
            new Tuple<string, Type>("Wizard", typeof(Wizard)) };
        public enum Language
        {
            [Description("Common")]
            COMMON,
            [Description("Draconic")]
            DRACONIC,
            [Description("Dwarvish")]
            DWARVISH,
            [Description("Elvish")]
            ELVISH,
            [Description("Gnomish")]
            GNOMISH,
            [Description("Undercommon")]
            UNDERCOMMON,
            [Description("Orc")]
            ORC,
            [Description("Halfling")]
            HALFLING,
            [Description("Infernal")]
            INFERNAL,
            [Description("Druidic")]
            DRUIDIC
        }
        public static List<Language> allLanguages = new List<Language>() { Language.COMMON, Language.DRACONIC, Language.DWARVISH, Language.ELVISH, Language.GNOMISH, Language.UNDERCOMMON, Language.ORC, 
            Language.HALFLING, Language.INFERNAL };
        public enum Skill
        {
            [Description("Acrobatics")]
            ACROBATICS,
            [Description("Animal Handling")]
            ANIMALS,
            [Description("Arcana")]
            ARCANA,
            [Description("Athletics")]
            ATHLETICS,
            [Description("Deception")]
            DECEPTION,
            [Description("History")]
            HISTORY,
            [Description("Insight")]
            INSIGHT,
            [Description("Intimidation")]
            INTIMIDATION,
            [Description("Investigation")]
            INVESTIGATION,
            [Description("Medicine")]
            MEDICINE,
            [Description("Nature")]
            NATURE,
            [Description("Perception")]
            PERCEPTION,
            [Description("Preformance")]
            PERFORMANCE,
            [Description("Persuasion")]
            PERSUASION,
            [Description("Religion")]
            RELIGION,
            [Description("Sleight of Hand")]
            SLEIGHTOFHAND,
            [Description("Stealth")]
            STEALTH,
            [Description("Survival")]
            SURVIVAL
        }
        public static List<Skill> allSkills = new List<Skill>() { Skill.ACROBATICS, Skill.ANIMALS, Skill.ARCANA, Skill.ATHLETICS, Skill.DECEPTION, Skill.HISTORY, Skill.INSIGHT,
            Skill.INTIMIDATION, Skill.INVESTIGATION, Skill.MEDICINE, Skill.NATURE, Skill.PERCEPTION, Skill.PERFORMANCE, Skill.PERSUASION, Skill.RELIGION, Skill.SLEIGHTOFHAND, Skill.STEALTH,
            Skill.SURVIVAL };
        public enum DamageType
        {

        }
        public static Spell acidSplash = new Spell("Acid Splash", 0, Spell.AcidSplash, 60, "You hurl a bubble of acid. Choose one or two creatures you can see within range. " +
            "A target must succeed on a\nDexterity saving throw or take 1d6 acid damage.");
        public static Spell animalFriendship = new Spell("Animal Friendship", 1, Spell.AnimalFriendship, 30, "Charm a beast nearby to you for 24 hours.");
        public static Spell bane = new Spell("Bane", 1, Spell.Bane, 30, "Up to three creatures in range will need to subtract a D4 to an attack roll or saving throw on their next turn.");
        public static Spell bless = new Spell("Bless", 1, Spell.Bless, 30, "Up to three creatures in range will add a D4 to an attack roll or saving throw on their next turn.");
        public static Spell charmPerson = new Spell("Charm Person", 1, Spell.CharmPerson, 30, "Charm a nearby person for an hour. Once the spell is done, they know you cast it on them.");
        public static Spell command = new Spell("Command", 1, Spell.Command, 60, "Send a one-word command to an enemy and they may spend their next turn following it.");
        public static Spell comprehendLanguage = new Spell("Comprehend Languages", 1, Spell.ComprehendLanguages, 1, "Cast this to be able to understand any language you hear/see for an hour.", true);
        public static Spell createOrDestroyWater = new Spell("Create or Destroy Water", 1, Spell.CreateDestroyWater, 30, "You are able to create or destroy up to 10 gallons of water" +
            ". You can also destroy fog.");
        public static Spell cureWounds = new Spell("Cure Wounds", 1, Spell.CureWounds, 1, "Slightly heal someone you can touch.");
        public static Spell dancingLights = new Spell("Dancing Lights", 0, Spell.DancingLights, 120, "Dancing lights appear around you, allowing you to see around you.");
        public static Spell detectGoodAndEvil = new Spell("Detect Good and Evil", 1, Spell.DetectGoodEvil, 30, "You know any good or evil energy in the area around you.");
        public static Spell detectMagic = new Spell("Detect Magic", 1, Spell.DetectMagic, 30, "You know any magical presence around you.", true);
        public static Spell detectPoison = new Spell("Detect Poison and Disease", 1, Spell.DetectPoison, 30, "You know of any poisons or diseases in your area.", true);
        public static Spell fireBolt = new Spell("Fire Bolt", 0, Spell.FireBolt, 120, "You hurl a mote of fire at a creature or object within range. Make a ranged spell attack against the " +
            "target. On a hit,\nthe target takes 1d10 fire damage. A flammable object hit by this spell ignites if it isn't being worn or carried.");
        public static Spell guidance = new Spell("Guidance", 0, Spell.Guidance, 1, "You touch a friend before they make an ability check and they add a D4 to the roll.");
        public static Spell guidingBolt = new Spell("Guiding Bolt", 1, Spell.GuidingBolt, 120, "Deals 4 D6 damage and makes it easier for the next damage.");
        public static Spell healingWord = new Spell("Healing Word", 1, Spell.HealingWord, 60, "Heals someone around you.");
        public static Spell inflictWounds = new Spell("Inflict Wounds", 1, Spell.InflictWounds, 1, "Melee attacks someone with 3 D10 Necrotic Damage.");
        public static Spell light = new Spell("Light", 0, Spell.Light, 1, "You touch an object around you. That object glows for about 40ft.");
        public static Spell mageHand = new Spell("Mage Hand", 0, Spell.MageHand, 30, "A spectral hand appears that you can control for your action.");
        public static Spell mending = new Spell("Mending", 0, Spell.Mending, 1, "Touching an object will repair minor damage, but not restore magic.");
        public static Spell minorIllusion = new Spell("Minor Illusion", 0, Spell.MinorIllusion, 30, "You can cast an illusion that can attempt to confuse or distract an enemy.");
        public static Spell poisonSpray = new Spell("Poison Spray", 0, Spell.PoisonSpray, 10, "You extend your hand toward a creature you can see within range and project a puff of noxious gas from your palm.\n" +
            "The creature must succeed on a Constitution saving throw or take 1d12 poison damage.");
        public static Spell prestidigitation = new Spell("Prestidigitation", 0, Spell.Prestidigitation, 10, "You can cast a variety of novel tricks.");
        public static Spell purifyFoodDrink = new Spell("Purify Food and Drink", 1, Spell.PurifyFoodDrink, 5, "You can remove poison and disease from non-magic food around you.", true);
        public static Spell rayOfFrost = new Spell("Ray of Frost", 0, Spell.RayOfFrost, 60, "A frigid beam of blue-white light streaks toward a creature within range. Make a ranged spell " +
            "attack against the\ntarget. On a hit, it takes 1d8 cold damage, and its speed is reduced by 10 feet until the start of your next turn.");
        public static Spell resistance = new Spell("Resistance", 0, Spell.Resistance, 1, "You touch a friend before they make a saving throw and they add a D4 to the saving throw.");
        public static Spell shieldOfFaith = new Spell("Shield of Faith", 1, Spell.ShieldOfFaith, 60, "Grant +2 to a creatures AC for 10 minutes.");
        public static Spell shockingGrasp = new Spell("Shocking Grasp", 0, Spell.ShockingGrasp, 1, "You cause melee spell lightning damage to a nearby enemy.");
        public static Spell spareTheDying = new Spell("Spare the Dying", 0, Spell.SpareTheDying, 1, "Touching someone who is at 0HP, they become stable again.");
        public static Spell thaumaturgy = new Spell("Thaumaturgy", 0, Spell.Thaumaturgy, 30, "You can cause minor disturbances around you.");
        public static List<Spell> allSpells = new List<Spell>() { animalFriendship, acidSplash, bane, bless, charmPerson, command, comprehendLanguage, createOrDestroyWater, cureWounds, 
            dancingLights, detectGoodAndEvil, detectMagic, detectPoison, fireBolt, guidance, guidingBolt, healingWord, inflictWounds, light, mageHand, mending, minorIllusion, poisonSpray, 
            prestidigitation, purifyFoodDrink, rayOfFrost, resistance, shieldOfFaith, shockingGrasp, spareTheDying };
        public static List<Spell> wizardSpells = new List<Spell>() { acidSplash, charmPerson, comprehendLanguage, dancingLights, detectMagic, fireBolt, light, mageHand, mending, minorIllusion, 
            poisonSpray, prestidigitation, rayOfFrost, shockingGrasp };
        public static List<Spell> bardSpells = new List<Spell>() { animalFriendship, bane, charmPerson, comprehendLanguage, cureWounds, dancingLights, detectMagic, healingWord, light, mageHand, mending, 
            minorIllusion, prestidigitation };
        public static List<Spell> clericSpells = new List<Spell>() { bane, bless, command, createOrDestroyWater, cureWounds, detectGoodAndEvil, detectMagic, detectPoison, guidance, guidingBolt, 
            healingWord, inflictWounds, light, mending, purifyFoodDrink, resistance, shieldOfFaith, spareTheDying };
        public static List<Spell> druidSpells = new List<Spell>() { animalFriendship, charmPerson, createOrDestroyWater, cureWounds, detectMagic, detectPoison, guidance, healingWord, mending, 
            poisonSpray, purifyFoodDrink, resistance };
        // Player Variables:
        public static string playerName;
        public static Race playerRace;
        public static Class playerClass;
        public static List<Spell> playerCantrips = new List<Spell>();
        public static List<Spell> playerKnownSpells = new List<Spell>();
        public static List<string> playerProficiencies = new List<string>();
        public static int playerProficiencyBonus = 2;
        public static int playerStrength = 0;
        public static int playerDexterity = 0;
        public static int playerConstitution = 0;
        public static int playerIntelligence = 0;
        public static int playerWisdom = 0;
        public static int playerCharisma = 0;
        public static int playerLevel = 1;
        public static int playerXP = 0;
        public static int playerGold = 0;
        public static int playerSilver = 0;
        public static int playerCopper = 0;

        static void Main(string[] args)
        {
            Console.Write("What is the name of your character?: ");
            playerName = FormatName(Console.ReadLine());
            bool selectedRace = false;
            while (!selectedRace)
            {
                Console.Clear();
                foreach (Tuple<string, Type> race in allRaces)
                {
                    Console.WriteLine(race.Item1);
                }
                Console.Write("\nWhich of these races do you wish to be?: ");
                string response = Console.ReadLine();
                foreach (Tuple<string, Type> race in allRaces)
                {
                    if (response.ToLower() == race.Item1.ToLower())
                    {
                        playerRace = (Race)Activator.CreateInstance(race.Item2);
                        Console.WriteLine($"You've chosen the race \"{playerRace.raceName}\", here is some more information about them:\n\n{playerRace.infoText}\n");
                        Console.Write("Do you wish for this to be your race?: ");
                        if (CheckConfirmation())
                        {
                            selectedRace = true;
                        }
                        break;
                    }
                }
            }
            playerRace.PlayerCreation();
            bool selectedClass = false;
            while (!selectedClass)
            {
                Console.Clear();
                foreach (Tuple<string, Type> clazz in allClasses)
                {
                    Console.WriteLine(clazz.Item1);
                }
                Console.Write("\nWhich of these classes do you want to be?: ");
                string response = Console.ReadLine().ToLower();
                foreach (Tuple<string, Type> clazz in allClasses)
                {
                    if (response == clazz.Item1.ToLower())
                    {
                        playerClass = (Class)Activator.CreateInstance(clazz.Item2);
                        Console.WriteLine($"You have selected \"{clazz.Item1}\", here is some information about that:\n\n{playerClass.classDescription}\n");
                        Console.Write($"Do you want your class to be {clazz.Item1}?: ");
                        if (CheckConfirmation())
                        {
                            selectedClass = true;
                        }
                        break;
                    }
                }
            }
            playerClass.PlayerCreation();
            Console.Clear();
            Console.ReadLine();
        }

        static string FormatName(string name)
        {
            string startName = name;
            name = name.ToLower();
            string returnName = "";
            bool canCapital = true;
            for (int i = 0; i < name.Length; i++)
            {
                char c = name[i];
                if (canCapital)
                {
                    returnName += c.ToString().ToUpper();
                    canCapital = false;
                } else
                {
                    returnName += c;
                    if (c == ' ')
                    {
                        canCapital = true;
                    }
                }
            }
            Console.Write($"Do you want the name to be formatted from \"{startName}\" to \"{returnName}\"?: ");
            if (CheckConfirmation())
            {
                return returnName;
            }
            return startName;
        }

        public static bool CheckConfirmation()
        {
            List<string> responseWords = SplitIntoWords(Console.ReadLine());
            foreach (string word in responseWords)
            {
                if (confirmations.Contains(word.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        static List<string> SplitIntoWords(string sentence)
        {
            List<string> returnList = new List<string>();
            string currentWord = "";
            for (int i = 0; i < sentence.Length; i++)
            {
                char c = sentence[i];
                if (Char.IsLetterOrDigit(c))
                {
                    currentWord += c;
                    if (i == sentence.Length - 1)
                    {
                        returnList.Add(currentWord);
                    }
                } else if (currentWord.Length > 0)
                {
                    returnList.Add(currentWord);
                    currentWord = "";
                }
            }
            return returnList;
        }

        public static void AddAbilityScore(string ability, int score)
        {
            switch(ability)
            {
                case "Strength":
                    playerStrength += score;
                    break;
                case "Dexterity":
                    playerDexterity += score;
                    break;
                case "Constitution":
                    playerConstitution += score;
                    break;
                case "Intelligence":
                    playerIntelligence += score;
                    break;
                case "Wisdom":
                    playerWisdom += score;
                    break;
                case "Charisma":
                    playerCharisma += score;
                    break;
                case "All":
                    playerStrength += score;
                    playerDexterity += score;
                    playerConstitution += score;
                    playerIntelligence += score;
                    playerWisdom += score;
                    playerCharisma += score;
                    break;
            }
        }

        public static int GetAbilityModifier(string abilityName)
        {
            int abilityScore = 1;
            switch (abilityName)
            {
                case "Strength":
                    abilityScore = playerStrength;
                    break;
                case "Dexterity":
                    abilityScore = playerDexterity;
                    break;
                case "Constitution":
                    abilityScore = playerConstitution;
                    break;
                case "Intelligence":
                    abilityScore = playerIntelligence;
                    break;
                case "Wisdom":
                    abilityScore = playerWisdom;
                    break;
                case "Charisma":
                    abilityScore = playerCharisma;
                    break;
            }
            return Convert.ToInt32(Math.Floor(Convert.ToDouble((abilityScore - 10) / 2)));
        }

        public static List<int> RollDice(bool displayValues, params Tuple<int, int>[] diceSets)
        {
            List<int> diceValues = new List<int>();
            foreach (Tuple<int, int> set in diceSets)
            {
                for (int i = 0; i < set.Item1; i++)
                {
                    int diceFace = RNG.Next(1, set.Item2 + 1);
                    if (displayValues)
                    {
                        Console.WriteLine($"You rolled a D{set.Item2} for a value of {diceFace}");
                    }
                    diceValues.Add(diceFace);
                }
            }
            return diceValues;
        }

        public static void AddProficiency(string proficiency)
        {
            if (!playerProficiencies.Contains(proficiency))
            {
                playerProficiencies.Add(proficiency);
            }
        }

        public static string GetDescription(Enum en)
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString()); // haha, "GetMember" lmao

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return en.ToString();
        }
    }
}
