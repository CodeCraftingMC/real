using GlasFlex.Website.Contract.Ratelimiter;

namespace GlasFlex.Website.Option.ComplianceContact;

public class DiscordComplianceContactOptions
{
    public string WebhookUrl { get; set; } = string.Empty;
    public string SupportRoleId { get; set; } = string.Empty;
}