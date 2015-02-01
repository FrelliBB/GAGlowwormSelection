using GlowwormSelection.TSP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlowwormSelection
{
    class Glowworm
    {
        public int ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public float Luciferin { get; set; }

        public Glowworm(int id, int x, int y, float luciferinStart)
        {
            this.ID = id;
            this.X = x;
            this.Y = y;
            this.Luciferin = luciferinStart;
        }

        public void MoveRandomly(int boundsX, int boundsY)
        {

        }

        public void MoveTowardsNeighbour(int neighourX, int neighbourY)
        {

        }
    }
}
