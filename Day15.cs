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

        private Dictionary<int, HashSet<int>> rows = new Dictionary<int, HashSet<int>>();
        
        public class Record
        {

            public int sensorX;
            public int sensorY;
            public int beaconX;
            public int beaconY;
            public int distance;

            public Record(int sensorX, int sensorY, int beaconX, int beaconY, int distance)
            {
                this.sensorX = sensorX;
                this.sensorY = sensorY;
                this.beaconX = beaconX;
                this.beaconY = beaconY;
                this.distance = distance;
            }
        }

        private List<Record> records = new List<Record>();

        public void Star2(string input, int limit)
        {
            string[] lines = File.ReadAllLines(input);
            foreach (string line in lines)
            {
                string[] split = line.Split(new string[] { "Sensor at ", ": closest beacon is at ", ", ", "x=", "y=" }, StringSplitOptions.RemoveEmptyEntries);

                int sensorX = int.Parse(split[0]);
                int sensorY = int.Parse(split[1]);
                int beaconX = int.Parse(split[2]);
                int beaconY = int.Parse(split[3]);

                records.Add(new Record(sensorX, sensorY, beaconX, beaconY, Math.Abs(sensorX - beaconX) + Math.Abs(sensorY - beaconY)));
            }

            records.Sort((a, b) =>
            {
                return a.sensorX.CompareTo(b.sensorX);
            });


            for (int y = 0; y <= limit; y++)
            {

                records.Sort((a, b) =>
                {
                    int usableDistanceA = a.distance - Math.Abs(a.sensorY - y);
                    int minimumA = usableDistanceA >= 0 ? a.sensorX - usableDistanceA : int.MaxValue;

                    int usableDistanceB = b.distance - Math.Abs(b.sensorY - y);
                    int minimumB = usableDistanceB >= 0 ? b.sensorX - usableDistanceB : int.MaxValue;

                    return minimumA.CompareTo(minimumB);
                });


                int x = 0;
                foreach (var record in records)
                {
                    int usableDistance = record.distance - Math.Abs(record.sensorY - y);
                    if (usableDistance < 0)
                    {
                        continue;
                    }

                    int minimum = record.sensorX - usableDistance;
                    int maximum = record.sensorX + usableDistance;

                    if (minimum > x)
                    {
                        Console.WriteLine(minimum - x);
                        long tuning = 4_000_000L * x + y;
                        Console.WriteLine(tuning);
                        break;
                    }

                    x = Math.Max(x, maximum + 1);
                }

                if (x <= limit)
                {
                    long tuning = 4_000_000 * x + y;
                    Console.WriteLine(tuning);
                }
            }


            Console.WriteLine(1);
        }
    }
}