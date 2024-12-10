using System.Data;

namespace Ato.AoC2024.Day6;

public class GuardGallivant : IProblem
{
    public string Solve1(string input)
    {
        var map = input
            .Split(Environment.NewLine)
            .Select(x => x.ToCharArray())
            .ToArray();

        var guard = FindGuard(map);

        return Walk(map, guard.position, guard.direction)
            .path
            .DistinctBy(x => x.Item1)
            .Count()
            .ToString();
    }

    public string Solve2(string input)
    {
        var map = input
            .Split(Environment.NewLine)
            .Select(x => x.ToCharArray())
            .ToArray();

        var guard = FindGuard(map);
        var loopingPaths = 0;

        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] is '.')
                {
                    map[i][j] = 'O';

                    if (Walk(map, guard.position, guard.direction) is { exitsMap: false } walk)
                    {
                        loopingPaths++;
                        //// return DrawPath(map, walk.path);
                    }

                    map[i][j] = '.';
                }
            }
        }

        return loopingPaths.ToString();
    }

    private static ((int, int) position, (int, int) direction) FindGuard(char[][] map)
    {
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == '^')
                {
                    return ((i, j), (-1, 0));
                }
            }
        }

        return ((0, 0), (0, 0));
    }

    private static (HashSet<((int, int), (int, int))> path, bool exitsMap) Walk(
        char[][] map, 
        (int, int) position, 
        (int, int) direction)
    {
        var path = new HashSet<((int, int), (int, int))>();

        while (IsValid(map, position, direction) && path.Add((position, direction)))
        {
            if (map[position.Item1 + direction.Item1][position.Item2 + direction.Item2] is '#' or 'O')
            {
                direction = direction switch
                {
                    (-1, 0) => (0, 1),   // Up -> Right
                    (0, 1) => (1, 0),    // Right -> Down
                    (1, 0) => (0, -1),   // Down -> Left
                    (0, -1) => (-1, 0),  // Left -> Up
                    _ => (0, 0)
                };
            }
            else
            {
                position.Item1 += direction.Item1;
                position.Item2 += direction.Item2;
            }
        }

        return (path, !IsValid(map, position, direction));
    }

    private static bool IsValid(char[][] map, (int, int) position, (int, int) direction)
        => position.Item1 + direction.Item1 >= 0 
        && position.Item1 + direction.Item1 < map.Length 
        && position.Item2 + direction.Item2 >= 0 
        && position.Item2 + direction.Item2 < map[position.Item1].Length;

    private static string DrawPath(char[][] map, HashSet<((int, int), (int, int))> path)
    {
        var drawing = map.Select(x => x.ToArray()).ToArray();

        foreach (var (position, direction) in path)
        {
            drawing[position.Item1][position.Item2] = 
                (drawing[position.Item1][position.Item2], direction) switch
            {
                ('^', _) => '^',
                ('+', _) => '+',
                ('-', (_, 0)) => '+',
                ('|', (0, _)) => '+',
                (_, (_, 0)) => '|',
                (_, (0, _)) => '-',
                (var ch, _) => ch,
            };
        }

        return string.Join(Environment.NewLine, drawing.Select(x => new string(x)));
    }
}
