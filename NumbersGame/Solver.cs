namespace NumbersGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Solver
    {
        public static List<Dictionary<Attempt, int>> Solve(int target, List<int> numbers)
        {
            Attempt attempt = new(new List<Dictionary<(int, int), Operation>>());
            List<Dictionary<Attempt, int>> raw = GetNumbers(attempt, numbers);

            int closest = raw.Select(i => i.Values.First()).Aggregate((x, y) => Math.Abs(x - target) < Math.Abs(y - target) ? x : y);

            List<Dictionary<Attempt, int>> results = raw.Where(i => i.Values.First() == closest).ToList();

            results.OrderBy(i => i.Keys.First().Results.Count);

            return results;
        }

        public static List<Dictionary<Attempt, int>> GetNumbers(Attempt attempt, List<int> numbers)
        {
            List<Dictionary<Attempt, int>> result = new();

            if (numbers.Count == 1)
            {
                Dictionary<Attempt, int> info = new();
                info.Add(attempt, numbers[0]);

                result.Add(info);

                return result;
            }

            foreach ((int i, int j) in Tuplelise(numbers))
            {
                foreach (Operation operation in (Operation[])Enum.GetValues(typeof(Operation)))
                {
                    Attempt child = new(attempt);

                    int? newnumber = null;
                    bool success = true;
                    switch (operation)
                    {
                        case Operation.Addition:
                            newnumber = i + j;
                            break;
                        case Operation.Subtraction:
                            newnumber = i - j;
                            break;
                        case Operation.Multiplication:
                            newnumber = i * j;
                            break;
                        case Operation.Division:
                            if (j == 0 || i % j != 0)
                            {
                                success = false;
                            }
                            else
                            {
                                newnumber = i / j;
                            }
                            break;
                    }

                    if (success && newnumber != null)
                    {
                        child.AddResult(i, j, operation);
                        List<int> numbersChild = numbers.ToList();
                        numbersChild.Remove(i);
                        numbersChild.Remove(j);
                        numbersChild.Add((int)newnumber);

                        result.AddRange(GetNumbers(child, numbersChild));
                    }
                }
            }

            return result;
        }

        public static List<Tuple<int, int>> Tuplelise(List<int> numbers)
        {
            return (from item in numbers
                    from item2 in numbers
                    where item < item2
                    select new Tuple<int, int>(item, item2)).Union(
                        from item in numbers
                        from item2 in numbers
                        where item != item2 && ((item2 != 0 && item % item2 == 0) || (item != 0 && item2 % item == 0))
                        select new Tuple<int, int>(item, item2))
                    .Distinct().ToList();
        }
    }
}
