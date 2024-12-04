using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024
{
    internal abstract class Day
    {
        public string[] data;
        public const string PART_A_MESSAGE = "__________                __       _____       \r\n\\______   \\_____ ________/  |_    /  _  \\   /\\ \r\n |     ___/\\__  \\\\_  __ \\   __\\  /  /_\\  \\  \\/ \r\n |    |     / __ \\|  | \\/|  |   /    |    \\ /\\ \r\n |____|    (____  /__|   |__|   \\____|__  / \\/ \r\n                \\/                      \\/     ";
        public const string PART_B_MESSAGE = "__________                __    __________     \r\n\\______   \\_____ ________/  |_  \\______   \\ /\\ \r\n |     ___/\\__  \\\\_  __ \\   __\\  |    |  _/ \\/ \r\n |    |     / __ \\|  | \\/|  |    |    |   \\ /\\ \r\n |____|    (____  /__|   |__|    |______  / \\/ \r\n                \\/                      \\/     ";
        
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

        public Day()
        {
            data = File.ReadAllLines("data.txt");
        }

        public abstract string PartA();
        public abstract string PartB();
    }
}
