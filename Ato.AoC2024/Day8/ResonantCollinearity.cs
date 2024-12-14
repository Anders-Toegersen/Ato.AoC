
namespace Ato.AoC2024.Day8;

public class ResonantCollinearity : IProblem
{
    public string Solve1(string input)
    {
        var map = input
            .Split(Environment.NewLine)
            .Select(x => x.ToCharArray())
            .ToArray();

        var antennas = FindAntennas(map);
        var antiNodes = new HashSet<(int, int)>();

        foreach (var frequency in antennas)
        {
            var frequencyAntennas = frequency.ToArray();

            var frequencyAntiNodes = frequencyAntennas
                .SelectMany(x => FindAntiNodes(map, x, frequencyAntennas));

            antiNodes.UnionWith(frequencyAntiNodes);
        }

        return antiNodes.Count.ToString();

        //// return DrawAntiNodes(map, antiNodes) + Environment.NewLine + antiNodes.Count.ToString();
    }

    public string Solve2(string input)
    {
        var map = input
            .Split(Environment.NewLine)
            .Select(x => x.ToCharArray())
            .ToArray();

        var antennas = FindAntennas(map);
        var antiNodes = new HashSet<(int, int)>();

        foreach (var frequency in antennas)
        {
            var frequencyAntennas = frequency.ToArray();

            var frequencyAntiNodes = frequencyAntennas
                .SelectMany(x => FindAntiNodesWithResonantHarmonics(map, x, frequencyAntennas));

            antiNodes.UnionWith(frequencyAntiNodes);
        }

        return antiNodes.Count.ToString();

        //// return DrawAntiNodes(map, antiNodes) + Environment.NewLine + antiNodes.Count.ToString();
    }

    private static ILookup<char, (int i, int j)> FindAntennas(char[][] map) 
        => map
            .SelectMany((r, i) => r.Select((chr, j) => (chr, (i, j))))
            .Where(x => x.chr is not '.')
            .ToLookup(x => x.chr, x => x.Item2);

    private static (int, int)[] FindAntiNodes(
        char[][] map,
        (int row, int col) antenna,
        (int row, int col)[] others)
        => others
            .Where(x => antenna.row != x.row || antenna.col != x.col)
            .Select(x => CalculateAntiNodeDirection(antenna, x))
            .Select(x => (antenna.row + x.row, antenna.col + x.col))
            .Where(x => IsValidAntiNode(map, x))
            .ToArray();

    private static (int row, int col) CalculateAntiNodeDirection(
        (int row, int col) from, 
        (int row, int col) to)
        => (from.row - to.row, from.col - to.col);

    private static bool IsValidAntiNode(
        char[][] map,
        (int row, int col) position)
        => position.row >= 0
        && position.row < map.Length
        && position.col >= 0
        && position.col < map[position.row].Length;

    private static (int, int)[] FindAntiNodesWithResonantHarmonics(
        char[][] map,
        (int row, int col) antenna,
        (int row, int col)[] others)
        => others
            .Where(x => antenna.row != x.row || antenna.col != x.col)
            .Select(x => CalculateAntiNodeDirection(antenna, x))
            .SelectMany(x => CalculateResonantHarmonics(map, antenna, x))
            .ToArray();

    private static IEnumerable<(int, int)> CalculateResonantHarmonics(
        char[][] map, 
        (int row, int col) antiNode, 
        (int row, int col) direction)
    {
        while (IsValidAntiNode(map, antiNode))
        {
            yield return antiNode;
            antiNode = (antiNode.row + direction.row, antiNode.col + direction.col);
        }
    }

    private static string DrawAntiNodes(
        char[][] map, 
        HashSet<(int, int)> antiNodes)
    {
        var drawing = map.Select(x => x.ToArray()).ToArray();

        foreach (var position in antiNodes)
        {
            drawing[position.Item1][position.Item2] = drawing[position.Item1][position.Item2] switch
            {
                '.' => '#',
                var chr => chr,
            };
        }

        return string.Join(Environment.NewLine, drawing.Select(x => new string(x)));
    }
}
