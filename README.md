# DEPI Sections — Student Map

Training material for the DEPI .NET track, split into branches so each group
gets exactly the sessions they need.

> **This `main` branch holds the map and the shared materials.** The session
> code lives on the branches below.

## Which branch do I need?

| Branch | Who it's for | Contents |
| ------ | ------------ | -------- |
| [`Depi-SHR5-SWD5-S1`](https://github.com/Ibra-himAbdo/depi-setions/tree/Depi-SHR5-SWD5-S1) | **Section 1 (SWD5-S1)** | 25 sessions: C# basics → OOP → advanced C# → ADO.NET/Dapper → EF Core → HTML/CSS/JS → MVC1–MVC8 (Identity, roles, services, file upload) → Web API |
| [`Depi-SHR5-SWD5-S2`](https://github.com/Ibra-himAbdo/depi-setions/tree/Depi-SHR5-SWD5-S2) | **Section 2 (SWD5-S2)** | 30 sessions: same track extended with `ApiCall`, MVC4 (FluentValidation + full CRUD), MVC5 (toasts + form partials), MVC6–MVC9 (Identity, roles, file upload, service layer + `Result` pattern), and a Web API |
| `Depi-SHR4-SWD5` | graduated group | archived work, kept for the record |
| `main` | everyone | the map + shared materials |

**Start here if you are in Section 1 → `Depi-SHR5-SWD5-S1`.**
**Section 2 → `Depi-SHR5-SWD5-S2`.**

## Get the code

Pick your branch:

```bash
# Section 1
git clone -b Depi-SHR5-SWD5-S1 https://github.com/Ibra-himAbdo/depi-setions.git

# Section 2
git clone -b Depi-SHR5-SWD5-S2 https://github.com/Ibra-himAbdo/depi-setions.git
```

Already have the repo? Switch instead of cloning again:

```bash
git fetch origin
git switch Depi-SHR5-SWD5-S1    # or Depi-SHR5-SWD5-S2
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
└── 30.API/                  ← session 30
```

## Shared materials (both sections)

The [`materials/`](materials/) folder holds interactive HTML labs that apply to
both sections. Open any file in your browser — no setup needed.

| Lab | Topic |
| --- | ----- |
| [EF Core change tracking](materials/01-ef-core-change-tracking.html) | Entity states, `Add` / `Update` / `Remove`, `AsNoTracking`, async |
| [Sync vs async](materials/02-sync-vs-async.html) | Blocking vs non-blocking code, `async` / `await` |
| [Middleware pipeline](materials/03-middleware-pipeline.html) | Pipeline order, authN / authZ, where MVC fits |
| [Architecture thinking](materials/04-architecture-thinking-lab.html) | Business rules → entities, operations, validation, requests |
| [ASP.NET Core API & onion architecture](materials/05-aspnet-api-onion-architecture.html) | REST APIs, controllers vs minimal APIs, layers |

See [`materials/README.md`](materials/README.md) for details.

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
C# basics ─► OOP ─► advanced C# ─► data access ─► HTML/CSS/JS ─► MVC/API
   01–05      06–07      08–12         13–14         15–16       17–25
```

**Section 2 — `Depi-SHR5-SWD5-S2`**

```
C# basics ─► OOP ─► advanced C# ─► data access ─► frontend ─► API ─► MVC/API
   01–05      05–10      10–15         16–17      18,20     19     21–30
```

## Tips for students

- Work through the sessions **in order** — each one reuses the previous concepts.
- Folders named `- Copy` / `- Copy - Copy` are **revision variants**, not new topics.
- Each session folder is self-contained: open it, run it, break it, fix it.
- The HTML labs in [`materials/`](materials/) open directly in a browser — no .NET install needed.
- Commit your own experiments in your fork — the branch history is your syllabus.

## License

See [LICENSE](LICENSE).
