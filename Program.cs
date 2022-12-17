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
            new Day16().Star1($"{Environment.CurrentDirectory}/Input/day16sample.txt");
            new Day16().Star1($"{Environment.CurrentDirectory}/Input/day16.txt");

            Console.ReadKey();
        }
    }
}
