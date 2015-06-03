using GlowwormSelection.GeneticAlgorithm.SelectionSchemes;
using GlowwormSelection.GeneticAlgorithm.TSP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlowwormSelectionTests
{
    [TestClass]
    public class GSONumberOfChromosomesCalculatedCount
    {
        [TestMethod]
        public void GSONumberOfChromosomesCalculatedCountTest()
        {
            int iterations = 100;
            GlowwormSwarmSelection gso = new GlowwormSwarmSelection();

            Console.WriteLine("Population Size,Selection Percent");

            for (int i = 33; i < 35; i++)
            {
                int populationSize = i * i;
                int totalSelected = 0;

                for (int j = 0; j < iterations; j++)
                {
                    Population p = new Population(100, populationSize);
                    gso.Select(p.Chromosomes, 1);

                    totalSelected += p.Chromosomes.Count(c => c.Cost != -1);
                }

                double averageSelected = ((double)totalSelected / (double)iterations) / (double)populationSize;
                Console.WriteLine(populationSize + "," + (averageSelected*100).ToString("#.##") + "%");
            }
        }
    }
}
