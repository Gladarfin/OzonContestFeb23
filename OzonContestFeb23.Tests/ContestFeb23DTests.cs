using OzonContestFeb23App.Solutions;

namespace OzonContestFeb23.Tests;

public class ContestFeb23DTests
{
    [Theory]
    [JsonFileData<ModelD>("tests-d.json")]
    public void ContestFeb23DSolve_WithJsonFileData_ShouldReturnsExpectedResult(ModelD model)
    {
        var contestD = new ContestFeb23D();

        var result = contestD.Solve(model.SportsmenTime);

        Assert.Equal(model.TestAnswers, result);
    }
}