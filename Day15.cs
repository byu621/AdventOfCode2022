using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day15
    {
        private HashSet<int> seen = new HashSet<int>();
        private HashSet<int> sensorOnRow = new HashSet<int>();
        private HashSet<int> beaconOnRow = new HashSet<int>();

        public void Star1(string input, int row)
        {
            string[] lines = File.ReadAllLines(input);
            foreach (string line in lines)
            {
                string[] split = line.Split(new string[] { "Sensor at ", ": closest beacon is at ", ", ", "x=", "y=" }, StringSplitOptions.RemoveEmptyEntries);

                int sensorX = int.Parse(split[0]);
                int sensorY = int.Parse(split[1]);
                int beaconX = int.Parse(split[2]);
                int beaconY = int.Parse(split[3]);

                if (sensorY == row)
                {
                    sensorOnRow.Add(sensorX);
                } 

                if (beaconY == row)
                {
                    beaconOnRow.Add(beaconX);
                }

                int distance = Math.Abs(sensorX - beaconX) + Math.Abs(sensorY - beaconY);

                if (Math.Abs(sensorY - row) > distance)
                {
                    continue;
                }

                int left = sensorX - (distance - Math.Abs(sensorY - row));
                int right = sensorX + (distance - Math.Abs(sensorY - row));
                for (int i = left; i <= right; i++)
                {
                    if (sensorOnRow.Contains(i) || beaconOnRow.Contains(i))
                    {
                        continue;
                    }

                    seen.Add(i);
                }
            }

            Console.WriteLine(seen.Count);
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