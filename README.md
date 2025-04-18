# LeaveManagementSystem
A .NET Core Web API for managing employee leave requests

## Technologies

- .NET 8 Core Web API  
- Entity Framework Core  
- SQLite Database

## NuGet Package
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Sqlite
- Microsoft.EntityFrameworkCore.Tools

## Models

**Employee**  
- Id  
- FullName  
- Department  
- JoiningDate  

**LeaveRequest**  
- Id  
- EmployeeId  
- LeaveType (Annual, Sick, Other)  
- StartDate  
- EndDate  
- Status (Pending, Approved, Rejected)  
- Reason  
- CreatedAt

## Endpoints

### CRUD - LeaveRequest

- GET `/api/leaveRequests`  
- POST `/api/leaveRequests`  
- PUT `/api/leaveRequests/{id}`
- GET `/api/leaveRequests/{id}`  
- DELETE `/api/leaveRequests/{id}`  
- GET `/api/leaveRequests/filter`
