namespace NumbersGame
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents an attempt at a problem.
    /// </summary>
    public struct Attempt
    {
        public List<Dictionary<(int, int), Operation>> Results { get; set; }

        public Attempt(List<Dictionary<(int, int), Operation>> results)
        {
            this.Results = new();
        }

        public Attempt(Attempt attempt)
        {
            this.Results = attempt.Results.ToList();
        }

        public void AddResult(int i, int j, Operation operation)
        {
            Dictionary<(int, int), Operation> add = new();
            add.Add((i, j), operation);
            this.Results.Add(add);
        }

        public override string ToString()
        {
            string result = string.Empty;

            foreach (Dictionary<(int, int), Operation> elem in this.Results)
            {
                result += $"{elem.Keys.First()}, {elem.Values.First()}\n";
            }

            return result;
        }
    }
}
