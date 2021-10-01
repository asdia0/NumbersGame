namespace NumbersGameConsole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NumbersGame;

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Pick a target number.");
            int target = int.Parse(Console.ReadLine());
            Console.WriteLine("Choose 6 numbers as your tiles. Separate them with a space.");
            List<int> numbers = new();
            foreach (string tileS in Console.ReadLine().Split(" "))
            {
                numbers.Add(int.Parse(tileS));
            }

            List<Dictionary<Attempt, int>> results = Solver.Solve(target, numbers);

            for (int i = 0; i < Math.Min(results.Count, 10); i++)
            {
                Console.WriteLine($"{results[i].Values.First()}\n{results[i].Keys.First()}");
            }
        }
    }
}
