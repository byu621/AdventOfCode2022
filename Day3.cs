using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day3
    {
        private static Dictionary<char, int> dict1 = new Dictionary<char, int>()
        {
            {'X', 1}, {'Y', 2}, {'Z', 3}
        };

        public static void Calculate()
        {
            string[] lines = File.ReadAllLines($"{Environment.CurrentDirectory}/Input/day3.txt");
            int output = 0;
            char common = ' ';
            foreach (var line in lines)
            {
                string firstHalf = line.Substring(0, line.Length / 2);
                string secondHalf = line.Substring(line.Length / 2, line.Length / 2);

                foreach (char c in firstHalf)
                {
                    if (secondHalf.Contains(c))
                    {
                        common = c;
                        break;
                    }
                }

                if (common >= 97 && common <= 122)
                {
                    output += common - 96;
                }
                else
                {
                    output += common - 65 + 27;
                }
            }
            Console.WriteLine(output);
        }

        public static void Calculate2()
        {
            string[] lines = File.ReadAllLines($"{Environment.CurrentDirectory}/Input/day3.txt");
            int output = 0;
            char common = ' ';
            for (int i = 0; i < lines.Length; i+=3)
            {
                string[] ben = new string[] { lines[i], lines[i + 1], lines[i + 2] };

                foreach (char c in ben[0])
                {
                    if (ben[1].Contains(c) && ben[2].Contains(c))
                    {
                        common = c;
                        break;
                    }
                }

                if (common >= 97 && common <= 122)
                {
                    output += common - 96;
                }
                else
                {
                    output += common - 65 + 27;
                }
            }
            Console.WriteLine(output);
        }
    }
}
