using LeaveManagementSystem.Data;
using LeaveManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace LeaveManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly AppDbContext appDbContext;

        public LeaveRequestsController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        //Get All LeaveRequest
        [HttpGet]
        public async Task<ActionResult<LeaveRequest>> GetLeaveRequests()
        {
            var leaveRequests = await appDbContext.LeaveRequests
        .Include(lr => lr.Employee)
        .ToListAsync();

            return Ok(leaveRequests);
        }
        //Get LeaveRequest By Id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<LeaveRequest>> GetLeaveRequestByID(int id)
        {
            var leaveRequest = await appDbContext.LeaveRequests.FirstOrDefaultAsync(e => e.Id == id);
            if (leaveRequest != null)
            {
                return Ok(leaveRequest);
            }
            return NotFound("LeaveRequest is not Found");
        }
        // Add New LeaveRequest
        [HttpPost]
        public async Task<ActionResult<LeaveRequest>> AddLeaveRequest(LeaveRequest leaveRequest)
        {
            if (leaveRequest != null)
            {
                appDbContext.LeaveRequests.Add(leaveRequest);
                await appDbContext.SaveChangesAsync();
                return Ok(await appDbContext.LeaveRequests.ToListAsync());
            }
            return BadRequest("Can't add new LeaveRequest");
        }
        // Update LeaveRequest
        [HttpPut]
        public async Task<ActionResult<LeaveRequest>> UpdateLeaveRequest(int id, LeaveRequest leaveRequest)
        {
            if (id != leaveRequest.Id) return BadRequest();

            appDbContext.Entry(leaveRequest).State = EntityState.Modified;
            await appDbContext.SaveChangesAsync();
            return NoContent();
        }
        //Delete LeaveRequest By ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveRequest(int id)
        {
            var leaveRequest = await appDbContext.LeaveRequests.FindAsync(id);
            if (leaveRequest == null) return NotFound();

            appDbContext.LeaveRequests.Remove(leaveRequest);
            await appDbContext.SaveChangesAsync();
            return NoContent();
        }
        // Get LeaveRequest with many Filters
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<LeaveRequest>>> FilterLeaveRequests(
            int? employeeId, LeaveType? leaveType, LeaveStatus? status, DateTime? startDate, DateTime? endDate,
            int page = 1, int pageSize = 10, string sortBy = "StartDate", string sortOrder = "asc", string keyword = "")
        {
            var query = appDbContext.LeaveRequests.AsQueryable();

            if (employeeId.HasValue) query = query.Where(lr => lr.EmployeeId == employeeId);
            if (leaveType.HasValue) query = query.Where(lr => lr.LeaveType == leaveType);
            if (status.HasValue) query = query.Where(lr => lr.Status == status);
            if (startDate.HasValue) query = query.Where(lr => lr.StartDate >= startDate);
            if (endDate.HasValue) query = query.Where(lr => lr.EndDate <= endDate);
            if (!string.IsNullOrEmpty(keyword)) query = query.Where(lr => lr.Reason.Contains(keyword));

            // Sorting
            if (sortOrder.ToLower() == "desc")
            {
                query = query.OrderByDescending(lr => EF.Property<object>(lr, sortBy));
            }
            else
            {
                query = query.OrderBy(lr => EF.Property<object>(lr, sortBy));
            }

            // Pagination
            var leaveRequests = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return Ok(leaveRequests);
        }

    }
}
