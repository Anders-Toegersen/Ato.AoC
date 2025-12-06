namespace Ato.AoC2025.Day04;

internal class PrintingDepartment : IProblem
{
    public string Solve1(string input)
    {
        var map = input
            .Split(Environment.NewLine)
            .Select(x => x.ToCharArray())
            .ToArray();

        var accessableRolls = 0;

        for (var row = 0; row < map.Length; row++)
        {
            for (var col = 0; col < map[row].Length; col++)
            {
                if (map[row][col] is '.')
                {
                    continue;
                }

                var neighbourRolls = Directions
                    .Select(dir => (row: row + dir.row, col: col + dir.col))
                    .Where(pos => IsValid(map, pos))
                    .Select(pos => map[pos.row][pos.col] == '@' ? 1 : 0)
                    .Sum();

                if (neighbourRolls < 4)
                {
                    accessableRolls++;
                }
            }
        }

        return accessableRolls.ToString();
    }

    public string Solve2(string input)
    {
        var map = input
            .Split(Environment.NewLine)
            .Select(x => x.ToCharArray())
            .ToArray();

        var changesMade = true;

        while(changesMade)
        {
            var currentChanges = 0;
            for (var row = 0; row < map.Length; row++)
            {
                for (var col = 0; col < map[row].Length; col++)
                {
                    if (map[row][col] is '.' or 'x')
                    {
                        continue;
                    }

                    var neighbourRolls = Directions
                        .Select(dir => (row: row + dir.row, col: col + dir.col))
                        .Where(pos => IsValid(map, pos))
                        .Select(pos => map[pos.row][pos.col] == '@' ? 1 : 0)
                        .Sum();

                    if (neighbourRolls < 4)
                    {
                        map[row][col] = 'x';
                        currentChanges++;
                    }
                }
            }
            changesMade = currentChanges > 0;
        }

        return map
            .SelectMany(x => x)
            .Count(x => x == 'x')
            .ToString();
    }

    private static (int row, int col)[] Directions 
        => [
            (-1, -1),
            (-1, 0),
            (-1, 1),
            (0, -1),
            (0, 1),
            (1, -1),    
            (1, 0),
            (1, 1),
        ];

    private static bool IsValid(char[][] map, (int row, int col) pos) 
        => pos.row >= 0 && pos.row < map.Length && pos.col >= 0 && pos.col < map[pos.row].Length;
}
