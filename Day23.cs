using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day23
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

        class Elve
        {

            public Position position;
            public Position? destinationPosition;
            public bool canIMoveHere = false;

            public Elve(Position position, Position? destinationPosition)
            {
                this.position = position;
                this.destinationPosition = destinationPosition;
            }
        }

        private List<Elve> map = new List<Elve>();
        private HashSet<Position> positions = new HashSet<Position>();
        private List<char> directions = new List<char>() { 'N', 'S', 'W', 'E' };

        public void Star1(string input)
        {
            string[] lines = File.ReadAllLines(input);
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                for (int j = 0; j < line.Length; j++)
                {
                    char c = line[j];
                    if (c == '#')
                    {
                        map.Add(new Elve(new Position(i, j), null));
                        positions.Add(new Position(i, j));
                    }
                }
            }

            int round = 1;
            while (true)
            {
                foreach (var elve in map)
                {
                    if (!elfNearby(elve))
                    {
                        continue;
                    }

                    List<Position> destinationPositions = new List<Position>();
                    foreach (var direction in directions)
                    {
                        destinationPositions = getDestinationPositions(elve.position, direction);
                        if (destinationPositions.Count == 0)
                        {
                            continue;
                        }

                        if (isElveHere(destinationPositions))
                        {
                            continue;
                        }

                        elve.destinationPosition = destinationPositions[0];
                        break;
                    }
                }

                CycleDirection();
                bool elfMoved = false;
                foreach (var elve in map)
                {
                    if (!elve.destinationPosition.HasValue)
                    {
                        continue;
                    }

                    elve.canIMoveHere = canIMoveHere(elve);
                }

                foreach (var elve in map)
                {
                    if (elve.canIMoveHere)
                    {
                        positions.Remove(elve.position);
                        elve.position = elve.destinationPosition.Value;
                        positions.Add(elve.position);
                        elve.destinationPosition = null;
                        elve.canIMoveHere = false;
                        elfMoved = true;
                    } 

                    elve.destinationPosition = null;
                }
                if (!elfMoved)
                {
                    map.Sort((a, b) =>
                    {
                        return a.position.row.CompareTo(b.position.row);
                    });
                    Console.WriteLine(round);
                    break;
                }
                round++;
            }
        }

        private void CycleDirection()
        {
            char toRemove = directions[0];
            directions.Remove(toRemove);
            directions.Add(toRemove);
        }

        private bool elfNearby(Elve elve)
        {
            bool a = positions.Contains(new Position(elve.position.row-1, elve.position.col-1));
            bool b = positions.Contains(new Position(elve.position.row-1, elve.position.col));
            bool c = positions.Contains(new Position(elve.position.row-1, elve.position.col+1));
            bool d = positions.Contains(new Position(elve.position.row, elve.position.col-1));
            bool e = positions.Contains(new Position(elve.position.row, elve.position.col+1));
            bool f = positions.Contains(new Position(elve.position.row+1, elve.position.col-1));
            bool g = positions.Contains(new Position(elve.position.row+1, elve.position.col));
            bool h = positions.Contains(new Position(elve.position.row+1, elve.position.col+1));
            return a || b || c || d || e || f || g || h;
        }

        private bool canIMoveHere(Elve elve)
        {
            if (!elve.destinationPosition.HasValue)
            {
                return false;
            }

            foreach (var elve2 in map)
            {
                if (elve.position.row == elve2.position.row && elve.position.col == elve2.position.col)
                {
                    continue;
                }
                if (!elve2.destinationPosition.HasValue)
                {
                    continue;
                }

                if (elve2.destinationPosition.Value.row == elve.destinationPosition.Value.row && elve.destinationPosition.Value.col == elve2.destinationPosition.Value.col)
                {
                    return false;
                }
            }

            return true;
        }

        private bool isElveHere(List<Position> positions)
        {
            foreach (var position in positions)
            {
                foreach (var e in map)
                {
                    if (e.position.row == position.row && e.position.col == position.col)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private List<Position> getDestinationPositions(Position currentPosition, char direction)
        {
            List<Position> positions = new List<Position>();
            if (direction == 'N')
            {
                positions.Add(new Position(currentPosition.row - 1, currentPosition.col));
                positions.Add(new Position(currentPosition.row - 1, currentPosition.col-1));
                positions.Add(new Position(currentPosition.row - 1, currentPosition.col+1));
            } else if (direction == 'S')
            {
                positions.Add(new Position(currentPosition.row + 1, currentPosition.col));
                positions.Add(new Position(currentPosition.row + 1, currentPosition.col - 1));
                positions.Add(new Position(currentPosition.row + 1, currentPosition.col + 1));
            }
            else if(direction == 'E')
            {
                positions.Add(new Position(currentPosition.row, currentPosition.col + 1));
                positions.Add(new Position(currentPosition.row - 1, currentPosition.col + 1));
                positions.Add(new Position(currentPosition.row + 1, currentPosition.col + 1));
            }
            else if(direction == 'W')
            {
                positions.Add(new Position(currentPosition.row, currentPosition.col - 1));
                positions.Add(new Position(currentPosition.row - 1, currentPosition.col - 1));
                positions.Add(new Position(currentPosition.row + 1, currentPosition.col - 1));
            } 
            return positions;
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