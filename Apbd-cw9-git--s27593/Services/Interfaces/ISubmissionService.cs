using Apbd_cw9_git__s27593.DTOs;
using Apbd_cw9_git__s27593.ModelsAndEntities;

namespace Apbd_cw9_git__s27593.Services.Interfaces;

public interface ISubmissionService
{
    public Task<SubmissionDto?> GetSubmissionAsync(int id);
    public Task<Submission> CreateSubmissionAsync(CreateSubmissionDto dto);
    public Task<bool> GradeSubmissionAsync(int id, GradeSubmissionDto dto);
    public Task<(bool Success, string? Error)> DeleteSubmissionAsync(int id);
}