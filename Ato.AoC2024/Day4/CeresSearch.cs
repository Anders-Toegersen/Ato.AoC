namespace Ato.AoC2024.Day4;

public class CeresSearch : IProblem
{
    public string Solve1(string input)
    {
        char[][] fields = input
            .Split(Environment.NewLine)
            .Select(x => x.ToCharArray())
            .ToArray();

        var xmasCount = 0;
        for ( int i = 0; i < fields.Length; i++)
        {
            for (int j = 0; j < fields[i].Length; j++)
            {
                if (fields[i][j] == 'X')
                {
                    xmasCount += AllDirections.Count(x => SearchXmas(fields, (i, j), x));
                }
            }
        }

        return xmasCount.ToString();
    }

    public string Solve2(string input)
    {
        char[][] fields = input
            .Split(Environment.NewLine)
            .Select(x => x.ToCharArray())
            .ToArray();

        var crossMasCount = 0;
        for ( int i = 1; i < fields.Length - 1; i++)
        {
            for (int j = 1; j < fields[i].Length - 1; j++)
            {
                if (fields[i][j] == 'A')
                {
                    crossMasCount += SearchCrossMas(fields, (i, j));
                }
            }
        }

        return crossMasCount.ToString();
    }

    private static bool IsValid(char[][] fields, (int, int) idx) => 
        idx.Item1 >= 0 &&
        idx.Item1 < fields.Length &&
        idx.Item2 >= 0 &&
        idx.Item2 < fields[idx.Item1].Length;

    private static (int, int)[] AllDirections =>
    [
        (-1, -1),
        (-1, 0),
        (-1, 1),
        (0, -1),
        (0, 1),
        (1, -1),
        (1, 0),
        (1, 1),
    ];

    private static bool SearchXmas(
        char[][] fields, 
        (int, int) index,
        (int, int) direction)
    {
        var nextIndex = (index.Item1 + direction.Item1, index.Item2 + direction.Item2);

        if (!IsValid(fields, index) || 
            !IsValid(fields, nextIndex))
        {
            return false;
        }

        return (fields[index.Item1][index.Item2], fields[nextIndex.Item1][nextIndex.Item2]) switch
        {
            ('X','M') => SearchXmas(fields, (nextIndex.Item1, nextIndex.Item2), direction),
            ('M', 'A') => SearchXmas(fields, (nextIndex.Item1, nextIndex.Item2), direction),
            ('A', 'S') => true,
            _ => false,
        };
    }

    private static int SearchCrossMas(
        char[][] fields, 
        (int, int) index)
    {
        return (
            fields[index.Item1 - 1][index.Item2 - 1],
            fields[index.Item1 + 1][index.Item2 + 1],
            fields[index.Item1 + 1][index.Item2 - 1], 
            fields[index.Item1 - 1][index.Item2 + 1]) switch
        {
            ('M', 'S', 'M', 'S') => 1,
            ('M', 'S', 'S', 'M') => 1,
            ('S', 'M', 'S', 'M') => 1,
            ('S', 'M', 'M', 'S') => 1,
            _ => 0,
        };
    }
}
