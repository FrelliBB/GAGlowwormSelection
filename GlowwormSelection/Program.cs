using GlowwormSelection.Extensions;
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
            ThreadSafeRandom.InitializeSeed(1337);
            int numberOfGenerations = 1000;
            Population p = new Population(1000, 225);

            for (int i = 0; i < numberOfGenerations; i++)
            {
                p.NextGeneration(new GlowwormSwarmSelection());
            }

            Console.ReadLine();
        }


    }
}
