using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024
{
    internal class Day4 : Day
    {

        class Coords
        {
            public int row;
            public int col;
            public string[] data;
            string search = "XMAS";
            public int diffX;
            public int diffY;

            public Coords(int row, int col, string[] data, string search)
            {
                this.row = row;
                this.col = col;
                this.data = data;
                this.search = search;
            }

            public bool IntersectsWith(Coords other)
            {
                int cx = col + diffX;
                int cy = row + diffY;
                int ocx = other.col + other.diffX;
                int ocy = other.row + other.diffY;
                return cx == ocx && cy == ocy;
            }

            public bool Check(int diffX, int diffY)
            {
                int r = row;
                int c = col;

                for (int i = 0; i < search.Length; i++)
                {
                    if (r >= data.Length || r < 0 || c >= data[0].Length || c < 0)
                        return false;

                    if (data[r][c] != search[i])
                    {
                        return false;
                    }
                    r += diffY;
                    c += diffX;
                }
                this.diffX = diffX;
                this.diffY = diffY;
                return true;
            }
        }

        public override string PartA()
        {
            int total = 0;
            List<Coords> coords = new List<Coords>();
            int[,] vectors = { 
                { 1, 0},   // left to right
                { -1, 0},   // right to left
                { 0, 1},    // top to bottom
                { 0, -1},   // bottom to top
                { 1, 1},    // diagonal top left to bottom right
                { -1, 1},   // diagonal top right to bottom left
                { 1, -1},   // diagonal bottom left to top right
                { -1, -1}   // diagonal bottom right to top left
            };
            
            // find possible start pos
            for(int row = 0; row < data.Length; row++)
            {
                for(int col = 0; col < data[row].Length; col++)
                {
                    
                    Coords c = new Coords(row, col, data, "XMAS");

                    for(int i = 0; i < vectors.Length/2; i++)
                    {
                        if(c.Check(vectors[i,0], vectors[i,1]))
                        {
                            coords.Add(c);
                            total++;
                            Log($"XMAS Found at ({col}, {row}) with vector [{c.diffX}, {c.diffY}]");
                        }
                    }
                        
                }
            }
            
            return $"{total}";
        }

        public override string PartB()
        {
            int total = 0;
            int[,] vectors = {
                { 1, 1},    // diagonal top left to bottom right
                { -1, 1},   // diagonal top right to bottom left
                { 1, -1},   // diagonal bottom left to top right
                { -1, -1}   // diagonal bottom right to top left
            };
            for (int row = 0; row < data.Length; row++)
            {
                for(int col = 0; col < data[0].Length; col++)
                {
                    if (data[row][col] == 'A')
                    {
                        int countM = 0;
                        int countS = 0;
                        for(int i = 0; i < vectors.Length/2;i++)
                        {
                            int r = row + vectors[i,1];
                            int c = col + vectors[i,0];
                            if (r >= data.Length || r < 0 || c >= data[0].Length || c < 0)
                                break;
                            if (data[r][c] == 'M')
                                countM++;
                            if (data[r][c] == 'S')
                                countS++;
                        }
                        if(countM == 2 && countS == 2)
                        {
                            // check for MAM or SAS
                            if (data[row-1][col - 1] == data[row + 1][col + 1])
                            {
                                Log($"Ignoring MAM or SAS at ({col},{row})");
                            } else
                            {
                                total++;
                                Log($"X-MAS found at ({col},{row})");
                            }
                            
                        }
                    }
                }
            }
            return $"{total}";
        }
    }
}
