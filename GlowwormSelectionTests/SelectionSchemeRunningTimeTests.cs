using GlowwormSelection.GeneticAlgorithm.SelectionSchemes;
using GlowwormSelection.GeneticAlgorithm.TSP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlowwormSelectionTests
{
    [TestClass]
    public class SelectionSchemeRunningTimeTests
    {
        private const int ITERATIONS = 5;

        [TestMethod]
        public void SelectionSchemeRunningTimeTest()
        {
            double averageRuntimeGlowworm100Cities = GetAverageRuntime(new GlowwormSwarmSelection(), numberOfCities: 100, populationSize: 100);
            double averageRuntimeGlowworm1000Cities = GetAverageRuntime(new GlowwormSwarmSelection(), numberOfCities: 1000, populationSize: 100);
            double averageRuntimeGlowworm10000Cities = GetAverageRuntime(new GlowwormSwarmSelection(), numberOfCities: 10000, populationSize: 100);

            Console.WriteLine(averageRuntimeGlowworm100Cities);
            Console.WriteLine(averageRuntimeGlowworm1000Cities);
            Console.WriteLine(averageRuntimeGlowworm10000Cities);
        }

        public double GetAverageRuntime(ISelection selectionScheme, int numberOfCities, int populationSize)
        {
            double totalTime = 0;
            Stopwatch s = new Stopwatch();
            Population p;

            p = new Population(numberOfCities, populationSize);

            for (int i = 0; i < ITERATIONS; i++)
            {
                p.ResetChromosomeFitness();
                s.Start();
                selectionScheme.Select(p.Chromosomes, 2);
                s.Stop();

                totalTime += s.Elapsed.TotalMilliseconds;

                s.Reset();
            }

            return totalTime / ITERATIONS;
        }
    }
}
