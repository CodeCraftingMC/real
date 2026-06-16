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
            Severity = IncidentSeverityLevel.High,
            Date = new DateTime(2026, 4, 23),
            Message = "Some criminal person left the dirty dishes in the sink again and now the dishwasher is banned.",
            IncidentResolveMessage = "Days until dishwasher returns:",
            IncidentHasResolvedMessage = "The dishwasher has returned.",
            DaysUntilResolved = 5,
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
