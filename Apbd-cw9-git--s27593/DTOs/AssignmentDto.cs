namespace Apbd_cw9_git__s27593.DTOs;

public class AssignmentDto
{
    public int AssignmentId { get; set; }

    public string Title { get; set; } = null!;

    public DateTime DueDate { get; set; }

    public int MaxPoints { get; set; }

    public bool IsPublished { get; set; }

    public int SubmissionsCount { get; set; }
}