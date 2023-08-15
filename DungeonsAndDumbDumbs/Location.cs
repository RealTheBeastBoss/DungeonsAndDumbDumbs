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
            public enum Lighting
            {
                LIGHT,
                DIMLIGHT,
                DARKNESS
            }
            public bool isDifficultTerrain;
            public bool hasSpace;
            public int fallHeight;
            public Lighting lighting;
            public Program.Terrain terrainType;
            public Weapon thrownWeapon = null;
            public Square(Lighting light = Lighting.LIGHT, bool space = false, Program.Terrain type = Program.Terrain.INSIDE, bool terrain = false, int falling = 0)
            {
                lighting = light;
                fallHeight = falling;
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
