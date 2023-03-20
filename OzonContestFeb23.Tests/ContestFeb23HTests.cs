using Newtonsoft.Json;
using OzonContestFeb23App.Solutions;

namespace OzonContestFeb23.Tests;

public class ContestFeb23HTests
{
    [Theory]
    [JsonFileData<ModelH>("tests-h.json")]
    public void ContestFeb23HSolve_WithJsonFileData_ShouldReturnsExpectedResult(ModelH model)
    {
        var contH = new ContestFeb23H();
        var result = contH.Solve(model.Field, model.Figures);

        Assert.Equal(model.Answer ,result);
    }
}