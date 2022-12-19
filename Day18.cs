using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{

    /**
     * 
     * 0
5
5
5
5
5
4
5
6
6
6
6
6
64
     */
    class Day18
    {
        struct Droplet
        {

            public int x;
            public int y;
            public int z;
            public Droplet(int x, int y, int z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }
        }

        private List<Droplet> droplets = new List<Droplet>();
        private HashSet<Droplet> lava = new HashSet<Droplet>();

        public void Star1(string input)
        {
            int output = 0;
            int insideCount = 0;
            int outsideCount = 0;
            string[] lines = File.ReadAllLines(input);
            foreach (string line in lines)
            {
                string[] split = line.Split(',');
                var droplet = new Droplet(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]));
                droplets.Add(droplet);
            }

            int minx = 0;
            int miny = 0;
            int minz = 0;
            int maxx = 0;
            int maxy = 0;
            int maxz = 0;

            foreach (var droplet in droplets)
            {
                minx = Math.Min(droplet.x, minx);
                miny = Math.Min(droplet.y, miny);
                minz = Math.Min(droplet.z, minz);
                maxx = Math.Max(droplet.x, maxx);
                maxy = Math.Max(droplet.y, maxy);
                maxz = Math.Max(droplet.z, maxz);
            }

            List<Droplet> newLava = new List<Droplet>() { new Droplet(minx, miny, minz) };
            while (newLava.Count > 0)
            {
                List<Droplet> newnewLava = new List<Droplet>();
                foreach (var droplet in newLava)
                {
                    int x = droplet.x;
                    int y = droplet.y;
                    int z = droplet.z;

                    List<Droplet> adjacentDroplets = new List<Droplet>();
                    adjacentDroplets.Add(new Droplet(x + 1, y, z));
                    adjacentDroplets.Add(new Droplet(x - 1, y, z));
                    adjacentDroplets.Add(new Droplet(x, y - 1, z));
                    adjacentDroplets.Add(new Droplet(x, y + 1, z));
                    adjacentDroplets.Add(new Droplet(x, y, z - 1));
                    adjacentDroplets.Add(new Droplet(x, y, z + 1));


                    foreach (var adjacent in adjacentDroplets)
                    {
                        if (adjacent.x < minx-1 || adjacent.x > maxx+1 || adjacent.y < miny-1 || adjacent.y > maxy+1 || adjacent.z < minz-1 || adjacent.z > maxz+1)
                        {
                            continue;
                        }

                        if (adjacent.x == 3 && adjacent.y ==2 && adjacent.z == 2)
                        {
                            Console.WriteLine("asdf");
                        }


                        if (!droplets.Contains(adjacent) && !newLava.Contains(adjacent) && !lava.Contains(adjacent) && !newnewLava.Contains(adjacent))
                        {
                            newnewLava.Add(adjacent);
                        }
                    }
                }

                foreach (var a in newLava)
                {
                    lava.Add(a);
                }
                newLava = new List<Droplet>(newnewLava);
            }


            foreach (var droplet in droplets)
            {
                int sa = 0;
                int x = droplet.x;
                int y = droplet.y;
                int z = droplet.z;

                List<Droplet> adjacentDroplets = new List<Droplet>();
                adjacentDroplets.Add(new Droplet(x + 1, y, z));
                adjacentDroplets.Add(new Droplet(x - 1, y, z));
                adjacentDroplets.Add(new Droplet(x, y - 1, z));
                adjacentDroplets.Add(new Droplet(x, y + 1, z));
                adjacentDroplets.Add(new Droplet(x, y, z - 1));
                adjacentDroplets.Add(new Droplet(x, y, z + 1));

                foreach (var adjacent in adjacentDroplets)
                {
                    if (droplets.Contains(adjacent))
                    {
                        continue;
                    }

                    if (lava.Contains(adjacent))
                    {
                        sa++;
                    }
                    else
                    {
                        Console.WriteLine("wtf");
                    }
                }

                Console.WriteLine(sa);
                output += sa;
            }

            Console.WriteLine(output);
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