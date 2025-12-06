using System;
using System.Collections.Generic;
using System.Text;

namespace Ato.AoC2025.Day03;

internal class Lobby : IProblem
{
    public string Solve1(string input)
    {
        var joltageSum = input
            .Split(Environment.NewLine)
            .Select(bank => bank.ToCharArray())
            .Select(batteries =>
            {
                var tensPlace = batteries[..^1].Max();
                var tensPlaceIndex = Array.IndexOf(batteries, tensPlace);
                var onesPlace = batteries[(tensPlaceIndex + 1)..].Max();

                return int.Parse($"{tensPlace}{onesPlace}");
            })
            .Sum();

        return joltageSum.ToString();
    }

    public string Solve2(string input)
    {
        var joltageSum = input
            .Split(Environment.NewLine)
            .Select(bank => bank.ToCharArray())
            .Select(batteries =>
            {
                var batteriesToTurn = 12;
                var lastTurnPlaceIndex = -1;
                var turnedBatteries = new List<char>();

                for (int i = 1; i <= batteriesToTurn; i++)
                {
                    var turnPlace = batteries[(lastTurnPlaceIndex + 1)..^(batteriesToTurn - i)].Max();
                    turnedBatteries.Add(turnPlace);

                    var turnPlaceIndex = Array.IndexOf(batteries, turnPlace, (lastTurnPlaceIndex + 1));
                    lastTurnPlaceIndex = turnPlaceIndex;
                }

                return long.Parse(turnedBatteries.ToArray());
            })
            .Sum();

        return joltageSum.ToString();
    }
}
