using System;
using GlasFlex.Website.Contract.Incident;
using GlasFlex.Website.Domain.Incident;

namespace GlasFlex.Website.Application.Incident;

public class IncidentService : IIncidentService
{
    private readonly IncidentDescription _latestIncident;

    public IncidentService()
    {
        _latestIncident = new IncidentDescription()
        {
            Severity = IncidentSeverityLevel.Unknown,
            Date = new DateTime(2026, 3, 23),
            Message = "I (the developer) have no idea what happened on this day, I'm just rewriting this code to make it even more unnecessary than it already is."
        };
    }

    public async Task<int> GetDaysSinceLatestIncidentAsync()
    {
        return (DateTime.Now - _latestIncident.Date).Days;
    }

    public async Task<IncidentDescription> GetLatestIncidentAsync()
    {
        return _latestIncident;
    }
}
