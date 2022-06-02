using DataAccess.DataModel;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly TechTestDbContext _techTestDbContext;

    public ClientRepository(TechTestDbContext techTestDbContext)
    {
        _techTestDbContext = techTestDbContext;
    }

    public async Task<DbClient> CreateClientAsync(DbClient client)
    {
        DbClient? exist = await _techTestDbContext.Client.Where(m => m.Id == client.Id).FirstOrDefaultAsync();
        if (exist == null)
        {
            exist = new DbClient();
            exist.Id = client.Id;
            await _techTestDbContext.AddAsync(exist);
        }

        exist.Update(client);
        exist.Jobs = _techTestDbContext.Job.Where(m => m.Id == client.Id).ToList();
        await _techTestDbContext.SaveChangesAsync();
        return exist;
    }

    public async Task<IReadOnlyList<DbClient>> GetClientsAsync(int skip = 0, int take = 100) => await _techTestDbContext.Client
        .Skip(skip)
        .Take(take)
        .Include(m => m.Jobs)
        .ToListAsync()
        .ConfigureAwait(continueOnCapturedContext: false);

    public Task<DbClient?> GetClientAsync(Guid id) => _techTestDbContext.Client.FirstOrDefaultAsync(x => x.Id == id);
    public async Task<IReadOnlyList<DbJob>> GetJobsAsync(Guid clientId) => await _techTestDbContext.Job
        .Where(m => m.ClientId == clientId)
        .Include(m => m.Client)
        .ToListAsync()
        .ConfigureAwait(false);
}
