namespace Application.Core;

public interface IEmailService
{
    Task<bool> SendEmailAsync(Email email);
}