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
            Random r = new Random();

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
                            distance = r.Next(1, 100);
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

        /// <summary>
        /// Uses Glowworm Swarm Optimization to find the best chromosomes in the population.
        /// </summary>
        /// <returns></returns>
        public List<Chromosome> GlowwormSelection()
        {
            if (Math.Sqrt(population.Count) % 1 != 0)
            {
                throw new Exception("Non-square population sizes currently not supported.");
            }

            int arraySize = (int)Math.Sqrt(population.Count);
            float initialLuciferin = 50f;
            List<Glowworm> glowworms = new List<Glowworm>(); //the glowworms that traverse the population for the selection process
            Random r = new Random();


            // 1. Transform population into 2D array

            // Randomize our population
            population.Shuffle();

            Chromosome[,] solutionSpace = new Chromosome[arraySize, arraySize];

            // Assign population to our 2D array
            for (int i = 0; i < solutionSpace.GetUpperBound(0); i++)
            {
                for (int j = 0; j < solutionSpace.GetUpperBound(1); j++)
                {
                    solutionSpace[i, j] = population.ElementAt(i + j);
                }
            }

            // 2. Distribute glowworms randomly across 2D array

            for (int i = 0; i < population.Count / 10; i++)
            {
                glowworms.Add(new Glowworm(r.Next(0, arraySize - 1), r.Next(0, arraySize - 1), initialLuciferin));
            }

            // 3. Luciferin update

            // 4. Glowworm movement

            // 5. Repeat 3-4 for x steps
            throw new NotImplementedException();
        }


    }
}
