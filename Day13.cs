using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class Member
    {

    }

    class ListA : Member
    {
        public List<Member> contents = new List<Member>();
    }

    class IntA : Member
    {
        public IntA(int _value)
        {
            value = _value;
        }
        public int value;
    }

    class Day13
    {

        private int sum = 0;
        private List<int> success = new List<int>();
        public void Star1(string input)
        {
            int output = 0;

            string[] lines = File.ReadAllLines(input);
            for (int i = 0; i < lines.Length; i+=3)
            {
                string left = lines[i];
                string right = lines[i + 1];
                ListA l = Parse(left);
                ListA r = Parse(right);

                if (Compare(l, r) == 1)
                {
                    success.Add(i/3 + 1);
                    sum += i/3 + 1;
                }
            }

            Console.WriteLine(sum);
        }

        //true, false, continue
        //int 
        // 1 == true, 0 == continue, -1 == false

        private int Compare(Member left, Member right)
        {
            if (left is ListA && right is ListA)
            {
                ListA leftList = (ListA)left;
                ListA rightList = (ListA)right;
                for (int i = 0; i < leftList.contents.Count; i++)
                {
                    Member inLeft = leftList.contents[i];
                    if (i == rightList.contents.Count)
                    {
                        return -1;
                    }

                    Member inRight = rightList.contents[i];
                    int outcome = Compare(inLeft, inRight);
                    if (outcome != 0)
                    {
                        return outcome;
                    }
                }

                if (rightList.contents.Count > leftList.contents.Count)
                {
                    return 1;
                }
            } else if (left is IntA && right is IntA)
            {
                if (((IntA)left).value > ((IntA)right).value)
                {
                    return -1;
                } else if (((IntA)right).value > ((IntA)left).value)
                {
                    return 1;
                } else
                {
                    return 0;
                }
            } else if (left is IntA)
            {
                ListA leftList = new ListA();
                leftList.contents.Add((IntA)left);
                return Compare(leftList, right);
            } else
            {
                ListA rightList = new ListA();
                rightList.contents.Add((IntA)right);
                return Compare(left, rightList);
            }

            return 0;
        }

        public void Star2(string input)
        {
            int output = 1;
            List<string> inorder = new List<string>();

            string[] lines = File.ReadAllLines(input);
            inorder.Add(lines[0]);
            inorder.Add("[[2]]");
            inorder.Add("[[6]]");
            for (int i = 1; i < lines.Length; i++)
            {
                if (lines[i] == "")
                {
                    continue;
                }

                inorder.Add(lines[i]);
            }

            inorder.Sort((a, b) =>
            {
                ListA aList = Parse(a);
                ListA bList = Parse(b);
                return Compare(aList, bList) * -1;
            });

            for (int i = 0; i < inorder.Count; i++)
            {
                if (inorder[i] == "[[2]]" || inorder[i] == "[[6]]")
                {
                    output *= i + 1;
                }
            }

            Console.WriteLine(output);
        }

        private ListA Parse(string input)
        {
            ListA list = new ListA();
            input = input.Substring(1, input.Length - 2);
            List<string> split = Split(input);
            foreach (string member in split)
            {
                if (member == "")
                {
                    return list;
                }

                if (member.StartsWith("["))
                {
                    list.contents.Add(Parse(member));
                }
                else
                {
                    list.contents.Add(new IntA(int.Parse(member)));
                }
            }

            return list;
        }

        private List<string> Split(string input)
        {
            List<string> output = new List<string>();
            int inList = 0;
            int previousSplit = 0;
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (c == '[')
                {
                    inList++;
                } else if (c == ']')
                {
                    inList--;
                }

                if (c == ',' && inList == 0)
                {
                    output.Add(input.Substring(previousSplit, i - previousSplit));
                    previousSplit = i + 1;
                }
            }

            output.Add(input.Substring(previousSplit));
            return output;
        }
    }
}