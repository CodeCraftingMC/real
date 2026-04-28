using System;

namespace GlasFlex.Website.Contract.Incident;

public class IncidentDescription
{
    public IncidentSeverityLevel Severity { get; set; } = IncidentSeverityLevel.Low;
    public DateTime Date { get; set; }
    public string Message { get; set; } = string.Empty;
}
