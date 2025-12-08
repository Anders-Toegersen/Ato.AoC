namespace Ato.AoC2025.Day07;

internal class Laboratories : IProblem
{
    public string Solve1(string input)
    {
        var map = input
            .Split(Environment.NewLine)
            .Select(line => line.ToCharArray())
            .ToArray();

        var count = 0;

        for (int row = 0; row < map.Length; row++)
        {
            for (int col = 0; col < map[row].Length; col++)
            {
                if (IsValid(map, (row - 1, col)) && map[row - 1][col] is 'S' or '|')
                {
                    if (map[row][col] == '.')
                    {
                        map[row][col] = '|';
                    }
                    else if (map[row][col] == '^')
                    {
                        count++;

                        if (IsValid(map, (row, col - 1)))
                        {
                            map[row][col - 1] = '|';
                        }

                        if (IsValid(map, (row, col + 1)))
                        {
                            map[row][col + 1] = '|';
                        }
                    }
                }
            }
        }

        return count.ToString();
    }

    public string Solve2(string input)
    {
        var map = input
                    .Split(Environment.NewLine)
                    .Select(line => line.ToCharArray())
                    .Select(x => x.Select(c => c switch
                    {
                        'S' => 1L,
                        '^' => -1L,
                        _ => 0L
                    }).ToArray())
                    .ToArray();

        for (int row = 0; row < map.Length; row++)
        {
            for (int col = 0; col < map[row].Length; col++)
            {
                if (IsValid(map, (row - 1, col)) && map[row - 1][col] is > 0)
                {
                    if (map[row][col] > -1)
                    {
                        map[row][col] += map[row - 1][col];
                    }
                    else if (map[row][col] == -1)
                    {
                        if (IsValid(map, (row, col - 1)))
                        {
                            map[row][col - 1] += map[row - 1][col];
                        }

                        if (IsValid(map, (row, col + 1)))
                        {
                            map[row][col + 1] += map[row - 1][col];
                        }
                    }
                }
            }
        }

        var LastRowTachyonCount = map[map.Length - 1].Aggregate(0L, (acc, val) => acc + val);

        return LastRowTachyonCount.ToString();
    }

    private static bool IsValid(char[][] map, (int row, int col) pos)
        => pos.row >= 0 && pos.row < map.Length && pos.col >= 0 && pos.col < map[pos.row].Length;

    private static bool IsValid(long[][] map, (int row, int col) pos)
        => pos.row >= 0 && pos.row < map.Length && pos.col >= 0 && pos.col < map[pos.row].Length;

}
