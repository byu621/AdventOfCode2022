﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Program
    {


        static void Main(string[] args)
        {
            //new Day20().Star1($"{Environment.CurrentDirectory}/Input/day20sample.txt");
            //new Day21().Star2($"{Environment.CurrentDirectory}/Input/day21sample.txt");
            new Day22Star2().Star1($"{Environment.CurrentDirectory}/Input/day22sample.txt");
            new Day22Star2().Star1($"{Environment.CurrentDirectory}/Input/day22.txt");
            Console.WriteLine("finished");
            Console.ReadKey();
        }
    }
}
