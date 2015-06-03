using GlowwormSelection.Extensions;
using GlowwormSelection.GeneticAlgorithm.Crossover;
using GlowwormSelection.GeneticAlgorithm.SelectionSchemes;
using GlowwormSelection.GeneticAlgorithm.TSP;
using System;
using System.Collections.Generic;
using System.IO;

namespace GlowwormSelection
{
    class Program
    {
        static void Main(string[] args)
        {
            SelectionSchemeConvergenceTest();

            Console.ReadLine();
        }

        public static void SelectionSchemeConvergenceTest()
        {
            int successCount = 0;
            int maxGenerations = 100000;

            int optimalTourLength;
            List<City> cities = ParseFile("wi29.tsp", out optimalTourLength);

            for (int i = 0; i < 100; i++)
            {
                Population p = new Population(cities, 1024);

                int generations = 0;
                do
                {
                    p.NextGeneration(new RouletteWheelSelection());
                    generations++;
                } while (p.BestTour > optimalTourLength && generations < maxGenerations);

                if (p.BestTour == optimalTourLength)
                {
                    successCount++;
                }

                Console.WriteLine("----DONE----");
                Console.WriteLine(generations);
                Console.WriteLine(p.BestTour);
            }

            Console.WriteLine("----DONE----");
            Console.WriteLine("Success COunt: " + successCount);

        }

        public static List<City> ParseFile(string filename, out int tour)
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
