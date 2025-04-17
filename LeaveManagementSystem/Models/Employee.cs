using System.Text.Json.Serialization;

namespace LeaveManagementSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public DateTime JoiningDate { get; set; }

        // Navigation property for LeaveRequests
        [JsonIgnore]
        public List<LeaveRequest>? LeaveRequests { get; set; }
    }
}
