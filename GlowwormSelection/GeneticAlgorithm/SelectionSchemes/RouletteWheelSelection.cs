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
            var maxCost = population.Max(c => c.GetFitness());
            var sumFitness = population.Sum(c => maxCost - c.GetFitness());

            if (sumFitness <= 0)
            {
                return population;
            }

            var rouleteWheel = new List<double>();
            var accumulativePercent = 0.0;

            foreach (var c in population)
            {
                accumulativePercent += (c.GetFitness()) / sumFitness;
                rouleteWheel.Add(accumulativePercent);
            }

            for (int i = 0; i < number; i++)
            {
                var pointer = ThreadSafeRandom.CurrentThreadRandom.NextDouble();
                var chromosome = rouleteWheel.Select((value, index) => new { Value = value, Index = index }).FirstOrDefault(r => r.Value >= pointer);
                selected.Add(population[chromosome.Index]);
            }

            return selected;
        }
    }
}
