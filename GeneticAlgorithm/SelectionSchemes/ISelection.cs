using GlowwormSelection.TSP;
using System.Collections.Generic;

namespace GlowwormSelection.SelectionSchemes
{
    interface ISelection
    {
        List<Chromosome> Select(List<Chromosome> population, int number);
    }
}
