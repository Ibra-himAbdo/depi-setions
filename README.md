# DEPI — Section 1 (SWD5-S1) — Student Map

A guided tour of the **19 sessions** in this section: from C# basics to a layered
ASP.NET Core MVC application.

> **Branch:** `Depi-SHR5-SWD5-S1`
> Everything for this section lives here — one folder per session.

## Quick start

```bash
git clone -b Depi-SHR5-SWD5-S1 https://github.com/Ibra-himAbdo/depi-setions.git
cd depi-setions
```

Already cloned? Just switch:

```bash
git fetch origin
git switch Depi-SHR5-SWD5-S1
```

Open the folder you need in Visual Studio / VS Code / Rider, then:

```bash
# console sessions
dotnet run --project "01. Csharp1/Csharp1"

# web sessions
dotnet run --project "17. MVC1/Demo.MVC"
```

## The map

### Part A — C# language (sessions 01–08)

| # | Folder | What you learn |
| - | ------ | -------------- |
| 01 | `01. Csharp1` | C# basics: variables, types, operators, conditions |
| 02 | `02. Csharp2` | Loops, arrays, strings, methods |
| 03 | `03. Csharp3Function` | Functions/methods in depth |
| 04 | `04. Csharp3Function - Copy` | Second take on the same session (review + practice) |
| 05 | `05. CsharpFunction2` | More function practice: parameters, overloading, recursion |
| 06 | `06. OOP1` | Classes, objects, constructors, properties |
| 07 | `07. OOP2` | Inheritance, polymorphism, abstraction, interfaces |
| 08 | `08. MISC_CSharp_1` | Assorted language features you'll meet in real code |

### Part B — Advanced C# (sessions 09–14)

| # | Folder | What you learn |
| - | ------ | -------------- |
| 09 | `09. ReflictionAndExceptionHandling` | Reflection, exception handling, and a plug-in style translator demo (`Refliction`, `Refliction.Lib1`, `TranslationProvider`, `TranslatorProject`) |
| 10 | `10. Generics` | Generic types & methods, plus `Collections` |
| 11 | `11. Delegates` | Delegates, events, callbacks |
| 12 | `12. LINQ` | Query syntax, method syntax, `LINQ` / `LINQP2` with `SampleData` |
| 13 | `13. FromAdoToDapper` | Data access from raw ADO.NET to Dapper |
| 14 | `14. EfCore` | Entity Framework Core + database-first scaffolding (`EfCore_Scaffolding`) |

### Part C — Web (sessions 15–19)

| # | Folder | What you learn |
| - | ------ | -------------- |
| 15 | `15. HTML&CSS` | HTML structure, CSS styling, assets in `images/` |
| 16 | `16. JS` | JavaScript basics: DOM, events, functions |
| 17 | `17. MVC1` | ASP.NET Core MVC: controllers, views, routing (`Demo.MVC`) |
| 18 | `18. MVC2` | Views & layouts, tag helpers, ToDo sample (`Demo.View`, `Demo.ToDo`) |
| 19 | `19. MVC3` | Layered MVC: `Demo.DAL` → `Demo.BLL` → `Demo.MVC` with EF Core |

## Suggested study order

```
C# basics ─► OOP ─► advanced C# ─► data access ─► HTML/CSS/JS ─► MVC
   01–05      06–07      08–12         13–14         15–16       17–19
```

Do the sessions in order — every web session reuses concepts from the C# ones.
The `- Copy` folders (04, and in other sections) are alternative takes on the same
lesson; use them for revision, not as a separate topic.

## Repo layout

```
Depi-SHR5-SWD5-S1/          ← you are here (sessions 01–19)
├── 01. Csharp1/
├── ...
├── 19. MVC3/
├── .gitignore
├── LICENSE
└── README.md               ← this map
```

Other branches in this repository:

| Branch | Contents |
| ------ | -------- |
| `Depi-SHR5-SWD5-S1` | **this section** — sessions 01–19 |
| `Depi-SHR5-SWD5-S2` | sister section — sessions 01–24 (includes `ApiCall` and `MVC4`) |
| `Depi-SHR4-SWD5` | previous ASP.NET Core track (sections 1–10: JWT, Serilog, Hangfire, AI, Specifications…) |
| `main` | repo map / entry point only |

## License

See [LICENSE](LICENSE).
