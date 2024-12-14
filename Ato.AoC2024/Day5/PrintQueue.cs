namespace Ato.AoC2024.Day5;

public class PrintQueue : IProblem
{
    public string Solve1(string input)
    {
        var inputs = input.Split("\r\n\r\n");

        var validBefore = inputs[0]
            .Split("\r\n")
            .Select(x => x.Split('|'))
            .ToLookup(x => x.First(), x => x.Last());

        var validAfter = inputs[0]
            .Split("\r\n")
            .Select(x => x.Split('|'))
            .ToLookup(x => x.Last(), x => x.First());

        var updates = inputs[1]
            .Split("\r\n")
            .Select(x => x.Split(','));

        return updates
            .Where(x => IsValid(validBefore, validAfter, x))
            .Sum(MiddlePageNumber)
            .ToString();
    }

    public string Solve2(string input)
    {
        var inputs = input.Split("\r\n\r\n");

        var validBefore = inputs[0]
            .Split("\r\n")
            .Select(x => x.Split('|'))
            .ToLookup(x => x.First(), x => x.Last());

        var validAfter = inputs[0]
            .Split("\r\n")
            .Select(x => x.Split('|'))
            .ToLookup(x => x.Last(), x => x.First());

        var updates = inputs[1]
            .Split("\r\n")
            .Select(x => x.Split(','));

        return updates
            .Where(x => !IsValid(validBefore, validAfter, x))
            .Select(x => x
                .Order(Comparer<string>.Create((x, y) => OrderUpdates(validBefore, x, y)))
                .ToArray())
            .Sum(MiddlePageNumber)
            .ToString();
    }

    private static bool IsValid(
        ILookup<string, string> before, 
        ILookup<string, string> after, 
        string[] update)
    {
        for (int i = 1; i < update.Length - 1; i++)
        {
            var page = update[i];

            if (before[page].Any(x => update.Take(i).Contains(x)))
            {
                return false;
            }

            if (after[page].Any(x => update.Skip(i + 1).Contains(x)))
            {
                return false;
            }
        }

        return true;
    }

    private int MiddlePageNumber(string[] update) 
        => int.Parse(update[update.Length / 2]);

    private static int OrderUpdates(
        ILookup<string, string> before,
        string x,
        string y)
    {
        if (before[x].Contains(y))
        {
            return -1;
        }

        if (before[y].Contains(x))
        {
            return 1;
        }

        return 0;
    }
}
