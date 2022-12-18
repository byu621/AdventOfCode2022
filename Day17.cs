using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day17
    {
        struct Position
        {

            public int x;
            public int y;

            public Position(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        class Rock
        {

            public int width;
            public int height;
            public List<Position> leftSide = new List<Position>();
            public List<Position> rightSide = new List<Position>();
            public List<Position> bottomSide = new List<Position>();
            public List<Position> all = new List<Position>();

            public Rock(int width, int height)
            {
                this.width = width;
                this.height = height;
            }

            public void AddParticle(int x, int y)
            {
                if (y == 0 || !all.Contains(new Position(x, y-1)))
                {
                    bottomSide.Add(new Position(x,y));
                }

                if (x == 0 || !all.Contains(new Position(x-1,y)))
                {
                    leftSide.Add(new Position(x, y));
                }

                if (x == width - 1 || !all.Contains(new Position(x+1,y)))
                {
                    rightSide.Add(new Position(x, y));
                }

                all.Add(new Position(x, y));
            }
        }

        private List<char> jet = new List<char>();
        private int jetNumber = 0;
        private char NextJet()
        {
            char output = jet[jetNumber];
            jetNumber = (jetNumber + 1) % jet.Count;
            return output;
        }

        private HashSet<Position> map = new HashSet<Position>();
        private int maxHeight = 0;
        private List<Rock> rocks = new List<Rock>();
        private Dictionary<int, int> pattern = new Dictionary<int, int>();
        private Dictionary<Position, int> rockJetMatch = new Dictionary<Position, int>();
        private Dictionary<int, int> maxHeightDict = new Dictionary<int, int>();

        public int Star1(string input, int number)
        {
            string[] lines = File.ReadAllLines(input);
            foreach (string line in lines)
            {
                foreach (var c in line)
                {
                    jet.Add(c);
                }
            }

            SetUpRocks();

            int rockIndex = 0;
            //5455  -> 8442   1
            //10910 -> 16903  2
            //16365 -> 25355  3
            //21820 -> 33786  4
            //         42235  5
            //         50666  6
            //         59130  7
            //         67572  8
            //         76008  9
            int previousMatchHeight = 0;
            int suspectedPattern = 0;
            int previousI = 0;
            int suspectedPatternI = 0;
            int count = 0;
            int countI = 0;
            int differenceValue = 0;
            for (int i = 0; i < 10_000*50455; i++)
            {
                if (rockJetMatch.ContainsKey(new Position(jetNumber, rockIndex)))
                {
                    rockJetMatch[new Position(jetNumber, rockIndex)] += 1;
                }
                else
                {
                    rockJetMatch.Add(new Position(jetNumber, rockIndex), 1);
                }
                if (jetNumber == 4 && rockIndex == 1)
                {
                    if (maxHeight - previousMatchHeight == suspectedPattern)
                    {
                        count++;
                    } else
                    {
                        suspectedPattern = maxHeight - previousMatchHeight;
                    }

                    if (i - previousI == suspectedPatternI)
                    {
                        countI++;
                    } else
                    {
                        suspectedPatternI = i - previousI;
                    }

                    previousI = i;
                    previousMatchHeight = maxHeight;

                    if (count > 100 && countI > 100)
                    {
                        long difference = 1_000_000_000_000-i - (((1_000_000_000_000-i) / suspectedPatternI) * suspectedPatternI);
                        differenceValue = maxHeightDict[i - suspectedPatternI + (int)difference] - maxHeightDict[i-suspectedPatternI];
                        Console.WriteLine(suspectedPattern);
                        long usePattern = (1_000_000_000_000 - i) / suspectedPatternI * suspectedPattern;
                        Console.WriteLine($"FINAL ANSWER: {maxHeight + usePattern + differenceValue}");
                        return 1;
                    }
                }

                if (i % 50455 == 0)
                {
                    int multiple = i / 50455;
                    //Console.WriteLine($"{multiple}-{maxHeight}");
                    pattern.Add(multiple, maxHeight);
                    if (multiple % 3 == 0 && multiple != 0 && pattern[multiple] % pattern[multiple/3] == 0)
                    {
                        //Console.WriteLine($"found it {multiple}");
                    }
                }

                Rock rock = rocks[rockIndex];
                rockIndex = (rockIndex + 1) % rocks.Count;

                int leftIndex = 2;
                int bottomIndex = maxHeight + 4;
                while (true)
                {
                    char jet = NextJet();
                    int toMove;
                    if (jet == '>')
                    {
                        toMove = 1;
                        foreach (var particle in rock.rightSide)
                        {
                            int x = particle.x + leftIndex + 1;
                            int y = particle.y + bottomIndex;
                            if (x>=7 || map.Contains(new Position(x, y)))
                            {
                                toMove = 0;
                                break;
                            }
                        }
                    } else
                    {
                        toMove = -1;
                        foreach (var particle in rock.leftSide)
                        {
                            int x = particle.x + leftIndex - 1;
                            int y = particle.y + bottomIndex;
                            if (x < 0 || map.Contains(new Position(x, y)))
                            {
                                toMove = 0;
                                break;
                            }
                        }
                    }

                    leftIndex += toMove;

                    bool drawn = false;
                    foreach (var particle in rock.bottomSide)
                    {
                        int x = particle.x + leftIndex;
                        int y = particle.y + bottomIndex - 1;
                        if (y <= 0 || map.Contains(new Position(x, y)))
                        {
                            Draw(bottomIndex, leftIndex, rock);
                            drawn = true;
                            break;
                        }
                    }

                    if (drawn)
                    {
                        break;
                    }

                    bottomIndex--;

                }
                maxHeightDict[i + 1] = maxHeight;

            }
            return maxHeight;
        }

        private void Draw(int bottomIndex, int leftIndex, Rock rock)
        {
            foreach (var particle in rock.all)
            {
                int x = particle.x + leftIndex;
                int y = particle.y + bottomIndex;
                if (map.Contains(new Position(x, y)))
                {
                    Console.WriteLine("what");
                }
                map.Add(new Position(x, y));
                maxHeight = Math.Max(y, maxHeight);
            }
            //Console.WriteLine(maxHeight);
        }

        private void SetUpRocks()
        {
            Rock rock = new Rock(4, 1);
            rock.AddParticle(0, 0);
            rock.AddParticle(1, 0);
            rock.AddParticle(2, 0);
            rock.AddParticle(3, 0);
            rocks.Add(rock);

            rock = new Rock(3, 3);
            rock.AddParticle(1, 0);
            rock.AddParticle(0, 1);
            rock.AddParticle(2, 1);
            rock.AddParticle(1, 2);
            rock.AddParticle(1, 1);

            rocks.Add(rock);

            rock = new Rock(3, 3);
            rock.AddParticle(0, 0);
            rock.AddParticle(1, 0);
            rock.AddParticle(2, 0);
            rock.AddParticle(2, 1);
            rock.AddParticle(2, 2);
            rocks.Add(rock);

            rock = new Rock(1, 4);
            rock.AddParticle(0, 0);
            rock.AddParticle(0, 1);
            rock.AddParticle(0, 2);
            rock.AddParticle(0, 3);
            rocks.Add(rock);

            rock = new Rock(2, 2);
            rock.AddParticle(0, 0);
            rock.AddParticle(1, 0);
            rock.AddParticle(0, 1);
            rock.AddParticle(1, 1);
            rocks.Add(rock);
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