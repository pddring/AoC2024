using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static AoC2024.Day8;

namespace AoC2024
{
    internal class Day8 : Day
    {
        public class Location
        {
            public int x;
            public int y;
            public override string ToString()
            {
                return $"({x},{y})";
            }
        }
        public class Antenna
        {
            public char name;
            public List<Location> locations = new List<Location>();
        }
        public class Map
        {
            public Dictionary<char, Antenna> antennae = new Dictionary<char, Antenna>();
            public List<Location> antinodes = new List<Location>();

            public int width;
            public int height;
            public Map(string[] data)
            {
                width = data[0].Length;
                height = data.Length;
                for(int x = 0; x < width; x++)
                {
                    for(int y = 0; y < height; y++)
                    {
                        char a = data[y][x];
                        if (a != '.')
                        {
                            Location pos = new Location() { x = x, y = y };
                            if (!antennae.ContainsKey(a))
                            {
                                antennae.Add(a, new Antenna() { name = a });
                            }
                            antennae[a].locations.Add(pos);
                        }
                    }
                }
            }

            internal bool containsLocation(Location antinode)
            {
                return antinode.x < width && antinode.x >= 0 && antinode.y < height && antinode.y >= 0;
            }

            internal bool containsAntennaAtLocation(Location l)
            {
                foreach(Location pos in antinodes)
                {
                    if(pos.x == l.x && pos.y == l.y)
                    {
                        return true;
                    }
                }
                return false;
            }

            public override string ToString()
            {
                StringBuilder b = new StringBuilder();
                for (int y = 0; y < height; y++) 
                {
                    for(int x = 0; x < width; x++)    
                    {
                        char indicator = '.';
                        foreach(char a in antennae.Keys)
                        {
                            foreach(Location pos in antennae[a].locations)
                            {
                                if(pos.x == x && pos.y == y)
                                {
                                    indicator = a;
                                }
                            }
                        }
                        foreach(Location pos in antinodes)
                        {
                            if(pos.x == x && pos.y == y)
                            {
                                indicator = '#';
                            }
                        }
                        b.Append(indicator);
                    }
                    b.Append("\n");
                }
                return b.ToString();
            }
        }
        public override string PartA()
        {
            Map m = new Map(data);
            foreach (char a in m.antennae.Keys)
            {
                Log($"Antenna {a}");
                for(int i = 0; i < m.antennae[a].locations.Count; i++)
                {
                    for(int j = 0; j < m.antennae[a].locations.Count; j++)
                    {
                        if (i != j)
                        {
                            Location vectorFromI = new Location()
                            {
                                x = m.antennae[a].locations[j].x - m.antennae[a].locations[i].x,
                                y = m.antennae[a].locations[j].y - m.antennae[a].locations[i].y
                            };

                            Location antinode = new Location()
                            {
                                x = m.antennae[a].locations[i].x - vectorFromI.x,
                                y = m.antennae[a].locations[i].y - vectorFromI.y
                            };

                            if(m.containsLocation(antinode) && !m.containsAntennaAtLocation(antinode))
                            {
                                m.antinodes.Add(antinode);
                            }

                        }
                    }
                }
            }
            Log($"Map size: ({m.width},{m.height})");
            foreach(Location l in m.antinodes)
            {
                Log($"Antinode at ({l.x},{l.y})");
            }
            Log(m.ToString());
            return $"{m.antinodes.Count} antinodes detected";
        }

        public override string PartB()
        {
            Map m = new Map(data);
            foreach (char a in m.antennae.Keys)
            {
                Log($"Antenna {a}");
                for (int i = 0; i < m.antennae[a].locations.Count; i++)
                {
                    for (int j = 0; j < m.antennae[a].locations.Count; j++)
                    {
                        if (i != j)
                        {
                            Location vectorFromI = new Location()
                            {
                                x = m.antennae[a].locations[j].x - m.antennae[a].locations[i].x,
                                y = m.antennae[a].locations[j].y - m.antennae[a].locations[i].y
                            };

                            

                            Location antinode = new Location()
                            {
                                x = m.antennae[a].locations[i].x + vectorFromI.x,
                                y = m.antennae[a].locations[i].y + vectorFromI.y
                            };

                            while(m.containsLocation(antinode))
                            {
                                if (!m.containsAntennaAtLocation(antinode))
                                {
                                    m.antinodes.Add(antinode);
                                }
                                antinode = new Location()
                                {
                                    x = antinode.x + vectorFromI.x,
                                    y = antinode.y + vectorFromI.y
                                };
                            }

                        }
                    }
                }
            }
            Log($"Map size: ({m.width},{m.height})");
            foreach (Location l in m.antinodes)
            {
                Log($"Antinode at ({l.x},{l.y})");
            }
            Log(m.ToString());
            return $"{m.antinodes.Count} antinodes detected";
        }
    }
}
