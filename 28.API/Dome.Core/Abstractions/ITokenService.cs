using Dome.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dome.Core.Abstractions;

public interface ITokenService
{
    Task<string> CreateTokenAsync(AppUser user);
}
