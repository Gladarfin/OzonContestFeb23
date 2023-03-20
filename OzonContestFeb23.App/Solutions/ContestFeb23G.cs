using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OzonContestFeb23App.Solutions;

public class ContestFeb23G
{
    private class PatientRecord
    {
        public int Index { get; set; }
        public int Window { get; set; }
        public char MoveDirection { get; set; }
    }

    private List<PatientRecord> AllPatients { get; set; }
    private bool[] Windows { get; set; }

    public string Solve(int numberOfPatient, int numberOfAppointmentWindows, List<int> windowNumber)
    {
        Windows = new bool[numberOfAppointmentWindows];
        AllPatients = new List<PatientRecord>();
        
        for (var i = 0; i < numberOfPatient; i++)
        {
            AllPatients.Add(new PatientRecord{Index = i, Window = windowNumber[i] - 1, MoveDirection = '0'});
        }

        AllPatients.Sort((a, b) => a.Window.CompareTo(b.Window));

        return !MoveWindows(AllPatients, Windows) ? 
            "x" : 
            GetResult(AllPatients);
    }
    private static bool MoveWindows(List<PatientRecord> records, bool[] windows)
    {
        foreach (var record in records)
        {
            var index = record.Window;
            
            if (index > 0 && !windows[index - 1])
            {
                windows[index - 1] = true;
                record.MoveDirection = '-';
                continue;
            }

            if (!windows[index])
            {
                windows[index] = true;
                continue;
            }

            if (index >= windows.Length - 1 || windows[index + 1]) 
                return false;
            
            windows[index + 1] = true;
            record.MoveDirection = '+';
        }
        return true;
    }
    
    private string GetResult(IEnumerable<PatientRecord> patients)
    {
        var result = new StringBuilder();
        foreach (var record in patients.OrderBy(x => x.Index))
        {
            result.Append(record.MoveDirection);
        }

        return result.ToString();
    }
}