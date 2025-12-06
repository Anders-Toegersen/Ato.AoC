using System.Linq;

namespace Ato.AoC2025.Day06;

internal class TrashCompactor : IProblem
{
    public string Solve1(string input)
    {
        var problemMap = input
            .Split(Environment.NewLine)
            .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            .ToArray();

        var resultSum = 0L;
        for (int column = 0; column < problemMap.Last().Length; column++)
        {
            var result = 0L;
            var operation = problemMap.Last()[column];

            for (int row = 0; row < problemMap.Length - 1; row++)
            {
                var value = int.Parse(problemMap[row][column]);

                if (operation == "+")
                {
                    result += value;
                }
                else if (operation == "*")
                {
                    result = row == 0 ? value : result * value;
                }
            }

            resultSum += result;
        }

        return resultSum.ToString();
    }

    public string Solve2(string input)
    {
        var charMap = input
            .Split(Environment.NewLine)
            .Select(line => line.ToCharArray())
            .ToArray();

        var transposedMap = new char[charMap[1].Length][];
        for (var column = 0; column < charMap[1].Length; column++)
        {
            transposedMap[column] = new char[charMap.Length];
            for (var row = 0; row < charMap.Length; row++)
            {
                transposedMap[column][row] = charMap[row][column];
            }
        }

        var problemMap = transposedMap
            .Aggregate(
                (Results: new List<long>(), Sign: ' '),
                (x, y) =>
                {
                    var current = y.Last();

                    if (current == '+')
                    {
                        x.Results.Add(0);
                        x.Sign = current;
                    }
                    else if (current == '*')
                    {
                        x.Results.Add(1);
                        x.Sign = current;
                    }

                    if (int.TryParse(y.AsSpan()[..^1], out var value))
                    {
                        if (x.Sign == '+')
                        {
                            x.Results[^1] += value;
                        }
                        else if (x.Sign == '*')
                        {
                            x.Results[^1] *= value;
                        }
                    }

                    return x;
                });

        return problemMap
            .Results
            .Sum()
            .ToString();
    }
}
