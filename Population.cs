using GlowwormSelection.Extensions;
using GlowwormSelection.TSP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlowwormSelection
{
    class Population
    {
        public List<Chromosome> population; //stores the possible solutions (chromosomes) for the TSP
        private List<City> cities; //stores the cities that need to be traversed for the TSP


        public List<City> GetCityData()
        {
            return this.cities;
        }

        public void SetCityData(List<City> cities)
        {
            this.cities = cities;
        }

        public void GenerateCityData(int numberOfCities)
        {
            cities = new List<City>();

            Dictionary<string, int> distances = new Dictionary<string, int>(); // stores the distances for the entire graph

            for (int i = 0; i < numberOfCities; i++)
            {
                Console.WriteLine("Generating data for City " + i);
                City city = new City("City " + i);


                //set distances for city i
                for (int j = 0; j < numberOfCities; j++)
                {
                    if (i != j)
                    {
                        int distance;

                        distances.TryGetValue(i + "-" + j, out distance);

                        // TryGetValue returns default value for int if the key was not found. Our distance can never be at 0 so we can use this to check if the key exists
                        if (distance == 0)
                        {
                            distance = ThreadSafeRandom.CurrentThreadRandom.Next(101);
                            distances[i + "-" + j] = distance;
                            distances[j + "-" + i] = distance;
                        }


                        city.SetDistance("City " + j, distance);
                    }
                }

                cities.Add(city);
            }
        }

        public void GenerateInitialPopulation(int populationSize)
        {
            population = new List<Chromosome>();

            for (int i = 0; i < populationSize; i++)
            {
                List<City> randomizedCities = new List<City>(cities);
                randomizedCities.Shuffle();
                population.Add(new Chromosome(randomizedCities));
            }
        }

        public void ResetChromosomeFitness()
        {
            foreach (Chromosome solution in population)
            {
                solution.ResetCost();
            }
        }
    }
}
