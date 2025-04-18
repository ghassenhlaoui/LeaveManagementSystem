namespace LeaveManagementSystem.Models
{
    public enum LeaveType
    {
        Annual = 0,
        Sick = 1,
        Other = 2
    }

    public enum LeaveStatus
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2
    }

    public class LeaveRequest
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public LeaveType LeaveType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveStatus Status { get; set; }
        public string Reason { get; set; }
        public DateTime CreatedAt { get; set; }

        
        public Employee? Employee { get; set; }
    }

}
