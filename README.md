# Workforce Console - C# Database Application

## Overview

Workforce Console is a .NET 10 command-line application that solves a real organizational need: maintaining a reliable, persistent record of employees and the departments they belong to. Rather than relying on spreadsheets or ad-hoc text files, it gives operators a structured, menu-driven interface to create, view, update, and delete workforce data backed by a SQLite database.

The application manages two related entities — employees and departments — with a one-to-many relationship. It enforces referential integrity at the service layer (e.g., validating that a department exists before assigning an employee to it) and supports flexible modeling through optional department assignments, reflecting how workforce data works in practice.

Architecturally, the project applies a service layer pattern to cleanly separate business logic from the UI, with constructor-injected `DbContext` enabling testability without coupling to a live database. Entity Framework Core handles schema migrations and object-relational mapping, keeping data access expressive and type-safe. The service layer is covered by an xUnit test suite with Coverlet for code coverage tracking.

This project was built to demonstrate practical proficiency in .NET backend development: relational data modeling, layered application architecture, dependency injection, database migrations, and unit testing — all applied within a focused, real-world domain.


## Development Environment

### Tools Used
- **Visual Studio Code** - Primary code editor with C# extensions
- **.NET 10.0 SDK** - Cross-platform development framework
- **Entity Framework Core CLI** - Database migration and management tools
- **SQLite** - Lightweight, serverless database engine
- **Git** - Version control system

### Programming Language and Libraries
- **C#** - Primary programming language utilizing modern syntax features
- **Entity Framework Core 9.0.7** - Object-relational mapping (ORM) framework
- **Microsoft.EntityFrameworkCore.Sqlite** - SQLite database provider
- **Microsoft.EntityFrameworkCore.Design** - EF Core design-time tools
- **xUnit 2.9.3** - Unit testing framework
- **Coverlet** - Code coverage collection for .NET
- **System.ComponentModel.DataAnnotations** - Data validation attributes
- **System.IO** - File system operations for database path management

## Useful Websites

- [Microsoft C# Documentation](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [SQLite Documentation](https://www.sqlite.org/docs.html)
- [.NET CLI Documentation](https://docs.microsoft.com/en-us/dotnet/core/tools/)
- [C# Naming Conventions](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/naming-guidelines)
- [Entity Framework Core Migrations](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/)

## Future Work

- **User Authentication System** - Implement secure login/logout functionality with role-based access control
- **Advanced Search and Filtering** - Add search capabilities by name, department, and other criteria
- **Data Export/Import** - Implement CSV/Excel export and import functionality
- **Audit Logging** - Track all changes made to employee and department records
- **Web API Integration** - Create RESTful API endpoints for web-based access
- **Configuration Management** - Implement appsettings.json for database connection strings
- **Performance Optimization** - Add pagination for large datasets and query optimization
- **Data Validation Enhancement** - Implement more robust input validation and business rules
- **Reporting Features** - Add employee statistics, department summaries, and custom reports 
