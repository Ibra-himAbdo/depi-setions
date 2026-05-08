namespace Application.Core;

public record LoginResponse(string? Token, DateTime ExpiresOn, bool IsEmailConfirmed);