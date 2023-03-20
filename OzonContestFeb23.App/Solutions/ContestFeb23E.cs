using System.Collections.Generic;
using System.Text;

namespace OzonContestFeb23App.Solutions;

public class ContestFeb23E
{
    public int Solve(List<string> request)
    {
        var requests = new HashSet<string>();
        foreach (var req in request)
        {
            var prev = req[0];
            var sameChars = 1;
            var proceed = new StringBuilder(prev.ToString());
            for (var i = 1; i < req.Length; i++)
            {
                if (prev == req[i])
                {
                    sameChars++;
                }
                else
                {
                    sameChars = 1;
                }

                prev = req[i];

                if (sameChars <= 2)
                {
                    proceed.Append(req[i]);
                }
            }

            requests.Add(proceed.ToString());
        }

        return requests.Count;
    }
}