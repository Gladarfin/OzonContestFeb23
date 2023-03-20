using System.Collections.Generic;

namespace OzonContestFeb23App.Models;

public class ModelG
{
    public int TestNumber { get; set; }
    public int NumberOfAppointmentWindows { get; set; }
    public int NumberOfPatients { get; set; }
    public List<int> WindowNumber { get; set; }
    public string Answer { get; set; }
}