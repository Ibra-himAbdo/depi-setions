## Fix CSS Isolation Bundle Link
**Problem:**
The styles defined in `_Layout.cshtml.css` were not being applied to the application layout, resulting in missing design elements like navbar styling and footer positioning.

**Root Cause:**
The `_Layout.cshtml` file was referencing `~/Application.Client.styles.css`. In ASP.NET Core, when a Razor Class Library with CSS isolation is referenced by a host project, all isolated styles are bundled into a single file named after the host project's assembly, which is `Application.styles.css`.

**Solution:**
Updated the stylesheet link in `_Layout.cshtml` to point to the correct host bundle.

**Code:**
```html
<link rel="stylesheet" href="~/Application.styles.css" asp-append-version="true" />
```
