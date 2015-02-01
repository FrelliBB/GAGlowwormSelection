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
                            distance = ThreadSafeRandom.ThisThreadsRandom.Next(1, 100);
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
            float initialLuciferin = 0.5f;
            List<Glowworm> glowworms = new List<Glowworm>(); //the glowworms that traverse the population for the selection process

            // 1. Transform population into 2D array

            // Randomize our population
            population.Shuffle();

            Chromosome[,] solutionSpace = new Chromosome[arraySize, arraySize];
            float glowwormRange = arraySize / 2f;

            // Assign population to our 2D array
            Stack<Chromosome> solutionsToAdd = new Stack<Chromosome>(population);
            for (int i = 0; i <= solutionSpace.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= solutionSpace.GetUpperBound(1); j++)
                {
                    solutionSpace[i, j] = solutionsToAdd.Pop();
                }
            }

            // 2. Distribute glowworms randomly across a 2d plane
            int glowwormCount = (population.Count + 9) / 10;
            for (int i = 0; i < glowwormCount; i++)
            {
                glowworms.Add(new Glowworm(i, ThreadSafeRandom.ThisThreadsRandom.Next(0, arraySize - 1), ThreadSafeRandom.ThisThreadsRandom.Next(0, arraySize - 1), initialLuciferin));
            }

            // 3. Repeat 4-5 for x steps for each glowworm
            for (int i = 0; i < population.Count / 4; i++)
            {
                // 4. Luciferin update
                foreach (Glowworm glowworm in glowworms)
                {
                    glowworm.Luciferin += solutionSpace[glowworm.X, glowworm.Y].GetFitness();
                }

                // 5. Glowworm movement
                foreach (Glowworm currentGlowworm in glowworms)
                {
                    Glowworm closestGlowworm = null;
                    double closestDistance = double.MaxValue;

                    //Find glowworm in range with the highest luciferin value
                    foreach (Glowworm neighbouringGlowworm in glowworms)
                    {
                        // Make sure that the current glowworm isn't the same as the neighbour
                        if (currentGlowworm.ID != neighbouringGlowworm.ID)
                        {
                            // check if neighbouring glowworm has higher luciferin value
                            if (neighbouringGlowworm.Luciferin > currentGlowworm.Luciferin)
                            {
                                //check if neighbouring glowworm is in range and closer than current closest glowworm (using euclidean distance)
                                double distance = Math.Sqrt(Math.Pow(currentGlowworm.X - neighbouringGlowworm.X, 2) + Math.Pow(currentGlowworm.Y - neighbouringGlowworm.Y, 2));

                                if (distance <= glowwormRange && distance < closestDistance)
                                {
                                    closestDistance = distance;
                                    closestGlowworm = neighbouringGlowworm;
                                }
                            }
                        }
                    }

                    if (closestGlowworm != null)
                    {
                        // If a glowworn with a higher luciferin was found in rage move towards it
                        currentGlowworm.MoveTowardsNeighbour(closestGlowworm.X, closestGlowworm.Y);
                    }
                    else
                    {
                        // Move randomly within bounds
                        currentGlowworm.MoveRandomly(arraySize - 1, arraySize - 1);
                    }

                }
            }

            // Find best solutions

            throw new NotImplementedException();
        }
    }
}
