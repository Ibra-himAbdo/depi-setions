# DEPI — Section 2 (SWD5-S2) — Student Map

A guided tour of the **25 sessions** in this section: C# fundamentals all the way
to a layered ASP.NET Core MVC application with validation, full CRUD and toast
notifications.

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

### Part C — Web (sessions 18–24)

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

## Suggested study order

```
C# basics ─► OOP ─► advanced C# ─► data access ─► frontend ─► API ─► MVC
   01–05      05–10      10–15         16–17      18,20     19     21–25
```

Sessions 06–09 repeat sessions 01–05 with variations. Use them as **revision**
and extra practice, not as new topics.

## What to look for in the MVC sessions

| Session | Key idea |
| ------- | -------- |
| 21 | Controller → View, `_Layout`, Razor, routing |
| 22 | Layouts, partials, tag helpers, passing models to views |
| 23 | Layering: DAL (EF Core) → BLL → MVC (separation of concerns) |
| 24 | Validation (FluentValidation + unobtrusive client-side), full Create/Read/Update/Delete |
| 25 | Reusing UI: `DepartmentFormFiledPartial` for Create/Edit + toast messages via `TempData` and `hcg-toast` |

## Repo layout

```
Depi-SHR5-SWD5-S2/          ← you are here (sessions 01–25)
├── 01. ConsoleApp1/
├── ...
├── 25. MVC5/
├── .gitignore
├── LICENSE
└── README.md               ← this map
```

Other branches in this repository:

| Branch | Contents |
| ------ | -------- |
| `Depi-SHR5-SWD5-S1` | sister section — sessions 01–19 |
| `Depi-SHR5-SWD5-S2` | **this section** — sessions 01–25 |
| `Depi-SHR4-SWD5` | graduated group — archived |
| `main` | repo map / entry point only |

## License

See [LICENSE](LICENSE).
