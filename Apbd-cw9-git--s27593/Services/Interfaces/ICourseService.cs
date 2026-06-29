using Apbd_cw9_git__s27593.DTOs;

namespace Apbd_cw9_git__s27593.Services.Interfaces;

public interface ICourseService
{
    public  Task<List<CourseDto>> GetCoursesAsync();

    public Task<List<AssignmentDto>?> GetAssignmentsAsync(int courseId);
}