using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day20
    {
        private List<string> circle = new List<string>();
        private HashSet<string> set = new HashSet<string>();
        private int id;
        public void Star1(string input)
        {
            int output = 0;
            string[] lines = File.ReadAllLines(input);
            foreach (string line in lines)
            {
                string actualValue = line;
                if (set.Contains(actualValue))
                {
                    actualValue += $"%{id++}";
                }

                circle.Add(actualValue);
                set.Add(actualValue);
            }

            List<string> originalOrder = new List<string>(circle);

            for (int i = 0; i < originalOrder.Count; i++)
            {
                string numberToMove = originalOrder[i];
                int index = circle.IndexOf(numberToMove);

                int value = int.Parse(circle[index].Split('%')[0]);
                if (value == 0)
                {
                    continue;
                }

                for (int j = 0; j < Math.Abs(value); j++)
                {
                    if (value < 0)
                    {
                        index = swapBack(index);
                    } else
                    {
                        index = swapForward(index);
                    }
                }
            }

            int outputA = (1000 + circle.IndexOf("0")) % circle.Count;
            int outputB = (2000 + circle.IndexOf("0")) % circle.Count;
            int outputC = (3000 + circle.IndexOf("0")) % circle.Count;
            int a = int.Parse(circle[outputA].Split('%')[0]);
            int b = int.Parse(circle[outputB].Split('%')[0]);
            int c = int.Parse(circle[outputC].Split('%')[0]);

            Console.WriteLine(a + b + c );
        }

        private int swapForward(int index)
        {
            if (index == circle.Count - 1)
            {
                swap(index, 0);
                return 0;
            }
            else
            {
                swap(index, index + 1);
                return index + 1;
            }
        }

        private int swapBack(int index)
        {
            if (index == 0)
            {
                swap(0, circle.Count-1);
                return circle.Count - 1;
            }
            else
            {
                swap(index, index - 1);
                return index - 1;
            }
        }

        private void swap(int indexA, int indexB)
        {
            string temp = circle[indexA];
            circle[indexA] = circle[indexB];
            circle[indexB] = temp;
        }

        public void Star2(string input)
        {
            int output = 0;
            long decryptionKey = 811_589_153;
            string[] lines = File.ReadAllLines(input);
            foreach (string line in lines)
            {
                long temp = long.Parse(line) * decryptionKey;
                string actualValue = temp.ToString();
                if (set.Contains(actualValue))
                {
                    actualValue += $"%{id++}";
                }

                circle.Add(actualValue);
                set.Add(actualValue);
            }

            List<string> originalOrder = new List<string>(circle);

            for (int iter = 0; iter < 10; iter++)
            {
                for (int i = 0; i < originalOrder.Count; i++)
                {
                    string numberToMove = originalOrder[i];
                    int index = circle.IndexOf(numberToMove);

                    long value = long.Parse(circle[index].Split('%')[0]);
                    if (value == 0)
                    {
                        continue;
                    }

                    //while (value < circle.Count)
                    //{
                    //    value += circle.Count;
                    //}

                    //while (value > circle.Count)
                    //{
                    //    value -= circle.Count;
                    //}

                    value %= (circle.Count-1);

                    for (int j = 0; j < Math.Abs(value); j++)
                    {
                        if (value < 0)
                        {
                            index = swapBack(index);
                        }
                        else
                        {
                            index = swapForward(index);
                        }
                    }
                }
            }

            int outputA = (1000 + circle.IndexOf("0")) % circle.Count;
            int outputB = (2000 + circle.IndexOf("0")) % circle.Count;
            int outputC = (3000 + circle.IndexOf("0")) % circle.Count;
            long a = long.Parse(circle[outputA].Split('%')[0]);
            long b = long.Parse(circle[outputB].Split('%')[0]);
            long c = long.Parse(circle[outputC].Split('%')[0]);

            Console.WriteLine(a + b + c);
        }
    }
}