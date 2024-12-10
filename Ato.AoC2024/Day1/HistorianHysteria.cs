namespace Ato.AoC2024.Day1;

public class HistorianHysteria : IProblem
{
    public string Solve1(string input)
    {
        var lines = input
            .Split(Environment.NewLine)
            .Select(x => x.Split("   ").Select(int.Parse));

        var left = lines
            .Select(x => x.First())
            .Order();

        var right = lines
            .Select(x => x.Last())
            .Order();

        var pairwiseSummmedDistances = left
            .Zip(right)
            .Sum(x => Math.Abs(x.First - x.Second));

        return pairwiseSummmedDistances.ToString();
    }

    public string Solve2(string input)
    {
        var lines = input
            .Split(Environment.NewLine)
            .Select(x => x.Split("   ").Select(int.Parse));

        var rightByCount = lines
            .Select(x => x.Last())
            .GroupBy(x => x)
            .ToDictionary(x => x.Key, x => x.Count());

        var similarityScore = lines
            .Select(x => x.First())
            .Sum(x => rightByCount.GetValueOrDefault(x) * x);

        return similarityScore.ToString();
    }
}
