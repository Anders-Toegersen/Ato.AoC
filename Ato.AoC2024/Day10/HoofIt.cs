using System.Threading.Tasks.Sources;

namespace Ato.AoC2024.Day10;

public class HoofIt : IProblem
{
    public string Solve1(string input)
    {
        var map = input
            .Split(Environment.NewLine)
            .Select(x => x.Select(x => Convert.ToInt32(x - '0')).ToArray())
            .ToArray();

        var trailheads = new List<(int row, int col, int score)>();
        for (int row = 0; row < map.Length; row++)
        {
            for (int col = 0; col < map[row].Length; col++)
            {
                if (map[row][col] == 0)
                {
                    trailheads.Add((row, col, CalculateTrailheadScore(map, row, col)));
                }
            }
        }

        var drawHeads = trailheads.Select(x => $"[{x.row},{x.col}]: {x.score}");

        return string.Join(Environment.NewLine, drawHeads);
    }

    public string Solve2(string input)
    {
        return string.Empty;
    }

    private static int CalculateTrailheadScore(int[][] map, int row, int col) 
        => AllDirections
            .Select(dir => (map[row][col], WalkToHeight(map, row + dir.row, col + dir.col)) switch
            {
                (8, 9) => 1,
                (var from, var to) when from + 1 == to => CalculateTrailheadScore(map, row + dir.row, col + dir.col),
                _ => 0,
            })
            .Sum();

    private static (int row, int col)[] AllDirections => 
        [
            (-1, 0),
            (0, -1),
            (1, 0),
            (0, 1),
        ];

    private static int? WalkToHeight(int[][] map, int row, int col)
        => row >= 0
            && row < map.Length
            && col >= 0
            && col < map[row].Length
        ? map[row][col]
        : null;
}
