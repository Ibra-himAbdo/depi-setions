# Project Goal: Identity + JWT Upgrade

`3.Demo.JWT` evolves the previous API into an Identity-based authentication
system with:

- custom user profile fields
- role seeding (`Admin`, `User`)
- JWT authentication
- Result-pattern based API responses
- email confirmation before successful login

The focus here is the **actual implemented flow** and code in `3.Demo.JWT`.

---

## Reconstructed Build Flow (Aligned With Current Code)

### Step 1: Add Identity + Mapping packages

- 🎯 **Goal**: start with the minimum libraries that unlock Identity and clean mapping.
- 🤔 **Problem**: before writing any auth code, I need the framework types
  (`IdentityUser`, JWT middleware, mapping) available in the right projects.
- 📦 **Package checkpoint (why now?)**:
  - `Microsoft.Extensions.Identity.Stores` -> required by Core Identity model contracts.
  - `Microsoft.AspNetCore.Identity.EntityFrameworkCore` -> required to build
    `IdentityDbContext`.
  - `Microsoft.EntityFrameworkCore.SqlServer` -> required because the DbContext
    will be persisted to SQL Server.
  - `Mapster` -> required before I can map `RegisterDto` to `ApplicationUser`.
  - `Microsoft.AspNetCore.Authentication.JwtBearer` -> required before enabling
    bearer-token auth in `Program.cs`.
  - `MailKit` + `MimeKit` -> required later for SMTP email delivery and message
    composition.
  - `System.IdentityModel.Tokens.Jwt` + `Microsoft.IdentityModel.Tokens` ->
    required later in `AuthService` token creation code.
- 🛠 **Implementation**:

```xml
<!-- src/Application.Core/Application.Core.csproj -->
<ItemGroup>
  <PackageReference Include="Microsoft.Extensions.Identity.Stores" Version="10.0.6" />
</ItemGroup>

<!-- src/Application.Infrastructure/Application.Infrastructure.csproj -->
<ItemGroup>
  <PackageReference Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="10.0.6" />
  <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="10.0.6" />
</ItemGroup>

<!-- src/Application/Application.csproj -->
<ItemGroup>
  <PackageReference Include="Mapster" Version="10.0.7" />
  <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="10.0.6" />
</ItemGroup>

<!-- src/Application.Services/Application.Services.csproj -->
<ItemGroup>
  <PackageReference Include="MailKit" Version="4.16.0" />
  <PackageReference Include="Microsoft.IdentityModel.Tokens" Version="7.7.1" />
  <PackageReference Include="MimeKit" Version="4.16.0" />
  <PackageReference Include="System.IdentityModel.Tokens.Jwt" Version="7.7.1" />
</ItemGroup>
```

- 💡 **Reasoning**: I install foundation packages first so every next step
  compiles without temporary hacks.
- 🔗 **Leads To**: defining an application-specific identity user.

---

### Step 2: Create the AppUser (`ApplicationUser`)

- 🎯 **Goal**: define the user model the business actually needs.
- 🤔 **Problem**: default Identity user is not enough because we need `FullName`.
- 📦 **Package checkpoint (used in this step)**:
  - `Microsoft.Extensions.Identity.Stores` (from Step 1) provides the Identity
    base model infrastructure used by `IdentityUser`.
- 🛠 **Implementation**:

```csharp
// src/Application.Core/Entities/ApplicationUser.cs
namespace Application.Core;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
}
```

- 💡 **Reasoning**: I keep all built-in Identity behavior and only extend what
  the domain needs.
- 🔗 **Leads To**: context and configuration that persist this new field.

---

### Step 3: Build the DbContext on Identity

- 🎯 **Goal**: move from just a user class to real database schema support.
- 🤔 **Problem**: plain `DbContext` won’t generate Identity tables or relationships.
- 📦 **Package checkpoint (used in this step)**:
  - `Microsoft.AspNetCore.Identity.EntityFrameworkCore` -> provides
    `IdentityDbContext<TUser>`.
  - `Microsoft.EntityFrameworkCore.SqlServer` -> required by provider-specific
    runtime operations.
- 🛠 **Implementation**:

```csharp
// src/Application.Infrastructure/Data/ApplicationDbContext.cs
namespace Application.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply entity configurations
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
```

- 💡 **Reasoning**: I inherit from `IdentityDbContext<ApplicationUser>` instead
  of reinventing Identity tables manually.
- 🔗 **Leads To**: custom constraints via entity configuration.

---

### Step 4: Add AppUser configuration

- 🎯 **Goal**: lock down `FullName` rules before running migrations.
- 🤔 **Problem**: if I skip constraints now, invalid/oversized data will slip in.
- 📦 **Package checkpoint (used in this step)**:
  - Uses EF Core fluent configuration APIs already available through
    Infrastructure EF references from Step 1.
- 🛠 **Implementation**:

```csharp
// src/Application.Infrastructure/Data/Configurations/ApplicationUserConfiguration.cs
namespace Application.Infrastructure;

internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(e => e.FullName)
            .IsRequired()
            .HasMaxLength(100);
    }
}
```

- 💡 **Reasoning**: I prefer Fluent API here so entity classes stay focused on
  domain intent, not persistence attributes.
- 🔗 **Leads To**: safe migration and deterministic schema.

---

### Step 5: Auto migration + seeding + roles constants

- 🎯 **Goal**: make first run reliable (schema + roles + admin).
- 🤔 **Problem**: if migration and role setup stay manual, every environment drifts.
- 📦 **Package checkpoint (critical for this step)**:
  - `Microsoft.EntityFrameworkCore.SqlServer` -> applies SQL Server migrations.
  - `Microsoft.EntityFrameworkCore.Design` -> tooling metadata for migrations.
  - `Microsoft.EntityFrameworkCore.Tools` -> CLI/PMC migration commands.
  - `Microsoft.AspNetCore.Identity.EntityFrameworkCore` -> role/user managers
    backed by EF store.
- 🛠 **Implementation**:

```csharp
// src/Application.Core/Models/Constants/ApplicationRoles.cs
namespace Application.Core;

public class ApplicationRoles
{
    public const string Admin = nameof(Admin);
    public const string User = nameof(User);
}
```

```csharp
// src/Application/Extensions/DatabaseMigrator.cs
public static class DatabaseMigrator
{
    private static List<string> roles = [ApplicationRoles.Admin, ApplicationRoles.User];

    public static async Task MigrateDatabaseAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        IServiceProvider serviceProvider = scope.ServiceProvider;
        ApplicationDbContext dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
        UserManager<ApplicationUser> userManger = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        RoleManager<IdentityRole> roleManger = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        await dbContext.Database.MigrateAsync();

        await SeedRoles(roleManger);
        await SeedAdmin(userManger);
    }
}
```

```csharp
// src/Application/Program.cs
await app.MigrateDatabaseAsync();
```

- Typical migration command sequence used for this setup:

```bash
dotnet ef migrations add InitalCreate -p src/Application.Infrastructure -s src/Application
dotnet ef database update -p src/Application.Infrastructure -s src/Application
```

- 💡 **Reasoning**: app startup migrates schema and ensures roles/admin exist
  before first request handling.
- 🔗 **Leads To**: authentication and authorization on ready data.

---

### Step 6: Create JWT authentication

- 🎯 **Goal**: secure API requests without server-side session state.
- 🤔 **Problem**: after users exist in DB, I still need a token validation pipeline.
- 📦 **Package checkpoint (used in this step)**:
  - `Microsoft.AspNetCore.Authentication.JwtBearer` -> adds JWT auth middleware
    and token validation integration.
  - `Microsoft.IdentityModel.Tokens` + `System.IdentityModel.Tokens.Jwt` ->
    required for token contracts used in auth service.
- 🛠 **Implementation**:

```csharp
// src/Application.Core/Models/Settings/JwtSettings.cs
namespace Application.Core;

public class JwtSettings
{
    public required string SecretKey { get; set; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public double DurationInDays { get; set; } = 7;
}
```

```csharp
// src/Application/Program.cs
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
```

```csharp
// src/Application.Core/Interfaces/IAuthService.cs (JWT contract in this step)
Task<LoginResponse> CreateJwtTokenAsync(ApplicationUser user);
```

```csharp
// src/Application.Services/Auth/AuthService.cs (JWT creation)
public async Task<LoginResponse> CreateJwtTokenAsync(ApplicationUser user)
{
    IList<string> userRoles = await _userManager.GetRolesAsync(user);

    List<Claim> claims = new()
    {
        new(ClaimTypes.NameIdentifier, user.Id),
        new(ClaimTypes.Email, user.Email!),
        new(ClaimTypes.GivenName, user.FullName!),
        new(JwtRegisteredClaimNames.Iat,
        new DateTimeOffset(now).ToUnixTimeSeconds().ToString(),
        ClaimValueTypes.Integer64)
    };

    claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

    SigningCredentials signingCredentials = new(
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
        SecurityAlgorithms.HmacSha256);

    JwtSecurityToken token = new(
        issuer: _jwtSettings.Issuer,
        audience: _jwtSettings.Audience,
        claims: claims,
        expires: DateTime.UtcNow.AddHours(_jwtSettings.DurationInDays),
        signingCredentials: signingCredentials);

    return new LoginResponse(new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo, user.EmailConfirmed);
}
```

```csharp
// src/Application/Program.cs
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!)),
            ClockSkew = TimeSpan.Zero
        };
    });
```

- 💡 **Reasoning**: I configure strict validation now so every protected endpoint
  behaves consistently from day one.
- 🔗 **Leads To**: token generation in auth service.

---

### Step 7: Result pattern + BaseApiController mapping

- 🎯 **Goal**: keep controller code boring and predictable.
- 🤔 **Problem**: mixing exceptions, booleans, and ad-hoc objects makes response
  handling noisy and inconsistent.
- 🛠 **Implementation**:

```csharp
// src/Application.Core/Models/ResultPattern/Result.cs
public record Result
{
    public bool IsSuccess { get; }
    public Error? Error { get; }

    public Result(bool isSuccess, Error? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, null);
    public static Result Failure(Error error) => new(false, error);

    public static implicit operator Result(Error error) => Failure(error);
}

public record Result<T> : Result
{
    public T? Value { get; }
    private Result(T value) : base(true, null) => Value = value;
    private Result(Error error) : base(false, error) { }
}

public enum ErrorType
{
    NotFound,
    Validation,
    Failure,
    Conflict
}

public record Error
{
    public string? Code { get; init; }
    public ErrorType Type { get; init; }
    public string? Description { get; init; }

    public Error(string? code, ErrorType type, string? description)
    {
        Code = code ?? type.ToString();
        Type = type;
        Description = description;
    }

    public static Error NotFound(string? code = null, string? description = null) => new(code, ErrorType.NotFound, description);

    public static Error Validation(string? code = null, string? description = null) => new(code, ErrorType.Validation, description);

    public static Error Failure(string? code = null, string? description = null) => new(code, ErrorType.Failure, description);

    public static Error Conflict(string? code = null, string? description = null) => new(code, ErrorType.Conflict, description);
}
```

```csharp
// src/Application/Controllers/BaseApiController.cs
protected IActionResult ReturnResult<T>(Result<T> result, ResponseStatus status = ResponseStatus.Ok)
{
    return result.IsSuccess ? HandleSuccess(status, result.Value) : HandleError(result.Error);
}

protected IActionResult ReturnResult(Result result, ResponseStatus status = ResponseStatus.Ok)
{
    return result.IsSuccess ? HandleSuccess(status) : HandleError(result.Error);
}

private IActionResult HandleError(Error? error)
{
    if (error is null)
        return StatusCode(500, new ApiResponse(500, "Unknown error"));

    return error.Type switch
    {
        ErrorType.NotFound => NotFound(new ApiResponse(404, error.Description)),
        ErrorType.Validation => BadRequest(new ApiResponse(400, error.Description)),
        ErrorType.Conflict => Conflict(new ApiResponse(409, error.Description)),
        ErrorType.Failure => StatusCode(500, new ApiResponse(500, error.Description)),
        _ => StatusCode(500, new ApiResponse(500, "Unexpected error"))
    };
}
```

- 💡 **Reasoning**: service layer decides business result; base controller owns HTTP translation.
- 🔗 **Leads To**: clean auth controller endpoints.

---

### Step 8: Add register + login first (without email service)

- 🎯 **Goal**: get authentication basics working before introducing SMTP concerns.
- 🤔 **Problem**: manual DTO mapping in each endpoint is repetitive and error-prone.
- 📦 **Package checkpoint (used in this step)**:
  - `Mapster` -> enables `dto.Adapt<ApplicationUser>()` and mapping config scan.
- 🛠 **Implementation**:

```csharp
// src/Application/Dtos/RegisterDto.cs
public record RegisterDto(string FullName, string Email, string Password);

// src/Application/Dtos/LoginDto.cs
public record LoginDto(string Email, string Password);
```

```csharp
// src/Application/Validators/RegisterDtoValidator.cs
public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().MinimumLength(3).MaximumLength(100);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6)
            .Matches("[A-Z]").Matches("[^a-zA-Z0-9]");
    }
}
```

```csharp
// src/Application/Validators/LoginDtoValidator.cs
public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6)
            .Matches("[A-Z]").Matches("[^a-zA-Z0-9]");
    }
}
```

```csharp
// src/Application/Helpers/MappingConfigs.cs
public class MappingConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterDto, ApplicationUser>()
            .Map(dest => dest.UserName, src => src.Email);
    }
}
```

```csharp
// src/Application/Program.cs
TypeAdapterConfig.GlobalSettings.Scan(typeof(MappingConfigs).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
```

```csharp
// src/Application.Core/Interfaces/IAuthService.cs (register contract in this step)
Task<Result> RegisterAsync(ApplicationUser user, string password);
Task<Result<LoginResponse>> LoginAsync(string email, string password);
```

```csharp
// src/Application.Services/Auth/AuthService.cs (register implementation)
public async Task<Result> RegisterAsync(ApplicationUser user, string password)
{
    bool isEmailTaken = await _userManager.FindByEmailAsync(user.Email!) is not null ? true : false;

    if (isEmailTaken) return Error.Conflict(description: "Email Already in use");

    IdentityResult? result;
    result = await _userManager.CreateAsync(user, password);

    if (result.Succeeded)
    {
        result = await _userManager.AddToRoleAsync(user, ApplicationRoles.User);

        if (result.Succeeded)
            return Result.Success();
    }

    IEnumerable<string> errors = result.Errors.Select(e => e.Description);

    return Error.Validation(description: string.Join("\n", errors));
}
```

```csharp
// src/Application.Services/Auth/AuthService.cs (inferred first login version, before email service)
// Inference: this was likely the initial login implementation in the flow.
public async Task<Result<LoginResponse>> LoginAsync(string email, string password)
{
    ApplicationUser? user = await _userManager.FindByEmailAsync(email);
    if (user is null || !await _userManager.CheckPasswordAsync(user, password))
        return Error.Validation(description: "Invalid Email or Password");

    return await CreateJwtTokenAsync(user);
}
```

```csharp
// src/Application/Controllers/AuthController.cs (register + login usage)
[HttpPost("Register")]
public async Task<IActionResult> RegisterAsync(RegisterDto dto)
{
    var validationResult = await ValidateRequestModel(dto);
    if (validationResult is not null) return validationResult;

    ApplicationUser user = dto.Adapt<ApplicationUser>();

    Result result = await _authService.RegisterAsync(user, dto.Password);

    return ReturnResult(result, ResponseStatus.Created);
}

[HttpPost("login")]
public async Task<IActionResult> LoginAsync(LoginDto dto)
{
    var validationResult = await ValidateRequestModel(dto);
    if (validationResult is not null) return validationResult;

    Result<LoginResponse> serviceResult = await _authService.LoginAsync(dto.Email, dto.Password);
    return ReturnResult(serviceResult);
}
```

- 💡 **Reasoning**: I keep endpoints focused on validation + orchestration,
  leaving identity business rules to `AuthService`. This keeps login functional
  even before email verification is introduced.
- 🔗 **Leads To**: adding mail transport for verification notifications.

---

### Step 9: Add email service

- 🎯 **Goal**: add a dedicated mail channel for verification messages.
- 🤔 **Problem**: login verification flow cannot work without SMTP delivery.
- 📦 **Package checkpoint (used in this step)**:
  - `MailKit` -> SMTP client implementation.
  - `MimeKit` -> compose MIME email message/body.
- 🛠 **Implementation**:

```csharp
// src/Application.Core/Models/Settings/EmailSettings.cs
namespace Application.Core;

public class EmailSettings
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Host { get; set; }
    public int Port { get; set; }
}
```

```csharp
// src/Application/Program.cs
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
```

```csharp
// src/Application.Services/DependencyInjection.cs
services.AddScoped<IEmailService, EmailService>();
```

```csharp
// src/Application.Services/Email/EmailService.cs
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
}
```

- 💡 **Reasoning**: `IEmailService` keeps auth logic testable and prevents SMTP
  details from leaking across layers.
- 🔗 **Leads To**: integrating verification into login/confirm flow.

---

### Step 10: Integrate email service with login + confirm journey

- 🎯 **Goal**: enforce a real verification gate before token issuance.
- 🤔 **Problem**: if unconfirmed users can log in directly, account trust is weak.
- 🛠 **Implementation**:

```csharp
// src/Application/Dtos/ConfirmEmailDto.cs
public record ConfirmEmailDto(string UserId, string Token);
```

```csharp
// src/Application/Validators/ConfirmEmailDtoValidation.cs
public class ConfirmEmailDtoValidation : AbstractValidator<ConfirmEmailDto>
{
    public ConfirmEmailDtoValidation()
    {
        RuleFor(x => x.UserId).NotEmpty().NotNull();
        RuleFor(x => x.Token).NotEmpty().NotNull();
    }
}
```

```csharp
// src/Application.Core/Interfaces/IAuthService.cs (login + confirm contracts in this step)
Task<Result<LoginResponse>> LoginAsync(string email, string password);
Task<Result<LoginResponse>> ConfirmEmailAsync(string userId, string token);
```

```csharp
// src/Application.Services/Auth/AuthService.cs
public async Task<Result<LoginResponse>> LoginAsync(string email, string password)
{
    ApplicationUser? user = await _userManager.FindByEmailAsync(email);

    if (user is null || !await _userManager.CheckPasswordAsync(user, password))
        return Error.Validation(description: "Invalid Email or Password");

    if (!user.EmailConfirmed)
    {
        string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        Email emailModel = new(
            To: [user.Email!],
            Title: "Verify Your Email",
            Subject: "Email Verification",
            Body: GenerateResetLink(_configuration["BaseAppUri"]!, user.Id, token));

        await _emailService.SendEmailAsync(emailModel);
        return Error.Validation(description: "Please confirm your email");
    }

    return await CreateJwtTokenAsync(user);
}

public async Task<Result<LoginResponse>> ConfirmEmailAsync(string userId, string token)
{
    ApplicationUser? user = await _userManager.FindByIdAsync(userId);
    if (user is null) return Error.Validation(description: "Email Not Confirmed");

    string decodedToken;
    try
    {
        decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
    }
    catch (FormatException)
    {
        return Error.Validation(description: "Invalid token");
    }

    var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
    if (!result.Succeeded)
        return Error.Validation(description: string.Join(", ", result.Errors.Select(e => e.Description)));

    return await CreateJwtTokenAsync(user);
}

private static string GenerateResetLink(string url, string userId, string token)
{
    var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
    return $"{url}/api/Auth/ConfirmEmail?userId={Uri.EscapeDataString(userId)}&token={encodedToken}";
}
```

```csharp
// src/Application/Controllers/AuthController.cs (login + confirm usage)
[HttpPost("login")]
public async Task<IActionResult> LoginAsync(LoginDto dto)
{
    var validationResult = await ValidateRequestModel(dto);
    if (validationResult is not null) return validationResult;

    Result<LoginResponse> serviceResult = await _authService.LoginAsync(dto.Email, dto.Password);
    return ReturnResult(serviceResult);
}

[HttpGet("ConfirmEmail")]
public async Task<IActionResult> ConfirmEmailAsync([FromQuery] ConfirmEmailDto dto)
{
    var validationResult = await ValidateRequestModel(dto);
    if (validationResult is not null) return validationResult;

    Result<LoginResponse> serviceResult = await _authService.ConfirmEmailAsync(dto.UserId, dto.Token);
    return ReturnResult(serviceResult);
}
```

- 💡 **Reasoning**: this code intentionally sends verification email on login
  attempt, then issues JWT only after confirm endpoint succeeds.
- 🔗 **Leads To**: final end-to-end request flow.

---

## Full Request Flow (Current Implementation)

1. Client sends `POST /api/auth/Register` with `RegisterDto`.
2. Controller validates DTO, maps via Mapster to `ApplicationUser`, and calls
   `RegisterAsync`.
3. Service creates user and adds `ApplicationRoles.User`.
4. Client attempts `POST /api/auth/login`.
5. If credentials are valid but `EmailConfirmed == false`, service sends
   verification email and returns validation error.
6. Client opens verification link and calls `GET /api/auth/ConfirmEmail`.
7. Service confirms token and returns `LoginResponse` with JWT.
8. Client uses bearer token to access authorized endpoints.

---

## Trade-offs and Alternatives

- Current approach keeps controllers thin and business rules in services.
- Email sending is integrated in login path, not registration path.
- `decodedToken` is computed but current code passes `token` to
  `ConfirmEmailAsync`; this is an implementation detail to review later if
  confirmation issues appear.
- `CreateJwtTokenAsync` currently uses `AddHours(_jwtSettings.DurationInDays)`;
  naming and unit may be refined for clarity.

---

## Scalability and Improvement Ideas

- Move email verification trigger to registration to shorten first login friction.
- Add retry/queue support for email sending.
- Add refresh token strategy for long-lived sessions.
- Add integration tests for register/login/confirm happy and error paths.

---

## Interview Questions + Answers

**Q1: Why extend `IdentityDbContext<ApplicationUser>`?**  
It keeps all Identity schema and workflows while allowing custom user fields.

**Q2: Why use a Result pattern in services?**  
It standardizes service outcomes and centralizes HTTP mapping in
`BaseApiController`.

**Q3: Why set `ClockSkew = TimeSpan.Zero`?**  
To remove default tolerance and enforce exact token expiration behavior.

---

## How to Present This Project Verbally

"This upgrade introduces ASP.NET Core Identity with a custom
`ApplicationUser`, EF Core identity context, startup migration/role seeding, JWT
authentication, and a Result pattern for consistent API responses. The auth flow
uses Mapster mapping, role assignment on registration, and email confirmation
before issuing JWT tokens."
