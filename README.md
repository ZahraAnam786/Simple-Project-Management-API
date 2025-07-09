# Simple Project Management API

A RESTful Web API built using ASP.NET Core 8.0 and Entity Framework Core that allows users to manage Projects and their associated Tasks.

---

## How to Run the API
https://localhost:7288/swagger/index.html

1-When you register or login via the API, the system returns a JWT (JSON Web Token) as proof of authentication.

2-This JWT token must be included in the Authorization header as a Bearer token for all subsequent API requests that require authentication.


## Improvements
1-Separation of Concerns:
Created separate Class Library projects for the Service Layer and Repository Layer to promote modularity, easier testing, and better maintainability.

2-User Tracking Enhancement:
Considered adding a UserID column in the Project entity to associate projects with specific users for multi-user scenarios and access control.
Note: This was not implemented in the current version to stay aligned with the original task requirements.


