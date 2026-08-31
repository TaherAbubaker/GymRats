# GymRats 🏋️

GymRats is a simple gym class booking website. Create an account, browse the class schedule, and reserve your spot — all in one place.

## What you can do

- **Sign up** for an account
- **Log in / log out**
- **Browse and search** gym classes by name or trainer
- **Book a class** with one click
- **View your bookings** — see upcoming and past sessions, cancel anytime
- **View your profile** — see your info and your role
- **Change your password**
- **Admins only:** add new classes to the schedule

## Try it out

An admin account is already set up so you (or anyone reviewing this project) can explore every feature, including adding new classes:

```
Email:    admin@gmail.com
Password: admin123
```

Log in with a regular account (just sign up with any email) to see the member-side experience, or use the admin account above to see the full picture, including the "Add New Class" button on the Classes page.

## Running it locally

1. Clone the repo and open `GymRats.sln` in Visual Studio.
2. Make sure **SQL Server Express LocalDB** is available (it ships with Visual Studio's ASP.NET workload). If the database update step fails with a connection error, open a terminal and run:
   ```
   sqllocaldb start mssqllocaldb
   ```
   then try again.
3. In the Package Manager Console, run:
   ```
   Update-Database
   ```
   This creates the database, including the seeded admin account above.
4. Run the project (F5) and open the site in your browser.

## Tech Stack

- ASP.NET Core MVC
- Entity Framework Core + SQL Server (LocalDB)
- Session-based authentication
