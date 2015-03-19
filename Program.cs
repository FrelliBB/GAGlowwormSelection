using GlowwormSelection.GeneticAlgorithm.TSP;
using System;

namespace GlowwormSelection
{
    class Program
    {
        static void Main(string[] args)
        {
            Population p = new Population();
            p.GenerateCityData(1000);
            p.GenerateInitialPopulation(100);
            new GlowwormSelection.GeneticAlgorithm.SelectionSchemes.GlowwormSelection().Select(p.population, 10);

            Console.ReadLine();
        }
    }
}
