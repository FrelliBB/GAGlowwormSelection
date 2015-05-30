using GlowwormSelection.Extensions;
using GlowwormSelection.GeneticAlgorithm.TSP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlowwormSelection.GeneticAlgorithm.SelectionSchemes
{
    public class TournamentSelection : ISelection
    {
        public List<Chromosome> Select(List<Chromosome> population, int number)
        {
            var candidates = population.ToList();
            var selected = new List<Chromosome>();

            foreach (var item in population)
            {
                item.GetFitness();
            }

            while (selected.Count < number)
            {
                int[] participantIndices = { ThreadSafeRandom.CurrentThreadRandom.Next(0, candidates.Count), ThreadSafeRandom.CurrentThreadRandom.Next(0, candidates.Count) };
                var tournamentWinner = candidates.Where((c, i) => participantIndices.Contains(i)).OrderByDescending(c => c.GetFitness()).First();

                selected.Add(tournamentWinner);
                candidates.Remove(tournamentWinner);
            }

            return selected;
        }
    }
}
