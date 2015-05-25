using System;
using System.Collections.Generic;

namespace GlowwormSelection.GeneticAlgorithm.TSP
{
    public class City
    {
        public string Name { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public City(string name, double x, double y)
        {
            this.Name = name;
            this.X = x;
            this.Y = y;
        }

        public double GetDistance(City otherCity)
        {
            return Math.Sqrt(Math.Pow(otherCity.X - this.X, 2) + Math.Pow(otherCity.Y - this.Y, 2));
        }

        public override string ToString()
        {
            return string.Format("{0} ({1},{2})", this.Name, this.X, this.Y);
        }
    }
}
