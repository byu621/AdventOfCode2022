using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day6
    {
        private static Stack<char>[] crates = new Stack<char>[9];
        private static List<int> positions = new List<int>() { 1, 5, 9, 13, 17, 21, 25, 29, 33 };

        public static void Star1()
        {
            int output = 0;
            Queue<char> queue = new Queue<char>();
            string text = File.ReadAllText($"{Environment.CurrentDirectory}/Input/day6.txt");
            queue.Enqueue(text[0]);
            queue.Enqueue(text[1]);
            queue.Enqueue(text[2]);
            for (var i = 3; i < text.Length; i++)
            {
                char a = text[i];
                queue.Enqueue(a);
                HashSet<char> hashSet = new HashSet<char>();
                foreach (var b in queue)
                {
                    hashSet.Add(b);
                }

                if (hashSet.Count == 4)
                {
                    Console.WriteLine(i+1);
                    return;
                }
                queue.Dequeue();
            }

            Console.WriteLine(output);
        }

        public static void Star2()
        {
            int output = 0;
            Queue<char> queue = new Queue<char>();
            string text = File.ReadAllText($"{Environment.CurrentDirectory}/Input/day6.txt");
            queue.Enqueue(text[0]);
            queue.Enqueue(text[1]);
            queue.Enqueue(text[2]);
            queue.Enqueue(text[3]);
            queue.Enqueue(text[4]);
            queue.Enqueue(text[5]);
            queue.Enqueue(text[6]);
            queue.Enqueue(text[7]);
            queue.Enqueue(text[8]);
            queue.Enqueue(text[9]);
            queue.Enqueue(text[10]);
            queue.Enqueue(text[11]);
            queue.Enqueue(text[12]);
            for (var i = 13; i < text.Length; i++)
            {
                char a = text[i];
                queue.Enqueue(a);
                HashSet<char> hashSet = new HashSet<char>();
                foreach (var b in queue)
                {
                    hashSet.Add(b);
                }

                if (hashSet.Count == 14)
                {
                    Console.WriteLine(i + 1);
                    return;
                }
                queue.Dequeue();
            }

            Console.WriteLine(output);
        }
    }
}
