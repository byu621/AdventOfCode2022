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
            new Day10().Star1($"{Environment.CurrentDirectory}/Input/day10sample.txt");
            new Day10().Star1($"{Environment.CurrentDirectory}/Input/day10.txt");
            new Day10().Star2($"{Environment.CurrentDirectory}/Input/day10.txt");
            Console.ReadKey();
        }
    }
}
