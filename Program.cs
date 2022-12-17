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
            //new Day13().Star1($"{Environment.CurrentDirectory}/Input/day13sample.txt");
            //new Day13().Star1($"{Environment.CurrentDirectory}/Input/day13.txt");

            new Day13().Star2($"{Environment.CurrentDirectory}/Input/day13sample.txt");
            new Day13().Star2($"{Environment.CurrentDirectory}/Input/day13.txt");

            Console.ReadKey();
        }
    }
}
