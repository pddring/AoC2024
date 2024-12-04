using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2024
{
    internal class Day3 : Day
    {
        public Day3()
        {
            data = new string[] { File.ReadAllText("data.txt")};
        }
        public override string PartA()
        {
            int total = 0;
            Log($"{data[0]}");
            foreach(Match m in Regex.Matches(data[0], @"mul\((\d+),(\d+)\)"))
            {
                int a = int.Parse(m.Groups[1].Value);
                int b = int.Parse(m.Groups[2].Value);
                int result = a * b;
                total += result;
                Log($"mul({a},{b}) = {result}");
            }
            Log($"Total: {total}");
            return total.ToString();
        }

        public override string PartB()
        {
            int total = 0;
            Log($"{data[0]}");
            bool enabled = true;
            foreach (Match m in Regex.Matches(data[0], @"(mul|do|don't)\(((\d+),(\d+))?\)"))
            {
                string opcode = m.Groups[1].Value;
                switch(opcode)
                {
                    case "mul":
                        int a = int.Parse(m.Groups[3].Value);
                        int b = int.Parse(m.Groups[4].Value);
                        int result = a * b;
                        if (enabled)
                        {
                            total += result;
                            Log($"mul({a},{b}) = {result}");
                        } else
                        {
                            Log($"Ignoring mul({a},{b}) = {result} because of don't instruction");
                        }
                        break;
                    case "do":
                        enabled = true;
                        Log($"Enabling multiplier");
                        break;
                    case "don't":
                        enabled = false;
                        Log($"Disabling multiplier");
                        break;
                    default:
                        Log($"Invalid opcode: {opcode}");
                        break;
                }
                
            }
            Log($"Total: {total}");
            return total.ToString();
        }
    }
}
