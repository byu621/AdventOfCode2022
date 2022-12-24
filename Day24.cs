using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day24
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

        class Thing
        {
            public List<char> blizzards = new List<char>();
            public bool wall = false;
        }


        private Dictionary<Position, Thing> map = new Dictionary<Position, Thing>();
        private int height = 0;
        private int width = 0;

        public void Star1(string input)
        {
            int output = 0;
            int id = 0;
            string[] lines = File.ReadAllLines(input);

            int cRow = 0;
            int cCol = 1;
            height = lines.Length;
            width = lines[0].Length;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                for (int j = 0; j < line.Length; j++)
                {
                    char c = line[j];
                    if (c == '#')
                    {
                        var thing = new Thing();
                        thing.wall = true;
                        map.Add(new Position(i, j), thing);
                    } else if (c == '.')
                    {
                        map.Add(new Position(i, j), new Thing());
                    } else
                    {
                        var thing = new Thing();
                        thing.blizzards.Add(c);
                        map.Add(new Position(i, j), thing);
                    }
                }
            }

            int move = MoveToEnd(cRow, cCol, height - 1, width - 2);
            move += MoveToEnd(height - 1, width - 2, cRow, cCol);
            move += MoveToEnd(cRow, cCol, height - 1, width - 2);
            Console.WriteLine(move);
        }

        private int MoveToEnd(int cRow, int cCol, int finishRow, int finishCol)
        {
            Dictionary<Position, int> positions = new Dictionary<Position, int>();
            positions.Add(new Position(cRow, cCol), 0);
            while (true)
            {
                MoveBlizzards();
                var positionsToAdd = new Dictionary<Position, int>();
                foreach (KeyValuePair<Position, int> entry in positions)
                {
                    var position = entry.Key;
                    var moveNumber = entry.Value;
                    List<Position> newPositions = new List<Position>()
                        { new Position(position.row + 1, position.col),
                          new Position(position.row - 1, position.col),
                          new Position(position.row, position.col + 1),
                          new Position(position.row, position.col - 1),
                          new Position(position.row, position.col)};

                    foreach (var p in newPositions)
                    {
                        if (positionsToAdd.ContainsKey(p))
                        {
                            positionsToAdd[p] = Math.Min(moveNumber + 1, positionsToAdd[p]);
                        }
                        else
                        {
                            positionsToAdd.Add(p, moveNumber + 1);
                        }
                    }
                }

                positionsToAdd = positionsToAdd.Where((kvp) => map.ContainsKey(kvp.Key) && !map[kvp.Key].wall && map[kvp.Key].blizzards.Count == 0).ToDictionary(item => item.Key, item => item.Value);

                positions.Clear();
                positions = positionsToAdd;

                if (positions.ContainsKey(new Position(finishRow, finishCol)))
                {
                    return positions[new Position(finishRow, finishCol)];
                }
            }
        }

        private void MoveBlizzards()
        {
            var newDictionary = new Dictionary<Position, Thing>();
            foreach (KeyValuePair<Position, Thing> entry in map)
            {
                var position = entry.Key;
                var thing = entry.Value;
                if (thing == null || thing.wall)
                {
                    continue;
                }

                foreach (var b in thing.blizzards)
                {
                    Position newPosition = getNewPosition(position, b);
                    if (!newDictionary.ContainsKey(newPosition))
                    {
                        newDictionary.Add(newPosition, new Thing());
                    }

                    newDictionary[newPosition].blizzards.Add(b);
                }

                map[position].blizzards.Clear();
            }

            foreach (KeyValuePair<Position, Thing> entry in newDictionary)
            {
                foreach (var b in entry.Value.blizzards)
                {
                    map[entry.Key].blizzards.Add(b);
                }
            }
        }

        private Position getNewPosition(Position position, char b)
        {
            Position newPosition;
            if (b == '>')
            {
                newPosition = new Position(position.row, position.col + 1);
                if (map[newPosition] != null && map[newPosition].wall)
                {
                    newPosition = new Position(position.row, 1);
                }
            }
            else if (b == '<')
            {
                newPosition = new Position(position.row, position.col - 1);
                if (map[newPosition].wall)
                {
                    newPosition = new Position(position.row, width-2);
                }
            }
            else if (b == '^')
            {
                newPosition = new Position(position.row - 1, position.col);
                if (map[newPosition].wall)
                {
                    newPosition = new Position(height-2, position.col);
                }
            }
            else if (b == 'v')
            {
                newPosition = new Position(position.row + 1, position.col);
                if (map[newPosition].wall)
                {
                    newPosition = new Position(1, position.col);
                }
            } else
            {
                throw new Exception();
            }
            return newPosition;

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