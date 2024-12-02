using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2024
{
    internal class Day2: Day
    {

        public Day2(int logLevel = 1)
        {
            this.logLevel = 1;
            data = File.ReadAllLines("data.txt");
        }

        public override string PartA()
        {
            int safeCount = 0;
            foreach (string line in data)
            {
                List<int> reports = new List<int>();

                foreach (Match m in Regex.Matches(line, @"(\d+)"))
                {
                    int report = int.Parse(m.Groups[1].Value);

                    reports.Add(report);
                }
                Result result = CheckSafety(reports);

                if (!result.allIncreasing && !result.allDecreasing)
                {
                    result.safe = false;
                }

                Log($"{line}: safe: {result.safe} AllInc: {result.allIncreasing} AllDec: {result.allDecreasing}", 2);
                if (result.safe)
                {
                    safeCount++;
                }
            }

            Log($"Total: {safeCount}");

            return safeCount.ToString();
        }

        public class Result
        {
            public bool safe = true;
            public bool allDecreasing = true;
            public bool allIncreasing = true;
        }

        public Result CheckSafety(List<int> reports)
        {
            Result result = new Result();
            for(int i = 1; i < reports.Count; i++)
            {

                int diff = Math.Abs(reports[i] - reports[i-1]);
                if (diff == 0)
                {
                    result.safe = false;
                    return result;
                }
                if (reports[i-1] < reports[i])
                {
                    result.allDecreasing = false;
                }
                if (reports[i-1] > reports[i])
                {
                    result.allIncreasing = false;
                }

                Log($"Difference between {reports[i-1]} and {reports[i]} is {diff}");
                if (diff > 3)
                {
                    result.safe = false;
                    break;
                }
            }

            if (!result.allIncreasing && !result.allDecreasing)
            {
                result.safe = false;
            }
            return result;
        }

        public override string PartB()
        {
            int safeCount = 0;
            foreach (string line in data)
            {
                List<int> reports = new List<int>();
               
                foreach (Match m in Regex.Matches(line, @"(\d+)"))
                {
                    int report = int.Parse(m.Groups[1].Value);

                    reports.Add(report);
                }
                Result result = CheckSafety(reports);

                if(!result.safe)
                {
                    
                    for(int i = 0; i < reports.Count; i++)
                    {
                        List<int> revisedReport = new List<int>(reports);
                        int remove = reports[i];
                        revisedReport.RemoveAt(i);
                        Log($"{line} is unsafe. Trying Problem Dampener: {ListToString(revisedReport)} - removing {remove} at position {i}", 2);
                        result = CheckSafety(revisedReport);
                        if(result.safe)
                        {
                            break;
                        }
                    }
                }

                Log($"{line}: safe: {result.safe} AllInc: {result.allIncreasing} AllDec: {result.allDecreasing}", 2);
                if (result.safe)
                {
                    safeCount++;
                }

            }

            Log($"Total: {safeCount}");

            return safeCount.ToString();
        }
    }
}
