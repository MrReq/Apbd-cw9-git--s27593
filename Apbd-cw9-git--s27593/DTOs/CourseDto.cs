namespace Apbd_cw9_git__s27593.DTOs;

public class CourseDto
{
    public int CourseId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Ects { get; set; }

    public int AssignmentsCount { get; set; }
}