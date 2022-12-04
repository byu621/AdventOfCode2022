using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day4
    {
        public static void Star1()
        {
            string[] lines = File.ReadAllLines($"{Environment.CurrentDirectory}/Input/day4.txt");
            int output = 0;
            foreach (var line in lines)
            {
                var asdf = line.Split(',');
                int bottomBound1 = Int32.Parse(asdf[0].Split('-')[0]);
                int bottomBound2 = Int32.Parse(asdf[1].Split('-')[0]);
                int upperbound1 = Int32.Parse(asdf[0].Split('-')[1]);
                int upperbound2 = Int32.Parse(asdf[1].Split('-')[1]);

                if (bottomBound1 <= bottomBound2 && upperbound1 >= upperbound2)
                {
                    output++;
                }else if (bottomBound2 <= bottomBound1 && upperbound2 >= upperbound1)
                {
                    output++;
                }
            }
            Console.WriteLine(output);
        }

        public static void Star2()
        {
            string[] lines = File.ReadAllLines($"{Environment.CurrentDirectory}/Input/day4.txt");
            int output = 0;
            foreach (var line in lines)
            {
                var asdf = line.Split(',');
                int bottomBound1 = Int32.Parse(asdf[0].Split('-')[0]);
                int bottomBound2 = Int32.Parse(asdf[1].Split('-')[0]);
                int upperbound1 = Int32.Parse(asdf[0].Split('-')[1]);
                int upperbound2 = Int32.Parse(asdf[1].Split('-')[1]);

                if (bottomBound1 >= bottomBound2 && bottomBound1 <= upperbound2)
                {
                    output++;
                }
                else if (bottomBound2 >= bottomBound1 && bottomBound2 <= upperbound1)
                {
                    output++;
                }
            }
            Console.WriteLine(output);
        }
    }
}
