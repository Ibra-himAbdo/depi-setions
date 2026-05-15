namespace Application.Core;
public record Email(
    List<string> To,
    string Title,
    string Subject,
    string Body);