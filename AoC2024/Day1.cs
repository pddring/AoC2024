using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2024
{
    internal class Day1: Day
    {

        public Day1(int logLevel=1)
        {
            this.logLevel = logLevel;
            data = File.ReadAllLines("data.txt");
        }

        public override string PartA()
        {
            List<int> Left = new List<int>();
            List<int> Right = new List<int>();

            foreach(string line in data)
            {
                Match m = Regex.Match(line, @"(\d+)   (\d+)");
                int l = int.Parse(m.Groups[1].Value);
                Left.Add(l);
                int r = int.Parse(m.Groups[2].Value);
                Right.Add(r);

                Log($"{line}: found {l} and {r}");

            }

            Log($"Sorting");
            Left.Sort();
            Right.Sort();

            int total = 0;
            for(int i = 0; i < Left.Count; i++)
            {
                int diff = Math.Abs(Left[i] - Right[i]);
                total = total + diff;
                Log($"Pos {i}: diff = |{Left[i]} - {Right[i]}| = {diff}");
            }

            Log($"Total: {total}");

            return total.ToString();
        }

        public override string PartB()
        {
            List<int> Left = new List<int>();
            Dictionary<int,int> Right = new Dictionary<int, int>();
            foreach (string line in data)
            {
                Match m = Regex.Match(line, @"(\d+)   (\d+)");
                int l = int.Parse(m.Groups[1].Value);
                Left.Add(l);
                int r = int.Parse(m.Groups[2].Value);
                if (Right.ContainsKey(r))
                {
                    Right[r]++;
                } else
                {
                    Right.Add(r, 1);
                }

                Log($"{line}: found {l} and {r}");

            }

            Log($"Calculating");

            int total = 0;
            for (int i = 0; i < Left.Count; i++)
            {
                int l = Left[i];
                
                int r = Right.ContainsKey(l)?Right[l]:0;
                int score = l * r;
                total = total + score;
                Log($"Pos {i}: {l} appears {r} times: score = {score}");
            }

            Log($"Total: {total}");

            return total.ToString();
        }
    }
}
