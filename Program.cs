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
            new Day14().Star2($"{Environment.CurrentDirectory}/Input/day14sample.txt");
            new Day14().Star2($"{Environment.CurrentDirectory}/Input/day14.txt");

            Console.ReadKey();
        }
    }
}
