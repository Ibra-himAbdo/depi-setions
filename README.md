# DEPI — Section 2 (SWD5-S2) — Student Map

A guided tour of the **33 sessions** in this section: C# fundamentals all the way
to a layered ASP.NET Core MVC application with validation, full CRUD, toast
notifications, ASP.NET Core Identity (users, roles, seeding, login),
role-based authorization, a service layer with the Result pattern, a layered
Web API with EF Core, repositories, logging and error middleware, an AI chat
endpoint, and JWT authentication.

> **Branch:** `Depi-SHR5-SWD5-S2`
> Everything for this section lives here — one folder per session.

## Quick start

```bash
git clone -b Depi-SHR5-SWD5-S2 https://github.com/Ibra-himAbdo/depi-setions.git
cd depi-setions
```

Already cloned? Just switch:

```bash
git fetch origin
git switch Depi-SHR5-SWD5-S2
```

Open the folder you need in Visual Studio / VS Code / Rider, then:

```bash
# console sessions
dotnet run --project "01. ConsoleApp1/ConsoleApp1"

# web sessions
dotnet run --project "21. MVC1/Demo.MVC"
```

## The map

### Part A — C# language (sessions 01–09)

| # | Folder | What you learn |
| - | ------ | -------------- |
| 01 | `01. ConsoleApp1` | C# console app basics: variables, types, input/output |
| 02 | `02. CSharp2` | Conditions, loops, arrays, strings |
| 03 | `03. ConsoleProblems1` | Problem solving with the basics you just learned |
| 04 | `04. Csharp3` | C# fundamentals part 3: methods and structures |
| 05 | `05. CsharpOOP1` | OOP part 1: classes, objects, constructors, properties |
| 06 | `06. ConsoleApp1 - Copy` | Console practice variant — revision of session 01 |
| 07 | `07. CsharpOOP1 - Copy` | OOP practice variant (extra projects) |
| 08 | `08. ConsoleApp1 - Copy - Copy` | Another console variant — more exercises |
| 09 | `09. CsharpOOP1 - Copy - Copy` | Another OOP variant — more exercises |

### Part B — Advanced C# (sessions 10–17)

| # | Folder | What you learn |
| - | ------ | -------------- |
| 10 | `10. OOP2` | Inheritance, polymorphism, abstraction, interfaces |
| 11 | `11. MISC_CSharp_1` | Miscellaneous language features |
| 12 | `12. MISC_CSharp_2` | Reflection + a multi-language translation demo (`MainApplication`, `*Translation`, `Translation.Abstractions`, `ReflictionDemo1/2`) |
| 13 | `13. Generics` | Generic types & methods, plus `Collections` |
| 14 | `14. Delegates` | Delegates, events and `HelpersMethods` |
| 15 | `15. LINQ` | Query & method syntax with `LINQ`, `LINQ_P2`, `SampleData` |
| 16 | `16. FromAdoToDapper` | Data access: raw ADO.NET → Dapper (`Ado_Net`) |
| 17 | `17. EfCore` | Entity Framework Core + database-first scaffolding |

### Part C — Web & API (sessions 18–33)

| # | Folder | What you learn |
| - | ------ | -------------- |
| 18 | `18. HTML&CSS` | HTML structure, CSS styling, assets in `images/` |
| 19 | `19. ApiCall` | Calling HTTP APIs from a C# client |
| 20 | `20. Js` | JavaScript basics: DOM, events, functions |
| 21 | `21. MVC1` | ASP.NET Core MVC: controllers, views, routing (`Demo.MVC`) |
| 22 | `22. MVC2` | Views & layouts, tag helpers, ToDo sample (`Demo.MVC.Views`, `Demo.Todo`) |
| 23 | `23. MVC3` | Layered MVC: `Demo.DAL` → `Demo.BLL` → `Demo.MVC` with EF Core |
| 24 | `24. MVC4` | Full CRUD, FluentValidation, DbContext, validation scripts |
| 25 | `25. MVC5` | Reusable form partials + toast notifications (`hcg-toast`), `TempData`-driven messages |
| 26 | `26. MVC6` | ASP.NET Core Identity persistence: `AppUser`, `AddIdentityTables` migration, admin user management (`AdminsController` + Create/Edit/Index) |
| 27 | `27. MVC7` | Identity login flow (`AccountsController`, `LoginViewModel`), user ↔ department relation, admin change-password |
| 28 | `28. MVC8` | Role seeding + default admin, `[Authorize(Roles = ...)]`, user roles tag helper, cascading state/city dropdowns, profile image upload |
| 29 | `29. MVC9` | Business service layer (`IDepartmentService`), `Result` / `Error` models, `BaseController` error handling |
| 30 | `30.API` | ASP.NET Core Web API: `TodoItemsController` CRUD, `CreateTodoItemDto` validation, `ApiResponse`, status-code error endpoint, Bruno requests |
| 31 | `31.API` | Layered API: `Demo.Core` / `Demo.Infrastructure` / `Demo.Services` / `Demo.API`, EF Core + SQL Server, generic repository, migrations + seeding, exception middleware, Serilog, Scalar |
| 32 | `32.API` | Unit of work + transaction, Mapster mapping, category + todo item endpoint, AI chat endpoint (Groq), HTTP client factory |
| 33 | `33.API` | JWT authentication: ASP.NET Core Identity, `AppUser`, `AddIdentityTables` migration, `TokenService`, login endpoint, `[Authorize]`, bearer scheme in Scalar, AI tools over the database |

### Session 31 in a nutshell

`31.API` turns the single-project demo into a layered, database-backed API:

- **`Demo.Core`** — entities (`BaseEntity`, `Category`, `TodoItem`) and repository contracts.
- **`Demo.Infrastructure`** — EF Core `AppDbContext`, `GenericRepository<T>`, `TodoItemRepository` with eager loading (`Include`), and the initial migration.
- **`Demo.Services`** — reserved for business logic in the next session.
- **`Demo.API`** — controllers, DTOs, exception middleware, Serilog and Scalar.
- **Database** — SQL Server; `DatabaseMigrator` applies migrations and seeds the three categories on startup.
- **Errors** — one `ApiResponse` shape for validation, 404 and unhandled exceptions (stack trace only in development).
- **Logging** — Serilog writes to the console and to per-level daily files under `Logs/`.
- **API docs** — Scalar UI at `/scalar/v1`; `ReferenceHandler.IgnoreCycles` stops entity navigation loops.

### Session 32 in a nutshell

`32.API` adds practical patterns on top of the layered API:

- **Unit of work** — `IUnitOfWork` exposes `CommitAsync`, `Repository<T>()` and `CustomRepository<TRepo>()`, so one `SaveChanges` call ends a request.
- **Transactions** — `ExecuteInTransactionAsync` wraps the category + todo item creation; any failure rolls both changes back.
- **Mapster** — `MapConfigs` maps `TodoItem` to `TodoItemToReturnDto` (renaming `Title` to `Name` and flattening `Category` to its name) with `Adapt<T>()`.
- **AI chat** — `AisController` calls a Groq-hosted model through `IHttpClientFactory`; add your own key where the code says `<your api key>`.

### Session 33 in a nutshell

`33.API` adds real authentication to the API:

- **Identity** — `AppDbContext` now extends `IdentityDbContext<AppUser>`; the `AddIdentityTables` migration creates the Identity tables.
- **JWT** — `TokenService` builds a signed token from the user's id, email, name and roles; `Jwt:SecretKey` and `Jwt:DurationInMinuties` live in `appsettings.json`.
- **Login** — `POST /api/AuthContoller/login` checks the password with `UserManager` and returns the token and its expiry.
- **Authorization** — `UseAuthentication()` + `[Authorize(Roles = "Admin,User")]` protect endpoints; Scalar shows the bearer padlock.
- **Seeding** — startup creates the `Admin` and `User` roles and an `admin@demo.com` user.
- **AI tools** — `AisController` v2 uses `Microsoft.Extensions.AI` with function calling over the repositories (read categories, read todo items, mark a task completed).
- **Secrets** — the API key and JWT secret are placeholders; replace `<your api key>` and `<your jwt secret key>` locally and never commit real values.

## Suggested study order

```
C# basics ─► OOP ─► advanced C# ─► data access ─► frontend ─► API ─► MVC/API
   01–05      05–10      10–15         16–17      18,20     19     21–33
```

Sessions 06–09 repeat sessions 01–05 with variations. Use them as **revision**
and extra practice, not as new topics.

## What to look for in the web sessions

| Session | Key idea |
| ------- | -------- |
| 21 | Controller → View, `_Layout`, Razor, routing |
| 22 | Layouts, partials, tag helpers, passing models to views |
| 23 | Layering: DAL (EF Core) → BLL → MVC (separation of concerns) |
| 24 | Validation (FluentValidation + unobtrusive client-side), full Create/Read/Update/Delete |
| 25 | Reusing UI: `DepartmentFormFiledPartial` for Create/Edit + toast messages via `TempData` and `hcg-toast` |
| 26 | Authentication data: Identity tables in EF Core, `AppUser`, admin CRUD for users |
| 27 | Full auth flow: login/logout, password change, assigning a user to a department |
| 28 | Roles & seeding, `[Authorize(Roles = ...)]`, a custom tag helper, AJAX cascading dropdowns, file upload |
| 29 | Service layer over repositories, `Result` / `Error` instead of exceptions, shared `BaseController` |
| 30 | Web API: controllers + routes, DTOs with data annotations, `ApiResponse` for errors, `UseStatusCodePagesWithReExecute`, testing with Bruno |
| 31 | Layered API: Core / Infrastructure / Services / API, EF Core repositories, migrations + seed data, exception middleware, Serilog, Scalar |
| 32 | Unit of work + transactions, Mapster DTO mapping, AI chat endpoint with `IHttpClientFactory` |
| 33 | JWT auth: Identity tables, `TokenService`, login endpoint, `[Authorize]`, Scalar bearer scheme, AI function calling |

## Repo layout

```
Depi-SHR5-SWD5-S2/          ← you are here (sessions 01–33)
├── 01. ConsoleApp1/
├── ...
├── 33.API/
├── .gitignore
├── LICENSE
└── README.md               ← this map
```

Other branches in this repository:

| Branch | Contents |
| ------ | -------- |
| `Depi-SHR5-SWD5-S1` | sister section — sessions 01–28 |
| `Depi-SHR5-SWD5-S2` | **this section** — sessions 01–33 |
| `Depi-SHR4-SWD5` | graduated group — archived |
| `main` | repo map + shared materials (`materials/`) |

> Shared interactive HTML labs (EF Core tracking, sync/async, middleware,
> architecture, onion API) live on `main` under
> [`materials/`](https://github.com/Ibra-himAbdo/depi-setions/tree/main/materials).
> They apply to both sections.

## License

See [LICENSE](LICENSE).
