using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2022
{
    class Day16Star2
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
        private int maxSoFar = 0;
        private List<Valve> valvesWithFlow = new List<Valve>();
        private Dictionary<string, string> memo = new Dictionary<string, string>();

        private String MemoKey(Valve currentValve, Valve elephantValve, List<Valve> openValves, int time)
        {
            var sb = new StringBuilder();
            sb.Append($"{currentValve.name}%{elephantValve.name}%");
            foreach (var open in openValves)
            {
                sb.Append($"{open.name}%");
            }
            return sb.ToString();
        }

        private int Recurse(Valve currentValve, Valve elephantValve, List<Valve> openValves, int flowRate, int time, int pressure, bool elephantToMove)
        {
            if (openValves.Count == valvesWithFlow.Count)
            {
                Console.WriteLine("all open");
            }

            //release pressure
            if (!elephantToMove)
            {
                string memoKey = MemoKey(currentValve, elephantValve, openValves, time);
                if (memo.ContainsKey(memoKey))
                {
                    int memoTime = int.Parse(memo[memoKey].Split('%')[0]);
                    int memoPressure = int.Parse(memo[memoKey].Split('%')[1]);

                    if (memoTime >= time && (memoTime-time) * flowRate + memoPressure >= pressure)
                    {
                        return 0;
                    }
                    else if (time >= memoTime && (time-memoTime)* flowRate + pressure >= memoPressure)
                    {
                        memo[memoKey] = $"{time}%{pressure}";
                    }
                } else
                {
                    memo.Add(memoKey, $"{time}%{pressure}");
                }


                int theoreticalMax = pressure + flowRate;
                int currentFlowRate = flowRate;
                List<int> unusedFlowRates = valvesWithFlow.Where(v => !openValves.Contains(v)).ToList().Select(a => a.flowRate).ToList();
                for (int i = 0; i < time - 1; i++)
                {
                    if (i % 2 == 0)
                    {
                        int newFlowRate = i < unusedFlowRates.Count ? unusedFlowRates[i] : 0;
                        currentFlowRate += newFlowRate;
                        newFlowRate = i + 1 < unusedFlowRates.Count ? unusedFlowRates[i + 1] : 0;
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
                    maxSoFar = pressure + time * flowRate;
                    Console.WriteLine(maxSoFar);
                }

                foreach (var valve in openValves)
                {
                    pressure += valve.flowRate;
                }
            }


            int maxAction = 0;
            if (elephantToMove)
            {
                if (!openValves.Contains(elephantValve) && elephantValve.flowRate > 0)
                {
                    openValves.Add(elephantValve);
                    maxAction = Math.Max(maxAction, Recurse(currentValve, elephantValve, openValves, flowRate + elephantValve.flowRate, time - 1, pressure, false));
                    openValves.Remove(elephantValve);
                }

                foreach (var tunnel in elephantValve.tunnels)
                {
                    maxAction = Math.Max(maxAction, Recurse(currentValve, dict[tunnel], openValves, flowRate, time - 1, pressure, false));
                }

                return maxAction;
            }  

            if (!openValves.Contains(currentValve) && currentValve.flowRate > 0)
            {
                openValves.Add(currentValve);
                maxAction = Math.Max(maxAction, Recurse(currentValve, elephantValve, openValves, flowRate + currentValve.flowRate, time, pressure, true));
                openValves.Remove(currentValve);
            }

            foreach (var tunnel in currentValve.tunnels)
            {
                maxAction = Math.Max(maxAction, Recurse(dict[tunnel], elephantValve, openValves, flowRate, time, pressure, true));
            }

            return maxAction;
        }

        public void Star2(string input)
        {
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

                if (valves.Last().flowRate != 0)
                {
                    valvesWithFlow.Add(valves.Last());
                }
            }

            valvesWithFlow.Sort((a, b) =>
            {
                return a.flowRate.CompareTo(b.flowRate) * -1;
            });

            foreach (var valve in valves)
            {
                dict.Add(valve.name, valve);
            }

            Recurse(startingValve, startingValve, new List<Valve>(), 0, 26, 0, false);
            Console.WriteLine(maxSoFar);
        }
    }
}