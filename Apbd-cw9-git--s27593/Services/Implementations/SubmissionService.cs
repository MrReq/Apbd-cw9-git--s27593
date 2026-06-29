using Apbd_cw9_git__s27593.DAL;
using Apbd_cw9_git__s27593.DTOs;
using Apbd_cw9_git__s27593.ModelsAndEntities;
using Apbd_cw9_git__s27593.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Apbd_cw9_git__s27593.Services;

public class SubmissionService : ISubmissionService
{
    private readonly UniversityTasksDbContext _context;

    public SubmissionService(UniversityTasksDbContext context)
    {
        _context = context;
    }

    public async Task<SubmissionDto?> GetSubmissionAsync(int id)
    {
        return await _context.Submissions
            .AsNoTracking()
            .Where(s => s.SubmissionId == id)
            .Select(s => new SubmissionDto
            {
                SubmissionId = s.SubmissionId,
                Student = s.Student.FirstName + " " + s.Student.LastName,
                Assignment = s.Assignment.Title,
                RepositoryUrl = s.RepositoryUrl,
                Status = s.Status,
                Score = s.Score,
                Feedback = s.Feedback
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Submission> CreateSubmissionAsync(CreateSubmissionDto dto)
    {
        var submission = new Submission
        {
            AssignmentId = dto.AssignmentId,
            StudentId = dto.StudentId,
            RepositoryUrl = dto.RepositoryUrl,
            SubmittedAt = DateTime.Now,
            Status = "Submitted"
        };

        _context.Submissions.Add(submission);
        await _context.SaveChangesAsync();

        return submission;
    }

    public async Task<bool> GradeSubmissionAsync(int id, GradeSubmissionDto dto)
    {
        var submission = await _context.Submissions
            .Include(s => s.Assignment)
            .FirstOrDefaultAsync(s => s.SubmissionId == id);

        if (submission == null)
            return false;

        submission.Score = dto.Score;
        submission.Feedback = dto.Feedback;
        submission.Status = "Graded";

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<(bool Success, string? Error)> DeleteSubmissionAsync(int id)
    {
        var submission = await _context.Submissions
            .FirstOrDefaultAsync(s => s.SubmissionId == id);

        if (submission == null)
            return (false, "Submission not found.");

        if (submission.Status == "Graded")
            return (false, "Graded submission cannot be deleted.");

        _context.Submissions.Remove(submission);

        await _context.SaveChangesAsync();

        return (true, null);
    }
}