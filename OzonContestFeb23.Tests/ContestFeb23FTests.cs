using OzonContestFeb23App.Solutions;

namespace OzonContestFeb23.Tests;

public class ContestFeb23FTests
{
    [Theory]
    [JsonFileData<ModelF>("tests-f.json")]
    public void ContestFeb23FSolve_WithJsonFileData_ShouldReturnsExpectedResult(ModelF model)
    {
        var contestF = new ContestFeb23F();

        var result = contestF.Solve(model.PagesToPrint, model.PrintedPages);

        Assert.Equal(model.Answer, result);
    }
}