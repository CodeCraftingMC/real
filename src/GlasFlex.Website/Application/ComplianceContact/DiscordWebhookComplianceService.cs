using GlasFlex.Website.Contract.ComplianceContact;
using GlasFlex.Website.Domain.ComplianceContact;
using GlasFlex.Website.Domain.Ratelimiter;
using GlasFlex.Website.Option.ComplianceContact;
using Microsoft.Extensions.Options;

namespace GlasFlex.Website.Application.ComplianceContact;

public class DiscordWebhookComplianceService : IComplianceService
{
    private readonly HttpClient _client;
    private readonly DiscordComplianceContactOptions _options;
    private readonly ILogger<DiscordWebhookComplianceService> _logger;

    public DiscordWebhookComplianceService(HttpClient client, IOptions<DiscordComplianceContactOptions> options, ILogger<DiscordWebhookComplianceService> logger)
    {
        _client = client;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<bool> SendComplianceNotificationAsync(ComplianceFormInput input)
    {
        var payload = new
        {
            content = $"<@&{_options.SupportRoleId}>",
            embeds = new[]
            {
                new
                {
                    title = "❗ Compliance Notification",
                    color = 4032511,
                    fields = new[]
                    {
                        new { name = "E-Mail:", value = input.Email },
                        new { name = "Name:", value = input.Name },
                        new { name = "Message:", value = input.Message }
                    }
                }
            },
            attachments = Array.Empty<object>()
        };

        try
        {
            _logger.LogInformation("Sending compliance notification to Discord for email: {Email}", input.Email);
            var response = await _client.PostAsJsonAsync(_options.WebhookUrl, payload);
            response.EnsureSuccessStatusCode();
            return true;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Failed to send compliance notification to Discord for email: {Email}", input.Email);
            return false;
        } 
    }
}