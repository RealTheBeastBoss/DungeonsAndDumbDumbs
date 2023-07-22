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
        public Action castSpell;
        public bool isRitual;
        public int spellRange;
        public string spellDescription;

        public Spell(string name, int level, Action cast, int range, string desc, bool ritual = false)
        {
            spellName = name;
            spellLevel = level;
            castSpell = cast;
            spellRange = range;
            spellDescription = desc;
            isRitual = ritual;
        }
        public static void AcidSplash()
        {

        }
        public static void AnimalFriendship()
        {

        }
        public static void CharmPerson()
        {

        }
        public static void ComprehendLanguages()
        {

        }
        public static void CureWounds()
        {

        }
        public static void DancingLights()
        {

        }
        public static void FireBolt()
        {

        }
        public static void Guidance()
        {

        }
        public static void Light()
        {

        }
        public static void MageHand()
        {

        }
        public static void Mending()
        {

        }
        public static void MinorIllusion()
        {

        }
        public static void PoisonSpray()
        {

        }
        public static void Prestidigitation()
        {

        }
        public static void RayOfFrost()
        {

        }
        public static void Resistance()
        {

        }
        public static void ShockingGrasp()
        {

        }
        public static void SpareTheDying()
        {

        }
        public static void Thaumaturgy()
        {

        }
    }
}
