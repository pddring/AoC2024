using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024
{
    internal class Day6 : Day
    {
        // implemented in python
        public override string PartA()
        {
            return "Not implemented yet";
        }

        public class Grid
        {
            public char[,] grid = new char[0,0];
            public void init(string[] data)
            {
                width = data[0].Length;
                height = data.Length;
                grid = new char[height, width];
            }
            public void load(string[] data)
            {
                for(int x = 0; x < width; x++)
                {
                    for(int y = 0; y < height; y++)
                    {
                        grid[y, x] = data[y][x];
                    }
                }
                calcPos();

            }
            public int width;
            public int height;
            public Pos pos = new Pos();
            public bool onGrid = true;

            public override string ToString()
            {
                StringBuilder s = new StringBuilder();
                s.AppendLine($"Pos: ({pos.x},{pos.y}) Size: ({width},{height}) Direction: {pos.direction}");
                for(int y = 0; y < height; y++)
                {
                    for(int x = 0; x < width; x++)
                    {
                        s.Append(grid[y, x]);
                    }
                    s.Append('\n');
                }
                return s.ToString();
            }

            public void calcOnGrid()
            {
                onGrid = pos.x >= 0 && pos.x < width
                    && pos.y >= 0 && pos.y < height;
            }

            public void move()
            {
                grid[pos.y, pos.x] = 'X';
                if(pos.direction == '^')
                {
                    if(pos.y - 1 < 0)
                    {
                        onGrid = false;
                    } else if (grid[pos.y - 1, pos.x] == '#')
                    {
                        pos.rotate();
                    } else
                    {
                        pos.y -= 1;
                    }
                } else if (pos.direction == '>')
                {
                    if (pos.x + 1 >= width)
                    {
                        onGrid = false;
                    }
                    else if (grid[pos.y, pos.x+1] == '#')
                    {
                        pos.rotate();
                    }
                    else
                    {
                        pos.x += 1;
                    }
                } else if (pos.direction == 'V')
                {
                    if (pos.y + 1 >= height)
                    {
                        onGrid = false;
                    }
                    else if (grid[pos.y + 1, pos.x] == '#')
                    {
                        pos.rotate();
                    }
                    else
                    {
                        pos.y += 1;
                    }
                } else if (pos.direction == '<')
                {
                    if (pos.x - 1 < 0)
                    {
                        onGrid = false;
                    }
                    else if (grid[pos.y, pos.x - 1] == '#')
                    {
                        pos.rotate();
                    }
                    else
                    {
                        pos.x -= 1;
                    }
                }
                grid[pos.y, pos.x] = pos.direction;
            }

            public void calcPos()
            {
                for(int x = 0; x < width; x++)
                {
                    for(int y = 0; y < height; y++)
                    {
                        switch(grid[y,x])
                        {
                            case '^':
                            case '>':
                            case 'V':
                            case '<':
                                pos.x = x;
                                pos.y = y;
                                pos.direction = grid[y, x];
                                return;
                        }
                    }
                }
            }

        } 

        public class Pos
        {
            public int x;
            public int y;
            public char direction;
            public override string ToString()
            {
                return $"{x} {y} {direction}";
            }

            public void rotate()
            {
                const string directions = "^>V<";
                direction = directions[(directions.IndexOf(direction) + 1) % (directions.Length)];
            }

        }

        public override string PartB()
        {
            Grid grid = new Grid();
            grid.init(data);
            int loops = 0;
            Dictionary<string, bool> visited = new Dictionary<string, bool>();
            Log(grid.ToString());
            for(int i = 0; i < grid.width; i++)
            {
                for(int j = 0; j < grid.height; j++)
                {
                    visited.Clear();
                    grid.load(data);
                    grid.calcPos();
                    grid.calcOnGrid();

                    if (i == grid.pos.x && j == grid.pos.y)
                        continue;
                    int progress = (i * grid.height + j) * 100 / (grid.width * grid.height);
                    Log($"({i},{j}) {progress}%");

                    grid.grid[j, i] = '#';
                    //Log(grid.ToString());

                    while (grid.onGrid)
                    {
                        grid.move();
                        string v = grid.pos.ToString();
                        if(visited.ContainsKey(v))
                        {
                            if(grid.onGrid)
                            {
                                loops++;
                                break;
                            } 
                        }
                        else
                        {
                            visited.Add(v, true);
                        }
                    }

                    grid.grid[grid.pos.y, grid.pos.x] = 'X';

                }
            }


            return $"{loops}";
        }
    }
}
