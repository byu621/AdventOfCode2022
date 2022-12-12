using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day12
    {
        //A -> -14
        //E -> -28
        private int[,] array;
        private int[,] real;
        private int width = 0;
        private int height = 0;

        private Queue<string> queue = new Queue<string>();

        public void Star1(string input)
        {
            int startI = 0;
            int startJ = 0;

            string[] lines = File.ReadAllLines(input);
            height = lines.Length;
            width = lines[0].Length;

            array = new int[height, width];
            real = new int[height, width];
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                width = line.Length;
                for (int j = 0; j < line.Length; j++)
                {
                    array[i, j] = line[j] - 'a';

                    if (array[i, j] == -28)
                    {

                        real[i, j] = 0;
                    }
                    else
                    {
                        real[i, j] = int.MaxValue;
                    }

                    if (array[i, j] == -14)
                    {
                        startI = i;
                        startJ = j;
                        array[i, j] = 0;
                    } else if (array[i,j] == -28)
                    {
                        array[i, j] = 25;
                    }
                }
            }

            for (int iteration = 0; iteration < 2000; iteration++)
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        recurse(i, j);
                    }
                }
            }

            Console.WriteLine(real[startI,startJ]);
        }

        private void recurse(int i, int j)
        {
            int current = real[i, j];
            if (current == int.MaxValue)
            {
                return;
            }

            if (i > 0 && array[i-1,j] >= (array[i,j] - 1))
            {
                real[i - 1, j] = Math.Min(current + 1, real[i-1,j]);
            }
            if (j > 0 && array[i, j - 1] >= (array[i, j] - 1))
            {
                real[i, j - 1] = Math.Min(current + 1, real[i, j-1]);
            }
            if (j < width - 1 && array[i, j + 1] >= (array[i, j] - 1))
            {
                real[i, j + 1] = Math.Min(current + 1, real[i, j+1]);
            }
            if (i < height - 1 && array[i + 1, j] >= (array[i, j] - 1))
            {
                real[i + 1, j] = Math.Min(current + 1, real[i+1, j]);
            }

        }

        public void Star2(string input)
        {

        }
    }
}