namespace Ato.AoC2025.Day02;

internal class GiftShop : IProblem
{
    public string Solve1(string input)
    {
        var invalidIdsSum = input
            .Split(',')
            .Select(range => range.Split('-'))
            .SelectMany(limits => 
            {
                var list = new List<string>();
                for (var i = long.Parse(limits[0]); i <= long.Parse(limits[1]); i++)
                {
                    list.Add(i.ToString());
                }
                return list;
            })
            .Select(id =>
            {
                var middle = id.Length / 2;
                var sequence1 = id[0..middle];
                var sequence2 = id[middle..];

                if (sequence1 == sequence2)
                {
                    return long.Parse(id);
                }

                return 0;
            })
            .Sum();

        return invalidIdsSum.ToString();
    }

    public string Solve2(string input)
    {
        var invalidIdsSum = input
            .Split(',')
            .Select(range => range.Split('-'))
            .SelectMany(limits =>
            {
                var list = new List<string>();
                for (var i = long.Parse(limits[0]); i <= long.Parse(limits[1]); i++)
                {
                    list.Add(i.ToString());
                }
                return list;
            })
            .Select(id =>
            {
                for (var patternLength = 1; patternLength <= id.Length / 2; patternLength++)
                {
                    if (id.Length % patternLength != 0)
                    {
                        continue;
                    }

                    var success = false;
                    var sequence1 = id[0..patternLength];

                    for (var patternIndex = patternLength; patternIndex < id.Length; patternIndex += patternLength)
                    {
                        var sequence2 = id[patternIndex..(patternIndex + patternLength)];

                        if (sequence1 != sequence2)
                        {
                            success = false;
                            break;
                        }

                        success = true;
                    }

                    if (success)
                    {
                        return long.Parse(id);
                    }
                }

                return 0;
            })
            .Sum();

        return invalidIdsSum.ToString();
    }
}
