using Apbd_cw9_git__s27593.DAL;
using Apbd_cw9_git__s27593.DTOs;
using Apbd_cw9_git__s27593.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Apbd_cw9_git__s27593.Services;

public class CourseService : ICourseService
{
    private readonly UniversityTasksDbContext _context;

    public CourseService(UniversityTasksDbContext context)
    {
        _context = context;
    }

    public async Task<List<CourseDto>> GetCoursesAsync()
    {
        return await _context.Courses
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
    }

    public async Task<List<AssignmentDto>?> GetAssignmentsAsync(int courseId)
    {
        var exists = await _context.Courses
            .AsNoTracking()
            .AnyAsync(c => c.CourseId == courseId);

        if (!exists)
            return null;

        return await _context.Assignments
            .AsNoTracking()
            .Where(a => a.CourseId == courseId)
            .Select(a => new AssignmentDto
            {
                AssignmentId = a.AssignmentId,
                Title = a.Title,
                DueDate = a.DueDate,
                MaxPoints = a.MaxPoints,
                IsPublished = a.IsPublished,
                SubmissionsCount = a.Submissions.Count
            })
            .ToListAsync();
    }
}