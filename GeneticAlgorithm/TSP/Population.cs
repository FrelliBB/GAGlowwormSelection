using GlowwormSelection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using GlowwormSelection.GeneticAlgorithm.Crossover;
using GlowwormSelection.GeneticAlgorithm.SelectionSchemes;

namespace GlowwormSelection.GeneticAlgorithm.TSP
{
    class Population
    {
        public List<Chromosome> chromosomes; //stores the possible solutions (chromosomes) for the TSP
        private List<City> cities; //stores the cities that need to be traversed for the TSP
        public int PopulationSize { get; set; }
        public int NumberOfCities { get; set; }

        int currentGeneration = 1;

        public Population(int numberOfCities, int populationSize)
        {
            this.PopulationSize = populationSize;
            this.NumberOfCities = numberOfCities;

            GenerateCityData(numberOfCities);
            GenerateInitialPopulation(populationSize);
        }

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
            chromosomes = new List<Chromosome>();

            for (int i = 0; i < populationSize; i++)
            {
                List<City> randomizedCities = new List<City>(cities);
                randomizedCities.Shuffle();
                chromosomes.Add(new Chromosome(randomizedCities));
            }
        }

        public void ResetChromosomeFitness()
        {
            foreach (Chromosome solution in chromosomes)
            {
                solution.ResetCost();
            }
        }

        public void NextGeneration()
        {
            // select chromosomes using gso
            var selected = new GlowwormSwarmSelection().Select(this.chromosomes, (int)Math.Floor(Math.Sqrt(cities.Count)));
            //var selected = new SelectionSchemes.RouletteWheelSelection().Select(this.chromosomes, (int)Math.Floor(Math.Sqrt(cities.Count)));

            // remove uncalculated chromosomes from population
            this.chromosomes.RemoveAll(m => m.GetCost() == -1);
            chromosomes = chromosomes.OrderByDescending(c => c.GetCost()).ToList();

            // leave only top 25% of population
            while (this.chromosomes.Count > PopulationSize / 4)
            {
                chromosomes.RemoveAt(0);
            }

            // fill the remaining empty space with children generated from selected chromosomes
            while (this.chromosomes.Count < PopulationSize)
            {
                Chromosome parent1 = selected[ThreadSafeRandom.CurrentThreadRandom.Next(0, selected.Count - 1)];
                Chromosome parent2 = selected[ThreadSafeRandom.CurrentThreadRandom.Next(0, selected.Count - 1)];

                Chromosome child = OrderedCrossover.MakeChild(parent1, parent2);
                if (ThreadSafeRandom.CurrentThreadRandom.NextDouble() < 0.002)
                {
                    child.Mutate();
                }

                chromosomes.Add(child);
            }

            Console.WriteLine("Generation " + currentGeneration + " Best: " + chromosomes.Where(c => c.GetCost() != -1).Min(c => c.GetCost()));
            currentGeneration++;
        }
    }
}
