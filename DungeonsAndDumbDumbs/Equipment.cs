using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDumbDumbs
{
    class Equipment
    {
        public string equipmentName;
        public Tuple<int, string> cost;
        public List<string> proficiencyNames;
        public Equipment(string name, Tuple<int, string> value, List<string> proficiency = null)
        {
            equipmentName = name;
            cost = value;
            proficiencyNames = proficiency;
        }
        public Equipment()
        {

        }
    }
    class Weapon : Equipment
    {
        public enum WeaponProperty
        {
            ARROWAMMUNITION,
            CROSSBOWAMMUNITION,
            NEEDLEAMMUNITION,
            SLINGBULLETAMMUNITION,
            FINESSE,
            HEAVY,
            LANCE,
            LIGHT,
            LOADING,
            MELEE,
            REACH,
            THROWN,
            TWOHANDED,
            VERSATILE
        }
        public Tuple<int, int> damageDice;
        public Tuple<int, int> versatileDice;
        public List<WeaponProperty> properties;
        public Program.DamageType damageType;
        public int normalRange;
        public int longRange;
        public Weapon(string name, Tuple<int, string> value, List<string> proficiency, Tuple<int, int> dmg, Program.DamageType dmgType, List<WeaponProperty> props, int smlRange = 5, int bigRange = 5, 
            Tuple<int, int> secondDice = null)
        {
            equipmentName = name;
            cost = value;
            proficiencyNames = proficiency;
            damageDice = dmg;
            damageType = dmgType;
            properties = props;
            normalRange = smlRange;
            longRange = bigRange;
            versatileDice = secondDice;
        }
    }
    class Armour : Equipment
    {
        public bool stealthDisadvantage;
        public int donTime;
        public int doffTime;
        public int strengthScoreNeeded;
        public int baseArmourClass;
        public Armour(string name, Tuple<int, string> value, int baseAC, List<string> proficiency, int don, int doff, bool stealth = false, int strength = 0)
        {
            equipmentName = name;
            cost = value;
            baseArmourClass = baseAC;
            proficiencyNames = proficiency;
            donTime = don;
            doffTime = doff;
            stealthDisadvantage = stealth;
            strengthScoreNeeded = strength;
        }
        public int CalculateArmourClass(PeacefulCreature creature)
        {
            if (proficiencyNames.Contains("Light Armour"))
            {
                return baseArmourClass + Program.GetAbilityModifier(creature, "Dexterity");
            } else if (proficiencyNames.Contains("Medium Armour"))
            {
                return baseArmourClass + Math.Min(2, Program.GetAbilityModifier(creature, "Dexterity"));
            } else if (proficiencyNames.Contains("Heavy Armour"))
            {
                return baseArmourClass;
            }
            return -99;
        }
    }
}
