using OzonContestFeb23App.Solutions;

namespace OzonContestFeb23.Tests;

public class ContestFeb23GTests
{
    [Theory]
    [JsonFileData<ModelG>("tests-g.json")]
    public void ContestFeb23GSolve_WithJsonFileData_ShouldReturnsExpectedResult(ModelG model)
    {
        var contestG = new ContestFeb23G();

        var result = contestG.Solve(model.NumberOfPatients, model.NumberOfAppointmentWindows, model.WindowNumber);

        Assert.Equal(model.Answer, result);
    }
}