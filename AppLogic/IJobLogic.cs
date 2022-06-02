using ServiceModel;

namespace AppLogic;

public interface IJobLogic
{
    public Task<Job> CreateJobAsync(Job job);
    public Task<Job> GetJobAsync(Guid id);
    public Task<IEnumerable<Job>> GetJobsAsync(int skip, int take);
    public Task<IEnumerable<Note>> GetNotesAsync(Guid jobId);
}
