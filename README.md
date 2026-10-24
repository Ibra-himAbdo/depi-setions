# DEPI — ASP.NET Core Track (Archive) — Student Map

The older **ASP.NET Core / Clean Architecture** track, kept here in full.
Each folder is a complete, runnable solution that builds on the previous one.

> **Branch:** `Depi-SHR4-SWD5`
> This is the archive branch: everything from the original repository lives here,
> including full git history. The C# fundamentals sections are on the
> `Depi-SHR5-SWD5-S1` and `Depi-SHR5-SWD5-S2` branches.

## Quick start

```bash
git clone -b Depi-SHR4-SWD5 https://github.com/Ibra-himAbdo/depi-setions.git
cd depi-setions
```

Already cloned? Just switch:

```bash
git fetch origin
git switch Depi-SHR4-SWD5
```

Every project is a .NET solution — open the `.slnx` / project folder in Visual
Studio or run it:

```bash
dotnet run --project "1.Demo.Base/src/Application"
```

## The map

### Stage 1 — Building the API (1 → 3)

| # | Folder | What you learn |
| - | ------ | -------------- |
| 1 | `1.Demo.Base` | Base Web API: controllers, DI, configuration, EF Core, clean project layout |
| 2 | `2.Demo.Serilog` | Structured logging with Serilog (console + file sinks) |
| 3 | `3.Demo.JWT` | Authentication with JWT: register/login, tokens, roles, authorization |

### Stage 2 — Background work & the front end (4 → 6)

| # | Folder | What you learn |
| - | ------ | -------------- |
| 4 | `4.Demo.Background_Jobs` | Hangfire: fire-and-forget, delayed and recurring jobs, dashboard |
| 5.1 | `5.1.Demo.Ajax - without AuthLayout` | AJAX login/register against the API (basic layout) |
| 5.2 | `5.2.Demo.Ajax - With AuthLayout` | Same idea with a dedicated auth layout |
| 5.3 | `5.3.Demo.Ajax - extra ex` | Extra AJAX examples |
| 6 | `6.Demo.Localization&Globalization` | Resources, `.resx`, culture switching, RTL/Arabic |

### Stage 3 — Integrations (7)

| # | Folder | What you learn |
| - | ------ | -------------- |
| 7.1 | `7.1.Demo.AiIntegration - http` | Calling an AI/chat API over HTTP from ASP.NET Core |
| 7.2 | `7.2.Demo.AiIntegration - advanced` | Streaming, chat UI widget, prompt handling |

### Stage 4 — Data access patterns (8)

| # | Folder | What you learn |
| - | ------ | -------------- |
| 8 | `8.Demo.(IGenericRepository - IUnitOfWork - CustomRepository)` | Generic repository, Unit of Work, custom repositories, specifications-ready persistence |

Snapshots of this stage (kept for reference, same code at different dates):

- `… - Section 6-5-2026`
- `… - Section 8-5-2026`
- `… -AI UI - Section 8-5-2026`

### Stage 5 — Query shaping & Specifications (9 → 10)

| # | Folder | What you learn |
| - | ------ | -------------- |
| 9 | `9.Demo.Projection` | DTO projection, `Select`, AutoMapper-style mapping |
| 9 | `9.Demo.Specifications - section 13-5-2026` | Specification pattern: filtering, includes, pagination |
| 9 | `9.Demo.Specifications - section 15-5-2026 -- final` | Final version: specifications + projection + paging metadata |
| 10 | `10.Demo.Specifications` | Latest specification-pattern project |

### Extras

| Path | What it is |
| ---- | ---------- |
| `callApiByC#` | Console client that consumes the API (HttpClient) |
| `*.rar` / `*.7z` | Archived snapshots of older sections |
| `.agents/` | Tooling/config files, not part of the lessons |

## Suggested order

```
API basics ─► logging ─► auth ─► background jobs ─► AJAX front end ─► localization
    1           2         3            4                 5                6
                                   │
              AI integration ──────┤
                 7.1–7.2            │
                                    ▼
              Repository / UoW ─► Projection ─► Specifications ─► (10) latest
                     8               9 (13-5)     9 (15-5 final)
```

The dated folders (`Section 6-5-2026`, `Section 8-5-2026`, `13-5-2026`,
`15-5-2026 -- final`) are **snapshots**, not new topics — use the latest version
of a stage when you study, and the snapshots only to compare changes.

## Repo layout (top level)

```
1.Demo.Base/ … 10.Demo.Specifications/   ← lessons, in order
callApiByC#/                             ← console API client
*.rar / *.7z                             ← archived snapshots
.agents/                                 ← tooling
LICENSE
README.md                                ← this map
```

Other branches in this repository:

| Branch | Contents |
| ------ | -------- |
| `Depi-SHR4-SWD5` | **this archive** — the full ASP.NET Core track |
| `Depi-SHR5-SWD5-S1` | C# → MVC track, sessions 01–19 |
| `Depi-SHR5-SWD5-S2` | C# → MVC track, sessions 01–24 |
| `main` | repo map / entry point only |

## License

See [LICENSE](LICENSE).
