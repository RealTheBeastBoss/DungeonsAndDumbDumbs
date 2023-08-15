using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonsAndDumbDumbs
{
    class Object
    {
        public string objectName;
        public int armourClass;
        public int hitPoints;
        public List<Program.DamageType> immunities = new List<Program.DamageType>() { Program.DamageType.POISON, Program.DamageType.PSYCHIC };
        public List<Program.DamageType> resistances;
        public List<Program.DamageType> vunerabilities;
        public void DefineResistances(List<Program.DamageType> resistTypes)
        {
            resistances = new List<Program.DamageType>(resistTypes);
        }
        public void DefineVunerabilities(List<Program.DamageType> vunerableTypes)
        {
            vunerabilities = new List<Program.DamageType>(vunerableTypes);
        }
    }
    class Container : Object
    {
        public List<Equipment> capacity;
        public int gold;
        public int silver;
        public int copper;
        public Container(string name, int gp, int sp, int cp, int armour, int hit, List<Equipment> inventory = null)
        {
            objectName = name;
            gold = gp;
            silver = sp;
            copper = cp;
            armourClass = armour;
            hitPoints = hit;
            capacity = inventory;
        }
    }
    class Trap : Object
    {
        public int spottingDC;
        public Action trapAction;
        public bool isMagic;
        public Equipment toolToBreak;
        public Trap(string name, int spotDC, Action action, bool magic = false, Equipment tool = null)
        {
            objectName = name;
            armourClass = 19;
            hitPoints = Program.RollDice(false, new Tuple<int, int>(4, 8)).Sum();
            spottingDC = spotDC;
            trapAction = action;
            isMagic = magic;
            toolToBreak = tool;
        }
    }
}
