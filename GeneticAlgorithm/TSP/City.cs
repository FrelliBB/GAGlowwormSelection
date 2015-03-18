using System.Collections.Generic;

namespace GlowwormSelection.TSP
{
    class City
    {
        public string Name { get; set; }
        public Dictionary<string, int> Distances = new Dictionary<string, int>();

        public City(string name)
        {
            this.Name = name;
        }

        public int GetDistance(string city)
        {
            if (city == this.Name)
            {
                return 0;
            }

            return Distances[city];
        }

        public void SetDistance(string city, int distance)
        {
            if (city != this.Name)
            {
                Distances.Add(city, distance);
            }
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
