# DEPI Sections — Student Map

Training material for the DEPI .NET track, split into branches so each group
gets exactly the sessions they need.

> **This `main` branch is only the map.** The code lives on the branches below.

## Which branch do I need?

| Branch | Who it's for | Contents |
| ------ | ------------ | -------- |
| [`Depi-SHR5-SWD5-S1`](https://github.com/Ibra-himAbdo/depi-setions/tree/Depi-SHR5-SWD5-S1) | **Section 1 (SWD5-S1)** | 19 sessions: C# basics → OOP → advanced C# → ADO.NET/Dapper → EF Core → HTML/CSS/JS → MVC1–MVC3 |
| [`Depi-SHR5-SWD5-S2`](https://github.com/Ibra-himAbdo/depi-setions/tree/Depi-SHR5-SWD5-S2) | **Section 2 (SWD5-S2)** | 24 sessions: same track extended with `ApiCall` and MVC4 (FluentValidation + full CRUD) |
| [`Depi-SHR4-SWD5`](https://github.com/Ibra-himAbdo/depi-setions/tree/Depi-SHR4-SWD5) | **ASP.NET Core track (archive)** | 10 advanced stages: base API, Serilog, JWT, Hangfire, AJAX auth, localization, AI integration, Repository/UoW, Projection, Specifications |
| `main` | everyone | this map only |

**Start here if you are in Section 1 → `Depi-SHR5-SWD5-S1`.**
**Section 2 → `Depi-SHR5-SWD5-S2`.**
The `Depi-SHR4-SWD5` branch is the earlier, more advanced ASP.NET Core track —
useful after you finish the MVC sessions.

## Get the code

Pick your branch:

```bash
# Section 1
git clone -b Depi-SHR5-SWD5-S1 https://github.com/Ibra-himAbdo/depi-setions.git

# Section 2
git clone -b Depi-SHR5-SWD5-S2 https://github.com/Ibra-himAbdo/depi-setions.git

# ASP.NET Core archive
git clone -b Depi-SHR4-SWD5 https://github.com/Ibra-himAbdo/depi-setions.git
```

Already have the repo? Switch instead of cloning again:

```bash
git fetch origin
git switch Depi-SHR5-SWD5-S1    # or S2, or Depi-SHR4-SWD5
```

## How the material is organised

Every lesson is one numbered folder, added in its own commit, so you can follow
the history like a timeline:

```bash
git log --oneline            # one commit per session
git show <commit> --stat     # what changed in that session
```

Example folder:

```
Depi-SHR5-SWD5-S2/
├── 01. ConsoleApp1/         ← session 1
├── 02. CSharp2/             ← session 2
├── ...
└── 24. MVC4/                ← session 24
```

## Running a lesson

```bash
# console lessons
dotnet run --project "01. ConsoleApp1/ConsoleApp1"

# MVC lessons
dotnet run --project "21. MVC1/Demo.MVC"
```

Requirements: [.NET SDK](https://dotnet.microsoft.com/download) (the MVC
projects target .NET 10; install the matching SDK) and a code editor
(Visual Studio 2022+, VS Code, or Rider). SQL Server / LocalDB is needed for the
EF Core lessons.

## Learning paths at a glance

**Section 1 — `Depi-SHR5-SWD5-S1`**

```
C# basics ─► OOP ─► advanced C# ─► data access ─► HTML/CSS/JS ─► MVC
   01–05      06–07      08–12         13–14         15–16       17–19
```

**Section 2 — `Depi-SHR5-SWD5-S2`**

```
C# basics ─► OOP ─► advanced C# ─► data access ─► frontend ─► API ─► MVC
   01–05      05–10      10–15         16–17      18,20     19     21–24
```

**ASP.NET Core archive — `Depi-SHR4-SWD5`**

```
API ─► Serilog ─► JWT ─► Hangfire ─► AJAX UI ─► Localization
 1        2        3        4          5            6
                          │
        AI ───────────────┘
       7.1–7.2
                          ▼
   Repository/UoW ─► Projection ─► Specifications ─► latest
         8              9              9 (final)        10
```

## Tips for students

- Work through the sessions **in order** — each one reuses the previous concepts.
- Folders named `- Copy` / `- Copy - Copy` are **revision variants**, not new topics.
- Dated folders in the archive (`Section 6-5-2026`, `13-5-2026`, `15-5-2026 -- final`…)
  are **snapshots** of the same lesson at different points; use the latest one.
- Each session folder is self-contained: open it, run it, break it, fix it.
- Commit your own experiments in your fork — the branch history is your syllabus.

## License

See [LICENSE](LICENSE).
