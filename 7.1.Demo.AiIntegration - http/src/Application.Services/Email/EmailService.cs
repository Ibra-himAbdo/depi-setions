using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace Application.Services;

internal class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
    {
        _emailSettings = emailSettings.Value;
        _logger = logger;
    }

    public async Task<bool> SendEmailAsync(Email email)
    {
        MimeMessage message = new();
        message.From.Add(new MailboxAddress("Application Support", _emailSettings.Email));

        string recipients = string.Join(", ", email.To);
        foreach (string recipient in email.To)
        {
            MailboxAddress? address = TryParseMailboxAddress(recipient);
            if (address is not null) message.To.Add(address);
        }

        message.Subject = email.Subject;

        BodyBuilder bodyBuilder = new()
        {
            HtmlBody = email.Body
        };
        message.Body = bodyBuilder.ToMessageBody();

        try
        {
            using SmtpClient smtp = new();
            smtp.CheckCertificateRevocation = false; // TODO: remove this in production

            await smtp.ConnectAsync(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_emailSettings.Email, _emailSettings.Password);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);

            _logger.LogInformation("Email sent successfully to {Recipients}", recipients);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to {Recipients}: {Message}", recipients, ex.Message);
            return false;
        }
    }

    private static MailboxAddress? TryParseMailboxAddress(string recipient)
    {
        return MailboxAddress.TryParse(recipient, out var address) ? address : null;
    }
}