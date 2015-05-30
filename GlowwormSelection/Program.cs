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
            //ThreadSafeRandom.InitializeSeed(1337);
            //int numberOfGenerations = 1000;
            //Population p = new Population(1000, 225);

            //for (int i = 0; i < numberOfGenerations; i++)
            //{
            //    p.NextGeneration(new GlowwormSwarmSelection());
            //}

            //Console.ReadLine();

            SelectionSchemeConvergenceTest();

            Console.ReadLine();
        }

        public static void SelectionSchemeConvergenceTest()
        {
            int optimalTourLength;
            List<City> cities = ParseFile("dj38.tsp", out optimalTourLength);
            Population p = new Population(cities, 100);

            //p.NextGeneration2(new GlowwormSwarmSelection());

            int generations = 0;
            do
            {
                p.NextGeneration(new RouletteWheelSelection());
                generations++;
            } while (p.BestTour > optimalTourLength);

            Console.WriteLine("----DONE----");
            Console.WriteLine(generations);
            Console.WriteLine(p.BestTour);
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
