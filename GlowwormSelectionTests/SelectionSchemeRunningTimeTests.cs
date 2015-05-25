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
            int[] cityCounts = { 100, 1000, 10000, 100000, 1000000 };

            //CPU warmup
            GetAverageRuntime(new GlowwormSwarmSelection(), numberOfCities: 100, populationSize: 100);
            GetAverageRuntime(new RouletteWheelSelection(), numberOfCities: 100, populationSize: 100);
            GetAverageRuntime(new TruncateSelection(), numberOfCities: 100, populationSize: 100);
            GetAverageRuntime(new TournamentSelection(), numberOfCities: 100, populationSize: 100);

            //print results
            foreach (var cityCount in cityCounts)
            {
                Console.WriteLine("City Count: " + cityCount);

                double runTimeGlowworm = GetAverageRuntime(new GlowwormSwarmSelection(), numberOfCities: cityCount, populationSize: 100);
                Console.WriteLine("GSO: " + runTimeGlowworm + "ms");

                double runtimeRoulette = GetAverageRuntime(new RouletteWheelSelection(), numberOfCities: cityCount, populationSize: 100);
                Console.WriteLine("Roulette: " + runtimeRoulette + "ms");

                double runtimeTruncate = GetAverageRuntime(new TruncateSelection(), numberOfCities: cityCount, populationSize: 100);
                Console.WriteLine("Truncate: " + runtimeTruncate + "ms");

                double runtimeTournament = GetAverageRuntime(new TournamentSelection(), numberOfCities: cityCount, populationSize: 100);
                Console.WriteLine("Tournament: " + runtimeTournament + "ms");

                Console.WriteLine();
            }
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
