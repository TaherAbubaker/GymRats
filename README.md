# GymRats 🏋️

A simple ASP.NET MVC web app where users can sign up, log in, browse gym classes, and book a spot in one.

Built for our web programming course project by **Taher**, **Abood**, and **Yamin**.

---

## What it does

The whole app is one flow:

**Sign up → Log in → Browse classes → Book a class → See it in "My Bookings" → Cancel if needed → Log out**

That's it. No extra complexity — just standard create/read/delete pages wired together through a shared database.

---

## Tech Stack

- **Framework:** ASP.NET MVC
- **Database:** SQL Server + Entity Framework (Code First)
- **Auth:** Custom login using `HttpContext.Session` (no external auth library — the assignment specifically requires sessions, so we're doing it manually)

---

## Database — 3 Tables

### `Users`
| Column | Type | Notes |
|---|---|---|
| Id | int | Primary key |
| Name | string | |
| Email | string | |
| PasswordHash | string | Never store plain text passwords |

### `Classes`
| Column | Type | Notes |
|---|---|---|
| Id | int | Primary key |
| Name | string | e.g. "Spin Class" |
| Trainer | string | |
| Time | datetime | |

### `Bookings`
| Column | Type | Notes |
|---|---|---|
| Id | int | Primary key |
| UserId | int | Foreign key → `Users.Id` |
| ClassId | int | Foreign key → `Classes.Id` |

`Bookings` is just a link table — it's what makes "a user booked a class" a real thing in the database.

---

## Who Owns What

Each person owns **one Model + one Controller + its Views**, start to finish. Don't edit someone else's controller — if something needs to change, ask in the group chat first.

| Person | Owns | Model | Controller | Views folder |
|---|---|---|---|---|
| **Taher** | Users (sign up / login / logout / profile) | `Models/User.cs` | `Controllers/AccountController.cs` | `Views/Account/` |
| **Abood** | Classes (list / search / add) | `Models/Class.cs` | `Controllers/ClassController.cs` | `Views/Class/` |
| **Yamin** | Bookings (book / view / cancel) | `Models/Booking.cs` | `Controllers/BookingController.cs` | `Views/Booking/` |

**Shared files** (everyone can touch, but coordinate before big changes):
- `Data/ApplicationDbContext.cs` — the database connection, lists all three tables
- `Views/Shared/_Layout.cshtml` — the nav bar / page template

---

## Jira Tickets

| Ticket | Assignee | Task |
|---|---|---|
| GYM-1 | Shared | Set up project, database, and the three models |
| GYM-2 | Taher | Sign up page |
| GYM-3 | Taher | Login page + session |
| GYM-4 | Taher | Logout |
| GYM-5 | Taher | Profile page |
| GYM-6 | Abood | List all classes page |
| GYM-7 | Abood | Search classes by name |
| GYM-8 | Abood | Add class page |
| GYM-9 | Yamin | Book button + save booking |
| GYM-10 | Yamin | My Bookings page |
| GYM-11 | Yamin | Cancel booking |
| GYM-12 | Shared | Put it all together, test, style, submit |

### Order that avoids blocking anyone
1. Everyone: agree on the 3 tables above (already done — see this README).
2. Taher finishes GYM-2/3 first (Yamin needs `Session["UserId"]` to exist).
3. Abood finishes GYM-6/8 first (Yamin needs classes to exist to book them).
4. Yamin can build GYM-9/10/11 against fake/seeded data while waiting, then swap in the real session/class code once it lands.
5. GYM-12 (styling, testing, submitting) happens together at the end.

---

## Project Structure

```
GymRats/
├── Models/
│   ├── User.cs         ← Taher
│   ├── Class.cs        ← Abood
│   └── Booking.cs      ← Yamin
├── Controllers/
│   ├── AccountController.cs   ← Taher
│   ├── ClassController.cs     ← Abood
│   └── BookingController.cs   ← Yamin
├── Views/
│   ├── Account/        ← Taher's pages
│   ├── Class/          ← Abood's pages
│   ├── Booking/        ← Yamin's pages
│   └── Shared/         ← shared nav/layout
└── Data/
    └── ApplicationDbContext.cs   ← shared, lists all 3 tables
```

Controller/model files already exist as starting points with `TODO` comments marking exactly what to fill in — open your file and look for `TODO`.

---

## Getting Started

1. Clone the repo.
2. Open the solution in Visual Studio.
3. Update the connection string in `Web.config` to point at your local SQL Server.
4. Run `Add-Migration Initial` then `Update-Database` in the Package Manager Console to create the tables.
5. Run the project — it should open to the home page.

## Git Workflow

- One branch per person: `taher-auth`, `abood-classes`, `yamin-bookings`.
- Commit small, commit often.
- Open a pull request into `main` when a feature works — someone else on the team gives it a quick look before merging.
- Pull `main` before you start work each session so you're not merging stale code.

## Definition of Done (per ticket)
- The page loads with no errors.
- The action actually reads/writes the database (no more `TODO`s for that ticket).
- You tested it yourself by clicking through it once.

---

## Setting Up the Shared Database (SQL Server)

We're using **one live Azure SQL Database** that all three of us connect to, so everyone sees the same data (not just the same schema).

### One person does this (Taher):
1. Go to [portal.azure.com](https://portal.azure.com) — sign in with a student/free account (GitHub Student Pack gives Azure credit).
2. Create a resource → **SQL Database** → name it `GymRatsDb`, create a new **SQL Server** (this is the *server*, not to be confused with the app). Set an admin username/password — save these somewhere safe.
3. Under the new server's **Networking** settings, enable "Allow Azure services" and add a firewall rule allowing your team's IPs (or `0.0.0.0`–`255.255.255.255` for simplicity during development — just don't leave real user data in it).
4. Go to the database → **Connection strings** → copy the ADO.NET connection string.
5. Share that connection string with Abood and Yamin **privately** (Discord DM/WhatsApp) — never paste it in the group repo or a public channel.

### Everyone does this:
1. Copy `appsettings.Development.json.example` → rename to `appsettings.Development.json`.
2. Paste the real connection string into it.
3. This file is in `.gitignore` — it will **never** get pushed to GitHub. Good, because it has a real password in it.
4. Install NuGet packages (Tools → NuGet Package Manager, or Package Manager Console):
   ```
   Install-Package Microsoft.EntityFrameworkCore.SqlServer
   Install-Package Microsoft.EntityFrameworkCore.Tools
   ```
5. In Package Manager Console, run:
   ```
   Add-Migration InitialCreate
   Update-Database
   ```
   This creates the three tables in the shared Azure database. Only do this once as a team (whoever does it first, tell the others so they don't duplicate it).
6. Run the project — you're now reading/writing the same database as your teammates.

**Careful:** since it's a shared live database, if you drop/recreate tables or run a bad migration, it affects everyone. Talk in the group chat before running any migration.
