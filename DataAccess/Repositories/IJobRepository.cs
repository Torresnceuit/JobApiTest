using DataAccess.DataModel;
namespace DataAccess.Repositories;

public interface IJobRepository
{
    public Task<DbJob> CreateJobAsync(DbJob job);
    public Task<DbJob?> GetJobAsync(Guid id);
    public Task<IReadOnlyList<DbJob>> GetJobsAsync(int skip, int take);
    public Task<IReadOnlyList<DbJob>> GetJobsAsync(JobStatus status, Guid clientId, string sort = "Time", string direction = "desc", int take = 100);
    public Task<IReadOnlyList<DbNote>> GetNotesAsync(Guid jobId);
}
