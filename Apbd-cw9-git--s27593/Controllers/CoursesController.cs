using Apbd_cw9_git__s27593.DAL;
using Apbd_cw9_git__s27593.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Apbd_cw9_git__s27593.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly UniversityTasksDbContext _context;

    public CoursesController(UniversityTasksDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
    {
        var courses = await _context.Courses
            .AsNoTracking()
            .Select(c => new CourseDto
            {
                CourseId = c.CourseId,
                Code = c.Code,
                Name = c.Name,
                Ects = c.Credits,
                AssignmentsCount = c.Assignments.Count
            })
            .ToListAsync();

        return Ok(courses);
    }

    [HttpGet("{id}/assignments")]
    public async Task<ActionResult<IEnumerable<AssignmentDto>>> GetAssignments(int id)
    {
        var courseExists = await _context.Courses
            .AsNoTracking()
            .AnyAsync(c=>c.CourseId == id);

        if (!courseExists)
        {
            return NotFound("Course not found, this courseID does not exist");
        }
        
        var assigments = await _context.Assignments
            .AsNoTracking()
            .Where(a => a.CourseId == id)
            .Select(a => new AssignmentDto
            {
                AssignmentId = a.AssignmentId,
                Title = a.Title,
                DueDate =  a.DueDate,
                MaxPoints =  a.MaxPoints,
                IsPublished =  a.IsPublished,
                SubmissionsCount =  a.Submissions.Count
            }).ToListAsync();
        
        return Ok(assigments);
    }
}