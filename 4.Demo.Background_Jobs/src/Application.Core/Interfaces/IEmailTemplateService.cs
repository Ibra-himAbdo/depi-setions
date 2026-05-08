namespace Application.Core;

public interface IEmailTemplateService
{
    string GenerateEmailBody(string title, string userName, string content, string? actionLink = null, string? actionText = null);
}
