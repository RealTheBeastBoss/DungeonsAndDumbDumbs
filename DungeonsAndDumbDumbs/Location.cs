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
            public Program.Terrain terrainType;
            public Weapon thrownWeapon = null;
            public Square(bool space = false, Program.Terrain type = Program.Terrain.INSIDE, bool terrain = false)
            {
                isDifficultTerrain = terrain;
                terrainType = type;
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
