using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDumbDumbs
{
    class Spell
    {
        public string spellName;
        public int spellLevel;
        public Action<int> castSpell;
        public bool isRitual;
        public int spellRange;
        public string spellDescription;

        public Spell(string name, int level, Action<int> cast, int range, string desc, bool ritual = false)
        {
            spellName = name;
            spellLevel = level;
            castSpell = cast;
            spellRange = range;
            spellDescription = desc;
            isRitual = ritual;
        }
        public static void AcidSplash(int spellLevel)
        {

        }
        public static void AnimalFriendship(int spellLevel)
        {

        }
        public static void Bane(int spellLevel)
        {

        }
        public static void Bless(int spellLevel)
        {

        }
        public static void CharmPerson(int spellLevel)
        {

        }
        public static void Command(int spellLevel)
        {

        }
        public static void ComprehendLanguages(int spellLevel)
        {

        }
        public static void CreateDestroyWater(int spellLevel)
        {

        }
        public static void CureWounds(int spellLevel)
        {

        }
        public static void DancingLights(int spellLevel)
        {

        }
        public static void DetectGoodEvil(int spellLevel)
        {

        }
        public static void DetectMagic(int spellLevel)
        {

        }
        public static void DetectPoison(int spellLevel)
        {

        }
        public static void FireBolt(int spellLevel)
        {

        }
        public static void Guidance(int spellLevel)
        {

        }
        public static void GuidingBolt(int spellLevel)
        {

        }
        public static void HealingWord(int spellLevel)
        {

        }
        public static void InflictWounds(int spellLevel)
        {

        }
        public static void Light(int spellLevel)
        {

        }
        public static void MageHand(int spellLevel)
        {

        }
        public static void Mending(int spellLevel)
        {

        }
        public static void MinorIllusion(int spellLevel)
        {

        }
        public static void PoisonSpray(int spellLevel)
        {

        }
        public static void Prestidigitation(int spellLevel)
        {

        }
        public static void PurifyFoodDrink(int spellLevel)
        {

        }
        public static void RayOfFrost(int spellLevel)
        {

        }
        public static void Resistance(int spellLevel)
        {

        }
        public static void ShieldOfFaith(int spellLevel)
        {

        }
        public static void ShockingGrasp(int spellLevel)
        {

        }
        public static void SpareTheDying(int spellLevel)
        {

        }
        public static void Thaumaturgy(int spellLevel)
        {

        }
    }
}
