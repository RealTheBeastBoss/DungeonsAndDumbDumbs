using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDumbDumbs
{
    // The Player, Monsters and Friendly NPCs
    class LivingCreature
    {
        public string name;
        public int walkSpeed;
        public int experiencePoints = 0;
        public List<Program.Language> languages = new List<Program.Language>();
        public List<Spell> cantrips = new List<Spell>();
        public List<Spell> knownSpells = new List<Spell>();
        public bool canRitual = false;
        public int firstLevelSlots = 0;
        public int secondLevelSlots = 0;
        public int thirdLevelSlots = 0;
        public int fourthLevelSlots = 0;
        public int fifthLevelSlots = 0;
        public int sixthLevelSlots = 0;
        public int seventhLevelSlots = 0;
        public int eighthLevelSlots = 0;
        public int ninthLevelSlots = 0;
        public int firstLevelSlotsRemaining = 0;
        public int secondLevelSlotsRemaining = 0;
        public int thirdLevelSlotsRemaining = 0;
        public int fourthLevelSlotsRemaining = 0;
        public int fifthLevelSlotsRemaining = 0;
        public int sixthLevelSlotsRemaining = 0;
        public int seventhLevelSlotsRemaining = 0;
        public int eighthLevelSlotsRemaining = 0;
        public int ninthLevelSlotsRemaining = 0;
        public bool usedSpellThisAction = false;
        public List<string> proficiencies = new List<string>();
        public int proficiencyBonus;
        public List<GameAction> actions = new List<GameAction>();
        public List<GameAction> bonusActions = new List<GameAction>();
        public List<Program.DamageType> immunities = new List<Program.DamageType>();
        public List<Program.DamageType> resistances = new List<Program.DamageType>();
        public List<Program.DamageType> vunerabilities = new List<Program.DamageType>();
        public List<EffectInstance> activeEffects = new List<EffectInstance>();
        public int strengthScore = 0;
        public int dexterityScore = 0;
        public int constitutionScore = 0;
        public int intelligenceScore = 0;
        public int wisdomScore = 0;
        public int charismaScore = 0;
        public int maxHitPoints;
        public int currentHitPoints;
        public int tempHitPoints;
        public Location currentLocation;
        public Tuple<int, int> position;
        public LivingCreature(List<GameAction> startActions)
        {
            actions = startActions;
        }
        public virtual void OnTimePassed(object sender, TimePassedEventArgs data)
        {
            foreach (EffectInstance effect in activeEffects)
            {
                if (!effect.isIndefinite) effect.effectDuration -= data.minutesPassed;
            }
        }
    }
    class PeacefulCreature : LivingCreature
    {
        public Race characterRace;
        public Class characterClass;
        public int gold = 0;
        public int silver = 0;
        public int copper = 0;
        public bool isInspired = false;
        public List<Equipment> inventory = new List<Equipment>();
        public Armour wornArmour;
        public bool hasShield = false;
        public PeacefulCreature(List<GameAction> startActions) : base(startActions)
        {
            languages.Add(Program.Language.COMMON);
        }
        public int CalculateArmourClass()
        {
            int shieldAC = 0;
            if (hasShield) shieldAC += Program.shield.baseArmourClass;
            if (wornArmour != null)
            {
                if (characterClass is Fighter)
                {
                    Fighter character = (Fighter)characterClass;
                    if (character.fightingStyle == Fighter.FightingStyle.DEFENSE)
                    {
                        return shieldAC + wornArmour.CalculateArmourClass(this) + 2;
                    }
                }
                return shieldAC + wornArmour.CalculateArmourClass(this);
            }
            return shieldAC + characterClass.CalculateArmourClass(this);
        }
        public Tuple<int, int> CalculateHitDice()
        {
            return characterClass.CalculateHitDice();
        }
        public int CalculateHitPoints()
        {
            return characterClass.CalculateHitPoints(this);
        }
        public override void OnTimePassed(object sender, TimePassedEventArgs data)
        {
            base.OnTimePassed(sender, data);
        }
    }
    class Player : PeacefulCreature
    {
        public Player(List<GameAction> startActions) : base(startActions)
        {
            proficiencyBonus = 2;
            bonusActions.Add(Program.useMagic);
            bonusActions.Add(Program.showInventory);
            bonusActions.Add(Program.showCharacterSheet);
        }
        public void ShowCharacterSheet() // TODO: Add Senses
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            string inspired = isInspired ? "Currently Inspired" : "Not Inspired";
            Console.WriteLine($"Character Sheet for {name}:\n");
            Console.WriteLine($"{characterRace.raceName} {characterClass.className} at Level {characterClass.classLevel} with {experiencePoints}XP. Strength: {strengthScore} ({Program.GetAbilityModifier(this, "Strength")}) " +
                $"| Dexterity: {dexterityScore} ({Program.GetAbilityModifier(this, "Dexterity")}) | Constitution: {constitutionScore} ({Program.GetAbilityModifier(this, "Constitution")}) " +
                $"| Intelligence: {intelligenceScore} ({Program.GetAbilityModifier(this, "Intelligence")}) | Wisdom: {wisdomScore} ({Program.GetAbilityModifier(this, "Wisdom")}) " +
                $"| Charisma: {charismaScore} ({Program.GetAbilityModifier(this, "Charisma")})\n");
            Console.WriteLine($"Proficiency Bonus: +{proficiencyBonus} | Walking Speed: {walkSpeed}ft. | Armour Class: {CalculateArmourClass()} | {inspired}.\n");
            Console.WriteLine($"Hit Points: {currentHitPoints} / {maxHitPoints} | Currency: {gold} gold, {silver} silver, {copper} copper\n");
            Console.WriteLine($"Saving Throw Modifiers: Strength: {Program.GetSavingThrowModifier(this, "Strength")} | Dexterity: {Program.GetSavingThrowModifier(this, "Dexterity")} | " +
                $"Constitution: {Program.GetSavingThrowModifier(this, "Constitution")} | Intelligence: {Program.GetSavingThrowModifier(this, "Intelligence")} | " +
                $"Wisdom: {Program.GetSavingThrowModifier(this, "Wisdom")} | Charisma: {Program.GetSavingThrowModifier(this, "Charisma")}\n");
            Console.WriteLine($"Skill Modifiers:\n");
            foreach (Tuple<Program.Skill, string> skill in Program.allSkillsAbilities)
            {
                Console.WriteLine($"{Program.GetDescription(skill.Item1)}: {Program.GetSkillModifier(this, Program.GetDescription(skill.Item1))}");
            }
            Console.WriteLine("\nProficiencies:\n");
            foreach (string proficiency in proficiencies)
            {
                foreach (Tuple<Program.Skill, string> skillAbility in Program.allSkillsAbilities)
                {
                    if (proficiency == Program.GetDescription(skillAbility.Item1) || proficiency == skillAbility.Item2 || proficiency == "Constitution") goto FoundSkillAbility;
                }
                Console.WriteLine(proficiency);
            FoundSkillAbility:
                continue;
            }
            Console.WriteLine("\nLanguages:\n");
            foreach (Program.Language language in languages)
            {
                Console.WriteLine(Program.GetDescription(language));
            }
        }
        public override void OnTimePassed(object sender, TimePassedEventArgs data)
        {
            base.OnTimePassed(sender, data);
        }
    }
    class NPC : PeacefulCreature
    {
        public NPC(string characterName, List<GameAction> startActions) : base(startActions)
        {
            name = characterName;
        }
        public override void OnTimePassed(object sender, TimePassedEventArgs data)
        {
            base.OnTimePassed(sender, data);
        }
    }
    class Monster : LivingCreature
    {
        public enum Size
        {
            [Description("Tiny")]
            TINY,
            [Description("Small")]
            SMALL,
            [Description("Medium")]
            MEDIUM,
            [Description("Large")]
            LARGE,
            [Description("Huge")]
            HUGE,
            [Description("Gargantuan")]
            GARGANTUAN
        }
        public Size monsterSize;
        public enum Type
        {
            [Description("Aberration")]
            ABBERATION,
            [Description("Beast")]
            BEAST,
            [Description("Celestial")]
            CELESTIAL,
            [Description("Construct")]
            CONSTRUCT,
            [Description("Dragon")]
            DRAGON,
            [Description("Elemental")]
            ELEMENTAL,
            [Description("Fey")]
            FEY,
            [Description("Fiend")]
            FIEND,
            [Description("Giant")]
            GIANT,
            [Description("Humanoid")]
            HUMANOID,
            [Description("Monstrosity")]
            MONSTROSITY,
            [Description("Ooze")]
            OOZE,
            [Description("Plant")]
            PLANT,
            [Description("Undead")]
            UNDEAD,
        }
        public static List<Type> allMonsterTypes = new List<Type>() { Type.ABBERATION, Type.BEAST, Type.CELESTIAL, Type.CONSTRUCT, Type.DRAGON, Type.ELEMENTAL, Type.FEY, Type.FIEND, Type.GIANT, 
            Type.MONSTROSITY, Type.OOZE, Type.PLANT, Type.UNDEAD };
        public Type monsterType;
        public int armourClass;
        public int challengeRating;
        public Monster(string monsterName, Size size, int hitPoints, Type type, int speed, int challenge, int xpWhenDefeated, int armour, List<GameAction> startActions) : base(startActions)
        {
            maxHitPoints = hitPoints;
            currentHitPoints = hitPoints;
            name = monsterName;
            monsterSize = size;
            monsterType = type;
            walkSpeed = speed;
            challengeRating = challenge;
            experiencePoints = xpWhenDefeated;
            armourClass = armour;
        }
        public int CalculateArmourClass()
        {
            return armourClass;
        }
        public override void OnTimePassed(object sender, TimePassedEventArgs data)
        {
            base.OnTimePassed(sender, data);
        }
    }
}
