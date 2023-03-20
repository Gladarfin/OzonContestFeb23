using System;
using System.Collections.Generic;
using System.Linq;

namespace OzonContestFeb23App.Solutions;

public class ContestFeb23F
{
    private List<int> AllPages { get; set; }
    
    public string Solve(int pagesToPrint, string printedPages)
    {
        AllPages = Enumerable.Range(1, pagesToPrint).ToList();

        var sortedPrintedPages = SortValues(printedPages);
        foreach (var curPage in sortedPrintedPages.TakeWhile(curPage => AllPages.Count != 0))
        {
            if (!curPage.Contains('-'))
            {
                var page = FindClosestToGivenNumber(AllPages, int.Parse(curPage));
                if (page != int.Parse(curPage))
                    continue;
                var pageIndex = AllPages.IndexOf(page);
                if (pageIndex >= 0)
                    AllPages.RemoveAt(pageIndex);
                continue;
            }

            var left = FindClosestToGivenNumber(AllPages, int.Parse(curPage.Split('-')[0]));
            var right = FindClosestToGivenNumber(AllPages, int.Parse(curPage.Split('-')[1]), false);

            if (left < 0 || right < 0 || left > right)
                continue;
            if (left == right)
            {
                var index = AllPages.IndexOf(right);
                if (index >= 0)
                    AllPages.RemoveAt(index);
                continue;
            }

            var startIndex = AllPages.IndexOf(left);
            var endIndex = AllPages.IndexOf(right);
            var count = endIndex - startIndex + 1;

            if (count == 0)
                continue;
            AllPages.RemoveRange(startIndex, count);
        }

        var result = GetRangesFromList(AllPages);
        
        return result;
    }

    private static List<string> SortValues(string stringOfPages)
    {
        var tmp = stringOfPages.Split(',').Distinct();

        var result = tmp.OrderBy(s => int.Parse(s.Split('-')[0]))
            .ThenBy(s => s).ToList();
        return result;
    }

    private static int FindClosestToGivenNumber(List<int> curList, int valueToSearch, bool isLeft = true)
    {

        var orderedList = curList.OrderBy(x => Math.Abs(x - valueToSearch));
        var result = isLeft ? 
            orderedList.FirstOrDefault(x => x >= valueToSearch) : 
            orderedList.FirstOrDefault(x => x <= valueToSearch);

        if (result == default(int))
            return -1;

        return result;
    }

    private static string GetRangesFromList(IReadOnlyList<int> list)
    {
        var result = new List<string>();
        var i = 0;

        while (i < list.Count)
        {
            var start = list[i];
            var end = start;

            while (i + 1 < list.Count && list[i + 1] - list[i] == 1)
            {
                end = list[i + 1];
                i++;
            }
            var formattedOutput = FormatRangesAsString(Tuple.Create(start, end));
            result.Add(formattedOutput);
            i++;
        }
        return string.Join(",", result);
    }

    private static string FormatRangesAsString(Tuple<int, int> values)
    {
        return values.Item1 == values.Item2 ? 
            values.Item1.ToString() : 
            $"{values.Item1}-{values.Item2}";
    }
}