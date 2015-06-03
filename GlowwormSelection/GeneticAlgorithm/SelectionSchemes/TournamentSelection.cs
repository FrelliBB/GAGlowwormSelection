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
            int tournamentSize = 5;
            var candidates = population.ToList();
            var selected = new List<Chromosome>();

            while (selected.Count < number)
            {
                int[] participantIndices = new int[tournamentSize];
                for (int i = 0; i < tournamentSize; i++)
                {
                    participantIndices[i] = ThreadSafeRandom.CurrentThreadRandom.Next(0, candidates.Count);
                }
                var tournamentWinner = candidates.Where((c, i) => participantIndices.Contains(i)).OrderByDescending(c => c.GetFitness()).First();

                selected.Add(tournamentWinner);
                candidates.Remove(tournamentWinner);
            }

            return selected;
        }
    }
}
