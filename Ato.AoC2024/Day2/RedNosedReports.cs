namespace Ato.AoC2024.Day2;

public class RedNosedReports : IProblem
{
    public string Solve1(string input)
    {
        var reports = input
            .Split(Environment.NewLine)
            .Select(x => x.Split(" ").Select(int.Parse).ToArray());

        var safeReports = reports.Count(IsSafe);

        return safeReports.ToString();
    }

    public string Solve2(string input)
    {
        var reports = input
            .Split(Environment.NewLine)
            .Select(x => x.Split(" ").Select(int.Parse).ToArray());

        var safeReportsWithDamnpner = reports.Count(IsSafeWithDampener);

        return safeReportsWithDamnpner.ToString();
    }

    private static bool IsSafe(int[] report)
    {
        bool? direction = null;
        for (int i = 0; i < report.Length - 1; i++)
        {
            direction ??= report[i] > report[i + 1];
            var diff = Math.Abs(report[i] - report[i + 1]);
            if (diff < 1 || diff > 3 || direction != report[i] > report[i + 1])
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsSafeWithDampener(int[] report)
    {
        bool? direction = null;
        for (int i = 0; i < report.Length - 1; i++)
        {
            direction ??= report[i] > report[i + 1];
            var diff = Math.Abs(report[i] - report[i + 1]);
            if (diff < 1 || diff > 3 || direction != report[i] > report[i + 1])
            {
                return IsSafeWithSkip(report, i - 1)
                    || IsSafeWithSkip(report, i)
                    || IsSafeWithSkip(report, i + 1);
            }
        }

        return true;
    }

    private static bool IsSafeWithSkip(int[] report, int skipIndex)
    {
        bool? direction = null;
        var lenght = report.Length - 1 == skipIndex 
            ? report.Length - 2 
            : report.Length - 1;

        for (int i = 0; i < lenght; i++)
        {
            if (i == skipIndex)
            {
                continue;
            }

            var nextIndex = i + 1 == skipIndex 
                ? 2 
                : 1;

            direction ??= report[i] > report[i + nextIndex];
            var diff = Math.Abs(report[i] - report[i + nextIndex]);
            if (diff < 1 || diff > 3 || direction != report[i] > report[i + nextIndex])
            {
                return false;
            }
        }

        return true;
    }
}
