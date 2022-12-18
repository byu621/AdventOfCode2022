using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{


    class Day16
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

        private List<Valve> valves = new List<Valve>();
        private Dictionary<string, Valve> dict = new Dictionary<string, Valve>();

        public void Star1(string input)
        {
            int output = 0;
            Valve startingValve = null;
            string[] lines = File.ReadAllLines(input);
            foreach (string line in lines)
            {
                string[] split = line.Split(new string[] { "Valve ", " has flow rate=", "; tunnels lead to valves ", ", ", "; tunnel leads to valve " }, StringSplitOptions.RemoveEmptyEntries);
                List<string> tunnels = new List<string>();
                for (int i = 2; i < split.Length; i++)
                {
                    tunnels.Add(split[i]);
                }

                valves.Add(new Valve(split[0], int.Parse(split[1]), tunnels));

                if (split[0] == "AA")
                {
                    startingValve = valves.Last();
                }
                maxFlowRate += valves.Last().flowRate;

                if (valves.Last().flowRate != 0)
                {
                    maxValvesOpen++;
                    valvesWithFlow.Add(valves.Last());
                }
            }

            foreach (var valve in valves)
            {
                dict.Add(valve.name, valve);
            }

            int max = Recurse(startingValve, new List<Valve>(), 0, 30, 0);
            Console.WriteLine(maxSoFar);
        }

        private int maxSoFar = 0;
        private int maxFlowRate = 0;
        private int maxValvesOpen = 0;

        private List<Valve> valvesWithFlow = new List<Valve>();

        private int Recurse(Valve currentValve, List<Valve> openValves, int flowRate, int time, int pressure)
        {
            int theoreticalMax = pressure + flowRate;
            int currentFlowRate = flowRate;
            List<int> unusedFlowRates = valvesWithFlow.Where(v => !openValves.Contains(v)).ToList().Select(a => a.flowRate).ToList();
            unusedFlowRates.Sort((a, b) => { return a.CompareTo(b) * -1; });
            for (int i = 0; i < time-1; i++)
            {
                if (i % 2 == 0)
                {
                    int newFlowRate = i / 2 < unusedFlowRates.Count ? unusedFlowRates[i / 2] : 0;
                    currentFlowRate += newFlowRate;
                }

                theoreticalMax += currentFlowRate;
            }

            if (theoreticalMax <= maxSoFar)
            {
                return 0;
            }

            if (time == 0)
            {
                return pressure;
            }

            if (pressure + time * flowRate > maxSoFar)
            {
                Console.WriteLine(pressure + time * flowRate);
                maxSoFar = pressure + time * flowRate;
            }

            //release pressure
            foreach (var valve in openValves)
            {
                pressure += valve.flowRate;
            }



            int maxAction = 0;
            //Actions
            if (!openValves.Contains(currentValve) && currentValve.flowRate > 0)
            {
                openValves.Add(currentValve);
                maxAction = Math.Max(maxAction, Recurse(currentValve, openValves, flowRate + currentValve.flowRate, time - 1, pressure));
                openValves.Remove(currentValve);
            }

            foreach (var tunnel in currentValve.tunnels)
            {
                maxAction = Math.Max(maxAction, Recurse(dict[tunnel], openValves, flowRate, time - 1, pressure));
            }

            maxAction = Math.Max(maxAction, Recurse(currentValve, openValves, flowRate, time - 1, pressure));
            return maxAction;
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