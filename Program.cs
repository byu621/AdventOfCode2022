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
            Day8.Star1($"{Environment.CurrentDirectory}/Input/day8sample.txt");
            Day8.Star1($"{Environment.CurrentDirectory}/Input/day8.txt");
            Day8.Star2($"{Environment.CurrentDirectory}/Input/day8.txt");
            Console.ReadKey();
        }
    }
}
