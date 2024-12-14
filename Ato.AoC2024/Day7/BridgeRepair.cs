namespace Ato.AoC2024.Day7;

public class BridgeRepair : IProblem
{
    public string Solve1(string input)
    {
        var calibrationEquations = input
            .Split(Environment.NewLine)
            .Select(x => (x.Split([':'])[0], (x.Split([' ']).Skip(1))))
            .Select(x => (long.Parse(x.Item1), x.Item2.Select(long.Parse).ToArray()));

        var validCalibrationsSum = calibrationEquations
            .Where(x => HasSolution(x.Item1, x.Item2))
            .Sum(x => x.Item1);

        return validCalibrationsSum.ToString();
    }

    public string Solve2(string input)
    {
        var calibrationEquations = input
            .Split(Environment.NewLine)
            .Select(x => (x.Split([':'])[0], (x.Split([' ']).Skip(1))))
            .Select(x => (long.Parse(x.Item1), x.Item2.Select(long.Parse).ToArray()));

        var validCalibrationsSum = calibrationEquations
            .Where(x => HasSolutionWithConcatenation(x.Item1, x.Item2))
            .Sum(x => x.Item1);

        return validCalibrationsSum.ToString();
    }

    private static bool HasSolution(long target, long[] elements) 
        => elements switch
        {
            [var first, var second, .. var rest] when first <= target
                => HasSolution(target, [first*second, .. rest]) 
                || HasSolution(target, [first+second, .. rest]),
            [var last] => target == last,
            _ => false
        };

    private static bool HasSolutionWithConcatenation(long target, long[] elements) 
        => elements switch
        {
            [var first, var second, .. var rest] when first <= target
                => HasSolutionWithConcatenation(target, [long.Parse($"{first}{second}"), .. rest])
                || HasSolutionWithConcatenation(target, [first*second, .. rest]) 
                || HasSolutionWithConcatenation(target, [first+second, .. rest]),
            [var last] => target == last,
            _ => false
        };
}
