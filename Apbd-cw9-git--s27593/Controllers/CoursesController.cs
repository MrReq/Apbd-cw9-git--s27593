using Apbd_cw9_git__s27593.DTOs;
using Apbd_cw9_git__s27593.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Apbd_cw9_git__s27593.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
    {
        return Ok(await _courseService.GetCoursesAsync());
    }

    [HttpGet("{id}/assignments")]
    public async Task<ActionResult<IEnumerable<AssignmentDto>>> GetAssignments(int id)
    {
        var assignments = await _courseService.GetAssignmentsAsync(id);

        if (assignments == null)
            return NotFound();

        return Ok(assignments);
    }
}