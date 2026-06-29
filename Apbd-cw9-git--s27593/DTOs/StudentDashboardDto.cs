namespace Apbd_cw9_git__s27593.DTOs;

public class StudentDashboardDto
{
    public int StudentId { get; set; }

    public string IndexNumber { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public bool IsActive { get; set; }

    public int EnrollmentsCount { get; set; }

    public int SubmissionsCount { get; set; }
}