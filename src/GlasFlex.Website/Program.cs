using GlasFlex.Website.Application.ComplianceContact;
using GlasFlex.Website.Application.Incident;
using GlasFlex.Website.Application.Ratelimiter;
using GlasFlex.Website.Components;
using GlasFlex.Website.Domain.ComplianceContact;
using GlasFlex.Website.Domain.Incident;
using GlasFlex.Website.Domain.Ratelimiter;
using GlasFlex.Website.Option.ComplianceContact;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<DiscordComplianceContactOptions>(builder.Configuration.GetSection("DiscordComplianceContact"));
builder.Services.Configure<ComplianceRateLimitOptions>(builder.Configuration.GetSection("ComplianceRateLimit"));

builder.Services.AddSingleton<IIncidentService, IncidentService>();
builder.Services.AddSingleton<IComplianceService, DiscordWebhookComplianceService>();
builder.Services.AddSingleton<IRateLimitStore, InMemoryRateLimitStore>();
builder.Services.AddSingleton<IRateLimiter, RateLimiter>();

if(!builder.Environment.IsDevelopment())
{
    builder.Services.AddResponseCompression();

    string? reverseProxyIp = Environment.GetEnvironmentVariable("REVERSE_PROXY_IP");

    if(string.IsNullOrWhiteSpace(reverseProxyIp))
    {
        throw new Exception("REVERSE_PROXY_IP environment variable is not set.");
    }

    builder.Services.Configure<ForwardedHeadersOptions>(options =>
    {
        options.ForwardedHeaders =
            Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor |
            Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto;

        // Get from environment
        options.KnownProxies.Add(System.Net.IPAddress.Parse(reverseProxyIp));
    });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseForwardedHeaders();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
