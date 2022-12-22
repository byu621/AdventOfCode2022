using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day22Star2
    {
        struct Position
        {

            public int row;
            public int col;

            public Position(int row, int col)
            {
                this.row = row;
                this.col = col;
            }
        }

        private Dictionary<Position, bool> map = new Dictionary<Position, bool>(); // true => wall
        private List<string> moves = new List<string>();
        private Position currentPosition = new Position(-1, -1);
        private int direction = 0; // 0 -> right , 1 -> down , 2 -> left, 3 -> up
        public void Star1(string input)
        {
            string[] lines = File.ReadAllLines(input);
            int row = 0;
            foreach (string line in lines)
            {
                if (!line.Contains("#") && !line.Contains(".") && line != "")
                {
                    ProcessInput(line);
                }

                row++;
                int col = 0;

                foreach (var c in line)
                {
                    col++;
                    if (c == ' ')
                    {
                        continue;
                    }

                    if (c == '.')
                    {
                        if (currentPosition.row == -1)
                        {
                            currentPosition.row = row;
                            currentPosition.col = col;
                        }
                        map.Add(new Position(row, col), false);
                    }
                    else
                    {
                        map.Add(new Position(row, col), true);
                    }
                }
            }

            foreach (var move in moves)
            {
                if (move == "L")
                {
                    direction = (direction + 3) % 4;
                } else if (move == "R")
                {
                    direction = (direction + 1) % 4;
                } else
                {
                    int value = int.Parse(move);
                    for (int i = 0; i < value; i++)
                    {
                        Position nextPosition = MoveInDirection();
                        if (map.ContainsKey(nextPosition))
                        {
                            if (map[nextPosition]) // wall
                            {
                                break;
                            }

                            currentPosition = nextPosition;
                        }
                        else
                        {
                            nextPosition = MoveOffSideCube(nextPosition);
                            if (map[nextPosition])
                            {
                                break;
                            }

                            currentPosition = nextPosition;
                        }
                    }
                }
            }

            Console.WriteLine(1000*currentPosition.row + 4*currentPosition.col + direction);
        }

        private Position MoveOffSideCube(Position position)
        {
            if (direction == 0)
            {
                while (map.ContainsKey(new Position(position.row, position.col - 1)))
                {
                    position.col = position.col - 1;
                }
            }
            else if (direction == 1)
            {
                while (map.ContainsKey(new Position(position.row - 1, position.col)))
                {
                    position.row = position.row - 1;
                }
            }
            else if (direction == 2)
            {
                while (map.ContainsKey(new Position(position.row, position.col + 1)))
                {
                    position.col = position.col + 1;
                }
            }
            else if (direction == 3)
            {
                while (map.ContainsKey(new Position(position.row + 1, position.col)))
                {
                    position.row = position.row + 1;
                }
            }
            return position;
        }

        private Position MoveInDirection()
        {
            Position nextPosition = new Position();
            if (direction == 0)
            {
                nextPosition.row = currentPosition.row;
                nextPosition.col = currentPosition.col + 1;
            }
            else if (direction == 1)
            {
                nextPosition.row = currentPosition.row + 1;
                nextPosition.col = currentPosition.col;
            }
            else if (direction == 2)
            {
                nextPosition.row = currentPosition.row;
                nextPosition.col = currentPosition.col - 1;
            }
            else if (direction == 3)
            {
                nextPosition.row = currentPosition.row - 1;
                nextPosition.col = currentPosition.col;
            }
            return nextPosition;
        }

        private void ProcessInput(string line)
        {
            int a = 0;
            int b = 0;
            while (b < line.Length)
            {
                if (line[b] == 'L' || line[b] == 'R')
                {
                    moves.Add(line.Substring(a, b - a));
                    moves.Add(line[b].ToString());
                    a = b + 1;
                    b++;
                } else
                {
                    b++;
                }
            }

            moves.Add(line.Substring(a, b - a));
        }
    }
}