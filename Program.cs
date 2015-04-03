using GlowwormSelection.GeneticAlgorithm.Crossover;
using GlowwormSelection.GeneticAlgorithm.SelectionSchemes;
using GlowwormSelection.GeneticAlgorithm.TSP;
using System;

namespace GlowwormSelection
{
    class Program
    {
        static void Main(string[] args)
        {

            int numberOfGenerations = 50;
            Population p = new Population(1000, 400);

            for (int i = 0; i < numberOfGenerations; i++)
            {
                p.NextGeneration();
            }

            Console.ReadLine();
        }


    }
}
