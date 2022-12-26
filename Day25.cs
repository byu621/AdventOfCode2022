using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day25
    {
        public void Star1(string input)
        {
            int output = 0;
            long decimalValue = 0;
            string[] lines = File.ReadAllLines(input);
            foreach (string line in lines)
            {
                decimalValue += toDecimal(line);
            }

            Console.WriteLine(decimalValue);

            var snafu = new StringBuilder();
            while (decimalValue > 0)
            {
                long a = decimalValue % 5;
                if (a == 3)
                {
                    snafu.Append('=');
                } else if (a == 4)
                {
                    snafu.Append('-');
                } else
                {
                    snafu.Append(a);
                }

                decimalValue -= charToInt(snafu[snafu.Length - 1]);
                decimalValue /= 5;
            }

            Console.WriteLine(new string(snafu.ToString().Reverse().ToArray()));
            Console.WriteLine(toDecimal(new string(snafu.ToString().Reverse().ToArray())));
        }

        private long toDecimal(string line)
        {
            long decimalValue = 0;
            long multiplier = 1;
            for (int i = line.Length - 1; i >= 0; i--)
            {
                char c = line[i];
                int value = charToInt(c);
                decimalValue += value * multiplier;
                multiplier *= 5;
            }

            return decimalValue;
        }

        private int charToInt(char c)
        {
            if (c == '-')
            {
                return -1;
            }

            if (c == '=')
            {
                return -2;
            }

            return c - '0';
        }

        public void Star2(string input)
        {
            int output = 0;
            string[] lines = File.ReadAllLines(input);
            foreach (string line in lines)
            {

            }

            Console.WriteLine(output);
        }
    }
}