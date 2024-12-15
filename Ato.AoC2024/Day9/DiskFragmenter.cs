using System.Text;

namespace Ato.AoC2024.Day9;

public class DiskFragmenter : IProblem
{
    public string Solve1(string input)
    {
        var disk = input
            .Select(x => Convert.ToInt32(x - '0'))
            .SelectMany(CreateFileBlocks)
            .ToArray();

        //// Console.WriteLine(DrawDisk(disk));

        CompressDisk(disk);

        //// Console.WriteLine(DrawDisk(disk));

        return CalculateChecksum(disk).ToString();
    }

    public string Solve2(string input)
    {
        var disk = input
            .Select(x => Convert.ToInt32(x - '0'))
            .Select(CreateFileBlocks)
            .ToArray();

        //// Console.WriteLine(DrawDisk(disk.SelectMany(x => x).ToArray()));

        DefragDisk(disk);

        //// Console.WriteLine(DrawDisk(disk.SelectMany(x => x).ToArray()));

        return CalculateChecksum(disk.SelectMany(x => x).ToArray()).ToString();
    }

    private static int[] CreateFileBlocks(int length, int index)
    {
        var id = index % 2 == 0 
            ? index / 2 
            : int.MinValue;

        return Enumerable
            .Repeat(id, length)
            .ToArray();
    }

    private static void CompressDisk(int[] disc)
    {
        var freeBlock = 0;
        var lastDataBlock = disc.Length - 1;

        while (freeBlock < lastDataBlock)
        {
            if (disc[freeBlock] != int.MinValue)
            {
                ++freeBlock;
                continue;
            }

            if (disc[lastDataBlock] == int.MinValue)
            {
                --lastDataBlock;
                continue;
            }

            disc[freeBlock] = disc[lastDataBlock];
            disc[lastDataBlock] = int.MinValue;
        }
    }

    private static void DefragDisk(int[][] disk)
    {
        var minFreeBlock = 0;
        var lastFile = disk.Length - 1;

        while (minFreeBlock < lastFile)
        {
            if (disk[lastFile].FirstOrDefault(int.MinValue) == int.MinValue)
            {
                --lastFile;
                continue;
            }

            if (!disk[minFreeBlock].Any(x => x == int.MinValue))
            {
                ++minFreeBlock;
                continue;
            }

            var targetFile = Array
                .FindIndex(
                    disk, 
                    minFreeBlock,
                    lastFile - minFreeBlock,
                    x => x.Count(x => x == int.MinValue) >= disk[lastFile].Length);

            if (targetFile == -1)
            {
                --lastFile;
                continue;
            }

            disk[lastFile].CopyTo(
                disk[targetFile], 
                Array.FindIndex(disk[targetFile], x => x == int.MinValue));
            
            Array.Fill(
                disk[lastFile], 
                int.MinValue);
        }
    }

    private static long CalculateChecksum(int[] disk)
    {
        var checksum = 0L;
        for (int i = 0; i < disk.Length - 1; i++)
        {
            if (disk[i] == int.MinValue)
            {
                continue;
            }
            checksum += i * disk[i];
        }

        return checksum;
    }

    private static string DrawDisk(int[] disk) 
        => disk
            .Aggregate(
                new StringBuilder(),
                (sb, s) => s == int.MinValue ? sb.Append("[.]") : sb.Append('[').Append(s).Append(']'))
            .ToString();
}
