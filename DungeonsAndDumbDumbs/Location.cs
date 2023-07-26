using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDumbDumbs
{
    class Location
    {
        public string locationName;
        public List<LivingCreature> allCreatures = new List<LivingCreature>();
        public List<Monster> allMonsters = new List<Monster>();
        public List<PeacefulCreature> allPeacefulCreatures = new List<PeacefulCreature>();

        public Location(string name)
        {
            locationName = name;
        }
    }
}
