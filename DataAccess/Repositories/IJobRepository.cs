using DataAccess.DataModel;
namespace DataAccess.Repositories;

public interface IJobRepository
{
    public Task<DbJob> CreateJobAsync(DbJob job);
    public Task<DbJob?> GetJobAsync(Guid id);
    public Task<IReadOnlyList<DbJob>> GetJobsAsync(int skip, int take);
    public Task<IReadOnlyList<DbNote>> GetNotesAsync(Guid jobId);
}
