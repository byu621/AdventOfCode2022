using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day19
    {
        class Blueprint
        {

            public int oreRobot;
            public int clayRobot;
            public int obRobotOre;
            public int obRobotClay;
            public int geoRobotOre;
            public int geoRobotObi;
            public Blueprint(int oreRobot, int clayRobot, int obRobotOre, int obRobotClay, int geoRobotOre, int geoRobotObi)
            {
                this.oreRobot = oreRobot;
                this.clayRobot = clayRobot;
                this.obRobotOre = obRobotOre;
                this.obRobotClay = obRobotClay;
                this.geoRobotOre = geoRobotOre;
                this.geoRobotObi = geoRobotObi;
            }
        }

        private List<Blueprint> blueprints = new List<Blueprint>();
        public void Star1(string input)
        {
            int output = 1;
            string[] lines = File.ReadAllLines(input);
            foreach (string line in lines)
            {
                string[] split = line.Split(new string[] { "Each ore robot costs ", " ore. Each clay robot costs ", " ore. Each obsidian robot costs ", " ore and ", " clay. Each geode robot costs ", " ore and ", " obsidian." }, StringSplitOptions.RemoveEmptyEntries);
                var bluePrint = new Blueprint(int.Parse(split[1]), int.Parse(split[2]), int.Parse(split[3]), int.Parse(split[4]), int.Parse(split[5]), int.Parse(split[6]));
                blueprints.Add(bluePrint);
            }

            for (int i = 0; i < blueprints.Count; i++)
            {
                Blueprint blueprint = blueprints[i];
                Recurse(blueprint, 0, 0, 0, 0, 1, 0, 0, 0, 32);
                Console.WriteLine($"max:{maxSoFar}");
                output *= maxSoFar;
                maxSoFar = 0;
                memo.Clear();
            }

            Console.WriteLine($"output: {output}");
        }

        private int maxSoFar = 0;
        private Dictionary<string, int> memo = new Dictionary<string, int>();

        private void Recurse(Blueprint blueprint, int ore, int clay, int obi, int geode, int oreRobot, int clayRobot, int obiRobot, int geodeRobot, int time)
        {
            string memoKey = $"{ore}%{clay}%{obi}%{geode}%{oreRobot}%{clayRobot}%{obiRobot}%{geodeRobot}";
            if (memo.ContainsKey(memoKey))
            {
                int memoTime = memo[memoKey];
                if (memoTime >= time)
                {
                    return;
                } else
                {
                    memo[memoKey] = time;
                }
            } else
            {
                memo.Add(memoKey, time);
            }

            int theoreticalMax = geode + time * geodeRobot;

            int theoObi = obi;
            int theoObiRobot = obiRobot;
            for (int i = time; i >= 0; i-=1)
            {

                if (theoObi >= blueprint.geoRobotObi)
                {
                    theoreticalMax += i;
                } else
                {
                    theoObiRobot += 1;
                }
                theoObi += theoObiRobot;
            }

            if (theoreticalMax < maxSoFar)
            {
                return;
            }

            if (geode + time * geodeRobot > maxSoFar)
            {
                maxSoFar = geode + time * geodeRobot;
                Console.WriteLine(maxSoFar);
            }

            if (time == 0)
            {
                return;
            }


            if (ore >= blueprint.geoRobotOre && obi >= blueprint.geoRobotObi)
            {
                Recurse(blueprint, ore - blueprint.geoRobotOre + oreRobot, clay + clayRobot, obi - blueprint.geoRobotObi + obiRobot, geode + geodeRobot, oreRobot, clayRobot, obiRobot, geodeRobot + 1, time - 1);
                return;
            }

            if (ore >= blueprint.oreRobot)
            {
                Recurse(blueprint, ore - blueprint.oreRobot + oreRobot, clay + clayRobot, obi + obiRobot, geode + geodeRobot, oreRobot + 1, clayRobot, obiRobot, geodeRobot, time - 1);
            }

            if (ore >= blueprint.clayRobot)
            {
                Recurse(blueprint, ore - blueprint.clayRobot + oreRobot, clay + clayRobot, obi + obiRobot, geode + geodeRobot, oreRobot, clayRobot + 1, obiRobot, geodeRobot, time - 1);
            }

            if (ore >= blueprint.obRobotOre && clay >= blueprint.obRobotClay)
            {
                Recurse(blueprint, ore - blueprint.obRobotOre + oreRobot, clay - blueprint.obRobotClay + clayRobot, obi + obiRobot, geode + geodeRobot, oreRobot, clayRobot, obiRobot + 1, geodeRobot, time - 1);
            }

            Recurse(blueprint, ore + oreRobot, clay + clayRobot, obi + obiRobot, geode + geodeRobot, oreRobot, clayRobot, obiRobot, geodeRobot, time - 1);
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