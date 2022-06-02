using DataAccess.DataModel;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class JobRepository : IJobRepository
{
    private readonly TechTestDbContext _techTestDbContext;

    public JobRepository(TechTestDbContext techTestDbContext)
    {
        _techTestDbContext = techTestDbContext;
    }

    public async Task<DbJob> CreateJobAsync(DbJob newJob)
    {
        if (!_techTestDbContext.Client.Select(x => x.Id).Contains(newJob.ClientId))
        {
            return null;
        }

        var exist = await _techTestDbContext.Job.FirstOrDefaultAsync(x => x.Id == newJob.Id);
        if (exist == null)
        {
            exist = new DbJob();
            exist.Id = newJob.Id;
            await _techTestDbContext.AddAsync(newJob);
        }

        exist.Update(newJob);
        await _techTestDbContext.SaveChangesAsync();
        return exist;
    }

    public async Task<IReadOnlyList<DbJob>> GetJobsAsync(int skip = 0, int take = 100) => await _techTestDbContext.Job
        .Skip(skip)
        .Take(take)
        .Include(m => m.Notes)
        .ToListAsync()
        .ConfigureAwait(continueOnCapturedContext: false);

    public Task<DbJob?> GetJobAsync(Guid id) => _techTestDbContext.Job.FirstOrDefaultAsync(x => x.Id == id);
    public async Task<IReadOnlyList<DbNote>> GetNotesAsync(Guid jobId) => await _techTestDbContext.Note
        .Where(m => m.JobId == jobId)
        .ToListAsync()
        .ConfigureAwait(false);
}
