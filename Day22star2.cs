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

            public static bool operator == (Position p1, Position p2)
            {
                return p1.row == p2.row && p1.col == p2.col;
            }

            public static bool operator != (Position p1, Position p2)
            {
                return p1.row != p2.row || p1.col != p2.col;
            }
        }

        private Dictionary<Position, bool> map = new Dictionary<Position, bool>(); // true => wall
        private List<string> moves = new List<string>();
        private Position currentPosition = new Position(-1, -1);
        private int direction = 0; // 0 -> right , 1 -> down , 2 -> left, 3 -> up
        private int size = 0;
        public void Star1(string input)
        {
            if (input.Contains("sample"))
            {
                size = 4;
            } else
            {
                size = 50;
            }

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

            GenerateCube(currentPosition);
            MoreGenerateCube();

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
                            nextPosition = MoveOffSideCube(currentPosition);
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
            Position basePosition = GetBasePosition(position);
            var oFace = faceMap[new Face(basePosition, direction)];
            int principalMapping = position.row - basePosition.row; // if right or left
            if (direction == 1 || direction == 3)
            {
                principalMapping = position.col - basePosition.col; // up or down
            }

            if (oFace.direction == 2 || oFace.direction == 0) // left or right
            {
                int newRow = oFace.position.row + principalMapping;
                if (direction == oFace.direction || direction == (oFace.direction + 3) % 4)
                {
                    newRow = oFace.position.row + size - 1 - principalMapping;
                }


                int newCol = oFace.direction == 2 ? oFace.position.col : oFace.position.col + size - 1;

                if (!map[new Position(newRow, newCol)])
                {
                    direction = (oFace.direction + 2) % 4;
                }

                return new Position(newRow, newCol);
            }

            if (oFace.direction == 1 || oFace.direction == 3)
            {
                int newCol = oFace.position.col + principalMapping; 
                if (direction == oFace.direction || direction == (oFace.direction + 1) % 4)
                {
                    newCol = oFace.position.col + size - 1 - principalMapping;
                }

                int newRow = oFace.direction == 3 ? oFace.position.row : oFace.position.row + size - 1;

                if (!map[new Position(newRow, newCol)])
                {
                    direction = (oFace.direction + 2) % 4;
                }

                return new Position(newRow, newCol);
            }

            throw new Exception("something went wrong");
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

        struct Face
        {
            public Position position;
            public int direction;

            public Face(Position position, int direction)
            {
                this.position = position;
                this.direction = direction;
            }
        }

        private Dictionary<Face, Face> faceMap = new Dictionary<Face, Face>();
        private HashSet<Position> allFaces = new HashSet<Position>();

        private void GenerateCube(Position startingPosition)
        {
            allFaces.Add(startingPosition);
            var adjacentFacePositions = getAdjacentFacePositions(startingPosition);
            for (int i = 0; i < adjacentFacePositions.Count; i++)
            {
                Position newPosition = adjacentFacePositions[i];
                if (map.ContainsKey(newPosition))
                {
                    var face = new Face(startingPosition, i); 
                    if (!faceMap.ContainsKey(face))
                    {
                        faceMap.Add(face, new Face(newPosition, (i + 2) % 4));
                        GenerateCube(newPosition);
                    }
                }
            }
        }

        private void MoreGenerateCube()
        {
            var addToFaceMap = new Dictionary<Face, Face>();
            foreach (KeyValuePair<Face, Face> entry in faceMap)
            {
                var face = entry.Key;
                var oFace = entry.Value;
                if (face.direction == 0 || face.direction == 2) // right, left
                {
                    if (faceMap.ContainsKey(new Face(face.position, 3)))
                    {
                        var a = faceMap[new Face(face.position, 3)];
                        var incomingDirection = face.direction == 0 ? (a.direction + 3) % 4 : (a.direction + 1) % 4;
                        var key = new Face(a.position, incomingDirection);
                        var newDirection = face.direction == 0 ? (oFace.direction + 1) % 4 : (oFace.direction + 3) % 4;
                        var value = new Face(oFace.position, newDirection);
                        if (!addToFaceMap.ContainsKey(key)) addToFaceMap.Add(key, value);
                    }

                    if (faceMap.ContainsKey(new Face(face.position, 1)))
                    {
                        var a = faceMap[new Face(face.position, 1)];
                        var incomingDirection = face.direction == 0 ? (a.direction + 1) % 4 : (a.direction + 3) % 4;
                        var key = new Face(a.position, incomingDirection);
                        var newDirection = face.direction == 0 ? (oFace.direction + 3) % 4 : (oFace.direction + 1) % 4;
                        var value = new Face(oFace.position, newDirection);
                        if (!addToFaceMap.ContainsKey(key)) addToFaceMap.Add(key, value);

                    }
                } else if (face.direction == 1 || face.direction == 3)
                {
                    if (faceMap.ContainsKey(new Face(face.position, 0)))
                    {
                        var a = faceMap[new Face(face.position, 0)];
                        var incomingDirection = face.direction == 1 ? (a.direction + 3) % 4 : (a.direction + 1) % 4;
                        var key = new Face(a.position, incomingDirection);
                        var newDirection = face.direction == 1 ? (oFace.direction + 1) % 4 : (oFace.direction + 3) % 4;
                        var value = new Face(oFace.position, newDirection);
                        if (!addToFaceMap.ContainsKey(key)) addToFaceMap.Add(key, value);
                    }

                    if (faceMap.ContainsKey(new Face(face.position, 2)))
                    {
                        var a = faceMap[new Face(face.position, 2)];
                        var incomingDirection = face.direction == 1 ? (a.direction + 1) % 4 : (a.direction + 3) % 4;
                        var key = new Face(a.position, incomingDirection);
                        var newDirection = face.direction == 1 ? (oFace.direction + 3) % 4 : (oFace.direction + 1) % 4;
                        var value = new Face(oFace.position, newDirection);
                        if (!addToFaceMap.ContainsKey(key)) addToFaceMap.Add(key, value);
                    }
                }

                //var oppositeDirection = (face.direction + 2) % 4;
                //if (faceMap.ContainsKey(new Face(face.position, oppositeDirection)))
                //{
                //    var a = faceMap[new Face(face.position, oppositeDirection)];
                //    if (faceMap.ContainsKey(new Face(a.position, oppositeDirection)))
                //    {
                //        var b = faceMap[new Face(a.position, oppositeDirection)];
                //        var key = new Face(b.position, oppositeDirection);
                //        var value = new Face(oFace.position, (oFace.direction + 2) % 4);
                //        if (!addToFaceMap.ContainsKey(key)) addToFaceMap.Add(key, value);
                //    }
                //}
            }

            bool added = false;
            foreach (var entry in addToFaceMap)
            {
                if (faceMap.ContainsKey(entry.Key))
                {
                    continue;
                }

                faceMap.Add(entry.Key, entry.Value);
                added = true;
            }

            if (added)
            {
                MoreGenerateCube();
                return;
            }

            foreach (var person in faceMap.OrderBy(i => i.Key.position.row * 100 + i.Key.position.col + i.Key.direction))
            {
                Console.WriteLine($"{person.Key.position.row},{person.Key.position.col},{person.Key.direction},{person.Value.position.row},{person.Value.position.col},{person.Value.direction}");
            }
        }
        
        private Position GetBasePosition(Position position)
        {
            // 1 5 9 13
            // 0 4 8 12
            // 1 => 1 2 => 1 3 => 1 4 => 1 5 => 5
            // minus 1 6 => 5 > 1 4 5
            int row = (position.row - 1) / size * size + 1;
            int col = (position.col - 1) / size * size + 1;
            return new Position(row, col);
        }

        private List<Position> getAdjacentFacePositions(Position position)
        {
            List<Position> output = new List<Position>();
            output.Add(new Position(position.row, position.col + size));
            output.Add(new Position(position.row + size, position.col));
            output.Add(new Position(position.row, position.col - size));
            output.Add(new Position(position.row - size, position.col));
            return output;
        }
    }
}