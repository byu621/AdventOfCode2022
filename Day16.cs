using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Valve
    {
        public string name;
        public int flowRate;
        public List<string> tunnels;

        public Valve(string name, int flowRate, List<string> tunnels)
        {
            this.name = name;
            this.flowRate = flowRate;
            this.tunnels = tunnels;
        }
    }

    class Day16
    {
        public void Star1(string input)
        {
            int output = 0;
            string[] lines = File.ReadAllLines(input);
            foreach (string line in lines)
            {
                string[] split = line.Split(new string[] { "Valve " });
            }

            Console.WriteLine(output);
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