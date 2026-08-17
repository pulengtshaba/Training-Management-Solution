# CONTRIBUTING.md

# Contributing Guide

Thank you for your interest in contributing to the **Training Management System (TMS)**.

This document explains the development workflow, coding expectations, and contribution process for this project.

---

# 1. Development Philosophy

The project aims to demonstrate enterprise software engineering practices.

Every contribution should:

* Improve maintainability
* Preserve code quality
* Follow Clean Architecture principles
* Include appropriate testing
* Update documentation where necessary

---

# 2. Before You Start

Ensure you have:

* .NET SDK installed
* SQL Server installed
* Visual Studio 2022 (or later) / Visual Studio Code
* Git
* A local copy of the repository

Clone the repository:

```bash id="fiy0go"
git clone https://github.com/<username>/training-management-api.git
```

---

# 3. Branching Strategy

The project uses a simplified GitHub Flow.

Protected Branches:

```text id="evb54q"
main
```

Development work should never be committed directly to `main`.

Create a feature branch from the latest `main`.

Example:

```text id="7lszkn"
feature/employee-registration

feature/jwt-authentication

feature/attendance-report

bugfix/session-capacity

hotfix/login-error

docs/update-api-guide
```

---

# 4. Development Workflow

Typical workflow:

```text id="8evn3q"
Create Issue

↓

Create Branch

↓

Implement Feature

↓

Write Tests

↓

Update Documentation

↓

Commit Changes

↓

Open Pull Request

↓

Code Review

↓

Merge into Main
```

---

# 5. Commit Message Convention

Follow the Conventional Commits specification.

Examples:

```text id="4gjd0i"
feat: add training session endpoint

fix: prevent duplicate employee registration

refactor: simplify attendance service

test: add integration tests for authentication

docs: update database design

chore: update NuGet packages
```

Commit messages should clearly describe the change.

---

# 6. Coding Standards

All contributions must comply with the project's coding standards.

Refer to:

`CODING_STANDARDS.md`

Key expectations:

* Follow Clean Architecture.
* Use Dependency Injection.
* Keep methods focused.
* Write meaningful names.
* Avoid duplicated code.
* Follow SOLID principles.

---

# 7. Testing Requirements

Every new feature should include appropriate automated tests.

Examples:

* Unit Tests
* Integration Tests

Before submitting a pull request:

```bash id="y3fx0b"
dotnet test
```

All tests must pass.

---

# 8. Documentation

Documentation is considered part of the project.

Update documentation when:

* Adding features
* Changing behaviour
* Modifying APIs
* Introducing new business rules
* Changing database schema

Relevant documents include:

* README.md
* API_DOCUMENTATION.md
* DATABASE_DESIGN.md
* SOFTWARE_ARCHITECTURE.md
* CHANGELOG.md

---

# 9. Pull Request Guidelines

Each Pull Request should:

* Address a single feature or bug
* Build successfully
* Pass all tests
* Include documentation updates where applicable
* Avoid unrelated changes

Pull Requests should include:

* Summary of changes
* Reason for change
* Testing performed
* Screenshots (if UI changes are introduced in future clients)

---

# 10. Code Review Expectations

Code reviews focus on:

* Correctness
* Maintainability
* Performance
* Security
* Readability
* Architecture compliance
* Test coverage

Constructive feedback is encouraged.

---

# 11. Issue Reporting

When reporting a bug, include:

* Description
* Steps to reproduce
* Expected behaviour
* Actual behaviour
* Environment
* API version
* Relevant logs (excluding sensitive information)

Feature requests should include:

* Business problem
* Proposed solution
* Expected benefit

---

# 12. Branch Naming Convention

Feature:

```text id="g66hqb"
feature/<feature-name>
```

Bug Fix:

```text id="lzpjhf"
bugfix/<bug-name>
```

Hot Fix:

```text id="v5fjlwm"
hotfix/<issue-name>
```

Documentation:

```text id="of3w3p"
docs/<document-name>
```

Refactoring:

```text id="hslv1d"
refactor/<area-name>
```

---

# 13. Definition of Done

A task is considered complete when:

* Requirements are implemented.
* Code follows project standards.
* Unit tests pass.
* Integration tests pass.
* Documentation is updated.
* Pull Request is approved.
* No critical defects remain.

---

# 14. Development Principles

Contributors should aim to:

* Keep the codebase simple.
* Prefer readability over cleverness.
* Follow established project patterns.
* Minimize technical debt.
* Leave the code better than they found it.

---

# 15. Getting Help

Before raising a question:

1. Read the project documentation.
2. Search existing issues.
3. Review related code.
4. Ask clear, specific questions with enough context to reproduce the problem.

---

# 16. Related Documentation

* README.md
* SYSTEM_OVERVIEW.md
* SOFTWARE_ARCHITECTURE.md
* DATABASE_DESIGN.md
* DOMAIN_MODEL.md
* API_DOCUMENTATION.md
* SECURITY.md
* TESTING.md
* CODING_STANDARDS.md
* DEPLOYMENT.md
* CHANGELOG.md
