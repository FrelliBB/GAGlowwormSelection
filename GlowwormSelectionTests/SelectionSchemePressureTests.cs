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
            Population p = new Population(numberOfCities: 100, populationSize: 100);

            Dictionary<Chromosome, int> d = new Dictionary<Chromosome, int>();

            foreach (var chrom in p.Chromosomes)
            {
                chrom.GetFitness();
                d.Add(chrom, 0);
            }

            TournamentSelection ts = new TournamentSelection();
            RouletteWheelSelection rws = new RouletteWheelSelection();
            GlowwormSwarmSelection gso = new GlowwormSwarmSelection();
            TruncateSelection trs = new TruncateSelection();
            for (int i = 0; i < 100000; i++)
            {
                var selected = gso.Select(p.Chromosomes, 2);

                foreach (var item in selected)
                {
                    d[item]++;
                }

            }

            var result = d.OrderByDescending(c => c.Key.GetFitness());

            int x = 2;
        }
    }
}
