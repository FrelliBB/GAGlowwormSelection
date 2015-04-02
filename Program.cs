using GlowwormSelection.GeneticAlgorithm.SelectionSchemes;
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

            var gso = new GlowwormSwarmSelection().Select(p.chromosomes, 10);
            p.ResetChromosomeFitness();
            var tourn = new TournamentSelection().Select(p.chromosomes, 10);
            p.ResetChromosomeFitness();
            var trunc = new TruncateSelection().Select(p.chromosomes, 10);
            p.ResetChromosomeFitness();
            var roul = new RouletteWheelSelection().Select(p.chromosomes, 10);



            Console.ReadLine();
        }


    }
}
