using GlasFlex.Website.Contract.ComplianceContact;

namespace GlasFlex.Website.Domain.ComplianceContact;

public interface IComplianceService
{
    Task<bool> SendComplianceNotificationAsync(string ip, string userAgent, ComplianceFormInput input);
}