using GlowwormSelection.GeneticAlgorithm.SelectionSchemes;
using GlowwormSelection.GeneticAlgorithm.TSP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlowwormSelectionTests
{
    [TestClass]
    public class SelectionSchemeConvergenceTests
    {
        [TestMethod]
        public void SelectionSchemeConvergenceTest()
        {
            int optimalTourLength;
            List<City> cities = ParseFile("lu980.tsp", out optimalTourLength);
            Population p = new Population(cities, 100);

            //p.NextGeneration2(new GlowwormSwarmSelection());

            int generations = 0;
            do
            {
                p.NextGeneration(new RouletteWheelSelection());
                generations++;
                Console.WriteLine(p.BestTour);
            } while (p.BestTour > optimalTourLength && generations < 1000);

            Console.WriteLine(generations);
            Console.WriteLine(p.BestTour);
        }

        public List<City> ParseFile(string filename, out int tour)
        {
            bool readFirstLine = false;
            var lines = File.ReadLines(filename);
            int optimalTourLength = 0;
            List<City> cities = new List<City>();

            foreach (var line in lines)
            {
                if (readFirstLine)
                {
                    var lineData = line.Split(' ');
                    cities.Add(new City(lineData[0], Convert.ToDouble(lineData[1]), Convert.ToDouble(lineData[2])));
                }
                else
                {
                    optimalTourLength = Convert.ToInt32(line);
                    readFirstLine = true;
                }
            }

            tour = optimalTourLength;
            return cities;
        }
    }
}
