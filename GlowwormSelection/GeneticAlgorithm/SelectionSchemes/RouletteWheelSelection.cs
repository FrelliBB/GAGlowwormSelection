using GlowwormSelection.Extensions;
using GlowwormSelection.GeneticAlgorithm.TSP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlowwormSelection.GeneticAlgorithm.SelectionSchemes
{
    public class RouletteWheelSelection : ISelection
    {
        public List<Chromosome> Select(List<Chromosome> population, int number)
        {
            var selected = new List<Chromosome>();
            var maxCost = population.Max(c => c.Cost);
            var sumFitness = population.Sum(c => maxCost - c.Cost);
            var rouleteWheel = new List<double>();
            var accumulativePercent = 0.0;

            foreach (var c in population)
            {
                accumulativePercent += (maxCost - c.Cost) / sumFitness;
                rouleteWheel.Add(accumulativePercent);
            }

            for (int i = 0; i < number; i++)
            {
                var pointer = ThreadSafeRandom.CurrentThreadRandom.NextDouble();
                var chromosomeIndex = rouleteWheel.Select((value, index) => new { Value = value, Index = index }).FirstOrDefault(r => r.Value >= pointer).Index;
                selected.Add(population[chromosomeIndex]);
            }

            return selected;
        }
    }
}
