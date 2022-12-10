using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day9
    {

        private static Dictionary<char, string> dict = new Dictionary<char, string>();
        private static HashSet<string> set = new HashSet<string>();

        public static void Star1(string input)
        {
            dict['h'] = "0%0";
            dict['1'] = "0%0";
            dict['2'] = "0%0";
            dict['3'] = "0%0";
            dict['4'] = "0%0";
            dict['5'] = "0%0";
            dict['6'] = "0%0";
            dict['7'] = "0%0";
            dict['8'] = "0%0";
            dict['9'] = "0%0";
            set.Add($"0%0");

            string[] lines = File.ReadAllLines(input);
            foreach (string line in lines)
            {
                var asdf = line.Split(' ');
                char direction = asdf[0][0];
                string amount = asdf[1];

                for (int i = 0; i < int.Parse(amount); i++)
                {
                    int hRow = int.Parse(dict['h'].Split('%')[0]);
                    int hCol = int.Parse(dict['h'].Split('%')[1]);
                    if (direction == 'L')
                    {
                        hCol--;
                    } else if (direction == 'R')
                    {
                        hCol++;
                    } else if (direction == 'U')
                    {
                        hRow--;
                    } else if (direction == 'D')
                    {
                        hRow++;
                    }

                    dict['h'] = $"{hRow}%{hCol}";
                    Update('h','1');

                    for (char c = '2'; c <= '9'; c++)
                    {
                        Update((char)(c-1) , c);
                    }

                    set.Add(dict['9']);
                }
            }

            Console.WriteLine(set.Count);
        }

        private static void Update(char predecessor, char current)
        {
            int pRow = int.Parse(dict[predecessor].Split('%')[0]);
            int pCol = int.Parse(dict[predecessor].Split('%')[1]);
            int cRow = int.Parse(dict[current].Split('%')[0]);
            int cCol = int.Parse(dict[current].Split('%')[1]);

            int rowDifference = Math.Abs(pRow - cRow);
            int colDifference = Math.Abs(pCol - cCol);
            if (rowDifference > 1 || colDifference > 1)
            {
                if (cRow != pRow)
                {
                    cRow += cRow > pRow ? -1 : 1;
                }
                if (cCol != pCol)
                {
                    cCol += cCol > pCol ? -1 : 1;
                }

                dict[current] = $"{cRow}%{cCol}";
            }
        }

        public static void Star2(string input)
        {
            int output = 0;
            string[] lines = File.ReadAllLines(input);


            Console.WriteLine(output);
        }
    }
}