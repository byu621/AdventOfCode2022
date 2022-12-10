using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{

    /**
     * 
     * The interesting signal strengths can be determined as follows:

During the 20th cycle, register X has the value 21, so the signal strength is 20 * 21 = 420. (The 20th cycle occurs in the middle of the second addx -1, so the value of register X is the starting value, 1, plus all of the other addx values up to that point: 1 + 15 - 11 + 6 - 3 + 5 - 1 - 8 + 13 + 4 = 21.)
During the 60th cycle, register X has the value 19, so the signal strength is 60 * 19 = 1140.
During the 100th cycle, register X has the value 18, so the signal strength is 100 * 18 = 1800.
During the 140th cycle, register X has the value 21, so the signal strength is 140 * 21 = 2940.
During the 180th cycle, register X has the value 16, so the signal strength is 180 * 16 = 2880.
During the 220th cycle, register X has the value 18, so the signal strength is 220 * 18 = 3960.
The sum of these signal strengths is 13140.

Find the signal strength during the 20th, 60th, 100th, 140th, 180th, and 220th cycles. What is the sum of these six signal strengths?
     */
    class Day10
    {
        private Dictionary<int, int> dict = new Dictionary<int, int>();
        public void Star1(string input)
        {
            int output = 1;
            int cycle = 1;
            string[] lines = File.ReadAllLines(input);
            foreach (string line in lines)
            {
                //start
                if (line == "noop")
                {

                }
                else
                {
                    int number = int.Parse(line.Split(' ')[1]);
                    if (!dict.ContainsKey(cycle + 1))
                    {
                        dict[cycle + 1] = 0;
                    }

                    dict[cycle + 1] += number;
                }
                //during

                Console.WriteLine($"{cycle} - {output}");

                //after
                if (dict.ContainsKey(cycle))
                {
                    output += dict[cycle];
                }

                cycle++;
            }

            Console.WriteLine(output);
        }

        public void Star2(string input)
        {
            int output = 0;
            string[] lines = File.ReadAllLines(input);
            foreach (string line in lines)
            {

            }

            Console.WriteLine(output);
        }
    }
}