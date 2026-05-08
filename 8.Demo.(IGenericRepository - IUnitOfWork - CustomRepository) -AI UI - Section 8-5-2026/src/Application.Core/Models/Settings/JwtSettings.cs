namespace Application.Core;

public class JwtSettings
{
    public required string SecretKey { get; set; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public double DurationInDays { get; set; } = 7;
}