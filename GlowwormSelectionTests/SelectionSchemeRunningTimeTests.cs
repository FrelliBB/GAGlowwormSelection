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
        private const int ITERATIONS = 100;

        [TestMethod]
        public void RunningTimeCityCountTest()
        {
            int[] cityCounts = { 100, 500, 2500, 5000, 10000, 10000 };
            ISelection[] schemes = { new TournamentSelection(), new GlowwormSwarmSelection(), new TruncateSelection(), new RouletteWheelSelection() };

            StringBuilder sb = new StringBuilder();

            //CPU warmup
            foreach (var scheme in schemes)
            {
                for (int i = 0; i < ITERATIONS; i++)
                {
                    GetAverageRuntime(scheme, numberOfCities: 100, populationSize: 100);
                }
            }

            sb.Append("City Count,");
            foreach (var scheme in schemes)
            {
                sb.Append(scheme.GetType().Name + ",");
            } sb.AppendLine();

            foreach (var cityCount in cityCounts)
            {
                sb.Append(cityCount + ",");
                foreach (var scheme in schemes)
                {
                    double runtime = GetAverageRuntime(scheme, numberOfCities: cityCount, populationSize: 100);
                    sb.Append(runtime + ",");
                }

                sb.AppendLine();
            }

            Console.WriteLine(sb.ToString());
        }

        [TestMethod]
        public void RunningTimePopulationSizeTest()
        {
            ISelection[] schemes = { new TournamentSelection(), new GlowwormSwarmSelection(), new TruncateSelection(), new RouletteWheelSelection() };

            StringBuilder sb = new StringBuilder();

            //CPU warmup
            foreach (var scheme in schemes)
            {
                for (int i = 0; i < ITERATIONS; i++)
                {
                    GetAverageRuntime(scheme, numberOfCities: 100, populationSize: 100);
                }
            }

            sb.Append("Population Size,");
            foreach (var scheme in schemes)
            {
                sb.Append(scheme.GetType().Name + ",");
            } sb.AppendLine();

            for (int i = 5; i < 25; i++)
            {
                int popSize = i * i;
                sb.Append(popSize + ",");
                foreach (var scheme in schemes)
                {
                    double runtime = GetAverageRuntime(scheme, numberOfCities: 5, populationSize: popSize);
                    sb.Append(runtime + ",");
                }

                sb.AppendLine();
            }

            Console.WriteLine(sb.ToString());
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
