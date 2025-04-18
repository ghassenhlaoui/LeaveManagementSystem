using LeaveManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
            Seed();
        }
        // Seeding Data for employees and leaveRequests
        private void Seed()
        {
            if (!Employees.Any())
            {
                var employees = new List<Employee>
            {
                new Employee { Id = 1, FullName = "Alice Johnson", Department = "HR", JoiningDate = new DateTime(2020, 5, 10) },
                new Employee { Id = 2, FullName = "Bob Smith", Department = "Engineering", JoiningDate = new DateTime(2019, 3, 1) }
            };

                Employees.AddRange(employees);
                SaveChanges();
            }

            if (!LeaveRequests.Any())
            {
                var leaveRequests = new List<LeaveRequest>
            {
                new LeaveRequest
                {
                    EmployeeId = 1,
                    LeaveType = LeaveType.Annual,
                    StartDate = DateTime.Today.AddDays(-10),
                    EndDate = DateTime.Today.AddDays(-5),
                    Status = LeaveStatus.Approved,
                    Reason = "Vacation",
                    CreatedAt = DateTime.Now.AddDays(-15)
                },
                new LeaveRequest
                {
                    EmployeeId = 2,
                    LeaveType = LeaveType.Sick,
                    StartDate = DateTime.Today.AddDays(-3),
                    EndDate = DateTime.Today,
                    Status = LeaveStatus.Rejected,
                    Reason = "Flu",
                    CreatedAt = DateTime.Now.AddDays(-4)
                }
            };

                LeaveRequests.AddRange(leaveRequests);
                SaveChanges();
            }
        }
    }

}
