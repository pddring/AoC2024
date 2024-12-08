using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2024
{
    internal class Day7 : Day
    {
        public class Equation
        {
            public long target;
            public List<long> numbers = new List<long>();
            
        }

        public List<long> Calc(Equation e, int i, List<long> startValues)
        {
            List<long> values = new List<long>();
            foreach (long start in startValues)
            {
                values.Add(start + e.numbers[i + 1]);
                values.Add(start * e.numbers[i + 1]);
            }
            return values;
        }

        public List<long> CalcB(Equation e, int i, List<long> startValues)
        {
            List<long> values = new List<long>();
            foreach (long start in startValues)
            {
                values.Add(start + e.numbers[i + 1]);
                values.Add(start * e.numbers[i + 1]);
                values.Add(long.Parse($"{start}{e.numbers[i+1]}"));
            }
            return values;
        }
        public override string PartA()
        {
            long total = 0;
            foreach(string line in data)
            {
                Match m = Regex.Match(line, @"(\d+): (.*)");
                Equation e = new Equation();
                if (m.Success)
                {
                    
                    e.target = long.Parse(m.Groups[1].Value);
                    foreach(Match m2 in Regex.Matches(m.Groups[2].Value, @"\d+"))
                    {
                        e.numbers.Add(int.Parse(m2.Value));
                    }
                }

                List<long> values = new List<long>() { e.numbers[0] };

                for (int i = 0; i < e.numbers.Count - 1; i++)
                {
                    values = Calc(e, i, values);
                }
               
                if(values.Contains(e.target))
                {
                    total += e.target;
                }
                

            }
            return $"{total}";
        }

        public override string PartB()
        {
            long total = 0;
            foreach (string line in data)
            {
                Match m = Regex.Match(line, @"(\d+): (.*)");
                Equation e = new Equation();
                if (m.Success)
                {

                    e.target = long.Parse(m.Groups[1].Value);
                    foreach (Match m2 in Regex.Matches(m.Groups[2].Value, @"\d+"))
                    {
                        e.numbers.Add(int.Parse(m2.Value));
                    }
                }

                List<long> values = new List<long>() { e.numbers[0] };

                for (int i = 0; i < e.numbers.Count - 1; i++)
                {
                    values = CalcB(e, i, values);
                }

                if (values.Contains(e.target))
                {
                    total += e.target;
                }


            }
            return $"{total}";
        }
    }
}
