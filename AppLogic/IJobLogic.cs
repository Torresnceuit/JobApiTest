using DataAccess.DataModel;
using ServiceModel;

namespace AppLogic;

public interface IJobLogic
{
    public Task<Job> CreateJobAsync(Job job);
    public Task<Job> GetJobAsync(Guid id);
    public Task<IEnumerable<Job>> GetJobsAsync(int skip, int take);
    public Task<IEnumerable<Job>> GetJobsAsync(JobStatus status, Guid clientId, string sort = "Time", string direction = "desc", int take = 100);
    public Task<IEnumerable<Note>> GetNotesAsync(Guid jobId);
}
