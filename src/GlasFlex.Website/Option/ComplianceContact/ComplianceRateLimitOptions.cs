using GlasFlex.Website.Contract.Ratelimiter;

namespace GlasFlex.Website.Option.ComplianceContact;

public sealed class ComplianceRateLimitOptions
{
    public string RatelimitStoreName { get; set; } = "ComplianceContact";

    // 0–9    => allow
    // 10–29  => show real rate-limit message
    // 30+    => fake success, silently drop
    public int MaxPoints { get; set; } = 10;
    public int FakeSuccessPoints { get; set; } = 30;

    // Added only after a real message was successfully sent.
    // This causes the next normal submit within 10 minutes to show rate-limit.
    public RatelimitScore NormalRequestPoints { get; set; } =
        new(10, TimeSpan.FromMinutes(10));

    // Obvious bot signals: instant fake success.
    public RatelimitScore HoneypotFilledPoints { get; set; } =
        new(30, TimeSpan.FromMinutes(30));

    public RatelimitScore DiscordTokenPoints { get; set; } =
        new(30, TimeSpan.FromHours(1));

    public RatelimitScore JwtTokenPoints { get; set; } =
        new(30, TimeSpan.FromHours(1));

    // Suspicious, but not instant fake-success by itself.
    public RatelimitScore NoUserAgentPoints { get; set; } =
        new(5, TimeSpan.FromMinutes(20));

    public RatelimitScore BlockedUserAgentPoints { get; set; } =
        new(10, TimeSpan.FromMinutes(30));

    public RatelimitScore LinkPoints { get; set; } =
        new(10, TimeSpan.FromMinutes(20));

    public RatelimitScore HtmlTagPoints { get; set; } =
        new(15, TimeSpan.FromMinutes(20));

    public RatelimitScore TooFastPoints { get; set; } =
        new(5, TimeSpan.FromMinutes(10));

    public string BlockedUserAgentsRegex { get; set; } =
        @"(?i)(curl|wget|python-requests|scrapy|aiohttp|httpclient|libwww-perl|bot|spider|crawler|^$)";

    public Dictionary<string, RatelimitScore> BlockedRegexPoints { get; set; } = new()
    {
        // HTML tags
        [@"<[^>]+>"] =
            new(15, TimeSpan.FromMinutes(20)),

        // JWT-like tokens
        [@"eyJ[A-Za-z0-9_-]{20,}\.[A-Za-z0-9_-]{20,}\.[A-Za-z0-9_-]{20,}"] =
            new(30, TimeSpan.FromHours(1)),

        // Discord-token-like secrets
        [@"\b[\w-]{20,30}\.[\w-]{6}\.[\w-]{25,40}\b"] =
            new(30, TimeSpan.FromHours(1))
    };
}