using System;

namespace GlasFlex.Website.Contract.Incident;

public class IncidentDescription
{
    public IncidentSeverityLevel Severity { get; set; } = IncidentSeverityLevel.Low;
    public DateTime Date { get; set; }
    public string Message { get; set; } = string.Empty;
    public string IncidentResolveMessage { get; set; } = "Days until incident is resolved:";
    public string IncidentHasResolvedMessage { get; set; } = "Incident has been resolved.";
    public int DaysUntilResolved { get; set; } = 5;
}
