using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Monkey
    {
        public int _number;
        public int _divisible;
        public int _isA;
        public int _isNotA;
        public Queue<long> items = new Queue<long>();
        public int multiply = 1;
        public int add = 0;
        public int count = 0;
        public Monkey(int number)
        {
            _number = number;
        }
    }

    class Day11
    {
        private Dictionary<int, int> dict = new Dictionary<int, int>();
        private Queue<int> queue = new Queue<int>();
        private List<Monkey> monkeys = new List<Monkey>();
        public void Star1(string input)
        {
            long god = 1;
            int output = 0;
            int currentMonkey = -1;
            string[] lines = File.ReadAllLines(input);
            for (int i1 = 0; i1 < lines.Length; i1++)
            {
                string line = lines[i1];
                line = line.Trim();
                if (line == "")
                {

                    continue;
                }

                if (line.StartsWith("Monkey"))
                {
                    int number = line.Split(' ')[1][0] - '0';
                    monkeys.Add(new Monkey(number));
                    currentMonkey = number;
                } else if (line.StartsWith("Starting items"))
                {
                    string[] split = line.Split(' ');
                    for (int i = 2; i < split.Length; i++)
                    {
                        string asdf = split[i];
                        if (asdf.Contains(','))
                        {
                            asdf = asdf.Substring(0, asdf.Length - 1);
                        }
                        monkeys[currentMonkey].items.Enqueue(int.Parse(asdf));
                    }
                } else if (line.StartsWith("Test"))
                {
                    monkeys[currentMonkey]._divisible = int.Parse(line.Split(' ')[3]);
                    god *= monkeys[currentMonkey]._divisible;
                } else if (line.StartsWith("If true:"))
                {
                    monkeys[currentMonkey]._isA = int.Parse(line.Split(' ')[5]);
                } else if (line.StartsWith("If false:"))
                {
                    monkeys[currentMonkey]._isNotA = int.Parse(line.Split(' ')[5]);
                } else if (line.StartsWith("Operation"))
                {
                    if (line.Split(' ')[4] == "*")
                    {
                        string asdf = line.Split(' ')[5];
                        if (asdf == "old")
                        {
                            monkeys[currentMonkey].multiply = -1;
                        } else
                        {
                            monkeys[currentMonkey].multiply = int.Parse(line.Split(' ')[5]);
                        }
                    } else
                    {
                        monkeys[currentMonkey].add = int.Parse(line.Split(' ')[5]); 
                    }
                }
            }

            for (int round = 1; round <= 20; round++)
            {
                foreach (Monkey monkey in monkeys)
                {
                    while (monkey.items.Count > 0)
                    {
                        long item = monkey.items.Dequeue();
                        monkey.count++;
                        if (monkey.multiply == -1)
                        {
                            item *= item;
                        } else
                        {
                            item *= monkey.multiply;
                        }

                        item += monkey.add;

                        item /= 3;
                        if (item % monkey._divisible == 0)
                        {
                            monkeys[monkey._isA].items.Enqueue(item);
                        } else
                        {
                            monkeys[monkey._isNotA].items.Enqueue(item);
                        }
                    }
                }
            }

            List<int> max = new List<int>();
            foreach (Monkey monkey in monkeys)
            {
                max.Add(monkey.count);
            }
            max.Sort();

            Console.WriteLine(max[max.Count - 1] * max[max.Count - 2]);
        }

        public void Star2(string input)
        {
            long god = 1;
            int output = 0;
            int currentMonkey = -1;
            string[] lines = File.ReadAllLines(input);
            for (int i1 = 0; i1 < lines.Length; i1++)
            {
                string line = lines[i1];
                line = line.Trim();
                if (line == "")
                {

                    continue;
                }

                if (line.StartsWith("Monkey"))
                {
                    int number = line.Split(' ')[1][0] - '0';
                    monkeys.Add(new Monkey(number));
                    currentMonkey = number;
                }
                else if (line.StartsWith("Starting items"))
                {
                    string[] split = line.Split(' ');
                    for (int i = 2; i < split.Length; i++)
                    {
                        string asdf = split[i];
                        if (asdf.Contains(','))
                        {
                            asdf = asdf.Substring(0, asdf.Length - 1);
                        }
                        monkeys[currentMonkey].items.Enqueue(int.Parse(asdf));
                    }
                }
                else if (line.StartsWith("Test"))
                {
                    monkeys[currentMonkey]._divisible = int.Parse(line.Split(' ')[3]);
                    god *= monkeys[currentMonkey]._divisible;
                }
                else if (line.StartsWith("If true:"))
                {
                    monkeys[currentMonkey]._isA = int.Parse(line.Split(' ')[5]);
                }
                else if (line.StartsWith("If false:"))
                {
                    monkeys[currentMonkey]._isNotA = int.Parse(line.Split(' ')[5]);
                }
                else if (line.StartsWith("Operation"))
                {
                    if (line.Split(' ')[4] == "*")
                    {
                        string asdf = line.Split(' ')[5];
                        if (asdf == "old")
                        {
                            monkeys[currentMonkey].multiply = -1;
                        }
                        else
                        {
                            monkeys[currentMonkey].multiply = int.Parse(line.Split(' ')[5]);
                        }
                    }
                    else
                    {
                        monkeys[currentMonkey].add = int.Parse(line.Split(' ')[5]);
                    }
                }
            }

            for (int round = 1; round <= 10000; round++)
            {
                foreach (Monkey monkey in monkeys)
                {
                    while (monkey.items.Count > 0)
                    {
                        long item = monkey.items.Dequeue();

                        monkey.count++;

                        item %= (god);
                        //item += (23 * 19 * 13 * 17);

                        if (monkey.multiply == -1)
                        {
                            item *= item;
                        }
                        else
                        {
                            item *= monkey.multiply;
                        }

                        item += monkey.add;
                        item %= (god);

                        if (item % monkey._divisible == 0)
                        {
                            monkeys[monkey._isA].items.Enqueue(item);
                        }
                        else
                        {
                            monkeys[monkey._isNotA].items.Enqueue(item);
                        }
                    }
                }
            }

            List<long> max = new List<long>();
            foreach (Monkey monkey in monkeys)
            {
                max.Add(monkey.count);
            }
            max.Sort();

            long real = max[max.Count - 1] * max[max.Count - 2];
            Console.WriteLine(real);
        }
    }
}