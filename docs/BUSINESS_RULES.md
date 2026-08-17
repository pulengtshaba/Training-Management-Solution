# BUSINESS_RULES.md

# Training Management System

## Business Rules Catalogue

Version: 1.0

Document Owner: Development Team

Status: Active

---

# 1. Purpose

This document defines the business rules that govern the behaviour of the Training Management System (TMS).

Each business rule has a unique identifier to improve communication between developers, testers, business analysts, and stakeholders. These identifiers can be referenced in user stories, test cases, bug reports, and technical documentation.

---

# 2. Rule Classification

Business rules are grouped into the following categories:

| Prefix     | Category                  |
| ---------- | ------------------------- |
| BR-AUTH    | Authentication & Security |
| BR-EMP     | Employee Management       |
| BR-BRANCH  | Branch Management         |
| BR-PROGRAM | Training Programmes       |
| BR-SESSION | Training Sessions         |
| BR-REG     | Registrations             |
| BR-ATT     | Attendance                |
| BR-NOTIFY  | Notifications             |
| BR-REPORT  | Reporting                 |
| BR-SYSTEM  | System-wide Rules         |

---

# 3. Authentication & Security Rules

| ID          | Business Rule                                                          |
| ----------- | ---------------------------------------------------------------------- |
| BR-AUTH-001 | Every user must authenticate before accessing protected API endpoints. |
| BR-AUTH-002 | JWT access tokens must be valid before requests are processed.         |
| BR-AUTH-003 | Users may only access resources permitted by their assigned role.      |
| BR-AUTH-004 | Passwords must never be stored in plain text.                          |
| BR-AUTH-005 | All authenticated requests must be transmitted over HTTPS.             |

---

# 4. Employee Management Rules

| ID         | Business Rule                                      |
| ---------- | -------------------------------------------------- |
| BR-EMP-001 | Every employee must have a unique employee number. |
| BR-EMP-002 | Every employee must have a unique email address.   |
| BR-EMP-003 | Every employee belongs to exactly one branch.      |
| BR-EMP-004 | Every employee is assigned one active role.        |
| BR-EMP-005 | Inactive employees cannot register for training.   |

---

# 5. Branch Rules

| ID            | Business Rule                                                |
| ------------- | ------------------------------------------------------------ |
| BR-BRANCH-001 | Branch codes must be unique.                                 |
| BR-BRANCH-002 | Branch names must be unique.                                 |
| BR-BRANCH-003 | Managers may only manage employees assigned to their branch. |

---

# 6. Training Programme Rules

| ID             | Business Rule                                 |
| -------------- | --------------------------------------------- |
| BR-PROGRAM-001 | Programme titles must be unique.              |
| BR-PROGRAM-002 | Only active programmes may be scheduled.      |
| BR-PROGRAM-003 | Programme duration must be greater than zero. |

---

# 7. Training Session Rules

| ID             | Business Rule                                                         |
| -------------- | --------------------------------------------------------------------- |
| BR-SESSION-001 | Every session must reference an existing training programme.          |
| BR-SESSION-002 | Every session must have an assigned trainer.                          |
| BR-SESSION-003 | Every session must have an assigned venue.                            |
| BR-SESSION-004 | Session end date must be later than the start date.                   |
| BR-SESSION-005 | Maximum participant capacity must be greater than zero.               |
| BR-SESSION-006 | Cancelled sessions cannot accept new registrations.                   |
| BR-SESSION-007 | Completed sessions are read-only unless modified by an administrator. |

---

# 8. Registration Rules

| ID         | Business Rule                                                                                            |
| ---------- | -------------------------------------------------------------------------------------------------------- |
| BR-REG-001 | An employee may register only once for a training session.                                               |
| BR-REG-002 | Registration is not permitted once the maximum session capacity has been reached.                        |
| BR-REG-003 | Registration is not permitted for cancelled sessions.                                                    |
| BR-REG-004 | Registration is not permitted for inactive employees.                                                    |
| BR-REG-005 | Registration cannot occur after the session has started unless explicitly authorised by business policy. |

---

# 9. Attendance Rules

| ID         | Business Rule                                                              |
| ---------- | -------------------------------------------------------------------------- |
| BR-ATT-001 | Attendance cannot exist without a valid registration.                      |
| BR-ATT-002 | Only the assigned trainer may record attendance for a session.             |
| BR-ATT-003 | Attendance may only be recorded after the scheduled session has begun.     |
| BR-ATT-004 | Attendance status must be one of: Pending, Attended, Missed, or Cancelled. |
| BR-ATT-005 | Attendance records cannot be deleted; corrections must be audited.         |

---

# 10. Notification Rules

| ID            | Business Rule                                                                   |
| ------------- | ------------------------------------------------------------------------------- |
| BR-NOTIFY-001 | Registration confirmation notifications are sent after successful registration. |
| BR-NOTIFY-002 | Reminder notifications are sent before scheduled training sessions.             |
| BR-NOTIFY-003 | Completion notifications are sent after successful course completion.           |
| BR-NOTIFY-004 | Notifications may be delivered to multiple recipients.                          |

---

# 11. Reporting Rules

| ID            | Business Rule                                                          |
| ------------- | ---------------------------------------------------------------------- |
| BR-REPORT-001 | Reports display only data the requesting user is authorised to access. |
| BR-REPORT-002 | Attendance reports include only completed training sessions.           |
| BR-REPORT-003 | Dashboard statistics are calculated using approved business rules.     |

---

# 12. System Rules

| ID            | Business Rule                                                                                              |
| ------------- | ---------------------------------------------------------------------------------------------------------- |
| BR-SYSTEM-001 | Every database record must maintain referential integrity.                                                 |
| BR-SYSTEM-002 | All business rule violations must return meaningful validation errors.                                     |
| BR-SYSTEM-003 | Every successful create, update, or delete operation should be recorded in the audit log where applicable. |
| BR-SYSTEM-004 | Business rules are enforced in the Domain layer and must not rely solely on client-side validation.        |

---

# 13. Rule Traceability

Business rule identifiers should be referenced throughout the project.

Examples:

### User Story

> As a manager, I want employees to register only once for a training session.

Implements:

**BR-REG-001**

---

### Unit Test

```text
RegistrationService_ShouldRejectDuplicateRegistration_BR_REG_001()
```

---

### API Validation

```text
400 Bad Request

Business Rule:

BR-REG-001

An employee may only register once for this training session.
```

---

### Bug Report

```
Reference:

BR-ATT-002

Assigned trainer restriction is not enforced.
```

---

# 14. Change Management

Whenever a business rule is added, modified, or removed:

1. Update this document.
2. Update related unit tests.
3. Update integration tests.
4. Update API documentation if applicable.
5. Review impacted business processes.

---

# 15. Summary

The Business Rules Catalogue is the authoritative source for all business rules within the Training Management System.

Maintaining this document ensures consistency between business requirements, implementation, testing, and documentation, while improving communication across technical and non-technical stakeholders.
