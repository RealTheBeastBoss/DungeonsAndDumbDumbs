using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDumbDumbs
{
    class Spell
    {
        public enum Type
        {
            ACTION,
            BONUS,
            REACTION
        }
        public string spellName;
        public int spellLevel;
        public Action<int, LivingCreature> castSpell;
        public bool isRitual;
        public int spellRange;
        public string spellDescription;
        public Type spellType;
        public List<Program.GameState> allowedStates = new List<Program.GameState>();

        public Spell(string name, int level, Action<int, LivingCreature> cast, int range, string desc, List<Program.GameState> states = null, Type type = Type.ACTION, bool ritual = false)
        {
            spellName = name;
            spellLevel = level;
            castSpell = cast;
            spellRange = range;
            spellDescription = desc;
            isRitual = ritual;
            if (states == null)
            {
                allowedStates.Add(Program.GameState.FREE);
                allowedStates.Add(Program.GameState.INTERACTION);
                allowedStates.Add(Program.GameState.COMBAT);
                return;
            }
            allowedStates = states;
        }
        public static void AcidSplash(int castLevel, LivingCreature spellCaster)
        {
            
        }
        public static void AnimalFriendship(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void Bane(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void Bless(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void BurningHands(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void CharmPerson(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void ColourSpray(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void Command(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void ComprehendLanguages(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void CreateDestroyWater(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void CureWounds(int castLevel, LivingCreature spellCaster)
        {
            
        }
        public static void DancingLights(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void DetectGoodEvil(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void DetectMagic(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void DetectPoison(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void DivineFavour(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void Entangle(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void ExpeditiousRetreat(int castLevel, LivingCreature spellCaster)
        {
            
        }
        public static void FireBolt(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void Guidance(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void GuidingBolt(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void HealingWord(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void HellishRebuke(int castLevel, LivingCreature spellCaster)
        {
            
        }
        public static void InflictWounds(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void Light(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void MageArmour(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void MageHand(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void MagicMissle(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void Mending(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void MinorIllusion(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void PoisonSpray(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void Prestidigitation(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void ProtectionFromEvilGood(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void PurifyFoodDrink(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void RayOfFrost(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void Resistance(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void ShieldOfFaith(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void ShockingGrasp(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void SpareTheDying(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void Thaumaturgy(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void ThunderWave(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void TrueStrike(int castLevel, LivingCreature spellCaster)
        {

        }
        public static void ViciousMockery(int castLevel, LivingCreature spellCaster)
        {
            
        }
    }
}
