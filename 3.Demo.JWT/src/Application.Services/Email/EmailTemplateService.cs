using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Application.Services;

internal class EmailTemplateService : IEmailTemplateService
{
    private readonly string _baseTemplate;

    public EmailTemplateService(IWebHostEnvironment webHostEnvironment)
    {
        var path = Path.Combine(webHostEnvironment.ContentRootPath, "Application.Services", "Email", "Templates", "BaseTemplate.html");
        
        if (!File.Exists(path))
        {
             path = Path.Combine(webHostEnvironment.ContentRootPath, "Email", "Templates", "BaseTemplate.html");
        }

        if (File.Exists(path))
        {
            _baseTemplate = File.ReadAllText(path);
        }
        else
        {
            _baseTemplate = "<html><body><h1>{{Title}}</h1><p>Hello, {{UserName}}!</p><p>{{Content}}</p><a href='{{ActionLink}}'>{{ActionText}}</a></body></html>";
        }
    }

    public string GenerateEmailBody(string title, string userName, string content, string? actionLink = null, string? actionText = null)
    {
        var builder = new StringBuilder(_baseTemplate);

        builder.Replace("{{Title}}", title);
        builder.Replace("{{UserName}}", userName);
        builder.Replace("{{Content}}", content);

        var result = builder.ToString();

        if (!string.IsNullOrEmpty(actionLink))
        {
            result = result.Replace("{{ActionLink}}", actionLink)
                           .Replace("{{ActionText}}", actionText ?? "Click Here");
            
            // Remove the conditional tags but keep the content
            result = Regex.Replace(result, @"\{\{#if ActionLink\}\}(.*?)\{\{/if\}\}", "$1", RegexOptions.Singleline);
        }
        else
        {
            // Remove the entire conditional block
            result = Regex.Replace(result, @"\{\{#if ActionLink\}\}.*?\{\{/if\}\}", string.Empty, RegexOptions.Singleline);
        }

        return result;
    }
}
