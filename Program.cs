﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlowwormSelection
{
    class Program
    {
        static void Main(string[] args)
        {
            Population p = new Population();
            p.GenerateCityData(100);
            p.GenerateInitialPopulation(25);
        }
    }
}
