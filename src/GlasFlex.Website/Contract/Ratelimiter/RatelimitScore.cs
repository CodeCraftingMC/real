namespace GlasFlex.Website.Contract.Ratelimiter;

public class RatelimitScore
{
    public int Points { get; set; }
    public TimeSpan Duration { get; set; }

    public RatelimitScore(){}

    public RatelimitScore(int pointsPerMinute, TimeSpan lifetime)
    {
        Points = pointsPerMinute;
        Duration = lifetime;
    } 
}