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
        private Queue<int> queue = new Queue<int>();
        public void Star1(string input)
        {
            string[] lines = File.ReadAllLines(input);
            foreach (string line in lines)
            {
                if (line == "noop")
                {
                    queue.Enqueue(0);
                } else
                {
                    int number = int.Parse(line.Split(' ')[1]);
                    queue.Enqueue(number);
                }
            }

            int current = -1;
            int output = 1;
            int state = 0; //0
            int res = 0;
            for (int cycle = 1; cycle < 230; cycle++)
            {
                //start
                if (state == 0)
                {
                    current = queue.Dequeue();
                    state = current == 0 ? 1 : 2;
                }
                //during

                if (cycle == 20 || cycle == 60 || cycle == 100 || cycle == 140 || cycle == 180 || cycle == 220)
                {
                    Console.WriteLine($"{cycle} - {output}");
                    res += cycle * output;
                }

                //after
                state--;
                if (state == 0)
                {
                    output += current;
                }
            }

            Console.WriteLine(res);
        }

        public void Star2(string input)
        {
            string pattern = "";
            string[] lines = File.ReadAllLines(input);
            foreach (string line in lines)
            {
                if (line == "noop")
                {
                    queue.Enqueue(0);
                }
                else
                {
                    int number = int.Parse(line.Split(' ')[1]);
                    queue.Enqueue(number);
                }
            }

            int current = -1;
            int output = 1;
            int state = 0; //0
            int res = 0;
            for (int cycle = 1; cycle <= 240; cycle++)
            {
                //start
                if (state == 0)
                {
                    current = queue.Dequeue();
                    state = current == 0 ? 1 : 2;
                }

                //during
                if (cycle == output - 1 || cycle == output || cycle == output + 1)
                {
                    pattern += '#';
                } else
                {
                    pattern += '.';
                }
                if (cycle % 40 == 0)
                {
                    pattern += '\n';
                }

                //after
                state--;
                if (state == 0)
                {
                    output += current;
                }
            }

            Console.WriteLine(pattern);
        }
    }
}