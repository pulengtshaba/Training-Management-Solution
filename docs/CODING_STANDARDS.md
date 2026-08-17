# CODING_STANDARDS.md

# Training Management System

## Coding Standards and Development Guidelines

Version: 1.0

Language: C#

Framework: ASP.NET Core

Architecture: Clean Architecture (Modular Monolith)

---

# 1. Purpose

This document defines the coding standards used throughout the Training Management System (TMS).

The objectives are to:

* Maintain a consistent codebase.
* Improve readability.
* Reduce defects.
* Simplify code reviews.
* Make onboarding easier for new developers.

These standards apply to all production code.

---

# 2. General Principles

Code should be:

* Readable
* Maintainable
* Testable
* Consistent
* Secure
* Self-explanatory

When in doubt, choose clarity over cleverness.

---

# 3. Project Structure

```text
src/

├── TrainingManagement.API
├── TrainingManagement.Application
├── TrainingManagement.Domain
└── TrainingManagement.Infrastructure

tests/

├── TrainingManagement.UnitTests
└── TrainingManagement.IntegrationTests
```

Each project has a single, well-defined responsibility.

---

# 4. Naming Conventions

## Classes

Use PascalCase.

Examples:

```text
EmployeeService
CreateEmployeeCommand
AttendanceRepository
```

---

## Interfaces

Prefix with **I**.

Examples:

```text
IEmployeeRepository
IEmailService
IUnitOfWork
```

---

## Methods

Use PascalCase.

Methods should describe an action.

Examples:

```text
CreateEmployee()

RegisterForTraining()

SendNotification()
```

---

## Variables

Use camelCase.

Example:

```text
employee
trainingSession
registrationCount
```

---

## Constants

Use PascalCase.

Example:

```text
MaximumSessionCapacity
JwtIssuer
```

---

## Private Fields

Prefix with an underscore.

Example:

```text
_employeeRepository
_logger
_mapper
```

---

# 5. Folder Organization

Each feature should group related files.

Example:

```text
Application/

Employees/

├── Commands
├── Queries
├── DTOs
├── Validators
└── Handlers
```

Avoid placing unrelated classes together.

---

# 6. Class Design

Each class should have a single responsibility.

Avoid "God Classes" that perform many unrelated tasks.

Keep classes focused and cohesive.

---

# 7. Method Design

Methods should:

* Perform one logical task.
* Have descriptive names.
* Be short where practical.
* Return early to reduce nesting.

Avoid deeply nested `if` statements.

---

# 8. Dependency Injection

Depend on abstractions, not concrete implementations.

Example:

```csharp
public class EmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
}
```

Do not instantiate dependencies with `new` inside application services.

---

# 9. Async Programming

Use asynchronous methods for I/O operations.

Suffix asynchronous methods with `Async`.

Examples:

```text
GetEmployeeAsync()

SaveChangesAsync()

RegisterEmployeeAsync()
```

Avoid blocking calls such as `.Result` or `.Wait()`.

---

# 10. Exception Handling

Use exceptions for exceptional situations only.

Do not use exceptions for normal application flow.

Handle exceptions centrally using middleware.

Return meaningful HTTP status codes.

---

# 11. Validation

Validate input as early as possible.

Use FluentValidation (or equivalent) for request models.

Business rule validation belongs in the domain or application layer.

---

# 12. Logging

Use structured logging.

Log:

* Information
* Warnings
* Errors
* Critical failures

Never log:

* Passwords
* JWT tokens
* Connection strings
* Secrets

---

# 13. Entity Framework Core

Guidelines:

* Use `AsNoTracking()` for read-only queries.
* Avoid N+1 query problems.
* Prefer projections (`Select`) over loading entire entities.
* Use eager loading only when necessary.
* Keep database transactions short.

---

# 14. API Design

Follow RESTful principles.

Use plural resource names.

Examples:

```text
/api/employees
/api/branches
/api/programmes
/api/sessions
```

Return consistent response formats and appropriate HTTP status codes.

---

# 15. DTO Usage

Do not expose Entity Framework entities directly.

Use DTOs for:

* Requests
* Responses

Separate DTOs from domain entities.

---

# 16. Comments

Write comments only when they add value.

Good comments explain **why**, not **what**.

Prefer expressive code over excessive comments.

---

# 17. Unit Testing

New business logic should include automated tests.

Tests should be:

* Independent
* Repeatable
* Fast
* Easy to understand

---

# 18. Git Commit Messages

Use clear, descriptive commit messages.

Examples:

```text
feat: add employee registration endpoint

fix: prevent duplicate session registrations

refactor: simplify attendance validation

test: add integration tests for login endpoint

docs: update API documentation
```

---

# 19. Pull Request Guidelines

Before opening a pull request:

* Ensure the solution builds successfully.
* Run all automated tests.
* Update documentation if required.
* Remove unused code.
* Address compiler warnings where appropriate.

Pull requests should focus on a single logical change.

---

# 20. Code Review Checklist

Reviewers should verify:

* Correctness
* Readability
* Security
* Performance
* Error handling
* Test coverage
* Naming consistency
* Architecture compliance

---

# 21. Definition of Done

A feature is considered complete when:

* Requirements are implemented.
* Code follows project standards.
* Automated tests pass.
* Documentation is updated.
* Code review is completed.
* No critical defects remain.

---

# 22. Related Documentation

* README.md
* SYSTEM_OVERVIEW.md
* SOFTWARE_ARCHITECTURE.md
* DATABASE_DESIGN.md
* DOMAIN_MODEL.md
* API_DOCUMENTATION.md
* SECURITY.md
* TESTING.md
* CONTRIBUTING.md
