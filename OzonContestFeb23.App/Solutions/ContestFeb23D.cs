using System.Collections.Generic;
using System.Linq;

namespace OzonContestFeb23App.Solutions;

public class ContestFeb23D
{
    public List<int> Solve(List<int> sportsmenTime)
    {
        var participants = sportsmenTime.Select((x, n) => new { participant = n, time = x, place = 0 })
            .ToDictionary(p => p.participant, p => new List<int> { p.time, p.place })
            .OrderBy(p => p.Value[0])
            .Select(x => x)
            .ToDictionary(p => p.Key, p => new List<int> { p.Value[0], p.Value[1] });

        var prev = participants.First();
        var currentPlace = 1;

        foreach (var p in participants)
        {
            if (p.Key == participants.First().Key)
            {
                p.Value[1] = 1;
                continue;
            }
            
            currentPlace++;
                
            if (p.Value[0] - 1 == prev.Value[0] || p.Value[0] == prev.Value[0])
            {
                p.Value[1] = prev.Value[1];
                prev = p;
                continue;
            }
            
            p.Value[1] = currentPlace;
            prev = p;
        }
        
        var result = participants
                .OrderBy(e => e.Key)
                .Select(x => x.Value[1]).ToList();
        return result;
    }
}