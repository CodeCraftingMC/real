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
            Date = new DateTime(2026, 4, 23),
            Message = "I (the developer) have no idea what happened on this day, I'm just rewriting this code to make it even more unnecessary than it already is."
        };
    }

    public async Task<int> GetDaysSinceLatestIncidentAsync()
    {
        DateTime start = _latestIncident.Date;
        DateTime end = DateTime.Now;
        
        if (end < start)
            (start, end) = (end, start);

        start = start.Date;
        end = end.Date;

        int totalDays = (end - start).Days + 1;
        int fullWeeks = totalDays / 7;
        int remainingDays = totalDays % 7;

        int weekdays = fullWeeks * 5;

        for (int i = 0; i < remainingDays; i++)
        {
            var day = start.AddDays(fullWeeks * 7 + i).DayOfWeek;
            if (day != DayOfWeek.Saturday && day != DayOfWeek.Sunday)
                weekdays++;
        }

        return weekdays;
    }

    public async Task<IncidentDescription> GetLatestIncidentAsync()
    {
        return _latestIncident;
    }
}
