using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Core.Abstractions;

public interface ITokenService
{
    Task<LoginResponse> CreateTokenAsync(AppUser user);
}

public class LoginResponse
{
    public string Token { get; set; }
    public DateTime ExpiresOn { get; set; }

    public LoginResponse(string token, DateTime expiresOn)
    {
        Token = token;
        ExpiresOn = expiresOn;
    }
}