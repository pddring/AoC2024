namespace AoC2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Day solver = new Day5();
            Console.WriteLine(Day.PART_A_MESSAGE);
            Console.WriteLine($"Part A: {solver.PartA()}");
            
            
            Console.WriteLine(Day.PART_B_MESSAGE);
            Console.WriteLine($"Part B: {solver.PartB()}");
            
        }
    }
}
