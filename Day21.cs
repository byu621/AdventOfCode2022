using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day21
    {


        public class Monkey
        {
            public Monkey()
            {

            }

            public Monkey(string name, Monkey monkey1, Monkey monkey2, int number, bool add, bool minus, bool times, bool divide)
            {
                this.name = name;
                this.monkey1 = monkey1;
                this.monkey2 = monkey2;
                this.number = number;
                this.add = add;
                this.minus = minus;
                this.times = times;
                this.divide = divide;
            }

            public string name;
            public Monkey monkey1;
            public Monkey monkey2;
            public int number;
            public bool add;
            public bool minus;
            public bool times;
            public bool divide;
        }

        private Dictionary<string, long> dict = new Dictionary<string, long>();
        private List<Monkey> monkeys = new List<Monkey>();
        private Monkey root1;
        private Monkey root2;
        private Dictionary<string, long> originalDict = new Dictionary<string, long>();
        private Dictionary<string, Monkey> monkeyDict = new Dictionary<string, Monkey>();


        public void Star2(string input)
        {
            int output = 0;
            int secondNumber = 150;
            string[] lines = File.ReadAllLines(input);
            foreach (string line in lines)
            {
                string[] split = line.Split(new string[] { ": ", " " }, StringSplitOptions.RemoveEmptyEntries);
                if (split.Length >= 4)
                {
                    Monkey monkey;
                    if (monkeyDict.ContainsKey(split[0]))
                    {
                        monkey = monkeyDict[split[0]];
                    } else
                    {
                        monkey = new Monkey();
                        monkeyDict.Add(split[0], monkey);
                    }

                    bool add = split[2] == "+";
                    bool minus = split[2] == "-";
                    bool times = split[2] == "*";
                    bool divide = split[2] == "/";

                    if (!monkeyDict.ContainsKey(split[1]))
                    {
                        monkeyDict.Add(split[1], new Monkey());
                    }

                    if (!monkeyDict.ContainsKey(split[3]))
                    {
                        monkeyDict.Add(split[3], new Monkey());
                    }

                    monkey.name = split[0];
                    monkey.monkey1 = monkeyDict[split[1]];
                    monkey.monkey2 = monkeyDict[split[3]];
                    monkey.add = add;
                    monkey.minus = minus;
                    monkey.times = times;
                    monkey.divide = divide;

                    monkeys.Add(monkey);

                    if (split[0] == "root")
                    {
                        root1 = monkey.monkey1;
                        root2 = monkey.monkey2;
                    }
                }
                else
                {
                    Monkey monkey;
                    if (monkeyDict.ContainsKey(split[0]))
                    {
                        monkey = monkeyDict[split[0]];
                    }
                    else
                    {
                        monkey = new Monkey();
                        monkeyDict.Add(split[0], monkey);
                    }

                    monkey.name = split[0];
                    dict.Add(split[0], int.Parse(split[1]));
                }
            }

            foreach (string monkeyName in dict.Keys)
            {
                monkeyDict.Remove(monkeyName);
            }

            int somethingHappened = 1;
            while (somethingHappened == 1)
            {
                somethingHappened = 0;
                foreach (var monkey in monkeys)
                {
                    if (dict.ContainsKey(monkey.name))
                    {
                        continue;
                    }

                    if (dict.ContainsKey(monkey.monkey1.name) && dict.ContainsKey(monkey.monkey2.name))
                    {
                        if (monkey.monkey1.name == "humn" || monkey.monkey2.name == "humn")
                        {
                            continue;
                        }

                        long value;
                        if (monkey.add)
                        {
                            value = dict[monkey.monkey1.name] + dict[monkey.monkey2.name];
                        }
                        else if (monkey.times)
                        {
                            value = dict[monkey.monkey1.name] * dict[monkey.monkey2.name];
                        }
                        else if (monkey.minus)
                        {
                            value = dict[monkey.monkey1.name] - dict[monkey.monkey2.name];
                        }
                        else
                        {
                            value = dict[monkey.monkey1.name] / dict[monkey.monkey2.name];
                        }

                        dict.Add(monkey.name, value);
                        somethingHappened = 1;
                    }
                }
            }

            if (dict.ContainsKey(root2.name))
            {
                dict[root1.name] = dict[root2.name];
            } else
            {
                dict[root2.name] = dict[root1.name];
            }
            dict.Remove("humn");

            somethingHappened = 1;
            while (somethingHappened == 1)
            {
                somethingHappened = 0;
                foreach (var monkey in monkeys)
                {
                    if (!dict.ContainsKey(monkey.name))
                    {
                        continue;
                    }

                    long main = dict[monkey.name];
                    if (dict.ContainsKey(monkey.monkey1.name) && !dict.ContainsKey(monkey.monkey2.name))
                    {
                        long monkey1 = dict[monkey.monkey1.name];
                        long monkey2 = 0;

                        if (monkey.add)
                        {
                            monkey2 = main - monkey1;
                        } else if (monkey.minus)
                        {
                            monkey2 = monkey1 - main;
                        }
                        else if (monkey.times)
                        {
                            monkey2 = main / monkey1;
                        }
                        else if (monkey.divide)
                        {
                            monkey2 = monkey1 / main;
                        }

                        dict.Add(monkey.monkey2.name, monkey2);
                        somethingHappened = 1;
                    } else if (dict.ContainsKey(monkey.monkey2.name) && !dict.ContainsKey(monkey.monkey1.name))
                    {
                        long monkey2 = dict[monkey.monkey2.name];
                        long monkey1 = 0;

                        if (monkey.add)
                        {
                            monkey1 = main - monkey2;
                        }
                        else if (monkey.minus)
                        {
                            monkey1 = monkey2 + main;
                        }
                        else if (monkey.times)
                        {
                            monkey1 = main / monkey2;
                        }
                        else if (monkey.divide)
                        {
                            monkey1 = monkey2 * main;
                        }

                        dict.Add(monkey.monkey1.name, monkey1);
                        somethingHappened = 1;
                    }
                }
            }

            Console.WriteLine("yte");

            //originalDict = new Dictionary<string, long>(dict);
            //dict = new Dictionary<string, long>(originalDict);

            //dict[root1] = secondNumber;

            //while (true)
            //{
            //    foreach (var monkey in monkeys)
            //    {
            //        if (dict.ContainsKey(monkey.name))
            //        {
            //            if (dict.ContainsKey(monkey.monkey1))
            //            {
            //                int value =
            //                if (monkey.add)
            //                {
            //                    value = dict[monkey.monkey1] + dict[monkey.monkey2];
            //                }
            //                else if (monkey.times)
            //                {
            //                    value = dict[monkey.monkey1] * dict[monkey.monkey2];
            //                }
            //                else if (monkey.minus)
            //                {
            //                    value = dict[monkey.monkey1] - dict[monkey.monkey2];
            //                }
            //                else
            //                {
            //                    value = dict[monkey.monkey1] / dict[monkey.monkey2];
            //                }
            //            }
            //        }

            //        int match = 0;
            //        if (dict.ContainsKey(monkey.name))
            //        {
            //            match++;
            //        } else if (dict.ContainsKey(monkey.monkey1))
            //        {
            //            match++;
            //        }
            //        else if (dict.ContainsKey(monkey.monkey2))
            //        {
            //            match++;
            //        }

            //        if (match == 2)
            //        {

            //        }

            //        if (dict.ContainsKey(monkey.name))
            //        {
            //            continue;
            //        }

            //        if (dict.ContainsKey(monkey.monkey1) && dict.ContainsKey(monkey.monkey2))
            //        {


            //            long value;
            //            if (monkey.add)
            //            {
            //                value = dict[monkey.monkey1] + dict[monkey.monkey2];
            //            }
            //            else if (monkey.times)
            //            {
            //                value = dict[monkey.monkey1] * dict[monkey.monkey2];
            //            }
            //            else if (monkey.minus)
            //            {
            //                value = dict[monkey.monkey1] - dict[monkey.monkey2];
            //            }
            //            else
            //            {
            //                value = dict[monkey.monkey1] / dict[monkey.monkey2];
            //            }

            //            dict.Add(monkey.name, value);
            //        }
            //    }

            //    if (dict.ContainsKey(root1) && dict.ContainsKey(root2))
            //    {
            //        Console.WriteLine($"{dict[root1]}, {dict[root2]}");
            //        if (dict[root1] == dict[root2])
            //        {
            //            Console.WriteLine(i);
            //            Console.WriteLine("finish");
            //            break;
            //        }
            //        dict = new Dictionary<string, long>(originalDict);
            //        break;
            //    }


            //}



            Console.WriteLine(output);
        }
    }
}