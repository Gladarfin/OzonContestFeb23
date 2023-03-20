using OzonContestFeb23App.Solutions;

namespace OzonContestFeb23.Tests;

public class ContestFeb23BTests
{

    [Theory]
    [JsonFileData<ModelB>("tests-b.json")]
    public void ContestFeb23BSolve_WithJsonFileData_ShouldReturnsExpectedResult(ModelB model)
    {
        var contestB = new ContestFeb23B();

        var result = contestB.Solve(model.TestData);
        
        Assert.Equal(model.TestAnswers, result);
    }
}