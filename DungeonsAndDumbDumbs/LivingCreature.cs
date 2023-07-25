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
        // TODO: Actions
        // TODO: Effects
        public int strengthScore = 0;
        public int dexterityScore = 0;
        public int constitutionScore = 0;
        public int intelligenceScore = 0;
        public int wisdomScore = 0;
        public int charismaScore = 0;
    }
    class PeacefulCreature : LivingCreature
    {
        public Race characterRace;
        public Class characterClass;
        public int gold = 0;
        public int silver = 0;
        public int copper = 0;
        public PeacefulCreature()
        {
            languages.Add(Program.Language.COMMON);
        }
    }
    class Player : PeacefulCreature
    {
        public Player()
        {
            proficiencyBonus = 2;
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
        public int challengeRating;
        public Monster(string monsterName, Size size, int speed, int challenge, int xpWhenDefeated)
        {
            name = monsterName;
            monsterSize = size;
            walkSpeed = speed;
            challengeRating = challenge;
            experiencePoints = xpWhenDefeated;
        }
    }
}
