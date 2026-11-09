# Shared materials

Interactive HTML labs for **both sections** (S1 and S2). Each file is
self-contained: open it in any modern browser — no install, no server, no build.

| # | File | Topic |
| - | ---- | ----- |
| 01 | [EF Core change tracking](01-ef-core-change-tracking.html) | How EF Core tracks entities: states, `Add` / `Update` / `Remove`, `AsNoTracking`, async |
| 02 | [Sync vs async](02-sync-vs-async.html) | Blocking vs non-blocking code, `async` / `await`, when it actually helps |
| 03 | [Middleware pipeline](03-middleware-pipeline.html) | The request pipeline, order of middleware, authN / authZ, where MVC fits |
| 04 | [Architecture thinking lab](04-architecture-thinking-lab.html) | Turning business rules into entities, operations, validation and requests |
| 05 | [ASP.NET Core API & onion architecture](05-aspnet-api-onion-architecture.html) | REST APIs, controllers vs minimal APIs, layers and dependency direction |

## How to open

- **Windows:** double-click the file, or run `start 01-ef-core-change-tracking.html`
- **macOS:** `open 01-ef-core-change-tracking.html`
- **Linux:** `xdg-open 01-ef-core-change-tracking.html`

You can also drag the file into a browser tab.

## Notes

- The explanations are in Arabic; code and technical terms stay in English.
- The labs load Google Fonts. Offline, the browser falls back to system fonts —
  the content still works.
- These labs are not tied to one session. They are extra material for both
  sections, so they live on `main` instead of a section branch.
