using Apbd_cw9_git__s27593.DAL;
using Apbd_cw9_git__s27593.DTOs;
using Apbd_cw9_git__s27593.ModelsAndEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Apbd_cw9_git__s27593.Controllers;

public class SubmissionsController : ControllerBase
{
    private readonly UniversityTasksDbContext _context;
    public SubmissionsController(UniversityTasksDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<SubmissionDto>> GetSubmission(int id)
    {
        var submission = await _context.Submissions
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

        if (submission == null)
            return NotFound();

        return Ok(submission);
    }

    [HttpGet]
    public async Task<IActionResult> CreateSubmissions(CreateSubmissionDto createSubmissionDto)
    {
        var assigmentexists = await _context.Assignments
            .AnyAsync(a => a.AssignmentId == createSubmissionDto.AssignmentId);
        
        if (!assigmentexists)
        {
            return BadRequest("Assignment not found, it does not exist");
        }
        
        var studentExist = await _context.Students
            .AnyAsync(s => s.StudentId == createSubmissionDto.StudentId);

        if (!studentExist)
        {
            return BadRequest("Student not found, he or she does not exist");
        }
        
        var alreadyAdded = await _context.Submissions
            .AnyAsync(sub => sub.AssignmentId == createSubmissionDto.AssignmentId 
            && sub.StudentId == createSubmissionDto.StudentId);

        if (alreadyAdded)
        {
            return BadRequest("Submission is already in use");
        }

        var submission = new Submission
        {
            AssignmentId = createSubmissionDto.AssignmentId,
            StudentId = createSubmissionDto.StudentId,
            RepositoryUrl = createSubmissionDto.RepositoryUrl,
            SubmittedAt = DateTime.Now,
            Status = "Submitted"
        };

        _context.Submissions.Add(submission);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(
            nameof(GetSubmission),
            new { id = submission.SubmissionId },
            submission.SubmissionId);
        
        return Ok(submission);
    }

    [HttpPut("{id}/grade")]
    public async Task<IActionResult> GradeSubmission(int id, GradeSubmissionDto gradeSubmissionDto)
    {
        var submission = await _context.Submissions
            .Include(s => s.Assignment)
            .FirstOrDefaultAsync(s => s.SubmissionId == id);

        if (submission == null)
        {
            return  BadRequest("Submission not found, it does not exist");
        }

        if (gradeSubmissionDto.Score < 0)
        {
            return BadRequest("Score is less than 0");
        }

        if (gradeSubmissionDto.Score > submission.Assignment.MaxPoints)
        {
            return BadRequest("Score is greater than maximum points");
        }
        
        submission.Score = gradeSubmissionDto.Score;
        submission.Feedback = gradeSubmissionDto.Feedback;
        submission.Status = "Submitted";
        
        await _context.SaveChangesAsync();
        
        
        return NoContent();
    }
}