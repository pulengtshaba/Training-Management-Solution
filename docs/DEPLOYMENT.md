# DEPLOYMENT.md

# Training Management System

## Deployment Guide

Version: 1.0

Framework: ASP.NET Core Web API (.NET 8)

Architecture: Modular Monolith

Database: Microsoft SQL Server

---

# 1. Purpose

This document describes the deployment process for the Training Management System (TMS).

It covers local development, testing, staging, and production deployments, along with environment configuration and release best practices.

---

# 2. Deployment Environments

The project supports four environments.

| Environment | Purpose                              |
| ----------- | ------------------------------------ |
| Development | Local development and debugging      |
| Testing     | Automated integration and QA testing |
| Staging     | Pre-production validation            |
| Production  | Live environment                     |

Each environment uses its own configuration, database, and secrets.

---

# 3. Technology Stack

Application

* ASP.NET Core Web API (.NET 8)

Database

* Microsoft SQL Server

Authentication

* JWT Bearer Authentication

Documentation

* Swagger / OpenAPI

Containerization

* Docker
* Docker Compose

Cloud (Future)

* Azure App Service
* Azure SQL Database
* Azure Cache for Redis
* Azure Service Bus (or RabbitMQ)

---

# 4. Prerequisites

Install the following software:

* .NET 8 SDK
* SQL Server 2022 (Developer Edition recommended)
* SQL Server Management Studio (SSMS)
* Docker Desktop (optional but recommended)
* Git

Verify installations:

```bash id="1t8d4j"
dotnet --version
docker --version
git --version
```

---

# 5. Clone the Repository

```bash id="n7q3xa"
git clone https://github.com/<username>/training-management-api.git

cd training-management-api
```

---

# 6. Restore Dependencies

```bash id="l6qj8u"
dotnet restore
```

---

# 7. Configure Environment Variables

Do **not** store secrets in source control.

Typical configuration values include:

```text id="g84vzm"
ConnectionStrings__DefaultConnection

Jwt__Issuer

Jwt__Audience

Jwt__Key

Email__Host

Email__Username

Email__Password
```

Development secrets should use:

* ASP.NET Core User Secrets

Production secrets should use:

* Environment Variables
* Azure Key Vault (recommended)

---

# 8. Configure the Database

Apply Entity Framework Core migrations:

```bash id="v5ewg0"
dotnet ef database update
```

If starting from scratch:

```bash id="x1mcsv"
dotnet ef migrations add InitialCreate

dotnet ef database update
```

---

# 9. Seed Initial Data

Seed the application with:

* Roles
* Administrator account
* Sample branches
* Sample venues
* Sample training programmes

This can be performed automatically during application startup or through a dedicated seed utility.

---

# 10. Running the Application

Start the API:

```bash id="svv4f2"
dotnet run
```

Default endpoints:

```text id="l9r8be"
https://localhost:5001

http://localhost:5000
```

Swagger UI:

```text id="bo3m3v"
https://localhost:5001/swagger
```

---

# 11. Docker Deployment

Build the application image:

```bash id="d8zpr1"
docker build -t training-management-api .
```

Run the container:

```bash id="uw2i1a"
docker run -p 5001:8080 training-management-api
```

---

# 12. Docker Compose

Use Docker Compose to start the complete local environment.

Services:

* API
* SQL Server
* Redis (future)
* RabbitMQ (future)

Example:

```bash id="v6mwrv"
docker compose up -d
```

To stop:

```bash id="m4k2n7"
docker compose down
```

---

# 13. CI/CD Pipeline

The recommended deployment pipeline is:

```text id="a3w0xy"
Developer

↓

Push to GitHub

↓

GitHub Actions

↓

Restore Packages

↓

Build Solution

↓

Run Unit Tests

↓

Run Integration Tests

↓

Publish Application

↓

Deploy to Staging

↓

Manual Approval

↓

Deploy to Production
```

A deployment should not proceed if any automated tests fail.

---

# 14. Production Deployment Checklist

Before deploying to production, verify that:

* All automated tests pass.
* Database migrations are reviewed.
* Configuration values are correct.
* Secrets are stored securely.
* HTTPS is enabled.
* Logging is configured.
* Health checks are operational.
* Monitoring is enabled.
* Backups are scheduled.

---

# 15. Health Checks

The API should expose a health endpoint.

Example:

```http id="o2ynl8"
GET /health
```

Typical checks include:

* Database connectivity
* External services
* Background workers (future)
* Cache connectivity (future)

---

# 16. Logging and Monitoring

Production deployments should include:

Logging

* Structured application logs
* Error logs
* Audit logs

Monitoring

* API availability
* Response times
* Database performance
* Failed requests
* Authentication failures

Recommended future tools:

* Azure Application Insights
* Serilog
* Grafana
* Prometheus

---

# 17. Backup and Recovery

Database backups should be performed regularly.

Recommended strategy:

* Daily full backups
* Hourly transaction log backups (where appropriate)
* Off-site backup storage
* Regular recovery testing

---

# 18. Rollback Strategy

If a deployment fails:

1. Stop the deployment.
2. Restore the previous application version.
3. Restore the database if a migration introduced incompatible changes.
4. Verify application health.
5. Investigate and resolve the issue before redeployment.

---

# 19. Scaling Strategy

Current architecture:

```text id="7l1z0c"
Client

↓

ASP.NET Core API

↓

SQL Server
```

Future scaling options:

* Multiple API instances behind a load balancer
* Redis distributed caching
* RabbitMQ background processing
* Azure App Service autoscaling
* Azure SQL performance scaling

---

# 20. Security During Deployment

Ensure that:

* Secrets are never committed to Git.
* HTTPS certificates are valid.
* Production databases are inaccessible from the public internet unless explicitly required.
* Firewall rules are configured.
* Administrative accounts use strong credentials.
* Sensitive configuration values are encrypted where possible.

---

# 21. Release Process

Every release should include:

* Updated CHANGELOG.md
* Successful CI pipeline
* Reviewed Pull Request
* Tagged Git release (e.g., `v1.0.0`)
* Deployment verification
* Post-deployment smoke tests

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
* CODING_STANDARDS.md
* CONTRIBUTING.md
* CHANGELOG.md
