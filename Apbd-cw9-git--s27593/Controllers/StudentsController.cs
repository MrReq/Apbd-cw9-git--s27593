using Apbd_cw9_git__s27593.DAL;
using Apbd_cw9_git__s27593.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Apbd_cw9_git__s27593.Controllers;

public class StudentsController : ControllerBase
{
    private readonly UniversityTasksDbContext _context;

    public StudentsController(UniversityTasksDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}/dashboard")]
    public async Task<ActionResult<StudentDashboardDto>> GetStudentDashboard(int id)
    {
        var student = await _context.Students
            .AsNoTracking()
            .Where(s => s.StudentId == id)
            .Select(s => new StudentDashboardDto
            {
                StudentId = s.StudentId,
                IndexNumber = s.IndexNumber,
                FullName = s.FirstName + " " + s.LastName,
                IsActive = s.IsActive,
                EnrollmentsCount = s.Enrollments.Count,
                SubmissionsCount = s.Submissions.Count
            })
            .FirstOrDefaultAsync();

        if (student == null)
            return NotFound();

        return Ok(student);
    }
}