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
            new Day15().Star2($"{Environment.CurrentDirectory}/Input/day15sample.txt", 20);
            new Day15().Star2($"{Environment.CurrentDirectory}/Input/day15.txt", 4_000_000);

            Console.ReadKey();
        }
    }
}
