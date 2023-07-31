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
        public List<string> proficiencies = new List<string>();
        public int proficiencyBonus;
        public List<GameAction> actions = new List<GameAction>();
        public List<Program.DamageType> immunities = new List<Program.DamageType>();
        public List<Program.DamageType> resistances = new List<Program.DamageType>();
        public List<Program.DamageType> vunerabilities = new List<Program.DamageType>();
        // TODO: Effects
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
        public PeacefulCreature()
        {
            languages.Add(Program.Language.COMMON);
        }
        public int CalculateArmourClass()
        {
            int shieldAC = 0;
            if (hasShield) shieldAC += Program.shield.baseArmourClass;
            if (wornArmour != null)
            {
                return shieldAC + wornArmour.CalculateArmourClass(this);
            }
            return shieldAC + characterClass.CalculateArmourClass();
        }
        public Tuple<int, int> CalculateHitDice()
        {
            return characterClass.CalculateHitDice();
        }
        public int CalculateHitPoints()
        {
            return characterClass.CalculateHitPoints();
        }
    }
    class Player : PeacefulCreature
    {
        public Player()
        {
            proficiencyBonus = 2;
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
            Console.WriteLine($"Hit Points: {currentHitPoints} / {maxHitPoints} | Currency: {gold}gp, {silver}sp, {copper}cp\n");
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
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\nPress Enter to Continue: ");
            Console.ReadLine();
        }
    }
    class NPC : PeacefulCreature
    {
        public NPC(string characterName)
        {
            name = characterName;
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
        public int armourClass;
        public int challengeRating;
        public Monster(string monsterName, Size size, int speed, int challenge, int xpWhenDefeated, int armour)
        {
            name = monsterName;
            monsterSize = size;
            walkSpeed = speed;
            challengeRating = challenge;
            experiencePoints = xpWhenDefeated;
            armourClass = armour;
        }
        public int CalculateArmourClass()
        {
            return armourClass;
        }
    }
}
