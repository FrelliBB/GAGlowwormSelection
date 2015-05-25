using GlowwormSelection.Extensions;
using System.Collections.Generic;

namespace GlowwormSelection.GeneticAlgorithm.TSP
{
    public class Chromosome
    {
        public List<City> cities;
        private int cost = -1;

        public Chromosome(List<City> cities)
        {
            this.cities = cities;
        }

        public double GetFitness()
        {
            // only calculate the cost once
            if (cost == -1)
            {
                // add the distance from the last city to the first city
                int totalCost = cities[0].GetDistance(cities[cities.Count - 1].Name);

                for (int i = 0; i < cities.Count - 1; i++)
                {
                    // add the distance from current city to the next city
                    totalCost += cities[i].GetDistance(cities[i + 1].Name);
                }

                cost = totalCost;

            }

            return 1.0 - (cost / (cities.Count * 100.0));
        }

        public int GetCost()
        {
            return cost;
        }

        public void ResetCost()
        {
            cost = -1;
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
