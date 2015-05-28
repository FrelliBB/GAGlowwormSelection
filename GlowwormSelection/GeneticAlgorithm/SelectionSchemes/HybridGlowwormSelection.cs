using GlowwormSelection.Extensions;
using GlowwormSelection.GeneticAlgorithm.TSP;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GlowwormSelection.GeneticAlgorithm.SelectionSchemes
{
    public class HybridGlowwormSwarmSelection : ISelection
    {
        public List<TSP.Chromosome> Select(List<TSP.Chromosome> population, int number)
        {
            if (Math.Sqrt(population.Count) % 1 != 0)
            {
                throw new Exception("Non-square chromosomes sizes not supported for glowworm selection.");
            }

            int arraySize = (int)Math.Sqrt(population.Count);
            float initialLuciferin = 0.5f;
            List<Glowworm> glowworms = new List<Glowworm>(); //the glowworms that traverse the population for the selection process

            // 1. Transform population into 2D array

            // Randomize our population
            population.Shuffle();

            Chromosome[,] solutionSpace = new Chromosome[arraySize, arraySize];
            float glowwormRange = arraySize / 3f;

            // Assign population to our 2D array
            Stack<Chromosome> solutionsToAdd = new Stack<Chromosome>(population);
            for (int i = 0; i <= solutionSpace.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= solutionSpace.GetUpperBound(1); j++)
                {
                    solutionSpace[i, j] = solutionsToAdd.Pop();
                }
            }

            // 2. Distribute glowworms randomly across a 2d plane
            int glowwormCount = (population.Count + 9) / 10;
            for (int i = 0; i < glowwormCount; i++)
            {
                glowworms.Add(new Glowworm(i, ThreadSafeRandom.CurrentThreadRandom.Next(arraySize), ThreadSafeRandom.CurrentThreadRandom.Next(arraySize), initialLuciferin));
            }


            // 3. Repeat 4-5 for x steps for x movement phases
            for (int i = 0; i < population.Count / 4; i++)
            {

                //Perform initial luciferin update
                foreach (Glowworm glowworm in glowworms)
                {
                    glowworm.Luciferin += solutionSpace[glowworm.X, glowworm.Y].GetFitness();
                }

                bool movementOccured = false;

                // 4. Glowworm movement
                foreach (Glowworm currentGlowworm in glowworms)
                {
                    Glowworm closestGlowworm = null;
                    double closestDistance = double.MaxValue;

                    //Find glowworm in range with the highest luciferin value
                    foreach (Glowworm neighbouringGlowworm in glowworms)
                    {
                        // Make sure that the current glowworm isn't the same as the neighbour
                        if (!(currentGlowworm.ID == neighbouringGlowworm.ID || (currentGlowworm.X == neighbouringGlowworm.X && currentGlowworm.Y == neighbouringGlowworm.Y)))
                        {
                            // check if neighbouring glowworm has higher luciferin value
                            if (neighbouringGlowworm.Luciferin > currentGlowworm.Luciferin)
                            {
                                //check if neighbouring glowworm is in range and closer than current closest glowworm (using euclidean distance)
                                double distance = Math.Sqrt(Math.Pow(currentGlowworm.X - neighbouringGlowworm.X, 2) + Math.Pow(currentGlowworm.Y - neighbouringGlowworm.Y, 2));

                                if (distance <= glowwormRange && distance < closestDistance)
                                {
                                    closestDistance = distance;
                                    closestGlowworm = neighbouringGlowworm;
                                }
                            }
                        }
                    }

                    if (closestGlowworm != null)
                    {
                        // If a glowworn with a higher luciferin was found in range move towards it
                        currentGlowworm.MoveTowardsNeighbour(closestGlowworm.X, closestGlowworm.Y);
                        movementOccured = true;
                    }
                }

                if (!movementOccured)
                {
                    break;
                }
            }

            // Find best solutions
            List<Chromosome> traversedSolutions = new List<Chromosome>();
            for (int i = 0; i < solutionSpace.GetUpperBound(0); i++)
            {
                for (int j = 0; j < solutionSpace.GetUpperBound(1); j++)
                {
                    if (solutionSpace[i, j].GetCost() != -1)
                    {
                        traversedSolutions.Add(solutionSpace[i, j]);
                    }
                }
            }

            return new RouletteWheelSelection().Select(traversedSolutions, number);
        }
    }
}
