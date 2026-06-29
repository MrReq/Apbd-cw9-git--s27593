using Apbd_cw9_git__s27593.DTOs;
using Apbd_cw9_git__s27593.Services;
using Apbd_cw9_git__s27593.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Apbd_cw9_git__s27593.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubmissionsController : ControllerBase
{
    private readonly ISubmissionService _submissionService;

    public SubmissionsController(ISubmissionService submissionService)
    {
        _submissionService = submissionService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SubmissionDto>> GetSubmission(int id)
    {
        var submission = await _submissionService.GetSubmissionAsync(id);

        if (submission == null)
            return NotFound();

        return Ok(submission);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubmission(CreateSubmissionDto dto)
    {
        var submission = await _submissionService.CreateSubmissionAsync(dto);

        return CreatedAtAction(
            nameof(GetSubmission),
            new { id = submission.SubmissionId },
            submission.SubmissionId);
    }

    [HttpPut("{id}/grade")]
    public async Task<IActionResult> GradeSubmission(int id, GradeSubmissionDto dto)
    {
        var success = await _submissionService.GradeSubmissionAsync(id, dto);

        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubmission(int id)
    {
        var result = await _submissionService.DeleteSubmissionAsync(id);

        if (!result.Success)
            return BadRequest(result.Error);

        return NoContent();
    }
    

}