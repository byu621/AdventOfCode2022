using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{

    class Director
    {
        public Director(int _size, string _name)
        {
            name = _name;
        }
        public List<Director> sub = new List<Director>();
        public int size;
        private string name;

        public void addSub(string newSub)
        {
            sub.Add(new Director(0, newSub));
        }

        public Director GetDirector(string subName)
        {
            foreach (var child in sub)
            {
                if (child.name == subName)
                {
                    return child;
                }
            }

            throw new Exception();
        }
    }

    class Day7
    {


        public static void Star1()
        {
            int output = 0;
            Director root = new Director(0, "/");
            Director currentdirectory = root;
            Stack<Director> stack = new Stack<Director>();
            stack.Push(root);
            string[] lines = File.ReadAllLines($"{Environment.CurrentDirectory}/Input/day7.txt");
            for (var i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line == "$ ls")
                {
                    i++;
                    line = lines[i];
                    while (!line.StartsWith("$"))
                    {
                        if (line.StartsWith("dir"))
                        {
                            currentdirectory.addSub(line.Split(' ')[1]);
                        }
                        else
                        {
                            currentdirectory.size += int.Parse(line.Split(' ')[0]);
                        }

                        i++;
                        if (i == lines.Length)
                        {
                            break;
                        }
                        line = lines[i];
                    }

                    i--;

                } else if (line.StartsWith("$ cd"))
                {
                    if (line == "$ cd ..")
                    {
                        stack.Pop();
                        currentdirectory = stack.Peek();
                    } else if (line == "$ cd /")
                    {
                    }else
                    {
                        stack.Push(currentdirectory.GetDirector(line.Split(' ')[2]));
                        currentdirectory = stack.Peek();
                    }
                }
            }

            recurse(root);
            int asdf = recurse2(root, 0);
            Console.WriteLine(recurse2(root,0));
        }

        private static int recurse(Director currentDirectory)
        {
            if (currentDirectory.sub.Count == 0)
            {
                return currentDirectory.size;
            }

            foreach (Director child in currentDirectory.sub)
            {
                currentDirectory.size += recurse(child);
            }

            return currentDirectory.size;
        }

        private static int recurse2(Director currentDirectory, int current)
        {
            if (currentDirectory.size < 100000)
            {
                current += currentDirectory.size;
            }

            foreach (Director child in currentDirectory.sub)
            {
                current = recurse2(child, current);
            }

            return current;
        }

        private static int smallestDirectory = 100000000;

        private static void recurse3(Director currentDirectory, int current, int minimum)
        {
            if (currentDirectory.size > minimum)
            {
                smallestDirectory = Math.Min(smallestDirectory, currentDirectory.size);
            }

            foreach (Director child in currentDirectory.sub)
            {
                recurse3(child, current, minimum);
            }
        }

        public static void Star2()
        {
            int output = 0;
            Director root = new Director(0, "/");
            Director currentdirectory = root;
            Stack<Director> stack = new Stack<Director>();
            stack.Push(root);
            string[] lines = File.ReadAllLines($"{Environment.CurrentDirectory}/Input/day7.txt");
            for (var i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line == "$ ls")
                {
                    i++;
                    line = lines[i];
                    while (!line.StartsWith("$"))
                    {
                        if (line.StartsWith("dir"))
                        {
                            currentdirectory.addSub(line.Split(' ')[1]);
                        }
                        else
                        {
                            currentdirectory.size += int.Parse(line.Split(' ')[0]);
                        }

                        i++;
                        if (i == lines.Length)
                        {
                            break;
                        }
                        line = lines[i];
                    }

                    i--;

                }
                else if (line.StartsWith("$ cd"))
                {
                    if (line == "$ cd ..")
                    {
                        stack.Pop();
                        currentdirectory = stack.Peek();
                    }
                    else if (line == "$ cd /")
                    {
                    }
                    else
                    {
                        stack.Push(currentdirectory.GetDirector(line.Split(' ')[2]));
                        currentdirectory = stack.Peek();
                    }
                }
            }

            recurse(root);
            int asdf = recurse2(root, 0);

            int freeSpace = 70000000 - root.size;
            int minimum = 30000000 - freeSpace;


            Console.WriteLine(recurse2(root, 0));
            recurse3(root, 0, minimum);
            Console.WriteLine(smallestDirectory);
        }
    }
}