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
        public bool debug;

        public void Log(string message)
        {
            if (debug) Console.WriteLine(message);
        }
    }
}
