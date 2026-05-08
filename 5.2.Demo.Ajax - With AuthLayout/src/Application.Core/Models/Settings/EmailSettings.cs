namespace Application.Core;

public class EmailSettings
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Host { get; set; }
    public int Port { get; set; }
}