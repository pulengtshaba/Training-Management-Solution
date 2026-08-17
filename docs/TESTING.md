# TESTING.md

# Training Management System

## Testing Strategy

Version: 1.0

Framework: xUnit

Mocking Framework: Moq

Assertions: FluentAssertions

---

# 1. Purpose

This document describes the testing strategy for the Training Management System (TMS).

The goal is to ensure that the application is reliable, maintainable, and behaves correctly as new features are introduced.

Testing is treated as an integral part of the software development lifecycle rather than an activity performed only before release.

---

# 2. Testing Objectives

The testing strategy aims to:

* Verify business rules.
* Prevent regressions.
* Validate API behaviour.
* Detect defects early.
* Increase confidence during deployments.
* Support continuous integration.

---

# 3. Testing Pyramid

The project follows the Testing Pyramid.

```text
                UI Tests
                   ▲
          Integration Tests
                   ▲
             Unit Tests
```

The majority of tests should be **Unit Tests**, followed by **Integration Tests**. End-to-end/UI tests should be used selectively.

---

# 4. Test Project Structure

```text
tests/

├── TrainingManagement.UnitTests
│
├── TrainingManagement.IntegrationTests
│
└── TrainingManagement.TestUtilities
```

### TrainingManagement.UnitTests

Tests individual classes in isolation.

### TrainingManagement.IntegrationTests

Tests the interaction between multiple components.

### TrainingManagement.TestUtilities

Contains:

* Test Builders
* Fake Data
* Shared Fixtures
* Custom Assertions

---

# 5. Unit Testing

Unit tests verify a single unit of behaviour.

Examples include:

* Domain entity methods
* Validators
* Application services
* Command handlers
* Query handlers
* Helper classes

Characteristics:

* Fast execution
* No database
* No network calls
* No file system access

Dependencies are mocked where appropriate.

---

# 6. Integration Testing

Integration tests verify that multiple components work together correctly.

Examples include:

* API endpoints
* Entity Framework Core
* Authentication
* Authorization
* Database persistence
* Middleware
* Dependency Injection configuration

These tests use a dedicated test database and should not rely on production data.

---

# 7. Naming Convention

Tests follow the pattern:

```text
MethodName_Scenario_ExpectedResult
```

Examples:

```text
RegisterEmployee_WithValidData_ReturnsSuccess

RegisterEmployee_WhenSessionIsFull_ReturnsConflict

Login_WithInvalidPassword_ReturnsUnauthorized
```

---

# 8. Mocking Strategy

External dependencies should be mocked in unit tests.

Examples include:

* Email service
* Notification service
* Time provider
* Repository interfaces
* Message broker

Business logic should be tested independently of infrastructure.

---

# 9. Test Data

Tests should use deterministic data.

Recommended approaches:

* Builder Pattern
* Object Mother Pattern
* AutoFixture (optional)

Avoid using random values unless randomness is part of the behaviour being tested.

---

# 10. Business Rule Testing

Every important business rule should have one or more tests.

Examples:

* Employee cannot register twice for the same session.
* Session capacity cannot be exceeded.
* Only assigned trainers can record attendance.
* Managers cannot manage employees outside their branch.

---

# 11. API Testing

API tests should verify:

* HTTP status codes
* Response payloads
* Validation errors
* Authentication requirements
* Authorization rules
* Pagination
* Filtering
* Sorting

---

# 12. Database Testing

Integration tests should verify:

* Entity relationships
* Foreign key constraints
* Cascade behaviours (where applicable)
* Data persistence
* Migrations

---

# 13. Authentication Testing

Authentication tests include:

* Successful login
* Invalid credentials
* Expired tokens
* Missing tokens
* Refresh token flow

---

# 14. Authorization Testing

Verify role-based access control.

Example scenarios:

| Role          | Scenario                              | Expected Result |
| ------------- | ------------------------------------- | --------------- |
| Administrator | Delete programme                      | Success         |
| Trainer       | Delete programme                      | Forbidden       |
| Employee      | View own registrations                | Success         |
| Employee      | View another employee's registrations | Forbidden       |

---

# 15. Performance Testing

Although not part of the standard automated suite, performance testing should validate:

* Response times
* Database query efficiency
* Concurrent user handling
* Memory usage

Suitable tools include:

* k6
* JMeter
* NBomber

---

# 16. Test Coverage

The project aims for meaningful coverage rather than 100% coverage.

Suggested targets:

| Layer           | Target                            |
| --------------- | --------------------------------- |
| Domain          | 90%+                              |
| Application     | 85%+                              |
| Infrastructure  | Risk-based                        |
| API Controllers | Covered through integration tests |

Critical business logic should always be tested.

---

# 17. Continuous Integration

The CI pipeline should automatically:

1. Restore dependencies.
2. Build the solution.
3. Execute all automated tests.
4. Generate a test report.
5. Fail the pipeline if tests fail.

No code should be merged into the main branch while automated tests are failing.

---

# 18. Test Environment

A dedicated test environment should be used.

Requirements:

* Isolated database
* Independent configuration
* Repeatable setup
* Automatic cleanup where possible

---

# 19. Best Practices

* Keep tests independent.
* Write descriptive test names.
* Avoid duplicated setup code.
* Test behaviour rather than implementation details.
* Keep tests fast and deterministic.
* Refactor tests alongside production code.

---

# 20. Related Documentation

* README.md
* SOFTWARE_ARCHITECTURE.md
* API_DOCUMENTATION.md
* DATABASE_DESIGN.md
* SECURITY.md
* DEPLOYMENT.md
* CONTRIBUTING.md
