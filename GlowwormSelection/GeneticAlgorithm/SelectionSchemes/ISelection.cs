using GlowwormSelection.GeneticAlgorithm.TSP;
using System.Collections.Generic;

namespace GlowwormSelection.GeneticAlgorithm.SelectionSchemes
{
    public interface ISelection
    {
        List<Chromosome> Select(List<Chromosome> population, int number);
    }
}
