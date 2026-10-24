# depi-setions

DEPI training sessions — ASP.NET Core demos, section by section.

> **📦 The code is not on this branch.**
>
> All source code, demos, archives and full commit history live on the
> **`Depi-SHR4-SWD5`** branch. This `main` branch intentionally contains only
> this README, `LICENSE` and `.gitignore`.

## Where did the content go?

Everything moved to the **`Depi-SHR4-SWD5`** branch, which is a full clone of the
former `main` (same commits, same history — nothing was rewritten or lost).

## How to get it

Already have the repo cloned? Just switch branches:

```bash
git fetch origin
git switch Depi-SHR4-SWD5
```

Fresh clone of the code branch:

```bash
git clone -b Depi-SHR4-SWD5 https://github.com/Ibra-himAbdo/depi-setions.git
```

## What is on `Depi-SHR4-SWD5`

Progressive ASP.NET Core / Clean Architecture training projects, each one building
on the previous session:

| Path | Topic |
| --- | --- |
| `1.Demo.Base` | Base Web API project |
| `2.Demo.Serilog` | Structured logging with Serilog |
| `3.Demo.JWT` | JWT authentication |
| `4.Demo.Background_Jobs` | Hangfire background jobs (also shipped as `.7z`) |
| `5.1.Demo.Ajax - without AuthLayout` | AJAX auth, basic layout |
| `5.2.Demo.Ajax - With AuthLayout` | AJAX auth with auth layout |
| `5.3.Demo.Ajax - extra ex` | Extra AJAX examples |
| `6.Demo.Localization&Globalization` | Localization & globalization |
| `7.1.Demo.AiIntegration - http` | AI integration over HTTP |
| `7.2.Demo.AiIntegration - advanced` | Advanced AI integration |
| `8.Demo.(IGenericRepository - IUnitOfWork - CustomRepository)` | Repository / Unit of Work pattern |
| `8.Demo.(…)- Section 6-5-2026` | Snapshot of the above, 6 May 2026 (also `.7z`) |
| `8.Demo.(…)- Section 8-5-2026` | Snapshot, 8 May 2026 |
| `8.Demo.(…)-AI UI - Section 8-5-2026` | Snapshot with AI UI, 8 May 2026 |
| `9.Demo.Projection` | DTO projection |
| `9.Demo.Specifications - section 13-5-2026` | Specification pattern, 13 May 2026 |
| `9.Demo.Specifications - section 15-5-2026 -- final` | Final specification-pattern version, 15 May 2026 |
| `10.Demo.Specifications` | Latest specification-pattern project |
| `callApiByC#` | C# console client consuming the API |
| `*.rar` / `*.7z` | Archived snapshots of older sections |

## History

The `Depi-SHR4-SWD5` branch starts from the same root commit and keeps every
commit that was ever on `main` (from `Initial commit` through
`spec + projection and final changes`). Nothing was squashed, and the files are
still reachable from `main`'s history as well if you ever need to look back.

## License

See [LICENSE](LICENSE).
