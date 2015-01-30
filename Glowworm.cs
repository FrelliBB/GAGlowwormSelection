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
        public int X { get; set; }
        public int Y { get; set; }
        public float Luciferin { get; set; }

        public Glowworm(int x, int y, float luciferinStart)
        {
            this.X = x;
            this.Y = y;
            this.Luciferin = luciferinStart;
        }
    }
}
