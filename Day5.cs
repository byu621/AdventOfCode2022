using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day5
    {
        private static Stack<char>[] crates = new Stack<char>[9];
        private static List<int> positions = new List<int>() { 1, 5, 9, 13, 17, 21, 25, 29, 33 };

        public static void Star1()
        {
            for (int i = 0; i < 9; i++)
            {
                crates[i] = new Stack<char>();
            }

            string[] lines = File.ReadAllLines($"{Environment.CurrentDirectory}/Input/day5.txt");
            int output = 0;
            for (int i = 0; i < 8; i++)
            {
                string line = lines[i];
                for (int i1 = 0; i1 < positions.Count; i1++)
                {
                    int position = positions[i1];
                    if (line[position] != ' ' && line[position] != ',' && line[position] != 32)
                    {
                        crates[i1].Push(line[position]);
                    }
                }
            }

            for (int i = 0; i < 9; i++)
            {
                Stack<char> rev = new Stack<char>();
                while (crates[i].Count != 0)
                {
                    rev.Push(crates[i].Pop());
                }
                crates[i] = rev;
            }

            for (int i = 10; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] asdf = line.Split(' ');
                int moveAmount = int.Parse(asdf[1]);
                int fromStack = int.Parse(asdf[3]) - 1;
                int toStack = int.Parse(asdf[5]) - 1;

                for (int j = 0; j < moveAmount; j++)
                {
                    char move = crates[fromStack].Pop();
                    crates[toStack].Push(move);
                }
            }

            string real = "";
            for (int i = 0; i < 9; i++)
            {
                real += crates[i].Peek();
            }
            Console.WriteLine(real);
        }

        public static void Star2()
        {
            for (int i = 0; i < 9; i++)
            {
                crates[i] = new Stack<char>();
            }

            string[] lines = File.ReadAllLines($"{Environment.CurrentDirectory}/Input/day5.txt");
            for (int i = 0; i < 8; i++)
            {
                string line = lines[i];
                for (int i1 = 0; i1 < positions.Count; i1++)
                {
                    int position = positions[i1];
                    if (line[position] != ' ' && line[position] != ',' && line[position] != 32)
                    {
                        crates[i1].Push(line[position]);
                    }
                }
            }

            for (int i = 0; i < 9; i++)
            {
                Stack<char> rev = new Stack<char>();
                while (crates[i].Count != 0)
                {
                    rev.Push(crates[i].Pop());
                }
                crates[i] = rev;
            }

            for (int i = 10; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] asdf = line.Split(' ');
                int moveAmount = int.Parse(asdf[1]);
                int fromStack = int.Parse(asdf[3]) - 1;
                int toStack = int.Parse(asdf[5]) - 1;

                Stack<char> temp = new Stack<char>();
                for (int j = 0; j < moveAmount; j++)
                {
                    temp.Push(crates[fromStack].Pop());
                }

                for (int j = 0; j < moveAmount; j++)
                {
                    crates[toStack].Push(temp.Pop());
                }
            }

            string real = "";
            for (int i = 0; i < 9; i++)
            {
                real += crates[i].Peek();
            }
            Console.WriteLine(real);
        }
    }
}
