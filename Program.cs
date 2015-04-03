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


            Population p = new Population();
            p.GenerateCityData(10);
            p.GenerateInitialPopulation(10);

            var parent1 = p.chromosomes[0];
            var parent2 = p.chromosomes[1];

            var child1 = Crossover.MakeChild(parent1, parent2);
            var child2 = Crossover.MakeChild(parent2, parent1);

            Console.Write("Parent 1:\t");
            foreach (var item in parent1.cities)
            {
                Console.Write(item.Name + " ");
            }

            Console.WriteLine();

            Console.Write("Parent 2:\t");
            foreach (var item in parent2.cities)
            {
                Console.Write(item.Name + " ");
            }

            Console.WriteLine();

            Console.Write("Child 1:\t");
            foreach (var item in child1.cities)
            {
                Console.Write(item.Name + " ");
            }

            Console.WriteLine();

            Console.Write("Child 2:\t");
            foreach (var item in child2.cities)
            {
                Console.Write(item.Name + " ");
            }

            //var gso = new GlowwormSwarmSelection().Select(p.chromosomes, 10);
            //p.ResetChromosomeFitness();
            //var tourn = new TournamentSelection().Select(p.chromosomes, 10);
            //p.ResetChromosomeFitness();
            //var trunc = new TruncateSelection().Select(p.chromosomes, 10);
            //p.ResetChromosomeFitness();
            //var roul = new RouletteWheelSelection().Select(p.chromosomes, 10);



            Console.ReadLine();
        }


    }
}
