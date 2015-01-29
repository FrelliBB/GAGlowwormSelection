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
        public List<City> cities; //stores the cities that need to be traversed for the TSP

        public void SetCityData(List<City> cities)
        {
            this.cities = cities;
        }

        public void GenerateCityData(int numberOfCities)
        {
            Dictionary<string, int> distances = new Dictionary<string, int>(); // stores the distances for the entire graph

            for (int i = 0; i < numberOfCities; i++)
            {
                City city = new City("City " + i);


                //set distances for city i
                for (int j = 0; j < numberOfCities; j++)
                {
                    int distance;



                    if (i != j)
                    {

                    }
                }
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
            double arraySize = Math.Sqrt(population.Count);

            if (arraySize % 1 != 0)
            {
                throw new Exception("Non-square population sizes currently not supported.");
            }

            // 1. Transform population into 2D array

            // Randomize our population
            population.Shuffle();

            Chromosome[,] solutionSpace = new Chromosome[(int)arraySize, (int)arraySize];

            // Assign population to our 2D array
            for (int i = 0; i < solutionSpace.GetUpperBound(0); i++)
            {
                for (int j = 0; j < solutionSpace.GetUpperBound(1); j++)
                {
                    solutionSpace[i, j] = population.ElementAt(i + j);
                }
            }

            // 2. Distribute glowworms randomly across 2D array

            // 3. Luciferin update

            // 4. Glowworm movement

            // 5. Repeat 3-4 for x steps
            throw new NotImplementedException();
        }


    }
}
