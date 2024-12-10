namespace Ato.AoC2024.Day3;

public class MullItOver : IProblem
{
    public string Solve1(string input)
    {
        var mulSum = input
            .Split("mul(")
            .Sum(ParseMul);

        return mulSum.ToString();
    }

    public string Solve2(string input)
    {
        var enabledMulSum = input
            .Split("do()")
            .SelectMany(x => x.Split("don't()")[0].Split("mul("))
            .Sum(ParseMul);

        return enabledMulSum.ToString();
    }

    private static int ParseMul(string input) 
        => input switch
        {
            [var x1, ',', var y1, ')', .. _] 
                when int.TryParse([x1], out var intx) && int.TryParse([y1], out var inty) => intx * inty,
            [var x1, var x2, ',', var y1, ')', .. _] 
                when int.TryParse([x1, x2], out var intx) && int.TryParse([y1], out var inty) => intx * inty,
            [var x1, var x2, var x3, ',', var y1, ')', .. _] 
                when int.TryParse([x1, x2, x3], out var intx) && int.TryParse([y1], out var inty) => intx * inty,
            [var x1, ',', var y1, var y2, ')', .. _] 
                when int.TryParse([x1], out var intx) && int.TryParse([y1, y2], out var inty) => intx * inty,
            [var x1, var x2, ',', var y1, var y2, ')', .. _] 
                when int.TryParse([x1, x2], out var intx) && int.TryParse([y1, y2], out var inty) => intx * inty,
            [var x1, var x2, var x3, ',', var y1, var y2, ')', .. _] 
                when int.TryParse([x1, x2, x3], out var intx) && int.TryParse([y1, y2], out var inty) => intx * inty,
            [var x1, ',', var y1, var y2, var y3, ')', .. _] 
                when int.TryParse([x1], out var intx) && int.TryParse([y1, y2, y3], out var inty) => intx * inty,
            [var x1, var x2, ',', var y1, var y2, var y3, ')', .. _] 
                when int.TryParse([x1, x2], out var intx) && int.TryParse([y1, y2, y3], out var inty) => intx * inty,
            [var x1, var x2, var x3, ',', var y1, var y2, var y3, ')', .. _] 
                when int.TryParse([x1, x2, x3], out var intx) && int.TryParse([y1, y2, y3], out var inty) => intx * inty,
            _ => 0,
        };
}
