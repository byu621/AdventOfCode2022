using System;
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
            new Day12().Star1($"{Environment.CurrentDirectory}/Input/day12sample.txt");
            new Day12().Star1($"{Environment.CurrentDirectory}/Input/day12.txt");
            //new Day12().Star2($"{Environment.CurrentDirectory}/Input/day12sample.txt");
            //new Day12().Star2($"{Environment.CurrentDirectory}/Input/day12.txt");
            Console.ReadKey();
        }
    }
}
