using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDumbDumbs
{
    class Location
    {
        public class Square // 5ft Square
        {
            public bool isDifficultTerrain;
            public bool hasSpace;
            public Weapon thrownWeapon;
            public Square(bool space = false, bool terrain = false)
            {
                isDifficultTerrain = terrain;
                hasSpace = space;
            }
        }
        public string locationName;
        public List<LivingCreature> allCreatures = new List<LivingCreature>();
        public List<Monster> allMonsters = new List<Monster>();
        public List<PeacefulCreature> allPeacefulCreatures = new List<PeacefulCreature>();
        public List<Object> allObjects = new List<Object>();
        public Square[,] area;

        public Location(string name, Square[,] squares)
        {
            locationName = name;
            area = squares;
        }
    }
}
