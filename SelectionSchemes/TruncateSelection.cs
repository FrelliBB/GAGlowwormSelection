using GlowwormSelection.TSP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlowwormSelection.SelectionSchemes
{
    class TruncateSelection : ISelection
    {
        public List<Chromosome> Select(List<Chromosome> population, int number)
        {
            return population.OrderBy(p => p.GetFitness()).Take(10).ToList();
        }
    }
}
