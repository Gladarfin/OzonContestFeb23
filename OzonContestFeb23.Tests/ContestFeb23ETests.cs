using OzonContestFeb23App.Solutions;

namespace OzonContestFeb23.Tests;

public class ContestFeb23ETests
{
    [Theory]
    [JsonFileData<ModelE>("tests-e.json")]
    public void ContestFeb23ESolve_WithJsonFileData_ShouldReturnsExpectedResult(ModelE model)
    {
        var contestE = new ContestFeb23E();

        var result = contestE.Solve(model.Requests);

        Assert.Equal(model.Answer, result);
    }
}