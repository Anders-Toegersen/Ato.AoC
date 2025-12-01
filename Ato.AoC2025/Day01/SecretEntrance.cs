namespace Ato.AoC2025.Day01;

internal class SecretEntrance : IProblem
{
    public string Solve1(string input)
    {
        var state = (Position: 50, LeftAtZeroCount: 0);

        var endState = input
            .Split(Environment.NewLine)
            .Select(y => (Direction: y[0], Change: int.Parse(y[1..])))
            .Aggregate(
                state,
                (x, y) =>
                {
                    x.Position = y.Direction == 'R' 
                        ? x.Position + y.Change
                        : x.Position - y.Change;

                    x.Position = ((x.Position % 100) + 100) % 100;

                    if (x.Position == 0)
                    {
                        x.LeftAtZeroCount++;
                    }
                    
                    return x;
                });

        return endState.ToString();
    }

    public string Solve2(string input)
    {
        var state = (Position: 50, PassedZeroCount: 0);

        var endState = input
            .Split(Environment.NewLine)
            .Select(y => (Direction: y[0], Change: int.Parse(y[1..])))
            .Aggregate(
                state,
                (x, y) =>
                {
                    var startedAtZero = x.Position == 0;

                    x.Position = y.Direction == 'R'
                        ? x.Position + y.Change
                        : x.Position - y.Change;

                    x.PassedZeroCount += Math.Abs(x.Position / 100);

                    if (x.Position <= 0 && !startedAtZero)
                    {
                        x.PassedZeroCount++;
                    }

                    x.Position = ((x.Position % 100) + 100) % 100;

                    return x;
                });

        return endState.ToString();
    }
}
