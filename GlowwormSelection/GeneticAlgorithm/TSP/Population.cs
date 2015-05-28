using GlowwormSelection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using GlowwormSelection.GeneticAlgorithm.Crossover;
using GlowwormSelection.GeneticAlgorithm.SelectionSchemes;

namespace GlowwormSelection.GeneticAlgorithm.TSP
{
    public class Population
    {
        public List<Chromosome> Chromosomes { get; set; } //stores the possible solutions (chromosomes) for the TSP
        public List<City> Cities { get; set; } //stores the cities that need to be traversed for the TSP
        public int PopulationSize { get; set; }
        public int NumberOfCities { get; set; }
        public Double BestTour { get; set; }

        int currentGeneration = 1;

        public Population(int numberOfCities, int populationSize)
        {
            BestTour = double.MaxValue;
            this.PopulationSize = populationSize;
            this.NumberOfCities = numberOfCities;

            GenerateCityData(numberOfCities);
            GenerateInitialPopulation(populationSize);
        }

        public Population(List<City> cities, int populationSize)
        {
            BestTour = double.MaxValue;
            this.PopulationSize = populationSize;
            this.NumberOfCities = cities.Count;

            this.Cities = cities;
            GenerateInitialPopulation(populationSize);
        }

        public void GenerateCityData(int numberOfCities)
        {
            Cities = new List<City>();
            int minX = 0;
            int maxX = 100;
            int minY = 0;
            int maxY = 100;

            for (int i = 0; i < numberOfCities; i++)
            {
                double x = ThreadSafeRandom.CurrentThreadRandom.NextDouble() * (maxX - minX) + minX;
                double y = ThreadSafeRandom.CurrentThreadRandom.NextDouble() * (maxY - minY) + minY;
                City city = new City("City " + i, x, y);

                Cities.Add(city);
            }

        }

        public void GenerateInitialPopulation(int populationSize)
        {
            Chromosomes = new List<Chromosome>();

            for (int i = 0; i < populationSize; i++)
            {
                List<City> randomizedCities = new List<City>(Cities);
                randomizedCities.Shuffle();
                Chromosomes.Add(new Chromosome(randomizedCities));
            }
        }

        public void ResetChromosomeFitness()
        {
            foreach (Chromosome solution in Chromosomes)
            {
                solution.ResetCost();
            }
        }

        public void NextGeneration(ISelection selectionScheme)
        {
            var selected = selectionScheme.Select(this.Chromosomes, (int)Math.Floor(Math.Sqrt(Cities.Count)));

            // remove uncalculated chromosomes from population
            this.Chromosomes.RemoveAll(m => m.GetCost() == -1);
            Chromosomes = Chromosomes.OrderByDescending(c => c.GetCost()).ToList();

            // leave only top 10% of population
            while (this.Chromosomes.Count > PopulationSize / 10)
            {
                Chromosomes.RemoveAt(0);
            }

            if (currentGeneration % 5 == 0)
            {
                Console.WriteLine("Generation " + currentGeneration + " Best: " + Chromosomes.Min(c => c.GetCost()));
            }

            // fill the remaining empty space with children generated from selected chromosomes
            while (this.Chromosomes.Count < PopulationSize)
            {
                var parents = new RouletteWheelSelection().Select(selected, 2);
                Chromosome parent1 = parents[0];
                Chromosome parent2 = parents[1];

                //Chromosome parent1 = selected[ThreadSafeRandom.CurrentThreadRandom.Next(0, selected.Count - 1)];
                //Chromosome parent2 = selected[ThreadSafeRandom.CurrentThreadRandom.Next(0, selected.Count - 1)];

                Chromosome child = OrderedCrossover.MakeChild(parent1, parent2);
                if (ThreadSafeRandom.CurrentThreadRandom.NextDouble() < 0.5)
                {
                    child.Mutate();
                }

                Chromosomes.Add(child);
            }

            BestTour = Chromosomes.Where(c => c.Cost > 0).Min(c => c.Cost);
            currentGeneration++;
        }
    }
}
