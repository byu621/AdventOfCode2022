using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day8
    {

        private static Dictionary<int, string> dict = new Dictionary<int, string>();

        public static void Star1(string input)
        {
            int output = 0;
            string[] lines = File.ReadAllLines(input);

            for (var i = 0; i < lines.Length; i++)
            {
                dict[i] = lines[i];
            }


            for (var i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                for (var j = 0; j < line.Length; j++)
                {
                    if (i == 0 || j == 0 || i == lines.Length - 1 || j == lines.Length - 1)
                    {
                        output++;
                        continue;
                    }

                    int current = line[j] - '0';
                    if (scanLeft(current, j, line) || scanBot(current, i, j, line) || scanRight(current, j, line) || scanTop(current, i ,j, line))
                    {
                        output++;
                    }
                }
            }

            Console.WriteLine(output);
        }

        private static bool scanLeft(int current, int j, string line)
        {
            for (int left = j - 1; left >= 0; left--)
            {
                int number = line[left] - '0';
                if (number >= current)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool scanTop(int current, int i, int j, string line)
        {
            for (int left = i - 1; left >= 0; left--)
            {
                int number = dict[left][j] - '0';
                if (number >= current)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool scanRight(int current, int j, string line)
        {
            for (int left = j + 1; left < line.Length; left++)
            {
                int number = line[left] - '0';
                if (number >= current)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool scanBot(int current, int i, int j, string line)
        {
            for (int left = i + 1; left < line.Length; left++)
            {
                int number = dict[left][j] - '0';
                if (number >= current)
                {
                    return false;
                }
            }

            return true;
        }

        private static int scanLeft2(int current, int j, string line)
        {
            int count = 0;
            for (int left = j - 1; left >= 0; left--)
            {
                int number = line[left] - '0';
                if (number >= current)
                {
                    return count+1;

                }

                count++;
            }

            return count;

        }

        private static int scanTop2(int current, int i, int j, string line)
        {
            int count = 0;

            for (int left = i - 1; left >= 0; left--)
            {
                int number = dict[left][j] - '0';
                if (number >= current)
                {
                    return count + 1;

                }

                count++;
            }

            return count;
        }

        private static int scanRight2(int current, int j, string line)
        {
            int count = 0;

            for (int left = j + 1; left < line.Length; left++)
            {
                int number = line[left] - '0';
                if (number >= current)
                {
                    return count +1;

                }

                count++;
            }

            return count;

        }

        private static int scanBot2(int current, int i, int j, string line)
        {
            int count = 0;

            for (int left = i + 1; left < line.Length; left++)
            {
                int number = dict[left][j] - '0';
                if (number >= current)
                {
                    return count + 1;

                }

                count++;
            }

            return count;

        }



        public static void Star2(string input)
        {
            int output = 0;
            string[] lines = File.ReadAllLines(input);

            for (var i = 0; i < lines.Length; i++)
            {
                dict[i] = lines[i];
            }

            int max = 0;

            for (var i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                for (var j = 0; j < line.Length; j++)
                {
                    if (i == 0 || j == 0 || i == lines.Length - 1 || j == lines.Length - 1)
                    {
                        output++;
                        continue;
                    }

                    int current = line[j] - '0';
                    int count = 0;
                    count = scanLeft2(current, j, line) * scanBot2(current, i, j, line) *
                             scanRight2(current, j, line) * scanTop2(current, i, j, line);

                    max = Math.Max(max, count);
                }
            }

            Console.WriteLine(max);
        }
    }
}