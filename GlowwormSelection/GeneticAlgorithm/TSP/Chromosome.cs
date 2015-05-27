using GlowwormSelection.Extensions;
using System.Collections.Generic;

namespace GlowwormSelection.GeneticAlgorithm.TSP
{
    public class Chromosome
    {
        public List<City> cities;
        public double Cost { get; set; }

        public Chromosome(List<City> cities)
        {
            Cost = -1;
            this.cities = cities;
        }

        public double GetFitness()
        {
            // only calculate the cost once
            if (Cost == -1)
            {
                // add the distance from the last city to the first city
                double totalCost = cities[0].GetDistance(cities[cities.Count - 1]);

                for (int i = 0; i < cities.Count - 1; i++)
                {
                    // add the distance from current city to the next city
                    totalCost += cities[i].GetDistance(cities[i + 1]);
                }

                Cost = totalCost;

            }

            return 1.0 - (Cost / (cities.Count * 100.0));
        }

        public double GetCost()
        {
            return Cost;
        }

        public void ResetCost()
        {
            Cost = -1;
        }

        public void Mutate()
        {
            int rndIndex = ThreadSafeRandom.CurrentThreadRandom.Next(0, cities.Count);
            City rndCity = cities[rndIndex];

            cities.Remove(rndCity);
            cities.Insert(ThreadSafeRandom.CurrentThreadRandom.Next(0, cities.Count), rndCity);
        }
    }
}
