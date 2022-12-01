using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Star1
    {
        public static void Calculate()
        {
            string[] lines = File.ReadAllLines($"{Environment.CurrentDirectory}/Input/Star1.txt");
            int currentTotalCalorie = 0;
            int currentCalorie = 0;
            int[] topCalories = new int[3];

            foreach (string line in lines)
            {
                if (line == string.Empty)
                {
                    int minIndex = 0;
                    int minValue = topCalories[0];
                    for (int i = 1; i < topCalories.Length; i++)
                    {
                        if (topCalories[i] < minValue)
                        {
                            minValue = topCalories[i];
                            minIndex = i;
                        }
                    }

                    topCalories[minIndex] = Math.Max(minValue, currentTotalCalorie);
                    currentTotalCalorie = 0;
                } 
                else
                {
                    currentCalorie = Int32.Parse(line);
                    currentTotalCalorie += currentCalorie;
                }
            }

            int maxCalorie = topCalories.Max();
            Console.WriteLine($"maxElfCalorie: {maxCalorie}");

            foreach (int topCalorie in topCalories)
            {
                Console.WriteLine($"top: {topCalorie}");
            }

            int topThreeCalorie = topCalories.Sum();
            Console.WriteLine($"topThreeCalorie: {topThreeCalorie}");
        }
    }
}
