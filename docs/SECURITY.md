# SECURITY.md

# Training Management System

## Security Guide

Version: 1.0

---

# 1. Purpose

This document describes the security architecture and practices used by the Training Management System (TMS).

The objective is to protect user data, prevent unauthorized access, and ensure the confidentiality, integrity, and availability of the system.

---

# 2. Security Principles

The application follows these core security principles:

* Least Privilege
* Defense in Depth
* Secure by Default
* Fail Securely
* Input Validation
* Principle of Separation of Duties
* Secure Secret Management

---

# 3. Authentication

The API uses **JWT Bearer Authentication**.

### Login Flow

1. User submits email and password.
2. Credentials are validated.
3. A JWT Access Token is issued.
4. A Refresh Token is generated.
5. The client includes the access token in the `Authorization` header for protected requests.

Example:

```http id="2t9y5g"
Authorization: Bearer eyJhbGciOi...
```

---

# 4. Authorization

Role-Based Access Control (RBAC) is enforced.

Supported roles:

* Administrator
* Manager
* Trainer
* Employee

Examples:

| Role          | Permission                                       |
| ------------- | ------------------------------------------------ |
| Administrator | Full system access                               |
| Manager       | Manage employees within assigned branch          |
| Trainer       | Manage assigned training sessions and attendance |
| Employee      | View and register for training                   |

---

# 5. Password Security

Passwords are **never stored in plain text**.

Requirements:

* Strong password policy
* Password hashing using ASP.NET Core Identity (PBKDF2 by default)
* Password verification performed using the hashing library
* Password reset via secure workflow

---

# 6. Transport Security

All communication must use **HTTPS**.

Production environments should:

* Redirect HTTP to HTTPS
* Use trusted TLS certificates
* Disable insecure protocols and ciphers

---

# 7. Input Validation

All incoming requests are validated.

Validation includes:

* Required fields
* String length limits
* Email format
* Date validation
* Numeric ranges
* Business rule validation

Server-side validation is always required, even if client-side validation exists.

---

# 8. Data Protection

Sensitive information must be protected.

Examples include:

* Password hashes
* JWT signing keys
* Connection strings
* API keys
* SMTP credentials

Secrets must **not** be committed to source control.

Use:

* User Secrets (Development)
* Environment Variables
* Azure Key Vault (Production)

---

# 9. Logging

Application logs should include:

* Login attempts
* Authorization failures
* Validation errors
* Application exceptions
* Significant business events

The following must **never** be logged:

* Passwords
* Access tokens
* Refresh tokens
* Secret keys
* Personally identifiable information unless required for auditing

---

# 10. Error Handling

The API returns standardized error responses.

Internal implementation details are not exposed to clients.

Example:

```json id="6y6qvn"
{
  "status": 500,
  "title": "An unexpected error occurred."
}
```

Detailed exception information should be available only in server logs.

---

# 11. CORS (Cross-Origin Resource Sharing)

Allowed origins should be explicitly configured.

Example:

* Development: `http://localhost:4200`
* Production: `https://training.company.com`

Avoid allowing all origins (`*`) in production.

---

# 12. Rate Limiting

To reduce abuse and denial-of-service risks, the API should support rate limiting.

Example policy:

* 100 requests per minute per client
* Stricter limits for authentication endpoints

---

# 13. SQL Injection Prevention

The application uses Entity Framework Core.

Benefits:

* Parameterized SQL generated automatically
* Reduced SQL injection risk

If raw SQL is required:

* Use parameterized queries
* Never concatenate user input into SQL statements

---

# 14. Cross-Site Scripting (XSS)

Although this is a Web API, any data that will later be rendered in a web client should be treated as untrusted.

Frontend applications should encode output appropriately.

---

# 15. Cross-Site Request Forgery (CSRF)

When using JWTs in the `Authorization` header, CSRF risk is significantly reduced compared to cookie-based authentication.

If cookies are introduced in the future, appropriate CSRF protections should be implemented.

---

# 16. Audit Logging

Important actions should be auditable.

Examples:

* User login
* Password change
* Role assignment
* Employee creation
* Attendance updates
* Programme deletion

Suggested audit fields:

* User
* Action
* Timestamp
* IP Address (where appropriate)
* Result

---

# 17. Dependency Security

Dependencies should be kept up to date.

Recommendations:

* Review NuGet package updates regularly
* Remove unused packages
* Address known vulnerabilities promptly
* Enable automated dependency scanning in GitHub

---

# 18. Secure Development Practices

Developers should:

* Follow secure coding guidelines
* Review code before merging
* Write unit and integration tests
* Avoid hard-coded secrets
* Validate all external input
* Apply the principle of least privilege

---

# 19. Incident Reporting

If a security vulnerability is discovered:

1. Do not disclose it publicly.
2. Notify the project maintainers.
3. Investigate and assess the impact.
4. Develop and test a fix.
5. Release the fix and document the change.

---

# 20. Future Security Enhancements

Planned improvements include:

* Multi-Factor Authentication (MFA)
* OAuth 2.0 / OpenID Connect
* Account lockout after repeated failed logins
* Security headers
* Refresh token rotation
* Distributed session revocation
* Azure Key Vault integration

---

# 21. Security Checklist

Before each release, verify that:

* HTTPS is enforced.
* Secrets are stored securely.
* Authentication is functioning correctly.
* Authorization policies are tested.
* Input validation is complete.
* Error handling does not expose sensitive information.
* Logs do not contain secrets.
* Dependencies are up to date.

---

# 22. Related Documentation

* README.md
* SYSTEM_OVERVIEW.md
* SOFTWARE_ARCHITECTURE.md
* API_DOCUMENTATION.md
* TESTING.md
* DEPLOYMENT.md
* CONTRIBUTING.md
