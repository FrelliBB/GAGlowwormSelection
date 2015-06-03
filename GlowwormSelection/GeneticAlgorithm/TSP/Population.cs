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
            var selected = selectionScheme.Select(Chromosomes, 5).OrderByDescending(s => s.Cost).ToList();

            Chromosomes.Remove(selected[0]);
            Chromosomes.Remove(selected[1]);

            // fill the remaining empty space with children generated from selected chromosomes
            Chromosome parent1 = selected[3];
            Chromosome parent2 = selected[4];

            Chromosome child1 = OrderedCrossover.MakeChild(parent1, parent2);
            Chromosome child2 = OrderedCrossover.MakeChild(parent2, parent1);

            if (ThreadSafeRandom.CurrentThreadRandom.Next(100) < 3)
            {
                child1.Mutate();
            }
            if (ThreadSafeRandom.CurrentThreadRandom.Next(100) < 3)
            {
                child2.Mutate();
            }

            Chromosomes.Add(child1);
            Chromosomes.Add(child2);

            double currentTour = Chromosomes.Where(c => c.Cost > 0).Min(c => c.Cost);

            if (currentTour < BestTour)
            {
                BestTour = currentTour;
                Console.WriteLine("Generation " + currentGeneration + " Best: " + BestTour);
            }

            currentGeneration++;

        }
    }
}
