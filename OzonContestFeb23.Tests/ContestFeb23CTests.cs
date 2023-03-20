using OzonContestFeb23App.Solutions;

namespace OzonContestFeb23.Tests;

public class ContestFeb23CTests
{
    [Theory]
    [JsonFileData<ModelC>("tests-c.json")]
    public void ContestFeb23CSolve_WithJsonFileData_ShouldReturnsExpectedResult(ModelC model)
    {
        var contestC = new ContestFeb23C();

        var result = contestC.Solve(model.TestData);
        
        Assert.Equal(model.TestAnswer, result);
    }
}