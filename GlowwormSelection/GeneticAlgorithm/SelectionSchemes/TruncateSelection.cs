using GlowwormSelection.GeneticAlgorithm.TSP;
using System.Collections.Generic;
using System.Linq;

namespace GlowwormSelection.GeneticAlgorithm.SelectionSchemes
{
    class TruncateSelection : ISelection
    {
        public List<Chromosome> Select(List<Chromosome> population, int number)
        {
            return population.OrderBy(p => p.GetFitness()).Take(10).ToList();
        }
    }
}
