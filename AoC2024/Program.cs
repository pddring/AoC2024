namespace AoC2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Day2 solver = new Day2();
            Console.WriteLine("__________                __       _____       \r\n\\______   \\_____ ________/  |_    /  _  \\   /\\ \r\n |     ___/\\__  \\\\_  __ \\   __\\  /  /_\\  \\  \\/ \r\n |    |     / __ \\|  | \\/|  |   /    |    \\ /\\ \r\n |____|    (____  /__|   |__|   \\____|__  / \\/ \r\n                \\/                      \\/     ");
            Console.WriteLine($"Part A: {solver.PartA(2)}");
            
            
            Console.WriteLine("__________                __    __________     \r\n\\______   \\_____ ________/  |_  \\______   \\ /\\ \r\n |     ___/\\__  \\\\_  __ \\   __\\  |    |  _/ \\/ \r\n |    |     / __ \\|  | \\/|  |    |    |   \\ /\\ \r\n |____|    (____  /__|   |__|    |______  / \\/ \r\n                \\/                      \\/     ");
            Console.WriteLine($"Part B: {solver.PartB(2)}");
            
        }
    }
}
