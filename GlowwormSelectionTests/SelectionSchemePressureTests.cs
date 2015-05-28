using GlowwormSelection.GeneticAlgorithm.SelectionSchemes;
using GlowwormSelection.GeneticAlgorithm.TSP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlowwormSelectionTests
{
    [TestClass]
    public class SelectionSchemePressureTests
    {
        [TestMethod]
        public void SelectionScehemePressureTest()
        {
            int iterations = 100000;
            int popSize = 100;

            Population p = new Population(numberOfCities: 1000, populationSize: popSize);

            foreach (var chrom in p.Chromosomes)
            {
                chrom.GetFitness();
            }

            ISelection[] schemes = { 
                new TournamentSelection(),
                new RouletteWheelSelection(),
                new GlowwormSwarmSelection(),
                new HybridGlowwormSwarmSelection(),
                new TruncateSelection()
            };

            bool printedFirstLine = false;
            foreach (var scheme in schemes)
            {
                Dictionary<Chromosome, int> d = new Dictionary<Chromosome, int>();
                foreach (var chrom in p.Chromosomes)
                {
                    d.Add(chrom, 0);
                }

                for (int i = 0; i < iterations; i++)
                {
                    var selected = scheme.Select(p.Chromosomes, 1);

                    foreach (var item in selected)
                    {
                        d[item]++;
                    }
                }

                var result = d.OrderByDescending(c => c.Key.GetFitness());

                if (!printedFirstLine)
                {
                    Console.Write("rank,");
                    for (int i = 0; i < result.Count(); i++)
                    {
                        Console.Write(i + ",");
                    }
                    Console.WriteLine();

                    Console.Write("cost,");
                    foreach (var item in result)
                    {
                        Console.Write(Convert.ToInt32(item.Key.Cost) + ",");
                    }
                    Console.WriteLine();
                    printedFirstLine = true;
                }

                Console.Write(scheme.GetType().Name + ",");
                foreach (var item in result)
                {
                    Console.Write(item.Value + ",");
                }
                Console.WriteLine();
            }
        }
    }
}
