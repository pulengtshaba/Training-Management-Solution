# SOFTWARE_ARCHITECTURE.md

# Training Management System

## Software Architecture Document

Version: 1.0

Architecture Style: Monolithic

Framework: ASP.NET Core Web API

Architecture Pattern: Clean Architecture

---

# 1. Purpose

This document describes the technical architecture of the Training Management System (TMS). It explains how the application is structured, how components interact, and the architectural principles used to ensure maintainability, scalability, and testability.

---

# 2. Architecture Overview

The Training Management System is implemented as a **modular monolithic application** using **Clean Architecture**.

Although deployed as a single application, the system is organized into independent modules with clear responsibilities.

### Architectural Goals

* Separation of Concerns
* Maintainability
* Testability
* Scalability
* Security
* Extensibility
* High Cohesion
* Low Coupling

---

# 3. High-Level Architecture

```text
                    Client Applications
          (Web | Mobile | Future Integrations)
                         │
                         ▼
                ASP.NET Core Web API
                         │
 ┌──────────────┬──────────────┬──────────────┐
 ▼              ▼              ▼
 Controllers  Middleware   Authentication
                         │
                         ▼
                 Application Layer
                         │
                         ▼
                   Domain Layer
                         │
                         ▼
              Infrastructure Layer
                         │
                         ▼
                    SQL Server
```

---

# 4. Clean Architecture Layers

## API Layer

Responsibilities:

* Receive HTTP requests
* Validate incoming models
* Authenticate users
* Return HTTP responses
* Invoke Application layer

Contains:

* Controllers
* Middleware
* Dependency Injection
* Swagger Configuration
* Authentication Configuration

This layer contains **no business rules**.

---

## Application Layer

Responsibilities:

* Execute business use cases
* Coordinate application workflows
* Validate commands and queries
* Define interfaces

Contains:

* Commands
* Queries
* DTOs
* Validators
* Interfaces
* Application Services

The Application layer depends only on the Domain layer.

---

## Domain Layer

The Domain layer contains the business model.

Responsibilities:

* Business rules
* Domain entities
* Value objects
* Domain services
* Business validation

The Domain layer has **no dependency** on ASP.NET Core, SQL Server, or Entity Framework Core.

---

## Infrastructure Layer

Provides technical implementations.

Responsibilities:

* Entity Framework Core
* SQL Server
* Repository implementations
* External integrations
* Email services
* File storage
* Logging

Infrastructure depends on the Application layer but is isolated from business rules.

---

# 5. Dependency Direction

Dependencies always point inward.

```text
API
 │
 ▼
Application
 │
 ▼
Domain

Infrastructure implements interfaces defined by the Application layer.
```

This follows the Dependency Inversion Principle.

---

# 6. Module Overview

The application is divided into logical modules:

* Authentication
* Users
* Branches
* Training Programmes
* Training Sessions
* Venues
* Registrations
* Attendance
* Notifications
* Reporting

Each module encapsulates its own business logic while sharing common infrastructure.

---

# 7. Request Lifecycle

A typical request follows this sequence:

1. Client sends an HTTP request.
2. Middleware processes the request.
3. Authentication and authorization are applied.
4. The controller receives the request.
5. The controller calls the appropriate application service or CQRS handler.
6. Business rules are enforced in the Domain layer.
7. Infrastructure persists or retrieves data.
8. A response is returned to the client.

---

# 8. Data Access

Data access is handled through Entity Framework Core.

Responsibilities include:

* Database connections
* Migrations
* LINQ queries
* Change tracking
* Transactions

Repositories abstract persistence from the Application layer.

---

# 9. Authentication and Authorization

The system uses JWT Bearer Authentication.

Authorization is role-based.

Supported roles include:

* Administrator
* Manager
* Trainer
* Employee

Authentication is enforced before business logic is executed.

---

# 10. Error Handling

Global exception handling is implemented using custom middleware.

Responsibilities:

* Capture unhandled exceptions
* Return standardized error responses
* Log application errors
* Prevent sensitive information from being exposed

---

# 11. Logging

Application logging records:

* Information events
* Warnings
* Errors
* Critical failures

Sensitive information such as passwords, tokens, and secrets must never be logged.

---

# 12. Validation

Validation occurs at multiple levels:

* Request validation
* Business rule validation
* Database constraint validation

Business validation remains within the Domain layer.

---

# 13. Security

The architecture incorporates:

* HTTPS
* JWT Authentication
* Role-Based Authorization
* Input validation
* Secure password hashing
* Secure configuration management

---

# 14. Testing Strategy

The architecture supports automated testing through separation of concerns.

Testing includes:

* Unit Tests
* Integration Tests
* Repository Tests
* Authentication Tests

---

# 15. Extensibility

The architecture allows future enhancements such as:

* Redis caching
* RabbitMQ messaging
* Background processing
* Azure deployment
* External Learning Management System (LMS) integration
* Multi-tenancy

These features can be added with minimal impact on existing modules.

---

# 16. Design Principles

The system follows these principles:

* SOLID Principles
* Clean Architecture
* Separation of Concerns
* Dependency Injection
* Repository Pattern
* CQRS (where appropriate)
* Domain-Driven Design concepts
* RESTful API design

---

# 17. Architectural Decisions

Key decisions include:

* **Monolithic Architecture** to reduce operational complexity during the initial development phase.
* **Clean Architecture** to separate business logic from infrastructure concerns.
* **Entity Framework Core** for object-relational mapping.
* **SQL Server** as the primary relational database.
* **JWT** for stateless authentication.
* **Swagger/OpenAPI** for API discovery and testing.

---

# 18. Future Evolution

As the application grows, it may evolve to include:

* Redis distributed caching
* RabbitMQ event-driven messaging
* Background worker services
* Azure cloud hosting
* API versioning
* API Gateway (if the application is decomposed into microservices)

The current architecture has been designed so these enhancements can be introduced incrementally without requiring a complete redesign.

---

# 19. Summary

The Training Management System adopts a modular monolithic architecture based on Clean Architecture principles. This design keeps business logic independent from framework-specific concerns, making the system easier to maintain, test, and extend.

The architecture provides a strong foundation for enterprise application development while remaining simple enough for a single deployment and development team.
