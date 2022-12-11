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
            //new Day11().Star1($"{Environment.CurrentDirectory}/Input/day11sample.txt");
            //new Day11().Star1($"{Environment.CurrentDirectory}/Input/day11.txt");
            new Day11().Star2($"{Environment.CurrentDirectory}/Input/day11sample.txt");
            new Day11().Star2($"{Environment.CurrentDirectory}/Input/day11.txt");
            Console.ReadKey();
        }
    }
}
