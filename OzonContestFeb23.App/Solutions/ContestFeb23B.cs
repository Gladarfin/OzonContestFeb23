using System;
using System.Collections.Generic;
using System.Linq;

namespace OzonContestFeb23App.Solutions;

public class ContestFeb23B
{
    public string Solve(List<int> data)
    {
        var result = new List<string>();

        if (data is null)
            throw new Exception("File doesn't exist");

        var correctShipsNumber = new[] { 4, 3, 2, 1 };
        var isCorrect = true;
        var ships = data.OrderBy(x => x).ToArray();

        foreach (var ship in ships)
        {
            correctShipsNumber[ship - 1]--;
            if (correctShipsNumber[ship - 1] >= 0)
                continue;

            isCorrect = false;
            break;
        }
        return isCorrect ? "YES" : "NO";
    }
}