using OzonContestFeb23App.Solutions;

namespace OzonContestFeb23.Tests;

public class ContestFeb23ITests
{
    [Theory]
    [JsonFileData<ModelI>("tests-i.json")]
    public void ContestFeb23ISolve_WithJsonFileData_ShouldReturnsExpectedResult(ModelI model)
    {
        var contestI = new ContestFeb23I();

        var result = contestI.Solve(model.TableInHtml);

        Assert.Equal(model.Answer, result);
    }
}