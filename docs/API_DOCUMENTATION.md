# API_DOCUMENTATION.md

# Training Management System

## REST API Documentation

Version: 1.0

API Style: RESTful

Data Format: JSON

Authentication: JWT Bearer Token

Base URL:

```text
https://localhost:5001/api
```

---

# 1. Introduction

The Training Management System (TMS) API provides secure endpoints for managing employees, training programmes, sessions, attendance, notifications, and reporting.

The API follows REST principles and uses JSON for request and response payloads.

---

# 2. Authentication

Most endpoints require a JWT Bearer Token.

Example Header

```http
Authorization: Bearer <access_token>
```

---

# 3. Standard HTTP Status Codes

| Code | Meaning               |
| ---- | --------------------- |
| 200  | OK                    |
| 201  | Created               |
| 204  | No Content            |
| 400  | Bad Request           |
| 401  | Unauthorized          |
| 403  | Forbidden             |
| 404  | Not Found             |
| 409  | Conflict              |
| 422  | Validation Error      |
| 500  | Internal Server Error |

---

# 4. Authentication Endpoints

## Login

### POST

```http
POST /auth/login
```

Request

```json
{
  "email": "john.smith@company.com",
  "password": "Password123!"
}
```

Response

```json
{
  "accessToken": "...",
  "refreshToken": "...",
  "expiresIn": 3600
}
```

---

## Refresh Token

### POST

```http
POST /auth/refresh
```

Request

```json
{
  "refreshToken": "..."
}
```

Response

```json
{
  "accessToken": "...",
  "refreshToken": "...",
  "expiresIn": 3600
}
```

---

# 5. Employee Endpoints

## Get All Employees

### GET

```http
GET /employees
```

Authorization

* Administrator
* Manager

Example Response

```json
[
  {
    "employeeId": 1,
    "employeeNumber": "EMP001",
    "firstName": "John",
    "lastName": "Smith",
    "branch": "Johannesburg"
  }
]
```

---

## Get Employee

### GET

```http
GET /employees/{employeeId}
```

Response

```json
{
  "employeeId": 1,
  "employeeNumber": "EMP001",
  "firstName": "John",
  "lastName": "Smith",
  "email": "john.smith@company.com",
  "branch": "Johannesburg"
}
```

---

## Create Employee

### POST

```http
POST /employees
```

Request

```json
{
  "employeeNumber": "EMP001",
  "firstName": "John",
  "lastName": "Smith",
  "email": "john.smith@company.com",
  "branchId": 2
}
```

Response

HTTP 201 Created

---

## Update Employee

### PUT

```http
PUT /employees/{employeeId}
```

---

## Delete Employee

### DELETE

```http
DELETE /employees/{employeeId}
```

---

# 6. Branch Endpoints

| Method | Endpoint       | Description   |
| ------ | -------------- | ------------- |
| GET    | /branches      | List branches |
| GET    | /branches/{id} | Get branch    |
| POST   | /branches      | Create branch |
| PUT    | /branches/{id} | Update branch |
| DELETE | /branches/{id} | Delete branch |

---

# 7. Training Programme Endpoints

| Method | Endpoint         |
| ------ | ---------------- |
| GET    | /programmes      |
| GET    | /programmes/{id} |
| POST   | /programmes      |
| PUT    | /programmes/{id} |
| DELETE | /programmes/{id} |

---

# 8. Training Session Endpoints

| Method | Endpoint       |
| ------ | -------------- |
| GET    | /sessions      |
| GET    | /sessions/{id} |
| POST   | /sessions      |
| PUT    | /sessions/{id} |
| DELETE | /sessions/{id} |

---

# 9. Venue Endpoints

| Method | Endpoint     |
| ------ | ------------ |
| GET    | /venues      |
| GET    | /venues/{id} |
| POST   | /venues      |
| PUT    | /venues/{id} |
| DELETE | /venues/{id} |

---

# 10. Registration Endpoints

## Register Employee

```http
POST /registrations
```

Request

```json
{
  "employeeId": 15,
  "sessionId": 8
}
```

Response

```json
{
  "registrationId": 125,
  "status": "Registered"
}
```

Business Rules

* Employee must exist.
* Session must exist.
* Session must have available capacity.
* Duplicate registrations are not allowed.

---

## Get Employee Registrations

```http
GET /employees/{employeeId}/registrations
```

---

# 11. Attendance Endpoints

## Mark Attendance

```http
POST /attendance/check-in
```

Request

```json
{
  "registrationId": 125,
  "checkInTime": "2026-07-22T08:30:00Z"
}
```

---

## Check Out

```http
POST /attendance/check-out
```

---

## Attendance History

```http
GET /attendance
```

---

# 12. Notification Endpoints

| Method | Endpoint                 |
| ------ | ------------------------ |
| GET    | /notifications           |
| GET    | /notifications/{id}      |
| PUT    | /notifications/{id}/read |

---

# 13. Reporting Endpoints

Examples

```http
GET /reports/training-summary

GET /reports/attendance

GET /reports/employee-history

GET /reports/branch-performance
```

---

# 14. Pagination

Endpoints returning collections support pagination.

Example

```http
GET /employees?pageNumber=1&pageSize=20
```

Response

```json
{
  "pageNumber": 1,
  "pageSize": 20,
  "totalRecords": 1450,
  "items": [ ]
}
```

---

# 15. Filtering

Example

```http
GET /employees?branchId=3

GET /sessions?status=Scheduled

GET /programmes?isActive=true
```

---

# 16. Sorting

Example

```http
GET /employees?sortBy=lastName

GET /sessions?sortBy=startDate
```

---

# 17. Error Response Format

Example

```json
{
  "status": 400,
  "title": "Validation Failed",
  "errors": [
    {
      "field": "Email",
      "message": "Email address is required."
    }
  ]
}
```

---

# 18. Security

Authentication

JWT Bearer Token

Authorization

Role-Based Access Control

Supported Roles

* Administrator
* Manager
* Trainer
* Employee

---

# 19. API Versioning

Current Version

v1

Future versions will follow URL versioning.

Example

```http
/api/v1/employees

/api/v2/employees
```

---

# 20. Rate Limiting

Future enhancement

Examples

* 100 requests per minute
* 1000 requests per hour

---

# 21. OpenAPI / Swagger

Interactive API documentation is available when the application is running.

Example

```text
https://localhost:5001/swagger
```

---

# 22. Future API Enhancements

* Bulk employee import
* QR Code attendance
* Certificate generation
* File uploads
* SMS notifications
* Webhooks
* GraphQL endpoint
* External LMS integration

---

# 23. API Design Principles

The API follows these principles:

* RESTful resource naming
* Stateless communication
* Consistent HTTP status codes
* JSON request and response bodies
* Pagination for large collections
* Filtering and sorting support
* Secure authentication
* Versioning for breaking changes
* Standardized error responses

---

# 24. Related Documentation

* README.md
* SYSTEM_OVERVIEW.md
* SOFTWARE_ARCHITECTURE.md
* DATABASE_DESIGN.md
* DOMAIN_MODEL.md
* SECURITY.md
* TESTING.md
* DEPLOYMENT.md
* CONTRIBUTING.md
