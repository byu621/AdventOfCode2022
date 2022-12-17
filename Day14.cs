using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Day14
    {
        private HashSet<string> dict = new HashSet<string>();
        private HashSet<int> columns = new HashSet<int>();

        public void Star1(string input)
        {
            int output = 0;
            string[] lines = File.ReadAllLines(input);
            foreach (string line in lines)
            {
                string[] points = line.Split(new string[] { " -> " }, StringSplitOptions.None);
                for (int i = 1; i < points.Length; i++)
                {
                    int leftColumn = int.Parse(points[i-1].Split(',')[0]);
                    int leftRow = int.Parse(points[i-1].Split(',')[1]);
                    int rightColumn = int.Parse(points[i].Split(',')[0]);
                    int rightRow = int.Parse(points[i].Split(',')[1]);

                    if (leftColumn == rightColumn)
                    {
                        columns.Add(leftColumn);
                        int smaller = Math.Min(leftRow, rightRow);
                        int bigger = Math.Max(leftRow, rightRow);
                        for (int j = smaller; j <= bigger; j++)
                        {
                            dict.Add($"{leftColumn}%{j}");
                        } 
                    }
                    else
                    {
                        int smaller = Math.Min(leftColumn, rightColumn);
                        int bigger = Math.Max(leftColumn, rightColumn);
                        for (int j = smaller; j <= bigger; j++)
                        {
                            columns.Add(j);
                            dict.Add($"{j}%{leftRow}");
                        }
                    }
                }
            }

            while (true)
            {
                int startingColumn = 500;
                int startingRow = 0;

                if (Fall(500, 0))
                {

                    break;
                }

                output++;
            }

            Console.WriteLine(output);
        }

        private bool WillStop(int column, int row)
        {
            foreach (var obstacle in dict)
            {
                if (obstacle.StartsWith(column.ToString()))
                {
                    int obstacleRow = int.Parse(obstacle.Split('%')[1]);
                    if (obstacleRow > row)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool Fall(int column, int row)
        {

            if (!WillStop(column, row))
            {
                return true; //end
            }

            while (!dict.Contains($"{column}%{row + 1}"))
            {
                row++;
            }

            if (!dict.Contains($"{column - 1}%{row + 1}"))
            {
                return Fall(column - 1, row + 1);
            }

            if (!dict.Contains($"{column + 1}%{row + 1}"))
            {
                return Fall(column + 1, row + 1);
            }

            dict.Add($"{column}%{row}");
            columns.Add(column);
            return false;
        }

        private bool Fall2(int column, int row)
        {
            while (!dict.Contains($"{column}%{row + 1}") && row+1 < floor)
            {
                row++;
            }

            if (!dict.Contains($"{column - 1}%{row + 1}") && row+1 < floor)
            {
                return Fall2(column - 1, row + 1);
            }

            if (!dict.Contains($"{column + 1}%{row + 1}") && row+1 < floor)
            {
                return Fall2(column + 1, row + 1);
            }

            dict.Add($"{column}%{row}");
            columns.Add(column);
            return false;
        }

        private int floor;

        public void Star2(string input)
        {
            int output = 0;
            string[] lines = File.ReadAllLines(input);
            foreach (string line in lines)
            {
                string[] points = line.Split(new string[] { " -> " }, StringSplitOptions.None);
                for (int i = 1; i < points.Length; i++)
                {
                    int leftColumn = int.Parse(points[i - 1].Split(',')[0]);
                    int leftRow = int.Parse(points[i - 1].Split(',')[1]);
                    int rightColumn = int.Parse(points[i].Split(',')[0]);
                    int rightRow = int.Parse(points[i].Split(',')[1]);

                    floor = Math.Max(floor, Math.Max(leftRow, rightRow) + 2);

                    if (leftColumn == rightColumn)
                    {
                        columns.Add(leftColumn);
                        int smaller = Math.Min(leftRow, rightRow);
                        int bigger = Math.Max(leftRow, rightRow);
                        for (int j = smaller; j <= bigger; j++)
                        {
                            dict.Add($"{leftColumn}%{j}");
                        }
                    }
                    else
                    {
                        int smaller = Math.Min(leftColumn, rightColumn);
                        int bigger = Math.Max(leftColumn, rightColumn);
                        for (int j = smaller; j <= bigger; j++)
                        {
                            columns.Add(j);
                            dict.Add($"{j}%{leftRow}");
                        }
                    }
                }
            }

            while (true)
            {
                int startingColumn = 500;
                int startingRow = 0;
                Fall2(500, 0);
                output++;

                if (dict.Contains($"500%0"))
                {
                    break;
                }

            }

            Console.WriteLine(output);
        }
    }
}