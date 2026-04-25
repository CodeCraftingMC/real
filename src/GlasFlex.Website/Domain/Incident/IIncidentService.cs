using System;
using GlasFlex.Website.Contract.Incident;

namespace GlasFlex.Website.Domain.Incident;

public interface IIncidentService
{
    Task<IncidentDescription> GetLatestIncidentAsync();
    Task<int> GetDaysSinceLatestIncidentAsync();
}
