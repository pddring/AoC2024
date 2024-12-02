using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024
{
    internal class Day
    {
        public string[] data;
        
        public int logLevel = 0;

        public void Log(string message, int level = 1)
        {
            if (level >= logLevel) Console.WriteLine(message);
        }

        public string ListToString(List<int> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach(object item in list)
            {
                sb.Append($"{item} ");
            }
            return sb.ToString();
        }
    }
}
