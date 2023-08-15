using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace DungeonsAndDumbDumbs
{
    class TimePassedEventArgs : EventArgs
    {
        public float minutesPassed { get; set; }
    }
    class Program
    {
        // Colour Code: Good - Green, Bad - Red, Game Info - Yellow, Player Input and Options - Blue, Prompt - Cyan, Incorrect Response - Magenta
        // Meta Game Variables:
        public static event EventHandler<TimePassedEventArgs> TimePassed;
        public static void OnTimePassed(float minutesToPass)
        {
            TimePassedEventArgs eventData = new TimePassedEventArgs
            {
                minutesPassed = minutesToPass
            };
            TimePassed?.Invoke(Program.player, eventData);
        }
        public static Random RNG = new Random();
        public static Dictionary<int, int> scoreToModifier = new Dictionary<int, int>() { { 1, -5 }, { 2, -4 }, { 3, -4 }, { 4, -3 }, { 5, -3 }, { 6, -2 }, { 7, -2 }, { 8, -1 }, { 9, -1 }, 
            { 10, 0 }, { 11, 0 }, { 12, 1 }, { 13, 1 }, { 14, 2 }, { 15, 2 }, { 16, 3 }, { 17, 3 }, { 18, 4 }, { 19, 4 }, { 20, 5 }, { 21, 5 }, { 22, 6 }, { 23, 6 }, { 24, 7 }, { 25, 7 }, 
            { 26, 8 }, { 27, 8 }, { 28, 9 }, { 29, 9 }, { 30, 10 } };
        public static Dictionary<int, int> leveltoXP = new Dictionary<int, int>() { { 2, 300 }, { 3, 900 }, { 4, 2700 }, { 5, 6500 }, { 6, 14000 }, { 7, 23000 }, { 8, 34000 }, { 9, 48000 }, 
            { 10, 64000 }, {11, 85000 }, { 12, 100000 }, { 13, 120000 }, { 14, 140000 }, { 15, 165000 }, { 16, 195000 }, { 17, 225000 }, { 18, 265000 }, { 19, 305000 }, { 20, 355000 } };
        public static List<string> confirmations = new List<string>() { "yes", "yep", "ok", "sure", "alright", "yeah", "y", "indeed" };
        public static List<Tuple<string, Type>> allRaces = new List<Tuple<string, Type>>() { new Tuple<string, Type>("Dragonborn", typeof(Dragonborn)), 
            new Tuple<string, Type>("Hill Dwarf", typeof(HillDwarf)), new Tuple<string, Type>("Mountain Dwarf", typeof(MountainDwarf)), new Tuple<string, Type>("High Elf", typeof(HighElf)),
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
            DRUIDIC,
            [Description("Thieve's Cant")]
            THIEVESCANT
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
            [Description("Performance")]
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
        public static List<Tuple<Skill, string>> allSkillsAbilities = new List<Tuple<Skill, string>>() { new Tuple<Skill, string>(Skill.ACROBATICS, "Dexterity"),
            new Tuple<Skill, string>(Skill.ANIMALS, "Wisdom"), new Tuple<Skill, string>(Skill.ARCANA, "Intelligence"), new Tuple<Skill, string>(Skill.ATHLETICS, "Strength"), 
            new Tuple<Skill, string>(Skill.DECEPTION, "Charisma"), new Tuple<Skill, string>(Skill.HISTORY, "Intelligence"), new Tuple<Skill, string>(Skill.INSIGHT, "Wisdom"), 
            new Tuple<Skill, string>(Skill.INTIMIDATION, "Charisma"), new Tuple<Skill, string>(Skill.INVESTIGATION, "Intelligence"), new Tuple<Skill, string>(Skill.MEDICINE, "Wisdom"), 
            new Tuple<Skill, string>(Skill.NATURE, "Intelligence"), new Tuple<Skill, string>(Skill.PERCEPTION, "Wisdom"), new Tuple<Skill, string>(Skill.PERFORMANCE, "Charisma"), 
            new Tuple<Skill, string>(Skill.PERSUASION, "Charisma"), new Tuple<Skill, string>(Skill.RELIGION, "Intelligence"), new Tuple<Skill, string>(Skill.SLEIGHTOFHAND, "Dexterity"), 
            new Tuple<Skill, string>(Skill.STEALTH, "Dexterity"), new Tuple<Skill, string>(Skill.SURVIVAL, "Wisdom") };
        public enum GameState
        {
            COMBAT,
            INTERACTION,
            FREE
        }
        public enum Effect
        {
            [Description("Blindness")]
            BLIND
        }
        public enum DamageType
        {
            [Description("Acid")]
            ACID,
            [Description("Bludgeoning")]
            BLUDGEONING,
            [Description("Cold")]
            COLD,
            [Description("Fire")]
            FIRE,
            [Description("Force")]
            FORCE,
            [Description("Lightning")]
            LIGHTNING,
            [Description("Necrotic")]
            NECROTIC,
            [Description("Piercing")]
            PIERCING,
            [Description("Poison")]
            POISON,
            [Description("Psychic")]
            PSYCHIC,
            [Description("Radiant")]
            RADIANT,
            [Description("Slashing")]
            SLASHING,
            [Description("Thunder")]
            THUNDER
        }
        public enum Terrain
        {
            [Description("Arctic")]
            ARCTIC,
            [Description("Coast")]
            COAST,
            [Description("Desert")]
            DESERT,
            [Description("Forest")]
            FOREST,
            [Description("Grassland")]
            GRASSLAND,
            [Description("Inside")]
            INSIDE,
            [Description("Mountain")]
            MOUNTAIN,
            [Description("Swamp")]
            SWAMP,
            [Description("Underdark")]
            UNDERDARK
        }
        public static List<Terrain> naturalTerrain = new List<Terrain>() { Terrain.ARCTIC, Terrain.COAST, Terrain.DESERT, Terrain.FOREST, Terrain.GRASSLAND, Terrain.MOUNTAIN, Terrain.SWAMP, 
            Terrain.UNDERDARK };
        // Game Spells:
        public static Spell acidSplash = new Spell("Acid Splash", 0, Spell.AcidSplash, 60, "You hurl a bubble of acid. Choose one or two creatures you can see within range. " +
            "A target must succeed on a\nDexterity saving throw or take 1d6 acid damage.", new List<GameState>() { GameState.COMBAT });

        public static Spell animalFriendship = new Spell("Animal Friendship", 1, Spell.AnimalFriendship, 30, "Charm a beast nearby to you for 24 hours.");
        public static Spell bane = new Spell("Bane", 1, Spell.Bane, 30, "Up to three creatures in range will need to subtract a D4 to an attack roll or saving throw on their next turn.", 
            new List<GameState>() { GameState.COMBAT });

        public static Spell bless = new Spell("Bless", 1, Spell.Bless, 30, "Up to three creatures in range will add a D4 to an attack roll or saving throw on their next turn.", 
            new List<GameState>() { GameState.COMBAT });

        public static Spell burningHands = new Spell("Burning Hands", 1, Spell.BurningHands, 15, "Each creature within 15ft may take 3D6 fire damage. On higher levels, the damage increases by " +
            "one D6 for each higher level.", new List<GameState>() { GameState.COMBAT });

        public static Spell charmPerson = new Spell("Charm Person", 1, Spell.CharmPerson, 30, "Charm a nearby person for an hour. Once the spell is done, they know you cast it on them.");
        public static Spell colourSpray = new Spell("Colour Spray", 1, Spell.ColourSpray, 15, "Creatures around you can get blinded.", new List<GameState>() { GameState.COMBAT });
        public static Spell command = new Spell("Command", 1, Spell.Command, 60, "Send a one-word command to an enemy and they may spend their next turn following it.", 
            new List<GameState>() { GameState.COMBAT });

        public static Spell comprehendLanguage = new Spell("Comprehend Languages", 1, Spell.ComprehendLanguages, 1, "Cast this to be able to understand any language you hear/see for an hour.", null,
            Spell.Type.ACTION, true);

        public static Spell createOrDestroyWater = new Spell("Create or Destroy Water", 1, Spell.CreateDestroyWater, 30, "You are able to create or destroy up to 10 gallons of water" +
            ". You can also destroy fog.");

        public static Spell cureWounds = new Spell("Cure Wounds", 1, Spell.CureWounds, 1, "Slightly heal someone you can touch.");
        public static Spell dancingLights = new Spell("Dancing Lights", 0, Spell.DancingLights, 120, "Dancing lights appear around you, allowing you to see around you.");
        public static Spell detectGoodAndEvil = new Spell("Detect Good and Evil", 1, Spell.DetectGoodEvil, 30, "You know any good or evil energy in the area around you.");
        public static Spell detectMagic = new Spell("Detect Magic", 1, Spell.DetectMagic, 30, "You know any magical presence around you.", null, Spell.Type.ACTION, true);
        public static Spell detectPoison = new Spell("Detect Poison and Disease", 1, Spell.DetectPoison, 30, "You know of any poisons or diseases in your area.", null, Spell.Type.ACTION, true);
        public static Spell divineFavour = new Spell("Divine Favour", 1, Spell.DivineFavour, 1, "For a minute, your weapon attacks will deal a d4 of Radiant damage.", 
            new List<GameState>() { GameState.COMBAT }, Spell.Type.BONUS);

        public static Spell entangle = new Spell("Entangle", 1, Spell.Entangle, 90, "You can create 20ft of difficult terrain and trap monsters in that area.", 
            new List<GameState>() { GameState.COMBAT });

        public static Spell expeditiousRetreat = new Spell("Expeditious Retreat", 1, Spell.ExpeditiousRetreat, 1, "When this spell is cast and for bonus actions each turn, you can dash.",
            new List<GameState>() { GameState.COMBAT }, Spell.Type.BONUS);

        public static Spell fireBolt = new Spell("Fire Bolt", 0, Spell.FireBolt, 120, "You hurl a mote of fire at a creature or object within range. Make a ranged spell attack against the " +
            "target. On a hit,\nthe target takes 1d10 fire damage. A flammable object hit by this spell ignites if it isn't being worn or carried.", new List<GameState>() { GameState.COMBAT });

        public static Spell guidance = new Spell("Guidance", 0, Spell.Guidance, 1, "You touch a friend before they make an ability check and they add a D4 to the roll.");
        public static Spell guidingBolt = new Spell("Guiding Bolt", 1, Spell.GuidingBolt, 120, "Deals 4 D6 damage and makes it easier for the next damage.", new List<GameState>() { GameState.COMBAT });
        public static Spell healingWord = new Spell("Healing Word", 1, Spell.HealingWord, 60, "Heals someone around you.", new List<GameState>() { GameState.COMBAT }, Spell.Type.BONUS);
        public static Spell hellishRebuke = new Spell("Hellish Rebuke", 1, Spell.HellishRebuke, 60, "The creature that damaged you makes a Dex save and takes 2d10 fire damage. Higher levels add a " +
            "d10.", new List<GameState>() { GameState.COMBAT }, Spell.Type.REACTION);

        public static Spell inflictWounds = new Spell("Inflict Wounds", 1, Spell.InflictWounds, 1, "Melee attacks someone with 3 D10 Necrotic Damage.", new List<GameState>() { GameState.COMBAT });
        public static Spell light = new Spell("Light", 0, Spell.Light, 1, "You touch an object around you. That object glows for about 40ft.");
        public static Spell mageArmour = new Spell("Mage Armour", 1, Spell.MageArmour, 1, "Touching a willing creature who isn't wearing armour will increase their AC.");
        public static Spell mageHand = new Spell("Mage Hand", 0, Spell.MageHand, 30, "A spectral hand appears that you can control for your action.");
        public static Spell magicMissile = new Spell("Magic Missile", 1, Spell.MagicMissle, 120, "Three magic missiles appear and you can send them at creatures in the area. They deal 1D4 + 1 " +
            "damage each.\nAt higher levels, you get an additional missile for each extra level.", new List<GameState>() { GameState.COMBAT });

        public static Spell mending = new Spell("Mending", 0, Spell.Mending, 1, "Touching an object will repair minor damage, but not restore magic.");
        public static Spell minorIllusion = new Spell("Minor Illusion", 0, Spell.MinorIllusion, 30, "You can cast an illusion that can attempt to confuse or distract an enemy.");
        public static Spell poisonSpray = new Spell("Poison Spray", 0, Spell.PoisonSpray, 10, "You extend your hand toward a creature you can see within range and project a puff of noxious gas from your palm.\n" +
            "The creature must succeed on a Constitution saving throw or take 1d12 poison damage.", new List<GameState>() { GameState.COMBAT });

        public static Spell prestidigitation = new Spell("Prestidigitation", 0, Spell.Prestidigitation, 10, "You can cast a variety of novel tricks.");
        public static Spell purifyFoodDrink = new Spell("Purify Food and Drink", 1, Spell.PurifyFoodDrink, 5, "You can remove poison and disease from non-magic food around you.", null, 
            Spell.Type.ACTION, true);
        public static Spell protectionEvilGood = new Spell("Protection From Evil and Good", 1, Spell.ProtectionFromEvilGood, 1, "You can touch a creature to protect them from aberrations, celestials, " +
            "elementals, fey, fiends, and undead.", new List<GameState>() { GameState.COMBAT });

        public static Spell rayOfFrost = new Spell("Ray of Frost", 0, Spell.RayOfFrost, 60, "A frigid beam of blue-white light streaks toward a creature within range. Make a ranged spell " +
            "attack against the\ntarget. On a hit, it takes 1d8 cold damage, and its speed is reduced by 10 feet until the start of your next turn.", new List<GameState>() { GameState.COMBAT });

        public static Spell resistance = new Spell("Resistance", 0, Spell.Resistance, 1, "You touch a friend before they make a saving throw and they add a D4 to the saving throw.");
        public static Spell shieldOfFaith = new Spell("Shield of Faith", 1, Spell.ShieldOfFaith, 60, "Grant +2 to a creatures AC for 10 minutes.", null, Spell.Type.BONUS);
        public static Spell shockingGrasp = new Spell("Shocking Grasp", 0, Spell.ShockingGrasp, 1, "You cause melee spell lightning damage to a nearby enemy.", new List<GameState>() { GameState.COMBAT });
        public static Spell spareTheDying = new Spell("Spare the Dying", 0, Spell.SpareTheDying, 1, "Touching someone who is at 0HP, they become stable again.");
        public static Spell thaumaturgy = new Spell("Thaumaturgy", 0, Spell.Thaumaturgy, 30, "You can cause minor disturbances around you.");
        public static Spell thunderWave = new Spell("Thunderwave", 1, Spell.ThunderWave, 15, "Each creature within range may take 2D8 thunder damage, and be pushed 10ft away.\nAt higher levels, " +
            "it will do an extra D8 of damage for each extra level.", new List<GameState>() { GameState.COMBAT });

        public static Spell trueStrike = new Spell("True Strike", 0, Spell.TrueStrike, 30, "You pick a target within range and you can see their defenses. You then get advantage on\n" +
            "an attack roll if made next turn.", new List<GameState>() { GameState.COMBAT });

        public static Spell viciousMockery = new Spell("Vicious Mockery", 0, Spell.ViciousMockery, 60, "A creature you choose makes a Wisdom save and takes 1d4 Psychic damage and has a hard time " +
            "attacking next turn.", new List<GameState>() { GameState.COMBAT });
        // Game Spell Sets:
        public static List<Spell> allSpells = new List<Spell>() { animalFriendship, acidSplash, bane, bless, burningHands, charmPerson, colourSpray, command, comprehendLanguage, createOrDestroyWater, cureWounds, 
            dancingLights, detectGoodAndEvil, detectMagic, detectPoison, divineFavour, entangle, expeditiousRetreat, fireBolt, guidance, guidingBolt, healingWord, hellishRebuke, inflictWounds, light, mageArmour, mageHand, magicMissile, mending, minorIllusion, poisonSpray, 
            prestidigitation, purifyFoodDrink, protectionEvilGood, rayOfFrost, resistance, shieldOfFaith, shockingGrasp, spareTheDying, thaumaturgy, thunderWave, trueStrike, viciousMockery };

        public static List<Spell> wizardSpells = new List<Spell>() { acidSplash, burningHands, charmPerson, colourSpray, comprehendLanguage, dancingLights, detectMagic, expeditiousRetreat, fireBolt, light, mageArmour, mageHand, magicMissile, mending, 
            minorIllusion, poisonSpray, prestidigitation, protectionEvilGood, rayOfFrost, shockingGrasp, thunderWave, trueStrike };

        public static List<Spell> bardSpells = new List<Spell>() { animalFriendship, bane, charmPerson, comprehendLanguage, cureWounds, dancingLights, detectMagic, healingWord, light, mageHand, mending, 
            minorIllusion, prestidigitation, thunderWave, trueStrike, viciousMockery };

        public static List<Spell> clericSpells = new List<Spell>() { bane, bless, command, createOrDestroyWater, cureWounds, detectGoodAndEvil, detectMagic, detectPoison, guidance, guidingBolt, 
            healingWord, inflictWounds, light, mending, purifyFoodDrink, protectionEvilGood, resistance, shieldOfFaith, spareTheDying, thaumaturgy };

        public static List<Spell> druidSpells = new List<Spell>() { animalFriendship, charmPerson, createOrDestroyWater, cureWounds, detectMagic, detectPoison, entangle, guidance, healingWord, mending, 
            poisonSpray, purifyFoodDrink, resistance, thunderWave };

        public static List<Spell> paladinSpells = new List<Spell>() { bless, command, cureWounds, detectGoodAndEvil, detectMagic, detectPoison, divineFavour, protectionEvilGood, purifyFoodDrink, 
            shieldOfFaith };

        public static List<Spell> rangerSpells = new List<Spell>() { animalFriendship, cureWounds, detectMagic, detectPoison };

        public static List<Spell> sorcererSpells = new List<Spell>() { acidSplash, burningHands, colourSpray, dancingLights, charmPerson, comprehendLanguage, detectMagic, expeditiousRetreat, fireBolt, light, mageArmour, mageHand, magicMissile, mending, 
            minorIllusion, poisonSpray, prestidigitation, rayOfFrost, shockingGrasp, thunderWave, trueStrike };

        public static List<Spell> warlockSpells = new List<Spell>() { burningHands, charmPerson, command, comprehendLanguage, expeditiousRetreat, hellishRebuke, mageHand, minorIllusion, poisonSpray, 
            prestidigitation, protectionEvilGood, trueStrike };
        // Game Armour:
        public static Armour padded = new Armour("Padded Armour", new Tuple<int, string>(5, "Gold"), 11, new List<string>() { "Light Armour" }, 1, 1, true);
        public static Armour leather = new Armour("Leather Armour", new Tuple<int, string>(10, "Gold"), 11, new List<string>() { "Light Armour" }, 1, 1);
        public static Armour studdedLeather = new Armour("Studded Leather Armour", new Tuple<int, string>(45, "Gold"), 12, new List<string>() { "Light Armour" }, 1, 1);
        public static Armour hide = new Armour("Hide Armour", new Tuple<int, string>(10, "Gold"), 12, new List<string>() { "Medium Armour" }, 5, 1);
        public static Armour chainShirt = new Armour("Chain Shirt", new Tuple<int, string>(50, "Gold"), 13, new List<string>() { "Medium Armour" }, 5, 1);
        public static Armour scaleMail = new Armour("Scale Mail Armour", new Tuple<int, string>(50, "Gold"), 14, new List<string>() { "Medium Armour" }, 5, 1, true);
        public static Armour breastPlate = new Armour("Breast Plate", new Tuple<int, string>(400, "Gold"), 14, new List<string>() { "Medium Armour" }, 5, 1);
        public static Armour halfPlate = new Armour("Half Plate", new Tuple<int, string>(750, "Gold"), 15, new List<string>() { "Medium Armour" }, 5, 1, true);
        public static Armour ringMail = new Armour("Ring Mail Armour", new Tuple<int, string>(30, "Gold"), 14, new List<string>() { "Heavy Armour" }, 10, 5, true);
        public static Armour chainMail = new Armour("Chain Mail Armour", new Tuple<int, string>(75, "Gold"), 16, new List<string>() { "Heavy Armour" }, 10, 5, true, 13);
        public static Armour splint = new Armour("Splint Armour", new Tuple<int, string>(200, "Gold"), 17, new List<string>() { "Heavy Armour" }, 10, 5, true, 15);
        public static Armour plate = new Armour("Plate Armour", new Tuple<int, string>(1500, "Gold"), 18, new List<string>() { "Heavy Armour" }, 10, 5, true, 15);
        public static Armour shield = new Armour("Shield", new Tuple<int, string>(10, "Gold"), 2, new List<string>() { "Shields" }, 0, 0);
        // Game Weapons:
        public static Weapon club = new Weapon("Club", new Tuple<int, string>(1, "Silver"), new List<string>() { "Simple Weapons", "Clubs" }, new Tuple<int, int>(1, 4), DamageType.BLUDGEONING, 
            new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.LIGHT, Weapon.WeaponProperty.MELEE });

        public static Weapon dagger = new Weapon("Dagger", new Tuple<int, string>(2, "Gold"), new List<string>() { "Simple Weapons", "Daggers" }, new Tuple<int, int>(1, 4), DamageType.PIERCING,
            new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.LIGHT, Weapon.WeaponProperty.FINESSE, Weapon.WeaponProperty.THROWN, Weapon.WeaponProperty.MELEE }, 20, 60);

        public static Weapon greatClub = new Weapon("Great Club", new Tuple<int, string>(2, "Silver"), new List<string>() { "Simple Weapons", "Clubs" }, new Tuple<int, int>(1, 8), DamageType.BLUDGEONING,
            new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.TWOHANDED, Weapon.WeaponProperty.MELEE });

        public static Weapon handAxe = new Weapon("Hand Axe", new Tuple<int, string>(5, "Gold"), new List<string>() { "Simple Weapons" }, new Tuple<int, int>(1, 6), DamageType.SLASHING,
            new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.LIGHT, Weapon.WeaponProperty.THROWN, Weapon.WeaponProperty.MELEE }, 20, 60);

        public static Weapon javelin = new Weapon("Javelin", new Tuple<int, string>(5, "Silver"), new List<string>() { "Simple Weapons", "Javelins" }, new Tuple<int, int>(1, 6), DamageType.PIERCING,
            new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.THROWN, Weapon.WeaponProperty.MELEE }, 30, 120);

        public static Weapon lightHammer = new Weapon("Light Hammer", new Tuple<int, string>(2, "Gold"), new List<string>() { "Simple Weapons" }, new Tuple<int, int>(1, 4), DamageType.BLUDGEONING,
            new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.THROWN, Weapon.WeaponProperty.MELEE, Weapon.WeaponProperty.LIGHT }, 20, 60);

        public static Weapon mace = new Weapon("Mace", new Tuple<int, string>(5, "Gold"), new List<string>() { "Simple Weapons", "Maces" }, new Tuple<int, int>(1, 6), DamageType.BLUDGEONING,
            new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.MELEE });

        public static Weapon quarterStaff = new Weapon("Quarterstaff", new Tuple<int, string>(2, "Silver"), new List<string>() { "Simple Weapons", "Quarterstaffs" }, new Tuple<int, int>(1, 6), 
            DamageType.BLUDGEONING, new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.VERSATILE, Weapon.WeaponProperty.MELEE }, 5, 5, new Tuple<int, int>(1, 8));

        public static Weapon sickle = new Weapon("Sickle", new Tuple<int, string>(1, "Gold"), new List<string>() { "Simple Weapons", "Sickles" }, new Tuple<int, int>(1, 4), DamageType.SLASHING,
            new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.LIGHT, Weapon.WeaponProperty.MELEE });

        public static Weapon spear = new Weapon("Spear", new Tuple<int, string>(1, "Gold"), new List<string>() { "Simple Weapons", "Spears" }, new Tuple<int, int>(1, 6), DamageType.PIERCING,
            new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.THROWN, Weapon.WeaponProperty.MELEE, Weapon.WeaponProperty.VERSATILE }, 20, 60, new Tuple<int, int>(1, 8));

        public static Weapon lightCrossbow = new Weapon("Light Crossbow", new Tuple<int, string>(25, "Gold"), new List<string>() { "Simple Weapons", "Light Crossbows" }, new Tuple<int, int>(1, 8), 
            DamageType.PIERCING, new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.CROSSBOWAMMUNITION, Weapon.WeaponProperty.LOADING, Weapon.WeaponProperty.TWOHANDED }, 80, 320);

        public static Weapon dart = new Weapon("Dart", new Tuple<int, string>(5, "Copper"), new List<string>() { "Simple Weapons", "Darts" }, new Tuple<int, int>(1, 4),
            DamageType.PIERCING, new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.FINESSE, Weapon.WeaponProperty.THROWN }, 20, 60);

        public static Weapon shortbow = new Weapon("Shortbow", new Tuple<int, string>(25, "Gold"), new List<string>() { "Simple Weapons" }, new Tuple<int, int>(1, 6),
            DamageType.PIERCING, new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.ARROWAMMUNITION, Weapon.WeaponProperty.TWOHANDED }, 80, 320);

        public static Weapon sling = new Weapon("Sling", new Tuple<int, string>(1, "Silver"), new List<string>() { "Simple Weapons", "Slings" }, new Tuple<int, int>(1, 4),
            DamageType.BLUDGEONING, new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.SLINGBULLETAMMUNITION }, 30, 120);

        public static Weapon battleAxe = new Weapon("Battleaxe", new Tuple<int, string>(10, "Gold"), new List<string>() { "Martial Weapons" }, new Tuple<int, int>(1, 8), DamageType.SLASHING, 
            new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.VERSATILE, Weapon.WeaponProperty.MELEE }, 5, 5, new Tuple<int, int>(1, 10));

        public static Weapon flail = new Weapon("Flail", new Tuple<int, string>(10, "Gold"), new List<string>() { "Martial Weapons" }, new Tuple<int, int>(1, 8), DamageType.BLUDGEONING,
            new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.MELEE });

        public static Weapon glaive = new Weapon("Glaive", new Tuple<int, string>(20, "Gold"), new List<string>() { "Martial Weapons" }, new Tuple<int, int>(1, 10), DamageType.SLASHING,
            new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.MELEE, Weapon.WeaponProperty.HEAVY, Weapon.WeaponProperty.REACH, Weapon.WeaponProperty.TWOHANDED });

        public static Weapon greatAxe = new Weapon("Great Axe", new Tuple<int, string>(30, "Gold"), new List<string>() { "Martial Weapons" }, new Tuple<int, int>(1, 12), DamageType.SLASHING,
            new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.MELEE, Weapon.WeaponProperty.HEAVY, Weapon.WeaponProperty.TWOHANDED });

        public static Weapon greatSword = new Weapon("Great Sword", new Tuple<int, string>(50, "Gold"), new List<string>() { "Martial Weapons" }, new Tuple<int, int>(2, 6), DamageType.SLASHING,
            new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.MELEE, Weapon.WeaponProperty.HEAVY, Weapon.WeaponProperty.TWOHANDED });

        public static Weapon halberd = new Weapon("Halberd", new Tuple<int, string>(20, "Gold"), new List<string>() { "Martial Weapons" }, new Tuple<int, int>(1, 10), DamageType.SLASHING,
            new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.MELEE, Weapon.WeaponProperty.HEAVY, Weapon.WeaponProperty.REACH, Weapon.WeaponProperty.TWOHANDED });

        public static Weapon lance = new Weapon("Lance", new Tuple<int, string>(10, "Gold"), new List<string>() { "Martial Weapons" }, new Tuple<int, int>(1, 12), DamageType.PIERCING,
            new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.MELEE, Weapon.WeaponProperty.REACH, Weapon.WeaponProperty.LANCE });

        public static Weapon longSword = new Weapon("Longsword", new Tuple<int, string>(15, "Gold"), new List<string>() { "Martial Weapons", "Longswords" }, new Tuple<int, int>(1, 8), DamageType.SLASHING,
            new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.VERSATILE, Weapon.WeaponProperty.MELEE }, 5, 5, new Tuple<int, int>(1, 10));

        public static Weapon maul = new Weapon("Maul", new Tuple<int, string>(10, "Gold"), new List<string>() { "Martial Weapons" }, new Tuple<int, int>(2, 6), DamageType.BLUDGEONING,
            new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.MELEE, Weapon.WeaponProperty.HEAVY, Weapon.WeaponProperty.TWOHANDED });

        public static Weapon morningStar = new Weapon("Morningstar", new Tuple<int, string>(15, "Gold"), new List<string>() { "Martial Weapons" }, new Tuple<int, int>(1, 8), DamageType.PIERCING,
            new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.MELEE });

        public static Weapon pike = new Weapon("Pike", new Tuple<int, string>(5, "Gold"), new List<string>() { "Martial Weapons" }, new Tuple<int, int>(1, 10), DamageType.PIERCING,
            new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.MELEE, Weapon.WeaponProperty.HEAVY, Weapon.WeaponProperty.REACH, Weapon.WeaponProperty.TWOHANDED });

        public static Weapon rapier = new Weapon("Rapier", new Tuple<int, string>(25, "Gold"), new List<string>() { "Martial Weapons", "Rapiers" }, new Tuple<int, int>(1, 8), DamageType.PIERCING,
            new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.MELEE, Weapon.WeaponProperty.FINESSE });

        public static Weapon scimitar = new Weapon("Scimitar", new Tuple<int, string>(25, "Gold"), new List<string>() { "Martial Weapons", "Scimitars" }, new Tuple<int, int>(1, 6), DamageType.SLASHING,
            new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.MELEE, Weapon.WeaponProperty.FINESSE, Weapon.WeaponProperty.LIGHT });

        public static Weapon shortSword = new Weapon("Shortsword", new Tuple<int, string>(10, "Gold"), new List<string>() { "Martial Weapons", "Shortswords" }, new Tuple<int, int>(1, 6), DamageType.PIERCING,
            new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.MELEE, Weapon.WeaponProperty.FINESSE, Weapon.WeaponProperty.LIGHT });

        public static Weapon trident = new Weapon("Trident", new Tuple<int, string>(5, "Gold"), new List<string>() { "Martial Weapons" }, new Tuple<int, int>(1, 6), DamageType.PIERCING,
            new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.VERSATILE, Weapon.WeaponProperty.MELEE, Weapon.WeaponProperty.THROWN }, 20, 60, new Tuple<int, int>(1, 8));

        public static Weapon warPick = new Weapon("War Pick", new Tuple<int, string>(5, "Gold"), new List<string>() { "Martial Weapons" }, new Tuple<int, int>(1, 8), DamageType.PIERCING,
            new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.MELEE });

        public static Weapon warhammer = new Weapon("Warhammer", new Tuple<int, string>(15, "Gold"), new List<string>() { "Martial Weapons" }, new Tuple<int, int>(1, 8), DamageType.BLUDGEONING,
           new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.VERSATILE, Weapon.WeaponProperty.MELEE }, 5, 5, new Tuple<int, int>(1, 10));

        public static Weapon whip = new Weapon("Whip", new Tuple<int, string>(2, "Gold"), new List<string>() { "Martial Weapons" }, new Tuple<int, int>(1, 4), DamageType.SLASHING,
           new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.FINESSE, Weapon.WeaponProperty.REACH, Weapon.WeaponProperty.MELEE });

        public static Weapon blowGun = new Weapon("Blowgun", new Tuple<int, string>(10, "Gold"), new List<string>() { "Martial Weapons" }, new Tuple<int, int>(1, 1), DamageType.PIERCING, 
            new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.NEEDLEAMMUNITION, Weapon.WeaponProperty.LOADING }, 25, 100);

        public static Weapon handCrossbow = new Weapon("Hand Crossbow", new Tuple<int, string>(75, "Gold"), new List<string>() { "Martial Weapons", "Hand Crossbows" }, new Tuple<int, int>(1, 6), 
            DamageType.PIERCING, new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.CROSSBOWAMMUNITION, Weapon.WeaponProperty.LOADING, Weapon.WeaponProperty.LIGHT }, 30, 120);

        public static Weapon heavyCrossbow = new Weapon("Heavy Crossbow", new Tuple<int, string>(50, "Gold"), new List<string>() { "Martial Weapons" }, new Tuple<int, int>(1, 10), DamageType.PIERCING,
            new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.CROSSBOWAMMUNITION, Weapon.WeaponProperty.LOADING, Weapon.WeaponProperty.HEAVY, Weapon.WeaponProperty.TWOHANDED }, 100, 400);

        public static Weapon longBow = new Weapon("Longbow", new Tuple<int, string>(50, "Gold"), new List<string>() { "Martial Weapons" }, new Tuple<int, int>(1, 8), DamageType.PIERCING,
            new List<Weapon.WeaponProperty>() { Weapon.WeaponProperty.ARROWAMMUNITION, Weapon.WeaponProperty.HEAVY, Weapon.WeaponProperty.TWOHANDED }, 150, 600);

        public static Weapon net = new Weapon("Net", new Tuple<int, string>(1, "Gold"), new List<string>() { "Martial Weapons" }, null, DamageType.FORCE, new List<Weapon.WeaponProperty>(), 5, 15);

        public static List<Weapon> allWeapons = new List<Weapon>() { club, dagger, greatClub, handAxe, javelin, lightHammer, mace, quarterStaff, sickle, spear, lightCrossbow, dart, shortbow, sling, 
            battleAxe, flail, glaive, greatAxe, greatSword, halberd, lance, longSword, maul, morningStar, pike, rapier, scimitar, shortSword, trident, warPick, warhammer, whip, blowGun, handCrossbow, 
            heavyCrossbow, longBow, net };

        public static List<Weapon> allSimpleMeleeWeapons = new List<Weapon>() { club, dagger, greatClub, handAxe, javelin, lightHammer, mace, quarterStaff, sickle, spear };

        public static List<Weapon> allSimpleWeapons = new List<Weapon>(allSimpleMeleeWeapons) { lightCrossbow, dart, shortbow, sling };

        public static List<Weapon> allMartialMeleeWeapons = new List<Weapon>() { battleAxe, flail, glaive, greatAxe, greatSword, halberd, lance, longSword, maul, morningStar, pike, rapier, scimitar, 
            shortSword, trident, warPick, warhammer, whip };

        public static List<Weapon> allMartialWeapons = new List<Weapon>(allMartialMeleeWeapons) { blowGun, handCrossbow, heavyCrossbow, longBow, net };
        // Game Basic Equipment:
        public static Equipment arrow = new Equipment("Arrow", new Tuple<int, string>(1, "Gold"));
        public static Equipment blowgunNeedle = new Equipment("Blowgun Needle", new Tuple<int, string>(1, "Gold"));
        public static Equipment crossbowBolt = new Equipment("Crossbow Bolt", new Tuple<int, string>(1, "Gold"));
        public static Equipment slingBullet = new Equipment("Sling Bullet", new Tuple<int, string>(4, "Copper"));
        public static Equipment thievesTools = new Equipment("Thieve's Tools", new Tuple<int, string>(25, "Gold"));
        // Game Actions:
        public static GameAction showCharacterSheet = new GameAction(GameAction.ShowCharacterSheet);
        public static GameAction showInventory = new GameAction(GameAction.ShowInventory);
        public static GameAction useMagic = new GameAction(GameAction.UseMagic);
        // Basic Game Variables:
        public static Player player = new Player(new List<GameAction>() { showCharacterSheet, showInventory, useMagic });
        public static GameState currentState = GameState.FREE;
        public static bool foundAction = false;
        public static bool finishedCombatTurn = false;
        public static bool currentlyBonusAction = false;
        public static List<LivingCreature> combatTurnOrder = new List<LivingCreature>();

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("What is the name of your character?: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            player.name = FormatName(Console.ReadLine());
            bool selectedRace = false;
            while (!selectedRace)
            {
                Console.Clear();
                foreach (Tuple<string, Type> race in allRaces)
                {
                    Console.WriteLine(race.Item1);
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nWhich of these races do you wish to be?: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string response = Console.ReadLine();
                foreach (Tuple<string, Type> race in allRaces)
                {
                    if (response.ToLower() == race.Item1.ToLower())
                    {
                        player.characterRace = (Race)Activator.CreateInstance(race.Item2);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nYou've chosen the race \"{player.characterRace.raceName}\", here is some more information about them:\n\n{player.characterRace.infoText}\n");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("Do you wish for this to be your race?: ");
                        if (CheckConfirmation())
                        {
                            selectedRace = true;
                        }
                        break;
                    }
                }
            }
            player.characterRace.PlayerCreation();
            bool selectedClass = false;
            while (!selectedClass)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                foreach (Tuple<string, Type> clazz in allClasses)
                {
                    Console.WriteLine(clazz.Item1);
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nWhich of these classes do you want to be?: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string response = Console.ReadLine().ToLower();
                foreach (Tuple<string, Type> clazz in allClasses)
                {
                    if (response == clazz.Item1.ToLower())
                    {
                        player.characterClass = (Class)Activator.CreateInstance(clazz.Item2);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nYou have selected \"{clazz.Item1}\", here is some information about that:\n\n{player.characterClass.classDescription}\n");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write($"Do you want your class to be {clazz.Item1}?: ");
                        if (CheckConfirmation())
                        {
                            selectedClass = true;
                        }
                        break;
                    }
                }
            }
            player.characterClass.PlayerCreation();
        NotDecidedAbilityMethod:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Do you want to use the \"Dice Roll\" or the \"Point Buy\" method of determining ability scores?: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            string answer = Console.ReadLine().ToLower();
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (answer == "dice roll")
            {
                Console.WriteLine("\nThe Dice Roll method includes rolling 6 sets of 4D6, getting the sum of the 3 highest die in each set and allowing that to be a score.\n");
            } else if (answer == "point buy")
            {
                Console.WriteLine("\nThe Point Buy method includes having all ability base scores at 8, then allowing you to spend 27 points increasing the scores.\n");
            } else
            {
                goto NotDecidedAbilityMethod;
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Do you want to use the selected method?: ");
            if (!CheckConfirmation())
            {
                goto NotDecidedAbilityMethod;
            }
            if (answer == "point buy")
            {
                bool selectedAbilityScores = false;
                while (!selectedAbilityScores)
                {
                    int remainingPoints = 27;
                    List<string> abilityNames = new List<string>() { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" };
                    List<Tuple<string, int>> abilityScores = new List<Tuple<string, int>>();
                    foreach (string name in abilityNames)
                    {
                        NotAllocatedPoints:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Points Remaining: {remainingPoints}\n");
                        foreach (string title in abilityNames)
                        {
                            bool foundAbility = false;
                            foreach (Tuple<string, int> ability in abilityScores)
                            {
                                if (ability.Item1 == title)
                                {
                                    Console.WriteLine($"{ability.Item1} - {ability.Item2}");
                                    foundAbility = true;
                                    break;
                                }
                            }
                            if (!foundAbility) Console.WriteLine(title);
                        }
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\n8");
                        if (remainingPoints > 0) Console.WriteLine("9 (-1 Point)");
                        if (remainingPoints > 1) Console.WriteLine("10 (-2 Points)");
                        if (remainingPoints > 2) Console.WriteLine("11 (-3 Points)");
                        if (remainingPoints > 3) Console.WriteLine("12 (-4 Points)");
                        if (remainingPoints > 4) Console.WriteLine("13 (-5 Points)");
                        if (remainingPoints > 6) Console.WriteLine("14 (-7 Points)");
                        if (remainingPoints > 8) Console.WriteLine("15 (-9 Points)");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write($"\nWhich option will you choose for {name}?: ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        string response = Console.ReadLine();
                        switch (response)
                        {
                            case "8":
                                abilityScores.Add(new Tuple<string, int>(name, 8));
                                break;
                            case "9":
                                if (remainingPoints < 1) goto NotAllocatedPoints;
                                abilityScores.Add(new Tuple<string, int>(name, 9));
                                remainingPoints--;
                                break;
                            case "10":
                                if (remainingPoints < 2) goto NotAllocatedPoints;
                                abilityScores.Add(new Tuple<string, int>(name, 10));
                                remainingPoints -= 2;
                                break;
                            case "11":
                                if (remainingPoints < 3) goto NotAllocatedPoints;
                                abilityScores.Add(new Tuple<string, int>(name, 11));
                                remainingPoints -= 3;
                                break;
                            case "12":
                                if (remainingPoints < 4) goto NotAllocatedPoints;
                                abilityScores.Add(new Tuple<string, int>(name, 12));
                                remainingPoints -= 4;
                                break;
                            case "13":
                                if (remainingPoints < 5) goto NotAllocatedPoints;
                                abilityScores.Add(new Tuple<string, int>(name, 13));
                                remainingPoints -= 5;
                                break;
                            case "14":
                                if (remainingPoints < 7) goto NotAllocatedPoints;
                                abilityScores.Add(new Tuple<string, int>(name, 14));
                                remainingPoints -= 7;
                                break;
                            case "15":
                                if (remainingPoints < 9) goto NotAllocatedPoints;
                                abilityScores.Add(new Tuple<string, int>(name, 15));
                                remainingPoints -= 9;
                                break;
                            default:
                                goto NotAllocatedPoints;
                        }
                        if (abilityScores.Count == 6)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            foreach (Tuple<string, int> ability in abilityScores)
                            {
                                Console.WriteLine($"{ability.Item1} - {ability.Item2}");
                            }
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("\nIs this how you want your abilities to be handled?: ");
                            if (CheckConfirmation())
                            {
                                selectedAbilityScores = true;
                                foreach (Tuple<string, int> ability in abilityScores)
                                {
                                    AddAbilityScore(player, ability.Item1, ability.Item2);
                                }
                            }
                        }
                    }
                }
            } else if (answer == "dice roll")
            {
                List<int> abilityValues = new List<int>();
                for (int i = 0; i < 6; i++)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Rolling the dice to help determine ability scores. ({i + 1}/6)\n");
                    List<int> diceValues = RollDice(true, new Tuple<int, int>(4, 6));
                    diceValues.Sort();
                    diceValues.RemoveAt(0);
                    int diceTotal = 0;
                    foreach (int num in diceValues)
                    {
                        diceTotal += num;
                    }
                    abilityValues.Add(diceTotal);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\nThe sum of the 3 highest dice rolls is {diceTotal}, this is one of the scores you will be able to pick from.\n");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Press Enter to Continue: ");
                    Console.ReadLine();
                }
                bool selectedAbilityScores = false;
                while (!selectedAbilityScores)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("The next step is to choose what your ability scores are going to be.\n");
                    Console.Write("The scores you can choose from are: ");
                    int iterations = 1;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    foreach (int value in abilityValues)
                    {
                        Console.Write(value);
                        if (iterations != 6)
                        {
                            Console.Write(", ");
                        }
                        else
                        {
                            Console.WriteLine(".\n");
                        }
                        iterations++;
                    }
                    List<string> abilityNames = new List<string>() { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" };
                    List<Tuple<string, int>> abilityScores = new List<Tuple<string, int>>();
                    List<int> remainingValues = new List<int>(abilityValues);
                    int values = 0;
                    foreach (int value in abilityValues)
                    {
                        values++;
                        if (values == 6)
                        {
                            int score = 0;
                            foreach (int number in remainingValues)
                            {
                                score = number;
                            }
                            foreach (string name in abilityNames)
                            {
                                bool foundAbility = false;
                                foreach (Tuple<string, int> ability in abilityScores)
                                {
                                    if (ability.Item1 == name)
                                    {
                                        foundAbility = true;
                                        break;
                                    }
                                }
                                if (!foundAbility)
                                {
                                    abilityScores.Add(new Tuple<string, int>(name, score));
                                    break;
                                }
                            }
                            break;
                        }
                        bool selectedAbility = false;
                        while (!selectedAbility)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("The scores you have yet to assign are: ");
                            iterations = 1;
                            foreach (int remaining in remainingValues)
                            {
                                Console.Write(remaining);
                                if (iterations != remainingValues.Count)
                                {
                                    Console.Write(", ");
                                }
                                else
                                {
                                    Console.WriteLine(".\n");
                                }
                                iterations++;
                            }
                            foreach (string name in abilityNames)
                            {
                                bool foundAbility = false;
                                foreach (Tuple<string, int> ability in abilityScores)
                                {
                                    if (ability.Item1 == name)
                                    {
                                        Console.WriteLine($"{ability.Item1} - {ability.Item2}");
                                        foundAbility = true;
                                        break;
                                    }
                                }
                                if (!foundAbility) Console.WriteLine(name);
                            }
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write($"\nWhich ability would you like to give a score of {value}?: ");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            string response = Console.ReadLine().ToLower();
                            foreach (string name in abilityNames)
                            {
                                if (response == name.ToLower())
                                {
                                    foreach (Tuple<string, int> ability in abilityScores)
                                    {
                                        if (ability.Item1 == name)
                                        {
                                            goto ScoreAlreadyAdded;
                                        }
                                    }
                                    abilityScores.Add(new Tuple<string, int>(name, value));
                                    remainingValues.Remove(value);
                                    selectedAbility = true;
                                    break;
                                ScoreAlreadyAdded:
                                    break;
                                }
                            }
                        }
                    }
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    foreach (Tuple<string, int> ability in abilityScores)
                    {
                        Console.WriteLine($"{ability.Item1} - {ability.Item2}");
                    }
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\nIs this how you want to assign your ability scores?: ");
                    if (CheckConfirmation())
                    {
                        selectedAbilityScores = true;
                        foreach (Tuple<string, int> ability in abilityScores)
                        {
                            AddAbilityScore(player, ability.Item1, ability.Item2);
                        }
                    }
                }
            }
            player.maxHitPoints = player.CalculateHitPoints();
            player.currentHitPoints = player.maxHitPoints;
            player.ShowCharacterSheet();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\nPress Enter to Continue: ");
            Console.ReadLine();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            string classSpecificText;
            if (player.characterClass is Barbarian || player.characterClass is Fighter || player.characterClass is Paladin || player.characterClass is Rogue)
            {
                classSpecificText = "Luckily, you seem the sort to be able to handle some of the brutes we find our justice systems struggling with.";
            } else if (player.characterClass is Ranger)
            {
                classSpecificText = "It seems you'll fit right in as you seem great at keeping some of the foul creatures at bay.";
            } else if (player.characterClass is Monk) 
            {
                classSpecificText = "Being level headed as you are, it is possible that you might not even have any problem with the situation as you'd be just fine.";
            } else
            {
                classSpecificText = "Although come to think of it, he was also a magic wielder which may cause people to hope you can rise to the occasion.";
            }
            Console.WriteLine("Welcome to Wickfordonia!\n\nHome to various brilliant species and races, we pride ourselves in being a diverse community with a variety of different backgrounds.\n" +
                $"It's a shame you join us at this time because it's only been 2 months since we lost our protector TheBeastBoss, and we are certainly feeling his absence.\n{classSpecificText}\n" +
                $"I cam imagine you must be quite tired from the journey here, I'll advise that you head down to the town hall and get yourself acquainted with the Mayor.");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\nPress Enter to Continue: ");
            Console.ReadLine();

            Console.ReadLine();
        }

        static string FormatName(string startName, bool askUser = true)
        {
            startName = startName.ToLower();
            string returnName = "";
            bool canCapital = true;
            for (int i = 0; i < startName.Length; i++)
            {
                char c = startName[i];
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
            if (askUser)
            {
                if (returnName == startName)
                {
                    return startName;
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"Do you want the name to be formatted from \"{startName}\" to \"{returnName}\"?: ");
                if (CheckConfirmation())
                {
                    return returnName;
                }
                return startName;
            }
            return returnName;
        }

        public static bool CheckConfirmation()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
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

        public static void GetAction()
        {
            foundAction = false;
            while (!foundAction)
            {
                Console.Clear();
                Console.Write("What would you like to do next?: ");
                List<string> response = SplitIntoWords(Console.ReadLine().ToLower());
                foreach (GameAction action in player.actions)
                {
                    action.actionMethod(player, response);
                    if (foundAction) break;
                }
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\nPress Enter to Continue: ");
            Console.ReadLine();
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

        public static int SortInventory(Equipment x, Equipment y)
        {
            if (x == y)
            {
                return 0;
            }
            List<string> names = new List<string>();
            names.Add(x.equipmentName);
            names.Add(y.equipmentName);
            names.Sort();
            foreach (string name in names)
            {
                if (name == x.equipmentName) return -1;
                else return 1;
            }
            return 69;
        }

        public static void AddAbilityScore(LivingCreature creature, string ability, int score)
        {
            switch(ability)
            {
                case "Strength":
                    creature.strengthScore += score;
                    break;
                case "Dexterity":
                    creature.dexterityScore += score;
                    break;
                case "Constitution":
                    creature.constitutionScore += score;
                    break;
                case "Intelligence":
                    creature.intelligenceScore += score;
                    break;
                case "Wisdom":
                    creature.wisdomScore += score;
                    break;
                case "Charisma":
                    creature.charismaScore += score;
                    break;
                case "All":
                    creature.strengthScore += score;
                    creature.dexterityScore += score;
                    creature.constitutionScore += score;
                    creature.intelligenceScore += score;
                    creature.wisdomScore += score;
                    creature.charismaScore += score;
                    break;
            }
        }

        public static int GetSavingThrowModifier(LivingCreature creature, string abilityName)
        {
            int modifier = GetAbilityModifier(creature, abilityName);
            if (creature.proficiencies.Contains(abilityName)) modifier += creature.proficiencyBonus;
            return modifier;
        }

        public static int GetPassiveSenseScore(LivingCreature creature, string senseName)
        {
            return 10 + GetSkillModifier(creature, senseName);
        }

        public static int GetSkillModifier(LivingCreature creature, string skillName, bool isAboutStone = false)
        {
            foreach (Tuple<Skill, string> skillAbility in allSkillsAbilities)
            {
                if (GetDescription(skillAbility.Item1) == skillName)
                {
                    int modifier = GetAbilityModifier(creature, skillAbility.Item2);
                    if (creature.proficiencies.Contains(skillName))
                    {
                        modifier += creature.proficiencyBonus;
                        if (creature is PeacefulCreature)
                        {
                            PeacefulCreature peace = (PeacefulCreature)creature;
                            if (peace.characterClass is Rogue)
                            {
                                Rogue rogue = (Rogue)peace.characterClass;
                                if (rogue.doubleProficiency.Contains(skillName)) modifier += creature.proficiencyBonus;
                            } else if (isAboutStone && (peace.characterRace is HillDwarf || peace.characterRace is MountainDwarf))
                            {
                                modifier += creature.proficiencyBonus;
                            }
                        }
                    }
                    return modifier;
                }
            }
            return -99;
        }

        public static int GetAbilityModifier(LivingCreature creature, string abilityName)
        {
            switch (abilityName)
            {
                case "Strength":
                    return scoreToModifier[creature.strengthScore];
                case "Dexterity":
                    return scoreToModifier[creature.dexterityScore];
                case "Constitution":
                    return scoreToModifier[creature.constitutionScore];
                case "Intelligence":
                    return scoreToModifier[creature.intelligenceScore];
                case "Wisdom":
                    return scoreToModifier[creature.wisdomScore];
                case "Charisma":
                    return scoreToModifier[creature.charismaScore];
            }
            return -99;
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
                        if (diceFace == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"You rolled a D{set.Item2} for a value of 1");
                        }
                        else if (diceFace == set.Item2)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"You rolled a D{set.Item2} for a value of {diceFace}");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"You rolled a D{set.Item2} for a value of {diceFace}");
                        }
                    }
                    diceValues.Add(diceFace);
                }
            }
            return diceValues;
        }

        public static void AddProficiency(LivingCreature creature, string proficiency)
        {
            if (!creature.proficiencies.Contains(proficiency))
            {
                creature.proficiencies.Add(proficiency);
            }
        }

        public static int GetDistanceBetweenSquares(int x1, int y1, int x2, int y2)
        {
            int horizontalDistance = Math.Abs(x1 - x2);
            horizontalDistance *= horizontalDistance;
            int verticalDistance = Math.Abs(y1 - y2);
            verticalDistance *= verticalDistance;
            int distance = horizontalDistance + verticalDistance;
            return 5 * Convert.ToInt32(Math.Floor(Math.Sqrt(distance)));
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
