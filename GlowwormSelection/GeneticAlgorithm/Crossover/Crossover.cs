using GlowwormSelection.Extensions;
using GlowwormSelection.GeneticAlgorithm.TSP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlowwormSelection.GeneticAlgorithm.Crossover
{
    class OrderedCrossover
    {
        public static Chromosome MakeChild(Chromosome parent1, Chromosome parent2)
        {
            //select subset of cities from p1

            //select 2 points for the start and end of the subset
            int size = parent1.cities.Count;

            int point1 = ThreadSafeRandom.CurrentThreadRandom.Next(0, size - 1);
            int point2 = ThreadSafeRandom.CurrentThreadRandom.Next(0, size);

            int subsetStart = Math.Min(point1, point2);
            int subsetEnd = Math.Max(point1, point2);

            List<City> child = new List<City>();

            // select the subset from the first parent
            List<City> subset = parent1.cities.GetRange(subsetStart, subsetEnd - subsetStart);

            // order the subset so that they have the same order as in p2
            Queue<City> orderedSubset = new Queue<City>();

            for (int i = 0; i < size; i++)
            {
                if (subset.Contains(parent2.cities[i]))
                {
                    orderedSubset.Enqueue(parent2.cities[i]);
                }
            }

            // create the child
            for (int i = 0; i < size; i++)
            {
                City curr = parent1.cities[i];
                if (subset.Contains(curr))
                {
                    // if the current city is in the subset, get the ordered crossover city from the orderedsubset
                    child.Add(orderedSubset.Dequeue());
                }
                else
                {
                    // else just add the city
                    child.Add(curr);
                }
            }

            return new Chromosome(child);
        }
    }
}
