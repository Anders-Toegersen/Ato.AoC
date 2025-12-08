namespace Ato.AoC2025.Day08;

internal class Playground : IProblem
{
    public string Solve1(string input)
    {
        var junctionBoxes = input
            .Split(Environment.NewLine)
            .Select(x => x.Split(','))
            .Select(x => (X: int.Parse(x[0]), Y: int.Parse(x[1]), Z: int.Parse(x[2])))
            .ToArray();

        var distances = new Dictionary<((int, int, int), (int, int, int)), double>(junctionBoxes.Length * junctionBoxes.Length);

        for (int i = 0; i < junctionBoxes.Length; i++)
        {
            for (int j = i + 1; j < junctionBoxes.Length; j++)
            {
                distances.Add(
                    (junctionBoxes[i], junctionBoxes[j]), 
                    EuclidianDistance(junctionBoxes[i], junctionBoxes[j]));
            }
        }

        var circuits = junctionBoxes
            .Select(x => new HashSet<(int, int, int)> { x })
            .ToList();

        var shortestDistances = distances
            .OrderBy(x => x.Value)
            .Select(x => x.Key)
            .Take(1000);

        foreach ((var from, var to) in shortestDistances)
        {
            if (circuits.Find(x => x.Contains(from)) is { } fromCircuit &&
                circuits.Find(x => x.Contains(to)) is { } toCircuit)
            {
                if (fromCircuit != toCircuit)
                {
                    fromCircuit.UnionWith(toCircuit);
                    circuits.Remove(toCircuit);
                }
            }
        }

        var biggestCircuitsProduct = circuits
            .OrderByDescending(x => x.Count)
            .Take(3)
            .Select(x => x.Count)
            .Aggregate(1L, (acc, val) => acc * val);

        return biggestCircuitsProduct.ToString();
    }

    public string Solve2(string input)
    {
        var junctionBoxes = input
            .Split(Environment.NewLine)
            .Select(x => x.Split(','))
            .Select(x => (X: int.Parse(x[0]), Y: int.Parse(x[1]), Z: int.Parse(x[2])))
            .ToArray();

        var distances = new Dictionary<((int, int, int), (int, int, int)), double>(junctionBoxes.Length * junctionBoxes.Length);

        for (int i = 0; i < junctionBoxes.Length; i++)
        {
            for (int j = i + 1; j < junctionBoxes.Length; j++)
            {
                distances.Add(
                    (junctionBoxes[i], junctionBoxes[j]),
                    EuclidianDistance(junctionBoxes[i], junctionBoxes[j]));
            }
        }

        var circuits = junctionBoxes
            .Select(x => new HashSet<(int, int, int)> { x })
            .ToList();

        var lastConnectedXCoordinatesProduct = 0L;
        foreach(((var from, var to), _) in distances.OrderBy(x => x.Value))
        {
            if (circuits.Find(x => x.Contains(from)) is { } fromCircuit &&
                circuits.Find(x => x.Contains(to)) is { } toCircuit)
            {
                if (fromCircuit != toCircuit)
                {
                    fromCircuit.UnionWith(toCircuit);
                    circuits.Remove(toCircuit);
                }

                if (circuits.Count == 1)
                {
                    lastConnectedXCoordinatesProduct = (long)from.Item1 * to.Item1;
                    break;
                }
            }
        }

        return lastConnectedXCoordinatesProduct.ToString();
    }

    private static double EuclidianDistance((int, int, int) from, (int, int, int) to) 
        => Math.Sqrt(
            Math.Pow(to.Item1 - from.Item1, 2) +
            Math.Pow(to.Item2 - from.Item2, 2) +
            Math.Pow(to.Item3 - from.Item3, 2));
}
