using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day2
    {
        private static Dictionary<char, int> dict1 = new Dictionary<char, int>()
        {
            {'X', 1}, {'Y', 2}, {'Z', 3}
        };

        private static Dictionary<char, char> translation = new Dictionary<char, char>()
        {
            {'A', 'X'}, {'B', 'Y'}, {'C', 'Z'}
        };

        private static Dictionary<char, char> beat = new Dictionary<char, char>()
        {
            {'X', 'Z'}, {'Y', 'X'}, {'Z', 'Y'}
        };

        private static Dictionary<char, char> draw = new Dictionary<char, char>()
        {
            {'X', 'X'}, {'Y', 'Y'}, {'Z', 'Z'}
        };

        private static Dictionary<char, char> lose = new Dictionary<char, char>()
        {
            {'X', 'Y'}, {'Y', 'Z'}, {'Z', 'X'}
        };

        private static Dictionary<char, int> scoreWinDrawLose = new Dictionary<char, int>()
        {
            {'X', 0}, {'Y', 3}, {'Z', 6}
        };

        public static void Calculate()
        {
            string[] lines = File.ReadAllLines($"{Environment.CurrentDirectory}/Input/day2.txt");
            int score = 0;
            foreach (var line in lines)
            {
                char opponent = line[0];
                opponent = translation[opponent];
                char me = line[2];

                int thisScore = dict1[me];

                if (opponent == me)
                {
                    thisScore += 3;
                } else if (beat[me] == opponent)
                {
                    thisScore += 6;
                }

                score += thisScore;

            }
            Console.WriteLine(score);
        }

        public static void Calculate2()
        {
            string[] lines = File.ReadAllLines($"{Environment.CurrentDirectory}/Input/day2.txt");
            int score = 0;
            foreach (var line in lines)
            {
                char opponent = line[0];
                opponent = translation[opponent];
                char me = line[2];

                int thisScore = scoreWinDrawLose[me];

                char myActual = ' ';
                if (me == 'X')
                {
                    myActual = beat[opponent];
                } else if (me == 'Y')
                {
                    myActual = opponent;
                }
                else
                {
                    myActual = lose[opponent];
                }

                thisScore += dict1[myActual];

                score += thisScore;

            }
            Console.WriteLine(score);
        }
    }
}
