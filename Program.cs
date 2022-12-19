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
            //new Day16Star2().Star2($"{Environment.CurrentDirectory}/Input/day16sample.txt");
            //new Day17().Star1($"{Environment.CurrentDirectory}/Input/day17sample.txt");
            //Dictionary<int, int> dict = new Dictionary<int, int>();
            //for (int i = 1; i < 100; i++)
            //{
            //    Console.WriteLine(i);
            //    dict.Add(i, new Day17().Star1($"{Environment.CurrentDirectory}/Input/day17.txt", i));
            //    Console.WriteLine(dict[i]);
            //    if (i % 2 == 0 && dict[i] % dict[i / 2] == 0)
            //    {
            //        Console.WriteLine("got em");
            //    }
            //}
            //new Day17().Star1($"{Environment.CurrentDirectory}/Input/day17sample.txt", 1);
            //new Day18().Star1($"{Environment.CurrentDirectory}/Input/day18sample.txt");
            new Day18().Star1($"{Environment.CurrentDirectory}/Input/day18sample.txt");
            new Day18().Star1($"{Environment.CurrentDirectory}/Input/day18.txt");
            Console.WriteLine("finished");
            Console.ReadKey();
        }
    }
}
