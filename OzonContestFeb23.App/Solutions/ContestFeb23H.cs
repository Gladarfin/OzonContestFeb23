using System;
using System.Collections.Generic;
using System.Linq;

namespace OzonContestFeb23App.Solutions;

public class ContestFeb23H
{
    private class ScoreAndField
    {
        public int Score { get; set; }
        public List<char[]> Field { get; set; }
    }
    
    public int Solve(IEnumerable<string> field, List<List<string>> figures)
    {
        var figuresSets = GeneratePermutations(figures).ToList();
        var fieldAsCharArray = field.Select(x => x.ToCharArray()).ToList();
        var bestScoreForEachSet = figuresSets
            .Select(figuresSet => GetBestSolution(fieldAsCharArray, figuresSet))
            .ToList();

        return  bestScoreForEachSet.Min() == int.MaxValue ? -1 : bestScoreForEachSet.Min();
    }
    private static int GetBestSolution(List<char[]> field, List<List<string>> figures)
    {
        var curFigure = 1;
        var bestScore = int.MaxValue;
        var fieldsForFigure = new Dictionary<int, List<ScoreAndField>> { { 0, new List<ScoreAndField>() } };
        fieldsForFigure[0].Add(new ScoreAndField{
            Score = int.MaxValue, 
            Field = CopyList(field)});
        foreach (var figure in figures)
        {
            fieldsForFigure.Add(curFigure, new List<ScoreAndField>());
            var figureWidth = figure[0].Length;
            var figureHeight = figure.Count;

            foreach (var fField in fieldsForFigure[curFigure - 1])
            {
                var secondField = CopyList(fField.Field);
                for (var r = 0; r <= 8 - figureHeight; r++)
                {
                    for (var c = 0; c <= 8 - figureWidth; c++)
                    {
                        if (!TryToPutFigure(r, c, figure, secondField))
                        {
                            continue;
                        }

                        PutFigure(r, c, figure, ref secondField);
                        
                        ClearFullRowsAndColumns(ref secondField, r, c, figureHeight, figureWidth);
                        
                        var score = GetCurrentScore(secondField);
                        if (curFigure != figures.Count)
                        {
                            fieldsForFigure[curFigure].Add(new ScoreAndField{
                                Score = score, 
                                Field = CopyList(secondField)});
                        }

                        if (curFigure == figures.Count && score <= bestScore)
                        {
                            bestScore = score;
                        }

                        secondField =  CopyList(fField.Field);
                    }
                }
            }
            curFigure++;
        }
        return bestScore;
        
    }
    private static List<char[]> CopyList(List<char[]> original)
    {
        var copy = new List<char[]>(original.Count);
        foreach (var array in original)
        {
            var newArray = new char[array.Length];
            Array.Copy(array, newArray, array.Length);
            copy.Add(newArray);
        }
        return copy;
    }
    private static bool TryToPutFigure(int r, int c, IReadOnlyList<string> figure, IReadOnlyList<char[]> field)
    {
        for (var fr = 0; fr < figure.Count; fr++)
        {
            for (var fc = 0; fc < figure[0].Length; fc++)
            {
                if (field[r + fr][c + fc] == '*' && figure[fr][fc] == '*')
                {
                    return false;
                }
            }
        }
        return true;
    }
    private static void PutFigure(int r, int c, List<string> figure, ref List<char[]> field)
    {
        for (var fr = 0; fr < figure.Count; fr++)
        {
            for (var fc = 0; fc < figure[0].Length; fc++)
            {
                if (figure[fr][fc] == '*')
                {
                    field[r + fr][c + fc] = '*';
                }
            }
        }
    }
    private static int GetCurrentScore(IEnumerable<char[]> field)
    {
        return field.Sum(x => x.Count(y => y == '*'));
    }
    private static void ClearFullRowsAndColumns(ref List<char[]> field, int row, int col, int figureHeight, int figureWidth)
    {
        var toClearRows = SearchFullOccupied(row, figureHeight, field, true);
        var toClearColumns = SearchFullOccupied(col, figureWidth, field, false);
    
        ClearFieldBy(ref field, toClearRows, true);
        ClearFieldBy(ref field, toClearColumns, false);
    }
    
    private static List<int> SearchFullOccupied(int start, int figureSize, List<char[]> field, bool isRow)
    {
        const char symbol = '*';
        var toClear = new List<int>();
        for (var i = start; i < start + figureSize; i++)
        {
            var allSame = isRow ? 
                field[i].All(c => c == symbol) 
                : field.Select(row => row[i]).All(c => c == symbol);
            
            if (allSame)
                toClear.Add(i);
        }

        return toClear;
    }

    private static void ClearFieldBy(ref List<char[]> field, List<int> toClear, bool isRow)
    {
        var cleared = Enumerable.Repeat('.', 8).ToArray();
        foreach (var i in toClear)
        {
            if (isRow)
            {
                field[i] = cleared;
                continue;
            }

            for (var j = 0; j < 8; j++)
            {
                field[j][i] = '.';
            }
        }
    }
    
    private static IEnumerable<List<List<T>>> GeneratePermutations<T>(IReadOnlyList<List<T>> list)
    {
        if (list.Count == 0)
            yield return new List<List<T>>();
        else
        {
            for (var i = 0; i < list.Count; i++)
            {
                var sublist = list[i];
                var rest = list.Take(i).Concat(list.Skip(i + 1)).ToList();
                foreach (var permutation in GeneratePermutations(rest))
                {
                    permutation.Insert(0, sublist);
                    yield return permutation;
                }
            }
        }
    }
}

