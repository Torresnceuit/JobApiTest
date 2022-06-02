using DataAccess.DataModel;
using DataAccess.Repositories;
using ServiceModel;

namespace AppLogic;

public class JobLogic : IJobLogic
{
    private readonly IJobRepository _jobRepo;
    private readonly IObjectMapper _mapper;

    public JobLogic(IJobRepository jobRepo, IObjectMapper mapper)
    {
        _jobRepo = jobRepo ?? throw new ArgumentNullException(nameof(jobRepo));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Job> CreateJobAsync(Job job)
    {
        if (job is null)
        {
            throw new ArgumentNullException(nameof(job));
        }

        var dbObject = _mapper.Map<Job, DbJob>(job);
        var result = await _jobRepo.CreateJobAsync(dbObject).ConfigureAwait(false);
        return _mapper.Map<DbJob, Job>(result);
    }

    public async Task<Job> GetJobAsync(Guid id)
    {
        var result = await _jobRepo.GetJobAsync(id).ConfigureAwait(false);
        return _mapper.Map<DbJob, Job>(result);
    }

    public async Task<IEnumerable<Job>> GetJobsAsync(int skip = 0, int take = 100)
    {
        var result = await _jobRepo.GetJobsAsync(skip, take).ConfigureAwait(false);
        return _mapper.Map<DbJob, Job>(result);
    }
    public async Task<IEnumerable<Note>> GetNotesAsync(Guid jobId)
    {
        var result = await _jobRepo.GetNotesAsync(jobId).ConfigureAwait(false);
        return _mapper.Map<DbNote, Note>(result);
    }
}
