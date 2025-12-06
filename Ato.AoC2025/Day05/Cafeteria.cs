namespace Ato.AoC2025.Day05;

internal class Cafeteria : IProblem
{
    public string Solve1(string input)
    {
        var freshIds = input
            .Split(Environment.NewLine + Environment.NewLine)[0]
            .Split(Environment.NewLine)
            .Select(line => line.Split('-'))
            .Select(limits => (Lower: long.Parse(limits[0]), Upper: long.Parse(limits[1])));

        var rottenIdsCount = input
            .Split(Environment.NewLine + Environment.NewLine)[1]
            .Split(Environment.NewLine)
            .Select(long.Parse)
            .Where(id => freshIds.Any(range => id >= range.Lower && id <= range.Upper))
            .Count();

        return rottenIdsCount.ToString();
    }

    public string Solve2(string input)
    {
        var freshIdsCount = input
            .Split(Environment.NewLine + Environment.NewLine)[0]
            .Split(Environment.NewLine)
            .Select(line => line.Split('-'))
            .Select(limits => (Lower: long.Parse(limits[0]), Upper: long.Parse(limits[1])))
            .OrderBy(range => range.Lower)
            .Aggregate(
                new List<(long Lower, long Upper)>(), 
                (mergedRanges, range) =>
                {
                    if (mergedRanges.Count == 0 || 
                        mergedRanges[^1].Upper < range.Lower)
                    {
                        mergedRanges.Add(range);
                        return mergedRanges;
                    }

                    mergedRanges[^1] = (mergedRanges[^1].Lower, Math.Max(mergedRanges[^1].Upper, range.Upper));

                    return mergedRanges;
                })
            .Select(x => x.Upper - x.Lower + 1)
            .Sum();

        return freshIdsCount.ToString();
    }
}
