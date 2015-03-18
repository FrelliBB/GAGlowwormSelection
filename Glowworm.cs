using GlowwormSelection.Extensions;
using GlowwormSelection.TSP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlowwormSelection
{
    class Glowworm
    {
        public int ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public double Luciferin { get; set; }

        public Glowworm(int id, int x, int y, double luciferinStart)
        {
            this.ID = id;
            this.X = x;
            this.Y = y;
            this.Luciferin = luciferinStart;
        }

        public void MoveRandomly(int boundsX, int boundsY)
        {
            bool moved = false;

            do
            {
                // decide wether we are moving on the x or the y axis
                //generate 0 or 1, if 0 move on x axis, if 1 move on y axis
                int axis = ThreadSafeRandom.CurrentThreadRandom.Next(2);

                // decide if we will move +1 or -1 on the chosen axis
                // generate 0 or 1, if 1 move positive direction, if 0 move negative direction
                int direction = ThreadSafeRandom.CurrentThreadRandom.Next(2) == 1 ? 1 : -1;

                // If the chosen movement is legal do it
                switch (axis)
                {
                    case 0:
                        // x axis movement
                        int newPosX = this.X + direction;
                        if (newPosX >= 0 && newPosX <= boundsX)
                        {
                            this.X = newPosX;
                            moved = true;
                        }
                        break;
                    case 1:
                        // y axis movement
                        int newPosY = this.Y + direction;
                        if (newPosY >= 0 && newPosY <= boundsY)
                        {
                            this.Y = newPosY;
                            moved = true;
                        }
                        break;
                    default:
                        // This should never happen
                        throw new Exception("Illegal axis chosen. Something went wrong.");
                }

                // Repeat until movement occured
            } while (!moved);

        }

        public void MoveTowardsNeighbour(int neighbourX, int neighbourY)
        {
            Console.WriteLine(String.Format("Worm {0} at ({1},{2}) is moving towards worm at location ({3},{4})", ID, X, Y, neighbourX, neighbourY));
            bool moved = false;

            do
            {
                // decide wether we are moving on the x or the y axis
                //generate 0 or 1, if 0 move on x axis, if 1 move on y axis
                int axis = ThreadSafeRandom.CurrentThreadRandom.Next(2);

                // check if movement is even required on the given axis (if not continue)
                switch (axis)
                {
                    case 0:
                        // x axis movement
                        if (this.X == neighbourX)
                        {
                            //we have already reached our neighbour
                            continue;
                        }
                        else if (this.X - neighbourX < 0)
                        {
                            // glowworm position is lower than neighbour's, therefore we move in a positive direction
                            this.X++;
                            moved = true;
                        }
                        else if (this.X - neighbourX > 0)
                        {
                            this.X--;
                            moved = true;
                        }

                        break;
                    case 1:
                        // y axis movement
                        if (this.Y == neighbourY)
                        {
                            //we have already reached our neighbour
                            continue;
                        }
                        else if (this.Y - neighbourY < 0)
                        {
                            // glowworm position is lower than neighbour's, therefore we move in a positive direction
                            this.Y++;
                            moved = true;
                        }
                        else if (this.Y - neighbourY > 0)
                        {
                            this.Y--;
                            moved = true;
                        }
                        break;
                    default:
                        // This should never happen
                        throw new Exception("Illegal axis chosen. Something went wrong.");
                }

            } while (!moved);

            Console.WriteLine(String.Format("Worm {0} at ({1},{2})", ID, X, Y));
        }
    }
}
